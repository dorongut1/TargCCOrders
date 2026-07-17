USE [TargCCOrdersNew]
GO
/****** Object:  Table [dbo].[BeehiveBuyerTracking]    Script Date: 16/07/2026 22:47:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BeehiveBuyerTracking](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[CustomerID] [bigint] NOT NULL,
	[LastOrderDate] [date] NULL,
	[BeehiveQuantity] [int] NULL,
	[ReminderMonth] [int] NULL,
	[blg_IsRelevant] [bit] NOT NULL,
	[Notes] [nvarchar](max) NULL,
	[DeletedOn] [datetime] NULL,
	[DeletedBy] [nvarchar](200) NULL,
	[AddedBy] [nvarchar](200) NULL,
	[AddedOn] [datetime] NULL,
	[ChangedBy] [nvarchar](200) NULL,
	[ChangedOn] [datetime] NULL,
	[UpdatingLoginID] [bigint] NULL,
	[TableName_BeehiveBuyerTracking] [nvarchar](50) NULL,
 CONSTRAINT [PK_BeehiveBuyerTracking] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[ccvwComboList_BeehiveBuyerTracking]    Script Date: 16/07/2026 22:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ccvwComboList_BeehiveBuyerTracking] WITH SCHEMABINDING 
AS 
    SELECT  
      [BeehiveBuyerTracking].[ID] AS [ID],  
      RIGHT(space(18) + CAST([BeehiveBuyerTracking].[ID] as varchar(19)),18) + ' ' AS [TextNS], 
      CAST([BeehiveBuyerTracking].[ID] as varchar(19)) AS [Text] 
    FROM [dbo].[BeehiveBuyerTracking] 
    
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [PK_ccvwComboList_BeehiveBuyerTracking]    Script Date: 16/07/2026 22:47:43 ******/
CREATE UNIQUE CLUSTERED INDEX [PK_ccvwComboList_BeehiveBuyerTracking] ON [dbo].[ccvwComboList_BeehiveBuyerTracking]
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  View [dbo].[ccvwComboList_BeehiveBuyerTrackingForCustomer]    Script Date: 16/07/2026 22:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ccvwComboList_BeehiveBuyerTrackingForCustomer] WITH SCHEMABINDING 
AS 
    SELECT  
      [BeehiveBuyerTracking].[ID] AS [ID],  
      [BeehiveBuyerTracking].[CustomerID] AS [ParentID],  
      RIGHT(space(18) + CAST([BeehiveBuyerTracking].[ID] as varchar(19)),18) + ' ' AS [TextNS], 
      CAST([BeehiveBuyerTracking].[ID] as varchar(19)) AS [Text] 
    FROM [dbo].[BeehiveBuyerTracking] 
    WHERE [BeehiveBuyerTracking].[CustomerID] IS NOT NULL 
    
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [PK_ccvwComboList_BeehiveBuyerTrackingForCustomer]    Script Date: 16/07/2026 22:47:43 ******/
CREATE UNIQUE CLUSTERED INDEX [PK_ccvwComboList_BeehiveBuyerTrackingForCustomer] ON [dbo].[ccvwComboList_BeehiveBuyerTrackingForCustomer]
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Delivery]    Script Date: 16/07/2026 22:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Delivery](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[OrderHeaderID] [bigint] NOT NULL,
	[DeliveryAddress] [nvarchar](500) NULL,
	[ContactPhone] [nvarchar](50) NULL,
	[ContactName] [nvarchar](255) NULL,
	[enmDeliveryMethod] [nvarchar](50) NULL,
	[OrderedDate] [date] NULL,
	[ReceivedDate] [date] NULL,
	[ArrivalToHubDate] [date] NULL,
	[ArrivalToCustomerDate] [date] NULL,
	[enmDeliveryStatus] [nvarchar](50) NOT NULL,
	[Location] [nvarchar](500) NULL,
	[blg_ProductsSummary] [nvarchar](max) NULL,
	[Notes] [nvarchar](max) NULL,
	[DeletedOn] [datetime] NULL,
	[DeletedBy] [nvarchar](200) NULL,
	[AddedBy] [nvarchar](200) NULL,
	[AddedOn] [datetime] NULL,
	[ChangedBy] [nvarchar](200) NULL,
	[ChangedOn] [datetime] NULL,
	[UpdatingLoginID] [bigint] NULL,
	[TableName_Delivery] [nvarchar](50) NULL,
 CONSTRAINT [PK_Delivery] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[ccvwComboList_Delivery]    Script Date: 16/07/2026 22:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ccvwComboList_Delivery] WITH SCHEMABINDING 
AS 
    SELECT  
      [Delivery].[ID] AS [ID],  
      RIGHT(space(18) + CAST([Delivery].[ID] as varchar(19)),18) + ' ' AS [TextNS], 
      CAST([Delivery].[ID] as varchar(19)) AS [Text] 
    FROM [dbo].[Delivery] 
    
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [PK_ccvwComboList_Delivery]    Script Date: 16/07/2026 22:47:43 ******/
CREATE UNIQUE CLUSTERED INDEX [PK_ccvwComboList_Delivery] ON [dbo].[ccvwComboList_Delivery]
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SupplierOrder]    Script Date: 16/07/2026 22:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SupplierOrder](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[OrderHeaderID] [bigint] NOT NULL,
	[SupplierEmail] [nvarchar](255) NULL,
	[EmailSubject] [nvarchar](500) NULL,
	[EmailBody] [nvarchar](max) NULL,
	[enmEmailStatus] [nvarchar](50) NOT NULL,
	[SentDate] [datetime] NULL,
	[blg_TotalCost] [decimal](12, 2) NULL,
	[enmDeliveryMethod] [nvarchar](50) NULL,
	[RequestedDeliveryDate] [date] NULL,
	[RequestedDeliveryDay] [nvarchar](10) NULL,
	[Notes] [nvarchar](max) NULL,
	[DeletedOn] [datetime] NULL,
	[DeletedBy] [nvarchar](200) NULL,
	[AddedBy] [nvarchar](200) NULL,
	[AddedOn] [datetime] NULL,
	[ChangedBy] [nvarchar](200) NULL,
	[ChangedOn] [datetime] NULL,
	[UpdatingLoginID] [bigint] NULL,
	[TableName_SupplierOrder] [nchar](10) NULL,
 CONSTRAINT [PK_SupplierOrder] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[ccvwComboList_SupplierOrder]    Script Date: 16/07/2026 22:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ccvwComboList_SupplierOrder] WITH SCHEMABINDING 
AS 
    SELECT  
      [SupplierOrder].[ID] AS [ID],  
      RIGHT(space(18) + CAST([SupplierOrder].[ID] as varchar(19)),18) + ' ' AS [TextNS], 
      CAST([SupplierOrder].[ID] as varchar(19)) AS [Text] 
    FROM [dbo].[SupplierOrder] 
    
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [PK_ccvwComboList_SupplierOrder]    Script Date: 16/07/2026 22:47:43 ******/
CREATE UNIQUE CLUSTERED INDEX [PK_ccvwComboList_SupplierOrder] ON [dbo].[ccvwComboList_SupplierOrder]
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 16/07/2026 22:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[CustomerCode] [nvarchar](50) NOT NULL,
	[CustomerName] [nvarchar](255) NOT NULL,
	[Phone] [nvarchar](20) NULL,
	[Email] [nvarchar](255) NULL,
	[Address] [nvarchar](max) NULL,
	[City] [nvarchar](100) NULL,
	[TaxID] [nvarchar](20) NULL,
	[enmCustomerType] [nvarchar](50) NULL,
	[PaymentTermsDays] [int] NULL,
	[Notes] [nvarchar](max) NULL,
	[blg_IsActive] [bit] NULL,
	[AddedBy] [nvarchar](50) NOT NULL,
	[AddedOn] [datetime] NOT NULL,
	[ChangedBy] [nvarchar](50) NULL,
	[ChangedOn] [datetime] NULL,
	[DeletedBy] [nvarchar](50) NULL,
	[DeletedOn] [datetime] NULL,
	[UpdatingLoginID] [bigint] NULL,
	[Location] [nvarchar](100) NULL,
	[AccountantEmail] [nvarchar](255) NULL,
	[enmAccountantMethod] [nvarchar](50) NULL,
	[InvoiceName] [nvarchar](255) NULL,
	[ProfitabilityCode] [nvarchar](50) NULL,
	[clc_CustomerIdentifier]  AS ((CONVERT([nvarchar](50),[CustomerCode])+N' ')+[CustomerName]) PERSISTED,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[ccvwComboList_Customer]    Script Date: 16/07/2026 22:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ccvwComboList_Customer] WITH SCHEMABINDING 
AS 
    SELECT  
      [Customer].[ID] AS [ID],  
      ' ' + CAST(REPLACE(COALESCE([Customer].[CustomerName], '') 
      + ' ' + COALESCE([Customer].[CustomerCode], '') 
     , ' ','') AS nvarchar(200)) + ' ' AS [TextNS], 
      COALESCE([Customer].[CustomerName], '') 
      + ' ' + COALESCE([Customer].[CustomerCode], '') 
      AS [Text] 
    FROM [dbo].[Customer] 
    
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [PK_ccvwComboList_Customer]    Script Date: 16/07/2026 22:47:43 ******/
CREATE UNIQUE CLUSTERED INDEX [PK_ccvwComboList_Customer] ON [dbo].[ccvwComboList_Customer]
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[c_LoggedAlert]    Script Date: 16/07/2026 22:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[c_LoggedAlert](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[TimeOccurred] [datetime] NULL,
	[FaultNumber] [int] NULL,
	[SystemName] [varchar](50) NULL,
	[CallingApplication] [varchar](50) NULL,
	[AffectedUserID] [bigint] NULL,
	[CallingApplicationVersion] [varchar](50) NULL,
	[CallingFunctionWithinApplication] [varchar](100) NULL,
	[FreeText] [nvarchar](max) NULL,
	[FaultingAssembly] [varchar](100) NULL,
	[AssemblyEntryPoint] [varchar](100) NULL,
	[FaultingClass] [varchar](50) NULL,
	[FaultingFunction] [varchar](100) NULL,
	[FaultingFunctionParameters] [nvarchar](max) NULL,
	[FaultIdent] [varchar](100) NULL,
	[FaultDescription] [nvarchar](100) NULL,
	[MessageSentToUser] [nvarchar](100) NULL,
	[ActionSentToUser] [nvarchar](200) NULL,
	[enmFaultType_FaultType] [varchar](50) NULL,
	[enmFaultSeverity_FaultSeverity] [varchar](50) NULL,
	[c_LoggedLoginID] [bigint] NULL,
	[Thread] [varchar](50) NULL,
	[lkpUserIdentityType] [varchar](50) NULL,
	[lkpUserIdentityTypeName] [int] NULL,
	[clc_DateOccurred]  AS (CONVERT([date],[TimeOccurred])) PERSISTED,
	[clc_MonthOccurred]  AS (datefromparts(datepart(year,[TimeOccurred]),datepart(month,[TimeOccurred]),(1))) PERSISTED,
 CONSTRAINT [PK_c_LoggedAlert] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[ccvwComboList_c_LoggedAlertForAffectedUser]    Script Date: 16/07/2026 22:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ccvwComboList_c_LoggedAlertForAffectedUser] WITH SCHEMABINDING 
AS 
    SELECT  
      [c_LoggedAlert].[ID] AS [ID],  
      [c_LoggedAlert].[AffectedUserID] AS [ParentID],  
      RIGHT(space(18) + CAST([c_LoggedAlert].[ID] as varchar(19)),18) + ' ' AS [TextNS], 
      CAST([c_LoggedAlert].[ID] as varchar(19)) AS [Text] 
    FROM [dbo].[c_LoggedAlert] 
    WHERE [c_LoggedAlert].[AffectedUserID] IS NOT NULL 
    
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [PK_ccvwComboList_c_LoggedAlertForAffectedUser]    Script Date: 16/07/2026 22:47:43 ******/
CREATE UNIQUE CLUSTERED INDEX [PK_ccvwComboList_c_LoggedAlertForAffectedUser] ON [dbo].[ccvwComboList_c_LoggedAlertForAffectedUser]
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomerDebt]    Script Date: 16/07/2026 22:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerDebt](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[CustomerID] [bigint] NOT NULL,
	[OrderHeaderID] [bigint] NULL,
	[DebtAmount] [decimal](10, 2) NOT NULL,
	[PaidAmount] [decimal](10, 2) NULL,
	[clc_RemainingAmount]  AS ([DebtAmount]-[PaidAmount]) PERSISTED,
	[DebtDate] [date] NOT NULL,
	[DueDate] [date] NULL,
	[enmDebtStatus] [nvarchar](50) NULL,
	[Notes] [nvarchar](max) NULL,
	[AddedBy] [nvarchar](50) NOT NULL,
	[AddedOn] [datetime] NOT NULL,
	[ChangedBy] [nvarchar](50) NULL,
	[ChangedOn] [datetime] NULL,
	[DeletedBy] [nvarchar](50) NULL,
	[DeletedOn] [datetime] NULL,
	[UpdatingLoginID] [bigint] NULL,
	[blg_NeedsAttention] [bit] NOT NULL,
	[ProductTypes] [nvarchar](500) NULL,
	[DeliveryDate] [date] NULL,
 CONSTRAINT [PK_CustomerDebt] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[ccvwComboList_CustomerDebt]    Script Date: 16/07/2026 22:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ccvwComboList_CustomerDebt] WITH SCHEMABINDING 
AS 
    SELECT  
      [CustomerDebt].[ID] AS [ID],  
      ' ' + CAST(REPLACE(COALESCE([Customer].[CustomerName], '') 
      + ' ' + COALESCE([Customer].[CustomerCode], '') 
     
      + 'bt of ' + COALESCE(CAST([CustomerDebt].[DebtAmount] as varchar(50)), '') 
     , ' ','') AS nvarchar(200)) + ' ' AS [TextNS], 
      COALESCE([Customer].[CustomerName], '') 
      + ' ' + COALESCE([Customer].[CustomerCode], '') 
     
      + 'bt of ' + COALESCE(CAST([CustomerDebt].[DebtAmount] as varchar(50)), '') 
      AS [Text] 
    FROM [dbo].[CustomerDebt] 
      INNER JOIN [dbo].[Customer] ON [Customer].[ID] = [CustomerDebt].[CustomerID] 
    
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [PK_ccvwComboList_CustomerDebt]    Script Date: 16/07/2026 22:47:43 ******/
CREATE UNIQUE CLUSTERED INDEX [PK_ccvwComboList_CustomerDebt] ON [dbo].[ccvwComboList_CustomerDebt]
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[c_MFA]    Script Date: 16/07/2026 22:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[c_MFA](
	[TableName_c_MFA] [bit] NULL,
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[CellOrEmail] [varchar](50) NULL,
	[ProtectedFunction] [varchar](50) NULL,
	[enoCode] [varchar](64) NULL,
	[AttemptNo] [int] NULL,
	[IsSuccessful] [bit] NULL,
	[LastAccessingIP] [varchar](50) NULL,
	[LastAccessingCountry] [varchar](5) NULL,
	[enmUILang_Language] [varchar](5) NULL,
	[WhenCreated] [datetimeoffset](7) NULL,
	[WhenAccessed] [datetimeoffset](7) NULL,
	[WhenExpires] [datetimeoffset](7) NULL,
	[AddedBy] [nvarchar](50) NULL,
	[AddedOn] [datetime] NOT NULL,
	[ChangedBy] [nvarchar](50) NULL,
	[ChangedOn] [datetime] NULL,
	[DeletedBy] [nvarchar](50) NULL,
	[DeletedOn] [datetime] NULL,
	[UpdatingLoginID] [bigint] NULL,
	[Details] [nvarchar](500) NULL,
	[c_UserID] [bigint] NULL,
 CONSTRAINT [PK_c_MFA] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[ccvwComboList_c_MFAForUser]    Script Date: 16/07/2026 22:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ccvwComboList_c_MFAForUser] WITH SCHEMABINDING 
