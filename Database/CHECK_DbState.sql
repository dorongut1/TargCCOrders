/*  CHECK_DbState.sql

    Reports what state a local database is in and which scripts it still
    needs. Read-only -- changes nothing.

    Exists because more than one developer now has a copy taken at a different
    time, and "run everything again" is a poor answer when the scripts are
    cheap but the confusion is not.

    Run with:
      sqlcmd -S localhost -d TargCCOrdersNew -E -I -f 65001 -b -i CHECK_DbState.sql
*/

SET NOCOUNT ON;

PRINT '';
PRINT '=== Health ===';

SELECT N'tables      : ' + CAST(COUNT(*) AS NVARCHAR(10)) + N'   (expected 41+)'
FROM sys.tables;

SELECT N'procedures  : ' + CAST(COUNT(*) AS NVARCHAR(10)) + N'   (expected 831; near zero means a schema-only copy, not usable)'
FROM sys.procedures;

SELECT N'clr enabled : ' + CAST(value_in_use AS NVARCHAR(10)) + N'   (must be 1, otherwise every endpoint fails while the log says the connection is fine)'
FROM sys.configurations WHERE name = 'clr enabled';

SELECT N'customers   : ' + CAST(COUNT(*) AS NVARCHAR(10)) FROM Customer WHERE DeletedOn IS NULL;
SELECT N'orders      : ' + CAST(COUNT(*) AS NVARCHAR(10)) FROM OrderHeader WHERE DeletedOn IS NULL;

PRINT '';
PRINT '=== Scripts still needed ===';

/* ADD_DeliveryMethods_2026-08-18.sql */
DECLARE @deliveryRows INT =
    (SELECT COUNT(*) FROM c_Enumeration
     WHERE EnumType = N'DeliveryMethod' AND DeletedOn IS NULL);

SELECT CASE WHEN @deliveryRows >= 18
    THEN N'[ok]   ADD_DeliveryMethods_2026-08-18.sql   already applied (' + CAST(@deliveryRows AS NVARCHAR(10)) + N' rows)'
    ELSE N'[RUN]  ADD_DeliveryMethods_2026-08-18.sql   -- only ' + CAST(@deliveryRows AS NVARCHAR(10)) + N' rows, expected 18'
END;

/* CREATE_EnumMetadata_2026-08-18.sql */
SELECT CASE WHEN OBJECT_ID(N'dbo.EnumMetadata', N'U') IS NULL
    THEN N'[RUN]  CREATE_EnumMetadata_2026-08-18.sql   -- table missing'
    ELSE N'[ok]   CREATE_EnumMetadata_2026-08-18.sql   already applied'
END;

/* Earlier fixes, in case the copy predates them */
SELECT CASE WHEN COL_LENGTH('OrderHeader', 'VATRatePercent') IS NULL
    THEN N'[RUN]  FIX_VATRate_ServerSide_2026-08-16.sql -- VATRatePercent column missing'
    ELSE N'[ok]   FIX_VATRate_ServerSide_2026-08-16.sql already applied'
END;

SELECT CASE WHEN COL_LENGTH('Customer', 'RivhitCustomerNo') IS NULL
    THEN N'[RUN]  DB_MIGRATION_2026-07-16.sql           -- RivhitCustomerNo column missing'
    ELSE N'[ok]   DB_MIGRATION_2026-07-16.sql           already applied'
END;

PRINT '';
PRINT '=== IsDelivery flags (expected: Elkana, LiorCarmiel) ===';

IF OBJECT_ID(N'dbo.EnumMetadata', N'U') IS NOT NULL
    SELECT N'  ' + EnumValue FROM dbo.EnumMetadata
    WHERE EnumType = N'DeliveryMethod' AND IsDelivery = 1
    ORDER BY EnumValue;
ELSE
    SELECT N'  (EnumMetadata does not exist yet)';

PRINT '';
PRINT 'Anything marked [RUN] should be run from the Database folder, in the order listed above.';
