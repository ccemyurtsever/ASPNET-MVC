
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/14/2024 15:25:49
-- Generated from EDMX file: C:\Users\ccemy\OneDrive\Desktop\GitHub\ASPNET-MVC\WebApplication\Models\BipModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Db_AracKiralama];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Tbl_Araclar]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Tbl_Araclar];
GO
IF OBJECT_ID(N'[dbo].[Tbl_Rezervasyonlar]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Tbl_Rezervasyonlar];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Tbl_Araclar'
CREATE TABLE [dbo].[Tbl_Araclar] (
    [AracId] int IDENTITY(1,1) NOT NULL,
    [Marka] nvarchar(50)  NULL,
    [Model] nvarchar(50)  NULL,
    [ModelYili] smallint  NULL,
    [Yakit] nvarchar(20)  NULL,
    [Vites] nvarchar(20)  NULL,
    [Fiyat] decimal(19,4)  NULL
);
GO

-- Creating table 'Tbl_Rezervasyonlar'
CREATE TABLE [dbo].[Tbl_Rezervasyonlar] (
    [RezervasyonId] int IDENTITY(1,1) NOT NULL,
    [AracId] int  NULL,
    [TcKimlik] varbinary(11)  NULL,
    [AdSoyad] nvarchar(50)  NULL,
    [AlmaTarihi] datetime  NULL,
    [TeslimTarihi] datetime  NULL,
    [Ucret] decimal(19,4)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [AracId] in table 'Tbl_Araclar'
ALTER TABLE [dbo].[Tbl_Araclar]
ADD CONSTRAINT [PK_Tbl_Araclar]
    PRIMARY KEY CLUSTERED ([AracId] ASC);
GO

-- Creating primary key on [RezervasyonId] in table 'Tbl_Rezervasyonlar'
ALTER TABLE [dbo].[Tbl_Rezervasyonlar]
ADD CONSTRAINT [PK_Tbl_Rezervasyonlar]
    PRIMARY KEY CLUSTERED ([RezervasyonId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------