AS 
    SELECT  
      [c_MFA].[ID] AS [ID],  
      [c_MFA].[c_UserID] AS [ParentID],  
      ' ' + CAST(REPLACE(COALESCE([c_MFA].[CellOrEmail], '') 
     , ' ','') AS nvarchar(200)) + ' ' AS [TextNS], 
      COALESCE([c_MFA].[CellOrEmail], '') 
      AS [Text] 
    FROM [dbo].[c_MFA] 
    WHERE [c_MFA].[c_UserID] IS NOT NULL 
    
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [PK_ccvwComboList_c_MFAForUser]    Script Date: 16/07/2026 22:47:43 ******/
CREATE UNIQUE CLUSTERED INDEX [PK_ccvwComboList_c_MFAForUser] ON [dbo].[ccvwComboList_c_MFAForUser]
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  View [dbo].[ccvwComboList_CustomerDebtForCustomer]    Script Date: 16/07/2026 22:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ccvwComboList_CustomerDebtForCustomer] WITH SCHEMABINDING 
AS 
    SELECT  
      [CustomerDebt].[ID] AS [ID],  
      [CustomerDebt].[CustomerID] AS [ParentID],  
      ' ' + CAST(REPLACE(COALESCE([Customer].[CustomerName], '') 
      + ' ' + COALESCE([Customer].[CustomerCode], '') 
     
      + 'bt of ' + COALESCE(CAST([CustomerDebt].[DebtAmount] as varchar(50)), '') 
     , ' ','') AS nvarchar(200)) + ' ' AS [TextNS], 
      COALESCE([Customer].[CustomerName], '') 
      + ' ' + COALESCE([Customer].[CustomerCode], '') 
     
      + 'bt of ' + COALESCE(CAST([CustomerDebt].[DebtAmount] as varchar(50)), '') 
      AS [Text] 
    FROM [dbo].[CustomerDebt] 
      INNER JOIN [dbo].[Customer] ON [Customer].[ID] = [CustomerDebt].[CustomerID] 
    WHERE [CustomerDebt].[CustomerID] IS NOT NULL 
    
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [PK_ccvwComboList_CustomerDebtForCustomer]    Script Date: 16/07/2026 22:47:43 ******/
CREATE UNIQUE CLUSTERED INDEX [PK_ccvwComboList_CustomerDebtForCustomer] ON [dbo].[ccvwComboList_CustomerDebtForCustomer]
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderHeader]    Script Date: 16/07/2026 22:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderHeader](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[OrderNumber] [int] NOT NULL,
	[CustomerID] [bigint] NOT NULL,
	[OrderDate] [datetime] NOT NULL,
	[clc_TotalAmount] [decimal](12, 2) NULL,
	[clc_VATAmount] [decimal](10, 2) NULL,
	[clc_TotalWithVAT] [decimal](12, 2) NULL,
	[enmPaymentMethod] [nvarchar](50) NULL,
	[enmPaymentStatus] [nvarchar](50) NULL,
	[PaymentDate] [date] NULL,
	[InvoiceNumber] [nvarchar](50) NULL,
	[enmDeliveryMethod] [nvarchar](50) NULL,
	[DeliveryDate] [date] NULL,
	[enmDeliveryDay] [nvarchar](10) NULL,
	[enmOrderStatus] [nvarchar](50) NULL,
	[Notes] [nvarchar](max) NULL,
	[AddedBy] [nvarchar](50) NOT NULL,
	[AddedOn] [datetime] NOT NULL,
	[ChangedBy] [nvarchar](50) NULL,
	[ChangedOn] [datetime] NULL,
	[DeletedBy] [nvarchar](50) NULL,
	[DeletedOn] [datetime] NULL,
	[UpdatingLoginID] [bigint] NULL,
	[Notes2] [nvarchar](max) NULL,
	[clc_OrderMonth]  AS ((right('0'+CONVERT([varchar](2),datepart(month,[OrderDate])),(2))+N' ')+case datepart(month,[OrderDate]) when (1) then N'ינואר' when (2) then N'פברואר' when (3) then N'מרץ' when (4) then N'אפריל' when (5) then N'מאי' when (6) then N'יוני' when (7) then N'יולי' when (8) then N'אוגוסט' when (9) then N'ספטמבר' when (10) then N'אוקטובר' when (11) then N'נובמבר' when (12) then N'דצמבר'  end),
	[clc_Quarter]  AS ('Q'+CONVERT([varchar](1),datepart(quarter,[OrderDate]))),
 CONSTRAINT [PK_OrderHeader] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_OrderHeader_OrderNumber] UNIQUE NONCLUSTERED 
(
	[OrderNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[ccvwComboList_OrderHeader]    Script Date: 16/07/2026 22:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ccvwComboList_OrderHeader] WITH SCHEMABINDING 
AS 
    SELECT  
      [OrderHeader].[ID] AS [ID],  
      RIGHT(space(18) + CAST([OrderHeader].[OrderNumber] as varchar(19)),18) + ' ' AS [TextNS], 
      CAST([OrderHeader].[OrderNumber] as varchar(19)) AS [Text] 
    FROM [dbo].[OrderHeader] 
    
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [PK_ccvwComboList_OrderHeader]    Script Date: 16/07/2026 22:47:43 ******/
CREATE UNIQUE CLUSTERED INDEX [PK_ccvwComboList_OrderHeader] ON [dbo].[ccvwComboList_OrderHeader]
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  View [dbo].[ccvwComboList_OrderHeaderForCustomer]    Script Date: 16/07/2026 22:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ccvwComboList_OrderHeaderForCustomer] WITH SCHEMABINDING 
AS 
    SELECT  
      [OrderHeader].[ID] AS [ID],  
      [OrderHeader].[CustomerID] AS [ParentID],  
      RIGHT(space(18) + CAST([OrderHeader].[OrderNumber] as varchar(19)),18) + ' ' AS [TextNS], 
      CAST([OrderHeader].[OrderNumber] as varchar(19)) AS [Text] 
    FROM [dbo].[OrderHeader] 
    WHERE [OrderHeader].[CustomerID] IS NOT NULL 
    
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [PK_ccvwComboList_OrderHeaderForCustomer]    Script Date: 16/07/2026 22:47:43 ******/
CREATE UNIQUE CLUSTERED INDEX [PK_ccvwComboList_OrderHeaderForCustomer] ON [dbo].[ccvwComboList_OrderHeaderForCustomer]
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderLine]    Script Date: 16/07/2026 22:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderLine](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[OrderHeaderID] [bigint] NOT NULL,
	[ProductID] [bigint] NOT NULL,
	[Quantity] [int] NOT NULL,
	[UnitPrice] [decimal](10, 2) NOT NULL,
	[DiscountPercent] [decimal](5, 2) NULL,
	[blg_UnitCost] [decimal](10, 2) NULL,
	[LineNumber] [int] NOT NULL,
	[AddedBy] [nvarchar](50) NOT NULL,
	[AddedOn] [datetime] NOT NULL,
	[ChangedBy] [nvarchar](50) NULL,
	[ChangedOn] [datetime] NULL,
	[DeletedBy] [nvarchar](50) NULL,
	[DeletedOn] [datetime] NULL,
	[UpdatingLoginID] [bigint] NULL,
	[clc_LineTotal]  AS (([Quantity]*[UnitPrice])*((1)-isnull([DiscountPercent],(0))/(100))) PERSISTED,
	[clc_TotalCost]  AS ([Quantity]*isnull([blg_UnitCost],(0))) PERSISTED,
	[clc_Profit]  AS (([Quantity]*[UnitPrice])*((1)-isnull([DiscountPercent],(0))/(100))-[Quantity]*isnull([blg_UnitCost],(0))) PERSISTED,
 CONSTRAINT [PK_OrderLine] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 16/07/2026 22:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[ProductCode] [nvarchar](50) NOT NULL,
	[ProductName] [nvarchar](255) NOT NULL,
	[enmCategory] [nvarchar](50) NULL,
	[UnitOfMeasure] [nvarchar](20) NULL,
	[Notes] [nvarchar](max) NULL,
	[blg_IsActive] [bit] NULL,
	[clc_CurrentStock] [int] NULL,
	[AddedBy] [nvarchar](50) NOT NULL,
	[AddedOn] [datetime] NOT NULL,
	[ChangedBy] [nvarchar](50) NULL,
	[ChangedOn] [datetime] NULL,
	[DeletedBy] [nvarchar](50) NULL,
	[DeletedOn] [datetime] NULL,
	[UpdatingLoginID] [bigint] NULL,
	[BaseCost] [decimal](10, 2) NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[ccvwComboList_OrderLine]    Script Date: 16/07/2026 22:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ccvwComboList_OrderLine] WITH SCHEMABINDING 
AS 
    SELECT  
      [OrderLine].[ID] AS [ID],  
      ' ' + CAST(REPLACE(COALESCE([Product].[ProductCode], '') 
      + ' - ' + COALESCE([Product].[ProductName], '') 
     
      + ' - Qty: ' + COALESCE(CAST([OrderLine].[Quantity] as varchar(50)), '') 
     , ' ','') AS nvarchar(200)) + ' ' AS [TextNS], 
      COALESCE([Product].[ProductCode], '') 
      + ' - ' + COALESCE([Product].[ProductName], '') 
     
      + ' - Qty: ' + COALESCE(CAST([OrderLine].[Quantity] as varchar(50)), '') 
      AS [Text] 
    FROM [dbo].[OrderLine] 
      INNER JOIN [dbo].[Product] ON [Product].[ID] = [OrderLine].[ProductID] 
    
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [PK_ccvwComboList_OrderLine]    Script Date: 16/07/2026 22:47:43 ******/
CREATE UNIQUE CLUSTERED INDEX [PK_ccvwComboList_OrderLine] ON [dbo].[ccvwComboList_OrderLine]
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  View [dbo].[ccvwComboList_Product]    Script Date: 16/07/2026 22:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ccvwComboList_Product] WITH SCHEMABINDING 
AS 
    SELECT  
      [Product].[ID] AS [ID],  
      ' ' + CAST(REPLACE(COALESCE([Product].[ProductCode], '') 
      + ' - ' + COALESCE([Product].[ProductName], '') 
     , ' ','') AS nvarchar(200)) + ' ' AS [TextNS], 
      COALESCE([Product].[ProductCode], '') 
      + ' - ' + COALESCE([Product].[ProductName], '') 
      AS [Text] 
    FROM [dbo].[Product] 
    
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [PK_ccvwComboList_Product]    Script Date: 16/07/2026 22:47:43 ******/
CREATE UNIQUE CLUSTERED INDEX [PK_ccvwComboList_Product] ON [dbo].[ccvwComboList_Product]
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductPrice]    Script Date: 16/07/2026 22:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductPrice](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[ProductID] [bigint] NOT NULL,
	[enmCustomerType] [nvarchar](50) NOT NULL,
	[SellingPrice] [decimal](10, 2) NOT NULL,
	[MinQuantity] [int] NULL,
	[DiscountPercent] [decimal](5, 2) NULL,
	[Notes] [nvarchar](max) NULL,
	[ChangedBy] [nvarchar](50) NULL,
	[ChangedOn] [datetime] NULL,
	[DeletedBy] [nvarchar](50) NULL,
	[DeletedOn] [datetime] NULL,
	[UpdatingLoginID] [bigint] NULL,
	[AddedBy] [nvarchar](50) NULL,
	[AddedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_ProductPrice] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[ccvwComboList_ProductPrice]    Script Date: 16/07/2026 22:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ccvwComboList_ProductPrice] WITH SCHEMABINDING 
AS 
    SELECT  
      [ProductPrice].[ID] AS [ID],  
      ' ' + CAST(REPLACE(COALESCE([Product].[ProductCode], '') 
      + ' - ' + COALESCE([Product].[ProductName], '') 
     
      + ' - ' + COALESCE([ProductPrice].[enmCustomerType], '') 
      + ': ?' + COALESCE(CAST([ProductPrice].[SellingPrice] as varchar(50)), '') 
     , ' ','') AS nvarchar(200)) + ' ' AS [TextNS], 
      COALESCE([Product].[ProductCode], '') 
      + ' - ' + COALESCE([Product].[ProductName], '') 
     
      + ' - ' + COALESCE([ProductPrice].[enmCustomerType], '') 
      + ': ?' + COALESCE(CAST([ProductPrice].[SellingPrice] as varchar(50)), '') 
      AS [Text] 
    FROM [dbo].[ProductPrice] 
      INNER JOIN [dbo].[Product] ON [Product].[ID] = [ProductPrice].[ProductID] 
    
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [PK_ccvwComboList_ProductPrice]    Script Date: 16/07/2026 22:47:43 ******/
CREATE UNIQUE CLUSTERED INDEX [PK_ccvwComboList_ProductPrice] ON [dbo].[ccvwComboList_ProductPrice]
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductPriceHist]    Script Date: 16/07/2026 22:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductPriceHist](
	[TableName_ProductPriceHist] [bit] NULL,
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[ProductID] [bigint] NOT NULL,
	[enmCustomerType] [nvarchar](50) NOT NULL,
	[BaseCost] [decimal](10, 2) NULL,
	[SellingPrice] [decimal](10, 2) NOT NULL,
	[MinQuantity] [int] NULL,
	[DiscountPercent] [decimal](5, 2) NULL,
	[ValidFrom] [date] NOT NULL,
	[ValidTo] [date] NOT NULL,
	[ArchivedDate] [datetime] NULL,
	[ArchivedReason] [nvarchar](255) NULL,
	[OriginalPriceID] [bigint] NULL,
	[Notes] [nvarchar](max) NULL,
	[AddFieldsHere] [nvarchar](50) NULL,
	[ChangedBy] [nvarchar](50) NULL,
	[ChangedOn] [datetime] NULL,
	[DeletedBy] [nvarchar](50) NULL,
	[DeletedOn] [datetime] NULL,
	[UpdatingLoginID] [bigint] NULL,
	[AddedBy] [nvarchar](50) NULL,
	[AddedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_ProductPriceHist] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[ccvwComboList_ProductPriceHist]    Script Date: 16/07/2026 22:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ccvwComboList_ProductPriceHist] WITH SCHEMABINDING 
AS 
    SELECT  
      [ProductPriceHist].[ID] AS [ID],  
      ' ' + CAST(REPLACE(COALESCE(CAST([ProductPriceHist].[ProductID] as varchar(50)), '') 
      + ' ' + COALESCE([ProductPriceHist].[enmCustomerType], '') 
     , ' ','') AS nvarchar(200)) + ' ' AS [TextNS], 
      COALESCE(CAST([ProductPriceHist].[ProductID] as varchar(50)), '') 
      + ' ' + COALESCE([ProductPriceHist].[enmCustomerType], '') 
      AS [Text] 
    FROM [dbo].[ProductPriceHist] 
    
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [PK_ccvwComboList_ProductPriceHist]    Script Date: 16/07/2026 22:47:43 ******/
CREATE UNIQUE CLUSTERED INDEX [PK_ccvwComboList_ProductPriceHist] ON [dbo].[ccvwComboList_ProductPriceHist]
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[c_AlertMessage]    Script Date: 16/07/2026 22:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[c_AlertMessage](
	[TableName_c_AlertMessage] [bit] NOT NULL,
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Number] [int] NULL,
	[Description] [nvarchar](100) NULL,
	[enmType_FaultType] [varchar](50) NULL,
	[enmSeverity_FaultSeverity] [varchar](50) NULL,
	[locMessage] [nvarchar](100) NULL,
	[locAction] [nvarchar](100) NULL,
	[ChangedBy] [nvarchar](50) NULL,
	[ChangedOn] [datetime] NULL,
	[UpdatingLoginID] [bigint] NULL,
	[AddedBy] [nvarchar](50) NULL,
	[AddedOn] [datetime] NULL,
	[DeletedBy] [nvarchar](50) NULL,
	[DeletedOn] [datetime] NULL,
 CONSTRAINT [PK_c_AlertMessage] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[ccvwComboList_c_AlertMessage]    Script Date: 16/07/2026 22:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ccvwComboList_c_AlertMessage] WITH SCHEMABINDING 
AS 
    SELECT  
      [c_AlertMessage].[ID] AS [ID],  
      RIGHT(space(18) + CAST([c_AlertMessage].[Number] as varchar(19)),18) + ' ' AS [TextNS], 
      CAST([c_AlertMessage].[Number] as varchar(19)) AS [Text] 
    FROM [dbo].[c_AlertMessage] 
    
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [PK_ccvwComboList_c_AlertMessage]    Script Date: 16/07/2026 22:47:43 ******/
CREATE UNIQUE CLUSTERED INDEX [PK_ccvwComboList_c_AlertMessage] ON [dbo].[ccvwComboList_c_AlertMessage]
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[c_Enumeration]    Script Date: 16/07/2026 22:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[c_Enumeration](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[IsSystem] [bit] NULL,
	[EnumType] [varchar](50) NULL,
	[EnumValue] [varchar](50) NULL,
	[locText] [nvarchar](50) NULL,
	[AddedBy] [nvarchar](50) NULL,
	[AddedOn] [datetime] NOT NULL,
	[ChangedBy] [nvarchar](50) NULL,
	[ChangedOn] [datetime] NULL,
	[TableName_c_Enumeration] [bit] NULL,
	[DeletedBy] [nvarchar](50) NULL,
	[DeletedOn] [datetime] NULL,
	[UpdatingLoginID] [bigint] NULL,
 CONSTRAINT [PK_c_Enumeration] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[ccvwComboList_c_Enumeration]    Script Date: 16/07/2026 22:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ccvwComboList_c_Enumeration] WITH SCHEMABINDING 
AS 
    SELECT  
      [c_Enumeration].[ID] AS [ID],  
      ' ' + CAST(REPLACE(COALESCE([c_Enumeration].[EnumType], '') 
      + ' --> ' + COALESCE([c_Enumeration].[EnumValue], '') 
     , ' ','') AS nvarchar(200)) + ' ' AS [TextNS], 
      COALESCE([c_Enumeration].[EnumType], '') 
      + ' --> ' + COALESCE([c_Enumeration].[EnumValue], '') 
      AS [Text] 
    FROM [dbo].[c_Enumeration] 
    
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [PK_ccvwComboList_c_Enumeration]    Script Date: 16/07/2026 22:47:43 ******/
CREATE UNIQUE CLUSTERED INDEX [PK_ccvwComboList_c_Enumeration] ON [dbo].[ccvwComboList_c_Enumeration]
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[c_Job]    Script Date: 16/07/2026 22:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[c_Job](
	[TableName_c_Job] [bit] NULL,
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[lkpJob] [varchar](50) NULL,
	[lkpJobRunner] [varchar](50) NULL,
	[Description] [nvarchar](500) NULL,
	[Instructions] [nvarchar](1000) NULL,
	[enmJobType] [varchar](50) NULL,
	[WhenToRun] [datetime] NULL,
	[CyclicCount] [int] NULL,
	[SendNotificationOnSuccess] [bit] NULL,
	[SendAlarmOnMissed] [bit] NULL,
	[TimeOutSec] [int] NULL,
	[Active] [bit] NULL,
	[ActivatingUser] [varchar](50) NULL,
	[NextRunTime] [datetime] NULL,
	[LastRunTime] [datetime] NULL,
	[enmJobStatus] [varchar](50) NULL,
	[WarningMailSent] [bit] NULL,
	[IsManaged] [bit] NULL,
	[LastRunBy] [nvarchar](50) NULL,
	[AddedBy] [nvarchar](50) NOT NULL,
	[AddedOn] [datetime] NOT NULL,
	[ChangedBy] [nvarchar](50) NULL,
	[ChangedOn] [datetime] NULL,
	[DeletedBy] [nvarchar](50) NULL,
	[DeletedOn] [datetime] NULL,
	[UpdatingLoginID] [bigint] NULL,
 CONSTRAINT [PK_c_Job] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[ccvwComboList_c_Job]    Script Date: 16/07/2026 22:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ccvwComboList_c_Job] WITH SCHEMABINDING 
