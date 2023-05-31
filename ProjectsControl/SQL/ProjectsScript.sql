Create database ProjectsManager;
go;
use ProjectsManager;
go;


IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Customers] (
    [CustomerId] nvarchar(450) NOT NULL,
    [DNIOfCustomer] int NOT NULL,
    [Name] nvarchar(120) NOT NULL,
    [Sector] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Customers] PRIMARY KEY ([CustomerId])
);
GO

CREATE TABLE [Employees] (
    [EmployeeId] nvarchar(450) NOT NULL,
    [EmployeeDNI] int NOT NULL,
    [Name] nvarchar(60) NOT NULL,
    [Position] nvarchar(max) NOT NULL,
    [DateofHiring] datetime2 NOT NULL,
    [DateOfBirth] datetime2 NOT NULL,
    [DateOfFired] datetime2 NOT NULL,
    [IsActive] bit NOT NULL,
    [MobileNumber] int NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    [Salary] real NOT NULL,
    CONSTRAINT [PK_Employees] PRIMARY KEY ([EmployeeId])
);
GO

CREATE TABLE [Offers] (
    [OfferId] nvarchar(450) NOT NULL,
    [NumberOfOffer] int NOT NULL,
    [Title] nvarchar(max) NOT NULL,
    [Type] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [Author] nvarchar(max) NOT NULL,
    [SaleManName] nvarchar(max) NOT NULL,
    [DateOfCreation] datetime2 NOT NULL,
    [Amount] real NOT NULL,
    [TypeCurrency] nvarchar(10) NULL,
    [LastEdition] datetime2 NOT NULL,
    [CustomerId] nvarchar(450) NULL,
    CONSTRAINT [PK_Offers] PRIMARY KEY ([OfferId]),
    CONSTRAINT [FK_Offers_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customers] ([CustomerId])
);
GO

CREATE TABLE [Actions] (
    [ActionId] nvarchar(450) NOT NULL,
    [Title] nvarchar(max) NOT NULL,
    [DateOfCreation] datetime2 NOT NULL,
    [Author] nvarchar(max) NOT NULL,
    [TypeOfAction] nvarchar(max) NOT NULL,
    [Description] nvarchar(340) NOT NULL,
    [IsActive] bit NOT NULL,
    [EmployeeId] nvarchar(450) NULL,
    CONSTRAINT [PK_Actions] PRIMARY KEY ([ActionId]),
    CONSTRAINT [FK_Actions_Employees_EmployeeId] FOREIGN KEY ([EmployeeId]) REFERENCES [Employees] ([EmployeeId])
);
GO

CREATE TABLE [Salary] (
    [SalaryId] nvarchar(450) NOT NULL,
    [SalaryAmount] float NOT NULL,
    [DayOfApplication] datetime2 NOT NULL,
    [notes] nvarchar(max) NULL,
    [isActive] bit NOT NULL,
    [EmployeeId] nvarchar(450) NULL,
    CONSTRAINT [PK_Salary] PRIMARY KEY ([SalaryId]),
    CONSTRAINT [FK_Salary_Employees_EmployeeId] FOREIGN KEY ([EmployeeId]) REFERENCES [Employees] ([EmployeeId])
);
GO

