USE [NORTHWND]
GO

/****** Object:  StoredProcedure [dbo].[SelectCustomers] ******/
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.SelectCustomers'))
   exec('CREATE PROCEDURE [dbo].[SelectCustomers] AS BEGIN SET NOCOUNT ON; END')
GO

ALTER PROCEDURE [dbo].[SelectCustomers] AS
BEGIN
	SELECT * FROM [dbo].[Customers];
END
GO

/****** Object:  StoredProcedure [dbo].[CreateCustomer] ******/
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.CreateCustomer'))
   exec('CREATE PROCEDURE [dbo].[CreateCustomer] AS BEGIN SET NOCOUNT ON; END')
GO

ALTER PROCEDURE [dbo].[CreateCustomer]
	@CustomerID nchar(5),
	@CompanyName varchar(40),
	@ContactName varchar(30),
	@ContactTitle varchar(30),
	@Address varchar(60)
AS
BEGIN
	INSERT INTO [dbo].[Customers]
		(CustomerID, CompanyName, ContactName, ContactTitle, Address)
	VALUES
		(@CustomerID, @CompanyName, @ContactName, @ContactTitle, @Address);
END
GO

/****** Object:  StoredProcedure [dbo].[CreateCustomer] ******/
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('dbo.GetCustomer'))
   exec('CREATE PROCEDURE [dbo].[GetCustomer] AS BEGIN SET NOCOUNT ON; END')
GO

ALTER PROCEDURE [dbo].[GetCustomer]
	@CustomerID nchar(5)
AS
BEGIN
	SELECT * FROM [dbo].[Customers]
		WHERE [CustomerID] = @CustomerID;

	SELECT * FROM [dbo].[Orders]
		WHERE [CustomerID] = @CustomerID;
END
GO