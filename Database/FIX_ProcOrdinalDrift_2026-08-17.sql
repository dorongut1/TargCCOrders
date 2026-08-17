/* ============================================================================
   FIX: column-ordinal drift in the Customer and CustomerDebt SELECT procedures
   ----------------------------------------------------------------------------
   The generated VB reads result columns BY POSITION. When columns were added to
   these tables the procedures' SELECT lists were updated but the VB was not
   regenerated, so the positions no longer agree:

     Customer      VB expects AddedOn at 18; the procedure returned
                   RivhitCustomerNo (int) there
                   -> "Unable to cast object of type 'System.Int32' to
                       type 'System.DateTime'"
     CustomerDebt  VB expects clc_RemainingAmount at 5; the procedure returned
                   it last, at 12, shifting DebtDate/DueDate/DeliveryDate
                   -> "Unable to cast object of type 'System.DateTime' to
                       type 'System.Decimal'"

   Both faults were DORMANT while the affected columns were NULL for every row,
   because the generated code guards each read with IsDBNull. Importing the real
   customer data populated RivhitCustomerNo and both endpoints began failing.
   This is the same failure mode as the VATRatePercent incident of 2026-08-09.

   The procedures are corrected rather than the VB, because the VB is generated
   and would be overwritten. Re-running TargCC WILL regenerate these procedures
   and reintroduce the drift — re-apply this script afterwards.

   Generated 2026-08-17T07:30. Run with: sqlcmd -I -f 65001
   ============================================================================ */

USE TargCCOrdersNew;
GO
SET QUOTED_IDENTIFIER ON;
SET ANSI_NULLS ON;
GO


/* ---- ccCustomerGetByCustomerCode ---- */
CREATE OR ALTER PROCEDURE [dbo].[ccCustomerGetByCustomerCode] 
  ( 
     @CustomerCode nvarchar(50)  
  ) 
AS   
  SET NOCOUNT ON 
 
      SELECT  
          [Customer].[ID]
        , [Customer].[CustomerCode]
        , [Customer].[CustomerName]
        , [Customer].[Phone]
        , [Customer].[Email]
        , [Customer].[Address]
        , [Customer].[City]
        , [Customer].[TaxID]
        , [Customer].[enmCustomerType]
        , [Customer].[PaymentTermsDays]
        , [Customer].[Notes]
        , [Customer].[blg_IsActive]
        , [Customer].[Location]
        , [Customer].[AccountantEmail]
        , [Customer].[enmAccountantMethod]
        , [Customer].[InvoiceName]
        , [Customer].[ProfitabilityCode]
        , [Customer].[clc_CustomerIdentifier]
        --Auditing 
        , [Customer].[AddedOn]
        , [Customer].[RivhitCustomerNo]
        , [Customer].[enmDefaultDeliveryMethod] 
      FROM [Customer] 
      WHERE  
            ([Customer].[CustomerCode] = @CustomerCode) 
       
   
  RETURN
GO


/* ---- ccCustomerGetByID ---- */
CREATE OR ALTER PROCEDURE [dbo].[ccCustomerGetByID] 
  ( 
     @ID bigint  
  ) 
AS   
  SET NOCOUNT ON 
 
      SELECT  
          [Customer].[ID]
        , [Customer].[CustomerCode]
        , [Customer].[CustomerName]
        , [Customer].[Phone]
        , [Customer].[Email]
        , [Customer].[Address]
        , [Customer].[City]
        , [Customer].[TaxID]
        , [Customer].[enmCustomerType]
        , [Customer].[PaymentTermsDays]
        , [Customer].[Notes]
        , [Customer].[blg_IsActive]
        , [Customer].[Location]
        , [Customer].[AccountantEmail]
        , [Customer].[enmAccountantMethod]
        , [Customer].[InvoiceName]
        , [Customer].[ProfitabilityCode]
        , [Customer].[clc_CustomerIdentifier]
        --Auditing 
        , [Customer].[AddedOn]
        , [Customer].[RivhitCustomerNo]
        , [Customer].[enmDefaultDeliveryMethod] 
      FROM [Customer] 
      WHERE  
            ([Customer].[ID] = @ID) 
       
   
  RETURN
GO


/* ---- ccCustomerGetByRivhitCustomerNo ---- */
CREATE OR ALTER PROCEDURE [dbo].[ccCustomerGetByRivhitCustomerNo] 
  ( 
     @RivhitCustomerNo int  
  ) 
AS   
  SET NOCOUNT ON 
 
      SELECT  
          [Customer].[ID]
        , [Customer].[CustomerCode]
        , [Customer].[CustomerName]
        , [Customer].[Phone]
        , [Customer].[Email]
        , [Customer].[Address]
        , [Customer].[City]
        , [Customer].[TaxID]
        , [Customer].[enmCustomerType]
        , [Customer].[PaymentTermsDays]
        , [Customer].[Notes]
        , [Customer].[blg_IsActive]
        , [Customer].[Location]
        , [Customer].[AccountantEmail]
        , [Customer].[enmAccountantMethod]
        , [Customer].[InvoiceName]
        , [Customer].[ProfitabilityCode]
        , [Customer].[clc_CustomerIdentifier]
        --Auditing 
        , [Customer].[AddedOn]
        , [Customer].[RivhitCustomerNo]
        , [Customer].[enmDefaultDeliveryMethod] 
      FROM [Customer] 
      WHERE  
            ([Customer].[RivhitCustomerNo] = @RivhitCustomerNo OR ([Customer].[RivhitCustomerNo] IS NULL AND @RivhitCustomerNo IS NULL)) 
        AND ([RivhitCustomerNo] IS NOT NULL AND [DeletedOn] IS NULL)
       
   
  RETURN
GO


/* ---- ccCustomersFill ---- */
CREATE OR ALTER PROCEDURE [dbo].[ccCustomersFill] 
  ( 
    @Top int 
  , @Dir varchar(4) 
  ) 
AS   
  SET NOCOUNT ON 
 
      IF (@Top > 0) 
        IF (@Dir = 'ASC') 
            SELECT TOP (@Top) 
                [Customer].[ID]
              , [Customer].[CustomerCode]
              , [Customer].[CustomerName]
              , [Customer].[Phone]
              , [Customer].[Email]
              , [Customer].[Address]
              , [Customer].[City]
              , [Customer].[TaxID]
              , [Customer].[enmCustomerType]
              , [Customer].[PaymentTermsDays]
              , [Customer].[Notes]
              , [Customer].[blg_IsActive]
              , [Customer].[Location]
              , [Customer].[AccountantEmail]
              , [Customer].[enmAccountantMethod]
              , [Customer].[InvoiceName]
              , [Customer].[ProfitabilityCode]
              , [Customer].[clc_CustomerIdentifier]
              --Auditing 
              , [Customer].[AddedOn]
        , [Customer].[RivhitCustomerNo]
        , [Customer].[enmDefaultDeliveryMethod] 
            FROM [Customer] 
            ORDER BY [ID] ASC 
      IF (@Top > 0) 
        IF (@Dir = 'DESC') 
            SELECT TOP (@Top) 
                [Customer].[ID]
              , [Customer].[CustomerCode]
              , [Customer].[CustomerName]
              , [Customer].[Phone]
              , [Customer].[Email]
              , [Customer].[Address]
              , [Customer].[City]
              , [Customer].[TaxID]
              , [Customer].[enmCustomerType]
              , [Customer].[PaymentTermsDays]
              , [Customer].[Notes]
              , [Customer].[blg_IsActive]
              , [Customer].[Location]
              , [Customer].[AccountantEmail]
              , [Customer].[enmAccountantMethod]
              , [Customer].[InvoiceName]
              , [Customer].[ProfitabilityCode]
              , [Customer].[clc_CustomerIdentifier]
              --Auditing 
              , [Customer].[AddedOn] 
            FROM [Customer] 
            ORDER BY [ID] DESC 
      IF (@Top <= 0) 
        IF (@Dir = 'ASC') 
            SELECT  
                [Customer].[ID]
              , [Customer].[CustomerCode]
              , [Customer].[CustomerName]
              , [Customer].[Phone]
              , [Customer].[Email]
              , [Customer].[Address]
              , [Customer].[City]
              , [Customer].[TaxID]
              , [Customer].[enmCustomerType]
              , [Customer].[PaymentTermsDays]
              , [Customer].[Notes]
              , [Customer].[blg_IsActive]
              , [Customer].[Location]
              , [Customer].[AccountantEmail]
              , [Customer].[enmAccountantMethod]
              , [Customer].[InvoiceName]
              , [Customer].[ProfitabilityCode]
              , [Customer].[clc_CustomerIdentifier]
              --Auditing 
              , [Customer].[AddedOn] 
            FROM [Customer] 
            ORDER BY [ID] ASC 
      IF (@Top <= 0) 
        IF (@Dir = 'DESC') 
            SELECT  
                [Customer].[ID]
              , [Customer].[CustomerCode]
              , [Customer].[CustomerName]
              , [Customer].[Phone]
              , [Customer].[Email]
              , [Customer].[Address]
              , [Customer].[City]
              , [Customer].[TaxID]
              , [Customer].[enmCustomerType]
              , [Customer].[PaymentTermsDays]
              , [Customer].[Notes]
              , [Customer].[blg_IsActive]
              , [Customer].[Location]
              , [Customer].[AccountantEmail]
              , [Customer].[enmAccountantMethod]
              , [Customer].[InvoiceName]
              , [Customer].[ProfitabilityCode]
              , [Customer].[clc_CustomerIdentifier]
              --Auditing 
              , [Customer].[AddedOn] 
            FROM [Customer] 
            ORDER BY [ID] DESC 
  
  RETURN
GO


/* ---- ccCustomersFillByBoundedCustomerCode ---- */
CREATE OR ALTER PROCEDURE [dbo].[ccCustomersFillByBoundedCustomerCode] 
  ( 
    @bndCustomerCodeFrom nvarchar(50)  
  , @bndCustomerCodeTo nvarchar(50)  
  , @Top int 
  , @Dir varchar(4) 
  ) 
