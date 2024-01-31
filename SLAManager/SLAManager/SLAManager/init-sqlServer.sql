/****** Object:  Database [SLAManager]    Script Date: 18/01/2024 20:25:46 ******/
CREATE DATABASE [SLAManager]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SLAManager', FILENAME = N'/var/opt/mssql/data/SLAManager.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SLAManager_log', FILENAME = N'/var/opt/mssql/data/SLAManager_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
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
/****** Object:  Table [dbo].[Services]    Script Date: 18/01/2024 20:25:46 ******/
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
    [Cluster] [int] NULL,
 CONSTRAINT [PK_users_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Heartbeat]    Script Date: 18/01/2024 20:25:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Heartbeat](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdService] [int] NULL,
	[Timestamp] [datetime] NULL,
 CONSTRAINT [PK_Heartbeat] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MetricData] (
    [Id] INT IDENTITY(1,1)  NOT NULL,
    [Metric_name] [varchar](255) NULL ,
    [Action] [varchar](255) NULL,
    [Code] [varchar](255) NULL,
    [Controller] [varchar](255) NULL,
    [Endpoint] [varchar](255) NULL,
    [Instance] [varchar](255) NULL,
    [Job] [varchar](255) NULL,
    [Method] [varchar](255) NULL,
    [Value1] [float] NULL,
    [Value2] [varchar](255) NULL,
	[Timestamp] [datetime] NULL,
 CONSTRAINT [PK_MetricData] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[Heartbeat_view]    Script Date: 18/01/2024 20:25:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[Heartbeat_view] AS
SELECT dbo.Services.Servicename , dbo.Heartbeat.Timestamp
FROM   dbo.Heartbeat INNER JOIN
             dbo.Services ON dbo.Heartbeat.IdService = dbo.Services.Id
GO
/****** Object:  Table [dbo].[MessageReceived]    Script Date: 18/01/2024 20:25:46 ******/
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
/****** Object:  Table [dbo].[MessageSent]    Script Date: 18/01/2024 20:25:46 ******/
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
/****** Object:  Table [dbo].[MonitoringMetric]    Script Date: 18/01/2024 20:25:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MonitoringMetric](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Metric] [varchar](100) NULL,
	[Description] [varchar](255) NULL,
 CONSTRAINT [PK_MetricMonitoring] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sla]    Script Date: 18/01/2024 20:25:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sla](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdMonitoringMetric] [int] NULL,
	[FromDesiredValue] [float] NULL,
	[ToDesiredValue] [float] NULL,
	[UpdateDatetime] [datetime] NULL,
 CONSTRAINT [PK_SLA] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SlaMetricStatus]    Script Date: 18/01/2024 20:25:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SlaMetricStatus](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdSla] [int] NULL,
	[IdStatus] [int] NULL,
	[Action] [varchar](255) NULL,
    [Code] [varchar](255) NULL,
    [Controller] [varchar](255) NULL,
    [Endpoint] [varchar](255) NULL,
    [Instance] [varchar](255) NULL,
    [Job] [varchar](255) NULL,
    [Method] [varchar](255) NULL,
	[Datetime] [datetime] NULL,
	[MisuredValue] [float] NULL,
	[FromDesiredValue] [float] NULL,
	[ToDesiredValue] [float] NULL,
 CONSTRAINT [PK_StatusMetricSla] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SlaMetricViolation]    Script Date: 18/01/2024 20:25:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SlaMetricViolation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdSla] [int] NULL,
    [Violation] [varchar](255) NULL,
	[Metric] [varchar](100) NULL,
	[MetricDescription] [varchar](255) NULL,
	[Action] [varchar](255) NULL,
    [Code] [varchar](255) NULL,
    [Controller] [varchar](255) NULL,
    [Endpoint] [varchar](255) NULL,
    [Instance] [varchar](255) NULL,
    [Job] [varchar](255) NULL,
    [Method] [varchar](255) NULL,
	[Datetime] [datetime] NULL,
	[MisuredValue] [float] NULL,
	[FromDesiredValue] [float] NULL,
	[ToDesiredValue] [float] NULL,
 CONSTRAINT [PK_ViolationMetricSla] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Status]    Script Date: 18/01/2024 20:25:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Status](
	[Id]  [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](50) NULL,
	[Description] [varchar](255) NULL,
  CONSTRAINT [PK_Status] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
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
ALTER TABLE [dbo].[Heartbeat]  WITH CHECK ADD  CONSTRAINT [FK_heartbeat_Services] FOREIGN KEY([IdService])
REFERENCES [dbo].[Services] ([Id])
GO
ALTER TABLE [dbo].[Heartbeat] CHECK CONSTRAINT [FK_heartbeat_Services]
GO
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
 CONSTRAINT [PK_users__users_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF__users__Nome__20A2B0EA]  DEFAULT (NULL) FOR [Nome]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF__users__Cognome__2196D523]  DEFAULT (NULL) FOR [Cognome]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF__users__Username__228AF95C]  DEFAULT (NULL) FOR [Username]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF__users__Password__237F1D95]  DEFAULT (NULL) FOR [Password]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF__users__isActive__247341CE]  DEFAULT (NULL) FOR [isActive]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF__users__DateUpdat__25676607]  DEFAULT (NULL) FOR [DateUpdate]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF__users__Address__265B8A40]  DEFAULT (NULL) FOR [Address]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF__users__Cap__274FAE79]  DEFAULT (NULL) FOR [Cap]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF__users__City__2843D2B2]  DEFAULT (NULL) FOR [City]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF__users__LastAcces__2B203F5D]  DEFAULT (NULL) FOR [LastAccess]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF__users__isBlocked__2C146396]  DEFAULT (NULL) FOR [isBlocked]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF__users__Partition__2T142312]  DEFAULT (NULL) FOR [Partition]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'dbo.users' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Users'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[Sla_view] AS
SELECT dbo.Sla.Id, dbo.Sla.IdMonitoringMetric, dbo.Sla.FromDesiredValue, dbo.Sla.ToDesiredValue, dbo.Sla.UpdateDatetime, dbo.MonitoringMetric.Metric, dbo.MonitoringMetric.Description
FROM   dbo.Sla INNER JOIN
             dbo.MonitoringMetric ON dbo.Sla.IdMonitoringMetric = dbo.MonitoringMetric.Id
GO
CREATE VIEW [dbo].[Sla_metric_status_view]  as 
SELECT  [dbo].[Sla].[Id] AS IdSla,
		[dbo].[Sla].[FromDesiredValue], 
		[dbo].[Sla].[ToDesiredValue],
		[dbo].[SlaMetricStatus].[MisuredValue] AS MisuredValue,
		[dbo].[MonitoringMetric].Metric, 
		[dbo].[MonitoringMetric].Description AS MetricDescription , 
		[dbo].[Status].[Code] AS StatusCode, 
		[dbo].[Status].Description AS StatusDescription, 
		[dbo].[SlaMetricStatus].datetime,
		[dbo].[SlaMetricStatus].[Action] AS Action,
		[dbo].[SlaMetricStatus].[Code] AS Code,
		[dbo].[SlaMetricStatus].[Controller] AS Controller,
		[dbo].[SlaMetricStatus].[Endpoint] AS Endpoint,
		[dbo].[SlaMetricStatus].[Instance] AS Instance,
		[dbo].[SlaMetricStatus].[Job] AS Job,
		[dbo].[SlaMetricStatus].[Method] AS Method
FROM   [dbo].[Sla] INNER JOIN
             [dbo].[MonitoringMetric] ON [dbo].[Sla].IdMonitoringMetric = [dbo].[MonitoringMetric].Id INNER JOIN
             [dbo].[SlaMetricStatus] ON [dbo].[Sla].Id = [dbo].[SlaMetricStatus].IdSla INNER JOIN
             [dbo].[Status] ON [dbo].[SlaMetricStatus].IdStatus = [dbo].[Status].Id
GO
INSERT INTO [dbo].[Status]
           ([Code]
           ,[Description])
     VALUES
           ('OK'
           ,'STATO OK'),
		   ('KO'
           ,'STATO VIOLAZIONE')
GO
INSERT INTO [dbo].[Services]
           ([Service]
           ,[Servicename]
           ,[Password]
           ,[Cluster]
           ,[Partition]
           ,[isActive]
)
     VALUES
           ('ConfiguratorService'
           ,'ConfiguratorService_cluster0_part0'
           ,'ConfiguratorService_cluster0_part0'
           ,0,0,1),
           ('ConfiguratorService'
           ,'ConfiguratorService_cluster0_part1'
           ,'ConfiguratorService_cluster0_part1'
           ,0,1,1),
           ('ConfiguratorService'
           ,'ConfiguratorService_cluster0_part2'
           ,'ConfiguratorService_cluster0_part2'
           ,0,2,1),
           ('ConfiguratorService'
           ,'ConfiguratorService_cluster1_part0'
           ,'ConfiguratorService_cluster1_part0'
           ,1,0,1),
           ('ConfiguratorService'
           ,'ConfiguratorService_cluster1_part1'
           ,'ConfiguratorService_cluster1_part1'
           ,1,1,1),
           ('ConfiguratorService'
           ,'ConfiguratorService_cluster1_part2'
           ,'ConfiguratorService_cluster1_part2'
           ,1,2,1),
           ('ConfiguratorService'
           ,'ConfiguratorService_cluster2_part0'
           ,'ConfiguratorService_cluster2_part0'
           ,2,0,1),
           ('ConfiguratorService'
           ,'ConfiguratorService_cluster2_part1'
           ,'ConfiguratorService_cluster2_part1'
           ,2,1,1),
           ('ConfiguratorService'
           ,'ConfiguratorService_cluster2_part2'
           ,'ConfiguratorService_cluster2_part2'
           ,2,2,1);
GO
INSERT INTO [dbo].[Services]
           ([Service]
           ,[Servicename]
           ,[Password]
           ,[Cluster]
           ,[Partition]
           ,[isActive]
)
     VALUES
           ('SchedulerService'
           ,'SchedulerService_cluster0_part0'
           ,'SchedulerService_cluster0_part0'
           ,0,0,1),
           ('SchedulerService'
           ,'SchedulerService_cluster0_part1'
           ,'SchedulerService_cluster0_part1'
           ,0,1,1),
           ('SchedulerService'
           ,'SchedulerService_cluster0_part2'
           ,'SchedulerService_cluster0_part2'
           ,0,2,1),
           ('SchedulerService'
           ,'SchedulerService_cluster1_part0'
           ,'SchedulerService_cluster1_part0'
           ,1,0,1),
           ('SchedulerService'
           ,'SchedulerService_cluster1_part1'
           ,'SchedulerService_cluster1_part1'
           ,1,1,1),
           ('SchedulerService'
           ,'SchedulerService_cluster1_part2'
           ,'SchedulerService_cluster1_part2'
           ,1,2,1),
           ('SchedulerService'
           ,'SchedulerService_cluster2_part0'
           ,'SchedulerService_cluster2_part0'
           ,2,0,1),
           ('SchedulerService'
           ,'SchedulerService_cluster2_part1'
           ,'SchedulerService_cluster2_part1'
           ,2,1,1),
           ('SchedulerService'
           ,'SchedulerService_cluster2_part2'
           ,'SchedulerService_cluster2_part2'
           ,2,2,1);
GO
 INSERT INTO [dbo].[Services]
           ([Service]
           ,[Servicename]
           ,[Password]
           ,[Cluster]
           ,[Partition]
           ,[isActive]
)
     VALUES
            ('WeatherService'
           ,'WeatherService_cluster0_part0'
           ,'WeatherService_cluster0_part0'
           ,0,0,1),
           ('WeatherService'
           ,'WeatherService_cluster0_part1'
           ,'WeatherService_cluster0_part1'
           ,0,1,1),
           ('WeatherService'
           ,'WeatherService_cluster0_part2'
           ,'WeatherService_cluster0_part2'
           ,0,2,1),
           ('WeatherService'
           ,'WeatherService_cluster1_part0'
           ,'WeatherService_cluster1_part0'
           ,1,0,1),
           ('WeatherService'
           ,'WeatherService_cluster1_part1'
           ,'WeatherService_cluster1_part1'
           ,1,1,1),
           ('WeatherService'
           ,'WeatherService_cluster1_part2'
           ,'WeatherService_cluster1_part2'
           ,1,2,1),
           ('WeatherService'
           ,'WeatherService_cluster2_part0'
           ,'WeatherService_cluster2_part0'
           ,2,0,1),
           ('WeatherService'
           ,'WeatherService_cluster2_part1'
           ,'WeatherService_cluster2_part1'
           ,2,1,1),
           ('WeatherService'
           ,'WeatherService_cluster2_part2'
           ,'WeatherService_cluster2_part2'
           ,2,2,1);
GO
INSERT INTO [dbo].[Services]
           ([Service]
           ,[Servicename]
           ,[Password]
           ,[Cluster]
           ,[Partition]
           ,[isActive]
)
     VALUES
            ('NotifierService'
           ,'NotifierService_cluster0_part0'
           ,'NotifierService_cluster0_part0'
           ,0,0,1),
           ('NotifierService'
           ,'NotifierService_cluster0_part1'
           ,'NotifierService_cluster0_part1'
           ,0,1,1),
           ('NotifierService'
           ,'NotifierService_cluster0_part2'
           ,'NotifierService_cluster0_part2'
           ,0,2,1),
           ('NotifierService'
           ,'NotifierService_cluster1_part0'
           ,'NotifierService_cluster1_part0'
           ,1,0,1),
           ('NotifierService'
           ,'NotifierService_cluster1_part1'
           ,'NotifierService_cluster1_part1'
           ,1,1,1),
           ('NotifierService'
           ,'NotifierService_cluster1_part2'
           ,'NotifierService_cluster1_part2'
           ,1,2,1),
           ('NotifierService'
           ,'NotifierService_cluster2_part0'
           ,'NotifierService_cluster2_part0'
           ,2,0,1),
           ('NotifierService'
           ,'NotifierService_cluster2_part1'
           ,'NotifierService_cluster2_part1'
           ,2,1,1),
           ('NotifierService'
           ,'NotifierService_cluster2_part2'
           ,'NotifierService_cluster2_part2'
           ,2,2,1);
GO
INSERT INTO [dbo].[Services]
           ([Service]
           ,[Servicename]
           ,[Password]
           ,[Cluster]
           ,[Partition]
           ,[isActive]
)
     VALUES
            ('TelegramService'
           ,'TelegramService_cluster0_part0'
           ,'TelegramService_cluster0_part0'
           ,0,0,1),
           ('TelegramService'
           ,'TelegramService_cluster0_part1'
           ,'TelegramService_cluster0_part1'
           ,0,1,1),
           ('TelegramService'
           ,'TelegramService_cluster0_part2'
           ,'TelegramService_cluster0_part2'
           ,0,2,1),
           ('TelegramService'
           ,'TelegramService_cluster1_part0'
           ,'TelegramService_cluster1_part0'
           ,1,0,1),
           ('TelegramService'
           ,'TelegramService_cluster1_part1'
           ,'TelegramService_cluster1_part1'
           ,1,1,1),
           ('TelegramService'
           ,'TelegramService_cluster1_part2'
           ,'TelegramService_cluster1_part2'
           ,1,2,1),
           ('TelegramService'
           ,'TelegramService_cluster2_part0'
           ,'TelegramService_cluster2_part0'
           ,2,0,1),
           ('TelegramService'
           ,'TelegramService_cluster2_part1'
           ,'TelegramService_cluster2_part1'
           ,2,1,1),
           ('TelegramService'
           ,'TelegramService_cluster2_part2'
           ,'TelegramService_cluster2_part2'
           ,2,2,1);
GO
INSERT INTO [dbo].[Services]
           ([Service]
           ,[Servicename]
           ,[Password]
           ,[Cluster]
           ,[Partition]
           ,[isActive]
)
     VALUES
            ('MailService'
           ,'MailService_cluster0_part0'
           ,'MailService_cluster0_part0'
           ,0,0,1),
           ('MailService'
           ,'MailService_cluster0_part1'
           ,'MailService_cluster0_part1'
           ,0,1,1),
           ('MailService'
           ,'MailService_cluster0_part2'
           ,'MailService_cluster0_part2'
           ,0,2,1),
           ('MailService'
           ,'MailService_cluster1_part0'
           ,'MailService_cluster1_part0'
           ,1,0,1),
           ('MailService'
           ,'MailService_cluster1_part1'
           ,'MailService_cluster1_part1'
           ,1,1,1),
           ('MailService'
           ,'MailService_cluster1_part2'
           ,'MailService_cluster1_part2'
           ,1,2,1),
           ('MailService'
           ,'MailService_cluster2_part0'
           ,'MailService_cluster2_part0'
           ,2,0,1),
           ('MailService'
           ,'MailService_cluster2_part1'
           ,'MailService_cluster2_part1'
           ,2,1,1),
           ('MailService'
           ,'MailService_cluster2_part2'
           ,'MailService_cluster2_part2'
           ,2,2,1);
GO
INSERT INTO [dbo].[MonitoringMetric]
           ([Metric]
           ,[Description])
     VALUES
	       ('http_request_duration_seconds_sum'
           ,'La somma della durata delle richieste HTTP. (The sum of the duration of HTTP requests)'),
           ('http_request_duration_seconds_count'
           ,'Il numero totale di richieste HTTP. (The total count of HTTP requests)'),
           ('http_request_duration_seconds_bucket'
           ,'Distribuzione della durata delle richieste in bucket di intervallo. (Distribution of the duration of requests in interval buckets)'),
           ('http_requests_received_total'
           ,'Il conteggio totale delle richieste HTTP ricevute, suddiviso per codice, metodo e altri dettagli. (Total count of received HTTP requests, categorized by code, method, and other details)'),
           ('http_requests_in_progress'
           ,'Il numero di richieste HTTP attualmente in corso, suddiviso per metodo, controller, azione e altri dettagli. (The number of HTTP requests currently in progress, categorized by method, controller, action, and other details)'),
           ('dotnet_collection_count_total'
           ,'Il conteggio totale delle raccolte del Garbage Collector, suddiviso per generazione. (Total count of Garbage Collector collections, categorized by generation)'),
           ('process_cpu_seconds_total'
           ,'Il tempo totale di CPU utilizzato dal processo. (Total CPU time used by the process)'),
           ('process_virtual_memory_bytes'
           ,'La dimensione della memoria virtuale del processo. (The size of the process virtual memory)'),
           ('process_working_set_bytes'
           ,'La quantità di memoria fisica attualmente utilizzata dal processo. (The amount of physical memory currently used by the process)'),
           ('process_private_memory_bytes'
           ,'La dimensione della memoria privata del processo. (The size of the process private memory)'),
           ('dotnet_total_memory_bytes'
           ,'La quantità totale di memoria .NET allocata. (Total amount of allocated .NET memory)'),
           ('system_runtime_gen_1_gc_budget'
           ,'Budget GC di generazione 1 in MB. (Generation 1 GC budget in MB)'),
           ('system_runtime_gen_2_gc_budget'
           ,'Budget GC di generazione 2 in MB. (Generation 2 GC budget in MB)'),
           ('system_runtime_gc_large_object_heap_size'
           ,'Dimensioni del heap degli oggetti grandi GC in MB. (Size of the GC large object heap in MB)'),
           ('system_runtime_gc_large_object_heap_size_peak'
           ,'Dimensioni di picco del heap degli oggetti grandi GC in MB. (Peak size of the GC large object heap in MB)'),
           ('system_runtime_threadpool_threads_total'
           ,'Numero totale di thread nel pool di thread. (Total number of threads in the thread pool)'),
           ('system_runtime_threadpool_threads_running'
           ,'Numero di thread attualmente in esecuzione nel pool di thread. (Number of threads currently running in the thread pool)'),
           ('system_runtime_threadpool_threads_active'
           ,'Numero di thread attualmente attivi nel pool di thread. (Number of threads currently active in the thread pool)'),
           ('system_runtime_threadpool_threads_inactive'
           ,'Numero di thread attualmente inattivi nel pool di thread. (Number of threads currently inactive in the thread pool)'),
           ('system_runtime_threadpool_threads_io'
           ,'Numero di thread I/O nel pool di thread. (Number of I/O threads in the thread pool)'),
           ('system_runtime_threadpool_threads_worker'
           ,'Numero di thread worker nel pool di thread. (Number of worker threads in the thread pool)'),
           ('system_runtime_threadpool_threads_min'
           ,'Numero minimo di thread nel pool di thread. (Minimum number of threads in the thread pool)'),
           ('system_runtime_threadpool_threads_max'
           ,'Numero massimo di thread nel pool di thread. (Maximum number of threads in the thread pool)'),
           ('system_runtime_exceptions_total'
           ,'Numero totale di eccezioni gestite e non gestite. (Total number of handled and unhandled exceptions)'),
           ('system_runtime_exceptions_unhandled_total'
           ,'Numero totale di eccezioni non gestite. (Total number of unhandled exceptions)'),
           ('system_runtime_exceptions_user_total'
           ,'Numero totale di eccezioni utente. (Total number of user exceptions)'),
           ('system_runtime_exceptions_system_total'
           ,'Numero totale di eccezioni di sistema. (Total number of system exceptions)'),
           ('system_runtime_interop_total'
           ,'Numero totale di chiamate di interoperabilità. (Total number of interop calls)'),
           ('system_runtime_interop_rate'
           ,'Tasso di chiamate di interoperabilità. (Rate of interop calls)'),
           ('system_runtime_assembly_loads_total'
           ,'Numero totale di caricamenti di assembly. (Total number of assembly loads)'),
           ('system_runtime_assembly_unloads_total'
           ,'Numero totale di scaricamenti di assembly. (Total number of assembly unloads)'),
           ('system_runtime_assembly_load_errors_total'
           ,'Numero totale di errori di caricamento dell assembly. (Total number of assembly load errors)'),
           ('system_runtime_interlocked_operations_total'
           ,'Numero totale di operazioni interlocked. (Total number of interlocked operations)'),
           ('system_runtime_memory_failures_total'
           ,'Numero totale di fallimenti di memoria. (Total number of memory failures)'),
           ('system_runtime_handle_count'
           ,'Numero di handle gestiti. (Number of managed handles)'),
           ('system_runtime_pinned_objects_total'
           ,'Numero totale di oggetti bloccati in memoria. (Total number of pinned objects in memory)'),
           ('system_runtime_locked_memory_total'
           ,'Quantità totale di memoria bloccata. (Total locked memory)'),
           ('system_runtime_page_faults_total'
           ,'Numero totale di errori di pagina. (Total number of page faults)'),
           ('system_runtime_page_file_bytes_total'
           ,'Numero totale di byte nel file di paging. (Total number of bytes in the page file)'),
           ('system_runtime_app_domains_total'
           ,'Numero totale di domini dell applicazione. (Total number of application domains)'),
           ('system_runtime_clr_exceptions_total'
           ,'Numero totale di eccezioni CLR. (Total number of CLR exceptions)'),
           ('system_runtime_clr_exceptions_rate'
           ,'Tasso di eccezioni CLR. (Rate of CLR exceptions)'),
           ('dotnet_collection_count_total'
           ,'Il conteggio delle raccolte del Garbage Collector (GC), raggruppate per generazione. (Count of GC collections, grouped by generation)'),
           ('process_cpu_seconds_total'
           ,'Il tempo totale di CPU utente e sistema speso dal processo. (Total CPU time used by the process)'),
           ('process_virtual_memory_bytes'
           ,'Dimensioni della memoria virtuale in byte. (Size of virtual memory in bytes)'),
           ('process_working_set_bytes'
           ,'Dimensioni del set di lavoro del processo. (Size of the working set of the process)'),
           ('process_private_memory_bytes'
           ,'Dimensioni della memoria privata del processo. (Size of private memory of the process)'),
           ('process_open_handles'
           ,'Numero di handle aperti. (Number of open handles)'),
           ('process_num_threads'
           ,'Numero totale di thread. (Total number of threads)'),
           ('dotnet_total_memory_bytes'
           ,'Totale della memoria allocata nota da parte del runtime .NET. (Total allocated memory known by the .NET runtime)'),
           ('prometheus_net_metric_families'
           ,'Numero di famiglie di metriche attualmente registrate, suddivise per tipo di metrica. (Number of metric families currently registered, categorized by metric type)'),
           ('prometheus_net_metric_instances'
           ,'Numero di istanze di metriche attualmente registrate tra tutte le famiglie di metriche, suddivise per tipo di metrica. (Number of metric instances currently registered across all metric families, categorized by metric type)'),
           ('prometheus_net_metric_timeseries'
           ,'Numero di serie temporali di metriche attualmente generate da tutte le istanze di metriche, suddivise per tipo di metrica. (Number of metric time series currently generated by all metric instances, categorized by metric type)'),
           ('prometheus_net_exemplars_recorded_total'
           ,'Numero di esemplari accettati nella memorizzazione in memoria nel SDK prometheus-net. (Number of exemplars accepted into in-memory storage in the prometheus-net SDK)'),
           ('prometheus_net_eventcounteradapter_sources_connected_total'
           ,'Numero di origini eventi attualmente collegate all adattatore. (Number of event sources currently connected to the adapter)'),
           ('prometheus_net_meteradapter_instruments_connected'
           ,'Numero di strumenti attualmente collegati all adattatore. (Number of instruments currently connected to the adapter)'),
           ('microsoft_aspnetcore_hosting_http_server_active_requests'
           ,'Numero di richieste HTTP attive del server. (Number of active HTTP requests to the server)'),
           ('microsoft_aspnetcore_routing_aspnetcore_routing_match_attempts'
           ,'Numero di tentativi di corrispondenza delle richieste a un endpoint. (Number of attempts to match requests to an endpoint)'),
           ('microsoft_aspnetcore_hosting_http_server_request_duration'
           ,'Durata delle richieste del server HTTP. (Duration of HTTP server requests)'),
           ('system_runtime_cpu_usage'
           ,'Utilizzo della CPU come percentuale. (CPU usage as a percentage)'),
           ('system_runtime_working_set'
           ,'Set di lavoro in MB. (Working set in MB)'),
           ('system_runtime_gc_heap_size'
           ,'Dimensioni del heap GC in MB. (GC heap size in MB)'),
           ('system_runtime_gen_0_gc_count_total'
           ,'Conteggio totale delle raccolte GC di generazione 0. (Total count of GC generation 0 collections)'),
           ('system_runtime_gen_1_gc_count_total'
           ,'Conteggio totale delle raccolte GC di generazione 1. (Total count of GC generation 1 collections)'),
           ('system_runtime_gen_2_gc_count_total'
           ,'Conteggio totale delle raccolte GC di generazione 2. (Total count of GC generation 2 collections)'),
           ('system_runtime_gen_0_gc_budget'
           ,'Budget GC di generazione 0 in MB. (Generation 0 GC budget in MB)');
GO
USE [master]
GO
ALTER DATABASE [SLAManager] SET  READ_WRITE 
GO