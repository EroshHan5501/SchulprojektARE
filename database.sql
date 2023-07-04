-- DROP USER IF EXISTS poketrainer;
-- CREATE USER 'poketrainer'@'localhost';
-- GRANT ALL PRIVILEGES ON pokedex.* TO 'poketrainer'@'localhost' IDENTIFIED BY 'password123';

DROP DATABASE IF EXISTS pokedex;
CREATE DATABASE pokedex;
USE pokedex;

CREATE TABLE pokemon(
    Id INTEGER PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    BaseExperience INTEGER NOT NULL,
    Height INTEGER NOT NULL,
    Weight INTEGER NOT NULL
);

CREATE TABLE moves(
    Id INTEGER PRIMARY KEY,
    Name VARCHAR(100) NOT NULL UNIQUE
);

CREATE TABLE pokemoves(
    pokeattackId INTEGER PRIMARY KEY AUTO_INCREMENT,
    fpokemonId INTEGER REFERENCES pokemon(Id),
    fmovesId INTEGER REFERENCES moves(Id)
);

CREATE TABLE sprites(
    Id INTEGER PRIMARY KEY AUTO_INCREMENT,
    Front TEXT NOT NULL UNIQUE,
    Back TEXT NOT NULL UNIQUE,
    FpokemonId INTEGER REFERENCES pokemon(Id) 
);