AS   
  SET NOCOUNT ON 
 
      IF (@Top > 0) 
        IF (@Dir = 'ASC') 
            SELECT TOP (@Top) 
                [Customer].[ID]
              , [Customer].[CustomerCode]
              , [Customer].[CustomerName]
              , [Customer].[Phone]
              , [Customer].[Email]
              , [Customer].[Address]
              , [Customer].[City]
              , [Customer].[TaxID]
              , [Customer].[enmCustomerType]
              , [Customer].[PaymentTermsDays]
              , [Customer].[Notes]
              , [Customer].[blg_IsActive]
              , [Customer].[Location]
              , [Customer].[AccountantEmail]
              , [Customer].[enmAccountantMethod]
              , [Customer].[InvoiceName]
              , [Customer].[ProfitabilityCode]
              , [Customer].[clc_CustomerIdentifier]
              --Auditing 
              , [Customer].[AddedOn]
        , [Customer].[RivhitCustomerNo]
        , [Customer].[enmDefaultDeliveryMethod] 
            FROM [Customer] 
            WHERE  
                ([Customer].[CustomerCode] >= @bndCustomerCodeFrom) 
            AND ([Customer].[CustomerCode] <= @bndCustomerCodeTo) 
          ORDER BY [CustomerCode] ASC
      IF (@Top > 0) 
        IF (@Dir = 'DESC') 
            SELECT TOP (@Top) 
                [Customer].[ID]
              , [Customer].[CustomerCode]
              , [Customer].[CustomerName]
              , [Customer].[Phone]
              , [Customer].[Email]
              , [Customer].[Address]
              , [Customer].[City]
              , [Customer].[TaxID]
              , [Customer].[enmCustomerType]
              , [Customer].[PaymentTermsDays]
              , [Customer].[Notes]
              , [Customer].[blg_IsActive]
              , [Customer].[Location]
              , [Customer].[AccountantEmail]
              , [Customer].[enmAccountantMethod]
              , [Customer].[InvoiceName]
              , [Customer].[ProfitabilityCode]
              , [Customer].[clc_CustomerIdentifier]
              --Auditing 
              , [Customer].[AddedOn] 
            FROM [Customer] 
            WHERE  
                ([Customer].[CustomerCode] >= @bndCustomerCodeFrom) 
            AND ([Customer].[CustomerCode] <= @bndCustomerCodeTo) 
          ORDER BY [CustomerCode] DESC
      IF (@Top <= 0) 
        IF (@Dir = 'ASC') 
            SELECT  
                [Customer].[ID]
              , [Customer].[CustomerCode]
              , [Customer].[CustomerName]
              , [Customer].[Phone]
              , [Customer].[Email]
              , [Customer].[Address]
              , [Customer].[City]
              , [Customer].[TaxID]
              , [Customer].[enmCustomerType]
              , [Customer].[PaymentTermsDays]
              , [Customer].[Notes]
              , [Customer].[blg_IsActive]
              , [Customer].[Location]
              , [Customer].[AccountantEmail]
              , [Customer].[enmAccountantMethod]
              , [Customer].[InvoiceName]
              , [Customer].[ProfitabilityCode]
              , [Customer].[clc_CustomerIdentifier]
              --Auditing 
              , [Customer].[AddedOn] 
            FROM [Customer] 
            WHERE  
                ([Customer].[CustomerCode] >= @bndCustomerCodeFrom) 
            AND ([Customer].[CustomerCode] <= @bndCustomerCodeTo) 
          ORDER BY [CustomerCode] ASC
      IF (@Top <= 0) 
        IF (@Dir = 'DESC') 
            SELECT  
                [Customer].[ID]
              , [Customer].[CustomerCode]
              , [Customer].[CustomerName]
              , [Customer].[Phone]
              , [Customer].[Email]
              , [Customer].[Address]
              , [Customer].[City]
              , [Customer].[TaxID]
              , [Customer].[enmCustomerType]
              , [Customer].[PaymentTermsDays]
              , [Customer].[Notes]
              , [Customer].[blg_IsActive]
              , [Customer].[Location]
              , [Customer].[AccountantEmail]
              , [Customer].[enmAccountantMethod]
              , [Customer].[InvoiceName]
              , [Customer].[ProfitabilityCode]
              , [Customer].[clc_CustomerIdentifier]
              --Auditing 
              , [Customer].[AddedOn] 
            FROM [Customer] 
            WHERE  
                ([Customer].[CustomerCode] >= @bndCustomerCodeFrom) 
            AND ([Customer].[CustomerCode] <= @bndCustomerCodeTo) 
          ORDER BY [CustomerCode] DESC
   
   
  RETURN
GO


/* ---- ccCustomersFillByBoundedID ---- */
CREATE OR ALTER PROCEDURE [dbo].[ccCustomersFillByBoundedID] 
  ( 
    @bndIDFrom bigint  
  , @bndIDTo bigint  
  , @Top int 
  , @Dir varchar(4) 
  ) 
AS   
  SET NOCOUNT ON 
 
      IF (@Top > 0) 
        IF (@Dir = 'ASC') 
            SELECT TOP (@Top) 
                [Customer].[ID]
              , [Customer].[CustomerCode]
              , [Customer].[CustomerName]
              , [Customer].[Phone]
              , [Customer].[Email]
              , [Customer].[Address]
              , [Customer].[City]
              , [Customer].[TaxID]
              , [Customer].[enmCustomerType]
              , [Customer].[PaymentTermsDays]
              , [Customer].[Notes]
              , [Customer].[blg_IsActive]
              , [Customer].[Location]
              , [Customer].[AccountantEmail]
              , [Customer].[enmAccountantMethod]
              , [Customer].[InvoiceName]
              , [Customer].[ProfitabilityCode]
              , [Customer].[clc_CustomerIdentifier]
              --Auditing 
              , [Customer].[AddedOn]
        , [Customer].[RivhitCustomerNo]
        , [Customer].[enmDefaultDeliveryMethod] 
            FROM [Customer] 
            WHERE  
                ([Customer].[ID] >= @bndIDFrom) 
            AND ([Customer].[ID] <= @bndIDTo) 
          ORDER BY [ID] ASC
      IF (@Top > 0) 
        IF (@Dir = 'DESC') 
            SELECT TOP (@Top) 
                [Customer].[ID]
              , [Customer].[CustomerCode]
              , [Customer].[CustomerName]
              , [Customer].[Phone]
              , [Customer].[Email]
              , [Customer].[Address]
              , [Customer].[City]
              , [Customer].[TaxID]
              , [Customer].[enmCustomerType]
              , [Customer].[PaymentTermsDays]
              , [Customer].[Notes]
              , [Customer].[blg_IsActive]
              , [Customer].[Location]
              , [Customer].[AccountantEmail]
              , [Customer].[enmAccountantMethod]
              , [Customer].[InvoiceName]
              , [Customer].[ProfitabilityCode]
              , [Customer].[clc_CustomerIdentifier]
              --Auditing 
              , [Customer].[AddedOn] 
            FROM [Customer] 
            WHERE  
                ([Customer].[ID] >= @bndIDFrom) 
            AND ([Customer].[ID] <= @bndIDTo) 
          ORDER BY [ID] DESC
      IF (@Top <= 0) 
        IF (@Dir = 'ASC') 
            SELECT  
                [Customer].[ID]
              , [Customer].[CustomerCode]
              , [Customer].[CustomerName]
              , [Customer].[Phone]
              , [Customer].[Email]
              , [Customer].[Address]
              , [Customer].[City]
              , [Customer].[TaxID]
              , [Customer].[enmCustomerType]
              , [Customer].[PaymentTermsDays]
              , [Customer].[Notes]
              , [Customer].[blg_IsActive]
              , [Customer].[Location]
              , [Customer].[AccountantEmail]
              , [Customer].[enmAccountantMethod]
              , [Customer].[InvoiceName]
              , [Customer].[ProfitabilityCode]
              , [Customer].[clc_CustomerIdentifier]
              --Auditing 
              , [Customer].[AddedOn] 
            FROM [Customer] 
            WHERE  
                ([Customer].[ID] >= @bndIDFrom) 
            AND ([Customer].[ID] <= @bndIDTo) 
          ORDER BY [ID] ASC
      IF (@Top <= 0) 
        IF (@Dir = 'DESC') 
            SELECT  
                [Customer].[ID]
              , [Customer].[CustomerCode]
              , [Customer].[CustomerName]
              , [Customer].[Phone]
              , [Customer].[Email]
              , [Customer].[Address]
              , [Customer].[City]
              , [Customer].[TaxID]
              , [Customer].[enmCustomerType]
              , [Customer].[PaymentTermsDays]
              , [Customer].[Notes]
              , [Customer].[blg_IsActive]
              , [Customer].[Location]
              , [Customer].[AccountantEmail]
              , [Customer].[enmAccountantMethod]
              , [Customer].[InvoiceName]
              , [Customer].[ProfitabilityCode]
              , [Customer].[clc_CustomerIdentifier]
              --Auditing 
              , [Customer].[AddedOn] 
            FROM [Customer] 
            WHERE  
                ([Customer].[ID] >= @bndIDFrom) 
            AND ([Customer].[ID] <= @bndIDTo) 
          ORDER BY [ID] DESC
   
   
  RETURN
GO


/* ---- ccCustomersFillByBoundedRivhitCustomerNo ---- */
CREATE OR ALTER PROCEDURE [dbo].[ccCustomersFillByBoundedRivhitCustomerNo] 
  ( 
    @bndRivhitCustomerNoFrom int  
  , @bndRivhitCustomerNoTo int  
  , @Top int 
  , @Dir varchar(4) 
  ) 
AS   
  SET NOCOUNT ON 
 
      IF (@Top > 0) 
        IF (@Dir = 'ASC') 
            SELECT TOP (@Top) 
                [Customer].[ID]
              , [Customer].[CustomerCode]
              , [Customer].[CustomerName]
              , [Customer].[Phone]
              , [Customer].[Email]
              , [Customer].[Address]
              , [Customer].[City]
              , [Customer].[TaxID]
              , [Customer].[enmCustomerType]
              , [Customer].[PaymentTermsDays]
              , [Customer].[Notes]
              , [Customer].[blg_IsActive]
              , [Customer].[Location]
              , [Customer].[AccountantEmail]
              , [Customer].[enmAccountantMethod]
              , [Customer].[InvoiceName]
              , [Customer].[ProfitabilityCode]
              , [Customer].[clc_CustomerIdentifier]
              --Auditing 
              , [Customer].[AddedOn]
        , [Customer].[RivhitCustomerNo]
        , [Customer].[enmDefaultDeliveryMethod] 
            FROM [Customer] 
            WHERE  
                ([Customer].[RivhitCustomerNo] >= @bndRivhitCustomerNoFrom) 
            AND ([Customer].[RivhitCustomerNo] <= @bndRivhitCustomerNoTo) 
          ORDER BY [RivhitCustomerNo] ASC
      IF (@Top > 0) 
        IF (@Dir = 'DESC') 
            SELECT TOP (@Top) 
                [Customer].[ID]
              , [Customer].[CustomerCode]
              , [Customer].[CustomerName]
              , [Customer].[Phone]
              , [Customer].[Email]
              , [Customer].[Address]
              , [Customer].[City]
              , [Customer].[TaxID]
              , [Customer].[enmCustomerType]
              , [Customer].[PaymentTermsDays]
              , [Customer].[Notes]
              , [Customer].[blg_IsActive]
              , [Customer].[Location]
              , [Customer].[AccountantEmail]
              , [Customer].[enmAccountantMethod]
              , [Customer].[InvoiceName]
              , [Customer].[ProfitabilityCode]
              , [Customer].[clc_CustomerIdentifier]
              --Auditing 
              , [Customer].[AddedOn] 
            FROM [Customer] 
            WHERE  
                ([Customer].[RivhitCustomerNo] >= @bndRivhitCustomerNoFrom) 
            AND ([Customer].[RivhitCustomerNo] <= @bndRivhitCustomerNoTo) 
          ORDER BY [RivhitCustomerNo] DESC
      IF (@Top <= 0) 
        IF (@Dir = 'ASC') 
            SELECT  
                [Customer].[ID]
              , [Customer].[CustomerCode]
              , [Customer].[CustomerName]
              , [Customer].[Phone]
              , [Customer].[Email]
              , [Customer].[Address]
              , [Customer].[City]
              , [Customer].[TaxID]
              , [Customer].[enmCustomerType]
              , [Customer].[PaymentTermsDays]
              , [Customer].[Notes]
              , [Customer].[blg_IsActive]
              , [Customer].[Location]
              , [Customer].[AccountantEmail]
              , [Customer].[enmAccountantMethod]
              , [Customer].[InvoiceName]
              , [Customer].[ProfitabilityCode]
              , [Customer].[clc_CustomerIdentifier]
              --Auditing 
              , [Customer].[AddedOn] 
            FROM [Customer] 
            WHERE  
                ([Customer].[RivhitCustomerNo] >= @bndRivhitCustomerNoFrom) 
            AND ([Customer].[RivhitCustomerNo] <= @bndRivhitCustomerNoTo) 
          ORDER BY [RivhitCustomerNo] ASC
      IF (@Top <= 0) 
        IF (@Dir = 'DESC') 
            SELECT  
                [Customer].[ID]
              , [Customer].[CustomerCode]
              , [Customer].[CustomerName]
              , [Customer].[Phone]
              , [Customer].[Email]
              , [Customer].[Address]
              , [Customer].[City]
              , [Customer].[TaxID]
              , [Customer].[enmCustomerType]
              , [Customer].[PaymentTermsDays]
              , [Customer].[Notes]
              , [Customer].[blg_IsActive]
              , [Customer].[Location]
              , [Customer].[AccountantEmail]
              , [Customer].[enmAccountantMethod]
              , [Customer].[InvoiceName]
              , [Customer].[ProfitabilityCode]
              , [Customer].[clc_CustomerIdentifier]
              --Auditing 
              , [Customer].[AddedOn] 
            FROM [Customer] 
            WHERE  
                ([Customer].[RivhitCustomerNo] >= @bndRivhitCustomerNoFrom) 
            AND ([Customer].[RivhitCustomerNo] <= @bndRivhitCustomerNoTo) 
          ORDER BY [RivhitCustomerNo] DESC
   
   
  RETURN
