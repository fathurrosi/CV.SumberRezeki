/*
SQLyog Community Edition- MySQL GUI v8.17 
MySQL - 5.5.5-10.4.14-MariaDB : Database - dbsrinternasional_v2
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
CREATE DATABASE /*!32312 IF NOT EXISTS*/`dbsrinternasional_v2` /*!40100 DEFAULT CHARACTER SET latin1 */;

USE `dbsrinternasional_v2`;

/*Table structure for table `menu` */

DROP TABLE IF EXISTS `menu`;

CREATE TABLE `menu` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Code` varchar(100) DEFAULT NULL,
  `Name` varchar(500) DEFAULT NULL,
  `ParentID` int(11) DEFAULT NULL,
  `Sequence` int(11) DEFAULT NULL,
  `Ico` blob DEFAULT NULL,
  `Description` varchar(1000) DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `CreatedBy` varchar(50) DEFAULT NULL,
  `ModifiedDate` datetime DEFAULT NULL,
  `ModifiedBy` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=52 DEFAULT CHARSET=latin1;

/*Data for the table `menu` */

insert  into `menu`(`ID`,`Code`,`Name`,`ParentID`,`Sequence`,`Ico`,`Description`,`CreatedDate`,`CreatedBy`,`ModifiedDate`,`ModifiedBy`) values (1,'File','File',0,1,NULL,'file',NULL,NULL,'2017-01-30 21:19:14','fathur'),(2,'Master','Master',0,2,NULL,'',NULL,NULL,'2017-01-30 21:19:27','fathur'),(3,'Transaction','Transaction',0,4,NULL,'',NULL,NULL,'2017-01-30 21:19:47','fathur'),(4,'report','Report',0,5,NULL,'',NULL,NULL,'2017-01-30 21:20:47','fathur'),(5,'UM','User Management',0,7,NULL,'',NULL,NULL,'2017-07-22 05:46:50','fathur'),(6,'user','User',5,10,NULL,'User',NULL,NULL,'2018-06-03 01:01:59','fathur'),(7,'role','Role',5,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(8,'Previllage','Previllage',5,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(9,'exit','Exit',1,2,NULL,'',NULL,NULL,'2016-12-26 09:33:30','fathur'),(10,'menu','Menu',5,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(11,'catalog','Catalog',2,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(12,'supplier','Supplier',2,NULL,NULL,NULL,NULL,NULL,NULL,NULL),(15,'salespoint','Sale',3,1,NULL,'Form Untuk menjual barang','2016-11-13 03:23:07','fathur','2016-12-31 23:56:12','fathur'),(16,'logout','Logout',1,3,NULL,'log out for this applicaition','2016-11-17 05:51:42','fathur','2016-12-26 09:34:50','fathur'),(18,'SaleList','List of Sale',3,2,NULL,'','2016-11-19 09:13:56','fathur','2017-01-30 21:37:31','fathur'),(19,'customer','Customer',2,5,NULL,'','2016-11-19 11:03:27','fathur',NULL,NULL),(21,'stock','Stock Detail/Update',28,1,NULL,'Stock Detail/Update','2016-11-21 21:44:49','fathur','2017-03-03 23:58:01','fathur'),(25,'salespermonth','Total Sales Per Catalog (Monthly)',34,6,NULL,'Grafik Penjualan Bulanan total','2016-12-16 00:48:19','fathur','2018-10-14 01:55:16','fathur'),(27,'lapstockdetail','Stock Detail',34,2,NULL,'Laporan Stock','2016-12-30 05:12:34','fathur','2018-10-14 01:53:17','fathur'),(28,'mstStock','Stock',0,3,NULL,'Stock','2017-03-03 22:04:04','fathur',NULL,NULL),(29,'purchase','Input Stock',28,2,NULL,'Input Stock','2017-03-03 23:57:35','fathur',NULL,NULL),(30,'salesperformancepermonth','Total Sales Performance',34,9,NULL,'Total Sales Performance','2017-03-06 22:21:34','fathur','2018-10-14 01:56:55','fathur'),(31,'polist','Stock List Per Supplier',28,3,NULL,'Stock List Per Supplier','2017-03-19 17:43:25','fathur',NULL,NULL),(32,'ar','Account Receivable',4,4,NULL,'Account Receivable','2017-04-25 12:42:36','lia','2017-07-22 23:03:07','fathur'),(33,'recon','Product Reconcile',3,5,NULL,'Product Reconcile','2017-05-01 21:58:55','fathur',NULL,NULL),(34,'neraca','Neraca',0,6,NULL,'Neraca','2017-07-22 05:42:07','fathur','2017-07-22 05:46:29','fathur'),(35,'dailysales','Daily Sales ( Per Customer)',4,1,NULL,'Daily Sales ( Per Customer)','2017-07-22 23:15:23','fathur','2017-08-01 20:47:01','fathur'),(36,'dailysalescatalog','Daily Sales ( Per Catalog)',4,2,NULL,'Daily Sales ( Per Catalog)','2017-08-01 20:47:42','fathur',NULL,NULL),(37,'lapstock','Current Stock',34,1,NULL,'Current Stock','2017-08-05 02:25:37','fathur',NULL,NULL),(38,'hpp','Harga Pokok Penjualan (HPP)',34,3,NULL,'Harga Pokok Penjualan (HPP)','2017-11-28 20:57:42','fathur','2018-10-14 01:53:30','fathur'),(39,'genhpp','Generate HPP',3,6,NULL,'Generate HPP','2018-01-21 18:33:12','fathur',NULL,NULL),(40,'rppc','Item Purchased Per Month',34,8,NULL,'Item Purchased Per Month','2018-01-24 20:59:49','fathur','2018-10-14 01:56:42','fathur'),(41,'frmTSCM','Total Sales per Customer (Monthly)',34,7,NULL,'Total Sales per Customer (Monthly)','2018-01-28 21:23:09','fathur','2018-10-14 01:55:48','fathur'),(42,'ndgp','Daily Gross Profit',34,4,NULL,'New Daily Gross Profit','2018-03-04 19:35:37','fathur','2018-10-14 01:54:04','fathur'),(45,'tpmonthly','Total Purchase (Monthly)',34,10,NULL,'Total Purchase (Monthly)','2018-04-08 20:40:14','fathur','2018-10-14 01:57:12','fathur'),(47,'tppsmonthly','Total Purchase Per Supplier (Monthly)',34,11,NULL,'Total Purchase Per Supplier (Monthly)','2018-04-08 23:10:26','fathur','2018-10-14 01:57:29','fathur'),(48,'MonthlySales','Monthly Sales',34,5,NULL,'Monthly Sales','2018-04-26 21:34:14','fathur','2018-10-14 01:54:49','fathur'),(49,'return_s','Data Retur Penjualan',3,10,NULL,'Data Retur','2021-12-21 00:00:00','fathur','2021-12-21 00:00:00','fathur'),(50,'return_p','Data Retur Pembelian',28,11,NULL,'Data Return Pembelian','2021-12-21 00:00:00','fathur','2021-12-21 20:52:49','admin'),(51,'profile','Informasi Perusahaan',2,5,NULL,'InformasiPerusahaan',NULL,NULL,NULL,NULL);

/*Table structure for table `profile` */

DROP TABLE IF EXISTS `profile`;

CREATE TABLE `profile` (
  `Alamat` varchar(4000) DEFAULT NULL,
  `Telp1` varchar(500) DEFAULT NULL,
  `Telp2` varchar(500) DEFAULT NULL,
  `Nama` varchar(500) DEFAULT NULL,
  `Keterangan` varchar(500) DEFAULT NULL,
  `web` varchar(500) DEFAULT NULL,
  `email` varchar(500) DEFAULT NULL,
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `logo` blob DEFAULT NULL,
  `logo_extension` varchar(30) DEFAULT NULL,
  `instagram` varchar(500) DEFAULT NULL,
  `facebook` varchar(500) DEFAULT NULL,
  `tweeter` varchar(500) DEFAULT NULL,
  `Alamat2` varchar(4000) DEFAULT NULL,
  `label_alamat` varchar(100) DEFAULT NULL,
  `label_alamat2` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;

/*Data for the table `profile` */

insert  into `profile`(`Alamat`,`Telp1`,`Telp2`,`Nama`,`Keterangan`,`web`,`email`,`id`,`logo`,`logo_extension`,`instagram`,`facebook`,`tweeter`,`Alamat2`,`label_alamat`,`label_alamat2`) values ('Jl. Cendrawasih Raya No. 45 Kel. Sawah Baru.\r\nCiputat, Tangerang Selatan.','0812-1227-3837','0859-2158-9354','CV. Sumber Rezeki Internasional','Trusted & Qualified Chicken Provider','https://www.ayamsri.com','sumerrezekiinternasional.84@gmail.vom',1,'�PNG\r\n\Z\n\0\0\0\rIHDR\0\0\00\0\0\00\0\0\0W��\0\0\0gAMA\0\0���a\0\0aIDAThC�_L[U�Q�1�A�����\\��Y4�fكك\n�#�R�%�b���u3�0`X�;���b`���!G�S��R���[�����9z<���]���\n�ɾ�\'i��s>��{��۔�?e�e*�,h�Z���6��ի�Ꮢ?FE�+���QQx(������f�lV�~��*�ʇ�P潉��\\�U�{�����$\r-Ew)�< �GP��@��>,ybl�_\'	�0��_�K�\0���$?�4\'�}�pN���9��*K�Ns�� ��><�c�?jTȽH��~,���!iΉ@���Ó#�v��z����Di1.mY�Ӗ��Olhuf�]�irO�]#9��e�{BXS�7�����dSg��<@�<���)\'>����ƋA/U�7��	��f����f���O���by��p�=f�dbſ��f�QwF�\Z5����BqF_\r�(�?��%&�l�5bA�xi��<ޅ�P�\0�ó��Ąj�]s��E�����cx�\0�oᷫ���IQ���\0��Pf�nv��G�8�C5\0�m ,�����X�\Znr�W�\r�B���V�Cp�v�<<+�F�1�v�	�%���zy7�n�eK�f���G��+��3\ZЌ�p��v⯭ck���G�T�\0�,�u�Ù�>�M�	�Z��耸���Z��aу~}��hQX\0�1^K�����f��wP�����P�\"����ʼ7���o�rR�����^�z!Qp=$`R�vų\0n���,��\0���\0Q4��4�?w���\0Ǎ/!8�z�TP:���\n�5da1�@���w\nǁm��,n�0�������T\0\n�E�v�������-��e#�@\\Sg\0= #�#b.\0�v_�{$�Hp�\Z���&�xY��N�񑛀��\"��Ǎs�����s҈�X��(�)�K��c�g {IV\Z���7\r,�9v�3�%=p�|�$,��m&wGݹFW1�N?����DWH�<h�֑;�\"�2S��b����걐�y|V��&w�%�)7�8�6�[?>K[I�g;.!p���=����:�C5���\0��0S�4V���k�$�`����(���J?�J��55�C٠XA�,��n��~�u6�f�(�\0&�}r�(��\Z���������j�8h�����6��7�����@���\\�����k�騰�Յ�<Q`�0����}B-�2�Ao���������,����̔n�n~]m*\\�o�Exf��BR���0��F����C]��-�A�{`���$\r8E2p�v�MU��M&&}�.���F�\\��B�����P9��+��»���\n���?�w {��9����}���T0���ʃ<H�$%�_��\r��bǠ\0\0\0\0IEND�B`�','.png','@ayam.sri','ayam.sri','@ayam.sri','Pabuaran, Gunung Sindur Bogor','Kantor Pusat :','Kantor Cabang :');

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