AS 
    SELECT  
      [c_Job].[ID] AS [ID],  
      ' ' + CAST(REPLACE(COALESCE([c_Job].[lkpJob], '') 
      + ' on ' + COALESCE([c_Job].[lkpJobRunner], '') 
     , ' ','') AS nvarchar(200)) + ' ' AS [TextNS], 
      COALESCE([c_Job].[lkpJob], '') 
      + ' on ' + COALESCE([c_Job].[lkpJobRunner], '') 
      AS [Text] 
    FROM [dbo].[c_Job] 
    
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [PK_ccvwComboList_c_Job]    Script Date: 16/07/2026 22:47:43 ******/
CREATE UNIQUE CLUSTERED INDEX [PK_ccvwComboList_c_Job] ON [dbo].[ccvwComboList_c_Job]
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[c_Language]    Script Date: 16/07/2026 22:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[c_Language](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](50) NULL,
	[Name] [varchar](50) NULL,
	[NameLoc] [nvarchar](50) NULL,
	[Culture] [varchar](10) NULL,
 CONSTRAINT [PK_c_Language] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[ccvwComboList_c_Language]    Script Date: 16/07/2026 22:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ccvwComboList_c_Language] WITH SCHEMABINDING 
AS 
    SELECT  
      [c_Language].[ID] AS [ID],  
      ' ' + CAST(REPLACE(COALESCE([c_Language].[Name], '') 
     , ' ','') AS nvarchar(200)) + ' ' AS [TextNS], 
      COALESCE([c_Language].[Name], '') 
      AS [Text] 
    FROM [dbo].[c_Language] 
    
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [PK_ccvwComboList_c_Language]    Script Date: 16/07/2026 22:47:43 ******/
CREATE UNIQUE CLUSTERED INDEX [PK_ccvwComboList_c_Language] ON [dbo].[ccvwComboList_c_Language]
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  View [dbo].[ccvwComboList_c_LoggedAlert]    Script Date: 16/07/2026 22:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ccvwComboList_c_LoggedAlert] WITH SCHEMABINDING 
AS 
    SELECT  
      [c_LoggedAlert].[ID] AS [ID],  
      RIGHT(space(18) + CAST([c_LoggedAlert].[ID] as varchar(19)),18) + ' ' AS [TextNS], 
      CAST([c_LoggedAlert].[ID] as varchar(19)) AS [Text] 
    FROM [dbo].[c_LoggedAlert] 
    
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [PK_ccvwComboList_c_LoggedAlert]    Script Date: 16/07/2026 22:47:43 ******/
CREATE UNIQUE CLUSTERED INDEX [PK_ccvwComboList_c_LoggedAlert] ON [dbo].[ccvwComboList_c_LoggedAlert]
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[c_LoggedLogin]    Script Date: 16/07/2026 22:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[c_LoggedLogin](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](50) NULL,
	[UserFullName] [nvarchar](50) NULL,
	[TimeLoggedIn] [datetime] NULL,
	[ApplicationName] [varchar](50) NULL,
	[lkpUserIdentityType] [varchar](50) NULL,
	[lkpUserIdentityTypeName] [int] NULL,
	[Roles] [varchar](250) NULL,
	[TimeLoggedOut] [datetime] NULL,
	[LoginFaultNumber] [int] NULL,
	[EnvironmentUserName] [nvarchar](100) NULL,
	[EnvironmentMachineName] [nvarchar](50) NULL,
	[EnvironmentUserDomainName] [nchar](10) NULL,
	[DnsGetHostName] [varchar](50) NULL,
	[AddressList] [varchar](100) NULL,
	[ComputerMACAddress] [varchar](100) NULL,
	[SystemDiskVolumeSerialNo] [varchar](100) NULL,
	[LocalTime] [datetime] NULL,
	[GmtTime] [datetime] NULL,
	[AccessingComputerDetails] [varchar](250) NULL,
	[UICulture] [varchar](50) NULL,
	[TotalPhysicalMemoryKb] [bigint] NULL,
	[AvailablePhysicalMemoryKb] [bigint] NULL,
	[ApplicationVersion] [varchar](250) NULL,
	[OriginatingIP] [varchar](100) NULL,
	[enmLanguage] [varchar](10) NULL,
	[HostingAssembly] [varchar](50) NULL,
	[OriginatingCountry] [varchar](10) NULL,
	[clc_DateLoggedIn]  AS (CONVERT([date],[TimeLoggedIn])) PERSISTED,
	[clc_MonthLoggedIn]  AS (datefromparts(datepart(year,[TimeLoggedIn]),datepart(month,[TimeLoggedIn]),(1))) PERSISTED,
	[ClientReportedIP] [varchar](100) NULL,
	[ClientReportedCountry] [varchar](10) NULL,
	[IPAdditionalDetails] [varchar](250) NULL,
 CONSTRAINT [PK_c_LoggedLogin] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[ccvwComboList_c_LoggedLogin]    Script Date: 16/07/2026 22:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ccvwComboList_c_LoggedLogin] WITH SCHEMABINDING 
AS 
    SELECT  
      [c_LoggedLogin].[ID] AS [ID],  
      RIGHT(space(18) + CAST([c_LoggedLogin].[ID] as varchar(19)),18) + ' ' AS [TextNS], 
      CAST([c_LoggedLogin].[ID] as varchar(19)) AS [Text] 
    FROM [dbo].[c_LoggedLogin] 
    
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [PK_ccvwComboList_c_LoggedLogin]    Script Date: 16/07/2026 22:47:43 ******/
CREATE UNIQUE CLUSTERED INDEX [PK_ccvwComboList_c_LoggedLogin] ON [dbo].[ccvwComboList_c_LoggedLogin]
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[c_Lookup]    Script Date: 16/07/2026 22:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[c_Lookup](
	[TableName_c_Lookup] [bit] NOT NULL,
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[enmParentLookupType_Lookup] [varchar](50) NULL,
	[ParentCode] [varchar](50) NULL,
	[enmLookupType_Lookup] [varchar](50) NULL,
	[Code] [varchar](50) NULL,
	[locText] [nvarchar](100) NULL,
	[AddedBy] [nvarchar](50) NULL,
	[AddedOn] [datetime] NOT NULL,
	[ChangedBy] [nvarchar](50) NULL,
	[ChangedOn] [datetime] NULL,
	[DeletedBy] [nvarchar](50) NULL,
	[DeletedOn] [datetime] NULL,
	[UpdatingLoginID] [bigint] NULL,
	[locDescription] [nvarchar](50) NULL,
 CONSTRAINT [PK_c_Lookup] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[ccvwComboList_c_Lookup]    Script Date: 16/07/2026 22:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ccvwComboList_c_Lookup] WITH SCHEMABINDING 
AS 
    SELECT  
      [c_Lookup].[ID] AS [ID],  
      ' ' + CAST(REPLACE(COALESCE([c_Lookup].[enmLookupType_Lookup], '') 
      + ' --> ' + COALESCE([c_Lookup].[Code], '') 
      + ' (' + COALESCE([c_Lookup].[locText], '') 
      + ')', ' ','') AS nvarchar(200)) + ' ' AS [TextNS], 
      COALESCE([c_Lookup].[enmLookupType_Lookup], '') 
      + ' --> ' + COALESCE([c_Lookup].[Code], '') 
      + ' (' + COALESCE([c_Lookup].[locText], '') 
      + ')' AS [Text] 
    FROM [dbo].[c_Lookup] 
    
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [PK_ccvwComboList_c_Lookup]    Script Date: 16/07/2026 22:47:43 ******/
CREATE UNIQUE CLUSTERED INDEX [PK_ccvwComboList_c_Lookup] ON [dbo].[ccvwComboList_c_Lookup]
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  View [dbo].[ccvwComboList_c_MFA]    Script Date: 16/07/2026 22:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ccvwComboList_c_MFA] WITH SCHEMABINDING 
AS 
    SELECT  
      [c_MFA].[ID] AS [ID],  
      ' ' + CAST(REPLACE(COALESCE([c_MFA].[CellOrEmail], '') 
     , ' ','') AS nvarchar(200)) + ' ' AS [TextNS], 
      COALESCE([c_MFA].[CellOrEmail], '') 
      AS [Text] 
    FROM [dbo].[c_MFA] 
    
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [PK_ccvwComboList_c_MFA]    Script Date: 16/07/2026 22:47:43 ******/
CREATE UNIQUE CLUSTERED INDEX [PK_ccvwComboList_c_MFA] ON [dbo].[ccvwComboList_c_MFA]
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[c_ObjectToTranslate]    Script Date: 16/07/2026 22:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[c_ObjectToTranslate](
	[TableName_c_ObjectToTranslate] [bit] NULL,
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[enmObjectType] [varchar](50) NOT NULL,
	[Object] [varchar](50) NOT NULL,
	[Item] [nvarchar](255) NULL,
	[AddedBy] [nvarchar](50) NOT NULL,
	[AddedOn] [datetime] NOT NULL,
	[ChangedBy] [nvarchar](50) NULL,
	[ChangedOn] [datetime] NULL,
	[DeletedBy] [nvarchar](50) NULL,
	[DeletedOn] [datetime] NULL,
	[UpdatingLoginID] [bigint] NULL,
 CONSTRAINT [PK_c_ObjectToTranslate] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[ccvwComboList_c_ObjectToTranslate]    Script Date: 16/07/2026 22:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ccvwComboList_c_ObjectToTranslate] WITH SCHEMABINDING 
AS 
    SELECT  
      [c_ObjectToTranslate].[ID] AS [ID],  
      ' ' + CAST(REPLACE(COALESCE([c_ObjectToTranslate].[enmObjectType], '') 
      + ': ' + COALESCE([c_ObjectToTranslate].[Object], '') 
      + ': ' + COALESCE([c_ObjectToTranslate].[Item], '') 
     , ' ','') AS nvarchar(200)) + ' ' AS [TextNS], 
      COALESCE([c_ObjectToTranslate].[enmObjectType], '') 
      + ': ' + COALESCE([c_ObjectToTranslate].[Object], '') 
      + ': ' + COALESCE([c_ObjectToTranslate].[Item], '') 
      AS [Text] 
    FROM [dbo].[c_ObjectToTranslate] 
    
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [PK_ccvwComboList_c_ObjectToTranslate]    Script Date: 16/07/2026 22:47:43 ******/
CREATE UNIQUE CLUSTERED INDEX [PK_ccvwComboList_c_ObjectToTranslate] ON [dbo].[ccvwComboList_c_ObjectToTranslate]
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[c_Process]    Script Date: 16/07/2026 22:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[c_Process](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NULL,
	[DateChecked] [datetime] NULL,
 CONSTRAINT [PK_c_Process] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[ccvwComboList_c_Process]    Script Date: 16/07/2026 22:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ccvwComboList_c_Process] WITH SCHEMABINDING 
AS 
    SELECT  
      [c_Process].[ID] AS [ID],  
      ' ' + CAST(REPLACE(COALESCE([c_Process].[Name], '') 
     , ' ','') AS nvarchar(200)) + ' ' AS [TextNS], 
      COALESCE([c_Process].[Name], '') 
      AS [Text] 
    FROM [dbo].[c_Process] 
    
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [PK_ccvwComboList_c_Process]    Script Date: 16/07/2026 22:47:43 ******/
CREATE UNIQUE CLUSTERED INDEX [PK_ccvwComboList_c_Process] ON [dbo].[ccvwComboList_c_Process]
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[c_Role]    Script Date: 16/07/2026 22:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[c_Role](
	[TableName_c_Role] [bit] NOT NULL,
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[AddedBy] [nvarchar](50) NULL,
	[AddedOn] [datetime] NOT NULL,
	[ChangedBy] [nvarchar](50) NULL,
	[ChangedOn] [datetime] NULL,
	[DeletedBy] [nvarchar](50) NULL,
	[DeletedOn] [datetime] NULL,
	[UpdatingLoginID] [bigint] NULL,
	[BaseRoleID] [bigint] NULL,
 CONSTRAINT [PK_c_Role] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[ccvwComboList_c_Role]    Script Date: 16/07/2026 22:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ccvwComboList_c_Role] WITH SCHEMABINDING 
AS 
    SELECT  
      [c_Role].[ID] AS [ID],  
      ' ' + CAST(REPLACE(COALESCE([c_Role].[Name], '') 
     , ' ','') AS nvarchar(200)) + ' ' AS [TextNS], 
      COALESCE([c_Role].[Name], '') 
      AS [Text] 
    FROM [dbo].[c_Role] 
    
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [PK_ccvwComboList_c_Role]    Script Date: 16/07/2026 22:47:43 ******/
CREATE UNIQUE CLUSTERED INDEX [PK_ccvwComboList_c_Role] ON [dbo].[ccvwComboList_c_Role]
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[c_SystemDefault]    Script Date: 16/07/2026 22:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[c_SystemDefault](
	[TableName_c_SystemDefault] [bit] NOT NULL,
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Group] [varchar](50) NULL,
	[SettingName] [varchar](50) NULL,
	[spt_SettingValue] [nvarchar](4000) NULL,
	[enmSystemDefaultType] [varchar](50) NULL,
	[AddedBy] [nvarchar](50) NULL,
	[AddedOn] [datetime] NOT NULL,
	[ChangedBy] [nvarchar](50) NULL,
	[ChangedOn] [datetime] NULL,
	[UpdatingLoginID] [bigint] NULL,
	[DeletedBy] [nvarchar](50) NULL,
	[DeletedOn] [datetime] NULL,
	[Description] [nvarchar](500) NULL,
 CONSTRAINT [PK_c_SystemDefault] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[ccvwComboList_c_SystemDefault]    Script Date: 16/07/2026 22:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ccvwComboList_c_SystemDefault] WITH SCHEMABINDING 
AS 
    SELECT  
      [c_SystemDefault].[ID] AS [ID],  
      ' ' + CAST(REPLACE(COALESCE([c_SystemDefault].[Group], '') 
      + '_' + COALESCE([c_SystemDefault].[SettingName], '') 
     , ' ','') AS nvarchar(200)) + ' ' AS [TextNS], 
      COALESCE([c_SystemDefault].[Group], '') 
      + '_' + COALESCE([c_SystemDefault].[SettingName], '') 
      AS [Text] 
    FROM [dbo].[c_SystemDefault] 
    
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [PK_ccvwComboList_c_SystemDefault]    Script Date: 16/07/2026 22:47:43 ******/
CREATE UNIQUE CLUSTERED INDEX [PK_ccvwComboList_c_SystemDefault] ON [dbo].[ccvwComboList_c_SystemDefault]
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[c_Table]    Script Date: 16/07/2026 22:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[c_Table](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[DefaultTextFields] [varchar](100) NULL,
	[UsedForIdentity] [bit] NULL,
	[IsSingleRow] [bit] NULL,
	[CanAdd] [varchar](1) NULL,
	[CanEdit] [varchar](1) NULL,
	[CanDelete] [varchar](1) NULL,
	[AuditAdd] [bit] NULL,
	[AuditEdit] [bit] NULL,
	[AuditDelete] [bit] NULL,
	[TrackRowChangers] [bit] NULL,
	[CreateUIMenu] [bit] NULL,
	[CreateUICollection] [bit] NULL,
	[CreateUIEntity] [bit] NULL,
	[SortOrder] [int] NULL,
 CONSTRAINT [PK_c_Table] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[ccvwComboList_c_Table]    Script Date: 16/07/2026 22:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ccvwComboList_c_Table] WITH SCHEMABINDING 
AS 
    SELECT  
      [c_Table].[ID] AS [ID],  
      ' ' + CAST(REPLACE(COALESCE([c_Table].[Name], '') 
     , ' ','') AS nvarchar(200)) + ' ' AS [TextNS], 
      COALESCE([c_Table].[Name], '') 
      AS [Text] 
    FROM [dbo].[c_Table] 
    
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [PK_ccvwComboList_c_Table]    Script Date: 16/07/2026 22:47:43 ******/
CREATE UNIQUE CLUSTERED INDEX [PK_ccvwComboList_c_Table] ON [dbo].[ccvwComboList_c_Table]
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[c_User]    Script Date: 16/07/2026 22:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[c_User](
	[TableName_c_User] [bit] NOT NULL,
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[FirstName] [nvarchar](50) NULL,
	[clc_FullName]  AS (([LastName]+' ')+[FirstName]),
	[NationalIDNo] [nvarchar](50) NULL,
	[Address] [nvarchar](250) NULL,
	[City] [nvarchar](50) NULL,
	[ProvinceState] [nvarchar](50) NULL,
	[PostalCode] [nvarchar](50) NULL,
	[Country] [nvarchar](50) NULL,
	[PhoneNumber] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[enoPassword] [varchar](64) NULL,
	[DatePasswordChanged] [datetime] NULL,
	[enmType_UserIdentityType] [varchar](50) NULL,
	[IDinType] [bigint] NULL,
	[RequiresComputerIdentification] [bit] NULL,
	[EnableSimultaneousLogins] [bit] NULL,
	[clc_DateActivated]  AS ([AddedOn]),
	[IsDisabled] [bit] NULL,
	[ExpiryDate] [datetime] NULL,
	[Comments] [nvarchar](250) NULL,
	[LastPasswords] [varchar](350) NULL,
	[AddedBy] [nvarchar](50) NULL,
	[AddedOn] [datetime] NOT NULL,
	[ChangedBy] [nvarchar](50) NULL,
	[ChangedOn] [datetime] NULL,
	[DeletedBy] [nvarchar](50) NULL,
	[DeletedOn] [datetime] NULL,
	[UpdatingLoginID] [bigint] NULL,
	[spl_Applications] [varchar](1000) NULL,
	[enmLanguage] [varchar](2) NOT NULL,
	[IsLockedOut] [bit] NULL,
	[RoleID] [bigint] NULL,
	[enmAuthenticationMethod] [varchar](50) NOT NULL,
	[RequiresFixedIP] [bit] NULL,
	[enmMessagingMode] [varchar](50) NULL,
	[spt_LoggedInIP] [varchar](100) NULL,
	[enoApprovalCode] [varchar](64) NULL,
	[ApprovalFunctionName] [nvarchar](100) NULL,
	[ApprovalTime] [datetimeoffset](7) NULL,
	[spt_LastSuccessfulLogin] [datetimeoffset](7) NULL,
	[PasswordNeverExpires] [bit] NULL,
	[lkpSecurityQuestion1_SecurityQuestion] [varchar](50) NULL,
	[entSecurityQuestion1Response] [varchar](max) NULL,
	[lkpSecurityQuestion2_SecurityQuestion] [varchar](50) NULL,
	[entSecurityQuestion2Response] [varchar](max) NULL,
	[lkpSecurityQuestion3_SecurityQuestion] [varchar](50) NULL,
	[entSecurityQuestion3Response] [varchar](max) NULL,
	[entPIN] [varchar](max) NULL,
 CONSTRAINT [PK_c_User] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[ccvwComboList_c_User]    Script Date: 16/07/2026 22:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ccvwComboList_c_User] WITH SCHEMABINDING 