GO


/* ---- ccCustomersFillByCustomerType ---- */
CREATE OR ALTER PROCEDURE [dbo].[ccCustomersFillByCustomerType] 
  ( 
    @enmCustomerType nvarchar(50)  
  , @Top int 
  , @Dir varchar(4) 
  ) 
AS   
  SET NOCOUNT ON 
 
      IF (@Top > 0) 
        IF (@Dir = 'ASC') 
            SELECT TOP (@Top) 
                [Customer].[ID]
              , [Customer].[CustomerCode]
              , [Customer].[CustomerName]
              , [Customer].[Phone]
              , [Customer].[Email]
              , [Customer].[Address]
              , [Customer].[City]
              , [Customer].[TaxID]
              , [Customer].[enmCustomerType]
              , [Customer].[PaymentTermsDays]
              , [Customer].[Notes]
              , [Customer].[blg_IsActive]
              , [Customer].[Location]
              , [Customer].[AccountantEmail]
              , [Customer].[enmAccountantMethod]
              , [Customer].[InvoiceName]
              , [Customer].[ProfitabilityCode]
              , [Customer].[clc_CustomerIdentifier]
              --Auditing 
              , [Customer].[AddedOn]
        , [Customer].[RivhitCustomerNo]
        , [Customer].[enmDefaultDeliveryMethod] 
            FROM [Customer] 
            WHERE  
                ([Customer].[enmCustomerType] = @enmCustomerType OR ([Customer].[enmCustomerType] IS NULL AND @enmCustomerType IS NULL)) 
            ORDER BY [ID] ASC 
      IF (@Top > 0) 
        IF (@Dir = 'DESC') 
            SELECT TOP (@Top) 
                [Customer].[ID]
              , [Customer].[CustomerCode]
              , [Customer].[CustomerName]
              , [Customer].[Phone]
              , [Customer].[Email]
              , [Customer].[Address]
              , [Customer].[City]
              , [Customer].[TaxID]
              , [Customer].[enmCustomerType]
              , [Customer].[PaymentTermsDays]
              , [Customer].[Notes]
              , [Customer].[blg_IsActive]
              , [Customer].[Location]
              , [Customer].[AccountantEmail]
              , [Customer].[enmAccountantMethod]
              , [Customer].[InvoiceName]
              , [Customer].[ProfitabilityCode]
              , [Customer].[clc_CustomerIdentifier]
              --Auditing 
              , [Customer].[AddedOn] 
            FROM [Customer] 
            WHERE  
                ([Customer].[enmCustomerType] = @enmCustomerType OR ([Customer].[enmCustomerType] IS NULL AND @enmCustomerType IS NULL)) 
            ORDER BY [ID] DESC 
      IF (@Top <= 0) 
        IF (@Dir = 'ASC') 
            SELECT  
                [Customer].[ID]
              , [Customer].[CustomerCode]
              , [Customer].[CustomerName]
              , [Customer].[Phone]
              , [Customer].[Email]
              , [Customer].[Address]
              , [Customer].[City]
              , [Customer].[TaxID]
              , [Customer].[enmCustomerType]
              , [Customer].[PaymentTermsDays]
              , [Customer].[Notes]
              , [Customer].[blg_IsActive]
              , [Customer].[Location]
              , [Customer].[AccountantEmail]
              , [Customer].[enmAccountantMethod]
              , [Customer].[InvoiceName]
              , [Customer].[ProfitabilityCode]
              , [Customer].[clc_CustomerIdentifier]
              --Auditing 
              , [Customer].[AddedOn] 
            FROM [Customer] 
            WHERE  
                ([Customer].[enmCustomerType] = @enmCustomerType OR ([Customer].[enmCustomerType] IS NULL AND @enmCustomerType IS NULL)) 
            ORDER BY [ID] ASC 
      IF (@Top <= 0) 
        IF (@Dir = 'DESC') 
            SELECT  
                [Customer].[ID]
              , [Customer].[CustomerCode]
              , [Customer].[CustomerName]
              , [Customer].[Phone]
              , [Customer].[Email]
              , [Customer].[Address]
              , [Customer].[City]
              , [Customer].[TaxID]
              , [Customer].[enmCustomerType]
              , [Customer].[PaymentTermsDays]
              , [Customer].[Notes]
              , [Customer].[blg_IsActive]
              , [Customer].[Location]
              , [Customer].[AccountantEmail]
              , [Customer].[enmAccountantMethod]
              , [Customer].[InvoiceName]
              , [Customer].[ProfitabilityCode]
              , [Customer].[clc_CustomerIdentifier]
              --Auditing 
              , [Customer].[AddedOn] 
            FROM [Customer] 
            WHERE  
                ([Customer].[enmCustomerType] = @enmCustomerType OR ([Customer].[enmCustomerType] IS NULL AND @enmCustomerType IS NULL)) 
            ORDER BY [ID] DESC 
   
  RETURN
GO


/* ---- ccCustomersFillByWildCardCustomerCode ---- */
CREATE OR ALTER PROCEDURE [dbo].[ccCustomersFillByWildCardCustomerCode] 
  ( 
    @wldCustomerCode nvarchar(50)  
  , @Top int 
  , @Dir varchar(4) 
  ) 
AS   
  SET NOCOUNT ON 
 
      IF (@Top > 0) 
        IF (@Dir = 'ASC') 
            SELECT TOP (@Top) 
                [Customer].[ID]
              , [Customer].[CustomerCode]
              , [Customer].[CustomerName]
              , [Customer].[Phone]
              , [Customer].[Email]
              , [Customer].[Address]
              , [Customer].[City]
              , [Customer].[TaxID]
              , [Customer].[enmCustomerType]
              , [Customer].[PaymentTermsDays]
              , [Customer].[Notes]
              , [Customer].[blg_IsActive]
              , [Customer].[Location]
              , [Customer].[AccountantEmail]
              , [Customer].[enmAccountantMethod]
              , [Customer].[InvoiceName]
              , [Customer].[ProfitabilityCode]
              , [Customer].[clc_CustomerIdentifier]
              --Auditing 
              , [Customer].[AddedOn]
        , [Customer].[RivhitCustomerNo]
        , [Customer].[enmDefaultDeliveryMethod] 
            FROM [Customer] 
            WHERE  
                ([Customer].[CustomerCode] LIKE @wldCustomerCode) 
            ORDER BY [CustomerCode] ASC   
      IF (@Top > 0) 
        IF (@Dir = 'DESC') 
            SELECT TOP (@Top) 
                [Customer].[ID]
              , [Customer].[CustomerCode]
              , [Customer].[CustomerName]
              , [Customer].[Phone]
              , [Customer].[Email]
              , [Customer].[Address]
              , [Customer].[City]
              , [Customer].[TaxID]
              , [Customer].[enmCustomerType]
              , [Customer].[PaymentTermsDays]
              , [Customer].[Notes]
              , [Customer].[blg_IsActive]
              , [Customer].[Location]
              , [Customer].[AccountantEmail]
              , [Customer].[enmAccountantMethod]
              , [Customer].[InvoiceName]
              , [Customer].[ProfitabilityCode]
              , [Customer].[clc_CustomerIdentifier]
              --Auditing 
              , [Customer].[AddedOn] 
            FROM [Customer] 
            WHERE  
                ([Customer].[CustomerCode] LIKE @wldCustomerCode) 
            ORDER BY [CustomerCode] DESC   
      IF (@Top <= 0) 
        IF (@Dir = 'ASC') 
            SELECT  
                [Customer].[ID]
              , [Customer].[CustomerCode]
              , [Customer].[CustomerName]
              , [Customer].[Phone]
              , [Customer].[Email]
              , [Customer].[Address]
              , [Customer].[City]
              , [Customer].[TaxID]
              , [Customer].[enmCustomerType]
              , [Customer].[PaymentTermsDays]
              , [Customer].[Notes]
              , [Customer].[blg_IsActive]
              , [Customer].[Location]
              , [Customer].[AccountantEmail]
              , [Customer].[enmAccountantMethod]
              , [Customer].[InvoiceName]
              , [Customer].[ProfitabilityCode]
              , [Customer].[clc_CustomerIdentifier]
              --Auditing 
              , [Customer].[AddedOn] 
            FROM [Customer] 
            WHERE  
                ([Customer].[CustomerCode] LIKE @wldCustomerCode) 
            ORDER BY [CustomerCode] ASC   
      IF (@Top <= 0) 
        IF (@Dir = 'DESC') 
            SELECT  
                [Customer].[ID]
              , [Customer].[CustomerCode]
              , [Customer].[CustomerName]
              , [Customer].[Phone]
              , [Customer].[Email]
              , [Customer].[Address]
              , [Customer].[City]
              , [Customer].[TaxID]
              , [Customer].[enmCustomerType]
              , [Customer].[PaymentTermsDays]
              , [Customer].[Notes]
              , [Customer].[blg_IsActive]
              , [Customer].[Location]
              , [Customer].[AccountantEmail]
              , [Customer].[enmAccountantMethod]
              , [Customer].[InvoiceName]
              , [Customer].[ProfitabilityCode]
              , [Customer].[clc_CustomerIdentifier]
              --Auditing 
              , [Customer].[AddedOn] 
            FROM [Customer] 
            WHERE  
                ([Customer].[CustomerCode] LIKE @wldCustomerCode) 
            ORDER BY [CustomerCode] DESC   
   
   
  RETURN
GO


/* ---- ccCustomerDebtGetByID ---- */
CREATE OR ALTER PROCEDURE [dbo].[ccCustomerDebtGetByID] 
  ( 
     @ID bigint  
  ,  @WithParentText bit = 0
  ) 
AS   
  SET NOCOUNT ON 
 
    IF (@WithParentText = 0) 
      SELECT  
          [CustomerDebt].[ID]
        , [CustomerDebt].[CustomerID]
        , [CustomerDebt].[OrderHeaderID]
        , [CustomerDebt].[DebtAmount]
        , [CustomerDebt].[PaidAmount]
              , [CustomerDebt].[clc_RemainingAmount]
        , [CustomerDebt].[DebtDate]
        , [CustomerDebt].[DueDate]
        , [CustomerDebt].[enmDebtStatus]
        , [CustomerDebt].[Notes]
        , [CustomerDebt].[blg_NeedsAttention]
        , [CustomerDebt].[ProductTypes]
        , [CustomerDebt].[DeliveryDate]
        --Auditing 
        , [CustomerDebt].[AddedOn] 
      FROM [CustomerDebt] 
      WHERE  
            ([CustomerDebt].[ID] = @ID) 
    ELSE 
      SELECT  
          [CustomerDebt].[ID]
        , [CustomerDebt].[CustomerID]
        , [CustomerDebt].[OrderHeaderID]
        , [CustomerDebt].[DebtAmount]
        , [CustomerDebt].[PaidAmount]
              , [CustomerDebt].[clc_RemainingAmount]
        , [CustomerDebt].[DebtDate]
        , [CustomerDebt].[DueDate]
        , [CustomerDebt].[enmDebtStatus]
        , [CustomerDebt].[Notes]
        , [CustomerDebt].[blg_NeedsAttention]
        , [CustomerDebt].[ProductTypes]
        , [CustomerDebt].[DeliveryDate]
        --Auditing 
        , [CustomerDebt].[AddedOn] 
        , CustomerForCustomer.[TEXT] AS CustomerText
        , OrderHeaderForOrderHeader.[TEXT] AS OrderHeaderText
      FROM [CustomerDebt] 
        LEFT OUTER JOIN ccvwComboList_Customer CustomerForCustomer ON [CustomerDebt].CustomerID = CustomerForCustomer.ID 
        LEFT OUTER JOIN ccvwComboList_OrderHeader OrderHeaderForOrderHeader ON [CustomerDebt].OrderHeaderID = OrderHeaderForOrderHeader.ID 
      WHERE  
            ([CustomerDebt].[ID] = @ID) 
       
   
  RETURN