CREATE TABLE [Projects] (
    [ProjectId] nvarchar(450) NOT NULL,
    [NumberOfProject] int NOT NULL,
    [NumberOfTask] int NOT NULL,
    [ProjectName] nvarchar(50) NOT NULL,
    [OC] nvarchar(max) NULL,
    [OCDate] datetime2 NOT NULL,
    [BeginDate] datetime2 NOT NULL,
    [EndDate] datetime2 NOT NULL,
    [Manager] nvarchar(max) NULL,
    [Technician] nvarchar(max) NULL,
    [Estatus] nvarchar(max) NULL,
    [IsOver] bit NOT NULL,
    [Amount] real NOT NULL,
    [Currency] nvarchar(max) NULL,
    [PendingAmount] float NOT NULL,
    [TypeOfJob] nvarchar(max) NULL,
    [Details] nvarchar(max) NOT NULL,
    [Ubication] nvarchar(max) NOT NULL,
    [NumberOfOffer] nvarchar(max) NULL,
    [EmployeeId] nvarchar(450) NULL,
    [CustomerId] nvarchar(450) NULL,
    [OfferId] nvarchar(450) NULL,
    CONSTRAINT [PK_Projects] PRIMARY KEY ([ProjectId]),
    CONSTRAINT [FK_Projects_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customers] ([CustomerId]),
    CONSTRAINT [FK_Projects_Employees_EmployeeId] FOREIGN KEY ([EmployeeId]) REFERENCES [Employees] ([EmployeeId]),
    CONSTRAINT [FK_Projects_Offers_OfferId] FOREIGN KEY ([OfferId]) REFERENCES [Offers] ([OfferId])
);
GO

