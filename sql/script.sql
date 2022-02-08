CREATE DATABASE cadastro;

CREATE TABLE cliente(

    id INTEGER PRIMARY KEY  AUTOINCREMENTE,
    nome TEXT NOT NULL,
    telefone TEXT NOT NULL,
    cep TEXT NOT NULL,
    email TEXT NOT NULL
);

CREATE TABLE contato(
    id INTEGER PRIMARY KEY  AUTOINCREMENTE,
    nome TEXT NOT NULL,
    telefone TEXT NOT NULL UNIQUE,
    grau_parestesco TEXT NOT NULL UNIQUE,
    idCliente INTEGER NOT NULL,
    FOREIGN KEY (idCliente) REFERENCES cliente(id)

);