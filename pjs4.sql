-- phpMyAdmin SQL Dump
-- version 4.6.4
-- https://www.phpmyadmin.net/
--
-- Client :  127.0.0.1
-- Généré le :  Mar 21 Mars 2017 à 15:20
-- Version du serveur :  5.7.14
-- Version de PHP :  5.6.25

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de données :  `pjs4`
--

-- --------------------------------------------------------

--
-- Structure de la table `amis`
--

CREATE TABLE `amis` (
  `idJoueurFrom` int(11) NOT NULL,
  `idJoueurTo` int(11) NOT NULL,
  `confirmer` tinyint(1) DEFAULT '0'
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

--
-- Déclencheurs `amis`
--
DELIMITER $$
CREATE TRIGGER `T_Parite_Amis` BEFORE INSERT ON `amis` FOR EACH ROW BEGIN
DECLARE msg text;
if new.idJoueurFrom = new.idJoueurTo THEN
set msg = 'erreur : vous ne pouvez pas êtes amis avec vous même';
signal sqlstate '45000' set message_text = msg;
end if;

if(select idJoueurFrom from amis where idJoueurFrom = new.idJoueurFrom and idJoueurTo = new.idJoueurTo) is not null THEN
set msg = 'erreur : vous ne pouvez pas êtes amis avec vous même';
signal sqlstate '45000' set message_text = msg;
end if;

if(select idJoueurFrom from amis where idJoueurFrom = new.idJoueurTo and idJoueurTo = new.idJoueurFrom) is not null THEN
set msg = 'erreur : vous ne pouvez pas êtes amis avec vous même';
signal sqlstate '45000' set message_text = msg;
end if;
END
$$
DELIMITER ;

-- --------------------------------------------------------

--
-- Structure de la table `joueur`
--

CREATE TABLE `joueur` (
  `idJoueur` int(11) NOT NULL,
  `pseudo` varchar(256) COLLATE utf8_bin NOT NULL,
  `passe` varchar(256) COLLATE utf8_bin NOT NULL,
  `email` varchar(256) COLLATE utf8_bin NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

--
-- Contenu de la table `joueur`
--

INSERT INTO `joueur` (`idJoueur`, `pseudo`, `passe`, `email`) VALUES
(1, 'Sami', 'psw', ''),
(2, 'Laurent', 'psw', ''),
(3, 'Jean-Luc', 'psw', ''),
(4, 'Seb', 'psw', ''),
(5, 'Mat', 'psw', '');

-- --------------------------------------------------------

--
-- Structure de la table `niveau`
--

CREATE TABLE `niveau` (
  `idNiveau` int(11) NOT NULL,
  `libelleNiveau` varchar(256) COLLATE utf8_bin NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

--
-- Contenu de la table `niveau`
--

INSERT INTO `niveau` (`idNiveau`, `libelleNiveau`) VALUES
(1, 'Plage'),
(2, 'Montagne'),
(3, 'Espace');

-- --------------------------------------------------------

--
-- Structure de la table `partie`
--

CREATE TABLE `partie` (
  `idPartie` int(11) NOT NULL,
  `idNiveau` int(11) NOT NULL,
  `idJoueur` int(11) NOT NULL,
  `score` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

--
-- Contenu de la table `partie`
--

INSERT INTO `partie` (`idPartie`, `idNiveau`, `idJoueur`, `score`) VALUES
(3, 1, 1, 15),
(4, 1, 2, 12),
(5, 1, 3, 25),
(6, 1, 5, 23),
(7, 1, 2, 13),
(8, 1, 2, 15),
(9, 1, 3, 21),
(10, 1, 3, 17),
(11, 1, 4, 18),
(12, 1, 4, 28),
(13, 2, 1, 45),
(14, 2, 1, 47);

--
-- Index pour les tables exportées
--

--
-- Index pour la table `amis`
--
ALTER TABLE `amis`
  ADD PRIMARY KEY (`idJoueurFrom`,`idJoueurTo`);

--
-- Index pour la table `joueur`
--
ALTER TABLE `joueur`
  ADD PRIMARY KEY (`idJoueur`);

--
-- Index pour la table `niveau`
--
ALTER TABLE `niveau`
  ADD PRIMARY KEY (`idNiveau`);

--
-- Index pour la table `partie`
--
ALTER TABLE `partie`
  ADD PRIMARY KEY (`idPartie`),
  ADD KEY `idNiveau` (`idNiveau`),
  ADD KEY `idJoueur` (`idJoueur`);

--
-- AUTO_INCREMENT pour les tables exportées
--

--
-- AUTO_INCREMENT pour la table `joueur`
--
ALTER TABLE `joueur`
  MODIFY `idJoueur` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;
--
-- AUTO_INCREMENT pour la table `niveau`
--
ALTER TABLE `niveau`
  MODIFY `idNiveau` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;
--
-- AUTO_INCREMENT pour la table `partie`
--
ALTER TABLE `partie`
  MODIFY `idPartie` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=15;
--
-- Contraintes pour les tables exportées
--

--
-- Contraintes pour la table `partie`
--
ALTER TABLE `partie`
  ADD CONSTRAINT `partie_ibfk_1` FOREIGN KEY (`idNiveau`) REFERENCES `niveau` (`idNiveau`),
  ADD CONSTRAINT `partie_ibfk_2` FOREIGN KEY (`idJoueur`) REFERENCES `joueur` (`idJoueur`) ON DELETE CASCADE;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