AS 
    SELECT  
      [c_User].[ID] AS [ID],  
      ' ' + CAST(REPLACE(COALESCE([c_User].[FirstName], '') 
      + ' ' + COALESCE([c_User].[LastName], '') 
      + ' (' + COALESCE([c_User].[UserName], '') 
      + ')', ' ','') AS nvarchar(200)) + ' ' AS [TextNS], 
      COALESCE([c_User].[FirstName], '') 
      + ' ' + COALESCE([c_User].[LastName], '') 
      + ' (' + COALESCE([c_User].[UserName], '') 
      + ')' AS [Text] 
    FROM [dbo].[c_User] 
    
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [PK_ccvwComboList_c_User]    Script Date: 16/07/2026 22:47:43 ******/
CREATE UNIQUE CLUSTERED INDEX [PK_ccvwComboList_c_User] ON [dbo].[ccvwComboList_c_User]
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vwManagementReport]    Script Date: 16/07/2026 22:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vwManagementReport]
AS
    SELECT
        oh.OrderNumber,
        oh.OrderDate,
        oh.clc_OrderMonth,
        oh.clc_Quarter,
        YEAR(oh.OrderDate)          AS OrderYear,
        c.CustomerName,
        c.enmCustomerType,
        oh.enmDeliveryMethod,
        oh.enmPaymentMethod,
        oh.enmPaymentStatus,
        ol.ProductID,
        p.ProductCode,
        p.ProductName,
        p.enmCategory,
        ol.Quantity,
        ol.UnitPrice,
        ol.clc_LineTotal,
        ol.blg_UnitCost,
        ol.clc_TotalCost,
        ol.clc_Profit,
        oh.clc_TotalAmount,
        oh.clc_VATAmount,
        oh.clc_TotalWithVAT
    FROM dbo.OrderHeader oh
    INNER JOIN dbo.Customer c ON c.ID = oh.CustomerID
    INNER JOIN dbo.OrderLine ol ON ol.OrderHeaderID = oh.ID AND ol.DeletedOn IS NULL
    INNER JOIN dbo.Product p ON p.ID = ol.ProductID
    WHERE oh.DeletedOn IS NULL

GO
/****** Object:  View [dbo].[vwManagementReportBiobee]    Script Date: 16/07/2026 22:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vwManagementReportBiobee]
AS
    SELECT * FROM dbo.vwManagementReport
    WHERE enmDeliveryMethod = 'Biobee'

GO
/****** Object:  View [dbo].[vwProductReport]    Script Date: 16/07/2026 22:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vwProductReport]
AS
    SELECT
        p.ProductCode,
        p.ProductName,
        p.enmCategory,
        SUM(ol.Quantity)            AS TotalQuantitySold,
        SUM(ol.clc_LineTotal)       AS TotalRevenue,
        SUM(ol.clc_TotalCost)      AS TotalCost,
        SUM(ol.clc_Profit)         AS TotalProfit,
        COUNT(DISTINCT oh.ID)       AS OrderCount,
        COUNT(DISTINCT oh.CustomerID) AS CustomerCount
    FROM dbo.Product p
    LEFT JOIN dbo.OrderLine ol ON ol.ProductID = p.ID AND ol.DeletedOn IS NULL
    LEFT JOIN dbo.OrderHeader oh ON oh.ID = ol.OrderHeaderID AND oh.DeletedOn IS NULL
    WHERE p.DeletedOn IS NULL
    GROUP BY p.ProductCode, p.ProductName, p.enmCategory

GO
/****** Object:  View [dbo].[vwCustomerDebtSummary]    Script Date: 16/07/2026 22:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vwCustomerDebtSummary]
AS
    SELECT
        c.ID AS CustomerID,
        c.CustomerCode,
        c.CustomerName,
        c.enmCustomerType,
        c.Phone,
        COUNT(cd.ID)                    AS DebtCount,
        SUM(cd.DebtAmount)              AS TotalDebtAmount,
        SUM(cd.PaidAmount)              AS TotalPaid,
        SUM(cd.clc_RemainingAmount)     AS TotalRemaining,
        MAX(cd.DebtDate)                AS LastDebtDate
    FROM dbo.Customer c
    INNER JOIN dbo.CustomerDebt cd ON cd.CustomerID = c.ID AND cd.DeletedOn IS NULL
    WHERE c.DeletedOn IS NULL
      AND cd.clc_RemainingAmount > 0
    GROUP BY c.ID, c.CustomerCode, c.CustomerName, c.enmCustomerType, c.Phone

GO
/****** Object:  View [dbo].[vwBeehiveDeliverySummary]    Script Date: 16/07/2026 22:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vwBeehiveDeliverySummary]
AS
    SELECT
        DATEPART(ISO_WEEK, oh.DeliveryDate) AS WeekNumber,
        YEAR(oh.DeliveryDate)               AS DeliveryYear,
        oh.enmDeliveryMethod,
        SUM(ol.Quantity)                    AS TotalBeehives
    FROM dbo.OrderHeader oh
    INNER JOIN dbo.OrderLine ol ON ol.OrderHeaderID = oh.ID AND ol.DeletedOn IS NULL
    INNER JOIN dbo.Product p ON p.ID = ol.ProductID
    WHERE oh.DeletedOn IS NULL
      AND p.enmCategory = 'Beehives'
      AND oh.DeliveryDate IS NOT NULL
    GROUP BY DATEPART(ISO_WEEK, oh.DeliveryDate), YEAR(oh.DeliveryDate), oh.enmDeliveryMethod

GO
/****** Object:  View [dbo].[vwProfitability]    Script Date: 16/07/2026 22:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vwProfitability]
AS
    SELECT
        oh.OrderNumber,
        oh.OrderDate,
        c.CustomerCode,
        c.CustomerName,
        c.ProfitabilityCode,
        p.ProductCode,
        p.ProductName,
        p.enmCategory,
        ol.Quantity,
        ol.UnitPrice,
        ol.clc_LineTotal        AS Revenue,
        ol.blg_UnitCost,
        ol.clc_TotalCost        AS Cost,
        ol.clc_Profit           AS Profit,
        CASE
            WHEN ol.clc_LineTotal > 0
            THEN CAST(ol.clc_Profit AS decimal(10,2)) / ol.clc_LineTotal * 100
            ELSE 0
        END                     AS ProfitMarginPercent
    FROM dbo.OrderHeader oh
    INNER JOIN dbo.Customer c ON c.ID = oh.CustomerID
    INNER JOIN dbo.OrderLine ol ON ol.OrderHeaderID = oh.ID AND ol.DeletedOn IS NULL
    INNER JOIN dbo.Product p ON p.ID = ol.ProductID
    WHERE oh.DeletedOn IS NULL

GO
/****** Object:  View [dbo].[vwButanoSlugYellowReport]    Script Date: 16/07/2026 22:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vwButanoSlugYellowReport]
AS
    SELECT * FROM dbo.vwManagementReport
    WHERE enmCategory IN ('Butano', 'Shmoolik')
       OR ProductName LIKE N'%סלאג%'
       OR ProductName LIKE N'%צהוב%'

GO
/****** Object:  View [dbo].[c_IndexFragmentation]    Script Date: 16/07/2026 22:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[c_IndexFragmentation] 
WITH SCHEMABINDING  
AS 
SELECT         
    CONVERT(BigInt, 0) AS ID 
  , CONVERT(varchar(200), '') AS TableName 
  , CONVERT(varchar(255), '') AS IndexName 
  , CONVERT(varchar(50), '') AS IndexType 
  , CONVERT(decimal(18,8), 0) AS  FragmentationPct   
  , CONVERT(int, 0) AS [PageCount] 
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [PK_c_IndexFragmentation]    Script Date: 16/07/2026 22:47:43 ******/
CREATE UNIQUE CLUSTERED INDEX [PK_c_IndexFragmentation] ON [dbo].[c_IndexFragmentation]
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  View [dbo].[c_IndexFragmentationData]    Script Date: 16/07/2026 22:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[c_IndexFragmentationData]  
AS  
 --http://www.databasejournal.com/features/mssql/article.php/3855556/Database-Indexing-Development-LifecycleSay-What.htm  
  
 -- Display Index Fragmentation 
 -- Written by Gregory A. Larsen 
SELECT rank() OVER (ORDER BY OBJECT_NAME(ps.[object_id], DB_ID()), index_id) as ID  
  , OBJECT_NAME(ps.[object_id], DB_ID()) AS [TableName]  
  , si.[name] AS [IndexName]  
  , ps.index_type_desc AS IndexType  
  , CAST(ps.[avg_fragmentation_in_percent] AS decimal(18,8))  As FragmentationPct  
  , CAST(ps.[page_count] AS int) As [PageCount]  
     
FROM sys.dm_db_index_physical_stats(DB_ID(), NULL, NULL, NULL, 'LIMITED') ps  
     JOIN sys.sysindexes si  
     ON  ps.OBJECT_ID = si.id    
     AND ps.index_id = si.indid    
WHERE index_type_desc <> 'HEAP'    
GO
/****** Object:  View [dbo].[c_TableSize]    Script Date: 16/07/2026 22:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[c_TableSize] WITH SCHEMABINDING 
AS 
SELECT         
    CONVERT(BigInt, 0) AS ID 
  , CONVERT(nvarchar(200), N'') AS TableName 
  , CONVERT(int, 0) AS NumberOfRows 
  , CONVERT(int, 0) AS ReservedSizeKb 
  , CONVERT(int, 0) AS DataSizeKb 
  , CONVERT(int, 0) AS IndexSizeKb 
  , CONVERT(int, 0) AS UnusedSizeKb 
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [PK_c_TableSize]    Script Date: 16/07/2026 22:47:43 ******/
CREATE UNIQUE CLUSTERED INDEX [PK_c_TableSize] ON [dbo].[c_TableSize]
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[c_AuditIndexed]    Script Date: 16/07/2026 22:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[c_AuditIndexed](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[OriginalID] [bigint] NULL,
	[TableName] [varchar](50) NULL,
	[RowID] [bigint] NULL,
	[Operation] [varchar](10) NULL,
	[OccurredAt] [datetime] NULL,
	[SqlCurrentUser] [nvarchar](50) NULL,
	[FieldName] [varchar](100) NULL,
	[OldValue] [nvarchar](1000) NULL,
	[NewValue] [nvarchar](1000) NULL,
	[ChangedByUser] [nvarchar](50) NULL,
	[ActiveLoginID] [bigint] NULL,
	[SqlSystemUser] [nvarchar](50) NULL,
	[SqlAppName] [nvarchar](250) NULL,
	[SqlHostName] [varchar](50) NULL,
 CONSTRAINT [PK_c_AuditIndexed] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[c_JobAlertRecipient]    Script Date: 16/07/2026 22:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[c_JobAlertRecipient](
	[TableName_c_JobAlertRecipient] [bit] NULL,
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[c_JobID] [bigint] NULL,
	[c_UserID] [bigint] NULL,
	[enmJobAlertType] [varchar](50) NULL,
	[OverrideName] [nvarchar](50) NULL,
	[OverrideEmailOrPhone] [nvarchar](50) NULL,
	[AddedBy] [nvarchar](50) NOT NULL,
	[AddedOn] [datetime] NOT NULL,
	[ChangedBy] [nvarchar](50) NULL,
	[ChangedOn] [datetime] NULL,
	[DeletedBy] [nvarchar](50) NULL,
	[DeletedOn] [datetime] NULL,
	[UpdatingLoginID] [bigint] NULL,
 CONSTRAINT [PK_c_JobAlertRecipient] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[c_LoggedJob]    Script Date: 16/07/2026 22:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[c_LoggedJob](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[c_JobID] [bigint] NULL,
	[WhenStarted] [datetime] NULL,
	[ActivatingUser] [varchar](50) NULL,
	[LastRunBy] [nvarchar](50) NULL,
	[ExecutionTimeSec] [int] NULL,
	[enmRunStatus_JobStatus] [varchar](50) NULL,
	[Remarks] [nvarchar](max) NULL,
	[c_LoggedAlertID] [bigint] NULL,
	[SuccessCount] [int] NULL,
	[FailureCount] [int] NULL,
	[AddedBy] [nvarchar](50) NOT NULL,
	[AddedOn] [datetime] NOT NULL,
	[ChangedBy] [nvarchar](50) NULL,
	[ChangedOn] [datetime] NULL,
	[DeletedBy] [nvarchar](50) NULL,
	[DeletedOn] [datetime] NULL,
	[UpdatingLoginID] [bigint] NULL,
 CONSTRAINT [PK_c_LoggedJob] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[c_LoggedRequest]    Script Date: 16/07/2026 22:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[c_LoggedRequest](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[c_LoggedLoginID] [bigint] NULL,
	[TimeAccessed] [datetime] NULL,
	[c_UserID] [bigint] NULL,
	[CallingFunctionWithinApplication] [varchar](100) NULL,
	[EntryPoint] [varchar](255) NULL,
	[Process] [varchar](75) NULL,
	[Thread] [varchar](50) NULL,
 CONSTRAINT [PK_c_LoggedRequest] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[c_Mail]    Script Date: 16/07/2026 22:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[c_Mail](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[enmMessagingMode] [varchar](50) NULL,
	[RecipientEmail] [nvarchar](50) NULL,
	[WhenSent] [datetimeoffset](7) NULL,
	[Subject] [nvarchar](50) NULL,
	[Body] [nvarchar](max) NULL,
	[WhenSeen] [datetimeoffset](7) NULL,
	[WasSeen] [bit] NOT NULL,
 CONSTRAINT [PK_c_Mail] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[c_ObjectTranslation]    Script Date: 16/07/2026 22:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[c_ObjectTranslation](
	[TableName_c_ObjectTranslation] [bit] NULL,
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[c_ObjectToTranslateID] [bigint] NOT NULL,
	[Instance] [bigint] NULL,
	[DefaultText] [nvarchar](max) NULL,
	[enmLanguage] [varchar](10) NOT NULL,
	[Text] [nvarchar](max) NOT NULL,
	[AddedBy] [nvarchar](50) NOT NULL,
	[AddedOn] [datetime] NOT NULL,
	[ChangedBy] [nvarchar](50) NULL,
	[ChangedOn] [datetime] NULL,
	[DeletedBy] [nvarchar](50) NULL,
	[DeletedOn] [datetime] NULL,
	[UpdatingLoginID] [bigint] NULL,
	[InstanceUniqueText] [nvarchar](500) NULL,
 CONSTRAINT [PK_c_ObjectTranslation] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[c_Permission]    Script Date: 16/07/2026 22:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[c_Permission](
	[TableName_c_Permission] [bit] NOT NULL,
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[c_ProcessID] [bigint] NULL,
	[c_RoleID] [bigint] NULL,
	[CanDo] [bit] NULL,
	[AddedBy] [nvarchar](50) NULL,
	[AddedOn] [datetime] NOT NULL,
	[ChangedBy] [nvarchar](50) NULL,
	[ChangedOn] [datetime] NULL,
	[DeletedBy] [nvarchar](50) NULL,
	[DeletedOn] [datetime] NULL,
	[UpdatingLoginID] [bigint] NULL,
 CONSTRAINT [PK_c_Permission] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[c_SystemAudit]    Script Date: 16/07/2026 22:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[c_SystemAudit](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[TableName] [varchar](50) NULL,
	[RowId] [bigint] NULL,
	[Operation] [varchar](10) NULL,
	[OccurredAt] [datetime] NULL,
	[SqlCurrentUser] [nvarchar](50) NULL,
	[ChangedByUser] [nvarchar](50) NULL,
	[ActiveLoginID] [bigint] NULL,
	[SqlSystemUser] [nvarchar](50) NULL,
	[SqlAppName] [nvarchar](250) NULL,
	[SqlHostName] [varchar](50) NULL,
	[Changes] [nvarchar](max) NULL,
	[DeletedBy] [nvarchar](50) NULL,
	[DeletedOn] [datetime] NULL,
 CONSTRAINT [PK_c_SystemAudit] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[c_TableFields]    Script Date: 16/07/2026 22:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[c_TableFields](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[GroupName] [nvarchar](50) NULL,
	[ShowInWinF] [bit] NULL,
	[OrdinalOnScreen] [int] NULL,
	[TABLE_NAME] [sysname] NOT NULL,
	[TABLE_TYPE] [varchar](10) NULL,
	[COLUMN_NAME] [sysname] NULL,
	[DATA_TYPE] [nvarchar](128) NULL,
	[CHARACTER_MAXIMUM_LENGTH] [int] NULL,
	[NUMERIC_PRECISION] [tinyint] NULL,
	[COLUMN_DEFAULT] [nvarchar](1000) NULL,
	[IS_NULLABLE] [varchar](3) NULL,
	[ORDINALINDATABASE] [int] NULL,
	[UseForCustomWinFormProject] [bit] NULL,
	[DtoForWebAPI] [varchar](50) NULL,
 CONSTRAINT [PK_c_TableFields] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[c_UserLoginKey]    Script Date: 16/07/2026 22:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[c_UserLoginKey](
	[TableName_c_UserLoginKey] [bit] NOT NULL,
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[c_UserID] [bigint] NULL,
	[ApplicationName] [varchar](50) NULL,
	[ApplicationIdentifier] [varchar](100) NULL,
	[enoKey] [varchar](64) NULL,
	[ExternalIPAtCreation] [varchar](100) NULL,
	[CountryAtCreation] [varchar](2) NULL,
	[LastAccessTime] [datetime] NULL,
	[LoggedLoginID] [bigint] NULL,
	[AddedBy] [nvarchar](50) NULL,
	[AddedOn] [datetime] NOT NULL,
	[ChangedBy] [nvarchar](50) NULL,
	[ChangedOn] [datetime] NULL,
	[DeletedBy] [nvarchar](50) NULL,
	[DeletedOn] [datetime] NULL,
	[UpdatingLoginID] [bigint] NULL,
 CONSTRAINT [PK_c_UserLoginKey] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[c_UserPermission]    Script Date: 16/07/2026 22:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[c_UserPermission](
	[TableName_c_UserPermission] [bit] NOT NULL,
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[c_UserID] [bigint] NULL,
	[ComputerIdentifier] [varchar](100) NULL,
	[ApplicationName] [varchar](50) NULL,
	[ComputerName] [nvarchar](50) NULL,
	[ExternalIP] [varchar](100) NULL,
	[HasPermission] [bit] NULL,
	[Comments] [nvarchar](200) NULL,
	[LastAccessTime] [datetime] NULL,
	[LoggedLoginID] [bigint] NULL,
	[AddedBy] [nvarchar](50) NULL,
	[AddedOn] [datetime] NOT NULL,
	[ChangedBy] [nvarchar](50) NULL,
	[ChangedOn] [datetime] NULL,
	[DeletedBy] [nvarchar](50) NULL,
	[DeletedOn] [datetime] NULL,
	[UpdatingLoginID] [bigint] NULL,
 CONSTRAINT [PK_c_UserPermission] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[c_UserStatus]    Script Date: 16/07/2026 22:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[c_UserStatus](
	[TableName_c_UserStatus] [bit] NOT NULL,
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[c_UserID] [bigint] NULL,
	[ApplicationName] [varchar](50) NULL,
	[LastLoggedLoginID] [bigint] NULL,
	[LoginTime] [datetime] NULL,
	[LogoutTime] [datetime] NULL,
	[AddedBy] [nvarchar](50) NULL,
	[AddedOn] [datetime] NOT NULL,
	[ChangedBy] [nvarchar](50) NULL,
	[ChangedOn] [datetime] NULL,
	[DeletedBy] [nvarchar](50) NULL,
	[DeletedOn] [datetime] NULL,
	[UpdatingLoginID] [bigint] NULL,
 CONSTRAINT [PK_c_UserStatus] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CheckPrg]    Script Date: 16/07/2026 22:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CheckPrg](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProgramName] [varchar](100) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[Amount] [decimal](12, 2) NULL,
	[Description] [nvarchar](250) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CheckPrg1]    Script Date: 16/07/2026 22:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CheckPrg1](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProgramName] [varchar](100) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[Amount] [decimal](12, 2) NULL,
	[Description] [nvarchar](250) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SupplierOrderLine]    Script Date: 16/07/2026 22:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SupplierOrderLine](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[SupplierOrderID] [bigint] NOT NULL,
	[ProductID] [bigint] NOT NULL,
	[Quantity] [int] NOT NULL,
	[UnitCost] [decimal](10, 2) NOT NULL,
	[clc_LineCost]  AS ([Quantity]*[UnitCost]) PERSISTED,
	[DeletedOn] [datetime] NULL,
	[DeletedBy] [nvarchar](200) NULL,
	[AddedBy] [nvarchar](200) NULL,
	[AddedOn] [datetime] NULL,
	[ChangedBy] [nvarchar](200) NULL,
	[ChangedOn] [datetime] NULL,
	[UpdatingLoginID] [bigint] NULL,
 CONSTRAINT [PK_SupplierOrderLine] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SystemSettings]    Script Date: 16/07/2026 22:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemSettings](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[SettingKey] [nvarchar](100) NOT NULL,
	[SettingValue] [nvarchar](500) NULL,
	[SettingType] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](500) NULL,
	[DeletedOn] [datetime] NULL,
	[DeletedBy] [nvarchar](200) NULL,
	[AddedBy] [nvarchar](200) NULL,
	[AddedOn] [datetime] NULL,
	[ChangedBy] [nvarchar](200) NULL,
	[ChangedOn] [datetime] NULL,
	[UpdatingLoginID] [bigint] NULL,
 CONSTRAINT [PK_SystemSettings] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_SystemSettings_Key] UNIQUE NONCLUSTERED 
(
	[SettingKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[zzPreviousRole]    Script Date: 16/07/2026 22:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[zzPreviousRole](
	[ID] [bigint] NOT NULL,
	[UserName] [varchar](50) NULL,
	[OldRole] [nvarchar](4000) NULL,
	[NewRole] [varchar](1) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Index [IX_BeehiveBuyer_CustomerID]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IX_BeehiveBuyer_CustomerID] ON [dbo].[BeehiveBuyerTracking]
(
	[CustomerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_BeehiveBuyer_ReminderMonth]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IX_BeehiveBuyer_ReminderMonth] ON [dbo].[BeehiveBuyerTracking]
(
	[ReminderMonth] ASC
)
WHERE ([DeletedOn] IS NULL AND [blg_IsRelevant]=(1))
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_c_AlertMessage_Description]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IX_c_AlertMessage_Description] ON [dbo].[c_AlertMessage]
(
	[Description] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_c_AlertMessage_Number]    Script Date: 16/07/2026 22:47:43 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_c_AlertMessage_Number] ON [dbo].[c_AlertMessage]
(
	[Number] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_c_AlertMessage_Type_Severity]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IX_c_AlertMessage_Type_Severity] ON [dbo].[c_AlertMessage]
(
	[enmType_FaultType] ASC,
	[enmSeverity_FaultSeverity] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_c_AuditIndexed_ActiveLoginID]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IX_c_AuditIndexed_ActiveLoginID] ON [dbo].[c_AuditIndexed]
(
	[ActiveLoginID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_c_AuditIndexed_ChangedByUser]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IX_c_AuditIndexed_ChangedByUser] ON [dbo].[c_AuditIndexed]
(
	[ChangedByUser] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_c_AuditIndexed_FieldName]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IX_c_AuditIndexed_FieldName] ON [dbo].[c_AuditIndexed]
