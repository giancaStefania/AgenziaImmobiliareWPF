create database AgenziaImmobiliare;

USE AgenziaImmobiliare
GO
-- tabella proprietari
create table Proprietari
(
	id int identity(1,1) primary key,
	CF char(16) not null unique,
	nome varchar(50),
	cognome varchar(50),
	telefono varchar(11)
);

-- tabella dei tipi di immobile
create table TipiImmobile
(
	id int identity(1,1) primary key,
	descrizione varchar(100)
);

-- tabella immobili
create table Immobili
(
	id int identity(1,1) primary key,
	id_tipo int references TipiImmobile(id),
	via varchar(100),
	superfice int,
	vani int,
	prezzo int,
	id_proprietario int references Proprietari(id),
	in_vendita bit
);

-- tabella annessi
create table TipiAnnessi
(
	id int identity(1,1) primary key,
	descrizione varchar(100)
);

-- tabella associative tra immobili ed annessi
create table AnnessiImmobili
(
	id_annesso int references TipiAnnessi(id),
	id_immobile int references Immobili(id)
);
