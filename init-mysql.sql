-- MySQL dump 10.13  Distrib 8.0.33, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: ConfigurationAlert
-- ------------------------------------------------------
-- Server version	8.2.0

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `ConfigurationUser`
--
use `ConfigurationAlert`;
DROP TABLE IF EXISTS `ConfigurationUser`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ConfigurationUser` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `IdUser` int DEFAULT NULL,
  `IdFrequency` int DEFAULT NULL,
  `Longitude` float DEFAULT NULL,
  `Latitude` float DEFAULT NULL,
  `DateTimeCreate` datetime DEFAULT NULL,
  `DateTimeUpdate` datetime DEFAULT NULL,
  `DateTimeActivation` datetime DEFAULT NULL,
  `IsActive` tinyint(1) DEFAULT NULL,
  `IdMetric` int DEFAULT NULL,
  `Symbol` char(2) DEFAULT NULL,
  `Value` float DEFAULT NULL,
  `NameConfiguration` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `idMetric_fk_idx` (`IdMetric`),
  KEY `idFrequency_fk_idx` (`IdFrequency`),
  CONSTRAINT `idFrequency_fk` FOREIGN KEY (`IdFrequency`) REFERENCES `Frequency` (`Id`),
  CONSTRAINT `idMetric_fk` FOREIGN KEY (`IdMetric`) REFERENCES `Metric` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3438 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ConfigurationUser`
--

LOCK TABLES `ConfigurationUser` WRITE;
/*!40000 ALTER TABLE `ConfigurationUser` DISABLE KEYS */;
/*!40000 ALTER TABLE `ConfigurationUser` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Frequency`
--

DROP TABLE IF EXISTS `Frequency`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Frequency` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `FrequencyName` varchar(100) DEFAULT NULL,
  `Minutes` int DEFAULT NULL,
  `IsActive` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=20 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Frequency`
--

LOCK TABLES `Frequency` WRITE;
/*!40000 ALTER TABLE `Frequency` DISABLE KEYS */;
INSERT INTO `Frequency` VALUES (13,'ogni ora',60,1),(14,'ogni 2 ore',120,1),(15,'ogni 4 ore',240,1),(16,'ogni 8 ore',480,1),(17,'ogni 12 ore',720,1),(18,'ogni giorno',1440,1);
/*!40000 ALTER TABLE `Frequency` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `MessageReceived`
--

DROP TABLE IF EXISTS `MessageReceived`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `MessageReceived` (
  `id` int NOT NULL AUTO_INCREMENT,
  `message` longtext,
  `offset` int DEFAULT NULL,
  `timestamp` datetime DEFAULT NULL,
  `type` varchar(50) DEFAULT NULL,
  `idOffsetResponse` int DEFAULT NULL,
  `tagMessage` varchar(50) DEFAULT NULL,
  `topic` varchar(50) DEFAULT NULL,
  `creator` varchar(500) DEFAULT NULL,
  `code` varchar(20) DEFAULT NULL,
  `partition` int DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=1275 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `MessageReceived`
--

LOCK TABLES `MessageReceived` WRITE;
/*!40000 ALTER TABLE `MessageReceived` DISABLE KEYS */;
/*!40000 ALTER TABLE `MessageReceived` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `MessageSent`
--

DROP TABLE IF EXISTS `MessageSent`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `MessageSent` (
  `id` int NOT NULL AUTO_INCREMENT,
  `message` longtext,
  `offset` int DEFAULT NULL,
  `timestamp` datetime DEFAULT NULL,
  `type` varchar(50) DEFAULT NULL,
  `idOffsetResponse` int DEFAULT NULL,
  `tagMessage` varchar(50) DEFAULT NULL,
  `topic` varchar(50) DEFAULT NULL,
  `creator` varchar(500) DEFAULT NULL,
  `code` varchar(20) DEFAULT NULL,
  `partition` int DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=1171 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `MessageSent`
--

LOCK TABLES `MessageSent` WRITE;
/*!40000 ALTER TABLE `MessageSent` DISABLE KEYS */;
/*!40000 ALTER TABLE `MessageSent` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Metric`
--

DROP TABLE IF EXISTS `Metric`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Metric` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Field` varchar(50) DEFAULT NULL,
  `ValueUnit` varchar(50) DEFAULT NULL,
  `Type` varchar(45) DEFAULT NULL,
  `IsActive` tinyint(1) DEFAULT NULL,
  `Parent` varchar(50) DEFAULT NULL,
  `Description` varchar(200) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=23 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Metric`
--

LOCK TABLES `Metric` WRITE;
/*!40000 ALTER TABLE `Metric` DISABLE KEYS */;
INSERT INTO `Metric` VALUES (6,'temp','K','Hourly',1,'main','Temperatura corrente in Kelvin'),(7,'feels_like','K','Hourly',1,'main','Temperatura percepita in Kelvin'),(8,'temp_min','K','Hourly',1,'main','Temperatura minima prevista in Kelvin'),(10,'temp_max','K','Hourly',1,'main','Temperatura massima prevista in Kelvin'),(11,'pressure','hPa','Hourly',1,'main','Pressione atmosferica al livello del mare in hPa'),(12,'sea_level','hPa','Hourly',1,'main','Pressione atmosferica al livello del mare in hPa (se disponibile)'),(13,'grnd_level','hPa','Hourly',1,'main','Pressione atmosferica al livello del suolo in hPa (se disponibile)'),(14,'humidity','%','Hourly',1,'main','Percentuale di umidità nell\'aria'),(15,'temp_kf',NULL,'Hourly',1,'main','Cambiamento di temperatura nelle ultime 3 ore'),(16,'speed','m/s','Hourly',1,'wind','Velocità del vento in metri al secondo'),(17,'deg','°','Hourly',1,'wind','Direzione del vento in gradi'),(18,'gust','m/s','Hourly',1,'wind','Velocità delle raffiche di vento in metri al secondo'),(19,'all','%','Hourly',1,'clouds','Percentuale di copertura nuvolosa'),(20,'visibility','m','Hourly',1,NULL,'Visibilità in metri'),(21,'pop','%','Hourly',1,NULL,'Probabilità di precipitazione, espressa come frazione'),(22,'3h',NULL,'Hourly',1,'rain','Volume di pioggia previsto nelle prossime 3 ore');
/*!40000 ALTER TABLE `Metric` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Temporary view structure for view `View_ConfigurationUser`
--

DROP TABLE IF EXISTS `View_ConfigurationUser`;
/*!50001 DROP VIEW IF EXISTS `View_ConfigurationUser`*/;
SET @saved_cs_client     = @@character_set_client;
/*!50503 SET character_set_client = utf8mb4 */;
/*!50001 CREATE VIEW `View_ConfigurationUser` AS SELECT 
 1 AS `Id`,
 1 AS `NameConfiguration`,
 1 AS `IdUser`,
 1 AS `Symbol`,
 1 AS `Value`,
 1 AS `IdFrequency`,
 1 AS `Longitude`,
 1 AS `Latitude`,
 1 AS `DateTimeCreate`,
 1 AS `DateTimeUpdate`,
 1 AS `DateTimeActivation`,
 1 AS `IsActive`,
 1 AS `IdMetric`,
 1 AS `FrequencyName`,
 1 AS `Minutes`,
 1 AS `FrequencyIsActive`,
 1 AS `Field`,
 1 AS `Description`,
 1 AS `Parent`,
 1 AS `ValueUnit`,
 1 AS `Type`,
 1 AS `MetricIsActive`*/;
SET character_set_client = @saved_cs_client;

--
-- Dumping events for database 'ConfigurationAlert'
--

--
-- Dumping routines for database 'ConfigurationAlert'
--

--
-- Final view structure for view `View_ConfigurationUser`
--

