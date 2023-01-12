# ************************************************************
# Sequel Pro SQL dump
# Versão 4541
#
# http://www.sequelpro.com/
# https://github.com/sequelpro/sequelpro
#
# Host: 127.0.0.1 (MySQL 5.7.34)
# Base de Dados: cartShopping
# Tempo de Geração: 2023-01-12 19:19:11 +0000
# ************************************************************


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


# Dump da tabela CartConfiguration
# ------------------------------------------------------------

DROP TABLE IF EXISTS `CartConfiguration`;

CREATE TABLE `CartConfiguration` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `EntityId` int(11) DEFAULT NULL,
  `TokenCart` varchar(100) DEFAULT NULL,
  `Status` int(11) DEFAULT NULL,
  `Created_at` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

LOCK TABLES `CartConfiguration` WRITE;
/*!40000 ALTER TABLE `CartConfiguration` DISABLE KEYS */;

INSERT INTO `CartConfiguration` (`Id`, `EntityId`, `TokenCart`, `Status`, `Created_at`)
VALUES
	(4,1,'807991939d574af4b07fd58e1496a567',0,'2023-01-11 00:00:00'),
	(6,1,'e6f2c49aadb6410b98d0f261444d6072',1,'2023-01-11 00:00:00'),
	(7,1,'35dd3fd3e32b410098c099b820ee0b90',1,'2023-01-11 00:00:00'),
	(8,3,'82a966e69735472b9ff3fd984fb04ee0',1,'2023-01-11 00:00:00'),
	(2345,1,'24a07b53d7ed4c57837ce087fa07a702',1,'2023-01-11 00:00:00'),
	(2347,1,'181c465433d948589c152de560874096',1,'2023-01-12 00:00:00');

/*!40000 ALTER TABLE `CartConfiguration` ENABLE KEYS */;
UNLOCK TABLES;


# Dump da tabela CartItem
# ------------------------------------------------------------

DROP TABLE IF EXISTS `CartItem`;

CREATE TABLE `CartItem` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `TokenCart` varchar(100) DEFAULT NULL,
  `ExternalId` int(11) DEFAULT NULL,
  `Name` varchar(100) DEFAULT NULL,
  `Price` double DEFAULT NULL,
  `Amount` int(11) DEFAULT NULL,
  `Variable` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

LOCK TABLES `CartItem` WRITE;
/*!40000 ALTER TABLE `CartItem` DISABLE KEYS */;

INSERT INTO `CartItem` (`Id`, `TokenCart`, `ExternalId`, `Name`, `Price`, `Amount`, `Variable`)
VALUES
	(3,'24a07b53d7ed4c57837ce087fa07a702',12,'sofá',23.9,2,'azul'),
	(4,'24a07b53d7ed4c57837ce087fa07a702',12,'sofá',23.9,2,'azul'),
	(6,'24a07b53d7ed4c57837ce087fa07a702',12,'sofá',23.9,2,'azul'),
	(8,'418b9a2fa050428db38f7c35d944383b',92,'Sofá',999.99,1,'Preto');

/*!40000 ALTER TABLE `CartItem` ENABLE KEYS */;
UNLOCK TABLES;


# Dump da tabela User.Api
# ------------------------------------------------------------

DROP TABLE IF EXISTS `User.Api`;

CREATE TABLE `User.Api` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `User` varchar(45) DEFAULT NULL,
  `Password` varchar(45) DEFAULT NULL,
  `Role` varchar(45) DEFAULT NULL,
  `Status` varchar(45) DEFAULT NULL,
  `Created_at` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

LOCK TABLES `User.Api` WRITE;
/*!40000 ALTER TABLE `User.Api` DISABLE KEYS */;

INSERT INTO `User.Api` (`Id`, `User`, `Password`, `Role`, `Status`, `Created_at`)
VALUES
	(1,'bruno','123','admin','1','2022-01-11 14:31:00'),
	(3,'jhon','2424','admin','1','2022-01-11 14:31:00');

/*!40000 ALTER TABLE `User.Api` ENABLE KEYS */;
UNLOCK TABLES;



/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