(
	[FieldName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_c_AuditIndexed_OccurredAt]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IX_c_AuditIndexed_OccurredAt] ON [dbo].[c_AuditIndexed]
(
	[OccurredAt] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_c_AuditIndexed_OriginalID]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IX_c_AuditIndexed_OriginalID] ON [dbo].[c_AuditIndexed]
(
	[OriginalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_c_AuditIndexed_RowID]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IX_c_AuditIndexed_RowID] ON [dbo].[c_AuditIndexed]
(
	[RowID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_c_AuditIndexed_TableName]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IX_c_AuditIndexed_TableName] ON [dbo].[c_AuditIndexed]
(
	[TableName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_c_AuditIndexed_TableName_RowID]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IX_c_AuditIndexed_TableName_RowID] ON [dbo].[c_AuditIndexed]
(
	[TableName] ASC,
	[RowID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_c_Enumeration_EnumType]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IX_c_Enumeration_EnumType] ON [dbo].[c_Enumeration]
(
	[EnumType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_c_Enumeration_EnumTypeAndEnumValue]    Script Date: 16/07/2026 22:47:43 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_c_Enumeration_EnumTypeAndEnumValue] ON [dbo].[c_Enumeration]
(
	[EnumType] ASC,
	[EnumValue] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_c_Job_Active]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IX_c_Job_Active] ON [dbo].[c_Job]
(
	[Active] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_c_Job_Job]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IX_c_Job_Job] ON [dbo].[c_Job]
(
	[lkpJob] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_c_Job_Job_JobRunner]    Script Date: 16/07/2026 22:47:43 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_c_Job_Job_JobRunner] ON [dbo].[c_Job]
(
	[lkpJob] ASC,
	[lkpJobRunner] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_c_Job_JobRunner]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IX_c_Job_JobRunner] ON [dbo].[c_Job]
(
	[lkpJobRunner] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_c_Job_JobRunner_Active]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IX_c_Job_JobRunner_Active] ON [dbo].[c_Job]
(
	[lkpJobRunner] ASC,
	[Active] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_c_JobAlertRecipient_c_JobID]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IX_c_JobAlertRecipient_c_JobID] ON [dbo].[c_JobAlertRecipient]
(
	[c_JobID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_c_JobAlertRecipient_c_UserID]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IX_c_JobAlertRecipient_c_UserID] ON [dbo].[c_JobAlertRecipient]
(
	[c_UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_c_JobAlertRecipient_JobAlertType]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IX_c_JobAlertRecipient_JobAlertType] ON [dbo].[c_JobAlertRecipient]
(
	[enmJobAlertType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_c_Language_Code]    Script Date: 16/07/2026 22:47:43 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_c_Language_Code] ON [dbo].[c_Language]
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_c_LoggedAlert_CallingApplication]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IX_c_LoggedAlert_CallingApplication] ON [dbo].[c_LoggedAlert]
(
	[CallingApplication] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [IX_c_LoggedAlert_DateOccurred]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IX_c_LoggedAlert_DateOccurred] ON [dbo].[c_LoggedAlert]
(
	[clc_DateOccurred] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_c_LoggedAlert_FaultNumber]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IX_c_LoggedAlert_FaultNumber] ON [dbo].[c_LoggedAlert]
(
	[FaultNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_c_LoggedAlert_FaultSeverity]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IX_c_LoggedAlert_FaultSeverity] ON [dbo].[c_LoggedAlert]
(
	[enmFaultSeverity_FaultSeverity] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_c_LoggedAlert_FaultType]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IX_c_LoggedAlert_FaultType] ON [dbo].[c_LoggedAlert]
(
	[enmFaultType_FaultType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_c_LoggedAlert_FaultTypeAndFaultSeverity]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IX_c_LoggedAlert_FaultTypeAndFaultSeverity] ON [dbo].[c_LoggedAlert]
(
	[enmFaultType_FaultType] ASC,
	[enmFaultSeverity_FaultSeverity] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_c_LoggedAlert_LoggedLoginID]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IX_c_LoggedAlert_LoggedLoginID] ON [dbo].[c_LoggedAlert]
(
	[c_LoggedLoginID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [IX_c_LoggedAlert_MonthOccurred]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IX_c_LoggedAlert_MonthOccurred] ON [dbo].[c_LoggedAlert]
(
	[clc_MonthOccurred] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_c_LoggedAlert_SystemName]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IX_c_LoggedAlert_SystemName] ON [dbo].[c_LoggedAlert]
(
	[SystemName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_c_LoggedAlert_TimeOccurred]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IX_c_LoggedAlert_TimeOccurred] ON [dbo].[c_LoggedAlert]
(
	[TimeOccurred] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_c_LoggedAlert_TimeOccurredAndFaultTypeAndFaultSeverity]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IX_c_LoggedAlert_TimeOccurredAndFaultTypeAndFaultSeverity] ON [dbo].[c_LoggedAlert]
(
	[TimeOccurred] ASC,
	[enmFaultType_FaultType] ASC,
	[enmFaultSeverity_FaultSeverity] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IXc_LoggedAlert_AffectedUserID]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IXc_LoggedAlert_AffectedUserID] ON [dbo].[c_LoggedAlert]
(
	[AffectedUserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_c_LoggedJob_JobID]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IX_c_LoggedJob_JobID] ON [dbo].[c_LoggedJob]
(
	[c_JobID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_c_LoggedJob_LoggedAlertID]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IX_c_LoggedJob_LoggedAlertID] ON [dbo].[c_LoggedJob]
(
	[c_LoggedAlertID] ASC
)
WHERE ([c_LoggedAlertID] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_c_LoggedLogin_ApplicationName]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IX_c_LoggedLogin_ApplicationName] ON [dbo].[c_LoggedLogin]
(
	[ApplicationName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [IX_c_LoggedLogin_DateLoggedIn]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IX_c_LoggedLogin_DateLoggedIn] ON [dbo].[c_LoggedLogin]
(
	[clc_DateLoggedIn] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_c_LoggedLogin_LoginFaultNumber]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IX_c_LoggedLogin_LoginFaultNumber] ON [dbo].[c_LoggedLogin]
(
	[LoginFaultNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [IX_c_LoggedLogin_MonthLoggedIn]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IX_c_LoggedLogin_MonthLoggedIn] ON [dbo].[c_LoggedLogin]
(
	[clc_MonthLoggedIn] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_c_LoggedLogin_OriginatingCountry]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IX_c_LoggedLogin_OriginatingCountry] ON [dbo].[c_LoggedLogin]
(
	[OriginatingCountry] ASC
)
WHERE ([OriginatingCountry] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_c_LoggedLogin_TimeLoggedIn]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IX_c_LoggedLogin_TimeLoggedIn] ON [dbo].[c_LoggedLogin]
(
	[TimeLoggedIn] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_c_LoggedLogin_UserName]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IX_c_LoggedLogin_UserName] ON [dbo].[c_LoggedLogin]
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_c_LoggedLogin_UserNameAndApplicationName]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IX_c_LoggedLogin_UserNameAndApplicationName] ON [dbo].[c_LoggedLogin]
(
	[UserName] ASC,
	[ApplicationName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_c_LoggedRequest_LoggedLoginID]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IX_c_LoggedRequest_LoggedLoginID] ON [dbo].[c_LoggedRequest]
(
	[c_LoggedLoginID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_c_LoggedRequest_UserID]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IX_c_LoggedRequest_UserID] ON [dbo].[c_LoggedRequest]
(
	[c_UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_c_Lookup_LookupType]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IX_c_Lookup_LookupType] ON [dbo].[c_Lookup]
(
	[enmLookupType_Lookup] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_c_Lookup_ParentLookupType_LookupType]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IX_c_Lookup_ParentLookupType_LookupType] ON [dbo].[c_Lookup]
(
	[enmParentLookupType_Lookup] ASC,
	[enmLookupType_Lookup] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_c_Lookup_ParentLookupType_ParentCode_LookupType]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IX_c_Lookup_ParentLookupType_ParentCode_LookupType] ON [dbo].[c_Lookup]
(
	[enmParentLookupType_Lookup] ASC,
	[ParentCode] ASC,
	[enmLookupType_Lookup] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_c_Lookup_ParentLookupType_ParentCode_LookupType_Code]    Script Date: 16/07/2026 22:47:43 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_c_Lookup_ParentLookupType_ParentCode_LookupType_Code] ON [dbo].[c_Lookup]
(
	[enmParentLookupType_Lookup] ASC,
	[ParentCode] ASC,
	[enmLookupType_Lookup] ASC,
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IXc_Mail_MailType_RecipientEmail]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IXc_Mail_MailType_RecipientEmail] ON [dbo].[c_Mail]
(
	[enmMessagingMode] ASC,
	[RecipientEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IXc_Mail_MailType_RecipientEmail_WasSeen]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IXc_Mail_MailType_RecipientEmail_WasSeen] ON [dbo].[c_Mail]
(
	[enmMessagingMode] ASC,
	[RecipientEmail] ASC,
	[WasSeen] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IXc_Mail_WasSeen]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IXc_Mail_WasSeen] ON [dbo].[c_Mail]
(
	[WasSeen] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_c_MFA_UserID]    Script Date: 16/07/2026 22:47:43 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_c_MFA_UserID] ON [dbo].[c_MFA]
(
	[c_UserID] ASC
)
WHERE ([c_UserID] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_c_MFA_UserID_CellOrEmail]    Script Date: 16/07/2026 22:47:43 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_c_MFA_UserID_CellOrEmail] ON [dbo].[c_MFA]
(
	[c_UserID] ASC,
	[CellOrEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_c_ObjectToTranslate_enmObjectType]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IX_c_ObjectToTranslate_enmObjectType] ON [dbo].[c_ObjectToTranslate]
(
	[enmObjectType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_c_ObjectToTranslate_enmObjectType_Object]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IX_c_ObjectToTranslate_enmObjectType_Object] ON [dbo].[c_ObjectToTranslate]
(
	[enmObjectType] ASC,
	[Object] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_c_ObjectToTranslate_enmObjectType_Object_Item]    Script Date: 16/07/2026 22:47:43 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_c_ObjectToTranslate_enmObjectType_Object_Item] ON [dbo].[c_ObjectToTranslate]
(
	[enmObjectType] ASC,
	[Object] ASC,
	[Item] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_c_ObjectTranslation_c_ObjectToTranslateID]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IX_c_ObjectTranslation_c_ObjectToTranslateID] ON [dbo].[c_ObjectTranslation]
(
	[c_ObjectToTranslateID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_c_ObjectTranslation_c_ObjectToTranslateID_Instance]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IX_c_ObjectTranslation_c_ObjectToTranslateID_Instance] ON [dbo].[c_ObjectTranslation]
(
	[c_ObjectToTranslateID] ASC,
	[Instance] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_c_ObjectTranslation_c_ObjectToTranslateID_Instance_enmLanguage]    Script Date: 16/07/2026 22:47:43 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_c_ObjectTranslation_c_ObjectToTranslateID_Instance_enmLanguage] ON [dbo].[c_ObjectTranslation]
(
	[c_ObjectToTranslateID] ASC,
	[Instance] ASC,
	[enmLanguage] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_c_ObjectTranslation_enmLanguage]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IX_c_ObjectTranslation_enmLanguage] ON [dbo].[c_ObjectTranslation]
(
	[enmLanguage] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_c_ObjectTranslation_InstanceUniqueText_Instance_Language]    Script Date: 16/07/2026 22:47:43 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_c_ObjectTranslation_InstanceUniqueText_Instance_Language] ON [dbo].[c_ObjectTranslation]
