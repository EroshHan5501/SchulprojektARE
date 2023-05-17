-- DROP USER IF EXISTS poketrainer;
-- CREATE USER 'poketrainer'@'localhost';
-- GRANT ALL PRIVILEGES ON pokedex.* TO 'poketrainer'@'localhost' IDENTIFIED BY 'password123';

DROP DATABASE IF EXISTS pokedex;
CREATE DATABASE pokedex;
USE pokedex;

CREATE TABLE pokemon(
    pokemonId INTEGER PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    height INTEGER NOT NULL,
    isDefault BOOLEAN NOT NULL,
    baseExperience INTEGER NOT NULL
);

CREATE TABLE attack(
    attackId INTEGER PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR(100) NOT NULL UNIQUE,
    url VARCHAR(100) NOT NULL UNIQUE
);

CREATE TABLE pokeattack(
    pokeattackId INTEGER PRIMARY KEY AUTO_INCREMENT,
    fpokemonId INTEGER REFERENCES pokemon(pokemonId),
    fattackId INTEGER REFERENCES attack(attackId)
);

CREATE TABLE image(
    imageId INTEGER PRIMARY KEY AUTO_INCREMENT,
    url TEXT NOT NULL UNIQUE,
    fpokemonId INTEGER REFERENCES pokemon(pokemonId) 
);