CREATE TABLE [Asistances] (
    [AsistanceId] nvarchar(450) NOT NULL,
    [DateOfBegin] datetime2 NOT NULL,
    [DateOfEnd] datetime2 NOT NULL,
    [EmployeeId] nvarchar(450) NULL,
    [ProjectId] nvarchar(450) NULL,
    [NumberOfWeek] nvarchar(max) NULL,
    CONSTRAINT [PK_Asistances] PRIMARY KEY ([AsistanceId]),
    CONSTRAINT [FK_Asistances_Employees_EmployeeId] FOREIGN KEY ([EmployeeId]) REFERENCES [Employees] ([EmployeeId]),
    CONSTRAINT [FK_Asistances_Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [Projects] ([ProjectId])
);
GO

CREATE TABLE [Bill] (
    [BillId] nvarchar(450) NOT NULL,
    [NumberOfBill] int NOT NULL,
    [DateOfCreation] datetime2 NOT NULL,
    [Author] nvarchar(max) NOT NULL,
    [Amount] real NOT NULL,
    [Currency] nvarchar(max) NULL,
    [Notes] nvarchar(max) NULL,
    [ProjectId] nvarchar(450) NULL,
    CONSTRAINT [PK_Bill] PRIMARY KEY ([BillId]),
    CONSTRAINT [FK_Bill_Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [Projects] ([ProjectId])
);
GO

CREATE TABLE [Expensives] (
    [ExpensiveId] nvarchar(450) NOT NULL,
    [Author] nvarchar(max) NOT NULL,
    [LastModification] datetime2 NOT NULL,
    [Type] nvarchar(max) NOT NULL,
    [Amount] real NOT NULL,
    [Currency] nvarchar(max) NOT NULL,
    [Note] nvarchar(max) NULL,
    [ProjectId] nvarchar(450) NULL,
    CONSTRAINT [PK_Expensives] PRIMARY KEY ([ExpensiveId]),
    CONSTRAINT [FK_Expensives_Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [Projects] ([ProjectId])
);
GO

CREATE TABLE [Notes] (
    [NotesId] nvarchar(450) NOT NULL,
    [Author] nvarchar(max) NOT NULL,
    [DateOfCreation] datetime2 NOT NULL,
    [Title] nvarchar(120) NOT NULL,
    [NoteDescription] nvarchar(340) NOT NULL,
    [ProjectId] nvarchar(450) NULL,
    CONSTRAINT [PK_Notes] PRIMARY KEY ([NotesId]),
    CONSTRAINT [FK_Notes_Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [Projects] ([ProjectId])
);
GO

CREATE TABLE [Report] (
    [ReportId] nvarchar(450) NOT NULL,
    [NumberOfReport] int NOT NULL,
    [Author] nvarchar(max) NULL,
    [BeginDate] datetime2 NOT NULL,
    [EndDate] datetime2 NOT NULL,
    [Status] nvarchar(max) NOT NULL,
    [Notes] nvarchar(max) NULL,
    [ProjectId] nvarchar(450) NULL,
    CONSTRAINT [PK_Report] PRIMARY KEY ([ReportId]),
    CONSTRAINT [FK_Report_Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [Projects] ([ProjectId])
);
GO

CREATE TABLE [ExtraHours] (
    [ExtraHourId] nvarchar(450) NOT NULL,
    [BeginTime] datetime2 NOT NULL,
    [EndTime] datetime2 NOT NULL,
    [TypeOfHour] nvarchar(max) NOT NULL,
    [Reason] nvarchar(max) NOT NULL,
    [Notes] nvarchar(max) NULL,
    [IsPaid] bit NOT NULL,
    [AceptedBy] nvarchar(max) NULL,
    [EmployeeId] nvarchar(450) NULL,
    [AsistanceId] nvarchar(450) NULL,
    [NumberOfWeek] nvarchar(max) NULL,
    CONSTRAINT [PK_ExtraHours] PRIMARY KEY ([ExtraHourId]),
    CONSTRAINT [FK_ExtraHours_Asistances_AsistanceId] FOREIGN KEY ([AsistanceId]) REFERENCES [Asistances] ([AsistanceId]),
    CONSTRAINT [FK_ExtraHours_Employees_EmployeeId] FOREIGN KEY ([EmployeeId]) REFERENCES [Employees] ([EmployeeId])
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CustomerId', N'DNIOfCustomer', N'Name', N'Sector') AND [object_id] = OBJECT_ID(N'[Customers]'))
    SET IDENTITY_INSERT [Customers] ON;
INSERT INTO [Customers] ([CustomerId], [DNIOfCustomer], [Name], [Sector])
VALUES (N'20f40614-6346-494d-bb7f-e91c0e746523', 110, N'Sample', N'Private');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'CustomerId', N'DNIOfCustomer', N'Name', N'Sector') AND [object_id] = OBJECT_ID(N'[Customers]'))
    SET IDENTITY_INSERT [Customers] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'EmployeeId', N'DateOfBirth', N'DateOfFired', N'DateofHiring', N'Email', N'EmployeeDNI', N'IsActive', N'MobileNumber', N'Name', N'Position', N'Salary') AND [object_id] = OBJECT_ID(N'[Employees]'))
    SET IDENTITY_INSERT [Employees] ON;
INSERT INTO [Employees] ([EmployeeId], [DateOfBirth], [DateOfFired], [DateofHiring], [Email], [EmployeeDNI], [IsActive], [MobileNumber], [Name], [Position], [Salary])
VALUES (N'2d457410-8875-4815-b7d6-ca17d32fab65', '2022-12-12T00:00:00.0000000-06:00', '2022-12-12T00:00:00.0000000-06:00', '2022-12-12T00:00:00.0000000-06:00', N'sample@grupomecsa.net', 1171292, CAST(1 AS bit), 888, N'Sample of name', N'd', CAST(0 AS real));
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'EmployeeId', N'DateOfBirth', N'DateOfFired', N'DateofHiring', N'Email', N'EmployeeDNI', N'IsActive', N'MobileNumber', N'Name', N'Position', N'Salary') AND [object_id] = OBJECT_ID(N'[Employees]'))
    SET IDENTITY_INSERT [Employees] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ActionId', N'Author', N'DateOfCreation', N'Description', N'EmployeeId', N'IsActive', N'Title', N'TypeOfAction') AND [object_id] = OBJECT_ID(N'[Actions]'))
    SET IDENTITY_INSERT [Actions] ON;