GO


/* ---- ccCustomerDebtsFill ---- */
CREATE OR ALTER PROCEDURE [dbo].[ccCustomerDebtsFill] 
  ( 
    @Top int 
  , @Dir varchar(4) 
  , @WithParentText bit = 0
  ) 
AS   
  SET NOCOUNT ON 
 
      IF (@Top > 0) 
        IF (@Dir = 'ASC') 
          IF (@WithParentText = 0) 
            SELECT TOP (@Top) 
                [CustomerDebt].[ID]
              , [CustomerDebt].[CustomerID]
              , [CustomerDebt].[OrderHeaderID]
              , [CustomerDebt].[DebtAmount]
              , [CustomerDebt].[PaidAmount]
              , [CustomerDebt].[clc_RemainingAmount]
              , [CustomerDebt].[DebtDate]
              , [CustomerDebt].[DueDate]
              , [CustomerDebt].[enmDebtStatus]
              , [CustomerDebt].[Notes]
              , [CustomerDebt].[blg_NeedsAttention]
              , [CustomerDebt].[ProductTypes]
              , [CustomerDebt].[DeliveryDate]
              --Auditing 
              , [CustomerDebt].[AddedOn] 
            FROM [CustomerDebt] 
            ORDER BY [ID] ASC 
      IF (@Top > 0) 
        IF (@Dir = 'DESC') 
          IF (@WithParentText = 0) 
            SELECT TOP (@Top) 
                [CustomerDebt].[ID]
              , [CustomerDebt].[CustomerID]
              , [CustomerDebt].[OrderHeaderID]
              , [CustomerDebt].[DebtAmount]
              , [CustomerDebt].[PaidAmount]
              , [CustomerDebt].[clc_RemainingAmount]
              , [CustomerDebt].[DebtDate]
              , [CustomerDebt].[DueDate]
              , [CustomerDebt].[enmDebtStatus]
              , [CustomerDebt].[Notes]
              , [CustomerDebt].[blg_NeedsAttention]
              , [CustomerDebt].[ProductTypes]
              , [CustomerDebt].[DeliveryDate]
              --Auditing 
              , [CustomerDebt].[AddedOn] 
            FROM [CustomerDebt] 
            ORDER BY [ID] DESC 
      IF (@Top <= 0) 
        IF (@Dir = 'ASC') 
          IF (@WithParentText = 0) 
            SELECT  
                [CustomerDebt].[ID]
              , [CustomerDebt].[CustomerID]
              , [CustomerDebt].[OrderHeaderID]
              , [CustomerDebt].[DebtAmount]
              , [CustomerDebt].[PaidAmount]
              , [CustomerDebt].[clc_RemainingAmount]
              , [CustomerDebt].[DebtDate]
              , [CustomerDebt].[DueDate]
              , [CustomerDebt].[enmDebtStatus]
              , [CustomerDebt].[Notes]
              , [CustomerDebt].[blg_NeedsAttention]
              , [CustomerDebt].[ProductTypes]
              , [CustomerDebt].[DeliveryDate]
              --Auditing 
              , [CustomerDebt].[AddedOn] 
            FROM [CustomerDebt] 
            ORDER BY [ID] ASC 
      IF (@Top <= 0) 
        IF (@Dir = 'DESC') 
          IF (@WithParentText = 0) 
            SELECT  
                [CustomerDebt].[ID]
              , [CustomerDebt].[CustomerID]
              , [CustomerDebt].[OrderHeaderID]
              , [CustomerDebt].[DebtAmount]
              , [CustomerDebt].[PaidAmount]
              , [CustomerDebt].[clc_RemainingAmount]
              , [CustomerDebt].[DebtDate]
              , [CustomerDebt].[DueDate]
              , [CustomerDebt].[enmDebtStatus]
              , [CustomerDebt].[Notes]
              , [CustomerDebt].[blg_NeedsAttention]
              , [CustomerDebt].[ProductTypes]
              , [CustomerDebt].[DeliveryDate]
              --Auditing 
              , [CustomerDebt].[AddedOn] 
            FROM [CustomerDebt] 
            ORDER BY [ID] DESC 
      IF (@Top > 0) 
        IF (@Dir = 'ASC') 
          IF (@WithParentText = 1) 
            SELECT TOP (@Top) 
                [CustomerDebt].[ID]
              , [CustomerDebt].[CustomerID]
              , [CustomerDebt].[OrderHeaderID]
              , [CustomerDebt].[DebtAmount]
              , [CustomerDebt].[PaidAmount]
              , [CustomerDebt].[clc_RemainingAmount]
              , [CustomerDebt].[DebtDate]
              , [CustomerDebt].[DueDate]
              , [CustomerDebt].[enmDebtStatus]
              , [CustomerDebt].[Notes]
              , [CustomerDebt].[blg_NeedsAttention]
              , [CustomerDebt].[ProductTypes]
              , [CustomerDebt].[DeliveryDate]
              --Auditing 
              , [CustomerDebt].[AddedOn] 
              , CustomerForCustomer.[TEXT] AS CustomerText
              , OrderHeaderForOrderHeader.[TEXT] AS OrderHeaderText
            FROM [CustomerDebt] 
              LEFT OUTER JOIN ccvwComboList_Customer CustomerForCustomer ON [CustomerDebt].CustomerID = CustomerForCustomer.ID 
              LEFT OUTER JOIN ccvwComboList_OrderHeader OrderHeaderForOrderHeader ON [CustomerDebt].OrderHeaderID = OrderHeaderForOrderHeader.ID 
            ORDER BY [ID] ASC 
      IF (@Top > 0) 
        IF (@Dir = 'DESC') 
          IF (@WithParentText = 1) 
            SELECT TOP (@Top) 
                [CustomerDebt].[ID]
              , [CustomerDebt].[CustomerID]
              , [CustomerDebt].[OrderHeaderID]
              , [CustomerDebt].[DebtAmount]
              , [CustomerDebt].[PaidAmount]
              , [CustomerDebt].[clc_RemainingAmount]
              , [CustomerDebt].[DebtDate]
              , [CustomerDebt].[DueDate]
              , [CustomerDebt].[enmDebtStatus]
              , [CustomerDebt].[Notes]
              , [CustomerDebt].[blg_NeedsAttention]
              , [CustomerDebt].[ProductTypes]
              , [CustomerDebt].[DeliveryDate]
              --Auditing 
              , [CustomerDebt].[AddedOn] 
              , CustomerForCustomer.[TEXT] AS CustomerText
              , OrderHeaderForOrderHeader.[TEXT] AS OrderHeaderText
            FROM [CustomerDebt] 
              LEFT OUTER JOIN ccvwComboList_Customer CustomerForCustomer ON [CustomerDebt].CustomerID = CustomerForCustomer.ID 
              LEFT OUTER JOIN ccvwComboList_OrderHeader OrderHeaderForOrderHeader ON [CustomerDebt].OrderHeaderID = OrderHeaderForOrderHeader.ID 
            ORDER BY [ID] DESC 
      IF (@Top <= 0) 
        IF (@Dir = 'ASC') 
          IF (@WithParentText = 1) 
            SELECT  
                [CustomerDebt].[ID]
              , [CustomerDebt].[CustomerID]
              , [CustomerDebt].[OrderHeaderID]
              , [CustomerDebt].[DebtAmount]
              , [CustomerDebt].[PaidAmount]
              , [CustomerDebt].[clc_RemainingAmount]
              , [CustomerDebt].[DebtDate]
              , [CustomerDebt].[DueDate]
              , [CustomerDebt].[enmDebtStatus]
              , [CustomerDebt].[Notes]
              , [CustomerDebt].[blg_NeedsAttention]
              , [CustomerDebt].[ProductTypes]
              , [CustomerDebt].[DeliveryDate]
              --Auditing 
              , [CustomerDebt].[AddedOn] 
              , CustomerForCustomer.[TEXT] AS CustomerText
              , OrderHeaderForOrderHeader.[TEXT] AS OrderHeaderText
            FROM [CustomerDebt] 
              LEFT OUTER JOIN ccvwComboList_Customer CustomerForCustomer ON [CustomerDebt].CustomerID = CustomerForCustomer.ID 
              LEFT OUTER JOIN ccvwComboList_OrderHeader OrderHeaderForOrderHeader ON [CustomerDebt].OrderHeaderID = OrderHeaderForOrderHeader.ID 
            ORDER BY [ID] ASC 
      IF (@Top <= 0) 
        IF (@Dir = 'DESC') 
          IF (@WithParentText = 1) 
            SELECT  
                [CustomerDebt].[ID]
              , [CustomerDebt].[CustomerID]
              , [CustomerDebt].[OrderHeaderID]
              , [CustomerDebt].[DebtAmount]
              , [CustomerDebt].[PaidAmount]
              , [CustomerDebt].[clc_RemainingAmount]
              , [CustomerDebt].[DebtDate]
              , [CustomerDebt].[DueDate]
              , [CustomerDebt].[enmDebtStatus]
              , [CustomerDebt].[Notes]
              , [CustomerDebt].[blg_NeedsAttention]
              , [CustomerDebt].[ProductTypes]
              , [CustomerDebt].[DeliveryDate]
              --Auditing 
              , [CustomerDebt].[AddedOn] 
              , CustomerForCustomer.[TEXT] AS CustomerText
              , OrderHeaderForOrderHeader.[TEXT] AS OrderHeaderText
            FROM [CustomerDebt] 
              LEFT OUTER JOIN ccvwComboList_Customer CustomerForCustomer ON [CustomerDebt].CustomerID = CustomerForCustomer.ID 
              LEFT OUTER JOIN ccvwComboList_OrderHeader OrderHeaderForOrderHeader ON [CustomerDebt].OrderHeaderID = OrderHeaderForOrderHeader.ID 
            ORDER BY [ID] DESC 
  
  RETURN
GO


/* ---- ccCustomerDebtsFillByBoundedID ---- */
CREATE OR ALTER PROCEDURE [dbo].[ccCustomerDebtsFillByBoundedID] 
  ( 
    @bndIDFrom bigint  
  , @bndIDTo bigint  
  , @Top int 
  , @Dir varchar(4) 
  , @WithParentText bit = 0
  ) 
