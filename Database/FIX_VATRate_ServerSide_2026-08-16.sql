/* ============================================================================
   FIX: VAT rate is resolved SERVER-SIDE, never trusted from the client
   ----------------------------------------------------------------------------
   ROOT CAUSE (confirmed against the live DB, 2026-08-16):
     ccOrderHeaderUpdate declares  @VATRatePercent decimal(5,2) = 0
     and the VB DBController never supplies it (0 occurrences of
     "VATRatePercent" in any .vb file in the solution). So EVERY save --
     insert and update alike -- wrote 0 into OrderHeader.VATRatePercent.
     trg_OrderLine_RecalcHeaderTotals then computes
         clc_VATAmount = TotalAmount * oh.VATRatePercent / 100
     i.e. multiplies by zero. Screens showed 18% while the DB stored 0.

   THE FIX:
     1. @VATRatePercent becomes OPTIONAL (default NULL).
     2. When it is NULL or 0 the procedure resolves the rate itself:
          INSERT -> c_SystemDefault (Group='Business', 'VATRatePercent'),
                    falling back to 18.00
          UPDATE -> preserve the order's existing rate (snapshot semantics:
                    a 2024 order stays at 17%), and only fall back to the
                    system default if the stored rate is also 0.
     3. After writing the header it recomputes clc_VATAmount /
        clc_TotalWithVAT from clc_TotalAmount, because the line trigger only
        fires on OrderLine changes -- a header-only save would otherwise
        leave the money fields stale.

   The client CANNOT set the rate. That is deliberate: a browser must not be
   able to decide the VAT on an invoice.

   WARNING: this is a CC-generated procedure. Re-running TargCC will
   overwrite it. Re-run this script afterwards.

   Run on: TargCCOrdersNew
   ============================================================================ */

USE TargCCOrdersNew;
GO

/* OrderHeader carries indexes that require these SET options. They must be ON
   both when the objects below are CREATED and when they RUN, otherwise
   Msg 1934. sqlcmd defaults QUOTED_IDENTIFIER to OFF -- run this file with
   sqlcmd -I, or from SSMS where it is already ON. */
SET QUOTED_IDENTIFIER ON;
SET ANSI_NULLS ON;
GO

/* ---------------------------------------------------------------------------
   1. Helper: single place that answers "what is the current VAT rate?"
   --------------------------------------------------------------------------- */
CREATE OR ALTER FUNCTION dbo.fnCurrentVATRate()
RETURNS decimal(5,2)
AS
BEGIN
    DECLARE @rate decimal(5,2);

    SELECT TOP 1 @rate = TRY_CONVERT(decimal(5,2), spt_SettingValue)
    FROM dbo.c_SystemDefault
    WHERE [Group] = 'Business'
      AND SettingName = 'VATRatePercent'
      AND DeletedOn IS NULL;

    -- 18% is the statutory Israeli rate from 1.1.2025; used only if the
    -- setting row is missing or unreadable.
    RETURN ISNULL(NULLIF(@rate, 0), 18.00);
END
GO

/* ---------------------------------------------------------------------------
   2. ccOrderHeaderUpdate -- rate resolved server-side
   --------------------------------------------------------------------------- */
CREATE OR ALTER PROCEDURE [dbo].[ccOrderHeaderUpdate]
  (
    @ID bigint
  , @OrderNumber int
  , @CustomerID bigint
  , @OrderDate datetime
  , @enmPaymentMethod nvarchar(50)
  , @enmPaymentStatus nvarchar(50)
  , @PaymentDate date
  , @InvoiceNumber nvarchar(50)
  , @enmDeliveryMethod nvarchar(50)
  , @DeliveryDate date
  , @enmDeliveryDay nvarchar(10)
  , @enmOrderStatus nvarchar(50)
  , @Notes nvarchar(max)
  , @Notes2 nvarchar(max)
  , @VATRatePercent decimal(5,2) = NULL   -- optional; ignored when 0/NULL
  , @ChangedBy nvarchar(50)
  )
