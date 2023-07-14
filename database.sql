-- DROP USER IF EXISTS poketrainer;
-- CREATE USER 'poketrainer'@'localhost';
-- GRANT ALL PRIVILEGES ON pokedex.* TO 'poketrainer'@'localhost' IDENTIFIED BY 'password123';

CREATE USER IF NOT EXISTS 'poketrainer'@'localhost' IDENTIFIED BY 'password123';
GRANT ALL PRIVILEGES ON pokedex TO 'pokedex'@'localhost' WITH GRANT OPTION;

DROP DATABASE IF EXISTS pokedex;
CREATE DATABASE pokedex;
USE pokedex;

CREATE TABLE pokemon(
    Id INTEGER PRIMARY KEY,
    Name VARCHAR(100),
    BaseExperience INTEGER,
    Height INTEGER,
    Weight INTEGER,
    Type VARCHAR(20),
    Hp INTEGER,
    Attack INTEGER,
    Defense INTEGER,
    SpecialAttack INTEGER,
    SpecialDefense INTEGER,
    Speed INTEGER
);

CREATE TABLE moves(
    Id INTEGER PRIMARY KEY,
    Name VARCHAR(100) NOT NULL UNIQUE
);

CREATE TABLE pokemoves(
    Id INTEGER PRIMARY KEY AUTO_INCREMENT,
    PokeId INTEGER REFERENCES pokemon(Id),
    MoveId INTEGER REFERENCES moves(Id)
);

CREATE TABLE abilities(
    Id INTEGER PRIMARY KEY,
    Name VARCHAR(100) NOT NULL UNIQUE,
    Entry VARCHAR(2000),
    ShortEntry VARCHAR(1000)
);

CREATE TABLE pokeabilities(
    Id INTEGER PRIMARY KEY AUTO_INCREMENT,
    PokeId INTEGER REFERENCES pokemon(Id),
    AbilityId INTEGER REFERENCES abilities(Id)
);

CREATE TABLE sprites(
    Id INTEGER PRIMARY KEY AUTO_INCREMENT,
    Front TEXT UNIQUE,
    Back TEXT UNIQUE,
    FpokemonId INTEGER REFERENCES pokemon(Id) 
);