AS   
  SET NOCOUNT ON 
 
      IF (@Top > 0) 
        IF (@Dir = 'ASC') 
          IF (@WithParentText = 0) 
            SELECT TOP (@Top) 
                [CustomerDebt].[ID]
              , [CustomerDebt].[CustomerID]
              , [CustomerDebt].[OrderHeaderID]
              , [CustomerDebt].[DebtAmount]
              , [CustomerDebt].[PaidAmount]
              , [CustomerDebt].[clc_RemainingAmount]
              , [CustomerDebt].[DebtDate]
              , [CustomerDebt].[DueDate]
              , [CustomerDebt].[enmDebtStatus]
              , [CustomerDebt].[Notes]
              , [CustomerDebt].[blg_NeedsAttention]
              , [CustomerDebt].[ProductTypes]
              , [CustomerDebt].[DeliveryDate]
              --Auditing 
              , [CustomerDebt].[AddedOn] 
            FROM [CustomerDebt] 
            WHERE  
                ([CustomerDebt].[ID] >= @bndIDFrom) 
            AND ([CustomerDebt].[ID] <= @bndIDTo) 
          ORDER BY [ID] ASC
      IF (@Top > 0) 
        IF (@Dir = 'DESC') 
          IF (@WithParentText = 0) 
            SELECT TOP (@Top) 
                [CustomerDebt].[ID]
              , [CustomerDebt].[CustomerID]
              , [CustomerDebt].[OrderHeaderID]
              , [CustomerDebt].[DebtAmount]
              , [CustomerDebt].[PaidAmount]
              , [CustomerDebt].[clc_RemainingAmount]
              , [CustomerDebt].[DebtDate]
              , [CustomerDebt].[DueDate]
              , [CustomerDebt].[enmDebtStatus]
              , [CustomerDebt].[Notes]
              , [CustomerDebt].[blg_NeedsAttention]
              , [CustomerDebt].[ProductTypes]
              , [CustomerDebt].[DeliveryDate]
              --Auditing 
              , [CustomerDebt].[AddedOn] 
            FROM [CustomerDebt] 
            WHERE  
                ([CustomerDebt].[ID] >= @bndIDFrom) 
            AND ([CustomerDebt].[ID] <= @bndIDTo) 
          ORDER BY [ID] DESC
      IF (@Top <= 0) 
        IF (@Dir = 'ASC') 
          IF (@WithParentText = 0) 
            SELECT  
                [CustomerDebt].[ID]
              , [CustomerDebt].[CustomerID]
              , [CustomerDebt].[OrderHeaderID]
              , [CustomerDebt].[DebtAmount]
              , [CustomerDebt].[PaidAmount]
              , [CustomerDebt].[clc_RemainingAmount]
              , [CustomerDebt].[DebtDate]
              , [CustomerDebt].[DueDate]
              , [CustomerDebt].[enmDebtStatus]
              , [CustomerDebt].[Notes]
              , [CustomerDebt].[blg_NeedsAttention]
              , [CustomerDebt].[ProductTypes]
              , [CustomerDebt].[DeliveryDate]
              --Auditing 
              , [CustomerDebt].[AddedOn] 
            FROM [CustomerDebt] 
            WHERE  
                ([CustomerDebt].[ID] >= @bndIDFrom) 
            AND ([CustomerDebt].[ID] <= @bndIDTo) 
          ORDER BY [ID] ASC
      IF (@Top <= 0) 
        IF (@Dir = 'DESC') 
          IF (@WithParentText = 0) 
            SELECT  
                [CustomerDebt].[ID]
              , [CustomerDebt].[CustomerID]
              , [CustomerDebt].[OrderHeaderID]
              , [CustomerDebt].[DebtAmount]
              , [CustomerDebt].[PaidAmount]
              , [CustomerDebt].[clc_RemainingAmount]
              , [CustomerDebt].[DebtDate]
              , [CustomerDebt].[DueDate]
              , [CustomerDebt].[enmDebtStatus]
              , [CustomerDebt].[Notes]
              , [CustomerDebt].[blg_NeedsAttention]
              , [CustomerDebt].[ProductTypes]
              , [CustomerDebt].[DeliveryDate]
              --Auditing 
              , [CustomerDebt].[AddedOn] 
            FROM [CustomerDebt] 
            WHERE  
                ([CustomerDebt].[ID] >= @bndIDFrom) 
            AND ([CustomerDebt].[ID] <= @bndIDTo) 
          ORDER BY [ID] DESC
      IF (@Top > 0) 
        IF (@Dir = 'ASC') 
          IF (@WithParentText = 1) 
            SELECT TOP (@Top) 
                [CustomerDebt].[ID]
              , [CustomerDebt].[CustomerID]
              , [CustomerDebt].[OrderHeaderID]
              , [CustomerDebt].[DebtAmount]
              , [CustomerDebt].[PaidAmount]
              , [CustomerDebt].[clc_RemainingAmount]
              , [CustomerDebt].[DebtDate]
              , [CustomerDebt].[DueDate]
              , [CustomerDebt].[enmDebtStatus]
              , [CustomerDebt].[Notes]
              , [CustomerDebt].[blg_NeedsAttention]
              , [CustomerDebt].[ProductTypes]
              , [CustomerDebt].[DeliveryDate]
              --Auditing 
              , [CustomerDebt].[AddedOn] 
              , CustomerForCustomer.[TEXT] AS CustomerText
              , OrderHeaderForOrderHeader.[TEXT] AS OrderHeaderText
            FROM [CustomerDebt] 
              LEFT OUTER JOIN ccvwComboList_Customer CustomerForCustomer ON [CustomerDebt].CustomerID = CustomerForCustomer.ID 
              LEFT OUTER JOIN ccvwComboList_OrderHeader OrderHeaderForOrderHeader ON [CustomerDebt].OrderHeaderID = OrderHeaderForOrderHeader.ID 
            WHERE  
                ([CustomerDebt].[ID] >= @bndIDFrom) 
            AND ([CustomerDebt].[ID] <= @bndIDTo) 
          ORDER BY [ID] ASC
      IF (@Top > 0) 
        IF (@Dir = 'DESC') 
          IF (@WithParentText = 1) 
            SELECT TOP (@Top) 
                [CustomerDebt].[ID]
              , [CustomerDebt].[CustomerID]
              , [CustomerDebt].[OrderHeaderID]
              , [CustomerDebt].[DebtAmount]
              , [CustomerDebt].[PaidAmount]
              , [CustomerDebt].[clc_RemainingAmount]
              , [CustomerDebt].[DebtDate]
              , [CustomerDebt].[DueDate]
              , [CustomerDebt].[enmDebtStatus]
              , [CustomerDebt].[Notes]
              , [CustomerDebt].[blg_NeedsAttention]
              , [CustomerDebt].[ProductTypes]
              , [CustomerDebt].[DeliveryDate]
              --Auditing 
              , [CustomerDebt].[AddedOn] 
              , CustomerForCustomer.[TEXT] AS CustomerText
              , OrderHeaderForOrderHeader.[TEXT] AS OrderHeaderText
            FROM [CustomerDebt] 
              LEFT OUTER JOIN ccvwComboList_Customer CustomerForCustomer ON [CustomerDebt].CustomerID = CustomerForCustomer.ID 
              LEFT OUTER JOIN ccvwComboList_OrderHeader OrderHeaderForOrderHeader ON [CustomerDebt].OrderHeaderID = OrderHeaderForOrderHeader.ID 
            WHERE  
                ([CustomerDebt].[ID] >= @bndIDFrom) 
            AND ([CustomerDebt].[ID] <= @bndIDTo) 
          ORDER BY [ID] DESC
      IF (@Top <= 0) 
        IF (@Dir = 'ASC') 
          IF (@WithParentText = 1) 
            SELECT  
                [CustomerDebt].[ID]
              , [CustomerDebt].[CustomerID]
              , [CustomerDebt].[OrderHeaderID]
              , [CustomerDebt].[DebtAmount]
              , [CustomerDebt].[PaidAmount]
              , [CustomerDebt].[clc_RemainingAmount]
              , [CustomerDebt].[DebtDate]
              , [CustomerDebt].[DueDate]
              , [CustomerDebt].[enmDebtStatus]
              , [CustomerDebt].[Notes]
              , [CustomerDebt].[blg_NeedsAttention]
              , [CustomerDebt].[ProductTypes]
              , [CustomerDebt].[DeliveryDate]
              --Auditing 
              , [CustomerDebt].[AddedOn] 
              , CustomerForCustomer.[TEXT] AS CustomerText
              , OrderHeaderForOrderHeader.[TEXT] AS OrderHeaderText
            FROM [CustomerDebt] 
              LEFT OUTER JOIN ccvwComboList_Customer CustomerForCustomer ON [CustomerDebt].CustomerID = CustomerForCustomer.ID 
              LEFT OUTER JOIN ccvwComboList_OrderHeader OrderHeaderForOrderHeader ON [CustomerDebt].OrderHeaderID = OrderHeaderForOrderHeader.ID 
            WHERE  
                ([CustomerDebt].[ID] >= @bndIDFrom) 
            AND ([CustomerDebt].[ID] <= @bndIDTo) 
          ORDER BY [ID] ASC
      IF (@Top <= 0) 
        IF (@Dir = 'DESC') 
          IF (@WithParentText = 1) 
            SELECT  
                [CustomerDebt].[ID]
              , [CustomerDebt].[CustomerID]
              , [CustomerDebt].[OrderHeaderID]
              , [CustomerDebt].[DebtAmount]
              , [CustomerDebt].[PaidAmount]
              , [CustomerDebt].[clc_RemainingAmount]
              , [CustomerDebt].[DebtDate]
              , [CustomerDebt].[DueDate]
              , [CustomerDebt].[enmDebtStatus]
              , [CustomerDebt].[Notes]
              , [CustomerDebt].[blg_NeedsAttention]
              , [CustomerDebt].[ProductTypes]
              , [CustomerDebt].[DeliveryDate]
              --Auditing 
              , [CustomerDebt].[AddedOn] 
              , CustomerForCustomer.[TEXT] AS CustomerText
              , OrderHeaderForOrderHeader.[TEXT] AS OrderHeaderText
            FROM [CustomerDebt] 
              LEFT OUTER JOIN ccvwComboList_Customer CustomerForCustomer ON [CustomerDebt].CustomerID = CustomerForCustomer.ID 
              LEFT OUTER JOIN ccvwComboList_OrderHeader OrderHeaderForOrderHeader ON [CustomerDebt].OrderHeaderID = OrderHeaderForOrderHeader.ID 
            WHERE  
                ([CustomerDebt].[ID] >= @bndIDFrom) 
            AND ([CustomerDebt].[ID] <= @bndIDTo) 
          ORDER BY [ID] DESC
   
   
  RETURN
GO


/* ---- ccCustomerDebtsFillByCustomerID ---- */
CREATE OR ALTER PROCEDURE [dbo].[ccCustomerDebtsFillByCustomerID] 
  ( 
    @CustomerID bigint  
  , @Top int 
  , @Dir varchar(4) 
  , @WithParentText bit = 0
  ) 