(
	[InstanceUniqueText] ASC,
	[Instance] ASC,
	[enmLanguage] ASC
)
WHERE ([InstanceUniqueText]<>'' AND [InstanceUniqueText] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_c_Permission_ProcessID]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IX_c_Permission_ProcessID] ON [dbo].[c_Permission]
(
	[c_ProcessID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_c_Permission_ProcessIDAndRoleID]    Script Date: 16/07/2026 22:47:43 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_c_Permission_ProcessIDAndRoleID] ON [dbo].[c_Permission]
(
	[c_ProcessID] ASC,
	[c_RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_c_Permission_RoleID]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IX_c_Permission_RoleID] ON [dbo].[c_Permission]
(
	[c_RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_c_Process_Name]    Script Date: 16/07/2026 22:47:43 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_c_Process_Name] ON [dbo].[c_Process]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_c_Role_Name]    Script Date: 16/07/2026 22:47:43 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_c_Role_Name] ON [dbo].[c_Role]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IXc_Role_BaseRoleID]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IXc_Role_BaseRoleID] ON [dbo].[c_Role]
(
	[BaseRoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_c_SystemDefault_Group_SettingName]    Script Date: 16/07/2026 22:47:43 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_c_SystemDefault_Group_SettingName] ON [dbo].[c_SystemDefault]
(
	[Group] ASC,
	[SettingName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IXc_SystemDefault_Group]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IXc_SystemDefault_Group] ON [dbo].[c_SystemDefault]
(
	[Group] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_c_Table_Name]    Script Date: 16/07/2026 22:47:43 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_c_Table_Name] ON [dbo].[c_Table]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_cTableFields_TABLENAME_COLUMNNAME]    Script Date: 16/07/2026 22:47:43 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_cTableFields_TABLENAME_COLUMNNAME] ON [dbo].[c_TableFields]
(
	[TABLE_NAME] ASC,
	[COLUMN_NAME] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_c_User_AddressCity]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IX_c_User_AddressCity] ON [dbo].[c_User]
(
	[City] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_c_User_LastNameAndFirstName]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IX_c_User_LastNameAndFirstName] ON [dbo].[c_User]
(
	[LastName] ASC,
	[FirstName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_c_User_LastSuccessfulLogin]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IX_c_User_LastSuccessfulLogin] ON [dbo].[c_User]
(
	[spt_LastSuccessfulLogin] ASC
)
WHERE ([spt_LastSuccessfulLogin] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_c_User_UserName]    Script Date: 16/07/2026 22:47:43 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_c_User_UserName] ON [dbo].[c_User]
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IXc_User_RoleID]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IXc_User_RoleID] ON [dbo].[c_User]
(
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IXc_User_Type_IDinType]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IXc_User_Type_IDinType] ON [dbo].[c_User]
(
	[enmType_UserIdentityType] ASC,
	[IDinType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_c_UserLoginKey_ApplicationName_ApplicationIdentifier]    Script Date: 16/07/2026 22:47:43 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_c_UserLoginKey_ApplicationName_ApplicationIdentifier] ON [dbo].[c_UserLoginKey]
(
	[ApplicationName] ASC,
	[ApplicationIdentifier] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_c_UserLoginKey_UserID]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IX_c_UserLoginKey_UserID] ON [dbo].[c_UserLoginKey]
(
	[c_UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_c_UserPermission_UserID]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IX_c_UserPermission_UserID] ON [dbo].[c_UserPermission]
(
	[c_UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_c_UserPermission_UserID_ApplicationName]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IX_c_UserPermission_UserID_ApplicationName] ON [dbo].[c_UserPermission]
(
	[c_UserID] ASC,
	[ApplicationName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_c_UserStatus_LastLoggedLoginID]    Script Date: 16/07/2026 22:47:43 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_c_UserStatus_LastLoggedLoginID] ON [dbo].[c_UserStatus]
(
	[LastLoggedLoginID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_c_UserStatus_LoginTimeNonUnique]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IX_c_UserStatus_LoginTimeNonUnique] ON [dbo].[c_UserStatus]
(
	[LoginTime] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_c_UserStatus_UserID]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IX_c_UserStatus_UserID] ON [dbo].[c_UserStatus]
(
	[c_UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_c_UserStatus_UserIDAndApplicationName]    Script Date: 16/07/2026 22:47:43 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_c_UserStatus_UserIDAndApplicationName] ON [dbo].[c_UserStatus]
(
	[c_UserID] ASC,
	[ApplicationName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Customer_enmCustomerType]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IX_Customer_enmCustomerType] ON [dbo].[Customer]
(
	[enmCustomerType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Customer_CustomerCode]    Script Date: 16/07/2026 22:47:43 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ_Customer_CustomerCode] ON [dbo].[Customer]
(
	[CustomerCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_CustomerDebt_CustomerID]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IX_CustomerDebt_CustomerID] ON [dbo].[CustomerDebt]
(
	[CustomerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_CustomerDebt_OrderHeaderID]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IX_CustomerDebt_OrderHeaderID] ON [dbo].[CustomerDebt]
(
	[OrderHeaderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Delivery_ArrivalToCustomerDate]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IX_Delivery_ArrivalToCustomerDate] ON [dbo].[Delivery]
(
	[ArrivalToCustomerDate] ASC
)
WHERE ([DeletedOn] IS NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Delivery_OrderHeaderID]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IX_Delivery_OrderHeaderID] ON [dbo].[Delivery]
(
	[OrderHeaderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Delivery_Status]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IX_Delivery_Status] ON [dbo].[Delivery]
(
	[enmDeliveryStatus] ASC
)
WHERE ([DeletedOn] IS NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_OrderHeader_CustomerID]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IX_OrderHeader_CustomerID] ON [dbo].[OrderHeader]
(
	[CustomerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_OrderHeader_enmOrderStatus]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IX_OrderHeader_enmOrderStatus] ON [dbo].[OrderHeader]
(
	[enmOrderStatus] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_OrderHeader_enmPaymentStatus]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IX_OrderHeader_enmPaymentStatus] ON [dbo].[OrderHeader]
(
	[enmPaymentStatus] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_OrderHeader_OrderDate]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IX_OrderHeader_OrderDate] ON [dbo].[OrderHeader]
(
	[OrderDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_OrderLine_OrderHeaderID]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IX_OrderLine_OrderHeaderID] ON [dbo].[OrderLine]
(
	[OrderHeaderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_OrderLine_ProductID]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IX_OrderLine_ProductID] ON [dbo].[OrderLine]
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Product_enmCategory]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IX_Product_enmCategory] ON [dbo].[Product]
(
	[enmCategory] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Product_ProductCode]    Script Date: 16/07/2026 22:47:43 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ_Product_ProductCode] ON [dbo].[Product]
(
	[ProductCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_ProductPrice_Lookup]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IX_ProductPrice_Lookup] ON [dbo].[ProductPrice]
(
	[ProductID] ASC,
	[enmCustomerType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ProductPrice_ProductID]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IX_ProductPrice_ProductID] ON [dbo].[ProductPrice]
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_SupplierOrder_OrderHeaderID]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IX_SupplierOrder_OrderHeaderID] ON [dbo].[SupplierOrder]
(
	[OrderHeaderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_SupplierOrder_SentDate]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IX_SupplierOrder_SentDate] ON [dbo].[SupplierOrder]
