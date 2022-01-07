-- setup db for file upload and download to asp.net web page in mvc razor

USE [master]
GO

IF  EXISTS (SELECT name FROM sys.databases WHERE name = N'testdb')
ALTER DATABASE testdb SET SINGLE_USER WITH ROLLBACK IMMEDIATE  --same as drop connections checkbox, if they exist, can lose data so be warned
DROP DATABASE testdb
GO


CREATE DATABASE testdb;
GO

use [testdb]
CREATE TABLE [dbo].[Images](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[imgdata] [varbinary](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