/*!50001 DROP VIEW IF EXISTS `View_ConfigurationUser`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8mb4 */;
/*!50001 SET character_set_results     = utf8mb4 */;
/*!50001 SET collation_connection      = utf8mb4_0900_ai_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`%` SQL SECURITY DEFINER */
/*!50001 VIEW `View_ConfigurationUser` AS select `cu`.`Id` AS `Id`,`cu`.`NameConfiguration` AS `NameConfiguration`,`cu`.`IdUser` AS `IdUser`,`cu`.`Symbol` AS `Symbol`,`cu`.`Value` AS `Value`,`cu`.`IdFrequency` AS `IdFrequency`,`cu`.`Longitude` AS `Longitude`,`cu`.`Latitude` AS `Latitude`,`cu`.`DateTimeCreate` AS `DateTimeCreate`,`cu`.`DateTimeUpdate` AS `DateTimeUpdate`,`cu`.`DateTimeActivation` AS `DateTimeActivation`,`cu`.`IsActive` AS `IsActive`,`cu`.`IdMetric` AS `IdMetric`,`f`.`FrequencyName` AS `FrequencyName`,`f`.`Minutes` AS `Minutes`,`f`.`IsActive` AS `FrequencyIsActive`,`m`.`Field` AS `Field`,`m`.`Description` AS `Description`,`m`.`Parent` AS `Parent`,`m`.`ValueUnit` AS `ValueUnit`,`m`.`Type` AS `Type`,`m`.`IsActive` AS `MetricIsActive` from ((`ConfigurationUser` `cu` join `Frequency` `f` on((`cu`.`IdFrequency` = `f`.`Id`))) join `Metric` `m` on((`cu`.`IdMetric` = `m`.`Id`))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-01-05 12:24:28
-- MySQL dump 10.13  Distrib 8.0.33, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: Telegram
-- ------------------------------------------------------
-- Server version	8.2.0

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `MessageReceived`
--
use `Telegram`;
DROP TABLE IF EXISTS `MessageReceived`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `MessageReceived` (
  `id` int NOT NULL AUTO_INCREMENT,
  `message` longtext,
  `offset` int DEFAULT NULL,
  `timestamp` datetime DEFAULT NULL,
  `type` varchar(50) DEFAULT NULL,
  `idOffsetResponse` int DEFAULT NULL,
  `tagMessage` varchar(50) DEFAULT NULL,
  `topic` varchar(50) DEFAULT NULL,
  `creator` varchar(500) DEFAULT NULL,
  `code` varchar(20) DEFAULT NULL,
  `partition` int DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=1749 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `MessageReceived`
--

LOCK TABLES `MessageReceived` WRITE;
/*!40000 ALTER TABLE `MessageReceived` DISABLE KEYS */;
/*!40000 ALTER TABLE `MessageReceived` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `MessageSent`
--

DROP TABLE IF EXISTS `MessageSent`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `MessageSent` (
  `id` int NOT NULL AUTO_INCREMENT,
  `message` longtext,
  `offset` int DEFAULT NULL,
  `timestamp` datetime DEFAULT NULL,
  `type` varchar(50) DEFAULT NULL,
  `idOffsetResponse` int DEFAULT NULL,
  `tagMessage` varchar(50) DEFAULT NULL,
  `topic` varchar(50) DEFAULT NULL,
  `creator` varchar(500) DEFAULT NULL,
  `code` varchar(20) DEFAULT NULL,
  `partition` int DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=1782 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `MessageSent`
--

LOCK TABLES `MessageSent` WRITE;
/*!40000 ALTER TABLE `MessageSent` DISABLE KEYS */;
/*!40000 ALTER TABLE `MessageSent` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `TelegramConfiguration`
--

DROP TABLE IF EXISTS `TelegramConfiguration`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `TelegramConfiguration` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `bot` varchar(50) DEFAULT NULL,
  `token` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `TelegramConfiguration`
--

LOCK TABLES `TelegramConfiguration` WRITE;
/*!40000 ALTER TABLE `TelegramConfiguration` DISABLE KEYS */;
INSERT INTO `TelegramConfiguration` VALUES (1,'WeatherEventNotifier','6783949622:AAHVzLeQ_WV_vx5YZAk8jJeNPe6fl64U2Zg');
/*!40000 ALTER TABLE `TelegramConfiguration` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `TelegramMessages`
--

DROP TABLE IF EXISTS `TelegramMessages`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `TelegramMessages` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `IdChat` longtext,
  `Testo` longtext,
  `Allegati` tinyint(1) DEFAULT NULL,
  `DateCreate` datetime DEFAULT NULL,
  `WasSent` tinyint(1) DEFAULT NULL,
  `Result` longtext,
  `IdUser` int NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=27 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `TelegramMessages`
--

LOCK TABLES `TelegramMessages` WRITE;
/*!40000 ALTER TABLE `TelegramMessages` DISABLE KEYS */;
/*!40000 ALTER TABLE `TelegramMessages` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `TelegramUsers`
--

DROP TABLE IF EXISTS `TelegramUsers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `TelegramUsers` (
  `id` int NOT NULL AUTO_INCREMENT,
  `idUser` int NOT NULL,
  `chat_id` char(10) NOT NULL,
  `isActive` varchar(45) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `TelegramUsers`
--

LOCK TABLES `TelegramUsers` WRITE;
/*!40000 ALTER TABLE `TelegramUsers` DISABLE KEYS */;
/*!40000 ALTER TABLE `TelegramUsers` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping events for database 'Telegram'
--

--
-- Dumping routines for database 'Telegram'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-01-05 12:24:28
-- MySQL dump 10.13  Distrib 8.0.33, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: Notifier
-- ------------------------------------------------------
-- Server version	8.2.0

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `MessageReceived`
--
use `Notifier`;
DROP TABLE IF EXISTS `MessageReceived`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `MessageReceived` (
  `id` int NOT NULL AUTO_INCREMENT,
  `message` longtext,
  `offset` int DEFAULT NULL,
  `timestamp` datetime DEFAULT NULL,
  `type` varchar(50) DEFAULT NULL,
  `idOffsetResponse` int DEFAULT NULL,
  `tagMessage` varchar(50) DEFAULT NULL,
  `topic` varchar(50) DEFAULT NULL,
  `creator` varchar(500) DEFAULT NULL,
  `code` varchar(20) DEFAULT NULL,
  `partition` int DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5043 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `MessageReceived`
--

LOCK TABLES `MessageReceived` WRITE;
/*!40000 ALTER TABLE `MessageReceived` DISABLE KEYS */;
/*!40000 ALTER TABLE `MessageReceived` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `MessageSent`
--

DROP TABLE IF EXISTS `MessageSent`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `MessageSent` (
  `id` int NOT NULL AUTO_INCREMENT,
  `message` longtext,
  `offset` int DEFAULT NULL,
  `timestamp` datetime DEFAULT NULL,
  `type` varchar(50) DEFAULT NULL,
  `idOffsetResponse` int DEFAULT NULL,
  `tagMessage` varchar(50) DEFAULT NULL,
  `topic` varchar(50) DEFAULT NULL,
  `creator` varchar(500) DEFAULT NULL,
  `code` varchar(20) DEFAULT NULL,
  `partition` int DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4937 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `MessageSent`
--

LOCK TABLES `MessageSent` WRITE;
/*!40000 ALTER TABLE `MessageSent` DISABLE KEYS */;
/*!40000 ALTER TABLE `MessageSent` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Notify`
--

DROP TABLE IF EXISTS `Notify`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Notify` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `IdUser` int DEFAULT NULL,
  `IdSchedule` int DEFAULT NULL,
  `Message` text,
  `DateTimeCreate` datetime DEFAULT NULL,
  `IdConfiguration` int DEFAULT NULL,
  `ValueWeather` float DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3887 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Notify`
--

LOCK TABLES `Notify` WRITE;
/*!40000 ALTER TABLE `Notify` DISABLE KEYS */;
/*!40000 ALTER TABLE `Notify` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping events for database 'Notifier'
--

--
-- Dumping routines for database 'Notifier'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-01-05 12:24:28
-- MySQL dump 10.13  Distrib 8.0.33, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: Weather
-- ------------------------------------------------------
-- Server version	8.2.0

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `Key`
--
use `Weather`;
DROP TABLE IF EXISTS `Key`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Key` (
  `id` int NOT NULL AUTO_INCREMENT,
  `apiKey` varchar(500) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Key`
--

LOCK TABLES `Key` WRITE;
/*!40000 ALTER TABLE `Key` DISABLE KEYS */;
INSERT INTO `Key` VALUES (1,'7922142ac1c5839b29f90140be5565ec');
/*!40000 ALTER TABLE `Key` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `MessageReceived`
--

DROP TABLE IF EXISTS `MessageReceived`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `MessageReceived` (
  `id` int NOT NULL AUTO_INCREMENT,
  `message` longtext,
  `offset` int DEFAULT NULL,
  `timestamp` datetime DEFAULT NULL,
  `type` varchar(50) DEFAULT NULL,
  `idOffsetResponse` int DEFAULT NULL,
  `tagMessage` varchar(50) DEFAULT NULL,
  `topic` varchar(50) DEFAULT NULL,
  `creator` varchar(500) DEFAULT NULL,
  `code` varchar(20) DEFAULT NULL,
  `partition` int DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=838 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `MessageReceived`
--

LOCK TABLES `MessageReceived` WRITE;
/*!40000 ALTER TABLE `MessageReceived` DISABLE KEYS */;
/*!40000 ALTER TABLE `MessageReceived` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `MessageSent`
--

DROP TABLE IF EXISTS `MessageSent`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `MessageSent` (
  `id` int NOT NULL AUTO_INCREMENT,
  `message` longtext,
  `offset` int DEFAULT NULL,
  `timestamp` datetime DEFAULT NULL,
  `type` varchar(50) DEFAULT NULL,
  `idOffsetResponse` int DEFAULT NULL,
  `tagMessage` varchar(50) DEFAULT NULL,
  `topic` varchar(50) DEFAULT NULL,
  `creator` varchar(500) DEFAULT NULL,
  `code` varchar(20) DEFAULT NULL,
  `partition` int DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=757 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `MessageSent`
--

LOCK TABLES `MessageSent` WRITE;
/*!40000 ALTER TABLE `MessageSent` DISABLE KEYS */;
/*!40000 ALTER TABLE `MessageSent` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping events for database 'Weather'
--

--
-- Dumping routines for database 'Weather'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-01-05 12:24:28
-- MySQL dump 10.13  Distrib 8.0.33, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: Scheduler
-- ------------------------------------------------------
-- Server version	8.2.0

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `MessageReceived`
--
use `Scheduler`;
DROP TABLE IF EXISTS `MessageReceived`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `MessageReceived` (
  `id` int NOT NULL AUTO_INCREMENT,
  `message` longtext,
  `offset` int DEFAULT NULL,
  `timestamp` datetime DEFAULT NULL,
  `type` varchar(50) DEFAULT NULL,
  `idOffsetResponse` int DEFAULT NULL,
  `tagMessage` varchar(50) DEFAULT NULL,
  `topic` varchar(50) DEFAULT NULL,
  `creator` varchar(500) DEFAULT NULL,
  `code` varchar(20) DEFAULT NULL,
  `partition` int DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=1278 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `MessageReceived`
--

LOCK TABLES `MessageReceived` WRITE;
/*!40000 ALTER TABLE `MessageReceived` DISABLE KEYS */;
/*!40000 ALTER TABLE `MessageReceived` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `MessageSent`
--

DROP TABLE IF EXISTS `MessageSent`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `MessageSent` (
  `id` int NOT NULL AUTO_INCREMENT,
  `message` longtext,
  `offset` int DEFAULT NULL,
  `timestamp` datetime DEFAULT NULL,
  `type` varchar(50) DEFAULT NULL,
  `idOffsetResponse` int DEFAULT NULL,
  `tagMessage` varchar(50) DEFAULT NULL,
  `topic` varchar(50) DEFAULT NULL,
  `creator` varchar(500) DEFAULT NULL,
  `code` varchar(20) DEFAULT NULL,
  `partition` int DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3761 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `MessageSent`
--

LOCK TABLES `MessageSent` WRITE;
/*!40000 ALTER TABLE `MessageSent` DISABLE KEYS */;
/*!40000 ALTER TABLE `MessageSent` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `RequestNotification`
--

DROP TABLE IF EXISTS `RequestNotification`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `RequestNotification` (
  `idRequestNotification` int NOT NULL AUTO_INCREMENT,
  `datetime` datetime DEFAULT NULL,
  PRIMARY KEY (`idRequestNotification`)
) ENGINE=InnoDB AUTO_INCREMENT=2261 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `RequestNotification`
--

LOCK TABLES `RequestNotification` WRITE;
/*!40000 ALTER TABLE `RequestNotification` DISABLE KEYS */;
/*!40000 ALTER TABLE `RequestNotification` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `RequestSchedulation`
--

DROP TABLE IF EXISTS `RequestSchedulation`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `RequestSchedulation` (
  `idRequestSchedulation` int NOT NULL AUTO_INCREMENT,
  `date` date DEFAULT NULL,
  PRIMARY KEY (`idRequestSchedulation`)
) ENGINE=InnoDB AUTO_INCREMENT=44 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `RequestSchedulation`
--

LOCK TABLES `RequestSchedulation` WRITE;
/*!40000 ALTER TABLE `RequestSchedulation` DISABLE KEYS */;
/*!40000 ALTER TABLE `RequestSchedulation` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ResponseSchedulation`
--

DROP TABLE IF EXISTS `ResponseSchedulation`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ResponseSchedulation` (
  `idResponseSchedulation` int NOT NULL AUTO_INCREMENT,
  `date` date DEFAULT NULL,
  PRIMARY KEY (`idResponseSchedulation`)
) ENGINE=InnoDB AUTO_INCREMENT=100 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ResponseSchedulation`
--

LOCK TABLES `ResponseSchedulation` WRITE;
/*!40000 ALTER TABLE `ResponseSchedulation` DISABLE KEYS */;
/*!40000 ALTER TABLE `ResponseSchedulation` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Schedule`
--

DROP TABLE IF EXISTS `Schedule`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Schedule` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `IdConfiguration` int DEFAULT NULL,
  `DateTimeToSchedule` datetime DEFAULT NULL,
  `FieldMetric` varchar(50) DEFAULT NULL,
  `Symbol` char(2) DEFAULT NULL,
  `Value` float DEFAULT NULL,
  `IdUser` int DEFAULT NULL,
  `Latitude` float DEFAULT NULL,
  `Longitude` float DEFAULT NULL,
  `ParentMetric` varchar(50) DEFAULT NULL,
  `ConfigurationName` varchar(100) DEFAULT NULL,
  `ValueUnit` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=28194 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Schedule`
--

LOCK TABLES `Schedule` WRITE;
/*!40000 ALTER TABLE `Schedule` DISABLE KEYS */;
/*!40000 ALTER TABLE `Schedule` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping events for database 'Scheduler'
--

--
-- Dumping routines for database 'Scheduler'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-01-05 12:24:28
-- MySQL dump 10.13  Distrib 8.0.33, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: Mail
-- ------------------------------------------------------
-- Server version	8.2.0

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `Mail`
--
use `Mail`;
DROP TABLE IF EXISTS `Mail`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `Mail` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Mittente` varchar(100) DEFAULT NULL,
  `Destinatario` longtext,
  `Oggetto` longtext,
  `Testo` longtext,
  `Allegati` tinyint(1) DEFAULT NULL,
  `DateCreate` datetime DEFAULT NULL,
  `WasSent` tinyint(1) DEFAULT NULL,
  `Result` longtext,
  `IdUser` int NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=40 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Mail`
--

LOCK TABLES `Mail` WRITE;
/*!40000 ALTER TABLE `Mail` DISABLE KEYS */;
INSERT INTO `Mail` VALUES (2,'weathereventnotifier@gmail.com','mircoantona1998@libero.it','Weather Event Notifier','Attenzione per la configurazione. Valore misurato: 25.19',0,'2024-01-05 10:36:44',1,'Ok',1002),(3,'weathereventnotifier@gmail.com','mircoantona1998@libero.it','Weather Event Notifier','Attenzione per la configurazione. Valore misurato: 25.19',0,'2024-01-05 10:37:39',1,'Ok',1002),(4,'weathereventnotifier@gmail.com','mircoantona1998@libero.it','Weather Event Notifier','Attenzione per la configurazione. Valore misurato: 25.19',0,'2024-01-05 10:37:46',1,'Ok',1002),(5,'weathereventnotifier@gmail.com','mircoantona1998@libero.it','Weather Event Notifier','Attenzione per la configurazione. Valore misurato: 25.19',0,'2024-01-05 10:37:48',1,'Ok',1002),(6,'weathereventnotifier@gmail.com','mircoantona1998@libero.it','Weather Event Notifier','Attenzione per la configurazione. Valore misurato: 25.19',0,'2024-01-05 10:37:50',1,'Ok',1002),(7,'weathereventnotifier@gmail.com','mircoantona1998@libero.it','Weather Event Notifier','Attenzione per la configurazione. Valore misurato: 25.19',0,'2024-01-05 10:37:52',1,'Ok',1002),(8,'weathereventnotifier@gmail.com','mircoantona1998@libero.it','Weather Event Notifier','Attenzione per la configurazione. Valore misurato: 25.19',0,'2024-01-05 10:37:54',1,'Ok',1002),(9,'weathereventnotifier@gmail.com','mircoantona1998@libero.it','Weather Event Notifier','Attenzione per la configurazione. Valore misurato: 25.19',0,'2024-01-05 10:37:56',1,'Ok',1002),(10,'weathereventnotifier@gmail.com','mircoantona1998@libero.it','Weather Event Notifier','Attenzione per la configurazione. Valore misurato: 25.19',0,'2024-01-05 10:37:58',1,'Ok',1002),(11,'weathereventnotifier@gmail.com','mircoantona1998@libero.it','Weather Event Notifier','Attenzione per la configurazione. Valore misurato: 25.19',0,'2024-01-05 10:38:00',1,'Ok',1002),(12,'weathereventnotifier@gmail.com','mircoantona1998@libero.it','Weather Event Notifier','Attenzione per la configurazione. Valore misurato: 25.19',0,'2024-01-05 10:38:01',1,'Ok',1002),(13,'weathereventnotifier@gmail.com','mircoantona1998@libero.it','Weather Event Notifier','Attenzione per la configurazione. Valore misurato: 25.19',0,'2024-01-05 10:38:03',1,'Ok',1002),(14,'weathereventnotifier@gmail.com','mircoantona1998@libero.it','Weather Event Notifier','Attenzione per la configurazione. Valore misurato: 25.19',0,'2024-01-05 10:38:04',1,'Ok',1002),(15,'weathereventnotifier@gmail.com','mircoantona1998@libero.it','Weather Event Notifier','Attenzione per la configurazione. Valore misurato: 25.19',0,'2024-01-05 10:38:06',1,'Ok',1002),(16,'weathereventnotifier@gmail.com','mircoantona1998@libero.it','Weather Event Notifier','Attenzione per la configurazione. Valore misurato: 25.19',0,'2024-01-05 10:38:08',1,'Ok',1002),(17,'weathereventnotifier@gmail.com','mircoantona1998@libero.it','Weather Event Notifier','Attenzione per la configurazione. Valore misurato: 25.19',0,'2024-01-05 10:38:09',1,'Ok',1002),(18,'weathereventnotifier@gmail.com','mircoantona1998@libero.it','Weather Event Notifier','Attenzione per la configurazione. Valore misurato: 25.19',0,'2024-01-05 10:38:11',1,'Ok',1002),(19,'weathereventnotifier@gmail.com','mircoantona1998@libero.it','Weather Event Notifier','Attenzione per la configurazione. Valore misurato: 25.19',0,'2024-01-05 10:38:13',1,'Ok',1002),(20,'weathereventnotifier@gmail.com','mircoantona1998@libero.it','Weather Event Notifier','Attenzione per la configurazione. Valore misurato: 25.19',0,'2024-01-05 10:38:14',1,'Ok',1002),(21,'weathereventnotifier@gmail.com','mircoantona1998@libero.it','Weather Event Notifier','Attenzione per la configurazione. Valore misurato: 25.19',0,'2024-01-05 10:38:16',1,'Ok',1002),(22,'weathereventnotifier@gmail.com','mircoantona1998@libero.it','Weather Event Notifier','Attenzione per la configurazione. Valore misurato: 25.19',0,'2024-01-05 10:38:18',1,'Ok',1002),(23,'weathereventnotifier@gmail.com','mircoantona1998@libero.it','Weather Event Notifier','Attenzione per la configurazione. Valore misurato: 25.19',0,'2024-01-05 10:38:19',1,'Ok',1002),(24,'weathereventnotifier@gmail.com','mircoantona1998@libero.it','Weather Event Notifier','Attenzione per la configurazione. Valore misurato: 25.19',0,'2024-01-05 10:38:21',1,'Ok',1002),(25,'weathereventnotifier@gmail.com','mircoantona1998@libero.it','Weather Event Notifier','Attenzione per la configurazione. Valore misurato: 25.19',0,'2024-01-05 10:38:22',1,'Ok',1002),(26,'weathereventnotifier@gmail.com','mircoantona1998@libero.it','Weather Event Notifier','Attenzione per la configurazione. Valore misurato: 25.19',0,'2024-01-05 10:38:24',1,'Ok',1002),(27,'weathereventnotifier@gmail.com','mircoantona1998@libero.it','Weather Event Notifier','Attenzione per la configurazione. Valore misurato: 25.19',0,'2024-01-05 10:38:26',1,'Ok',1002),(28,'weathereventnotifier@gmail.com','mircoantona1998@libero.it','Weather Event Notifier','Attenzione per la configurazione. Valore misurato: 25.19',0,'2024-01-05 10:38:27',1,'Ok',1002),(29,'weathereventnotifier@gmail.com','mircoantona1998@libero.it','Weather Event Notifier','Attenzione per la configurazione. Valore misurato: 25.19',0,'2024-01-05 10:38:29',1,'Ok',1002),(30,'weathereventnotifier@gmail.com','mircoantona1998@libero.it','Weather Event Notifier','Attenzione per la configurazione. Valore misurato: 25.19',0,'2024-01-05 10:38:31',1,'Ok',1002),(31,'weathereventnotifier@gmail.com','mircoantona1998@libero.it','Weather Event Notifier','Attenzione per la configurazione. Valore misurato: 25.19',0,'2024-01-05 10:38:33',1,'Ok',1002),(32,'weathereventnotifier@gmail.com','mircoantona1998@libero.it','Weather Event Notifier','Attenzione per la configurazione. Valore misurato: 25.19',0,'2024-01-05 10:38:34',1,'Ok',1002),(33,'weathereventnotifier@gmail.com','mircoantona1998@libero.it','Weather Event Notifier','Attenzione per la configurazione. Valore misurato: 25.19',0,'2024-01-05 10:38:36',1,'Ok',1002),(34,'weathereventnotifier@gmail.com','mircoantona1998@libero.it','Weather Event Notifier','Attenzione per la configurazione. Valore misurato: 25.19',0,'2024-01-05 10:38:38',1,'Ok',1002),(35,'weathereventnotifier@gmail.com','mircoantona1998@libero.it','Weather Event Notifier','Attenzione per la configurazione. Valore misurato: 25.19',0,'2024-01-05 10:38:39',1,'Ok',1002),(36,'weathereventnotifier@gmail.com','mircoantona1998@libero.it','Weather Event Notifier','Attenzione per la configurazione. Valore misurato: 25.19',0,'2024-01-05 10:38:41',1,'Ok',1002),(37,'weathereventnotifier@gmail.com','mircoantona1998@libero.it','Weather Event Notifier','Attenzione per la configurazione. Valore misurato: 25.19',0,'2024-01-05 10:38:43',1,'Ok',1002),(38,'weathereventnotifier@gmail.com','mircoantona1998@libero.it','Weather Event Notifier','Attenzione per la configurazione. Valore misurato: 25.19',0,'2024-01-05 10:38:45',1,'Ok',1002),(39,'weathereventnotifier@gmail.com','mircoantona1998@libero.it','Weather Event Notifier','Attenzione per la configurazione. Valore misurato: 25.19',0,'2024-01-05 10:38:46',1,'Ok',1002);
/*!40000 ALTER TABLE `Mail` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `MailConfiguration`
--

DROP TABLE IF EXISTS `MailConfiguration`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `MailConfiguration` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `mail` varchar(50) DEFAULT NULL,
  `name` varchar(50) DEFAULT NULL,
  `password` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `MailConfiguration`
--

LOCK TABLES `MailConfiguration` WRITE;
/*!40000 ALTER TABLE `MailConfiguration` DISABLE KEYS */;
INSERT INTO `MailConfiguration` VALUES (1,'weathereventnotifier@gmail.com','weathereventnotifier@gmail.com','wmjc xkok dutx xmkk');
/*!40000 ALTER TABLE `MailConfiguration` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `MailUsers`
--

DROP TABLE IF EXISTS `MailUsers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `MailUsers` (
  `id` int NOT NULL AUTO_INCREMENT,
  `idUser` int NOT NULL,
  `mail` varchar(500) NOT NULL,
  `isActive` tinyint NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `MailUsers`
--

LOCK TABLES `MailUsers` WRITE;
/*!40000 ALTER TABLE `MailUsers` DISABLE KEYS */;
INSERT INTO `MailUsers` VALUES (2,1002,'mircoantona1998@libero.it',1);
/*!40000 ALTER TABLE `MailUsers` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `MessageReceived`
--

DROP TABLE IF EXISTS `MessageReceived`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `MessageReceived` (
  `id` int NOT NULL AUTO_INCREMENT,
  `message` longtext,
  `offset` int DEFAULT NULL,
  `timestamp` datetime DEFAULT NULL,
  `type` varchar(50) DEFAULT NULL,
  `idOffsetResponse` int DEFAULT NULL,
  `tagMessage` varchar(50) DEFAULT NULL,
  `topic` varchar(50) DEFAULT NULL,
  `creator` varchar(500) DEFAULT NULL,
  `code` varchar(20) DEFAULT NULL,
  `partition` int DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=1520 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `MessageReceived`
--

LOCK TABLES `MessageReceived` WRITE;
/*!40000 ALTER TABLE `MessageReceived` DISABLE KEYS */;
INSERT INTO `MessageReceived` VALUES (1468,'{\r\n  \"IdUser\": 1002\r\n}',0,'2024-01-05 09:42:58','Request',-1,'GetUserMail','topic_to_mail','ExposeAPIService','Ok',0),(1469,'{\r\n  \"IdUser\": 1002,\r\n  \"Mail\": \"mircoantona1998@libero.it\"\r\n}',1,'2024-01-05 09:43:16','Request',-1,'AddUserMail','topic_to_mail','ExposeAPIService','Ok',0),(1470,'{\r\n  \"IdUser\": 1002,\r\n  \"Mail\": \"mircoantona1998@libero.it\"\r\n}',2,'2024-01-05 09:44:20','Request',-1,'AddUserMail','topic_to_mail','ExposeAPIService','Ok',0),(1471,'{\r\n  \"IdUser\": 1002\r\n}',3,'2024-01-05 09:44:24','Request',-1,'GetUserMail','topic_to_mail','ExposeAPIService','Ok',0),(1472,'{\r\n  \"IdUser\": 1002,\r\n  \"Mail\": \"mircoantona1998@libero.it\"\r\n}',4,'2024-01-05 09:44:29','Request',-1,'AddUserMail','topic_to_mail','ExposeAPIService','Ok',0),(1473,'{\r\n  \"IdUser\": 1002\r\n}',5,'2024-01-05 09:44:32','Request',-1,'GetUserMail','topic_to_mail','ExposeAPIService','Ok',0),(1474,'{\n  \"Data\": {\n    \"IdUser\": 1002,\n    \"IdSchedule\": 28179,\n    \"Message\": \"Attenzione per la configurazione. Valore misurato: 25.19\",\n    \"DateTimeCreate\": \"2024-01-05 10:00:03\",\n    \"IdConfiguration\": 3437,\n    \"ValueWeather\": 25.19\n  }\n}',6,'2024-01-05 10:00:03','Request',-1,'NewTip','topic_to_mail','NotifierService','Ok',0),(1475,'{\n  \"Data\": {\n    \"IdUser\": 1002,\n    \"IdSchedule\": 28179,\n    \"Message\": \"Attenzione per la configurazione. Valore misurato: 25.19\",\n    \"DateTimeCreate\": \"2024-01-05 10:00:04\",\n    \"IdConfiguration\": 3437,\n    \"ValueWeather\": 25.19\n  }\n}',7,'2024-01-05 10:14:26','Request',-1,'NewTip','topic_to_mail','NotifierService','Ok',0),(1476,'{\n  \"Data\": {\n    \"IdUser\": 1002,\n    \"IdSchedule\": 28179,\n    \"Message\": \"Attenzione per la configurazione. Valore misurato: 25.19\",\n    \"DateTimeCreate\": \"2024-01-05 10:00:06\",\n    \"IdConfiguration\": 3437,\n    \"ValueWeather\": 25.19\n  }\n}',8,'2024-01-05 10:15:30','Request',-1,'NewTip','topic_to_mail','NotifierService','Ok',0),(1477,'{\n  \"Data\": {\n    \"IdUser\": 1002,\n    \"IdSchedule\": 28179,\n    \"Message\": \"Attenzione per la configurazione. Valore misurato: 25.19\",\n    \"DateTimeCreate\": \"2024-01-05 10:00:07\",\n    \"IdConfiguration\": 3437,\n    \"ValueWeather\": 25.19\n  }\n}',9,'2024-01-05 10:17:37','Request',-1,'NewTip','topic_to_mail','NotifierService','Ok',0),(1478,'{\n  \"Data\": {\n    \"IdUser\": 1002,\n    \"IdSchedule\": 28179,\n    \"Message\": \"Attenzione per la configurazione. Valore misurato: 25.19\",\n    \"DateTimeCreate\": \"2024-01-05 10:00:08\",\n    \"IdConfiguration\": 3437,\n    \"ValueWeather\": 25.19\n  }\n}',10,'2024-01-05 10:25:02','Request',-1,'NewTip','topic_to_mail','NotifierService','Ok',0),(1479,'{\n  \"Data\": {\n    \"IdUser\": 1002,\n    \"IdSchedule\": 28179,\n    \"Message\": \"Attenzione per la configurazione. Valore misurato: 25.19\",\n    \"DateTimeCreate\": \"2024-01-05 10:00:10\",\n    \"IdConfiguration\": 3437,\n    \"ValueWeather\": 25.19\n  }\n}',11,'2024-01-05 10:26:45','Request',-1,'NewTip','topic_to_mail','NotifierService','Ok',0),(1480,'{\n  \"Data\": {\n    \"IdUser\": 1002,\n    \"IdSchedule\": 28179,\n    \"Message\": \"Attenzione per la configurazione. Valore misurato: 25.19\",\n    \"DateTimeCreate\": \"2024-01-05 10:00:14\",\n    \"IdConfiguration\": 3437,\n    \"ValueWeather\": 25.19\n  }\n}',12,'2024-01-05 10:33:09','Request',-1,'NewTip','topic_to_mail','NotifierService','Ok',0),(1481,'{\n  \"Data\": {\n    \"IdUser\": 1002,\n    \"IdSchedule\": 28179,\n    \"Message\": \"Attenzione per la configurazione. Valore misurato: 25.19\",\n    \"DateTimeCreate\": \"2024-01-05 10:00:15\",\n    \"IdConfiguration\": 3437,\n    \"ValueWeather\": 25.19\n  }\n}',13,'2024-01-05 10:36:30','Request',-1,'NewTip','topic_to_mail','NotifierService','Ok',0),(1482,'{\n  \"Data\": {\n    \"IdUser\": 1002,\n    \"IdSchedule\": 28179,\n    \"Message\": \"Attenzione per la configurazione. Valore misurato: 25.19\",\n    \"DateTimeCreate\": \"2024-01-05 10:00:16\",\n    \"IdConfiguration\": 3437,\n    \"ValueWeather\": 25.19\n  }\n}',14,'2024-01-05 10:36:49','Request',-1,'NewTip','topic_to_mail','NotifierService','Ok',0),(1483,'{\n  \"Data\": {\n    \"IdUser\": 1002,\n    \"IdSchedule\": 28179,\n    \"Message\": \"Attenzione per la configurazione. Valore misurato: 25.19\",\n    \"DateTimeCreate\": \"2024-01-05 10:00:18\",\n    \"IdConfiguration\": 3437,\n    \"ValueWeather\": 25.19\n  }\n}',15,'2024-01-05 10:37:39','Request',-1,'NewTip','topic_to_mail','NotifierService','Ok',0),(1484,'{\n  \"Data\": {\n    \"IdUser\": 1002,\n    \"IdSchedule\": 28179,\n    \"Message\": \"Attenzione per la configurazione. Valore misurato: 25.19\",\n    \"DateTimeCreate\": \"2024-01-05 10:00:19\",\n    \"IdConfiguration\": 3437,\n    \"ValueWeather\": 25.19\n  }\n}',16,'2024-01-05 10:37:47','Request',-1,'NewTip','topic_to_mail','NotifierService','Ok',0),(1485,'{\n  \"Data\": {\n    \"IdUser\": 1002,\n    \"IdSchedule\": 28179,\n    \"Message\": \"Attenzione per la configurazione. Valore misurato: 25.19\",\n    \"DateTimeCreate\": \"2024-01-05 10:00:20\",\n    \"IdConfiguration\": 3437,\n    \"ValueWeather\": 25.19\n  }\n}',17,'2024-01-05 10:37:48','Request',-1,'NewTip','topic_to_mail','NotifierService','Ok',0),(1486,'{\n  \"Data\": {\n    \"IdUser\": 1002,\n    \"IdSchedule\": 28179,\n    \"Message\": \"Attenzione per la configurazione. Valore misurato: 25.19\",\n    \"DateTimeCreate\": \"2024-01-05 10:00:21\",\n    \"IdConfiguration\": 3437,\n    \"ValueWeather\": 25.19\n  }\n}',18,'2024-01-05 10:37:50','Request',-1,'NewTip','topic_to_mail','NotifierService','Ok',0),(1487,'{\n  \"Data\": {\n    \"IdUser\": 1002,\n    \"IdSchedule\": 28179,\n    \"Message\": \"Attenzione per la configurazione. Valore misurato: 25.19\",\n    \"DateTimeCreate\": \"2024-01-05 10:00:22\",\n    \"IdConfiguration\": 3437,\n    \"ValueWeather\": 25.19\n  }\n}',19,'2024-01-05 10:37:52','Request',-1,'NewTip','topic_to_mail','NotifierService','Ok',0),(1488,'{\n  \"Data\": {\n    \"IdUser\": 1002,\n    \"IdSchedule\": 28179,\n    \"Message\": \"Attenzione per la configurazione. Valore misurato: 25.19\",\n    \"DateTimeCreate\": \"2024-01-05 10:00:23\",\n    \"IdConfiguration\": 3437,\n    \"ValueWeather\": 25.19\n  }\n}',20,'2024-01-05 10:37:54','Request',-1,'NewTip','topic_to_mail','NotifierService','Ok',0),(1489,'{\n  \"Data\": {\n    \"IdUser\": 1002,\n    \"IdSchedule\": 28179,\n    \"Message\": \"Attenzione per la configurazione. Valore misurato: 25.19\",\n    \"DateTimeCreate\": \"2024-01-05 10:00:24\",\n    \"IdConfiguration\": 3437,\n    \"ValueWeather\": 25.19\n  }\n}',21,'2024-01-05 10:37:56','Request',-1,'NewTip','topic_to_mail','NotifierService','Ok',0),(1490,'{\n  \"Data\": {\n    \"IdUser\": 1002,\n    \"IdSchedule\": 28179,\n    \"Message\": \"Attenzione per la configurazione. Valore misurato: 25.19\",\n    \"DateTimeCreate\": \"2024-01-05 10:00:26\",\n    \"IdConfiguration\": 3437,\n    \"ValueWeather\": 25.19\n  }\n}',22,'2024-01-05 10:37:58','Request',-1,'NewTip','topic_to_mail','NotifierService','Ok',0),(1491,'{\n  \"Data\": {\n    \"IdUser\": 1002,\n    \"IdSchedule\": 28179,\n    \"Message\": \"Attenzione per la configurazione. Valore misurato: 25.19\",\n    \"DateTimeCreate\": \"2024-01-05 10:00:27\",\n    \"IdConfiguration\": 3437,\n    \"ValueWeather\": 25.19\n  }\n}',23,'2024-01-05 10:38:00','Request',-1,'NewTip','topic_to_mail','NotifierService','Ok',0),(1492,'{\n  \"Data\": {\n    \"IdUser\": 1002,\n    \"IdSchedule\": 28179,\n    \"Message\": \"Attenzione per la configurazione. Valore misurato: 25.19\",\n    \"DateTimeCreate\": \"2024-01-05 10:00:28\",\n    \"IdConfiguration\": 3437,\n    \"ValueWeather\": 25.19\n  }\n}',24,'2024-01-05 10:38:01','Request',-1,'NewTip','topic_to_mail','NotifierService','Ok',0),(1493,'{\n  \"Data\": {\n    \"IdUser\": 1002,\n    \"IdSchedule\": 28179,\n    \"Message\": \"Attenzione per la configurazione. Valore misurato: 25.19\",\n    \"DateTimeCreate\": \"2024-01-05 10:00:29\",\n    \"IdConfiguration\": 3437,\n    \"ValueWeather\": 25.19\n  }\n}',25,'2024-01-05 10:38:03','Request',-1,'NewTip','topic_to_mail','NotifierService','Ok',0),(1494,'{\n  \"Data\": {\n    \"IdUser\": 1002,\n    \"IdSchedule\": 28179,\n    \"Message\": \"Attenzione per la configurazione. Valore misurato: 25.19\",\n    \"DateTimeCreate\": \"2024-01-05 10:00:31\",\n    \"IdConfiguration\": 3437,\n    \"ValueWeather\": 25.19\n  }\n}',26,'2024-01-05 10:38:04','Request',-1,'NewTip','topic_to_mail','NotifierService','Ok',0),(1495,'{\n  \"Data\": {\n    \"IdUser\": 1002,\n    \"IdSchedule\": 28179,\n    \"Message\": \"Attenzione per la configurazione. Valore misurato: 25.19\",\n    \"DateTimeCreate\": \"2024-01-05 10:00:32\",\n    \"IdConfiguration\": 3437,\n    \"ValueWeather\": 25.19\n  }\n}',27,'2024-01-05 10:38:06','Request',-1,'NewTip','topic_to_mail','NotifierService','Ok',0),(1496,'{\n  \"Data\": {\n    \"IdUser\": 1002,\n    \"IdSchedule\": 28179,\n    \"Message\": \"Attenzione per la configurazione. Valore misurato: 25.19\",\n    \"DateTimeCreate\": \"2024-01-05 10:00:32\",\n    \"IdConfiguration\": 3437,\n    \"ValueWeather\": 25.19\n  }\n}',28,'2024-01-05 10:38:08','Request',-1,'NewTip','topic_to_mail','NotifierService','Ok',0),(1497,'{\n  \"Data\": {\n    \"IdUser\": 1002,\n    \"IdSchedule\": 28179,\n    \"Message\": \"Attenzione per la configurazione. Valore misurato: 25.19\",\n    \"DateTimeCreate\": \"2024-01-05 10:00:33\",\n    \"IdConfiguration\": 3437,\n    \"ValueWeather\": 25.19\n  }\n}',29,'2024-01-05 10:38:09','Request',-1,'NewTip','topic_to_mail','NotifierService','Ok',0),(1498,'{\n  \"Data\": {\n    \"IdUser\": 1002,\n    \"IdSchedule\": 28179,\n    \"Message\": \"Attenzione per la configurazione. Valore misurato: 25.19\",\n    \"DateTimeCreate\": \"2024-01-05 10:00:34\",\n    \"IdConfiguration\": 3437,\n    \"ValueWeather\": 25.19\n  }\n}',30,'2024-01-05 10:38:11','Request',-1,'NewTip','topic_to_mail','NotifierService','Ok',0),(1499,'{\n  \"Data\": {\n    \"IdUser\": 1002,\n    \"IdSchedule\": 28179,\n    \"Message\": \"Attenzione per la configurazione. Valore misurato: 25.19\",\n    \"DateTimeCreate\": \"2024-01-05 10:00:36\",\n    \"IdConfiguration\": 3437,\n    \"ValueWeather\": 25.19\n  }\n}',31,'2024-01-05 10:38:13','Request',-1,'NewTip','topic_to_mail','NotifierService','Ok',0),(1500,'{\n  \"Data\": {\n    \"IdUser\": 1002,\n    \"IdSchedule\": 28179,\n    \"Message\": \"Attenzione per la configurazione. Valore misurato: 25.19\",\n    \"DateTimeCreate\": \"2024-01-05 10:00:37\",\n    \"IdConfiguration\": 3437,\n    \"ValueWeather\": 25.19\n  }\n}',32,'2024-01-05 10:38:14','Request',-1,'NewTip','topic_to_mail','NotifierService','Ok',0),(1501,'{\n  \"Data\": {\n    \"IdUser\": 1002,\n    \"IdSchedule\": 28179,\n    \"Message\": \"Attenzione per la configurazione. Valore misurato: 25.19\",\n    \"DateTimeCreate\": \"2024-01-05 10:00:37\",\n    \"IdConfiguration\": 3437,\n    \"ValueWeather\": 25.19\n  }\n}',33,'2024-01-05 10:38:16','Request',-1,'NewTip','topic_to_mail','NotifierService','Ok',0),(1502,'{\n  \"Data\": {\n    \"IdUser\": 1002,\n    \"IdSchedule\": 28179,\n    \"Message\": \"Attenzione per la configurazione. Valore misurato: 25.19\",\n    \"DateTimeCreate\": \"2024-01-05 10:00:38\",\n    \"IdConfiguration\": 3437,\n    \"ValueWeather\": 25.19\n  }\n}',34,'2024-01-05 10:38:18','Request',-1,'NewTip','topic_to_mail','NotifierService','Ok',0),(1503,'{\n  \"Data\": {\n    \"IdUser\": 1002,\n    \"IdSchedule\": 28179,\n    \"Message\": \"Attenzione per la configurazione. Valore misurato: 25.19\",\n    \"DateTimeCreate\": \"2024-01-05 10:00:39\",\n    \"IdConfiguration\": 3437,\n    \"ValueWeather\": 25.19\n  }\n}',35,'2024-01-05 10:38:19','Request',-1,'NewTip','topic_to_mail','NotifierService','Ok',0),(1504,'{\n  \"Data\": {\n    \"IdUser\": 1002,\n    \"IdSchedule\": 28179,\n    \"Message\": \"Attenzione per la configurazione. Valore misurato: 25.19\",\n    \"DateTimeCreate\": \"2024-01-05 10:00:41\",\n    \"IdConfiguration\": 3437,\n    \"ValueWeather\": 25.19\n  }\n}',36,'2024-01-05 10:38:21','Request',-1,'NewTip','topic_to_mail','NotifierService','Ok',0),(1505,'{\n  \"Data\": {\n    \"IdUser\": 1002,\n    \"IdSchedule\": 28179,\n    \"Message\": \"Attenzione per la configurazione. Valore misurato: 25.19\",\n    \"DateTimeCreate\": \"2024-01-05 10:00:42\",\n    \"IdConfiguration\": 3437,\n    \"ValueWeather\": 25.19\n  }\n}',37,'2024-01-05 10:38:23','Request',-1,'NewTip','topic_to_mail','NotifierService','Ok',0),(1506,'{\n  \"Data\": {\n    \"IdUser\": 1002,\n    \"IdSchedule\": 28179,\n    \"Message\": \"Attenzione per la configurazione. Valore misurato: 25.19\",\n    \"DateTimeCreate\": \"2024-01-05 10:00:43\",\n    \"IdConfiguration\": 3437,\n    \"ValueWeather\": 25.19\n  }\n}',38,'2024-01-05 10:38:24','Request',-1,'NewTip','topic_to_mail','NotifierService','Ok',0),(1507,'{\n  \"Data\": {\n    \"IdUser\": 1002,\n    \"IdSchedule\": 28179,\n    \"Message\": \"Attenzione per la configurazione. Valore misurato: 25.19\",\n    \"DateTimeCreate\": \"2024-01-05 10:00:44\",\n    \"IdConfiguration\": 3437,\n    \"ValueWeather\": 25.19\n  }\n}',39,'2024-01-05 10:38:26','Request',-1,'NewTip','topic_to_mail','NotifierService','Ok',0),(1508,'{\n  \"Data\": {\n    \"IdUser\": 1002,\n    \"IdSchedule\": 28179,\n    \"Message\": \"Attenzione per la configurazione. Valore misurato: 25.19\",\n    \"DateTimeCreate\": \"2024-01-05 10:00:45\",\n    \"IdConfiguration\": 3437,\n    \"ValueWeather\": 25.19\n  }\n}',40,'2024-01-05 10:38:28','Request',-1,'NewTip','topic_to_mail','NotifierService','Ok',0),(1509,'{\n  \"Data\": {\n    \"IdUser\": 1002,\n    \"IdSchedule\": 28179,\n    \"Message\": \"Attenzione per la configurazione. Valore misurato: 25.19\",\n    \"DateTimeCreate\": \"2024-01-05 10:00:46\",\n    \"IdConfiguration\": 3437,\n    \"ValueWeather\": 25.19\n  }\n}',41,'2024-01-05 10:38:29','Request',-1,'NewTip','topic_to_mail','NotifierService','Ok',0),(1510,'{\n  \"Data\": {\n    \"IdUser\": 1002,\n    \"IdSchedule\": 28179,\n    \"Message\": \"Attenzione per la configurazione. Valore misurato: 25.19\",\n    \"DateTimeCreate\": \"2024-01-05 10:00:47\",\n    \"IdConfiguration\": 3437,\n    \"ValueWeather\": 25.19\n  }\n}',42,'2024-01-05 10:38:31','Request',-1,'NewTip','topic_to_mail','NotifierService','Ok',0),(1511,'{\n  \"Data\": {\n    \"IdUser\": 1002,\n    \"IdSchedule\": 28179,\n    \"Message\": \"Attenzione per la configurazione. Valore misurato: 25.19\",\n    \"DateTimeCreate\": \"2024-01-05 10:00:49\",\n    \"IdConfiguration\": 3437,\n    \"ValueWeather\": 25.19\n  }\n}',43,'2024-01-05 10:38:33','Request',-1,'NewTip','topic_to_mail','NotifierService','Ok',0),(1512,'{\n  \"Data\": {\n    \"IdUser\": 1002,\n    \"IdSchedule\": 28179,\n    \"Message\": \"Attenzione per la configurazione. Valore misurato: 25.19\",\n    \"DateTimeCreate\": \"2024-01-05 10:00:51\",\n    \"IdConfiguration\": 3437,\n    \"ValueWeather\": 25.19\n  }\n}',44,'2024-01-05 10:38:34','Request',-1,'NewTip','topic_to_mail','NotifierService','Ok',0),(1513,'{\n  \"Data\": {\n    \"IdUser\": 1002,\n    \"IdSchedule\": 28179,\n    \"Message\": \"Attenzione per la configurazione. Valore misurato: 25.19\",\n    \"DateTimeCreate\": \"2024-01-05 10:00:51\",\n    \"IdConfiguration\": 3437,\n    \"ValueWeather\": 25.19\n  }\n}',45,'2024-01-05 10:38:36','Request',-1,'NewTip','topic_to_mail','NotifierService','Ok',0),(1514,'{\n  \"Data\": {\n    \"IdUser\": 1002,\n    \"IdSchedule\": 28179,\n    \"Message\": \"Attenzione per la configurazione. Valore misurato: 25.19\",\n    \"DateTimeCreate\": \"2024-01-05 10:00:52\",\n    \"IdConfiguration\": 3437,\n    \"ValueWeather\": 25.19\n  }\n}',46,'2024-01-05 10:38:38','Request',-1,'NewTip','topic_to_mail','NotifierService','Ok',0),(1515,'{\n  \"Data\": {\n    \"IdUser\": 1002,\n    \"IdSchedule\": 28179,\n    \"Message\": \"Attenzione per la configurazione. Valore misurato: 25.19\",\n    \"DateTimeCreate\": \"2024-01-05 10:00:54\",\n    \"IdConfiguration\": 3437,\n    \"ValueWeather\": 25.19\n  }\n}',47,'2024-01-05 10:38:40','Request',-1,'NewTip','topic_to_mail','NotifierService','Ok',0),(1516,'{\n  \"Data\": {\n    \"IdUser\": 1002,\n    \"IdSchedule\": 28179,\n    \"Message\": \"Attenzione per la configurazione. Valore misurato: 25.19\",\n    \"DateTimeCreate\": \"2024-01-05 10:00:55\",\n    \"IdConfiguration\": 3437,\n    \"ValueWeather\": 25.19\n  }\n}',48,'2024-01-05 10:38:41','Request',-1,'NewTip','topic_to_mail','NotifierService','Ok',0),(1517,'{\n  \"Data\": {\n    \"IdUser\": 1002,\n    \"IdSchedule\": 28179,\n    \"Message\": \"Attenzione per la configurazione. Valore misurato: 25.19\",\n    \"DateTimeCreate\": \"2024-01-05 10:00:56\",\n    \"IdConfiguration\": 3437,\n    \"ValueWeather\": 25.19\n  }\n}',49,'2024-01-05 10:38:43','Request',-1,'NewTip','topic_to_mail','NotifierService','Ok',0),(1518,'{\n  \"Data\": {\n    \"IdUser\": 1002,\n    \"IdSchedule\": 28179,\n    \"Message\": \"Attenzione per la configurazione. Valore misurato: 25.19\",\n    \"DateTimeCreate\": \"2024-01-05 10:00:57\",\n    \"IdConfiguration\": 3437,\n    \"ValueWeather\": 25.19\n  }\n}',50,'2024-01-05 10:38:45','Request',-1,'NewTip','topic_to_mail','NotifierService','Ok',0),(1519,'{\n  \"Data\": {\n    \"IdUser\": 1002,\n    \"IdSchedule\": 28179,\n    \"Message\": \"Attenzione per la configurazione. Valore misurato: 25.19\",\n    \"DateTimeCreate\": \"2024-01-05 10:00:58\",\n    \"IdConfiguration\": 3437,\n    \"ValueWeather\": 25.19\n  }\n}',51,'2024-01-05 10:38:46','Request',-1,'NewTip','topic_to_mail','NotifierService','Ok',0);
/*!40000 ALTER TABLE `MessageReceived` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `MessageSent`
--

DROP TABLE IF EXISTS `MessageSent`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `MessageSent` (
  `id` int NOT NULL AUTO_INCREMENT,
  `message` longtext,
  `offset` int DEFAULT NULL,
  `timestamp` datetime DEFAULT NULL,
  `type` varchar(50) DEFAULT NULL,
  `idOffsetResponse` int DEFAULT NULL,
  `tagMessage` varchar(50) DEFAULT NULL,
  `topic` varchar(50) DEFAULT NULL,
  `creator` varchar(500) DEFAULT NULL,
  `code` varchar(20) DEFAULT NULL,
  `partition` int DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=1545 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `MessageSent`
--

LOCK TABLES `MessageSent` WRITE;
/*!40000 ALTER TABLE `MessageSent` DISABLE KEYS */;
INSERT INTO `MessageSent` VALUES (1538,'{\n  \"Data\": []\n}',21,'2024-01-05 09:42:58','Response',0,'GetUserMail','topic_to_userdata','MailService','Ok',0),(1539,'{\'Data\': \'(MySQLdb.DataError) (1406, \"Data too long for column \\\'mail\\\' at row 1\")\\n[SQL: INSERT INTO `MailUsers` (`idUser`, mail, `isActive`) VALUES (%s, %s, %s)]\\n[parameters: (1002, \\\'mircoantona1998@libero.it\\\', True)]\\n(Background on this error at: https://sqlalche.me/e/14/9h9h)\'}',22,'2024-01-05 09:43:16','Response',1,'AddUserMail','topic_to_userdata','MailService','Error',0),(1540,'{\n  \"Data\": \"true\"\n}',23,'2024-01-05 09:44:20','Response',2,'AddUserMail','topic_to_userdata','MailService','Ok',0),(1541,'{\n  \"Data\": [\n    {\n      \"id\": 1,\n      \"mail\": \"mircoantona1998@libero.it\",\n      \"idUser\": 1002,\n      \"isActive\": true\n    }\n  ]\n}',24,'2024-01-05 09:44:24','Response',3,'GetUserMail','topic_to_userdata','MailService','Ok',0),(1542,'{\n  \"Data\": \"true\"\n}',25,'2024-01-05 09:44:30','Response',4,'AddUserMail','topic_to_userdata','MailService','Ok',0),(1543,'{\n  \"Data\": [\n    {\n      \"id\": 2,\n      \"mail\": \"mircoantona1998@libero.it\",\n      \"idUser\": 1002,\n      \"isActive\": true\n    }\n  ]\n}',26,'2024-01-05 09:44:32','Response',5,'GetUserMail','topic_to_userdata','MailService','Ok',0),(1544,'{\'Data\': \'list indices must be integers or slices, not str\'}',2431,'2024-01-05 10:15:30','Response',7,'NewTip','topic_to_notifier','MailService','Error',0);
/*!40000 ALTER TABLE `MessageSent` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping events for database 'Mail'
--

--
-- Dumping routines for database 'Mail'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-01-05 12:24:29
