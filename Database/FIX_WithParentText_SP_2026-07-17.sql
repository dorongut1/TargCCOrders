/* ============================================================================
   FIX: make @WithParentText optional on all CC Fill/Get stored procedures  (v2)
   ----------------------------------------------------------------------------
   The installed CC version regenerated the DB stored procedures with a new
   REQUIRED parameter @WithParentText. The (git-restored) VB code is older and
   does not pass it, so every Fill call fails with:
     "Procedure 'ccXxxFill' expects parameter '@WithParentText', which was not supplied."

   This rebuilds each such procedure with @WithParentText defaulted to 0
   (= the old code's behaviour). Uses sys.parameters for precise detection and
   the real parameter type, so it works regardless of the type spelling.

   Idempotent, prints what it changed. Run on: TargCCOrdersNew
   ============================================================================ */

SET NOCOUNT ON;

DECLARE @objid   INT,
        @schema  SYSNAME,
        @name    SYSNAME,
        @def     NVARCHAR(MAX),
        @new     NVARCHAR(MAX),
        @type    SYSNAME,
        @pos     INT,
        @fixed   INT = 0,
        @err     INT = 0;

DECLARE cur CURSOR LOCAL FAST_FORWARD FOR
    SELECT o.object_id, s.name, o.name, m.definition
    FROM sys.objects o
    JOIN sys.schemas s      ON s.schema_id = o.schema_id
    JOIN sys.sql_modules m  ON m.object_id = o.object_id
    WHERE o.type = 'P'
      AND EXISTS (SELECT 1 FROM sys.parameters p
                  WHERE p.object_id = o.object_id
                    AND p.name = '@WithParentText'
                    AND p.has_default_value = 0);

OPEN cur;
FETCH NEXT FROM cur INTO @objid, @schema, @name, @def;

WHILE @@FETCH_STATUS = 0
BEGIN
    -- real declared type of the parameter (e.g. bit, tinyint, int)
    SELECT @type = TYPE_NAME(p.user_type_id)
    FROM sys.parameters p
    WHERE p.object_id = @objid AND p.name = '@WithParentText';

    SET @new = @def;
    -- add "= 0" right after the parameter's type, covering [type], type, and tab-separated
    SET @new = REPLACE(@new, '@WithParentText [' + @type + ']', '@WithParentText [' + @type + '] = 0');
    SET @new = REPLACE(@new, '@WithParentText '  + @type,       '@WithParentText '  + @type + ' = 0');
    SET @new = REPLACE(@new, '@WithParentText' + CHAR(9) + @type, '@WithParentText ' + @type + ' = 0');

    IF @new <> @def
    BEGIN
        SET @pos = CHARINDEX('CREATE', @new);
        IF @pos > 0 SET @new = STUFF(@new, @pos, 6, 'ALTER');
        BEGIN TRY
            EXEC sys.sp_executesql @new;
            SET @fixed = @fixed + 1;
            PRINT 'FIXED : ' + @schema + '.' + @name + '  (' + @type + ')';
        END TRY
        BEGIN CATCH
            SET @err = @err + 1;
            PRINT 'ERROR : ' + @schema + '.' + @name + '  -> ' + ERROR_MESSAGE();
        END CATCH
    END
    ELSE
    BEGIN
        SET @err = @err + 1;
        PRINT 'SKIP  : ' + @schema + '.' + @name + '  (type=' + ISNULL(@type,'?') + ' - pattern not found, tell Claude)';
    END

    FETCH NEXT FROM cur INTO @objid, @schema, @name, @def;
END

CLOSE cur;
DEALLOCATE cur;

PRINT '----------------------------------------';
PRINT 'Procedures fixed:   ' + CAST(@fixed AS VARCHAR(10));
PRINT 'Errors / skipped:   ' + CAST(@err AS VARCHAR(10));
GO