AS   
  SET NOCOUNT ON 
 
      IF (@Top > 0) 
        IF (@Dir = 'ASC') 
          IF (@WithParentText = 0) 
            SELECT TOP (@Top) 
                [CustomerDebt].[ID]
              , [CustomerDebt].[CustomerID]
              , [CustomerDebt].[OrderHeaderID]
              , [CustomerDebt].[DebtAmount]
              , [CustomerDebt].[PaidAmount]
              , [CustomerDebt].[clc_RemainingAmount]
              , [CustomerDebt].[DebtDate]
              , [CustomerDebt].[DueDate]
              , [CustomerDebt].[enmDebtStatus]
              , [CustomerDebt].[Notes]
              , [CustomerDebt].[blg_NeedsAttention]
              , [CustomerDebt].[ProductTypes]
              , [CustomerDebt].[DeliveryDate]
              --Auditing 
              , [CustomerDebt].[AddedOn] 
            FROM [CustomerDebt] 
            WHERE  
                ([CustomerDebt].[CustomerID] = @CustomerID) 
            ORDER BY [ID] ASC 
      IF (@Top > 0) 
        IF (@Dir = 'DESC') 
          IF (@WithParentText = 0) 
            SELECT TOP (@Top) 
                [CustomerDebt].[ID]
              , [CustomerDebt].[CustomerID]
              , [CustomerDebt].[OrderHeaderID]
              , [CustomerDebt].[DebtAmount]
              , [CustomerDebt].[PaidAmount]
              , [CustomerDebt].[clc_RemainingAmount]
              , [CustomerDebt].[DebtDate]
              , [CustomerDebt].[DueDate]
              , [CustomerDebt].[enmDebtStatus]
              , [CustomerDebt].[Notes]
              , [CustomerDebt].[blg_NeedsAttention]
              , [CustomerDebt].[ProductTypes]
              , [CustomerDebt].[DeliveryDate]
              --Auditing 
              , [CustomerDebt].[AddedOn] 
            FROM [CustomerDebt] 
            WHERE  
                ([CustomerDebt].[CustomerID] = @CustomerID) 
            ORDER BY [ID] DESC 
      IF (@Top <= 0) 
        IF (@Dir = 'ASC') 
          IF (@WithParentText = 0) 
            SELECT  
                [CustomerDebt].[ID]
              , [CustomerDebt].[CustomerID]
              , [CustomerDebt].[OrderHeaderID]
              , [CustomerDebt].[DebtAmount]
              , [CustomerDebt].[PaidAmount]
              , [CustomerDebt].[clc_RemainingAmount]
              , [CustomerDebt].[DebtDate]
              , [CustomerDebt].[DueDate]
              , [CustomerDebt].[enmDebtStatus]
              , [CustomerDebt].[Notes]
              , [CustomerDebt].[blg_NeedsAttention]
              , [CustomerDebt].[ProductTypes]
              , [CustomerDebt].[DeliveryDate]
              --Auditing 
              , [CustomerDebt].[AddedOn] 
            FROM [CustomerDebt] 
            WHERE  
                ([CustomerDebt].[CustomerID] = @CustomerID) 
            ORDER BY [ID] ASC 
      IF (@Top <= 0) 
        IF (@Dir = 'DESC') 
          IF (@WithParentText = 0) 
            SELECT  
                [CustomerDebt].[ID]
              , [CustomerDebt].[CustomerID]
              , [CustomerDebt].[OrderHeaderID]
              , [CustomerDebt].[DebtAmount]
              , [CustomerDebt].[PaidAmount]
              , [CustomerDebt].[clc_RemainingAmount]
              , [CustomerDebt].[DebtDate]
              , [CustomerDebt].[DueDate]
              , [CustomerDebt].[enmDebtStatus]
              , [CustomerDebt].[Notes]
              , [CustomerDebt].[blg_NeedsAttention]
              , [CustomerDebt].[ProductTypes]
              , [CustomerDebt].[DeliveryDate]
              --Auditing 
              , [CustomerDebt].[AddedOn] 
            FROM [CustomerDebt] 
            WHERE  
                ([CustomerDebt].[CustomerID] = @CustomerID) 
            ORDER BY [ID] DESC 
      IF (@Top > 0) 
        IF (@Dir = 'ASC') 
          IF (@WithParentText = 1) 
            SELECT TOP (@Top) 
                [CustomerDebt].[ID]
              , [CustomerDebt].[CustomerID]
              , [CustomerDebt].[OrderHeaderID]
              , [CustomerDebt].[DebtAmount]
              , [CustomerDebt].[PaidAmount]
              , [CustomerDebt].[clc_RemainingAmount]
              , [CustomerDebt].[DebtDate]
              , [CustomerDebt].[DueDate]
              , [CustomerDebt].[enmDebtStatus]
              , [CustomerDebt].[Notes]
              , [CustomerDebt].[blg_NeedsAttention]
              , [CustomerDebt].[ProductTypes]
              , [CustomerDebt].[DeliveryDate]
              --Auditing 
              , [CustomerDebt].[AddedOn] 
              , CustomerForCustomer.[TEXT] AS CustomerText
              , OrderHeaderForOrderHeader.[TEXT] AS OrderHeaderText
            FROM [CustomerDebt] 
              LEFT OUTER JOIN ccvwComboList_Customer CustomerForCustomer ON [CustomerDebt].CustomerID = CustomerForCustomer.ID 
              LEFT OUTER JOIN ccvwComboList_OrderHeader OrderHeaderForOrderHeader ON [CustomerDebt].OrderHeaderID = OrderHeaderForOrderHeader.ID 
            WHERE  
                ([CustomerDebt].[CustomerID] = @CustomerID) 
            ORDER BY [ID] ASC 
      IF (@Top > 0) 
        IF (@Dir = 'DESC') 
          IF (@WithParentText = 1) 
            SELECT TOP (@Top) 
                [CustomerDebt].[ID]
              , [CustomerDebt].[CustomerID]
              , [CustomerDebt].[OrderHeaderID]
              , [CustomerDebt].[DebtAmount]
              , [CustomerDebt].[PaidAmount]
              , [CustomerDebt].[clc_RemainingAmount]
              , [CustomerDebt].[DebtDate]
              , [CustomerDebt].[DueDate]
              , [CustomerDebt].[enmDebtStatus]
              , [CustomerDebt].[Notes]
              , [CustomerDebt].[blg_NeedsAttention]
              , [CustomerDebt].[ProductTypes]
              , [CustomerDebt].[DeliveryDate]
              --Auditing 
              , [CustomerDebt].[AddedOn] 
              , CustomerForCustomer.[TEXT] AS CustomerText
              , OrderHeaderForOrderHeader.[TEXT] AS OrderHeaderText
            FROM [CustomerDebt] 
              LEFT OUTER JOIN ccvwComboList_Customer CustomerForCustomer ON [CustomerDebt].CustomerID = CustomerForCustomer.ID 
              LEFT OUTER JOIN ccvwComboList_OrderHeader OrderHeaderForOrderHeader ON [CustomerDebt].OrderHeaderID = OrderHeaderForOrderHeader.ID 
            WHERE  
                ([CustomerDebt].[CustomerID] = @CustomerID) 
            ORDER BY [ID] DESC 
      IF (@Top <= 0) 
        IF (@Dir = 'ASC') 
          IF (@WithParentText = 1) 
            SELECT  
                [CustomerDebt].[ID]
              , [CustomerDebt].[CustomerID]
              , [CustomerDebt].[OrderHeaderID]
              , [CustomerDebt].[DebtAmount]
              , [CustomerDebt].[PaidAmount]
              , [CustomerDebt].[clc_RemainingAmount]
              , [CustomerDebt].[DebtDate]
              , [CustomerDebt].[DueDate]
              , [CustomerDebt].[enmDebtStatus]
              , [CustomerDebt].[Notes]
              , [CustomerDebt].[blg_NeedsAttention]
              , [CustomerDebt].[ProductTypes]
              , [CustomerDebt].[DeliveryDate]
              --Auditing 
              , [CustomerDebt].[AddedOn] 
              , CustomerForCustomer.[TEXT] AS CustomerText
              , OrderHeaderForOrderHeader.[TEXT] AS OrderHeaderText
            FROM [CustomerDebt] 
              LEFT OUTER JOIN ccvwComboList_Customer CustomerForCustomer ON [CustomerDebt].CustomerID = CustomerForCustomer.ID 
              LEFT OUTER JOIN ccvwComboList_OrderHeader OrderHeaderForOrderHeader ON [CustomerDebt].OrderHeaderID = OrderHeaderForOrderHeader.ID 
            WHERE  
                ([CustomerDebt].[CustomerID] = @CustomerID) 
            ORDER BY [ID] ASC 
      IF (@Top <= 0) 
        IF (@Dir = 'DESC') 
          IF (@WithParentText = 1) 
            SELECT  
                [CustomerDebt].[ID]
              , [CustomerDebt].[CustomerID]
              , [CustomerDebt].[OrderHeaderID]
              , [CustomerDebt].[DebtAmount]
              , [CustomerDebt].[PaidAmount]
              , [CustomerDebt].[clc_RemainingAmount]
              , [CustomerDebt].[DebtDate]
              , [CustomerDebt].[DueDate]
              , [CustomerDebt].[enmDebtStatus]
              , [CustomerDebt].[Notes]
              , [CustomerDebt].[blg_NeedsAttention]
              , [CustomerDebt].[ProductTypes]
              , [CustomerDebt].[DeliveryDate]
              --Auditing 
              , [CustomerDebt].[AddedOn] 
              , CustomerForCustomer.[TEXT] AS CustomerText
              , OrderHeaderForOrderHeader.[TEXT] AS OrderHeaderText
            FROM [CustomerDebt] 
              LEFT OUTER JOIN ccvwComboList_Customer CustomerForCustomer ON [CustomerDebt].CustomerID = CustomerForCustomer.ID 
              LEFT OUTER JOIN ccvwComboList_OrderHeader OrderHeaderForOrderHeader ON [CustomerDebt].OrderHeaderID = OrderHeaderForOrderHeader.ID 
            WHERE  
                ([CustomerDebt].[CustomerID] = @CustomerID) 
            ORDER BY [ID] DESC 
   
  RETURN
GO


/* ---- ccCustomerDebtsFillByDebtStatus ---- */
CREATE OR ALTER PROCEDURE [dbo].[ccCustomerDebtsFillByDebtStatus] 
  ( 
    @enmDebtStatus nvarchar(50)  
  , @Top int 
  , @Dir varchar(4) 
  , @WithParentText bit = 0
  ) 