INSERT INTO [Actions] ([ActionId], [Author], [DateOfCreation], [Description], [EmployeeId], [IsActive], [Title], [TypeOfAction])
VALUES (N'fde2495c-231f-4594-948a-f9875ae4edb0', N'Sample of author', '2022-12-12T00:00:00.0000000-06:00', N'sample of description', N'2d457410-8875-4815-b7d6-ca17d32fab65', CAST(1 AS bit), N'Sample of title', N'sample of type');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ActionId', N'Author', N'DateOfCreation', N'Description', N'EmployeeId', N'IsActive', N'Title', N'TypeOfAction') AND [object_id] = OBJECT_ID(N'[Actions]'))
    SET IDENTITY_INSERT [Actions] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'OfferId', N'Amount', N'Author', N'CustomerId', N'DateOfCreation', N'Description', N'LastEdition', N'NumberOfOffer', N'SaleManName', N'Title', N'Type', N'TypeCurrency') AND [object_id] = OBJECT_ID(N'[Offers]'))
    SET IDENTITY_INSERT [Offers] ON;
INSERT INTO [Offers] ([OfferId], [Amount], [Author], [CustomerId], [DateOfCreation], [Description], [LastEdition], [NumberOfOffer], [SaleManName], [Title], [Type], [TypeCurrency])
VALUES (N'8e1855e5-5a41-4d11-a442-9a1203aff384', CAST(100.3 AS real), N'Sample of Author', N'20f40614-6346-494d-bb7f-e91c0e746523', '2022-12-12T00:00:00.0000000-06:00', N'sample of description', '2022-12-12T00:00:00.0000000-06:00', 1, N'Sample of name', N'Title Sample', N'installation', N'dolar');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'OfferId', N'Amount', N'Author', N'CustomerId', N'DateOfCreation', N'Description', N'LastEdition', N'NumberOfOffer', N'SaleManName', N'Title', N'Type', N'TypeCurrency') AND [object_id] = OBJECT_ID(N'[Offers]'))
    SET IDENTITY_INSERT [Offers] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ProjectId', N'Amount', N'BeginDate', N'Currency', N'CustomerId', N'Details', N'EmployeeId', N'EndDate', N'Estatus', N'IsOver', N'Manager', N'NumberOfOffer', N'NumberOfProject', N'NumberOfTask', N'OC', N'OCDate', N'OfferId', N'PendingAmount', N'ProjectName', N'Technician', N'TypeOfJob', N'Ubication') AND [object_id] = OBJECT_ID(N'[Projects]'))
    SET IDENTITY_INSERT [Projects] ON;
INSERT INTO [Projects] ([ProjectId], [Amount], [BeginDate], [Currency], [CustomerId], [Details], [EmployeeId], [EndDate], [Estatus], [IsOver], [Manager], [NumberOfOffer], [NumberOfProject], [NumberOfTask], [OC], [OCDate], [OfferId], [PendingAmount], [ProjectName], [Technician], [TypeOfJob], [Ubication])
VALUES (N'2ff24d98-9e51-4355-9586-35b099a11b02', CAST(100 AS real), '2022-12-12T00:00:00.0000000-06:00', N'Dolar', N'20f40614-6346-494d-bb7f-e91c0e746523', N'Sample of details', N'2d457410-8875-4815-b7d6-ca17d32fab65', '2022-12-12T00:00:00.0000000-06:00', N'In progress', CAST(0 AS bit), N'Sample of Name', N'PS1', 1, 1, N'Oc Id Sample', '2022-12-12T00:00:00.0000000-06:00', N'8e1855e5-5a41-4d11-a442-9a1203aff384', 0.0E0, N'Sample Of Project', N'Sample', N'Sample of Job', N'San JoseCosta Rica');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ProjectId', N'Amount', N'BeginDate', N'Currency', N'CustomerId', N'Details', N'EmployeeId', N'EndDate', N'Estatus', N'IsOver', N'Manager', N'NumberOfOffer', N'NumberOfProject', N'NumberOfTask', N'OC', N'OCDate', N'OfferId', N'PendingAmount', N'ProjectName', N'Technician', N'TypeOfJob', N'Ubication') AND [object_id] = OBJECT_ID(N'[Projects]'))
    SET IDENTITY_INSERT [Projects] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'AsistanceId', N'DateOfBegin', N'DateOfEnd', N'EmployeeId', N'NumberOfWeek', N'ProjectId') AND [object_id] = OBJECT_ID(N'[Asistances]'))
    SET IDENTITY_INSERT [Asistances] ON;
