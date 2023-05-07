-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `mydb` DEFAULT CHARACTER SET utf8 ;
USE `mydb` ;

-- -----------------------------------------------------
-- Table `mydb`.`User`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`User` (
  `idUser` INT NOT NULL AUTO_INCREMENT,
  `login` VARCHAR(45) NOT NULL,
  `password` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`idUser`),
  UNIQUE INDEX `idUser_UNIQUE` (`idUser` ASC) VISIBLE,
  UNIQUE INDEX `login_UNIQUE` (`login` ASC) VISIBLE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Quiz`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`Quiz` (
  `idQuiz` INT NOT NULL AUTO_INCREMENT,
  `User_idUser` INT NOT NULL,
  UNIQUE INDEX `idQuiz_UNIQUE` (`idQuiz` ASC) VISIBLE,
  PRIMARY KEY (`idQuiz`),
  INDEX `fk_Quiz_User_idx` (`User_idUser` ASC) VISIBLE,
  CONSTRAINT `fk_Quiz_User`
    FOREIGN KEY (`User_idUser`)
    REFERENCES `mydb`.`User` (`idUser`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Question`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`Question` (
  `idQuestion` INT NOT NULL AUTO_INCREMENT,
  `Content` VARCHAR(45) NOT NULL,
  `Quiz_idQuiz` INT NOT NULL,
  PRIMARY KEY (`idQuestion`),
  UNIQUE INDEX `idQuestion_UNIQUE` (`idQuestion` ASC) VISIBLE,
  INDEX `fk_Question_Quiz1_idx` (`Quiz_idQuiz` ASC) VISIBLE,
  CONSTRAINT `fk_Question_Quiz1`
    FOREIGN KEY (`Quiz_idQuiz`)
    REFERENCES `mydb`.`Quiz` (`idQuiz`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Answer`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`Answer` (
  `idAnswer` INT NOT NULL AUTO_INCREMENT,
  `Content` VARCHAR(45) NOT NULL,
  `Question_idQuestion` INT NOT NULL,
  `isCorrect` TINYINT NOT NULL,
  PRIMARY KEY (`idAnswer`),
  UNIQUE INDEX `idAnswer_UNIQUE` (`idAnswer` ASC) VISIBLE,
  INDEX `fk_Answer_Question1_idx` (`Question_idQuestion` ASC) VISIBLE,
  CONSTRAINT `fk_Answer_Question1`
    FOREIGN KEY (`Question_idQuestion`)
    REFERENCES `mydb`.`Question` (`idQuestion`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
