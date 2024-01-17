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
create database `ConfigurationAlert`;
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
) ENGINE=InnoDB AUTO_INCREMENT=0 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
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
) ENGINE=InnoDB AUTO_INCREMENT=0 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Frequency`
--

LOCK TABLES `Frequency` WRITE;
/*!40000 ALTER TABLE `Frequency` DISABLE KEYS */;
INSERT INTO `Frequency` VALUES (1,'ogni 5 minuti',5,1),(2,'ogni 10 minuti',10,1),(3,'ogni 15 minuti',15,1),(4,'ogni 30 minuti',30,1),(5,'ogni 45 minuti',45,1), (6,'ogni ora',60,1),(7,'ogni 2 ore',120,1),(8,'ogni 4 ore',240,1),(9,'ogni 6 ore',360,1),(10,'ogni 12 ore',720,1),(11,'ogni giorno',1440,1);
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
) ENGINE=InnoDB AUTO_INCREMENT=0 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `MessageReceived`
--
--
-- Table structure for table `HeartbeatSent`
--

DROP TABLE IF EXISTS `HeartbeatSent`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;


CREATE TABLE `HeartbeatSent` (
  `id` int NOT NULL AUTO_INCREMENT,
  `datetime` datetime DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=0 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

LOCK TABLES `HeartbeatSent` WRITE;
/*!40000 ALTER TABLE `HeartbeatSent` DISABLE KEYS */;
/*!40000 ALTER TABLE `HeartbeatSent` ENABLE KEYS */;
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
) ENGINE=InnoDB AUTO_INCREMENT=0 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
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
) ENGINE=InnoDB AUTO_INCREMENT=0 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Metric`
--

LOCK TABLES `Metric` WRITE;
/*!40000 ALTER TABLE `Metric` DISABLE KEYS */;
INSERT INTO `Metric` VALUES (1,'temp','°C','Hourly',1,'main','Temperatura corrente'),(2,'feels_like','°C','Hourly',1,'main','Temperatura percepita'),(3,'temp_min','°C','Hourly',1,'main','Temperatura minima prevista'),(4,'temp_max','°C','Hourly',1,'main','Temperatura massima prevista'),(5,'pressure','hPa','Hourly',1,'main','Pressione atmosferica al livello del mare in hPa'),(6,'sea_level','hPa','Hourly',1,'main','Pressione atmosferica al livello del mare in hPa (se disponibile)'),(7,'grnd_level','hPa','Hourly',1,'main','Pressione atmosferica al livello del suolo in hPa (se disponibile)'),(8,'humidity','%','Hourly',1,'main','Percentuale di umidità nell\'aria'),(9,'temp_kf',NULL,'Hourly',1,'main','Cambiamento di temperatura nelle ultime 3 ore'),(10,'speed','m/s','Hourly',1,'wind','Velocità del vento in metri al secondo'),(11,'deg','°','Hourly',1,'wind','Direzione del vento in gradi'),(12,'gust','m/s','Hourly',1,'wind','Velocità delle raffiche di vento in metri al secondo'),(13,'all','%','Hourly',1,'clouds','Percentuale di copertura nuvolosa'),(14,'visibility','m','Hourly',1,NULL,'Visibilità in metri'),(15,'pop','%','Hourly',1,NULL,'Probabilità di precipitazione, espressa come frazione'),(16,'3h',NULL,'Hourly',1,'rain','Volume di pioggia previsto nelle prossime 3 ore');
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

