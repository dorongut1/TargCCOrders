/*  CREATE_EnumMetadata_2026-08-18.sql

    Display metadata for the values in c_Enumeration: sort order, whether a
    value is still offered for new records, and per-type flags such as
    IsDelivery.

    Deliberately NOT a TargCC table. It has no VB entity and no generated
    stored procedures, so it is reached with plain SQL from the API. That
    keeps it entirely outside the ordinal-drift pattern -- adding a column
    here can never desynchronise a positional read in the VB layer, which is
    the failure that has bitten this project three times.

    What lives where:
      c_Enumeration   the value and its Hebrew label (locText)
      EnumMetadata    how it is shown and whether it is offered
      csSptEnums.vb   the values themselves (adding one is still a code change)

    Safe to run more than once.

    Run with:
      sqlcmd -S localhost -d TargCCOrdersNew -E -I -f 65001 -b -i CREATE_EnumMetadata_2026-08-18.sql
*/

SET NOCOUNT ON;

IF OBJECT_ID(N'dbo.EnumMetadata', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.EnumMetadata
    (
        ID          BIGINT IDENTITY(1,1) NOT NULL CONSTRAINT PK_EnumMetadata PRIMARY KEY,
        EnumType    NVARCHAR(100)  NOT NULL,
        EnumValue   NVARCHAR(100)  NOT NULL,
        -- Hidden values stay valid on existing records; they are simply no
        -- longer offered when creating one. Never delete a value that history
        -- refers to.
        IsActive    BIT            NOT NULL CONSTRAINT DF_EnumMetadata_IsActive DEFAULT (1),
        -- Delivery methods handled by the Deliver application (Elkana, Lior).
        -- Meaningless for other enum types and simply left 0 there.
        IsDelivery  BIT            NOT NULL CONSTRAINT DF_EnumMetadata_IsDelivery DEFAULT (0),
        SortOrder   INT            NOT NULL CONSTRAINT DF_EnumMetadata_SortOrder DEFAULT (0),
        ChangedBy   NVARCHAR(100)  NULL,
        ChangedOn   DATETIME       NULL,
        CONSTRAINT UQ_EnumMetadata_TypeValue UNIQUE (EnumType, EnumValue)
    );
END;

/*  Seed a row for every value that has none, so the screen shows the full
    list from the first load. Alphabetical seeding of SortOrder just gives a
    stable starting point; the screen owns it from then on.                */
INSERT INTO dbo.EnumMetadata (EnumType, EnumValue, IsActive, IsDelivery, SortOrder, ChangedBy, ChangedOn)
SELECT e.EnumType,
       e.EnumValue,
       1,
       CASE WHEN e.EnumType = N'DeliveryMethod' AND e.EnumValue IN (N'Elkana', N'LiorCarmiel')
            THEN 1 ELSE 0 END,
       ROW_NUMBER() OVER (PARTITION BY e.EnumType ORDER BY e.EnumValue) * 10,
       N'CREATE_EnumMetadata_2026-08-18',
       GETDATE()
FROM c_Enumeration e
WHERE e.DeletedOn IS NULL
  AND e.EnumType IS NOT NULL
  AND e.EnumValue IS NOT NULL
  AND NOT EXISTS (
      SELECT 1 FROM dbo.EnumMetadata m
      WHERE m.EnumType = e.EnumType AND m.EnumValue = e.EnumValue
  );

/*  Elkana and Lior are the two areas worked through the Deliver application;
    everything else is shown on the board but never exported. Re-applied on
    every run so a fresh database lands in the right state.                */
UPDATE dbo.EnumMetadata
SET IsDelivery = 1
WHERE EnumType = N'DeliveryMethod'
  AND EnumValue IN (N'Elkana', N'LiorCarmiel')
  AND IsDelivery = 0;

SELECT N'EnumMetadata rows: ' + CAST(COUNT(*) AS NVARCHAR(10)) FROM dbo.EnumMetadata;
SELECT N'Marked IsDelivery: ' + CAST(COUNT(*) AS NVARCHAR(10)) FROM dbo.EnumMetadata WHERE IsDelivery = 1;
