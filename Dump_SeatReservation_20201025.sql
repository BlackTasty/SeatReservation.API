-- MySQL dump 10.13  Distrib 8.0.18, for Win64 (x86_64)
--
-- Host: localhost    Database: seat_reservation
-- ------------------------------------------------------
-- Server version	8.0.18

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
-- Table structure for table `__efmigrationshistory`
--

DROP TABLE IF EXISTS `__efmigrationshistory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `__efmigrationshistory` (
  `MigrationId` varchar(95) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `__efmigrationshistory`
--

LOCK TABLES `__efmigrationshistory` WRITE;
/*!40000 ALTER TABLE `__efmigrationshistory` DISABLE KEYS */;
INSERT INTO `__efmigrationshistory` VALUES ('20200519141856_Init','2.1.14-servicing-32113'),('20200520050857_UpdatedScheduleSlot','2.1.14-servicing-32113'),('20200523175724_UpdatedMovie','2.1.14-servicing-32113'),('20201023012018_Locations','2.1.14-servicing-32113'),('20201023022258_RoomAssignments','2.1.14-servicing-32113'),('20201024113208_RoomTechnologies','2.1.14-servicing-32113'),('20201024165415_SeatPrices','2.1.14-servicing-32113'),('20201024204319_Reservations','2.1.14-servicing-32113');
/*!40000 ALTER TABLE `__efmigrationshistory` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `genres`
--

DROP TABLE IF EXISTS `genres`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `genres` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` longtext NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=19 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `genres`
--