AS   
  SET NOCOUNT ON 
 
      IF (@Top > 0) 
        IF (@Dir = 'ASC') 
          IF (@WithParentText = 0) 
            SELECT TOP (@Top) 
                [CustomerDebt].[ID]
              , [CustomerDebt].[CustomerID]
              , [CustomerDebt].[OrderHeaderID]
              , [CustomerDebt].[DebtAmount]
              , [CustomerDebt].[PaidAmount]
              , [CustomerDebt].[clc_RemainingAmount]
              , [CustomerDebt].[DebtDate]
              , [CustomerDebt].[DueDate]
              , [CustomerDebt].[enmDebtStatus]
              , [CustomerDebt].[Notes]
              , [CustomerDebt].[blg_NeedsAttention]
              , [CustomerDebt].[ProductTypes]
              , [CustomerDebt].[DeliveryDate]
              --Auditing 
              , [CustomerDebt].[AddedOn] 
            FROM [CustomerDebt] 
            WHERE  
                ([CustomerDebt].[enmDebtStatus] = @enmDebtStatus OR ([CustomerDebt].[enmDebtStatus] IS NULL AND @enmDebtStatus IS NULL)) 
            ORDER BY [ID] ASC 
      IF (@Top > 0) 
        IF (@Dir = 'DESC') 
          IF (@WithParentText = 0) 
            SELECT TOP (@Top) 
                [CustomerDebt].[ID]
              , [CustomerDebt].[CustomerID]
              , [CustomerDebt].[OrderHeaderID]
              , [CustomerDebt].[DebtAmount]
              , [CustomerDebt].[PaidAmount]
              , [CustomerDebt].[clc_RemainingAmount]
              , [CustomerDebt].[DebtDate]
              , [CustomerDebt].[DueDate]
              , [CustomerDebt].[enmDebtStatus]
              , [CustomerDebt].[Notes]
              , [CustomerDebt].[blg_NeedsAttention]
              , [CustomerDebt].[ProductTypes]
              , [CustomerDebt].[DeliveryDate]
              --Auditing 
              , [CustomerDebt].[AddedOn] 
            FROM [CustomerDebt] 
            WHERE  
                ([CustomerDebt].[enmDebtStatus] = @enmDebtStatus OR ([CustomerDebt].[enmDebtStatus] IS NULL AND @enmDebtStatus IS NULL)) 
            ORDER BY [ID] DESC 
      IF (@Top <= 0) 
        IF (@Dir = 'ASC') 
          IF (@WithParentText = 0) 
            SELECT  
                [CustomerDebt].[ID]
              , [CustomerDebt].[CustomerID]
              , [CustomerDebt].[OrderHeaderID]
              , [CustomerDebt].[DebtAmount]
              , [CustomerDebt].[PaidAmount]
              , [CustomerDebt].[clc_RemainingAmount]
              , [CustomerDebt].[DebtDate]
              , [CustomerDebt].[DueDate]
              , [CustomerDebt].[enmDebtStatus]
              , [CustomerDebt].[Notes]
              , [CustomerDebt].[blg_NeedsAttention]
              , [CustomerDebt].[ProductTypes]
              , [CustomerDebt].[DeliveryDate]
              --Auditing 
              , [CustomerDebt].[AddedOn] 
            FROM [CustomerDebt] 
            WHERE  
                ([CustomerDebt].[enmDebtStatus] = @enmDebtStatus OR ([CustomerDebt].[enmDebtStatus] IS NULL AND @enmDebtStatus IS NULL)) 
            ORDER BY [ID] ASC 
      IF (@Top <= 0) 
        IF (@Dir = 'DESC') 
          IF (@WithParentText = 0) 
            SELECT  
                [CustomerDebt].[ID]
              , [CustomerDebt].[CustomerID]
              , [CustomerDebt].[OrderHeaderID]
              , [CustomerDebt].[DebtAmount]
              , [CustomerDebt].[PaidAmount]
              , [CustomerDebt].[clc_RemainingAmount]
              , [CustomerDebt].[DebtDate]
              , [CustomerDebt].[DueDate]
              , [CustomerDebt].[enmDebtStatus]
              , [CustomerDebt].[Notes]
              , [CustomerDebt].[blg_NeedsAttention]
              , [CustomerDebt].[ProductTypes]
              , [CustomerDebt].[DeliveryDate]
              --Auditing 
              , [CustomerDebt].[AddedOn] 
            FROM [CustomerDebt] 
            WHERE  
                ([CustomerDebt].[enmDebtStatus] = @enmDebtStatus OR ([CustomerDebt].[enmDebtStatus] IS NULL AND @enmDebtStatus IS NULL)) 
            ORDER BY [ID] DESC 
      IF (@Top > 0) 
        IF (@Dir = 'ASC') 
          IF (@WithParentText = 1) 
            SELECT TOP (@Top) 
                [CustomerDebt].[ID]
              , [CustomerDebt].[CustomerID]
              , [CustomerDebt].[OrderHeaderID]
              , [CustomerDebt].[DebtAmount]
              , [CustomerDebt].[PaidAmount]
              , [CustomerDebt].[clc_RemainingAmount]
              , [CustomerDebt].[DebtDate]
              , [CustomerDebt].[DueDate]
              , [CustomerDebt].[enmDebtStatus]
              , [CustomerDebt].[Notes]
              , [CustomerDebt].[blg_NeedsAttention]
              , [CustomerDebt].[ProductTypes]
              , [CustomerDebt].[DeliveryDate]
              --Auditing 
              , [CustomerDebt].[AddedOn] 
              , CustomerForCustomer.[TEXT] AS CustomerText
              , OrderHeaderForOrderHeader.[TEXT] AS OrderHeaderText
            FROM [CustomerDebt] 
              LEFT OUTER JOIN ccvwComboList_Customer CustomerForCustomer ON [CustomerDebt].CustomerID = CustomerForCustomer.ID 
              LEFT OUTER JOIN ccvwComboList_OrderHeader OrderHeaderForOrderHeader ON [CustomerDebt].OrderHeaderID = OrderHeaderForOrderHeader.ID 
            WHERE  
                ([CustomerDebt].[enmDebtStatus] = @enmDebtStatus OR ([CustomerDebt].[enmDebtStatus] IS NULL AND @enmDebtStatus IS NULL)) 
            ORDER BY [ID] ASC 
      IF (@Top > 0) 
        IF (@Dir = 'DESC') 
          IF (@WithParentText = 1) 
            SELECT TOP (@Top) 
                [CustomerDebt].[ID]
              , [CustomerDebt].[CustomerID]
              , [CustomerDebt].[OrderHeaderID]
              , [CustomerDebt].[DebtAmount]
              , [CustomerDebt].[PaidAmount]
              , [CustomerDebt].[clc_RemainingAmount]
              , [CustomerDebt].[DebtDate]
              , [CustomerDebt].[DueDate]
              , [CustomerDebt].[enmDebtStatus]
              , [CustomerDebt].[Notes]
              , [CustomerDebt].[blg_NeedsAttention]
              , [CustomerDebt].[ProductTypes]
              , [CustomerDebt].[DeliveryDate]
              --Auditing 
              , [CustomerDebt].[AddedOn] 
              , CustomerForCustomer.[TEXT] AS CustomerText
              , OrderHeaderForOrderHeader.[TEXT] AS OrderHeaderText
            FROM [CustomerDebt] 
              LEFT OUTER JOIN ccvwComboList_Customer CustomerForCustomer ON [CustomerDebt].CustomerID = CustomerForCustomer.ID 
              LEFT OUTER JOIN ccvwComboList_OrderHeader OrderHeaderForOrderHeader ON [CustomerDebt].OrderHeaderID = OrderHeaderForOrderHeader.ID 
            WHERE  
                ([CustomerDebt].[enmDebtStatus] = @enmDebtStatus OR ([CustomerDebt].[enmDebtStatus] IS NULL AND @enmDebtStatus IS NULL)) 
            ORDER BY [ID] DESC 
      IF (@Top <= 0) 
        IF (@Dir = 'ASC') 
          IF (@WithParentText = 1) 
            SELECT  
                [CustomerDebt].[ID]
              , [CustomerDebt].[CustomerID]
              , [CustomerDebt].[OrderHeaderID]
              , [CustomerDebt].[DebtAmount]
              , [CustomerDebt].[PaidAmount]
              , [CustomerDebt].[clc_RemainingAmount]
              , [CustomerDebt].[DebtDate]
              , [CustomerDebt].[DueDate]
              , [CustomerDebt].[enmDebtStatus]
              , [CustomerDebt].[Notes]
              , [CustomerDebt].[blg_NeedsAttention]
              , [CustomerDebt].[ProductTypes]
              , [CustomerDebt].[DeliveryDate]
              --Auditing 
              , [CustomerDebt].[AddedOn] 
              , CustomerForCustomer.[TEXT] AS CustomerText
              , OrderHeaderForOrderHeader.[TEXT] AS OrderHeaderText
            FROM [CustomerDebt] 
              LEFT OUTER JOIN ccvwComboList_Customer CustomerForCustomer ON [CustomerDebt].CustomerID = CustomerForCustomer.ID 
              LEFT OUTER JOIN ccvwComboList_OrderHeader OrderHeaderForOrderHeader ON [CustomerDebt].OrderHeaderID = OrderHeaderForOrderHeader.ID 
            WHERE  
                ([CustomerDebt].[enmDebtStatus] = @enmDebtStatus OR ([CustomerDebt].[enmDebtStatus] IS NULL AND @enmDebtStatus IS NULL)) 
            ORDER BY [ID] ASC 
      IF (@Top <= 0) 
        IF (@Dir = 'DESC') 
          IF (@WithParentText = 1) 
            SELECT  
                [CustomerDebt].[ID]
              , [CustomerDebt].[CustomerID]
              , [CustomerDebt].[OrderHeaderID]
              , [CustomerDebt].[DebtAmount]
              , [CustomerDebt].[PaidAmount]
              , [CustomerDebt].[clc_RemainingAmount]
              , [CustomerDebt].[DebtDate]
              , [CustomerDebt].[DueDate]
              , [CustomerDebt].[enmDebtStatus]
              , [CustomerDebt].[Notes]
              , [CustomerDebt].[blg_NeedsAttention]
              , [CustomerDebt].[ProductTypes]
              , [CustomerDebt].[DeliveryDate]
              --Auditing 
              , [CustomerDebt].[AddedOn] 
              , CustomerForCustomer.[TEXT] AS CustomerText
              , OrderHeaderForOrderHeader.[TEXT] AS OrderHeaderText
            FROM [CustomerDebt] 
              LEFT OUTER JOIN ccvwComboList_Customer CustomerForCustomer ON [CustomerDebt].CustomerID = CustomerForCustomer.ID 
              LEFT OUTER JOIN ccvwComboList_OrderHeader OrderHeaderForOrderHeader ON [CustomerDebt].OrderHeaderID = OrderHeaderForOrderHeader.ID 
            WHERE  
                ([CustomerDebt].[enmDebtStatus] = @enmDebtStatus OR ([CustomerDebt].[enmDebtStatus] IS NULL AND @enmDebtStatus IS NULL)) 
            ORDER BY [ID] DESC 
   
  RETURN
GO


/* ---- ccCustomerDebtsFillByOrderHeaderID ---- */
CREATE OR ALTER PROCEDURE [dbo].[ccCustomerDebtsFillByOrderHeaderID] 
  ( 
    @OrderHeaderID bigint  
  , @Top int 
  , @Dir varchar(4) 
  , @WithParentText bit = 0
  ) 