INSERT INTO [Asistances] ([AsistanceId], [DateOfBegin], [DateOfEnd], [EmployeeId], [NumberOfWeek], [ProjectId])
VALUES (N'b2daeec7-d789-4699-8f17-4b8d69ec698e', '2022-12-12T00:00:00.0000000-06:00', '2022-12-12T00:00:00.0000000-06:00', N'2d457410-8875-4815-b7d6-ca17d32fab65', N'01', N'2ff24d98-9e51-4355-9586-35b099a11b02');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'AsistanceId', N'DateOfBegin', N'DateOfEnd', N'EmployeeId', N'NumberOfWeek', N'ProjectId') AND [object_id] = OBJECT_ID(N'[Asistances]'))
    SET IDENTITY_INSERT [Asistances] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'BillId', N'Amount', N'Author', N'Currency', N'DateOfCreation', N'Notes', N'NumberOfBill', N'ProjectId') AND [object_id] = OBJECT_ID(N'[Bill]'))
    SET IDENTITY_INSERT [Bill] ON;
INSERT INTO [Bill] ([BillId], [Amount], [Author], [Currency], [DateOfCreation], [Notes], [NumberOfBill], [ProjectId])
VALUES (N'636b6614-57a8-48c3-8383-f03e588c0559', CAST(1 AS real), N'Sample', N'Dolar', '2022-12-12T00:00:00.0000000-06:00', N'Sample of notes', 1, N'2ff24d98-9e51-4355-9586-35b099a11b02');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'BillId', N'Amount', N'Author', N'Currency', N'DateOfCreation', N'Notes', N'NumberOfBill', N'ProjectId') AND [object_id] = OBJECT_ID(N'[Bill]'))
    SET IDENTITY_INSERT [Bill] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ExpensiveId', N'Amount', N'Author', N'Currency', N'LastModification', N'Note', N'ProjectId', N'Type') AND [object_id] = OBJECT_ID(N'[Expensives]'))
    SET IDENTITY_INSERT [Expensives] ON;
INSERT INTO [Expensives] ([ExpensiveId], [Amount], [Author], [Currency], [LastModification], [Note], [ProjectId], [Type])
VALUES (N'b7050f75-cf75-4a3d-931d-67a639954791', CAST(1.12 AS real), N'Sample Of authot', N'Dolar', '2022-12-12T00:00:00.0000000-06:00', N'Sample', N'2ff24d98-9e51-4355-9586-35b099a11b02', N'Km Cost');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ExpensiveId', N'Amount', N'Author', N'Currency', N'LastModification', N'Note', N'ProjectId', N'Type') AND [object_id] = OBJECT_ID(N'[Expensives]'))
    SET IDENTITY_INSERT [Expensives] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'NotesId', N'Author', N'DateOfCreation', N'NoteDescription', N'ProjectId', N'Title') AND [object_id] = OBJECT_ID(N'[Notes]'))
    SET IDENTITY_INSERT [Notes] ON;
INSERT INTO [Notes] ([NotesId], [Author], [DateOfCreation], [NoteDescription], [ProjectId], [Title])
VALUES (N'e27b1552-66d1-467e-866c-cfc8ba19e932', N'Sample', '2022-12-12T00:00:00.0000000-06:00', N'Description of the action', N'2ff24d98-9e51-4355-9586-35b099a11b02', N'Sample');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'NotesId', N'Author', N'DateOfCreation', N'NoteDescription', N'ProjectId', N'Title') AND [object_id] = OBJECT_ID(N'[Notes]'))
    SET IDENTITY_INSERT [Notes] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ReportId', N'Author', N'BeginDate', N'EndDate', N'Notes', N'NumberOfReport', N'ProjectId', N'Status') AND [object_id] = OBJECT_ID(N'[Report]'))
    SET IDENTITY_INSERT [Report] ON;