LOCK TABLES `genres` WRITE;
/*!40000 ALTER TABLE `genres` DISABLE KEYS */;
INSERT INTO `genres` VALUES (1,'Abenteuer'),(2,'Action'),(3,'Drama'),(4,'Fantasy'),(5,'Biografie'),(6,'Comedy'),(7,'Horror'),(8,'Kriegsfilm'),(9,'Romantik'),(10,'Martial-Arts'),(11,'Roadmovie'),(12,'Science-Fiction'),(13,'Sportfilm'),(14,'Thriller'),(15,'Western'),(16,'Dokumentation'),(17,'Animation'),(18,'Familienfilm');
/*!40000 ALTER TABLE `genres` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `locations`
--

DROP TABLE IF EXISTS `locations`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `locations` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` longtext NOT NULL,
  `Address` longtext NOT NULL,
  `ZipCode` int(11) NOT NULL,
  `Country` longtext NOT NULL,
  `State` longtext NOT NULL,
  `Logo` longtext,
  `IsShutdown` bit(1) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `locations`
--

LOCK TABLES `locations` WRITE;
/*!40000 ALTER TABLE `locations` DISABLE KEYS */;
INSERT INTO `locations` VALUES (1,'Tasty Cinemas','Maxgasse',1160,'Österreich','Wien','https://i.imgur.com/BVD4LaI.png',_binary '\0'),(4,'Cineplexx','Görgengasse 2',1190,'Österreich','Wien','https://img.cineplexx.at/static/images/logo_aut.png',_binary '\0');
/*!40000 ALTER TABLE `locations` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `movies`
--

DROP TABLE IF EXISTS `movies`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `movies` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Title` longtext NOT NULL,
  `Banner` longtext NOT NULL,
  `Poster` longtext NOT NULL,
  `Logo` longtext NOT NULL,
  `Trailer` longtext NOT NULL,
  `Description` longtext NOT NULL,
  `MovieLength` int(11) NOT NULL,
  `ReleaseDate` datetime(6) NOT NULL,
  `IsArchived` bit(1) NOT NULL,
  `Genres` longtext,
  `IsFeatured` bit(1) NOT NULL DEFAULT b'0',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `movies`
--

LOCK TABLES `movies` WRITE;
/*!40000 ALTER TABLE `movies` DISABLE KEYS */;
INSERT INTO `movies` VALUES (1,'Avengers - Endgame','https://i.redd.it/6lzpvqgj5ry21.jpg','https://reggiestake.files.wordpress.com/2019/04/avengers-endgame-poster-45.jpg',' https://i.redd.it/aa2enqws8bp21.jpg',' ','Thanos (Josh Brolin) hat also tatsächlich Wort gehalten, seinen Plan in die Tat umgesetzt und die Hälfte allen Lebens im Universum ausgelöscht. Die Avengers? Machtlos. Iron Man (Robert Downey Jr.) und Nebula (Karen Gillan) sitzen auf dem Planeten Titan fest, während auf der Erde absolutes Chaos herrscht. Doch dann finden Captain America (Chris Evans) und die anderen überlebenden Helden auf der Erde heraus, dass Nick Fury (Samuel L. Jackson) vor den verheerenden Ereignissen gerade noch ein Notsignal absetzen konnte, um Verstärkung auf den Plan zu rufen. Die Superhelden-Gemeinschaft bekommt mit Captain Marvel (Brie Larson) kurzerhand tatkräftige Unterstützung im Kampf gegen ihren vermeintlich übermächtigen Widersacher. Und dann ist da auch noch Ant-Man (Paul Rudd), der wie aus dem Nichts auftaucht und sich der Truppe erneut anschließt, um die ganze Sache womöglich doch noch zu einem guten Ende zu bringen...',182,'2019-04-21 22:00:00.000000',_binary '\0','2;12',_binary ''),(2,'Drachenzähmen leicht gemacht',' https://www.iamag.co/wp-content/uploads/2014/04/how-to-train-your-dragon-banner.jpg',' https://extralifereviews.files.wordpress.com/2019/03/how-to-train-your-dragon.jpg',' https://media1.jpc.de/image/w600/front/0/0030206701203.jpg',' ',' Auf der kleinen Insel Berk, hoch oben im Norden, wird nicht lange gefackelt: Mordsstarke Wikinger müssen ihre Insel vor wilden Drachen beschützen, die ihnen das Leben zur Hölle machen. Auch der schmächtige Teenager Hicks (Daniel Axt), Beiname \"der Hüne\", ist Feuer und Flamme für die Drachenjagd – nur leider hat er im Umgang mit Waffen zwei linke Hände. Da sein Vater niemand Geringeres ist als Haudrauf \"der Stoische\" (Dominic Raacke), Häuptling des Wikingerstammes und ein berühmter Drachenjäger, soll Hicks auf der Drachenschule nun auch die raue Kampfkunst der Wikinger erlernen. Doch ehe Hicks – der übrigens ein pfiffiger und begeisterter Tüftler ist – den gefürchteten Unterricht antreten kann, wird sein Dorf von einer wilden Drachen-Armada angegriffen. Das ist Hicks\' große Chance, einmal einen Drachen zu besiegen und die Ehre seines Vaters zu retten! Mit viel Mut und Köpfchen gelingt es ihm, einen \"Nachtschatten\", eine der gefährlichsten Drachenarten, abzuschießen. Als Hicks seiner Beute endlich gegenübersteht, traut er seinen Augen kaum: Traurig schnaufend schaut der Drache ihn mit großen Augen an. Statt seinem Vater von seinem bemerkenswerten Fang zu erzählen, freundet sich Hicks mit dem Nachtschatten an. Es ist der Beginn einer Freundschaft, die vor allem Hicks’ Welt auf den Kopf stellt: Ein Wikinger, der einen Drachen zum Freund hat? Das geht gegen alle Regeln der Wikingertradition! Niemals wird sein Vater das akzeptieren…',98,'2020-05-16 11:34:12.082000',_binary '\0','17;18',_binary '\0'),(3,'Eragon',' ',' ','https://www.chip.de/ii/9/5/5/7/1/0/9/3/93227f90389d65cf.jpeg',' ',' ',90,'2020-05-16 17:33:56.535000',_binary '','2;4',_binary '\0'),(4,'Jurassic Park','https://cdn.shopify.com/s/files/1/0969/9128/products/Jurassic_Park_-_Tallenge_Hollywood_Movie_Poster_Collection_de904a9a-4ad6-4e4a-83ce-323ae9c4ff8e.jpg?v=1577693352','https://d13ezvd6yrslxm.cloudfront.net/wp/wp-content/images/JURASSIC-PARK-JC-Richard.jpeg','https://logoeps.com/wp-content/uploads/2013/03/jurassic-park-vector-logo.png',' ',' Der millionenschwere Unternehmer John Hammond (Richard Attenborough) errichtet auf einer südamerikanischen Insel einen kostenaufwendigen Freizeitpark, für dessen Inbetriebnahme er den Segen von Paläontologen braucht. Also lässt er das dinosaurierbegeisterte Forscherpärchen Dr. Grant (Sam Neill) und Dr. Sattler (Laura Dern) zusammen mit dem eigenwilligen Mathematiker Dr. Malcolm (Jeff Goldblum) auf die Insel fliegen und führt ihnen voller Stolz seine außergewöhnliche Attraktion vor: Einen Park voller Dinosaurier, die mit Hilfe urzeitlicher DNA und modernster Technik zum Leben erweckt wurden. Die Wissenschaftler sind zunächst begeistert, aber Dr. Malcolm legt auch Skepsis an den Tag. Und er soll Recht behalten: Bei der ersten Generalprobe der scheinbar perfekten Sensation geht etwas schief, denn Dinos und Menschen handeln jeweils nicht so, wie es sich Hammond vorgestellt hat…',127,'1993-06-08 22:00:00.000000',_binary '\0','1;12',_binary ''),(5,'Drachenzähmen leicht gemacht 3: Die geheime Welt','https://cdn-az.allevents.in/banners/33197c40-122c-11e9-bda8-cdd7e4e7df8b-rimg-w590-h247-gmir.jpg','https://trailer.kinopolis.de/grafiken/plakate_608x860/1224642.jpg','https://mytoys.scene7.com/is/image/myToys/ext/10947318-01.jpg?$rtf_mt_prod-main_xl$',' ',' Hicks (Stimme: Daniel Axt) hat als Häuptling von Berk seinen langgehegten Traum wahrgemacht und eine Welt geschaffen, in der Drachen und Menschen friedlich zusammenleben können. Auch Hicks bester Freund, der Nachtschatten Ohnezahn, fühlt sich in dieser Welt wohl – bis er eines Tages einem ungezähmten und eigensinnigen Drachenweibchen begegnet, das ihm völlig den Kopf verdreht. Das Weibchen sieht aus wie Ohnezahn, nur dass es weiß ist und somit wohl ein Tagschatten sein muss. Während der Flirt der beiden Drachen die Freundschaft von Hicks und Ohnezahn gehörig auf die Probe stellt, bahnt sich ein noch viel schwerwiegenderes Problem an: Der fiese Drachenjäger Grimmel (Lutz Riedel) bedroht das Dorf und seine tierischen und menschlichen Bewohner. Nun müssen sich die Drachenreiter und ihre geflügelten Freunde nach einem neuen Ort umsehen, an dem sie leben können – und sie beginnen mit der gefahrvollen Suche nach der sagenumwobenen „verborgenen Welt“, in der sie alle Frieden finden könnten.',104,'2019-01-02 23:00:00.000000',_binary '\0','17;18',_binary '\0'),(6,'It Chapter Two','https://www.eyecinema.ie/thumb.php?src=/images/films/it-chapter-two-backdrop-35.jpg&w=765&h=350&zc=1','https://cdn.collider.com/wp-content/uploads/2019/07/it-chapter-two-new-poster.jpg','https://pbs.twimg.com/profile_images/1187467830876225538/3cJV5Sng_400x400.jpg',' ','27 Jahre sind vergangen, seit sich mehrere Kinder in der Kleinstadt Derry einem Monster in den Weg stellten und es scheinbar besiegten. Als Derry erneut von einer Mordserie heimgesucht wird, dämmert dem als Bibliothekar arbeitenden Mike Hanlon (Isaiah Mustafa), dass ES zurück ist und er beschließt, seine einstigen Freunde vom Club der Verlierer anzurufen. Sie alle haben Derry längst den Rücken gekehrt und erinnern sich nicht einmal mehr daran, was damals passiert ist. Doch als Bill Denbrough (James McAvoy), Beverly Marsh (Jessica Chastain), Ben Hanscom (Jay Ryan), Richie Tozier (Bill Hader), Eddie Kaspbrak (James Ransone) und Stanley Uris (Andy Bean) den Anruf von Mike erhalten und von ihm an ein altes Versprechen erinnert werden, wissen sie, dass sie aus irgendeinem Grund zurück nach Derry müssen. Bevor sie jedoch erneut den Kampf mit dem meist in Form des sadistischen Clowns Pennywise (Bill Skarsgård) auftretenden ES aufnehmen können, müssen sie sich erst einmal daran erinnern, was in ihrer Vergangenheit passiert ist…\n',169,'2019-09-05 22:00:00.000000',_binary '\0','7;14',_binary ''),(7,'Inception','http://de.web.img3.acsta.net/r_1920_1080/medias/nmedia/18/72/34/14/19461079.jpg',' http://de.web.img3.acsta.net/r_1920_1080/medias/nmedia/18/76/58/35/19602258.jpg',' http://de.web.img3.acsta.net/r_1920_1080/medias/nmedia/18/72/34/14/19476652.jpg',' ','Dom Cobb (Leonardo DiCaprio) ist der beste Extraktor, den man bekommen kann. In den Träumen seiner Opfer fahndet er nach Wirtschaftsgeheimnissen, um sie dann gewinnträchtig weiterzuverkaufen. Das Problem: Seine riskanten Methoden haben ihn auf die Abschussliste diverser Konzerne gebracht, sodass er sich nirgendwo mehr sicher wähnen kann. Die Heimkehr in die USA bleibt ihm verwehrt, wo seine kleinen Kinder auf ihn warten. Der Großindustrielle Saito (Ken Watanabe) heuert Cobb für einen letzten Job an - einen, der ihm den lang ersehnten Weg nach Hause ebnen könnte, sollte er Erfolg haben. Bloß, diesmal ist auch die Aufgabe ungleich schwieriger. Cobb und sein Dreamteam sollen keine Idee stehlen, sondern eine im Unbewussten eines Konzern-Erben (Cillian Murphy) einpflanzen, dessen Unternehmen zur Gefahr für Saito geworden ist. Akribisch bereiten sich Cobb und seine Mannschaft auf den Zugriff vor, doch eine Variable bleibt unkalkulierbar: Das psychische Echo von Cobbs toter Ehefrau (Marion Cotillard)...',88,'2010-07-29 22:00:00.000000',_binary '\0','2;14',_binary '\0'),(8,'John Wick',' http://de.web.img2.acsta.net/r_1920_1080/pictures/14/10/07/11/29/511172.jpg',' http://de.web.img3.acsta.net/r_1920_1080/pictures/14/10/01/14/18/135831.jpg',' http://de.web.img2.acsta.net/r_1920_1080/pictures/14/10/07/11/29/511172.jpg',' ','John Wick (Keanu Reeves) genießt seinen frühen Ruhestand in der Vorstadt. Doch als seine Frau (Bridget Moynahan) einer tödlichen Krankheit erliegt, verfällt er in Trauer. Nur sein Hund bleibt ihm noch als Gefährte – und wird von drei russischen Gangstern getötet, als die in Wicks Haus einbrechen, um einen 1969er Boss Mustang zu stehlen. Damit wird Wick von seiner finsteren Vergangenheit eingeholt, war er doch früher einer der besten Auftragskiller des Landes. Er verlässt die Vorstadtidylle und macht sich, seine Rache vor Augen, auf die Suche nach den Einbrechern. Einer von ihnen ist Iosef Tarasov (Alfie Allen), der Sohn des einflussreichen Verbrecherbosses Viggo Tarasov (Michael Nyqvist), für den Wick selbst einmal gearbeitet hatte. Doch alte Verbindungen zählen jetzt nicht mehr. Und so hat der Rächer bald auch den Ex-Kollegen Marcus (Willem Dafoe) an seinen Fersen...',107,'2014-10-12 22:00:00.000000',_binary '\0','14',_binary '');
/*!40000 ALTER TABLE `movies` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `permissions`
--

DROP TABLE IF EXISTS `permissions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `permissions` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` longtext NOT NULL,
  `Description` longtext NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `permissions`
--

LOCK TABLES `permissions` WRITE;
/*!40000 ALTER TABLE `permissions` DISABLE KEYS */;
INSERT INTO `permissions` VALUES (1,'Administrator','Hat Zugriff auf alle Funktionen.'),(2,'Besitzer','Kann Daten des Unternehmens �ndern, neue Standorte hinzuf�gen. Hat au�erdem Zugriff auf den Zeitplan f�r jeden Raum und kann diese auch bearbeiten.'),(3,'Raum Verwaltung','Darf R�ume und deren Plan erstellen, bearbeiten und entfernen.'),(4,'Finanzen','Hat Einsicht in Einnahmen und Ausgaben.'),(5,'Film Planung','Kann das Filmangebot �ndern und Zeitpl�ne erstellen und R�umen zuweisen.'),(6,'Benutzer','Kann Erinnerungen f�r Filme setzen, Tickets buchen und den Buchungsverlauf einsehen.');
/*!40000 ALTER TABLE `permissions` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `reservations`
--

DROP TABLE IF EXISTS `reservations`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `reservations` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `SeatId` int(11) NOT NULL,
  `ScheduleSlotId` int(11) NOT NULL,
  `RoomId` int(11) NOT NULL,
  `ReservationStatus` int(11) NOT NULL,
  `BookingDate` datetime(6) NOT NULL,
  `UserId` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `reservations`
--

LOCK TABLES `reservations` WRITE;
/*!40000 ALTER TABLE `reservations` DISABLE KEYS */;
/*!40000 ALTER TABLE `reservations` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `room_assignments`
--

DROP TABLE IF EXISTS `room_assignments`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `room_assignments` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `LocationId` int(11) NOT NULL,
  `RoomIds` longtext NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `room_assignments`
--

LOCK TABLES `room_assignments` WRITE;
/*!40000 ALTER TABLE `room_assignments` DISABLE KEYS */;
INSERT INTO `room_assignments` VALUES (1,1,'1;2'),(2,4,'3;4;5;6;7');
/*!40000 ALTER TABLE `room_assignments` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `room_plan`
--

DROP TABLE IF EXISTS `room_plan`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `room_plan` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Seats` longtext,
  `Columns` int(11) NOT NULL,
  `Rows` int(11) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `room_plan`
--

LOCK TABLES `room_plan` WRITE;
/*!40000 ALTER TABLE `room_plan` DISABLE KEYS */;
INSERT INTO `room_plan` VALUES (1,'1;2;3;4;5;6;25;36;37;7;8;9;10;11;12;26;38;39;13;14;15;16;17;18;27;40;41;19;20;21;22;23;24;28;42;43;29;30;31;32;33;34;35;44;45;226;227;228;229;230;231;232;233;234',9,6),(2,'46;47;48;49;50;51;52;53;54;55;56;57;58;59;60;61;62;63;64;65;66;67;68;69;70;71;72;73;74;75;76;77;78;79;80;81;82;83;84;85;86;87;88;89;90;91;92;93;94;95;96;97;98;99;100;101;102;103;104;105',10,6),(3,'106;107;108;109;110;111;112;113;114;115;116;117;118;119;120;121;122;123;124;125;126;127;128;129;130;131;132;133;134;135;136;137;138;139;140;141;142;143;144;145;146;147;148;149;150;151;152;153;154;155;156;157;158;159;160;161;162;163;164;165',10,6),(4,'166;167;168;169;170;171;172;173;174;175;176;177;178;179;180;181;182;183;184;185;186;187;188;189;190;191;192;193;194;195;196;197;198;199;200;201;202;203;204;205;206;207;208;209;210;211;212;213;214;215;216;217;218;219;220;221;222;223;224;225',10,6),(5,'235;236;237;238;239;240;241;242;243;244;245;246;247;248;249;250;251;252;253;254;255;256;257;258;259;260;261;262;263;264;265;266;267;268;269;270;271;272;273;274;275;276;277;278;279;280;281;282;283;284;285;286;287;288;289;290;291;292;293;294',10,6),(6,'463;464;465;466;467;468;469;470;471;472;473;474;475;476;477;478;479;480;481;482;483;484;485;486',6,4),(7,'487;488;489;490;491;492;493;494;495;496;497;498;499;500;501;502;503;504;505;506;507;508;509;510;511;512;513;514;515;516;517;518;519;520;521;522;523;524;525;526;527;528;529;530;531',5,3);
/*!40000 ALTER TABLE `room_plan` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `room_technologies`
--

DROP TABLE IF EXISTS `room_technologies`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `room_technologies` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` longtext NOT NULL,
  `Description` longtext,
  `ExtraCharge` double NOT NULL DEFAULT '0',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `room_technologies`
--

LOCK TABLES `room_technologies` WRITE;
/*!40000 ALTER TABLE `room_technologies` DISABLE KEYS */;
INSERT INTO `room_technologies` VALUES (1,'Digital 2D','',0),(2,'Real 3D','',2.4),(3,'3D Dolby Atmos','',4.4),(4,'IMAX 2D','',13),(5,'IMAX 3D','',15.4),(6,'IMAX Immersive Sound 3D','',19.4);
/*!40000 ALTER TABLE `room_technologies` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `rooms`
--

DROP TABLE IF EXISTS `rooms`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `rooms` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` longtext NOT NULL,
  `ScheduleId` int(11) NOT NULL,
  `RoomPlanId` int(11) NOT NULL,
  `IsOpen` bit(1) NOT NULL,
  `TechnologyId` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `rooms`
--

LOCK TABLES `rooms` WRITE;
/*!40000 ALTER TABLE `rooms` DISABLE KEYS */;
INSERT INTO `rooms` VALUES (1,'Saal 01',1,1,_binary '',6),(2,'Saal 02',2,2,_binary '',2),(3,'Saal 03',3,3,_binary '',3),(4,'Saal 04',4,4,_binary '\0',4),(5,'Saal 01',5,5,_binary '\0',5),(6,'Saal 02',6,6,_binary '\0',6),(7,'Saal 05',7,7,_binary '\0',1);
/*!40000 ALTER TABLE `rooms` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `schedule_slots`
--

DROP TABLE IF EXISTS `schedule_slots`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `schedule_slots` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `MovieId` int(11) NOT NULL,
  `Start` datetime(6) NOT NULL,
  `End` datetime(6) NOT NULL,
  `Reservations` longtext,
  `ScheduleId` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=34 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `schedule_slots`
--

LOCK TABLES `schedule_slots` WRITE;
/*!40000 ALTER TABLE `schedule_slots` DISABLE KEYS */;
INSERT INTO `schedule_slots` VALUES (1,1,'2020-05-23 19:00:00.000000','2020-05-23 22:02:00.000000','',1),(2,4,'2020-05-23 19:30:00.000000','2020-05-23 21:37:00.000000','',2),(3,5,'2020-05-23 19:45:00.000000','2020-05-23 21:29:00.000000','',3),(4,2,'2020-05-23 19:35:00.000000','2020-05-23 21:13:00.000000','',4),(5,1,'2020-06-15 15:56:00.000000','2020-06-15 18:58:00.000000','',2),(6,4,'2020-06-15 14:30:00.000000','2020-06-15 16:37:00.000000','',1),(7,1,'2020-06-15 18:00:00.000000','2020-06-15 21:02:00.000000','',3),(8,2,'2020-06-15 16:45:00.000000','2020-06-15 18:23:00.000000','',1),(9,1,'2020-06-16 15:00:00.000000','2020-06-16 18:02:00.000000','',3),(10,4,'2020-06-16 10:00:00.000000','2020-06-16 12:07:00.000000','',1),(11,6,'2020-06-16 20:15:00.000000','2020-06-16 23:04:00.000000','',3),(12,2,'2020-06-16 14:00:00.000000','2020-06-16 15:38:00.000000','',2),(13,1,'2020-06-16 12:30:00.000000','2020-06-16 15:32:00.000000','',1),(14,7,'2020-06-16 12:00:00.000000','2020-06-16 13:28:00.000000','',2),(15,7,'2020-06-16 15:45:00.000000','2020-06-16 17:13:00.000000','',1),(16,8,'2020-06-16 13:05:00.000000','2020-06-16 14:52:00.000000','',3),(17,7,'2020-09-11 15:30:00.000000','2020-09-11 16:58:00.000000','',3),(18,4,'2020-09-11 15:10:00.000000','2020-09-11 17:17:00.000000','',2),(19,1,'2020-10-23 02:00:00.000000','2020-10-23 05:02:00.000000','',1),(20,4,'2020-10-23 02:00:00.000000','2020-10-23 04:07:00.000000','',2),(21,2,'2020-10-23 04:15:00.000000','2020-10-23 05:53:00.000000','',2),(22,5,'2020-10-23 06:15:00.000000','2020-10-23 07:59:00.000000','',2),(23,6,'2020-10-23 15:10:00.000000','2020-10-23 17:59:00.000000','',2),(24,8,'2020-10-23 15:20:00.000000','2020-10-23 17:07:00.000000','',1),(25,2,'2020-10-23 19:14:00.000000','2020-10-23 20:52:00.000000','',3),(26,2,'2020-10-23 18:10:00.000000','2020-10-23 19:48:00.000000','',2),(27,7,'2020-10-23 19:36:00.000000','2020-10-23 21:04:00.000000','',1),(28,4,'2020-10-23 19:36:00.000000','2020-10-23 21:43:00.000000','',4),(29,7,'2020-10-23 21:20:00.000000','2020-10-23 22:48:00.000000','',1),(30,1,'2020-10-23 20:00:00.000000','2020-10-23 23:02:00.000000','',2),(31,2,'2020-10-23 12:00:00.000000','2020-10-23 13:38:00.000000','',3),(32,2,'2020-10-24 08:00:00.000000','2020-10-24 09:38:00.000000','',5),(33,2,'2020-10-24 13:15:00.000000','2020-10-24 14:53:00.000000','',4);
/*!40000 ALTER TABLE `schedule_slots` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `schedules`
--

DROP TABLE IF EXISTS `schedules`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `schedules` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `MovieSchedule` longtext,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=68 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `schedules`
--

LOCK TABLES `schedules` WRITE;
/*!40000 ALTER TABLE `schedules` DISABLE KEYS */;
INSERT INTO `schedules` VALUES (1,'1;6;8;10;13;15;19;24;27;29'),(2,'2;5;12;14;18;20;21;22;23;26;30'),(3,'3;7;9;11;16;17;25;31'),(4,'4;28;33'),(5,'32'),(6,''),(7,'');
/*!40000 ALTER TABLE `schedules` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `seat_positions`
--

DROP TABLE IF EXISTS `seat_positions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `seat_positions` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `SeatTypeId` int(11) NOT NULL,
  `Column` int(11) NOT NULL,
  `Row` int(11) NOT NULL,
  `Rotation` int(11) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=532 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `seat_positions`
--

LOCK TABLES `seat_positions` WRITE;
/*!40000 ALTER TABLE `seat_positions` DISABLE KEYS */;
INSERT INTO `seat_positions` VALUES (1,1,1,1,0),(2,1,2,1,0),(3,3,3,1,0),(4,1,4,1,0),(5,1,5,1,0),(6,1,6,1,0),(7,1,1,2,0),(8,1,2,2,0),(9,3,3,2,0),(10,1,4,2,0),(11,1,5,2,0),(12,1,6,2,0),(13,1,1,3,0),(14,1,2,3,0),(15,3,3,3,0),(16,1,4,3,0),(17,1,5,3,0),(18,1,6,3,0),(19,1,1,4,0),(20,1,2,4,0),(21,3,3,4,0),(22,3,4,4,0),(23,3,5,4,0),(24,3,6,4,0),(25,3,7,1,0),(26,3,7,2,0),(27,3,7,3,0),(28,3,7,4,0),(29,1,1,5,0),(30,2,2,5,0),(31,3,3,5,0),(32,2,4,5,0),(33,2,5,5,0),(34,2,6,5,0),(35,3,7,5,0),(36,1,8,1,0),(37,1,9,1,0),(38,1,8,2,0),(39,1,9,2,0),(40,1,8,3,0),(41,1,9,3,0),(42,1,8,4,0),(43,1,9,4,0),(44,2,8,5,0),(45,1,9,5,0),(46,1,1,1,0),(47,1,2,1,0),(48,1,3,1,0),(49,1,4,1,0),(50,1,5,1,0),(51,1,6,1,0),(52,1,7,1,0),(53,1,8,1,0),(54,1,9,1,0),(55,1,10,1,0),(56,1,1,2,0),(57,1,2,2,0),(58,1,3,2,0),(59,1,4,2,0),(60,1,5,2,0),(61,1,6,2,0),(62,1,7,2,0),(63,1,8,2,0),(64,1,9,2,0),(65,1,10,2,0),(66,1,1,3,0),(67,1,2,3,0),(68,1,3,3,0),(69,1,4,3,0),(70,1,5,3,0),(71,1,6,3,0),(72,1,7,3,0),(73,1,8,3,0),(74,1,9,3,0),(75,1,10,3,0),(76,1,1,4,0),(77,1,2,4,0),(78,1,3,4,0),(79,1,4,4,0),(80,1,5,4,0),(81,1,6,4,0),(82,1,7,4,0),(83,1,8,4,0),(84,1,9,4,0),(85,1,10,4,0),(86,1,1,5,0),(87,1,2,5,0),(88,1,3,5,0),(89,1,4,5,0),(90,1,5,5,0),(91,1,6,5,0),(92,1,7,5,0),(93,1,8,5,0),(94,1,9,5,0),(95,1,10,5,0),(96,1,1,6,0),(97,1,2,6,0),(98,1,3,6,0),(99,1,4,6,0),(100,1,5,6,0),(101,1,6,6,0),(102,1,7,6,0),(103,1,8,6,0),(104,1,9,6,0),(105,1,10,6,0),(106,1,1,1,0),(107,1,2,1,0),(108,1,3,1,0),(109,1,4,1,0),(110,1,5,1,0),(111,1,6,1,0),(112,1,7,1,0),(113,1,8,1,0),(114,1,9,1,0),(115,1,10,1,0),(116,1,1,2,0),(117,1,2,2,0),(118,1,3,2,0),(119,1,4,2,0),(120,1,5,2,0),(121,1,6,2,0),(122,1,7,2,0),(123,1,8,2,0),(124,1,9,2,0),(125,1,10,2,0),(126,1,1,3,0),(127,1,2,3,0),(128,1,3,3,0),(129,1,4,3,0),(130,1,5,3,0),(131,1,6,3,0),(132,1,7,3,0),(133,1,8,3,0),(134,1,9,3,0),(135,1,10,3,0),(136,1,1,4,0),(137,1,2,4,0),(138,1,3,4,0),(139,1,4,4,0),(140,1,5,4,0),(141,1,6,4,0),(142,1,7,4,0),(143,1,8,4,0),(144,1,9,4,0),(145,1,10,4,0),(146,1,1,5,0),(147,1,2,5,0),(148,1,3,5,0),(149,1,4,5,0),(150,1,5,5,0),(151,1,6,5,0),(152,1,7,5,0),(153,1,8,5,0),(154,1,9,5,0),(155,1,10,5,0),(156,1,1,6,0),(157,1,2,6,0),(158,1,3,6,0),(159,1,4,6,0),(160,1,5,6,0),(161,1,6,6,0),(162,1,7,6,0),(163,1,8,6,0),(164,1,9,6,0),(165,1,10,6,0),(166,1,1,1,0),(167,1,2,1,0),(168,1,3,1,0),(169,1,4,1,0),(170,1,5,1,0),(171,1,6,1,0),(172,1,7,1,0),(173,1,8,1,0),(174,1,9,1,0),(175,1,10,1,0),(176,1,1,2,0),(177,1,2,2,0),(178,1,3,2,0),(179,1,4,2,0),(180,1,5,2,0),(181,1,6,2,0),(182,1,7,2,0),(183,1,8,2,0),(184,1,9,2,0),(185,1,10,2,0),(186,1,1,3,0),(187,1,2,3,0),(188,1,3,3,0),(189,1,4,3,0),(190,1,5,3,0),(191,1,6,3,0),(192,1,7,3,0),(193,1,8,3,0),(194,1,9,3,0),(195,1,10,3,0),(196,1,1,4,0),(197,1,2,4,0),(198,1,3,4,0),(199,1,4,4,0),(200,1,5,4,0),(201,1,6,4,0),(202,1,7,4,0),(203,1,8,4,0),(204,1,9,4,0),(205,1,10,4,0),(206,1,1,5,0),(207,1,2,5,0),(208,1,3,5,0),(209,1,4,5,0),(210,1,5,5,0),(211,1,6,5,0),(212,1,7,5,0),(213,1,8,5,0),(214,1,9,5,0),(215,1,10,5,0),(216,1,1,6,0),(217,1,2,6,0),(218,1,3,6,0),(219,1,4,6,0),(220,1,5,6,0),(221,1,6,6,0),(222,1,7,6,0),(223,1,8,6,0),(224,1,9,6,0),(225,1,10,6,0),(226,1,1,6,0),(227,1,2,6,0),(228,3,3,6,0),(229,1,4,6,0),(230,1,5,6,0),(231,1,6,6,0),(232,3,7,6,0),(233,1,8,6,0),(234,1,9,6,0),(235,1,1,1,0),(236,1,2,1,0),(237,1,3,1,0),(238,1,4,1,0),(239,1,5,1,0),(240,1,6,1,0),(241,1,7,1,0),(242,1,8,1,0),(243,1,9,1,0),(244,1,10,1,0),(245,1,1,2,0),(246,1,2,2,0),(247,1,3,2,0),(248,1,4,2,0),(249,1,5,2,0),(250,1,6,2,0),(251,1,7,2,0),(252,1,8,2,0),(253,1,9,2,0),(254,1,10,2,0),(255,1,1,3,0),(256,1,2,3,0),(257,1,3,3,0),(258,1,4,3,0),(259,1,5,3,0),(260,1,6,3,0),(261,1,7,3,0),(262,1,8,3,0),(263,1,9,3,0),(264,1,10,3,0),(265,1,1,4,0),(266,1,2,4,0),(267,1,3,4,0),(268,1,4,4,0),(269,1,5,4,0),(270,1,6,4,0),(271,1,7,4,0),(272,1,8,4,0),(273,1,9,4,0),(274,1,10,4,0),(275,1,1,5,0),(276,1,2,5,0),(277,1,3,5,0),(278,1,4,5,0),(279,1,5,5,0),(280,1,6,5,0),(281,1,7,5,0),(282,1,8,5,0),(283,1,9,5,0),(284,1,10,5,0),(285,1,1,6,0),(286,1,2,6,0),(287,1,3,6,0),(288,1,4,6,0),(289,1,5,6,0),(290,1,6,6,0),(291,1,7,6,0),(292,1,8,6,0),(293,1,9,6,0),(294,1,10,6,0),(295,1,1,1,0),(296,1,2,1,0),(297,1,3,1,0),(298,1,4,1,0),(299,1,5,1,0),(300,1,6,1,0),(301,1,7,1,0),(302,1,8,1,0),(303,1,9,1,0),(304,1,10,1,0),(305,1,1,2,0),(306,1,2,2,0),(307,1,3,2,0),(308,1,4,2,0),(309,1,5,2,0),(310,1,6,2,0),(311,1,7,2,0),(312,1,8,2,0),(313,1,9,2,0),(314,1,10,2,0),(315,1,1,3,0),(316,1,2,3,0),(317,1,3,3,0),(318,1,4,3,0),(319,1,5,3,0),(320,1,6,3,0),(321,1,7,3,0),(322,1,8,3,0),(323,1,9,3,0),(324,1,10,3,0),(325,1,1,4,0),(326,1,2,4,0),(327,1,3,4,0),(328,1,4,4,0),(329,1,5,4,0),(330,1,6,4,0),(331,1,7,4,0),(332,1,8,4,0),(333,1,9,4,0),(334,1,10,4,0),(335,1,1,5,0),(336,1,2,5,0),(337,1,3,5,0),(338,1,4,5,0),(339,1,5,5,0),(340,1,6,5,0),(341,1,7,5,0),(342,1,8,5,0),(343,1,9,5,0),(344,1,10,5,0),(345,1,1,6,0),(346,1,2,6,0),(347,1,3,6,0),(348,1,4,6,0),(349,1,5,6,0),(350,1,6,6,0),(351,1,7,6,0),(352,1,8,6,0),(353,1,9,6,0),(354,1,10,6,0),(355,1,1,1,0),(356,1,2,1,0),(357,1,3,1,0),(358,1,4,1,0),(359,1,5,1,0),(360,1,6,1,0),(361,1,7,1,0),(362,1,8,1,0),(363,1,9,1,0),(364,1,10,1,0),(365,1,1,2,0),(366,1,2,2,0),(367,1,3,2,0),(368,1,4,2,0),(369,1,5,2,0),(370,1,6,2,0),(371,1,7,2,0),(372,1,8,2,0),(373,1,9,2,0),(374,1,10,2,0),(375,1,1,3,0),(376,1,2,3,0),(377,1,3,3,0),(378,1,4,3,0),(379,1,5,3,0),(380,1,6,3,0),(381,1,7,3,0),(382,1,8,3,0),(383,1,9,3,0),(384,1,10,3,0),(385,1,1,4,0),(386,1,2,4,0),(387,1,3,4,0),(388,1,4,4,0),(389,1,5,4,0),(390,1,6,4,0),(391,1,7,4,0),(392,1,8,4,0),(393,1,9,4,0),(394,1,10,4,0),(395,1,1,5,0),(396,1,2,5,0),(397,1,3,5,0),(398,1,4,5,0),(399,1,5,5,0),(400,1,6,5,0),(401,1,7,5,0),(402,1,8,5,0),(403,1,9,5,0),(404,1,10,5,0),(405,1,1,6,0),(406,1,2,6,0),(407,1,3,6,0),(408,1,4,6,0),(409,1,5,6,0),(410,1,6,6,0),(411,1,7,6,0),(412,1,8,6,0),(413,1,9,6,0),(414,1,10,6,0),(415,1,1,1,0),(416,1,2,1,0),(417,1,3,1,0),(418,1,4,1,0),(419,1,5,1,0),(420,1,6,1,0),(421,1,1,2,0),(422,1,2,2,0),(423,1,3,2,0),(424,1,4,2,0),(425,1,5,2,0),(426,1,6,2,0),(427,1,1,3,0),(428,1,2,3,0),(429,1,3,3,0),(430,1,4,3,0),(431,1,5,3,0),(432,1,6,3,0),(433,1,1,4,0),(434,1,2,4,0),(435,1,3,4,0),(436,1,4,4,0),(437,1,5,4,0),(438,1,6,4,0),(439,1,1,1,0),(440,1,2,1,0),(441,1,3,1,0),(442,1,4,1,0),(443,1,5,1,0),(444,1,6,1,0),(445,1,1,2,0),(446,1,2,2,0),(447,1,3,2,0),(448,1,4,2,0),(449,1,5,2,0),(450,1,6,2,0),(451,1,1,3,0),(452,1,2,3,0),(453,1,3,3,0),(454,1,4,3,0),(455,1,5,3,0),(456,1,6,3,0),(457,1,1,4,0),(458,1,2,4,0),(459,1,3,4,0),(460,1,4,4,0),(461,1,5,4,0),(462,1,6,4,0),(463,1,1,1,0),(464,1,2,1,0),(465,1,3,1,0),(466,1,4,1,0),(467,1,5,1,0),(468,1,6,1,0),(469,1,1,2,0),(470,1,2,2,0),(471,1,3,2,0),(472,1,4,2,0),(473,1,5,2,0),(474,1,6,2,0),(475,1,1,3,0),(476,1,2,3,0),(477,1,3,3,0),(478,1,4,3,0),(479,1,5,3,0),(480,1,6,3,0),(481,1,1,4,0),(482,1,2,4,0),(483,1,3,4,0),(484,1,4,4,0),(485,1,5,4,0),(486,1,6,4,0),(487,1,1,1,0),(488,1,2,1,0),(489,1,3,1,0),(490,1,4,1,0),(491,1,5,1,0),(492,1,6,1,0),(493,1,7,1,0),(494,1,4,1,0),(495,1,5,1,0),(496,1,1,2,0),(497,1,2,2,0),(498,1,3,2,0),(499,1,4,2,0),(500,1,5,2,0),(501,1,6,2,0),(502,1,7,2,0),(503,1,4,2,0),(504,1,5,2,0),(505,1,1,3,0),(506,1,2,3,0),(507,1,3,3,0),(508,1,4,3,0),(509,1,5,3,0),(510,1,6,3,0),(511,1,7,3,0),(512,1,4,3,0),(513,1,5,3,0),(514,1,1,4,0),(515,1,2,4,0),(516,1,3,4,0),(517,1,4,4,0),(518,1,5,4,0),(519,1,6,4,0),(520,1,7,4,0),(521,1,4,4,0),(522,1,5,4,0),(523,1,1,5,0),(524,1,2,5,0),(525,1,3,5,0),(526,1,4,5,0),(527,1,5,5,0),(528,1,6,5,0),(529,1,7,5,0),(530,1,4,5,0),(531,1,5,5,0);
/*!40000 ALTER TABLE `seat_positions` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `seat_types`
--

DROP TABLE IF EXISTS `seat_types`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `seat_types` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` longtext NOT NULL,
  `SeatImage` longtext,
  `SeatCount` int(11) NOT NULL,
  `BasePrice` double NOT NULL DEFAULT '0',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `seat_types`
--

LOCK TABLES `seat_types` WRITE;
/*!40000 ALTER TABLE `seat_types` DISABLE KEYS */;
INSERT INTO `seat_types` VALUES (1,'Sitzplatz (Standard)','https://i.imgur.com/pvyYUER.png',1,9.9),(2,'Sitzplatz (Premium)','https://i.imgur.com/tpUxxPI.png',1,13.9),(3,'Leer','https://i.imgur.com/wm0ETrR.png',0,0);
/*!40000 ALTER TABLE `seat_types` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `users` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Username` longtext NOT NULL,
  `Password` longtext NOT NULL,
  `FirstName` longtext NOT NULL,
  `LastName` longtext NOT NULL,
  `RegisterDate` datetime(6) NOT NULL,
  `Permissions` longtext NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` VALUES (1,'admin','799824ba3560d3955f302c392de50e2232991ffaeca6f24200cf46571b523489',' ',' ','2020-05-12 22:33:00.000000','1');
/*!40000 ALTER TABLE `users` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2020-10-25  1:06:43
