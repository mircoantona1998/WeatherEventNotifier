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
create database `Telegram`;
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