INSERT INTO [Report] ([ReportId], [Author], [BeginDate], [EndDate], [Notes], [NumberOfReport], [ProjectId], [Status])
VALUES (N'4c27f5ea-4ea6-4390-93d0-cdf767398b6a', N'Sample of author', '2022-12-12T00:00:00.0000000-06:00', '2022-12-12T00:00:00.0000000-06:00', N'sample of notes', 1, N'2ff24d98-9e51-4355-9586-35b099a11b02', N'sample of estatus');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ReportId', N'Author', N'BeginDate', N'EndDate', N'Notes', N'NumberOfReport', N'ProjectId', N'Status') AND [object_id] = OBJECT_ID(N'[Report]'))
    SET IDENTITY_INSERT [Report] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ExtraHourId', N'AceptedBy', N'AsistanceId', N'BeginTime', N'EmployeeId', N'EndTime', N'IsPaid', N'Notes', N'NumberOfWeek', N'Reason', N'TypeOfHour') AND [object_id] = OBJECT_ID(N'[ExtraHours]'))
    SET IDENTITY_INSERT [ExtraHours] ON;
INSERT INTO [ExtraHours] ([ExtraHourId], [AceptedBy], [AsistanceId], [BeginTime], [EmployeeId], [EndTime], [IsPaid], [Notes], [NumberOfWeek], [Reason], [TypeOfHour])
VALUES (N'fc7c01f5-058a-4027-ad96-bb96eec2926d', N'Nyree', N'b2daeec7-d789-4699-8f17-4b8d69ec698e', '2022-12-12T00:00:00.0000000-06:00', N'2d457410-8875-4815-b7d6-ca17d32fab65', '2022-12-12T00:00:00.0000000-06:00', CAST(0 AS bit), N'as', N'01', N'ad', N'double');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ExtraHourId', N'AceptedBy', N'AsistanceId', N'BeginTime', N'EmployeeId', N'EndTime', N'IsPaid', N'Notes', N'NumberOfWeek', N'Reason', N'TypeOfHour') AND [object_id] = OBJECT_ID(N'[ExtraHours]'))
    SET IDENTITY_INSERT [ExtraHours] OFF;
GO

CREATE INDEX [IX_Actions_EmployeeId] ON [Actions] ([EmployeeId]);
GO

CREATE INDEX [IX_Asistances_EmployeeId] ON [Asistances] ([EmployeeId]);
GO

CREATE INDEX [IX_Asistances_ProjectId] ON [Asistances] ([ProjectId]);
GO

CREATE INDEX [IX_Bill_ProjectId] ON [Bill] ([ProjectId]);
GO

CREATE INDEX [IX_Expensives_ProjectId] ON [Expensives] ([ProjectId]);
GO

CREATE INDEX [IX_ExtraHours_AsistanceId] ON [ExtraHours] ([AsistanceId]);
GO

CREATE INDEX [IX_ExtraHours_EmployeeId] ON [ExtraHours] ([EmployeeId]);
GO

CREATE INDEX [IX_Notes_ProjectId] ON [Notes] ([ProjectId]);
GO

CREATE INDEX [IX_Offers_CustomerId] ON [Offers] ([CustomerId]);
GO

CREATE INDEX [IX_Projects_CustomerId] ON [Projects] ([CustomerId]);
GO

CREATE INDEX [IX_Projects_EmployeeId] ON [Projects] ([EmployeeId]);
GO

CREATE INDEX [IX_Projects_OfferId] ON [Projects] ([OfferId]);
GO

CREATE INDEX [IX_Report_ProjectId] ON [Report] ([ProjectId]);
GO

CREATE INDEX [IX_Salary_EmployeeId] ON [Salary] ([EmployeeId]);
GO