AS
  SET NOCOUNT ON

  DECLARE @EffectiveVAT decimal(5,2)

  IF (@ID = 0)
    BEGIN
    -- New order: snapshot today's rate.
    SET @EffectiveVAT = ISNULL(NULLIF(@VATRatePercent, 0), dbo.fnCurrentVATRate())

    INSERT INTO [dbo].[OrderHeader]
      ([OrderNumber],[CustomerID],[OrderDate],[enmPaymentMethod],[enmPaymentStatus]
      ,[PaymentDate],[InvoiceNumber],[enmDeliveryMethod],[DeliveryDate],[enmDeliveryDay]
      ,[enmOrderStatus],[Notes],[Notes2],[VATRatePercent],[AddedBy])
    VALUES
      (@OrderNumber,@CustomerID,@OrderDate,@enmPaymentMethod,@enmPaymentStatus
      ,@PaymentDate,@InvoiceNumber,@enmDeliveryMethod,@DeliveryDate,@enmDeliveryDay
      ,@enmOrderStatus,@Notes,@Notes2,@EffectiveVAT,@ChangedBy)

    SET @ID = SCOPE_IDENTITY()
    END
  ELSE
    BEGIN
    -- Existing order: keep its own historical rate unless it is 0/NULL.
    SELECT @EffectiveVAT = ISNULL(NULLIF(@VATRatePercent, 0),
                                  ISNULL(NULLIF(VATRatePercent, 0), dbo.fnCurrentVATRate()))
    FROM [dbo].[OrderHeader] WHERE [ID] = @ID

    SET @EffectiveVAT = ISNULL(@EffectiveVAT, dbo.fnCurrentVATRate())

    UPDATE [dbo].[OrderHeader] WITH(ROWLOCK)
    SET
       [OrderNumber] = @OrderNumber
      ,[CustomerID] = @CustomerID
      ,[OrderDate] = @OrderDate
      ,[enmPaymentMethod] = @enmPaymentMethod
      ,[enmPaymentStatus] = @enmPaymentStatus
      ,[PaymentDate] = @PaymentDate
      ,[InvoiceNumber] = @InvoiceNumber
      ,[enmDeliveryMethod] = @enmDeliveryMethod
      ,[DeliveryDate] = @DeliveryDate
      ,[enmDeliveryDay] = @enmDeliveryDay
      ,[enmOrderStatus] = @enmOrderStatus
      ,[Notes] = @Notes
      ,[Notes2] = @Notes2
      ,[VATRatePercent] = @EffectiveVAT
      ,[ChangedBy] = @ChangedBy
      ,[ChangedOn] = GETDATE()
    WHERE ([ID] = @ID)
    END

  /* Re-derive the money fields from the (possibly new) rate. The OrderLine
     trigger only fires on line changes, so a header-only save must do this
     itself or the totals go stale. */
  UPDATE [dbo].[OrderHeader]
  SET clc_VATAmount    = ROUND(ISNULL(clc_TotalAmount,0) * VATRatePercent / 100.0, 2)
     ,clc_TotalWithVAT = ROUND(ISNULL(clc_TotalAmount,0), 2)
                       + ROUND(ISNULL(clc_TotalAmount,0) * VATRatePercent / 100.0, 2)
  WHERE [ID] = @ID

  SELECT @ID AS ID

  RETURN
GO

/* ---------------------------------------------------------------------------
   3. Repair the damaged rows: every order saved through the Web UI since the
      VATRatePercent column was introduced carries rate 0 and VAT 0.
      Orders before 1.1.2025 get the old statutory 17%.
   --------------------------------------------------------------------------- */
UPDATE dbo.OrderHeader
SET VATRatePercent = CASE WHEN OrderDate < '2025-01-01' THEN 17.00
                          ELSE dbo.fnCurrentVATRate() END
WHERE ISNULL(VATRatePercent, 0) = 0;
GO

UPDATE dbo.OrderHeader
SET clc_VATAmount    = ROUND(ISNULL(clc_TotalAmount,0) * VATRatePercent / 100.0, 2)
   ,clc_TotalWithVAT = ROUND(ISNULL(clc_TotalAmount,0), 2)
                     + ROUND(ISNULL(clc_TotalAmount,0) * VATRatePercent / 100.0, 2)
WHERE ISNULL(clc_VATAmount,0) <> ROUND(ISNULL(clc_TotalAmount,0) * VATRatePercent / 100.0, 2);
GO

PRINT 'FIX_VATRate_ServerSide_2026-08-16 applied.';
GO
