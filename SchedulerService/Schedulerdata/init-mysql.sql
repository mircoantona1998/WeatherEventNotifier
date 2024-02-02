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
create database `Scheduler`;
use `Scheduler`;
-- Table structure for table `HeartbeatSent`
--

DROP TABLE IF EXISTS `HeartbeatSent`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;

CREATE TABLE `HeartbeatSent` (
  `id` int NOT NULL AUTO_INCREMENT,
  `datetime` datetime DEFAULT NULL,
  `partition` int DEFAULT NULL,
  `cluster` int DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
LOCK TABLES `HeartbeatSent` WRITE;
/*!40000 ALTER TABLE `HeartbeatSent` DISABLE KEYS */;
/*!40000 ALTER TABLE `HeartbeatSent` ENABLE KEYS */;
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
-- Table structure for table `RequestNotification`
--

DROP TABLE IF EXISTS `RequestNotification`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `RequestNotification` (
  `idRequestNotification` int NOT NULL AUTO_INCREMENT,
  `datetime` datetime DEFAULT NULL,
  `partition` int DEFAULT NULL,
  `cluster` int DEFAULT NULL,
  PRIMARY KEY (`idRequestNotification`)
) ENGINE=InnoDB AUTO_INCREMENT=0 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
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
   `partition` int DEFAULT NULL,
  `cluster` int DEFAULT NULL,
  PRIMARY KEY (`idRequestSchedulation`)
) ENGINE=InnoDB AUTO_INCREMENT=0 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
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
  `partition` int DEFAULT NULL,
  `cluster` int DEFAULT NULL,
  PRIMARY KEY (`idResponseSchedulation`)
) ENGINE=InnoDB AUTO_INCREMENT=0 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
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
  `IdMetric` int DEFAULT NULL,
  `DateTimeToSchedule` datetime DEFAULT NULL,
  `FieldMetric` varchar(50) DEFAULT NULL,
  `Symbol` char(2) DEFAULT NULL,
  `Value` float DEFAULT NULL,
  `IdUser` int DEFAULT NULL,
  `Latitude` float DEFAULT NULL,
  `Longitude` float DEFAULT NULL,
  `ParentMetric` varchar(50) DEFAULT NULL,
  `ValueUnit` varchar(50) DEFAULT NULL,
  `Description` varchar(200) DEFAULT NULL,
  `NameConfiguration` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=0 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
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
