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
CREATE SCHEMA IF NOT EXISTS `mydb` DEFAULT CHARACTER SET utf8mb3 ;
USE `mydb` ;

-- -----------------------------------------------------
-- Table `mydb`.`user`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`user` (
  `idUser` INT NOT NULL AUTO_INCREMENT,
  `login` VARCHAR(100) NOT NULL,
  `password` VARCHAR(100) NOT NULL,
  PRIMARY KEY (`idUser`),
  UNIQUE INDEX `idUser_UNIQUE` (`idUser` ASC) VISIBLE,
  UNIQUE INDEX `login_UNIQUE` (`login` ASC) VISIBLE)
ENGINE = InnoDB
AUTO_INCREMENT = 4
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `mydb`.`quiz`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`quiz` (
  `idQuiz` INT NOT NULL AUTO_INCREMENT,
  `User_idUser` INT NOT NULL,
  `Name` VARCHAR(100) NOT NULL,
  `time` INT NULL DEFAULT NULL,
  PRIMARY KEY (`idQuiz`),
  UNIQUE INDEX `idQuiz_UNIQUE` (`idQuiz` ASC) VISIBLE,
  INDEX `fk_Quiz_User_idx` (`User_idUser` ASC) VISIBLE,
  CONSTRAINT `fk_Quiz_User`
    FOREIGN KEY (`User_idUser`)
    REFERENCES `mydb`.`user` (`idUser`))
ENGINE = InnoDB
AUTO_INCREMENT = 4
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `mydb`.`question`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`question` (
  `idQuestion` INT NOT NULL AUTO_INCREMENT,
  `Content` VARCHAR(100) NOT NULL,
  `Quiz_idQuiz` INT NULL DEFAULT NULL,
  PRIMARY KEY (`idQuestion`),
  UNIQUE INDEX `idQuestion_UNIQUE` (`idQuestion` ASC) VISIBLE,
  INDEX `fk_Question_Quiz1_idx` (`Quiz_idQuiz` ASC) VISIBLE,
  CONSTRAINT `fk_Question_Quiz1`
    FOREIGN KEY (`Quiz_idQuiz`)
    REFERENCES `mydb`.`quiz` (`idQuiz`))
ENGINE = InnoDB
AUTO_INCREMENT = 4
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `mydb`.`answer`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`answer` (
  `idAnswer` INT NOT NULL AUTO_INCREMENT,
  `Content` VARCHAR(256) NOT NULL,
  `IsCorrect` TINYINT NOT NULL,
  `question_idQuestion` INT NULL DEFAULT NULL,
  PRIMARY KEY (`idAnswer`),
  UNIQUE INDEX `idAnswer_UNIQUE` (`idAnswer` ASC) VISIBLE,
  INDEX `fk_answer_question1_idx` (`question_idQuestion` ASC) VISIBLE,
  CONSTRAINT `fk_answer_question1`
    FOREIGN KEY (`question_idQuestion`)
    REFERENCES `mydb`.`question` (`idQuestion`))
ENGINE = InnoDB
AUTO_INCREMENT = 9
DEFAULT CHARACTER SET = utf8mb3;


-- -----------------------------------------------------
-- Table `mydb`.`result`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`result` (
  `idResult` INT NOT NULL,
  `result` INT NOT NULL,
  `Quiz_idQuiz` INT NOT NULL,
  PRIMARY KEY (`idResult`),
  UNIQUE INDEX `idResult_UNIQUE` (`idResult` ASC) VISIBLE,
  INDEX `fk_Result_Quiz1_idx` (`Quiz_idQuiz` ASC) VISIBLE,
  CONSTRAINT `fk_Result_Quiz1`
    FOREIGN KEY (`Quiz_idQuiz`)
    REFERENCES `mydb`.`quiz` (`idQuiz`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb3;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