-- =============================================
-- Author:		Steven Gazo
-- Create date: 23/8/21
-- Description:	Search a projects by the specific information
-- =============================================
Create PROCEDURE SearchProjects
	-- Add the parameters for the stored procedure here
	 @_Month varchar(5)= null,
	 @_Year varchar(5)= null,
	 @_ProjectName varchar(max) =null,
	 @_ProjectStatus varchar(max) = null,
	 @_ProjectType varchar(max) =null,
	 @_ProjectNumber varchar(50) =null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	
	DECLARE	 @_sqlcommand varchar(max)= 'SELECT * FROM Projects';
	DECLARE	 @_band binary = 0;
	DECLARE	 @_exec binary = 0;
	IF @_ProjectName IS NOT NULL
	BEGIN
		IF @_band =0
		begin
			SET @_sqlcommand = @_sqlcommand + (' WHERE (ProjectName LIKE CONCAT(''%'',''' + @_ProjectName+ ''',''%''))');		
			SET @_band= 1;
			SET @_exec=1;
		end
	END
	IF @_ProjectNumber IS NOT NULL
	BEGIN
		IF @_band =0
		begin
			SET @_sqlcommand = @_sqlcommand + (' WHERE (NumberOfProject LIKE CONCAT(''%'',''' + @_ProjectNumber + ''',''%''))');
			SET @_band= 1;
			SET @_exec=1;
		end
		ELSE
		begin
			SET @_sqlcommand = @_sqlcommand + (' AND (NumberOfProject LIKE CONCAT(''%'',''' + @_ProjectNumber + ''',''%''))');
			SET @_exec=1;
		end
	END
	IF @_Month IS NOT NULL
	BEGIN
		IF @_band =0
		begin
			SET @_sqlcommand = @_sqlcommand + (' WHERE MONTH(OCDate) = '+@_Month) ;
			SET @_band= 1;
			SET @_exec=1;
		end
		ELSE
		begin
			SET @_sqlcommand = @_sqlcommand + (' AND MONTH(OCDate) = '+@_Month) ;
			SET @_exec=1;
		end
	END
	IF @_Year IS NOT NULL
	BEGIN
		IF @_band =0
		begin
			SET @_sqlcommand = @_sqlcommand + (' WHERE YEAR(OCDate)=' + @_Year);
			SET @_band= 1;
			SET @_exec=1;
		end
		ELSE
		begin
			SET @_sqlcommand = @_sqlcommand + (' AND YEAR(OCDate)=' + @_Year);
			SET @_band= 1;
			SET @_exec=1;
		end
	END
	IF @_ProjectStatus IS NOT NULL
	BEGIN
		IF @_band =0
		begin
			SET @_sqlcommand = @_sqlcommand + (' WHERE (Estatus LIKE CONCAT(''%'',''' + @_ProjectStatus + ''',''%''))');
			SET @_band= 1;
			SET @_exec=1;
		end
		ELSE
		begin
			SET @_sqlcommand = @_sqlcommand + (' AND (Estatus LIKE CONCAT(''%'',''' + @_ProjectStatus + ''',''%''))');
			SET @_exec=1;
		end
	END
	IF @_ProjectType IS NOT NULL
	BEGIN
		IF @_band =0
		begin
			SET @_sqlcommand = @_sqlcommand + (' WHERE (TypeOfJob LIKE CONCAT(''%'',''' + @_ProjectType + ''',''%''))');
			SET @_band= 1;
			SET @_exec=1;
		end
		ELSE
		begin
			SET @_sqlcommand = @_sqlcommand + (' AND (TypeOfJob LIKE CONCAT(''%'',''' + @_ProjectType + ''',''%''))');	
			SET @_exec=1;
		end
	END
	if @_exec = 1
		begin
			print(@_sqlcommand)
			EXEC (@_sqlcommand)
		end
	else
		begin

			select * from projects where ProjectId is null
		end
END

					
GO

-- =============================================
										-- Author:		Steven Gazo
										-- Create date: 02-03-2022
										-- Description:	Search in the table Asistances and return and specifics rows
										-- =============================================
										Create PROCEDURE SearchAsistances
											-- Add the parameters for the stored procedure here
											@_EmployeeId varchar(max)= null,
											@_ProjectId varchar(max)= null,
											@_DateToSearch varchar(max) = null,
											@_WeekNumber varchar(max) =  null
										AS
										BEGIN
											-- SET NOCOUNT ON added to prevent extra result sets from
											-- interfering with SELECT statements.
											SET NOCOUNT ON;

											-- Insert statements for procedure here
											DECLARE @_sqlCommand varchar(max) = 'SELECT Asistances.* FROM ASISTANCES';
											DECLARE @_flagParameters binary = 0 ;
											IF( @_EmployeeId IS NOT NULL)
											BEGIN
												if(@_flagParameters = 0)
													BEGIN
														SET @_flagParameters = 1;
														set @_sqlCommand  = @_sqlCommand + ' ' + ' WHERE EmployeeId = ''' + @_EmployeeId  + '''';
													END
												ELSE 
													BEGIN
														SET @_flagParameters = 1;
														set @_sqlCommand  = @_sqlCommand + ' ' + ' AND EmployeeId = ''' + @_EmployeeId  + '''';
													END	
											END
											IF( @_ProjectId IS NOT NULL )
											BEGIN
												if(@_flagParameters = 0)
												BEGIN
														SET @_flagParameters = 1;
														set @_sqlCommand  = @_sqlCommand + ' ' + ' WHERE ProjectId =  ''' + @_ProjectId + '''';
												END
												ELSE
													BEGIN
														SET @_flagParameters = 1;
														set @_sqlCommand  = @_sqlCommand + ' ' + ' AND ProjectId =  ''' + @_ProjectId + '''';
													END	
											END
											IF( @_WeekNumber IS NOT NULL)
											BEGIN
												IF( @_flagParameters = 0)
													BEGIN
														SET @_flagParameters = 1;
														set @_sqlCommand  = @_sqlCommand + ' ' + ' WHERE NumberOfWeek =  ''' + @_WeekNumber + '''';
													END
												ELSE
													BEGIN
														SET @_flagParameters = 1;
														set @_sqlCommand  = @_sqlCommand + ' ' + ' AND NumberOfWeek =  ''' + @_WeekNumber + '''';
													END
											END
											IF( @_DateToSearch IS NOT NULL)
											BEGIN
												IF( @_flagParameters = 0)
												BEGIN
														SET @_flagParameters = 1;
														set @_sqlCommand  = @_sqlCommand + ' ' + ' WHERE	CAST(DateOfBegin AS date) = CAST( ''' + @_DateToSearch+ ''' AS date) ';
														
												END
												ELSE
												BEGIN
														SET @_flagParameters = 1;
														set @_sqlCommand  = @_sqlCommand + ' ' + ' AND	CAST(DateOfBegin AS date) = CAST( ''' + @_DateToSearch+ ''' AS date) ';
												END
											END
											PRINT(@_sqlCommand)
											IF( @_flagParameters = 1)
											BEGIN
												EXEC (@_sqlCommand);
											END	
										END
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Steven Gazo
-- Create date: 22 November 2021
-- Description:	Pagination of the Projects in the database. Also, the projects are order by the numberProject
-- =============================================
CREATE PROCEDURE GetProjectsByPage
	-- Add the parameters for the stored procedure here
	@_PageNumber int = 1 ,
	@_QuantityOfDevices int = 10
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here   
    if(@_PageNumber = 1)
    BEGIN
        SELECT * 
        FROM Projects
        ORDER BY NumberOfProject DESC
        OFFSET 0 ROWS 
        FETCH NEXT @_QuantityOfDevices ROWS ONLY;
    END
    else
    BEGIN
        SELECT * 
        FROM Projects
        ORDER BY NumberOfProject Desc
        OFFSET @_QuantityOfDevices * (@_PageNumber - 1) ROWS 
        FETCH NEXT @_QuantityOfDevices ROWS ONLY;
    END
END
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20221212204357_migracionproyecto', N'6.0.10');
GO

COMMIT;
GO