(
	[SentDate] ASC
)
WHERE ([DeletedOn] IS NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_SupplierOrderLine_SupplierOrderID]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IX_SupplierOrderLine_SupplierOrderID] ON [dbo].[SupplierOrderLine]
(
	[SupplierOrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [IX_c_IndexFragmentation_TableName]    Script Date: 16/07/2026 22:47:43 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_c_IndexFragmentation_TableName] ON [dbo].[c_IndexFragmentation]
(
	[TableName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [IX_c_TableSize_TableName]    Script Date: 16/07/2026 22:47:43 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_c_TableSize_TableName] ON [dbo].[c_TableSize]
(
	[TableName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [IXccvwComboList_BeehiveBuyerTracking_Text]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IXccvwComboList_BeehiveBuyerTracking_Text] ON [dbo].[ccvwComboList_BeehiveBuyerTracking]
(
	[Text] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [IXccvwComboList_BeehiveBuyerTracking_TextNS]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IXccvwComboList_BeehiveBuyerTracking_TextNS] ON [dbo].[ccvwComboList_BeehiveBuyerTracking]
(
	[TextNS] ASC
)
INCLUDE([Text]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [IXccvwComboList_BeehiveBuyerTrackingForCustomer_ParentID_TextNS]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IXccvwComboList_BeehiveBuyerTrackingForCustomer_ParentID_TextNS] ON [dbo].[ccvwComboList_BeehiveBuyerTrackingForCustomer]
(
	[ParentID] ASC,
	[TextNS] ASC
)
INCLUDE([Text]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [IXccvwComboList_BeehiveBuyerTrackingForCustomer_Text]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IXccvwComboList_BeehiveBuyerTrackingForCustomer_Text] ON [dbo].[ccvwComboList_BeehiveBuyerTrackingForCustomer]
(
	[Text] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [IXccvwComboList_BeehiveBuyerTrackingForCustomer_TextNS]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IXccvwComboList_BeehiveBuyerTrackingForCustomer_TextNS] ON [dbo].[ccvwComboList_BeehiveBuyerTrackingForCustomer]
(
	[TextNS] ASC
)
INCLUDE([Text]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [IXccvwComboList_c_AlertMessage_Text]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IXccvwComboList_c_AlertMessage_Text] ON [dbo].[ccvwComboList_c_AlertMessage]
(
	[Text] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [IXccvwComboList_c_AlertMessage_TextNS]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IXccvwComboList_c_AlertMessage_TextNS] ON [dbo].[ccvwComboList_c_AlertMessage]
(
	[TextNS] ASC
)
INCLUDE([Text]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [IXccvwComboList_c_Enumeration_Text]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IXccvwComboList_c_Enumeration_Text] ON [dbo].[ccvwComboList_c_Enumeration]
(
	[Text] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [IXccvwComboList_c_Enumeration_TextNS]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IXccvwComboList_c_Enumeration_TextNS] ON [dbo].[ccvwComboList_c_Enumeration]
(
	[TextNS] ASC
)
INCLUDE([Text]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [IXccvwComboList_c_Job_Text]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IXccvwComboList_c_Job_Text] ON [dbo].[ccvwComboList_c_Job]
(
	[Text] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [IXccvwComboList_c_Job_TextNS]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IXccvwComboList_c_Job_TextNS] ON [dbo].[ccvwComboList_c_Job]
(
	[TextNS] ASC
)
INCLUDE([Text]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [IXccvwComboList_c_Language_Text]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IXccvwComboList_c_Language_Text] ON [dbo].[ccvwComboList_c_Language]
(
	[Text] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [IXccvwComboList_c_Language_TextNS]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IXccvwComboList_c_Language_TextNS] ON [dbo].[ccvwComboList_c_Language]
(
	[TextNS] ASC
)
INCLUDE([Text]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [IXccvwComboList_c_LoggedAlert_Text]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IXccvwComboList_c_LoggedAlert_Text] ON [dbo].[ccvwComboList_c_LoggedAlert]
(
	[Text] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [IXccvwComboList_c_LoggedAlert_TextNS]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IXccvwComboList_c_LoggedAlert_TextNS] ON [dbo].[ccvwComboList_c_LoggedAlert]
(
	[TextNS] ASC
)
INCLUDE([Text]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [IXccvwComboList_c_LoggedAlertForAffectedUser_ParentID_TextNS]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IXccvwComboList_c_LoggedAlertForAffectedUser_ParentID_TextNS] ON [dbo].[ccvwComboList_c_LoggedAlertForAffectedUser]
(
	[ParentID] ASC,
	[TextNS] ASC
)
INCLUDE([Text]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [IXccvwComboList_c_LoggedAlertForAffectedUser_Text]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IXccvwComboList_c_LoggedAlertForAffectedUser_Text] ON [dbo].[ccvwComboList_c_LoggedAlertForAffectedUser]
(
	[Text] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [IXccvwComboList_c_LoggedAlertForAffectedUser_TextNS]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IXccvwComboList_c_LoggedAlertForAffectedUser_TextNS] ON [dbo].[ccvwComboList_c_LoggedAlertForAffectedUser]
(
	[TextNS] ASC
)
INCLUDE([Text]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [IXccvwComboList_c_LoggedLogin_Text]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IXccvwComboList_c_LoggedLogin_Text] ON [dbo].[ccvwComboList_c_LoggedLogin]
(
	[Text] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [IXccvwComboList_c_LoggedLogin_TextNS]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IXccvwComboList_c_LoggedLogin_TextNS] ON [dbo].[ccvwComboList_c_LoggedLogin]
(
	[TextNS] ASC
)
INCLUDE([Text]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [IXccvwComboList_c_Lookup_Text]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IXccvwComboList_c_Lookup_Text] ON [dbo].[ccvwComboList_c_Lookup]
(
	[Text] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [IXccvwComboList_c_Lookup_TextNS]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IXccvwComboList_c_Lookup_TextNS] ON [dbo].[ccvwComboList_c_Lookup]
(
	[TextNS] ASC
)
INCLUDE([Text]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [IXccvwComboList_c_MFA_Text]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IXccvwComboList_c_MFA_Text] ON [dbo].[ccvwComboList_c_MFA]
(
	[Text] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [IXccvwComboList_c_MFA_TextNS]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IXccvwComboList_c_MFA_TextNS] ON [dbo].[ccvwComboList_c_MFA]
(
	[TextNS] ASC
)
INCLUDE([Text]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [IXccvwComboList_c_MFAForUser_ParentID_TextNS]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IXccvwComboList_c_MFAForUser_ParentID_TextNS] ON [dbo].[ccvwComboList_c_MFAForUser]
(
	[ParentID] ASC,
	[TextNS] ASC
)
INCLUDE([Text]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [IXccvwComboList_c_MFAForUser_Text]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IXccvwComboList_c_MFAForUser_Text] ON [dbo].[ccvwComboList_c_MFAForUser]
(
	[Text] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [IXccvwComboList_c_MFAForUser_TextNS]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IXccvwComboList_c_MFAForUser_TextNS] ON [dbo].[ccvwComboList_c_MFAForUser]
(
	[TextNS] ASC
)
INCLUDE([Text]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [IXccvwComboList_c_ObjectToTranslate_Text]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IXccvwComboList_c_ObjectToTranslate_Text] ON [dbo].[ccvwComboList_c_ObjectToTranslate]
(
	[Text] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [IXccvwComboList_c_ObjectToTranslate_TextNS]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IXccvwComboList_c_ObjectToTranslate_TextNS] ON [dbo].[ccvwComboList_c_ObjectToTranslate]
(
	[TextNS] ASC
)
INCLUDE([Text]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [IXccvwComboList_c_Process_Text]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IXccvwComboList_c_Process_Text] ON [dbo].[ccvwComboList_c_Process]
(
	[Text] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [IXccvwComboList_c_Process_TextNS]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IXccvwComboList_c_Process_TextNS] ON [dbo].[ccvwComboList_c_Process]
(
	[TextNS] ASC
)
INCLUDE([Text]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [IXccvwComboList_c_Role_Text]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IXccvwComboList_c_Role_Text] ON [dbo].[ccvwComboList_c_Role]
(
	[Text] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [IXccvwComboList_c_Role_TextNS]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IXccvwComboList_c_Role_TextNS] ON [dbo].[ccvwComboList_c_Role]
(
	[TextNS] ASC
)
INCLUDE([Text]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [IXccvwComboList_c_SystemDefault_Text]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IXccvwComboList_c_SystemDefault_Text] ON [dbo].[ccvwComboList_c_SystemDefault]
(
	[Text] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [IXccvwComboList_c_SystemDefault_TextNS]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IXccvwComboList_c_SystemDefault_TextNS] ON [dbo].[ccvwComboList_c_SystemDefault]
(
	[TextNS] ASC
)
INCLUDE([Text]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [IXccvwComboList_c_Table_Text]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IXccvwComboList_c_Table_Text] ON [dbo].[ccvwComboList_c_Table]
(
	[Text] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [IXccvwComboList_c_Table_TextNS]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IXccvwComboList_c_Table_TextNS] ON [dbo].[ccvwComboList_c_Table]
(
	[TextNS] ASC
)
INCLUDE([Text]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [IXccvwComboList_c_User_Text]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IXccvwComboList_c_User_Text] ON [dbo].[ccvwComboList_c_User]
(
	[Text] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [IXccvwComboList_c_User_TextNS]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IXccvwComboList_c_User_TextNS] ON [dbo].[ccvwComboList_c_User]
(
	[TextNS] ASC
)
INCLUDE([Text]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [IXccvwComboList_Customer_Text]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IXccvwComboList_Customer_Text] ON [dbo].[ccvwComboList_Customer]
(
	[Text] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [IXccvwComboList_Customer_TextNS]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IXccvwComboList_Customer_TextNS] ON [dbo].[ccvwComboList_Customer]
(
	[TextNS] ASC
)
INCLUDE([Text]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [IXccvwComboList_CustomerDebt_Text]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IXccvwComboList_CustomerDebt_Text] ON [dbo].[ccvwComboList_CustomerDebt]
(
	[Text] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [IXccvwComboList_CustomerDebt_TextNS]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IXccvwComboList_CustomerDebt_TextNS] ON [dbo].[ccvwComboList_CustomerDebt]
(
	[TextNS] ASC
)
INCLUDE([Text]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [IXccvwComboList_CustomerDebtForCustomer_ParentID_TextNS]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IXccvwComboList_CustomerDebtForCustomer_ParentID_TextNS] ON [dbo].[ccvwComboList_CustomerDebtForCustomer]
(
	[ParentID] ASC,
	[TextNS] ASC
)
INCLUDE([Text]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [IXccvwComboList_CustomerDebtForCustomer_Text]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IXccvwComboList_CustomerDebtForCustomer_Text] ON [dbo].[ccvwComboList_CustomerDebtForCustomer]
(
	[Text] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [IXccvwComboList_CustomerDebtForCustomer_TextNS]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IXccvwComboList_CustomerDebtForCustomer_TextNS] ON [dbo].[ccvwComboList_CustomerDebtForCustomer]
(
	[TextNS] ASC
)
INCLUDE([Text]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [IXccvwComboList_Delivery_Text]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IXccvwComboList_Delivery_Text] ON [dbo].[ccvwComboList_Delivery]
(
	[Text] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [IXccvwComboList_Delivery_TextNS]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IXccvwComboList_Delivery_TextNS] ON [dbo].[ccvwComboList_Delivery]
(
	[TextNS] ASC
)
INCLUDE([Text]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [IXccvwComboList_OrderHeader_Text]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IXccvwComboList_OrderHeader_Text] ON [dbo].[ccvwComboList_OrderHeader]
(
	[Text] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [IXccvwComboList_OrderHeader_TextNS]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IXccvwComboList_OrderHeader_TextNS] ON [dbo].[ccvwComboList_OrderHeader]
(
	[TextNS] ASC
)
INCLUDE([Text]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [IXccvwComboList_OrderHeaderForCustomer_ParentID_TextNS]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IXccvwComboList_OrderHeaderForCustomer_ParentID_TextNS] ON [dbo].[ccvwComboList_OrderHeaderForCustomer]
(
	[ParentID] ASC,
	[TextNS] ASC
)
INCLUDE([Text]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [IXccvwComboList_OrderHeaderForCustomer_Text]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IXccvwComboList_OrderHeaderForCustomer_Text] ON [dbo].[ccvwComboList_OrderHeaderForCustomer]
(
	[Text] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [IXccvwComboList_OrderHeaderForCustomer_TextNS]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IXccvwComboList_OrderHeaderForCustomer_TextNS] ON [dbo].[ccvwComboList_OrderHeaderForCustomer]
(
	[TextNS] ASC
)
INCLUDE([Text]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [IXccvwComboList_OrderLine_Text]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IXccvwComboList_OrderLine_Text] ON [dbo].[ccvwComboList_OrderLine]
(
	[Text] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [IXccvwComboList_OrderLine_TextNS]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IXccvwComboList_OrderLine_TextNS] ON [dbo].[ccvwComboList_OrderLine]
(
	[TextNS] ASC
)
INCLUDE([Text]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [IXccvwComboList_Product_Text]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IXccvwComboList_Product_Text] ON [dbo].[ccvwComboList_Product]
(
	[Text] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [IXccvwComboList_Product_TextNS]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IXccvwComboList_Product_TextNS] ON [dbo].[ccvwComboList_Product]
(
	[TextNS] ASC
)
INCLUDE([Text]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [IXccvwComboList_ProductPrice_Text]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IXccvwComboList_ProductPrice_Text] ON [dbo].[ccvwComboList_ProductPrice]
(
	[Text] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [IXccvwComboList_ProductPrice_TextNS]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IXccvwComboList_ProductPrice_TextNS] ON [dbo].[ccvwComboList_ProductPrice]
(
	[TextNS] ASC
)
INCLUDE([Text]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [IXccvwComboList_ProductPriceHist_Text]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IXccvwComboList_ProductPriceHist_Text] ON [dbo].[ccvwComboList_ProductPriceHist]
(
	[Text] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [IXccvwComboList_ProductPriceHist_TextNS]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IXccvwComboList_ProductPriceHist_TextNS] ON [dbo].[ccvwComboList_ProductPriceHist]
(
	[TextNS] ASC
)
INCLUDE([Text]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [IXccvwComboList_SupplierOrder_Text]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IXccvwComboList_SupplierOrder_Text] ON [dbo].[ccvwComboList_SupplierOrder]
(
	[Text] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF
GO
/****** Object:  Index [IXccvwComboList_SupplierOrder_TextNS]    Script Date: 16/07/2026 22:47:43 ******/
CREATE NONCLUSTERED INDEX [IXccvwComboList_SupplierOrder_TextNS] ON [dbo].[ccvwComboList_SupplierOrder]
(
	[TextNS] ASC
)
INCLUDE([Text]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[BeehiveBuyerTracking] ADD  DEFAULT ((1)) FOR [blg_IsRelevant]
GO
ALTER TABLE [dbo].[BeehiveBuyerTracking] ADD  DEFAULT (getdate()) FOR [AddedOn]
GO
ALTER TABLE [dbo].[c_AlertMessage] ADD  CONSTRAINT [DF_TableName_c_AlertMessage]  DEFAULT ((0)) FOR [TableName_c_AlertMessage]
GO
ALTER TABLE [dbo].[c_AlertMessage] ADD  CONSTRAINT [DF_c_AlertMessage_AddedOn]  DEFAULT (getdate()) FOR [AddedOn]
GO
ALTER TABLE [dbo].[c_Enumeration] ADD  CONSTRAINT [DF_c_Enumeration_AddedOn]  DEFAULT (getdate()) FOR [AddedOn]
GO
ALTER TABLE [dbo].[c_Job] ADD  CONSTRAINT [DF_TableName_c_Job]  DEFAULT ((0)) FOR [TableName_c_Job]
GO
ALTER TABLE [dbo].[c_Job] ADD  CONSTRAINT [DF_c_Job_AddedOn]  DEFAULT (getdate()) FOR [AddedOn]
GO
ALTER TABLE [dbo].[c_JobAlertRecipient] ADD  CONSTRAINT [DF_TableName_c_JobAlertRecipient]  DEFAULT ((0)) FOR [TableName_c_JobAlertRecipient]
GO
ALTER TABLE [dbo].[c_JobAlertRecipient] ADD  CONSTRAINT [DF_c_JobAlertRecipient_AddedOn]  DEFAULT (getdate()) FOR [AddedOn]
GO
ALTER TABLE [dbo].[c_LoggedJob] ADD  CONSTRAINT [DF_c_LoggedJob_AddedOn]  DEFAULT (getdate()) FOR [AddedOn]
GO
ALTER TABLE [dbo].[c_Lookup] ADD  CONSTRAINT [DF_TableName_c_Lookup]  DEFAULT ((0)) FOR [TableName_c_Lookup]
GO
ALTER TABLE [dbo].[c_Lookup] ADD  CONSTRAINT [DF_c_Lookup_AddedOn]  DEFAULT (getdate()) FOR [AddedOn]
GO
ALTER TABLE [dbo].[c_Mail] ADD  CONSTRAINT [DF_c_Mail_WasSeen]  DEFAULT ((0)) FOR [WasSeen]
GO
ALTER TABLE [dbo].[c_MFA] ADD  CONSTRAINT [DF_TableName_c_MFA]  DEFAULT ((0)) FOR [TableName_c_MFA]
GO
ALTER TABLE [dbo].[c_MFA] ADD  CONSTRAINT [DF_c_MFA_AddedOn]  DEFAULT (getdate()) FOR [AddedOn]
GO
ALTER TABLE [dbo].[c_ObjectToTranslate] ADD  CONSTRAINT [DF_TableName_c_ObjectToTranslate]  DEFAULT ((0)) FOR [TableName_c_ObjectToTranslate]
GO
ALTER TABLE [dbo].[c_ObjectToTranslate] ADD  CONSTRAINT [DF_c_ObjectToTranslate_AddedOn]  DEFAULT (getdate()) FOR [AddedOn]
GO
ALTER TABLE [dbo].[c_ObjectTranslation] ADD  CONSTRAINT [DF_TableName_c_ObjectTranslation]  DEFAULT ((0)) FOR [TableName_c_ObjectTranslation]
GO
ALTER TABLE [dbo].[c_ObjectTranslation] ADD  CONSTRAINT [DF_c_ObjectTranslation_AddedOn]  DEFAULT (getdate()) FOR [AddedOn]
GO
ALTER TABLE [dbo].[c_Permission] ADD  CONSTRAINT [DF_TableName_c_Permission]  DEFAULT ((0)) FOR [TableName_c_Permission]
GO
ALTER TABLE [dbo].[c_Permission] ADD  CONSTRAINT [DF_c_Permission_AddedOn]  DEFAULT (getdate()) FOR [AddedOn]
GO
ALTER TABLE [dbo].[c_Role] ADD  CONSTRAINT [DF_TableName_c_Role]  DEFAULT ((0)) FOR [TableName_c_Role]
GO
ALTER TABLE [dbo].[c_Role] ADD  CONSTRAINT [DF_c_Role_AddedOn]  DEFAULT (getdate()) FOR [AddedOn]
GO
ALTER TABLE [dbo].[c_SystemDefault] ADD  CONSTRAINT [DF_TableName_c_SystemDefault]  DEFAULT ((0)) FOR [TableName_c_SystemDefault]
GO
ALTER TABLE [dbo].[c_SystemDefault] ADD  CONSTRAINT [DF_c_SystemDefault_AddedOn]  DEFAULT (getdate()) FOR [AddedOn]
GO
ALTER TABLE [dbo].[c_User] ADD  CONSTRAINT [DF_TableName_c_User]  DEFAULT ((0)) FOR [TableName_c_User]
GO
ALTER TABLE [dbo].[c_User] ADD  CONSTRAINT [DF_c_User_AddedOn]  DEFAULT (getdate()) FOR [AddedOn]
GO
ALTER TABLE [dbo].[c_User] ADD  CONSTRAINT [DF_c_User_enmLanguage]  DEFAULT ('en') FOR [enmLanguage]
GO
ALTER TABLE [dbo].[c_User] ADD  CONSTRAINT [DF_c_User_enmAuthenticationMethod]  DEFAULT ('UD') FOR [enmAuthenticationMethod]
GO
ALTER TABLE [dbo].[c_UserLoginKey] ADD  CONSTRAINT [DF_TableName_c_UserLoginKey]  DEFAULT ((0)) FOR [TableName_c_UserLoginKey]
GO
ALTER TABLE [dbo].[c_UserLoginKey] ADD  CONSTRAINT [DF_c_UserLoginKey_AddedOn]  DEFAULT (getdate()) FOR [AddedOn]
GO
ALTER TABLE [dbo].[c_UserPermission] ADD  CONSTRAINT [DF_TableName_c_UserPermission]  DEFAULT ((0)) FOR [TableName_c_UserPermission]
GO
ALTER TABLE [dbo].[c_UserPermission] ADD  CONSTRAINT [DF_c_UserPermission_AddedOn]  DEFAULT (getdate()) FOR [AddedOn]
GO
ALTER TABLE [dbo].[c_UserStatus] ADD  CONSTRAINT [DF_TableName_c_UserStatus]  DEFAULT ((0)) FOR [TableName_c_UserStatus]
GO
ALTER TABLE [dbo].[c_UserStatus] ADD  CONSTRAINT [DF_c_UserStatus_AddedOn]  DEFAULT (getdate()) FOR [AddedOn]
GO
ALTER TABLE [dbo].[CheckPrg] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[CheckPrg] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[CheckPrg1] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[CheckPrg1] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Customer] ADD  DEFAULT ('Private') FOR [enmCustomerType]
GO
ALTER TABLE [dbo].[Customer] ADD  DEFAULT ((0)) FOR [PaymentTermsDays]
GO
ALTER TABLE [dbo].[Customer] ADD  DEFAULT ((1)) FOR [blg_IsActive]
GO
ALTER TABLE [dbo].[Customer] ADD  CONSTRAINT [DF_Customer_AddedOn]  DEFAULT (getdate()) FOR [AddedOn]
GO
ALTER TABLE [dbo].[CustomerDebt] ADD  DEFAULT ((0)) FOR [PaidAmount]
GO
ALTER TABLE [dbo].[CustomerDebt] ADD  DEFAULT (getdate()) FOR [DebtDate]
GO
ALTER TABLE [dbo].[CustomerDebt] ADD  DEFAULT ('Open') FOR [enmDebtStatus]
GO
ALTER TABLE [dbo].[CustomerDebt] ADD  CONSTRAINT [DF_CustomerDebt_AddedOn]  DEFAULT (getdate()) FOR [AddedOn]
GO
ALTER TABLE [dbo].[CustomerDebt] ADD  DEFAULT ((0)) FOR [blg_NeedsAttention]
GO
ALTER TABLE [dbo].[Delivery] ADD  DEFAULT ('Pending') FOR [enmDeliveryStatus]
GO
ALTER TABLE [dbo].[Delivery] ADD  DEFAULT (getdate()) FOR [AddedOn]
GO
ALTER TABLE [dbo].[OrderHeader] ADD  DEFAULT (getdate()) FOR [OrderDate]
GO
ALTER TABLE [dbo].[OrderHeader] ADD  DEFAULT ((0)) FOR [clc_TotalAmount]
GO
ALTER TABLE [dbo].[OrderHeader] ADD  DEFAULT ((0)) FOR [clc_VATAmount]
GO
ALTER TABLE [dbo].[OrderHeader] ADD  DEFAULT ((0)) FOR [clc_TotalWithVAT]
GO
ALTER TABLE [dbo].[OrderHeader] ADD  DEFAULT ('Pending') FOR [enmPaymentStatus]
GO
ALTER TABLE [dbo].[OrderHeader] ADD  DEFAULT ('New') FOR [enmOrderStatus]
GO
ALTER TABLE [dbo].[OrderHeader] ADD  CONSTRAINT [DF_OrderHeader_AddedOn]  DEFAULT (getdate()) FOR [AddedOn]
GO
ALTER TABLE [dbo].[OrderLine] ADD  DEFAULT ((0)) FOR [DiscountPercent]
GO
ALTER TABLE [dbo].[OrderLine] ADD  DEFAULT ((0)) FOR [blg_UnitCost]
GO
ALTER TABLE [dbo].[OrderLine] ADD  CONSTRAINT [DF_OrderLine_AddedOn]  DEFAULT (getdate()) FOR [AddedOn]
GO
ALTER TABLE [dbo].[Product] ADD  DEFAULT ('General') FOR [enmCategory]
GO
ALTER TABLE [dbo].[Product] ADD  DEFAULT (N'יחידה') FOR [UnitOfMeasure]
GO
ALTER TABLE [dbo].[Product] ADD  DEFAULT ((1)) FOR [blg_IsActive]
GO
ALTER TABLE [dbo].[Product] ADD  DEFAULT ((0)) FOR [clc_CurrentStock]
GO
ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF_Product_AddedOn]  DEFAULT (getdate()) FOR [AddedOn]
GO
ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF_Product_BaseCost]  DEFAULT ((0)) FOR [BaseCost]
GO
ALTER TABLE [dbo].[ProductPrice] ADD  DEFAULT ((1)) FOR [MinQuantity]
GO
ALTER TABLE [dbo].[ProductPrice] ADD  DEFAULT ((0)) FOR [DiscountPercent]
GO
ALTER TABLE [dbo].[ProductPrice] ADD  CONSTRAINT [DF_ProductPrice_AddedOn]  DEFAULT (getdate()) FOR [AddedOn]
GO
ALTER TABLE [dbo].[ProductPriceHist] ADD  CONSTRAINT [DF_TableName_ProductPriceHist]  DEFAULT ((0)) FOR [TableName_ProductPriceHist]
GO
ALTER TABLE [dbo].[ProductPriceHist] ADD  DEFAULT ((0)) FOR [BaseCost]
GO
ALTER TABLE [dbo].[ProductPriceHist] ADD  DEFAULT ((1)) FOR [MinQuantity]
GO
ALTER TABLE [dbo].[ProductPriceHist] ADD  DEFAULT ((0)) FOR [DiscountPercent]
GO
ALTER TABLE [dbo].[ProductPriceHist] ADD  DEFAULT (getdate()) FOR [ArchivedDate]
GO
ALTER TABLE [dbo].[ProductPriceHist] ADD  CONSTRAINT [DF_ProductPriceHist_AddedOn]  DEFAULT (getdate()) FOR [AddedOn]
GO
ALTER TABLE [dbo].[SupplierOrder] ADD  DEFAULT ('Draft') FOR [enmEmailStatus]
GO
ALTER TABLE [dbo].[SupplierOrder] ADD  DEFAULT (getdate()) FOR [AddedOn]
GO
ALTER TABLE [dbo].[SupplierOrderLine] ADD  DEFAULT (getdate()) FOR [AddedOn]
GO
ALTER TABLE [dbo].[SystemSettings] ADD  DEFAULT ('String') FOR [SettingType]
GO
ALTER TABLE [dbo].[SystemSettings] ADD  DEFAULT (getdate()) FOR [AddedOn]
GO
ALTER TABLE [dbo].[BeehiveBuyerTracking]  WITH CHECK ADD  CONSTRAINT [FK_BeehiveBuyerTracking_Customer] FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customer] ([ID])
GO
ALTER TABLE [dbo].[BeehiveBuyerTracking] CHECK CONSTRAINT [FK_BeehiveBuyerTracking_Customer]
GO
ALTER TABLE [dbo].[c_JobAlertRecipient]  WITH CHECK ADD  CONSTRAINT [FK_c_JobAlertRecipient_c_Job] FOREIGN KEY([c_JobID])
REFERENCES [dbo].[c_Job] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[c_JobAlertRecipient] CHECK CONSTRAINT [FK_c_JobAlertRecipient_c_Job]
GO
ALTER TABLE [dbo].[c_JobAlertRecipient]  WITH CHECK ADD  CONSTRAINT [FK_c_JobAlertRecipient_c_User] FOREIGN KEY([c_UserID])
REFERENCES [dbo].[c_User] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[c_JobAlertRecipient] CHECK CONSTRAINT [FK_c_JobAlertRecipient_c_User]
GO
ALTER TABLE [dbo].[c_LoggedAlert]  WITH NOCHECK ADD  CONSTRAINT [FK_c_LoggedAlert_c_LoggedLogin] FOREIGN KEY([c_LoggedLoginID])
REFERENCES [dbo].[c_LoggedLogin] ([ID])
ON DELETE SET NULL
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[c_LoggedAlert] NOCHECK CONSTRAINT [FK_c_LoggedAlert_c_LoggedLogin]
GO
ALTER TABLE [dbo].[c_LoggedAlert]  WITH CHECK ADD  CONSTRAINT [FK_c_LoggedAlert_c_User] FOREIGN KEY([AffectedUserID])
REFERENCES [dbo].[c_User] ([ID])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[c_LoggedAlert] CHECK CONSTRAINT [FK_c_LoggedAlert_c_User]
GO
ALTER TABLE [dbo].[c_LoggedJob]  WITH CHECK ADD  CONSTRAINT [FK_c_LoggedJob_c_Job] FOREIGN KEY([c_JobID])
REFERENCES [dbo].[c_Job] ([ID])
GO
ALTER TABLE [dbo].[c_LoggedJob] CHECK CONSTRAINT [FK_c_LoggedJob_c_Job]
GO
ALTER TABLE [dbo].[c_LoggedJob]  WITH CHECK ADD  CONSTRAINT [FK_c_LoggedJob_c_LoggedAlert] FOREIGN KEY([c_LoggedAlertID])
REFERENCES [dbo].[c_LoggedAlert] ([ID])
GO
ALTER TABLE [dbo].[c_LoggedJob] CHECK CONSTRAINT [FK_c_LoggedJob_c_LoggedAlert]
GO
ALTER TABLE [dbo].[c_LoggedRequest]  WITH CHECK ADD  CONSTRAINT [FK_c_LoggedRequest_c_LoggedLogin] FOREIGN KEY([c_LoggedLoginID])
REFERENCES [dbo].[c_LoggedLogin] ([ID])
GO
ALTER TABLE [dbo].[c_LoggedRequest] CHECK CONSTRAINT [FK_c_LoggedRequest_c_LoggedLogin]
GO
ALTER TABLE [dbo].[c_LoggedRequest]  WITH CHECK ADD  CONSTRAINT [FK_c_LoggedRequest_c_User] FOREIGN KEY([c_UserID])
REFERENCES [dbo].[c_User] ([ID])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[c_LoggedRequest] CHECK CONSTRAINT [FK_c_LoggedRequest_c_User]
GO
ALTER TABLE [dbo].[c_MFA]  WITH NOCHECK ADD  CONSTRAINT [FK_c_MFA_c_User] FOREIGN KEY([c_UserID])
REFERENCES [dbo].[c_User] ([ID])
GO
ALTER TABLE [dbo].[c_MFA] CHECK CONSTRAINT [FK_c_MFA_c_User]
GO
ALTER TABLE [dbo].[c_ObjectTranslation]  WITH CHECK ADD  CONSTRAINT [FK_c_ObjectTranslation_c_ObjectToTranslate] FOREIGN KEY([c_ObjectToTranslateID])
REFERENCES [dbo].[c_ObjectToTranslate] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[c_ObjectTranslation] CHECK CONSTRAINT [FK_c_ObjectTranslation_c_ObjectToTranslate]
GO
ALTER TABLE [dbo].[c_Permission]  WITH NOCHECK ADD  CONSTRAINT [FK_c_Permission_c_Process] FOREIGN KEY([c_ProcessID])
REFERENCES [dbo].[c_Process] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[c_Permission] CHECK CONSTRAINT [FK_c_Permission_c_Process]
GO
ALTER TABLE [dbo].[c_Permission]  WITH NOCHECK ADD  CONSTRAINT [FK_c_Permission_c_Role] FOREIGN KEY([c_RoleID])
REFERENCES [dbo].[c_Role] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[c_Permission] CHECK CONSTRAINT [FK_c_Permission_c_Role]
GO
ALTER TABLE [dbo].[c_Role]  WITH CHECK ADD  CONSTRAINT [FK_c_Role_c_Role] FOREIGN KEY([BaseRoleID])
REFERENCES [dbo].[c_Role] ([ID])
GO
ALTER TABLE [dbo].[c_Role] CHECK CONSTRAINT [FK_c_Role_c_Role]
GO
ALTER TABLE [dbo].[c_User]  WITH CHECK ADD  CONSTRAINT [FK_c_User_c_Role] FOREIGN KEY([RoleID])
REFERENCES [dbo].[c_Role] ([ID])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[c_User] CHECK CONSTRAINT [FK_c_User_c_Role]
GO
ALTER TABLE [dbo].[c_UserLoginKey]  WITH NOCHECK ADD  CONSTRAINT [FK_c_UserLoginKey_c_User] FOREIGN KEY([c_UserID])
REFERENCES [dbo].[c_User] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[c_UserLoginKey] CHECK CONSTRAINT [FK_c_UserLoginKey_c_User]
GO
ALTER TABLE [dbo].[c_UserPermission]  WITH CHECK ADD  CONSTRAINT [FK_c_UserPermission_c_User] FOREIGN KEY([c_UserID])
REFERENCES [dbo].[c_User] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[c_UserPermission] CHECK CONSTRAINT [FK_c_UserPermission_c_User]
GO
ALTER TABLE [dbo].[c_UserStatus]  WITH CHECK ADD  CONSTRAINT [FK_c_UserStatus_c_User] FOREIGN KEY([c_UserID])
REFERENCES [dbo].[c_User] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[c_UserStatus] CHECK CONSTRAINT [FK_c_UserStatus_c_User]
GO
ALTER TABLE [dbo].[CustomerDebt]  WITH CHECK ADD  CONSTRAINT [FK_CustomerDebt_Customer] FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customer] ([ID])
GO
ALTER TABLE [dbo].[CustomerDebt] CHECK CONSTRAINT [FK_CustomerDebt_Customer]
GO
ALTER TABLE [dbo].[CustomerDebt]  WITH CHECK ADD  CONSTRAINT [FK_CustomerDebt_OrderHeader] FOREIGN KEY([OrderHeaderID])
REFERENCES [dbo].[OrderHeader] ([ID])
GO
ALTER TABLE [dbo].[CustomerDebt] CHECK CONSTRAINT [FK_CustomerDebt_OrderHeader]
GO
ALTER TABLE [dbo].[Delivery]  WITH CHECK ADD  CONSTRAINT [FK_Delivery_OrderHeader] FOREIGN KEY([OrderHeaderID])
REFERENCES [dbo].[OrderHeader] ([ID])
GO
ALTER TABLE [dbo].[Delivery] CHECK CONSTRAINT [FK_Delivery_OrderHeader]
GO
ALTER TABLE [dbo].[OrderHeader]  WITH CHECK ADD  CONSTRAINT [FK_OrderHeader_Customer] FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customer] ([ID])
GO
ALTER TABLE [dbo].[OrderHeader] CHECK CONSTRAINT [FK_OrderHeader_Customer]
GO
ALTER TABLE [dbo].[OrderLine]  WITH CHECK ADD  CONSTRAINT [FK_OrderLine_OrderHeader] FOREIGN KEY([OrderHeaderID])
REFERENCES [dbo].[OrderHeader] ([ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrderLine] CHECK CONSTRAINT [FK_OrderLine_OrderHeader]
GO
ALTER TABLE [dbo].[OrderLine]  WITH CHECK ADD  CONSTRAINT [FK_OrderLine_Product] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ID])
GO
ALTER TABLE [dbo].[OrderLine] CHECK CONSTRAINT [FK_OrderLine_Product]
GO
ALTER TABLE [dbo].[ProductPrice]  WITH CHECK ADD  CONSTRAINT [FK_ProductPrice_Product] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ID])
GO
ALTER TABLE [dbo].[ProductPrice] CHECK CONSTRAINT [FK_ProductPrice_Product]
GO
ALTER TABLE [dbo].[SupplierOrder]  WITH CHECK ADD  CONSTRAINT [FK_SupplierOrder_OrderHeader] FOREIGN KEY([OrderHeaderID])
REFERENCES [dbo].[OrderHeader] ([ID])
GO
ALTER TABLE [dbo].[SupplierOrder] CHECK CONSTRAINT [FK_SupplierOrder_OrderHeader]
GO
ALTER TABLE [dbo].[SupplierOrderLine]  WITH CHECK ADD  CONSTRAINT [FK_SupplierOrderLine_Product] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ID])
GO
ALTER TABLE [dbo].[SupplierOrderLine] CHECK CONSTRAINT [FK_SupplierOrderLine_Product]
GO
ALTER TABLE [dbo].[SupplierOrderLine]  WITH CHECK ADD  CONSTRAINT [FK_SupplierOrderLine_SupplierOrder] FOREIGN KEY([SupplierOrderID])
REFERENCES [dbo].[SupplierOrder] ([ID])
GO
ALTER TABLE [dbo].[SupplierOrderLine] CHECK CONSTRAINT [FK_SupplierOrderLine_SupplierOrder]
GO
/****** Object:  Trigger [dbo].[trTableDefaultDesignationChanged]    Script Date: 16/07/2026 22:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[trTableDefaultDesignationChanged] 
   ON  [dbo].[c_Table]
   AFTER INSERT,DELETE,UPDATE
AS 
BEGIN
	SET NOCOUNT ON;

  DECLARE @WhatDone varchar(1)
  DECLARE @DefaultTextFieldsIns varchar(100)

  DECLARE @TableNameIns varchar(50)
  SELECT @TableNameIns = i.[Name], @DefaultTextFieldsIns = i.DefaultTextFields
  FROM inserted i

  DECLARE @TableNameDel varchar(50)
  DECLARE @DefaultTextFieldsDel varchar(100)
  SELECT @TableNameDel = d.[Name], @DefaultTextFieldsDel = d.DefaultTextFields
  FROM deleted d

  IF (@TableNameIns IS NULL) 
    SET @WhatDone = 'D'
  ELSE
    IF (@TableNameDel IS NULL) 
      SET @WhatDone = 'I'
    ELSE
      SET @WhatDone = 'U'
    
  IF (@WhatDone = 'U')
    IF (@DefaultTextFieldsDel = @DefaultTextFieldsIns)
      RETURN

  DECLARE @TableName varchar(50)
  IF (@WhatDone = 'I') OR (@WhatDone = 'U')
    SET @TableName = @TableNameIns

  IF (@WhatDone = 'D')
    SET @TableName = @TableNameDel

  IF EXISTS (SELECT [name] FROM [sys].[views] WHERE [name] = 'ccvwComboList_' + @TableName)
    BEGIN
    DECLARE @sql NVARCHAR(MAX) = N'';

        SELECT @sql += '
        DROP VIEW ' 
            + QUOTENAME(s.name)
            + '.' + QUOTENAME(t.name) + ';'
            FROM sys.views AS t
            INNER JOIN sys.schemas AS s
            ON t.[schema_id] = s.[schema_id] 
            WHERE t.name LIKE 'ccvwComboList_' + @TableName + '%';

    EXEC sp_executesql @sql;
    END
END
GO
ALTER TABLE [dbo].[c_Table] ENABLE TRIGGER [trTableDefaultDesignationChanged]
GO
/****** Object:  Trigger [dbo].[trg_OrderHeader_AutoCreateDelivery]    Script Date: 16/07/2026 22:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TRIGGER [dbo].[trg_OrderHeader_AutoCreateDelivery]
ON [dbo].[OrderHeader]
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON

    INSERT INTO [dbo].[Delivery] (
        [OrderHeaderID], [DeliveryAddress], [ContactPhone], [ContactName],
        [enmDeliveryMethod], [enmDeliveryStatus]
    )
    SELECT
        i.ID,
        c.[Address] + CASE WHEN c.City IS NOT NULL THEN N', ' + c.City ELSE '' END,
        c.Phone,
        c.CustomerName,
        i.enmDeliveryMethod,
        'Pending'
    FROM inserted i
    INNER JOIN dbo.Customer c ON c.ID = i.CustomerID
    WHERE i.enmDeliveryMethod IS NOT NULL
      AND i.enmDeliveryMethod <> 'NoDelivery'
END

GO
ALTER TABLE [dbo].[OrderHeader] ENABLE TRIGGER [trg_OrderHeader_AutoCreateDelivery]
GO
/****** Object:  Trigger [dbo].[trg_OrderLine_SetUnitCost]    Script Date: 16/07/2026 22:47:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TRIGGER [dbo].[trg_OrderLine_SetUnitCost]
ON [dbo].[OrderLine]
AFTER INSERT, UPDATE
AS
BEGIN
    UPDATE ol
    SET blg_UnitCost = p.BaseCost
    FROM OrderLine ol
    INNER JOIN inserted i ON ol.ID = i.ID
    INNER JOIN Product p ON p.ID = ol.ProductID
END
GO
ALTER TABLE [dbo].[OrderLine] ENABLE TRIGGER [trg_OrderLine_SetUnitCost]
GO
EXEC sys.sp_addextendedproperty @name=N'ccUsedForTableCleanup', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'c_AuditIndexed', @level2type=N'COLUMN',@level2name=N'OccurredAt'
GO
EXEC sys.sp_addextendedproperty @name=N'ccUpdateSetToNow', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'c_Job', @level2type=N'COLUMN',@level2name=N'Active'
GO
EXEC sys.sp_addextendedproperty @name=N'ccType', @value=N'blg' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'c_Job', @level2type=N'COLUMN',@level2name=N'ActivatingUser'
GO
EXEC sys.sp_addextendedproperty @name=N'ccUpdateSetToNow', @value=N'4' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'c_Job', @level2type=N'COLUMN',@level2name=N'ActivatingUser'
GO
EXEC sys.sp_addextendedproperty @name=N'ccDNA', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'c_Job', @level2type=N'COLUMN',@level2name=N'NextRunTime'
GO
EXEC sys.sp_addextendedproperty @name=N'ccType', @value=N'blg' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'c_Job', @level2type=N'COLUMN',@level2name=N'NextRunTime'
GO
EXEC sys.sp_addextendedproperty @name=N'ccUpdateSetToNow', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'c_Job', @level2type=N'COLUMN',@level2name=N'NextRunTime'
GO
EXEC sys.sp_addextendedproperty @name=N'ccDNA', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'c_Job', @level2type=N'COLUMN',@level2name=N'LastRunTime'
GO
EXEC sys.sp_addextendedproperty @name=N'ccType', @value=N'blg' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'c_Job', @level2type=N'COLUMN',@level2name=N'LastRunTime'
GO
EXEC sys.sp_addextendedproperty @name=N'ccDNA', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'c_Job', @level2type=N'COLUMN',@level2name=N'enmJobStatus'
GO
EXEC sys.sp_addextendedproperty @name=N'ccType', @value=N'blg' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'c_Job', @level2type=N'COLUMN',@level2name=N'enmJobStatus'
GO
EXEC sys.sp_addextendedproperty @name=N'ccDNA', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'c_Job', @level2type=N'COLUMN',@level2name=N'WarningMailSent'
GO
EXEC sys.sp_addextendedproperty @name=N'ccType', @value=N'blg' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'c_Job', @level2type=N'COLUMN',@level2name=N'WarningMailSent'
GO
EXEC sys.sp_addextendedproperty @name=N'ccUpdateSetToNow', @value=N'3' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'c_Job', @level2type=N'COLUMN',@level2name=N'WarningMailSent'
GO
EXEC sys.sp_addextendedproperty @name=N'ccDNA', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'c_Job', @level2type=N'COLUMN',@level2name=N'LastRunBy'
GO
EXEC sys.sp_addextendedproperty @name=N'ccUsedForTableCleanup', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'c_LoggedAlert', @level2type=N'COLUMN',@level2name=N'TimeOccurred'
GO
EXEC sys.sp_addextendedproperty @name=N'ccUsedForTableCleanup', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'c_LoggedLogin', @level2type=N'COLUMN',@level2name=N'TimeLoggedIn'
GO
EXEC sys.sp_addextendedproperty @name=N'ccUsedForTableCleanup', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'c_LoggedRequest', @level2type=N'COLUMN',@level2name=N'TimeAccessed'
GO
EXEC sys.sp_addextendedproperty @name=N'ccType', @value=N'blg' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'c_ObjectTranslation', @level2type=N'COLUMN',@level2name=N'DefaultText'
GO
EXEC sys.sp_addextendedproperty @name=N'ccDNA', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'c_User', @level2type=N'COLUMN',@level2name=N'clc_FullName'
GO
EXEC sys.sp_addextendedproperty @name=N'ccType', @value=N'clc' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'c_User', @level2type=N'COLUMN',@level2name=N'clc_FullName'
GO
EXEC sys.sp_addextendedproperty @name=N'ccDNA', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'c_User', @level2type=N'COLUMN',@level2name=N'enoPassword'
GO
EXEC sys.sp_addextendedproperty @name=N'ccType', @value=N'spt' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'c_User', @level2type=N'COLUMN',@level2name=N'enoPassword'
GO
EXEC sys.sp_addextendedproperty @name=N'ccType', @value=N'clc' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'c_User', @level2type=N'COLUMN',@level2name=N'DatePasswordChanged'
GO
EXEC sys.sp_addextendedproperty @name=N'ccDNA', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'c_User', @level2type=N'COLUMN',@level2name=N'clc_DateActivated'
GO
EXEC sys.sp_addextendedproperty @name=N'ccType', @value=N'clc' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'c_User', @level2type=N'COLUMN',@level2name=N'clc_DateActivated'
GO
EXEC sys.sp_addextendedproperty @name=N'ccType', @value=N'spt' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'c_User', @level2type=N'COLUMN',@level2name=N'Comments'
GO
EXEC sys.sp_addextendedproperty @name=N'ccDNA', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'c_User', @level2type=N'COLUMN',@level2name=N'LastPasswords'
GO
EXEC sys.sp_addextendedproperty @name=N'ccType', @value=N'clc' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'c_User', @level2type=N'COLUMN',@level2name=N'LastPasswords'
GO
EXEC sys.sp_addextendedproperty @name=N'ccUpdateApproval', @value=N'1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'c_User', @level2type=N'COLUMN',@level2name=N'enoApprovalCode'
GO
EXEC sys.sp_addextendedproperty @name=N'ccUpdateApproval', @value=N'2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'c_User', @level2type=N'COLUMN',@level2name=N'ApprovalFunctionName'
GO
EXEC sys.sp_addextendedproperty @name=N'ccUpdateApproval', @value=N'3' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'c_User', @level2type=N'COLUMN',@level2name=N'ApprovalTime'
GO
