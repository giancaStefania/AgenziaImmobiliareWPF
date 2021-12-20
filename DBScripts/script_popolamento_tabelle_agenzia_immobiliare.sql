USE AgenziaImmobiliare
GO

insert into proprietari(CF, nome, cognome, telefono) 
	 values ('SQLNML77D26L219B','Alessandro','Stella','3922381785');
insert into proprietari(CF, nome, cognome, telefono) 
	 values ('PRCAMN77D26L219B','Amanda','Procida','3775655434');
insert into proprietari(CF, nome, cognome, telefono) 
	 values ('ASDFGH77D26L217A','Viola','Rossi','3123456789');

insert into tipiImmobile(descrizione) values('villa');
insert into tipiImmobile(descrizione) values('appartamento');
insert into tipiImmobile(descrizione) values('rurale');
 
insert into immobili(id_tipo, via, superfice, vani, prezzo, id_proprietario, in_vendita)
	 values (2, 'via carlo alberto', 150, 4, 2500000, 1, 1);
insert into immobili(id_tipo, via, superfice, vani, prezzo, id_proprietario, in_vendita)
	 values (2, 'via roma', 50, 2, 300000, 3, 1);
insert into immobili(id_tipo, via, superfice, vani, prezzo, id_proprietario, in_vendita)
	 values (1, 'corso vittorio emanuele', 250, 8, 5000000, 1, 0);

insert into TipiAnnessi(descrizione) values('garage');
insert into TipiAnnessi(descrizione) values('cantina');
insert into TipiAnnessi(descrizione) values('giardino');
insert into TipiAnnessi(descrizione) values('box');

insert into AnnessiImmobili(id_annesso, id_immobile) values(1, 3);
insert into AnnessiImmobili(id_annesso, id_immobile) values(3, 3);