/* ============================================================================
   Master-data import from the legacy workbook — STAGING LOAD (step 1 of 2)
   ----------------------------------------------------------------------------
   Loads the three CSVs exported from the 2022 workbook into stg_ tables as raw
   strings, with no constraints. Nothing here touches the live tables; that is
   step 2, after the cleaning report has been reviewed.

   Columns are positional (c1..cN) because the sheets' Hebrew headers are
   inconsistent, partly blank, and not on row 1. The meaning of each column is
   recorded below from the surveyed data.

     customers.csv  (1433 rows, header was on row 2)
       c1  running number          c2  Rivhit customer no      c3  customer name
       c4  phone (sometimes with a contact name attached)
       c5  location               c6  invoice name            c7  tax id / national id
       c8  address                c10 email                   c11 DEFAULT DELIVERY METHOD
       c12 CUSTOMER TYPE          c13 payment terms (days)    c14 notes
       c15 "code name" composite key used by other sheets

     pricelist.csv  (117 rows, header was on row 3)
       c1  product code           c2  product name            c3  purchase cost (Biobee)
       c4  price: private         c5  price: farmer           c6  price: hydro/retail
       c7  min quantity           c9  category                c10 composite key

     beehives.csv   (1432 rows)
       c1  "code name" -> customer   c2  last order date      c3  hive quantity
       c4  phone                     c5  notes                c6  reminder month

   NOTE c11 vs c12: c11 holds values like ביובי / נצח / דואר — those are
   delivery methods, not customer classes. The class that drives pricing is c12
   (פרטי / חקלאים / ...). Mixing these two up would misprice every customer.

   Run with: sqlcmd -I   (OrderHeader indexes require QUOTED_IDENTIFIER ON)
   ============================================================================ */

USE TargCCOrdersNew;
GO
SET QUOTED_IDENTIFIER ON;
SET ANSI_NULLS ON;
GO

DROP TABLE IF EXISTS dbo.stg_Customer;
DROP TABLE IF EXISTS dbo.stg_PriceList;
DROP TABLE IF EXISTS dbo.stg_Beehive;
GO

CREATE TABLE dbo.stg_Customer (
    c1 nvarchar(200) NULL, c2 nvarchar(200) NULL, c3 nvarchar(500) NULL,
    c4 nvarchar(200) NULL, c5 nvarchar(200) NULL, c6 nvarchar(500) NULL,
    c7 nvarchar(200) NULL, c8 nvarchar(1000) NULL, c9 nvarchar(500) NULL,
    c10 nvarchar(500) NULL, c11 nvarchar(200) NULL, c12 nvarchar(200) NULL,
    c13 nvarchar(200) NULL, c14 nvarchar(1000) NULL, c15 nvarchar(500) NULL
);

CREATE TABLE dbo.stg_PriceList (
    c1 nvarchar(200) NULL, c2 nvarchar(500) NULL, c3 nvarchar(200) NULL,
    c4 nvarchar(200) NULL, c5 nvarchar(200) NULL, c6 nvarchar(200) NULL,
    c7 nvarchar(200) NULL, c8 nvarchar(500) NULL, c9 nvarchar(200) NULL,
    c10 nvarchar(500) NULL
);

CREATE TABLE dbo.stg_Beehive (
    c1 nvarchar(500) NULL, c2 nvarchar(200) NULL, c3 nvarchar(200) NULL,
    c4 nvarchar(200) NULL, c5 nvarchar(1000) NULL, c6 nvarchar(500) NULL
);
GO

BULK INSERT dbo.stg_Customer
FROM 'C:\Dev\NonTFS\TargCCOrders\Database\import\customers.csv'
WITH (FORMAT='CSV', FIRSTROW=2, CODEPAGE='65001', FIELDQUOTE='"',
      ROWTERMINATOR='0x0d0a', TABLOCK);

BULK INSERT dbo.stg_PriceList
FROM 'C:\Dev\NonTFS\TargCCOrders\Database\import\pricelist.csv'
WITH (FORMAT='CSV', FIRSTROW=2, CODEPAGE='65001', FIELDQUOTE='"',
      ROWTERMINATOR='0x0d0a', TABLOCK);

BULK INSERT dbo.stg_Beehive
FROM 'C:\Dev\NonTFS\TargCCOrders\Database\import\beehives.csv'
WITH (FORMAT='CSV', FIRSTROW=2, CODEPAGE='65001', FIELDQUOTE='"',
      ROWTERMINATOR='0x0d0a', TABLOCK);
GO

SELECT 'stg_Customer' AS t, COUNT(*) AS n FROM dbo.stg_Customer
UNION ALL SELECT 'stg_PriceList', COUNT(*) FROM dbo.stg_PriceList
UNION ALL SELECT 'stg_Beehive', COUNT(*) FROM dbo.stg_Beehive;
GO
