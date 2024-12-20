USE [master]
GO
/****** Object:  Database [Materia]    Script Date: 01.11.2024 18:49:03 ******/
CREATE DATABASE [Materia]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Materia', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\Materia.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Materia_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\Materia_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Materia] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Materia].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Materia] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Materia] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Materia] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Materia] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Materia] SET ARITHABORT OFF 
GO
ALTER DATABASE [Materia] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Materia] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Materia] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Materia] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Materia] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Materia] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Materia] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Materia] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Materia] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Materia] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Materia] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Materia] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Materia] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Materia] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Materia] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Materia] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Materia] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Materia] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Materia] SET  MULTI_USER 
GO
ALTER DATABASE [Materia] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Materia] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Materia] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Materia] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Materia] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Materia] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [Materia] SET QUERY_STORE = OFF
GO
USE [Materia]
GO
/****** Object:  Table [dbo].[AuthHistory]    Script Date: 01.11.2024 18:49:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AuthHistory](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[Date] [date] NOT NULL,
 CONSTRAINT [PK_AuthHistory] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Material]    Script Date: 01.11.2024 18:49:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Material](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TypeID] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[DefectRate] [float] NOT NULL,
	[PartnerID] [int] NOT NULL,
	[Description] [text] NOT NULL,
	[Image] [nvarchar](50) NOT NULL,
	[Cost] [float] NOT NULL,
	[MeasurementUnitID] [int] NOT NULL,
	[QuantityInPack] [int] NOT NULL,
	[MinAcceptCount] [int] NOT NULL,
 CONSTRAINT [PK_Material] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MaterialsInStore]    Script Date: 01.11.2024 18:49:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MaterialsInStore](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[StoreID] [int] NOT NULL,
	[MaterialID] [int] NOT NULL,
	[Amount] [int] NOT NULL,
 CONSTRAINT [PK_MaterialsInStore] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MaterialsInSupply]    Script Date: 01.11.2024 18:49:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MaterialsInSupply](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SupplyID] [int] NOT NULL,
	[MaterialID] [int] NOT NULL,
	[Amount] [int] NOT NULL,
 CONSTRAINT [PK_MaterialsInSupply] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MaterialType]    Script Date: 01.11.2024 18:49:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MaterialType](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_MaterialType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MeasurementUnits]    Script Date: 01.11.2024 18:49:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MeasurementUnits](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_MeasurementUnits] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 01.11.2024 18:49:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Status] [bit] NOT NULL,
	[UserID] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
 CONSTRAINT [PK_Sale] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Partner]    Script Date: 01.11.2024 18:49:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Partner](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[TypeID] [int] NOT NULL,
	[Director] [nvarchar](255) NOT NULL,
	[Email] [nvarchar](255) NOT NULL,
	[PhoneNumber] [nvarchar](255) NOT NULL,
	[Address] [nvarchar](255) NOT NULL,
	[INN] [float] NOT NULL,
	[Rating] [float] NOT NULL,
 CONSTRAINT [PK_Partner] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PartnerType]    Script Date: 01.11.2024 18:49:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PartnerType](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_PartnerType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 01.11.2024 18:49:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[Article] [int] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[TypeID] [int] NOT NULL,
	[MinCostForPartner] [float] NOT NULL,
	[Logo] [nvarchar](255) NULL,
	[Description] [text] NULL,
	[PackageSize] [nvarchar](50) NULL,
	[WeightWithPack] [float] NULL,
	[WeightWithoutPack] [float] NULL,
	[QualityCertificate] [nvarchar](50) NULL,
	[StandardNumber] [int] NULL,
	[Cost] [float] NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[Article] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductsInOrder]    Script Date: 01.11.2024 18:49:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductsInOrder](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[OrderID] [int] NOT NULL,
	[ProductArticle] [int] NOT NULL,
	[Amount] [int] NOT NULL,
	[Cost] [int] NOT NULL,
 CONSTRAINT [PK_ProductsInOrder] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductsInSale]    Script Date: 01.11.2024 18:49:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductsInSale](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SaleID] [int] NOT NULL,
	[ProductArticle] [int] NOT NULL,
	[Amount] [int] NOT NULL,
 CONSTRAINT [PK_ProductsInSale] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductType]    Script Date: 01.11.2024 18:49:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductType](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Сoefficient] [float] NOT NULL,
 CONSTRAINT [PK_MaterialType$] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RequiredMaterials]    Script Date: 01.11.2024 18:49:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RequiredMaterials](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MaterialID] [int] NOT NULL,
	[Amount] [int] NOT NULL,
 CONSTRAINT [PK_RequiredMaterials] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 01.11.2024 18:49:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sale]    Script Date: 01.11.2024 18:49:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sale](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PlaceID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[Date] [date] NOT NULL,
 CONSTRAINT [PK_SalesHistory] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SalePlace]    Script Date: 01.11.2024 18:49:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalePlace](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[TypeID] [int] NOT NULL,
 CONSTRAINT [PK_SalePlace] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SalePlaceType]    Script Date: 01.11.2024 18:49:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalePlaceType](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_SalePlaceType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Store]    Script Date: 01.11.2024 18:49:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Store](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Store] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Supply]    Script Date: 01.11.2024 18:49:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Supply](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[Date] [date] NOT NULL,
 CONSTRAINT [PK_Supply] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 01.11.2024 18:49:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RoleID] [int] NOT NULL,
	[PartnerID] [int] NULL,
	[Health] [bit] NOT NULL,
	[FIO] [nvarchar](100) NOT NULL,
	[Birthday] [date] NOT NULL,
	[Passport] [nvarchar](10) NOT NULL,
	[HaveFamily] [bit] NOT NULL,
	[Login] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Material] ON 

INSERT [dbo].[Material] ([ID], [TypeID], [Name], [DefectRate], [PartnerID], [Description], [Image], [Cost], [MeasurementUnitID], [QuantityInPack], [MinAcceptCount]) VALUES (13, 1, N'Берёза', 2, 4, N'?', N'и', 10, 1, 1, 1)
INSERT [dbo].[Material] ([ID], [TypeID], [Name], [DefectRate], [PartnerID], [Description], [Image], [Cost], [MeasurementUnitID], [QuantityInPack], [MinAcceptCount]) VALUES (14, 1, N'Дуб', 1, 4, N'? ', N'й', 11, 1, 1, 1)
SET IDENTITY_INSERT [dbo].[Material] OFF
GO
SET IDENTITY_INSERT [dbo].[MaterialsInStore] ON 

INSERT [dbo].[MaterialsInStore] ([ID], [StoreID], [MaterialID], [Amount]) VALUES (4, 1, 13, 10)
INSERT [dbo].[MaterialsInStore] ([ID], [StoreID], [MaterialID], [Amount]) VALUES (5, 1, 14, 15)
INSERT [dbo].[MaterialsInStore] ([ID], [StoreID], [MaterialID], [Amount]) VALUES (6, 1, 14, 5)
INSERT [dbo].[MaterialsInStore] ([ID], [StoreID], [MaterialID], [Amount]) VALUES (7, 1, 13, 1)
INSERT [dbo].[MaterialsInStore] ([ID], [StoreID], [MaterialID], [Amount]) VALUES (8, 1, 14, 66)
SET IDENTITY_INSERT [dbo].[MaterialsInStore] OFF
GO
SET IDENTITY_INSERT [dbo].[MaterialType] ON 

INSERT [dbo].[MaterialType] ([ID], [Name]) VALUES (1, N'Дерево')
SET IDENTITY_INSERT [dbo].[MaterialType] OFF
GO
SET IDENTITY_INSERT [dbo].[MeasurementUnits] ON 

INSERT [dbo].[MeasurementUnits] ([ID], [Name]) VALUES (1, N'Штука')
SET IDENTITY_INSERT [dbo].[MeasurementUnits] OFF
GO
SET IDENTITY_INSERT [dbo].[Order] ON 

INSERT [dbo].[Order] ([ID], [Status], [UserID], [Date]) VALUES (1, 1, 1, CAST(N'2023-03-23T00:00:00.000' AS DateTime))
INSERT [dbo].[Order] ([ID], [Status], [UserID], [Date]) VALUES (2, 1, 1, CAST(N'2023-12-18T00:00:00.000' AS DateTime))
INSERT [dbo].[Order] ([ID], [Status], [UserID], [Date]) VALUES (3, 1, 1, CAST(N'2024-06-07T00:00:00.000' AS DateTime))
INSERT [dbo].[Order] ([ID], [Status], [UserID], [Date]) VALUES (4, 1, 1, CAST(N'2022-12-02T00:00:00.000' AS DateTime))
INSERT [dbo].[Order] ([ID], [Status], [UserID], [Date]) VALUES (5, 1, 1, CAST(N'2023-05-17T00:00:00.000' AS DateTime))
INSERT [dbo].[Order] ([ID], [Status], [UserID], [Date]) VALUES (6, 1, 1, CAST(N'2024-06-07T00:00:00.000' AS DateTime))
INSERT [dbo].[Order] ([ID], [Status], [UserID], [Date]) VALUES (7, 1, 2, CAST(N'2024-07-01T00:00:00.000' AS DateTime))
INSERT [dbo].[Order] ([ID], [Status], [UserID], [Date]) VALUES (8, 1, 3, CAST(N'2023-01-22T00:00:00.000' AS DateTime))
INSERT [dbo].[Order] ([ID], [Status], [UserID], [Date]) VALUES (9, 1, 3, CAST(N'2024-07-05T00:00:00.000' AS DateTime))
INSERT [dbo].[Order] ([ID], [Status], [UserID], [Date]) VALUES (10, 0, 4, CAST(N'2023-03-20T00:00:00.000' AS DateTime))
INSERT [dbo].[Order] ([ID], [Status], [UserID], [Date]) VALUES (11, 1, 4, CAST(N'2024-03-12T00:00:00.000' AS DateTime))
INSERT [dbo].[Order] ([ID], [Status], [UserID], [Date]) VALUES (12, 1, 4, CAST(N'2024-05-14T00:00:00.000' AS DateTime))
INSERT [dbo].[Order] ([ID], [Status], [UserID], [Date]) VALUES (13, 0, 5, CAST(N'2023-09-19T00:00:00.000' AS DateTime))
INSERT [dbo].[Order] ([ID], [Status], [UserID], [Date]) VALUES (14, 1, 5, CAST(N'2023-11-10T00:00:00.000' AS DateTime))
INSERT [dbo].[Order] ([ID], [Status], [UserID], [Date]) VALUES (15, 1, 5, CAST(N'2024-04-15T00:00:00.000' AS DateTime))
INSERT [dbo].[Order] ([ID], [Status], [UserID], [Date]) VALUES (16, 1, 5, CAST(N'2024-06-12T00:00:00.000' AS DateTime))
INSERT [dbo].[Order] ([ID], [Status], [UserID], [Date]) VALUES (18, 0, 1, CAST(N'2024-11-01T15:29:32.013' AS DateTime))
SET IDENTITY_INSERT [dbo].[Order] OFF
GO
SET IDENTITY_INSERT [dbo].[Partner] ON 

INSERT [dbo].[Partner] ([ID], [Name], [TypeID], [Director], [Email], [PhoneNumber], [Address], [INN], [Rating]) VALUES (1, N'База Строитель', 1, N'Иванова Александра Ивановна', N'aleksandraivanova@ml.ru', N'493 123 45 67', N'652050, Кемеровская область, город Юрга, ул. Лесная, 15', 2222455179, 7)
INSERT [dbo].[Partner] ([ID], [Name], [TypeID], [Director], [Email], [PhoneNumber], [Address], [INN], [Rating]) VALUES (2, N'Паркет 29', 2, N'Петров Василий Петрович', N'vppetrov@vl.ru', N'987 123 56 78', N'164500, Архангельская область, город Северодвинск, ул. Строителей, 18', 3333888520, 7)
INSERT [dbo].[Partner] ([ID], [Name], [TypeID], [Director], [Email], [PhoneNumber], [Address], [INN], [Rating]) VALUES (3, N'Стройсервис', 3, N'Соловьев Андрей Николаевич', N'ansolovev@st.ru', N'812 223 32 00', N'188910, Ленинградская область, город Приморск, ул. Парковая, 21', 4440391035, 7)
INSERT [dbo].[Partner] ([ID], [Name], [TypeID], [Director], [Email], [PhoneNumber], [Address], [INN], [Rating]) VALUES (4, N'Ремонт и отделка', 4, N'Воробьева Екатерина Валерьевна', N'ekaterina.vorobeva@ml.ru', N'444 222 33 11', N'143960, Московская область, город Реутов, ул. Свободы, 51', 4442223311, 2)
INSERT [dbo].[Partner] ([ID], [Name], [TypeID], [Director], [Email], [PhoneNumber], [Address], [INN], [Rating]) VALUES (5, N'МонтажПро', 1, N'Степанов Степан Сергеевич', N'stepanov@stepan.ru', N'912 888 33 33', N'309500, Белгородская область, город Старый Оскол, ул. Рабочая, 122', 5552431140, 10)
INSERT [dbo].[Partner] ([ID], [Name], [TypeID], [Director], [Email], [PhoneNumber], [Address], [INN], [Rating]) VALUES (6, N'Стройсервис', 3, N'Соловьев Андрей Николаевич', N'ansolovev@st.ru', N'812 223 32 00', N'188910, Ленинградская область, город Приморск, ул. Парковая, 21', 8122233200, 1)
SET IDENTITY_INSERT [dbo].[Partner] OFF
GO
SET IDENTITY_INSERT [dbo].[PartnerType] ON 

INSERT [dbo].[PartnerType] ([ID], [Name]) VALUES (1, N'ЗАО')
INSERT [dbo].[PartnerType] ([ID], [Name]) VALUES (2, N'ООО')
INSERT [dbo].[PartnerType] ([ID], [Name]) VALUES (3, N'ПАО')
INSERT [dbo].[PartnerType] ([ID], [Name]) VALUES (4, N'ОАО')
SET IDENTITY_INSERT [dbo].[PartnerType] OFF
GO
INSERT [dbo].[Product] ([Article], [Name], [TypeID], [MinCostForPartner], [Logo], [Description], [PackageSize], [WeightWithPack], [WeightWithoutPack], [QualityCertificate], [StandardNumber], [Cost]) VALUES (5012543, N'Пробковое напольное клеевое покрытие 32 класс 4 мм', 4, 5450.59, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Product] ([Article], [Name], [TypeID], [MinCostForPartner], [Logo], [Description], [PackageSize], [WeightWithPack], [WeightWithoutPack], [QualityCertificate], [StandardNumber], [Cost]) VALUES (7028748, N'Ламинат Дуб серый 32 класс 8 мм с фаской', 1, 3890.41, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Product] ([Article], [Name], [TypeID], [MinCostForPartner], [Logo], [Description], [PackageSize], [WeightWithPack], [WeightWithoutPack], [QualityCertificate], [StandardNumber], [Cost]) VALUES (7750282, N'Ламинат Дуб дымчато-белый 33 класс 12 мм', 1, 1799.33, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 56.34)
INSERT [dbo].[Product] ([Article], [Name], [TypeID], [MinCostForPartner], [Logo], [Description], [PackageSize], [WeightWithPack], [WeightWithoutPack], [QualityCertificate], [StandardNumber], [Cost]) VALUES (8758385, N'Паркетная доска Ясень темный однополосная 14 мм', 3, 4456.9, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 106)
INSERT [dbo].[Product] ([Article], [Name], [TypeID], [MinCostForPartner], [Logo], [Description], [PackageSize], [WeightWithPack], [WeightWithoutPack], [QualityCertificate], [StandardNumber], [Cost]) VALUES (8858958, N'Инженерная доска Дуб Французская елка однополосная 12 мм', 3, 7330.99, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 15)
INSERT [dbo].[Product] ([Article], [Name], [TypeID], [MinCostForPartner], [Logo], [Description], [PackageSize], [WeightWithPack], [WeightWithoutPack], [QualityCertificate], [StandardNumber], [Cost]) VALUES (8858959, N'rer', 2, 1, N'', N'', N'', 51, 42, N'', 658959, 1)
GO
SET IDENTITY_INSERT [dbo].[ProductsInOrder] ON 

INSERT [dbo].[ProductsInOrder] ([ID], [OrderID], [ProductArticle], [Amount], [Cost]) VALUES (4, 1, 8758385, 15500, 0)
INSERT [dbo].[ProductsInOrder] ([ID], [OrderID], [ProductArticle], [Amount], [Cost]) VALUES (5, 2, 7750282, 12350, 0)
INSERT [dbo].[ProductsInOrder] ([ID], [OrderID], [ProductArticle], [Amount], [Cost]) VALUES (6, 3, 7028748, 37400, 0)
INSERT [dbo].[ProductsInOrder] ([ID], [OrderID], [ProductArticle], [Amount], [Cost]) VALUES (7, 4, 8858958, 35000, 0)
INSERT [dbo].[ProductsInOrder] ([ID], [OrderID], [ProductArticle], [Amount], [Cost]) VALUES (9, 5, 5012543, 1250, 0)
INSERT [dbo].[ProductsInOrder] ([ID], [OrderID], [ProductArticle], [Amount], [Cost]) VALUES (10, 6, 7750282, 1000, 0)
INSERT [dbo].[ProductsInOrder] ([ID], [OrderID], [ProductArticle], [Amount], [Cost]) VALUES (11, 7, 8758385, 7550, 0)
INSERT [dbo].[ProductsInOrder] ([ID], [OrderID], [ProductArticle], [Amount], [Cost]) VALUES (13, 8, 8758385, 7250, 0)
INSERT [dbo].[ProductsInOrder] ([ID], [OrderID], [ProductArticle], [Amount], [Cost]) VALUES (14, 9, 8858958, 2500, 0)
INSERT [dbo].[ProductsInOrder] ([ID], [OrderID], [ProductArticle], [Amount], [Cost]) VALUES (15, 10, 7028748, 59050, 0)
INSERT [dbo].[ProductsInOrder] ([ID], [OrderID], [ProductArticle], [Amount], [Cost]) VALUES (16, 11, 7750282, 37200, 0)
INSERT [dbo].[ProductsInOrder] ([ID], [OrderID], [ProductArticle], [Amount], [Cost]) VALUES (17, 12, 5012543, 4500, 0)
INSERT [dbo].[ProductsInOrder] ([ID], [OrderID], [ProductArticle], [Amount], [Cost]) VALUES (18, 13, 7750282, 50000, 0)
INSERT [dbo].[ProductsInOrder] ([ID], [OrderID], [ProductArticle], [Amount], [Cost]) VALUES (19, 14, 7028748, 670000, 0)
INSERT [dbo].[ProductsInOrder] ([ID], [OrderID], [ProductArticle], [Amount], [Cost]) VALUES (20, 15, 8758385, 35000, 0)
INSERT [dbo].[ProductsInOrder] ([ID], [OrderID], [ProductArticle], [Amount], [Cost]) VALUES (21, 16, 8858958, 25000, 0)
INSERT [dbo].[ProductsInOrder] ([ID], [OrderID], [ProductArticle], [Amount], [Cost]) VALUES (1004, 18, 7028748, 1, 3890)
SET IDENTITY_INSERT [dbo].[ProductsInOrder] OFF
GO
SET IDENTITY_INSERT [dbo].[ProductType] ON 

INSERT [dbo].[ProductType] ([ID], [Name], [Сoefficient]) VALUES (1, N'Ламинат', 2.35)
INSERT [dbo].[ProductType] ([ID], [Name], [Сoefficient]) VALUES (2, N'Массивная доска', 5.15)
INSERT [dbo].[ProductType] ([ID], [Name], [Сoefficient]) VALUES (3, N'Паркетная доска', 4.34)
INSERT [dbo].[ProductType] ([ID], [Name], [Сoefficient]) VALUES (4, N'Пробковое покрытие', 1.5)
INSERT [dbo].[ProductType] ([ID], [Name], [Сoefficient]) VALUES (5, N'Инженерная доска ', 1)
SET IDENTITY_INSERT [dbo].[ProductType] OFF
GO
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([ID], [Name]) VALUES (1, N'Админ')
INSERT [dbo].[Role] ([ID], [Name]) VALUES (2, N'Менеджер')
INSERT [dbo].[Role] ([ID], [Name]) VALUES (3, N'Мастер')
INSERT [dbo].[Role] ([ID], [Name]) VALUES (5, N'Партнёр')
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
SET IDENTITY_INSERT [dbo].[Sale] ON 

INSERT [dbo].[Sale] ([ID], [PlaceID], [UserID], [Date]) VALUES (1, 1, 22, CAST(N'2005-01-01' AS Date))
SET IDENTITY_INSERT [dbo].[Sale] OFF
GO
SET IDENTITY_INSERT [dbo].[SalePlace] ON 

INSERT [dbo].[SalePlace] ([ID], [Name], [TypeID]) VALUES (1, N'test', 1)
INSERT [dbo].[SalePlace] ([ID], [Name], [TypeID]) VALUES (2, N'Ljv', 3)
INSERT [dbo].[SalePlace] ([ID], [Name], [TypeID]) VALUES (3, N'fgfg', 2)
INSERT [dbo].[SalePlace] ([ID], [Name], [TypeID]) VALUES (4, N'rer', 1)
SET IDENTITY_INSERT [dbo].[SalePlace] OFF
GO
SET IDENTITY_INSERT [dbo].[SalePlaceType] ON 

INSERT [dbo].[SalePlaceType] ([ID], [Name]) VALUES (1, N'Розничный магазин')
INSERT [dbo].[SalePlaceType] ([ID], [Name]) VALUES (2, N'Оптовый магазин')
INSERT [dbo].[SalePlaceType] ([ID], [Name]) VALUES (3, N'Интернет магазин')
INSERT [dbo].[SalePlaceType] ([ID], [Name]) VALUES (4, N'Компания')
SET IDENTITY_INSERT [dbo].[SalePlaceType] OFF
GO
SET IDENTITY_INSERT [dbo].[Store] ON 

INSERT [dbo].[Store] ([ID], [Name]) VALUES (1, N'Main')
SET IDENTITY_INSERT [dbo].[Store] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([ID], [RoleID], [PartnerID], [Health], [FIO], [Birthday], [Passport], [HaveFamily], [Login], [Password]) VALUES (5, 1, NULL, 0, N'admin', CAST(N'2005-10-06' AS Date), N'admin', 0, N'admin', N'admin')
INSERT [dbo].[User] ([ID], [RoleID], [PartnerID], [Health], [FIO], [Birthday], [Passport], [HaveFamily], [Login], [Password]) VALUES (9, 2, NULL, 1, N'manager', CAST(N'2006-10-06' AS Date), N'234234234', 1, N'manager', N'manager')
INSERT [dbo].[User] ([ID], [RoleID], [PartnerID], [Health], [FIO], [Birthday], [Passport], [HaveFamily], [Login], [Password]) VALUES (10, 3, NULL, 1, N'master', CAST(N'2006-10-06' AS Date), N'323232323', 1, N'master', N'master')
INSERT [dbo].[User] ([ID], [RoleID], [PartnerID], [Health], [FIO], [Birthday], [Passport], [HaveFamily], [Login], [Password]) VALUES (16, 5, 1, 1, N'База Строитель', CAST(N'2001-10-01' AS Date), N'234364563', 1, N'baza', N'baza')
INSERT [dbo].[User] ([ID], [RoleID], [PartnerID], [Health], [FIO], [Birthday], [Passport], [HaveFamily], [Login], [Password]) VALUES (17, 5, 2, 1, N'Паркет 29', CAST(N'2003-01-01' AS Date), N'325435345', 1, N'parket', N'parket')
INSERT [dbo].[User] ([ID], [RoleID], [PartnerID], [Health], [FIO], [Birthday], [Passport], [HaveFamily], [Login], [Password]) VALUES (18, 5, 3, 1, N'Стройсервис', CAST(N'2003-02-02' AS Date), N'252454253', 1, N'ctroi', N'ctroi')
INSERT [dbo].[User] ([ID], [RoleID], [PartnerID], [Health], [FIO], [Birthday], [Passport], [HaveFamily], [Login], [Password]) VALUES (20, 5, 4, 1, N'Ремонт и отделка', CAST(N'2005-11-22' AS Date), N'457563634', 1, N'remont', N'remont')
INSERT [dbo].[User] ([ID], [RoleID], [PartnerID], [Health], [FIO], [Birthday], [Passport], [HaveFamily], [Login], [Password]) VALUES (22, 5, 5, 1, N'МонтажПро', CAST(N'2007-11-11' AS Date), N'314324233', 1, N'montaj', N'montaj')
INSERT [dbo].[User] ([ID], [RoleID], [PartnerID], [Health], [FIO], [Birthday], [Passport], [HaveFamily], [Login], [Password]) VALUES (23, 1, NULL, 1, N'tests', CAST(N'2009-02-06' AS Date), N'4545', 0, N'test54', NULL)
SET IDENTITY_INSERT [dbo].[User] OFF
GO
ALTER TABLE [dbo].[AuthHistory]  WITH CHECK ADD  CONSTRAINT [FK_AuthHistory_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[AuthHistory] CHECK CONSTRAINT [FK_AuthHistory_User]
GO
ALTER TABLE [dbo].[Material]  WITH CHECK ADD  CONSTRAINT [FK_Material_MaterialType] FOREIGN KEY([TypeID])
REFERENCES [dbo].[MaterialType] ([ID])
GO
ALTER TABLE [dbo].[Material] CHECK CONSTRAINT [FK_Material_MaterialType]
GO
ALTER TABLE [dbo].[Material]  WITH CHECK ADD  CONSTRAINT [FK_Material_MeasurementUnits] FOREIGN KEY([MeasurementUnitID])
REFERENCES [dbo].[MeasurementUnits] ([ID])
GO
ALTER TABLE [dbo].[Material] CHECK CONSTRAINT [FK_Material_MeasurementUnits]
GO
ALTER TABLE [dbo].[MaterialsInStore]  WITH CHECK ADD  CONSTRAINT [FK_MaterialsInStore_Material] FOREIGN KEY([MaterialID])
REFERENCES [dbo].[Material] ([ID])
GO
ALTER TABLE [dbo].[MaterialsInStore] CHECK CONSTRAINT [FK_MaterialsInStore_Material]
GO
ALTER TABLE [dbo].[MaterialsInStore]  WITH CHECK ADD  CONSTRAINT [FK_MaterialsInStore_Store] FOREIGN KEY([StoreID])
REFERENCES [dbo].[Store] ([ID])
GO
ALTER TABLE [dbo].[MaterialsInStore] CHECK CONSTRAINT [FK_MaterialsInStore_Store]
GO
ALTER TABLE [dbo].[MaterialsInSupply]  WITH CHECK ADD  CONSTRAINT [FK_MaterialsInSupply_Material] FOREIGN KEY([MaterialID])
REFERENCES [dbo].[Material] ([ID])
GO
ALTER TABLE [dbo].[MaterialsInSupply] CHECK CONSTRAINT [FK_MaterialsInSupply_Material]
GO
ALTER TABLE [dbo].[MaterialsInSupply]  WITH CHECK ADD  CONSTRAINT [FK_MaterialsInSupply_Supply] FOREIGN KEY([SupplyID])
REFERENCES [dbo].[Supply] ([ID])
GO
ALTER TABLE [dbo].[MaterialsInSupply] CHECK CONSTRAINT [FK_MaterialsInSupply_Supply]
GO
ALTER TABLE [dbo].[Partner]  WITH CHECK ADD  CONSTRAINT [FK_Partner_PartnerType] FOREIGN KEY([TypeID])
REFERENCES [dbo].[PartnerType] ([ID])
GO
ALTER TABLE [dbo].[Partner] CHECK CONSTRAINT [FK_Partner_PartnerType]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_MaterialType] FOREIGN KEY([TypeID])
REFERENCES [dbo].[ProductType] ([ID])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_MaterialType]
GO
ALTER TABLE [dbo].[ProductsInOrder]  WITH CHECK ADD  CONSTRAINT [FK_ProductsInOrder_Order] FOREIGN KEY([OrderID])
REFERENCES [dbo].[Order] ([ID])
GO
ALTER TABLE [dbo].[ProductsInOrder] CHECK CONSTRAINT [FK_ProductsInOrder_Order]
GO
ALTER TABLE [dbo].[ProductsInOrder]  WITH CHECK ADD  CONSTRAINT [FK_ProductsInOrder_Product] FOREIGN KEY([ProductArticle])
REFERENCES [dbo].[Product] ([Article])
GO
ALTER TABLE [dbo].[ProductsInOrder] CHECK CONSTRAINT [FK_ProductsInOrder_Product]
GO
ALTER TABLE [dbo].[ProductsInSale]  WITH CHECK ADD  CONSTRAINT [FK_ProductsInSale_Product] FOREIGN KEY([ProductArticle])
REFERENCES [dbo].[Product] ([Article])
GO
ALTER TABLE [dbo].[ProductsInSale] CHECK CONSTRAINT [FK_ProductsInSale_Product]
GO
ALTER TABLE [dbo].[RequiredMaterials]  WITH CHECK ADD  CONSTRAINT [FK_RequiredMaterials_Material] FOREIGN KEY([MaterialID])
REFERENCES [dbo].[Material] ([ID])
GO
ALTER TABLE [dbo].[RequiredMaterials] CHECK CONSTRAINT [FK_RequiredMaterials_Material]
GO
ALTER TABLE [dbo].[Sale]  WITH CHECK ADD  CONSTRAINT [FK_Sale_SalePlace] FOREIGN KEY([PlaceID])
REFERENCES [dbo].[SalePlace] ([ID])
GO
ALTER TABLE [dbo].[Sale] CHECK CONSTRAINT [FK_Sale_SalePlace]
GO
ALTER TABLE [dbo].[Sale]  WITH CHECK ADD  CONSTRAINT [FK_Sale_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Sale] CHECK CONSTRAINT [FK_Sale_User]
GO
ALTER TABLE [dbo].[SalePlace]  WITH CHECK ADD  CONSTRAINT [FK_SalePlace_SalePlaceType] FOREIGN KEY([TypeID])
REFERENCES [dbo].[SalePlaceType] ([ID])
GO
ALTER TABLE [dbo].[SalePlace] CHECK CONSTRAINT [FK_SalePlace_SalePlaceType]
GO
ALTER TABLE [dbo].[Supply]  WITH CHECK ADD  CONSTRAINT [FK_Supply_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Supply] CHECK CONSTRAINT [FK_Supply_User]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Partner] FOREIGN KEY([PartnerID])
REFERENCES [dbo].[Partner] ([ID])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Partner]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Role] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Role] ([ID])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Role]
GO
USE [master]
GO
ALTER DATABASE [Materia] SET  READ_WRITE 
GO
