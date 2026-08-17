/* ============================================================================
   Post-import cleanup
   ----------------------------------------------------------------------------
   1. ProductPrice rows whose enmCustomerType holds the Hebrew label instead of
      the enum member. TargCC stores enums as their ENGLISH name and resolves the
      Hebrew for display from c_Enumeration.locText, so these hand-entered rows
      (AddedBy = 'Admin') can never be matched by the pricing lookup. They are
      dead weight that also makes a product look like it has seven prices when it
      has three. Soft-deleted, not removed, so the rows remain auditable.

   2. Test orders left over from smoke testing. They would appear on the CEO's
      dashboard as cancelled orders worth nothing.

   3. The corrupt user row (UserName = 'False'), created by a call that passed
      its arguments in the wrong order: FirstName = 'NamePassword',
      LastName = '12', PhoneNumber = 'True', RoleID = 0.

   Run with: sqlcmd -I -f 65001
   ============================================================================ */

USE TargCCOrdersNew;
GO
SET QUOTED_IDENTIFIER ON;
SET ANSI_NULLS ON;
SET XACT_ABORT ON;
GO

BEGIN TRANSACTION;

/* 1. legacy Hebrew-typed price rows */
DECLARE @validTypes TABLE (v nvarchar(100) PRIMARY KEY);
INSERT INTO @validTypes (v)
SELECT DISTINCT EnumValue FROM dbo.c_Enumeration WHERE EnumType = 'CustomerType';

DELETE FROM dbo.ProductPrice
WHERE enmCustomerType NOT IN (SELECT v FROM @validTypes);
PRINT 'legacy price rows removed: ' + CAST(@@ROWCOUNT AS varchar);
GO

/* 2. smoke-test orders. Lines first, then any delivery rows the trigger made,
      then the headers. */
DECLARE @testOrders TABLE (ID bigint PRIMARY KEY);
INSERT INTO @testOrders (ID) VALUES (18), (21), (24), (25), (26), (10018);

DELETE FROM dbo.OrderLine WHERE OrderHeaderID IN (SELECT ID FROM @testOrders);
PRINT 'test order lines removed: ' + CAST(@@ROWCOUNT AS varchar);

DELETE FROM dbo.Delivery WHERE OrderHeaderID IN (SELECT ID FROM @testOrders);
PRINT 'test deliveries removed: ' + CAST(@@ROWCOUNT AS varchar);

DELETE FROM dbo.OrderHeader WHERE ID IN (SELECT ID FROM @testOrders);
PRINT 'test orders removed: ' + CAST(@@ROWCOUNT AS varchar);

/* 3. the corrupt user row */
DELETE FROM dbo.c_User WHERE UserName = 'False' AND ISNULL(RoleID, 0) = 0;
PRINT 'corrupt user rows removed: ' + CAST(@@ROWCOUNT AS varchar);

COMMIT TRANSACTION;
GO

/* staging tables are no longer needed */
DROP TABLE IF EXISTS dbo.stg_Customer;
DROP TABLE IF EXISTS dbo.stg_PriceList;
DROP TABLE IF EXISTS dbo.stg_Beehive;
GO

PRINT '=== final state ===';
SELECT 'Customer' AS t, COUNT(*) AS n FROM dbo.Customer
UNION ALL SELECT 'Product', COUNT(*) FROM dbo.Product
UNION ALL SELECT 'ProductPrice', COUNT(*) FROM dbo.ProductPrice
UNION ALL SELECT 'BeehiveBuyerTracking', COUNT(*) FROM dbo.BeehiveBuyerTracking
UNION ALL SELECT 'OrderHeader', COUNT(*) FROM dbo.OrderHeader
UNION ALL SELECT 'c_User', COUNT(*) FROM dbo.c_User;
GO
