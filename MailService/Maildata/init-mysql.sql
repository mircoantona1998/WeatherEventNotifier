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
create database `Mail`;
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
) ENGINE=InnoDB AUTO_INCREMENT=0 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Mail`
--


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
) ENGINE=InnoDB AUTO_INCREMENT=0 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
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
) ENGINE=InnoDB AUTO_INCREMENT=0 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `MailUsers`
--


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
