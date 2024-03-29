
/****** Object:  Database [Userdata]    Script Date: 05/01/2024 19:27:23 ******/
CREATE DATABASE [Userdata] CONTAINMENT = NONE ON  PRIMARY ( NAME = N'Userdata', FILENAME = N'/var/opt/mssql/data/Userdata.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB ) LOG ON ( NAME = N'Userdata_log', FILENAME = N'/var/opt/mssql/data/Userdata_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB ) WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
USE [Userdata]
GO
ALTER DATABASE [Userdata] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Userdata].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Userdata] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Userdata] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Userdata] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Userdata] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Userdata] SET ARITHABORT OFF 
GO
ALTER DATABASE [Userdata] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Userdata] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Userdata] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Userdata] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Userdata] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Userdata] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Userdata] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Userdata] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Userdata] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Userdata] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Userdata] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Userdata] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Userdata] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Userdata] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Userdata] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Userdata] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Userdata] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Userdata] SET RECOVERY FULL 
GO
ALTER DATABASE [Userdata] SET  MULTI_USER 
GO
ALTER DATABASE [Userdata] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Userdata] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Userdata] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Userdata] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Userdata] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Userdata] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [Userdata] SET QUERY_STORE = ON
GO
ALTER DATABASE [Userdata] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [Userdata]
GO
/****** Object:  Table [dbo].[MessageReceived]    Script Date: 05/01/2024 19:27:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MessageReceived](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[message] [nvarchar](max) NULL,
	[offset] [int] NULL,
	[timestamp] [datetime] NULL,
	[type] [varchar](50) NULL,
	[idOffsetResponse] [int] NULL,
	[tagMessage] [varchar](50) NULL,
	[topic] [varchar](50) NULL,
	[creator] [varchar](200) NULL,
	[code] [varchar](20) NULL,
	[partition] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MessageSent]    Script Date: 05/01/2024 19:27:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MessageSent](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[message] [nvarchar](max) NULL,
	[offset] [int] NULL,
	[timestamp] [datetime] NULL,
	[type] [varchar](50) NULL,
	[idOffsetResponse] [int] NULL,
	[tagMessage] [varchar](50) NULL,
	[topic] [varchar](50) NULL,
	[creator] [varchar](200) NULL,
	[code] [varchar](20) NULL,
	[partition] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 05/01/2024 19:27:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](50) NULL,
	[Cognome] [varchar](50) NULL,
	[Username] [varchar](50) NULL,
	[Password] [nvarchar](max) NULL,
	[isActive] [bit] NULL,
	[DateUpdate] [datetime] NULL,
	[Address] [varchar](50) NULL,
	[Cap] [varchar](50) NULL,
	[City] [varchar](50) NULL,
	[LastAccess] [datetime] NULL,
	[isBlocked] [int] NULL,
	[Partition] [int] NULL,
	[Cluster] [int] NULL,
 CONSTRAINT [PK_users_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF__users__users__Nome__20A2B0EA]  DEFAULT (NULL) FOR [Nome]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF__users__users__Cognome__2196D523]  DEFAULT (NULL) FOR [Cognome]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF__users__users__Username__228AF95C]  DEFAULT (NULL) FOR [Username]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF__users__users__Password__237F1D95]  DEFAULT (NULL) FOR [Password]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF__users__users__isActive__247341CE]  DEFAULT (NULL) FOR [isActive]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF__users__users__DateUpdat__25676607]  DEFAULT (NULL) FOR [DateUpdate]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF__users__users__Address__265B8A40]  DEFAULT (NULL) FOR [Address]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF__users__users__Cap__274FAE79]  DEFAULT (NULL) FOR [Cap]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF__users__users__City__2843D2B2]  DEFAULT (NULL) FOR [City]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF__users__users__LastAcces__2B203F5D]  DEFAULT (NULL) FOR [LastAccess]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF__users__users__isBlocked__2C146396]  DEFAULT (NULL) FOR [isBlocked]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF__users__users__Partition__2T142312]  DEFAULT (NULL) FOR [Partition]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF__users__users__Cluster__2T142332]  DEFAULT (NULL) FOR [Partition]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'dbo.users' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Users'
GO
USE [master]
GO
ALTER DATABASE [Userdata] SET  READ_WRITE 
GO