AS   
  SET NOCOUNT ON 
 
      IF (@Top > 0) 
        IF (@Dir = 'ASC') 
          IF (@WithParentText = 0) 
            SELECT TOP (@Top) 
                [CustomerDebt].[ID]
              , [CustomerDebt].[CustomerID]
              , [CustomerDebt].[OrderHeaderID]
              , [CustomerDebt].[DebtAmount]
              , [CustomerDebt].[PaidAmount]
              , [CustomerDebt].[clc_RemainingAmount]
              , [CustomerDebt].[DebtDate]
              , [CustomerDebt].[DueDate]
              , [CustomerDebt].[enmDebtStatus]
              , [CustomerDebt].[Notes]
              , [CustomerDebt].[blg_NeedsAttention]
              , [CustomerDebt].[ProductTypes]
              , [CustomerDebt].[DeliveryDate]
              --Auditing 
              , [CustomerDebt].[AddedOn] 
            FROM [CustomerDebt] 
            WHERE  
                ([CustomerDebt].[OrderHeaderID] = @OrderHeaderID OR ([CustomerDebt].[OrderHeaderID] IS NULL AND @OrderHeaderID IS NULL)) 
            ORDER BY [ID] ASC 
      IF (@Top > 0) 
        IF (@Dir = 'DESC') 
          IF (@WithParentText = 0) 
            SELECT TOP (@Top) 
                [CustomerDebt].[ID]
              , [CustomerDebt].[CustomerID]
              , [CustomerDebt].[OrderHeaderID]
              , [CustomerDebt].[DebtAmount]
              , [CustomerDebt].[PaidAmount]
              , [CustomerDebt].[clc_RemainingAmount]
              , [CustomerDebt].[DebtDate]
              , [CustomerDebt].[DueDate]
              , [CustomerDebt].[enmDebtStatus]
              , [CustomerDebt].[Notes]
              , [CustomerDebt].[blg_NeedsAttention]
              , [CustomerDebt].[ProductTypes]
              , [CustomerDebt].[DeliveryDate]
              --Auditing 
              , [CustomerDebt].[AddedOn] 
            FROM [CustomerDebt] 
            WHERE  
                ([CustomerDebt].[OrderHeaderID] = @OrderHeaderID OR ([CustomerDebt].[OrderHeaderID] IS NULL AND @OrderHeaderID IS NULL)) 
            ORDER BY [ID] DESC 
      IF (@Top <= 0) 
        IF (@Dir = 'ASC') 
          IF (@WithParentText = 0) 
            SELECT  
                [CustomerDebt].[ID]
              , [CustomerDebt].[CustomerID]
              , [CustomerDebt].[OrderHeaderID]
              , [CustomerDebt].[DebtAmount]
              , [CustomerDebt].[PaidAmount]
              , [CustomerDebt].[clc_RemainingAmount]
              , [CustomerDebt].[DebtDate]
              , [CustomerDebt].[DueDate]
              , [CustomerDebt].[enmDebtStatus]
              , [CustomerDebt].[Notes]
              , [CustomerDebt].[blg_NeedsAttention]
              , [CustomerDebt].[ProductTypes]
              , [CustomerDebt].[DeliveryDate]
              --Auditing 
              , [CustomerDebt].[AddedOn] 
            FROM [CustomerDebt] 
            WHERE  
                ([CustomerDebt].[OrderHeaderID] = @OrderHeaderID OR ([CustomerDebt].[OrderHeaderID] IS NULL AND @OrderHeaderID IS NULL)) 
            ORDER BY [ID] ASC 
      IF (@Top <= 0) 
        IF (@Dir = 'DESC') 
          IF (@WithParentText = 0) 
            SELECT  
                [CustomerDebt].[ID]
              , [CustomerDebt].[CustomerID]
              , [CustomerDebt].[OrderHeaderID]
              , [CustomerDebt].[DebtAmount]
              , [CustomerDebt].[PaidAmount]
              , [CustomerDebt].[clc_RemainingAmount]
              , [CustomerDebt].[DebtDate]
              , [CustomerDebt].[DueDate]
              , [CustomerDebt].[enmDebtStatus]
              , [CustomerDebt].[Notes]
              , [CustomerDebt].[blg_NeedsAttention]
              , [CustomerDebt].[ProductTypes]
              , [CustomerDebt].[DeliveryDate]
              --Auditing 
              , [CustomerDebt].[AddedOn] 
            FROM [CustomerDebt] 
            WHERE  
                ([CustomerDebt].[OrderHeaderID] = @OrderHeaderID OR ([CustomerDebt].[OrderHeaderID] IS NULL AND @OrderHeaderID IS NULL)) 
            ORDER BY [ID] DESC 
      IF (@Top > 0) 
        IF (@Dir = 'ASC') 
          IF (@WithParentText = 1) 
            SELECT TOP (@Top) 
                [CustomerDebt].[ID]
              , [CustomerDebt].[CustomerID]
              , [CustomerDebt].[OrderHeaderID]
              , [CustomerDebt].[DebtAmount]
              , [CustomerDebt].[PaidAmount]
              , [CustomerDebt].[clc_RemainingAmount]
              , [CustomerDebt].[DebtDate]
              , [CustomerDebt].[DueDate]
              , [CustomerDebt].[enmDebtStatus]
              , [CustomerDebt].[Notes]
              , [CustomerDebt].[blg_NeedsAttention]
              , [CustomerDebt].[ProductTypes]
              , [CustomerDebt].[DeliveryDate]
              --Auditing 
              , [CustomerDebt].[AddedOn] 
              , CustomerForCustomer.[TEXT] AS CustomerText
              , OrderHeaderForOrderHeader.[TEXT] AS OrderHeaderText
            FROM [CustomerDebt] 
              LEFT OUTER JOIN ccvwComboList_Customer CustomerForCustomer ON [CustomerDebt].CustomerID = CustomerForCustomer.ID 
              LEFT OUTER JOIN ccvwComboList_OrderHeader OrderHeaderForOrderHeader ON [CustomerDebt].OrderHeaderID = OrderHeaderForOrderHeader.ID 
            WHERE  
                ([CustomerDebt].[OrderHeaderID] = @OrderHeaderID OR ([CustomerDebt].[OrderHeaderID] IS NULL AND @OrderHeaderID IS NULL)) 
            ORDER BY [ID] ASC 
      IF (@Top > 0) 
        IF (@Dir = 'DESC') 
          IF (@WithParentText = 1) 
            SELECT TOP (@Top) 
                [CustomerDebt].[ID]
              , [CustomerDebt].[CustomerID]
              , [CustomerDebt].[OrderHeaderID]
              , [CustomerDebt].[DebtAmount]
              , [CustomerDebt].[PaidAmount]
              , [CustomerDebt].[clc_RemainingAmount]
              , [CustomerDebt].[DebtDate]
              , [CustomerDebt].[DueDate]
              , [CustomerDebt].[enmDebtStatus]
              , [CustomerDebt].[Notes]
              , [CustomerDebt].[blg_NeedsAttention]
              , [CustomerDebt].[ProductTypes]
              , [CustomerDebt].[DeliveryDate]
              --Auditing 
              , [CustomerDebt].[AddedOn] 
              , CustomerForCustomer.[TEXT] AS CustomerText
              , OrderHeaderForOrderHeader.[TEXT] AS OrderHeaderText
            FROM [CustomerDebt] 
              LEFT OUTER JOIN ccvwComboList_Customer CustomerForCustomer ON [CustomerDebt].CustomerID = CustomerForCustomer.ID 
              LEFT OUTER JOIN ccvwComboList_OrderHeader OrderHeaderForOrderHeader ON [CustomerDebt].OrderHeaderID = OrderHeaderForOrderHeader.ID 
            WHERE  
                ([CustomerDebt].[OrderHeaderID] = @OrderHeaderID OR ([CustomerDebt].[OrderHeaderID] IS NULL AND @OrderHeaderID IS NULL)) 
            ORDER BY [ID] DESC 
      IF (@Top <= 0) 
        IF (@Dir = 'ASC') 
          IF (@WithParentText = 1) 
            SELECT  
                [CustomerDebt].[ID]
              , [CustomerDebt].[CustomerID]
              , [CustomerDebt].[OrderHeaderID]
              , [CustomerDebt].[DebtAmount]
              , [CustomerDebt].[PaidAmount]
              , [CustomerDebt].[clc_RemainingAmount]
              , [CustomerDebt].[DebtDate]
              , [CustomerDebt].[DueDate]
              , [CustomerDebt].[enmDebtStatus]
              , [CustomerDebt].[Notes]
              , [CustomerDebt].[blg_NeedsAttention]
              , [CustomerDebt].[ProductTypes]
              , [CustomerDebt].[DeliveryDate]
              --Auditing 
              , [CustomerDebt].[AddedOn] 
              , CustomerForCustomer.[TEXT] AS CustomerText
              , OrderHeaderForOrderHeader.[TEXT] AS OrderHeaderText
            FROM [CustomerDebt] 
              LEFT OUTER JOIN ccvwComboList_Customer CustomerForCustomer ON [CustomerDebt].CustomerID = CustomerForCustomer.ID 
              LEFT OUTER JOIN ccvwComboList_OrderHeader OrderHeaderForOrderHeader ON [CustomerDebt].OrderHeaderID = OrderHeaderForOrderHeader.ID 
            WHERE  
                ([CustomerDebt].[OrderHeaderID] = @OrderHeaderID OR ([CustomerDebt].[OrderHeaderID] IS NULL AND @OrderHeaderID IS NULL)) 
            ORDER BY [ID] ASC 
      IF (@Top <= 0) 
        IF (@Dir = 'DESC') 
          IF (@WithParentText = 1) 
            SELECT  
                [CustomerDebt].[ID]
              , [CustomerDebt].[CustomerID]
              , [CustomerDebt].[OrderHeaderID]
              , [CustomerDebt].[DebtAmount]
              , [CustomerDebt].[PaidAmount]
              , [CustomerDebt].[clc_RemainingAmount]
              , [CustomerDebt].[DebtDate]
              , [CustomerDebt].[DueDate]
              , [CustomerDebt].[enmDebtStatus]
              , [CustomerDebt].[Notes]
              , [CustomerDebt].[blg_NeedsAttention]
              , [CustomerDebt].[ProductTypes]
              , [CustomerDebt].[DeliveryDate]
              --Auditing 
              , [CustomerDebt].[AddedOn] 
              , CustomerForCustomer.[TEXT] AS CustomerText
              , OrderHeaderForOrderHeader.[TEXT] AS OrderHeaderText
            FROM [CustomerDebt] 
              LEFT OUTER JOIN ccvwComboList_Customer CustomerForCustomer ON [CustomerDebt].CustomerID = CustomerForCustomer.ID 
              LEFT OUTER JOIN ccvwComboList_OrderHeader OrderHeaderForOrderHeader ON [CustomerDebt].OrderHeaderID = OrderHeaderForOrderHeader.ID 
            WHERE  
                ([CustomerDebt].[OrderHeaderID] = @OrderHeaderID OR ([CustomerDebt].[OrderHeaderID] IS NULL AND @OrderHeaderID IS NULL)) 
            ORDER BY [ID] DESC 
   
  RETURN
GO


/* ---- ccCustomerDebtsFillOnTheFly ---- */
CREATE OR ALTER PROCEDURE [dbo].[ccCustomerDebtsFillOnTheFly] 
  ( 
    @bndIDFrom bigint  
  , @bndIDTo bigint  
  , @CustomerID bigint  
  , @OrderHeaderID bigint  
  , @enmDebtStatus nvarchar(50)  
  , @Top int 
  , @Dir varchar(4) 
  , @WithParentText bit = 0
  ) 
WITH EXECUTE AS OWNER 
AS   
  SET NOCOUNT ON
  DECLARE @SQL nvarchar(max) 
  DECLARE @ParamList nvarchar(max) 
 
  SELECT @SQL =  
       'SELECT ' 
 
  IF (@Top > 0)   
    SELECT @SQL = @SQL +  
         ' TOP (@Top) ' 
   
  SELECT @SQL =  @SQL 
      + '   [CustomerDebt].[ID] ' 
      + ' , [CustomerDebt].[CustomerID] ' 
      + ' , [CustomerDebt].[OrderHeaderID] ' 
      + ' , [CustomerDebt].[DebtAmount] ' 
      + ' , [CustomerDebt].[PaidAmount]
              , [CustomerDebt].[clc_RemainingAmount] ' 
      + ' , [CustomerDebt].[DebtDate] ' 
      + ' , [CustomerDebt].[DueDate] ' 
      + ' , [CustomerDebt].[enmDebtStatus] ' 
      + ' , [CustomerDebt].[Notes] ' 
      + ' , [CustomerDebt].[blg_NeedsAttention] ' 
      + ' , [CustomerDebt].[ProductTypes] ' 
      + ' , [CustomerDebt].[DeliveryDate] ' 
      + ' ' 
 
        --Auditing 
      + ' , [CustomerDebt].[AddedOn]  ' 
  IF (@WithParentText = 1) 
    SELECT @SQL =  @SQL  
           +  ' , [CustomerForCustomer].[CustomerName] + '' '' + [CustomerForCustomer].[CustomerCode] AS CustomerText '
 
  IF (@WithParentText = 1) 
    SELECT @SQL =  @SQL  
           +  ' , CAST([OrderHeaderForOrderHeader].[OrderNumber] AS varchar(250)) AS OrderHeaderText '
 
  SELECT @SQL =  @SQL +  
       ' FROM [dbo].[CustomerDebt] ' 
 
  IF (@WithParentText = 1) 
    SELECT @SQL =  @SQL  
      + '  LEFT OUTER JOIN [Customer] CustomerForCustomer ON [CustomerDebt].[CustomerID] = [CustomerForCustomer].[ID] ' 
 
  IF (@WithParentText = 1) 
    SELECT @SQL =  @SQL  
      + '  LEFT OUTER JOIN [OrderHeader] OrderHeaderForOrderHeader ON [CustomerDebt].[OrderHeaderID] = [OrderHeaderForOrderHeader].[ID] ' 
 
  SELECT @SQL =  @SQL +  
       ' WHERE 1 = 1 ' 
 
  IF (@bndIDFrom IS NOT NULL) 
    IF (@bndIDFrom = @bndIDTo) OR (@bndIDTo IS NULL)  
      SELECT @SQL = @SQL +  
        ' AND ([CustomerDebt].[ID] = @bndIDFrom) ' 
    ELSE 
      SELECT @SQL = @SQL +  
        ' AND ([CustomerDebt].[ID] >= @bndIDFrom AND [CustomerDebt].[ID] <= @bndIDTo) ' 
 
  IF (@CustomerID IS NOT NULL) 
    SELECT @SQL = @SQL +  
      ' AND ([CustomerDebt].[CustomerID] = @CustomerID) ' 
 
  IF (@OrderHeaderID IS NOT NULL) 
    SELECT @SQL = @SQL +  
      ' AND ([CustomerDebt].[OrderHeaderID] = @OrderHeaderID) ' 
 
  IF (@enmDebtStatus <> 'UD') 
    SELECT @SQL = @SQL +  
      ' AND ([CustomerDebt].[enmDebtStatus] = @enmDebtStatus) ' 
 
 
  IF (@Dir = 'ASC') 
    SELECT @SQL = @SQL +  
      '  ORDER BY [ID] ASC ' 
  ELSE 
    SELECT @SQL = @SQL +  
      '  ORDER BY [ID] DESC ' 
 
  --PRINT @SQL 
 
  SELECT @ParamList =  '
    @bndIDFrom bigint 
   , @bndIDTo bigint 
   ,@CustomerID bigint 
   ,@OrderHeaderID bigint 
   ,@enmDebtStatus nvarchar(50) 
   ,@Top int'
 
  EXEC sp_executesql @SQL, @ParamList, 
    @bndIDFrom
   , @bndIDTo
   ,@CustomerID
   ,@OrderHeaderID
   ,@enmDebtStatus
   ,@Top 
   WITH RECOMPILE
   
  RETURN
GO


PRINT 'FIX_ProcOrdinalDrift_2026-08-17 applied (16 procedures).';
GO
