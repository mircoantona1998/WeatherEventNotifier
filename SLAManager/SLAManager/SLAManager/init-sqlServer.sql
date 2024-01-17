
/****** Object:  Database [SLAManager]    Script Date: 05/01/2024 19:27:23 ******/
CREATE DATABASE [SLAManager] CONTAINMENT = NONE ON  PRIMARY ( NAME = N'SLAManager', FILENAME = N'/var/opt/mssql/data/SLAManager.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB ) LOG ON ( NAME = N'SLAManager_log', FILENAME = N'/var/opt/mssql/data/SLAManager_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB ) WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
USE [SLAManager]
GO
ALTER DATABASE [SLAManager] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SLAManager].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SLAManager] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SLAManager] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SLAManager] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SLAManager] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SLAManager] SET ARITHABORT OFF 
GO
ALTER DATABASE [SLAManager] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SLAManager] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SLAManager] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SLAManager] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SLAManager] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SLAManager] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SLAManager] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SLAManager] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SLAManager] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SLAManager] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SLAManager] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SLAManager] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SLAManager] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SLAManager] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SLAManager] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SLAManager] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SLAManager] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SLAManager] SET RECOVERY FULL 
GO
ALTER DATABASE [SLAManager] SET  MULTI_USER 
GO
ALTER DATABASE [SLAManager] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SLAManager] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SLAManager] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SLAManager] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [SLAManager] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [SLAManager] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [SLAManager] SET QUERY_STORE = ON
GO
ALTER DATABASE [SLAManager] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [SLAManager]
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
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Services](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Service] [varchar](50) NULL,
	[Servicename] [varchar](50) NULL,
	[Password] [nvarchar](max) NULL,
	[isActive] [bit] NULL,
	[DateUpdate] [datetime] NULL,
	[isBlocked] [int] NULL,
	[Partition] [int] NULL,
 CONSTRAINT [PK_users_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Services] ADD  CONSTRAINT [DF__users__Nome__20A2B0EA]  DEFAULT (NULL) FOR [Service]
GO
ALTER TABLE [dbo].[Services] ADD  CONSTRAINT [DF__users__Username__228AF95C]  DEFAULT (NULL) FOR [Servicename]
GO
ALTER TABLE [dbo].[Services] ADD  CONSTRAINT [DF__users__Password__237F1D95]  DEFAULT (NULL) FOR [Password]
GO
ALTER TABLE [dbo].[Services] ADD  CONSTRAINT [DF__users__isActive__247341CE]  DEFAULT (NULL) FOR [isActive]
GO
ALTER TABLE [dbo].[Services] ADD  CONSTRAINT [DF__users__DateUpdat__25676607]  DEFAULT (NULL) FOR [DateUpdate]
GO
ALTER TABLE [dbo].[Services] ADD  CONSTRAINT [DF__users__isBlocked__2C146396]  DEFAULT (NULL) FOR [isBlocked]
GO
ALTER TABLE [dbo].[Services] ADD  CONSTRAINT [DF__users__Partition__2T142312]  DEFAULT (NULL) FOR [Partition]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'dbo.users' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Services'
GO
ALTER TABLE dbo.Services SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Heartbeat
	(
	Id int NOT NULL IDENTITY (1, 1),
	IdService int NULL,
	Timestamp datetime NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Heartbeat ADD CONSTRAINT
	PK_Heartbeat PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.Heartbeat ADD CONSTRAINT
	FK_heartbeat_Services FOREIGN KEY
	(
	IdService
	) REFERENCES dbo.Services
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Heartbeat SET (LOCK_ESCALATION = TABLE)
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[Heartbeat_view] AS
SELECT dbo.Services.Servicename , dbo.Heartbeat.Timestamp
FROM   dbo.Heartbeat INNER JOIN
             dbo.Services ON dbo.Heartbeat.IdService = dbo.Services.Id
GO
INSERT INTO [dbo].[Services]
           ([Service]
           ,[Servicename]
           ,[Password]
           ,[Partition]
           ,[isActive]
)
     VALUES
           ('ConfiguratorService'
           ,'ConfiguratorService_part0'
           ,'ConfiguratorService_part0'
           ,0,1);
GO
INSERT INTO [dbo].[Services]
           ([Service]
           ,[Servicename]
           ,[Password]
           ,[Partition]
           ,[isActive]
)
     VALUES
           ('SchedulerService'
           ,'SchedulerService_part0'
           ,'SchedulerService_part0'
           ,0,1);
GO
 INSERT INTO [dbo].[Services]
           ([Service]
           ,[Servicename]
           ,[Password]
           ,[Partition]
           ,[isActive]
)
     VALUES
           ('WeatherService'
           ,'WeatherService_part0'
           ,'WeatherService_part0'
           ,0,1);
GO
INSERT INTO [dbo].[Services]
           ([Service]
           ,[Servicename]
           ,[Password]
           ,[Partition]
           ,[isActive]
)
     VALUES
           ('NotifierService'
           ,'NotifierService_part0'
           ,'NotifierService_part0'
           ,0,1);
GO
INSERT INTO [dbo].[Services]
           ([Service]
           ,[Servicename]
           ,[Password]
           ,[Partition]
           ,[isActive]
)
     VALUES
           ('TelegramService'
           ,'TelegramService_part0'
           ,'TelegramService_part0',0,1);
GO
INSERT INTO [dbo].[Services]
           ([Service]
           ,[Servicename]
           ,[Password]
           ,[Partition]
           ,[isActive]
)
     VALUES
           ('MailService'
           ,'MailService_part0'
           ,'MailService_part0'
           ,0
           ,1);
GO
USE [master]
GO
ALTER DATABASE [SLAManager] SET  READ_WRITE 
GO
