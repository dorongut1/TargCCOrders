/*  ADD_DeliveryMethods_2026-08-18.sql

    Adds the four delivery methods the business maintains in the live
    workbook's settings sheet but which the system never had:
    Elkana, YDM, BeerTuvia, BGabriel.

    Why this matters beyond a dropdown: an order carrying a value the enum
    does not know is read as UD and rewritten to 'UD' on the first save, with
    no error anywhere. Importing the 2,818 orders from the workbook before
    these exist would silently destroy their delivery method. See
    SPIKE_1.1_ENUM_EXTENSIBILITY_2026-08-18.md.

    This script covers the label only. The values themselves live in the VB
    enum (csSptEnums.vb) and both halves are required -- a row here without
    the enum member appears in no dropdown, because /api/enums builds its list
    from Enum.GetValues.

    Safe to run more than once.

    Run with:
      sqlcmd -S localhost -d TargCCOrdersNew -E -I -f 65001 -b -i ADD_DeliveryMethods_2026-08-18.sql
*/

SET NOCOUNT ON;

DECLARE @NewValues TABLE (EnumValue NVARCHAR(100), locText NVARCHAR(200));

INSERT INTO @NewValues (EnumValue, locText) VALUES
    (N'Elkana',    N'אלקנה'),
    (N'YDM',       N'YDM'),
    (N'BeerTuvia', N'באר טוביה'),
    (N'BGabriel',  N'ב. גבריאל');

INSERT INTO c_Enumeration (EnumType, EnumValue, locText, AddedBy, AddedOn)
SELECT N'DeliveryMethod', n.EnumValue, n.locText, N'ADD_DeliveryMethods_2026-08-18', GETDATE()
FROM @NewValues n
WHERE NOT EXISTS (
    SELECT 1 FROM c_Enumeration e
    WHERE e.EnumType = N'DeliveryMethod'
      AND e.EnumValue = n.EnumValue
      AND e.DeletedOn IS NULL
);

SELECT N'DeliveryMethod rows now: ' + CAST(COUNT(*) AS NVARCHAR(10))
FROM c_Enumeration
WHERE EnumType = N'DeliveryMethod' AND DeletedOn IS NULL;
