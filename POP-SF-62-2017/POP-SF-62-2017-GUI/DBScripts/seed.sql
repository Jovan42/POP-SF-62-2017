INSERT INTO TipNamestaja (Naziv) VALUES ('Krevet');
INSERT INTO TipNamestaja (Naziv) VALUES ('Ugaona garnitura');
INSERT INTO TipNamestaja (Naziv) VALUES ('Kauc');


INSERT INTO Namestaj(TipNamestajaID, Naziv, Cena, Kolicina, Obrisan) 
	VALUES (1 , 'Francuski Krevet', 123.5 , 22 , 0);
INSERT INTO Namestaj (TipNamestajaID, Naziv, Cena, Kolicina, Obrisan) 
	VALUES (2, 'Sofija ugaona', 223.9, 12, 0);
	
	
INSERT INTO Namestaj (TipNamestajaID, Naziv, Cena, Kolicina, Obrisan) 
	VALUES (3, 'Ivan kauc', 525 ,12 , 0); 
INSERT INTO Korisnik (Ime, Prezime, KorIme, Lozinka, Admin)
	VALUES ('admin', 'admin', 'admin', 'admin', 1); 
INSERT INTO Korisnik (Ime, Prezime, KorIme, Lozinka, Admin)
	VALUES ('user', 'user', 'user', 'user', 0); 


INSERT INTO DodatnaUsluga(Naziv, Cena) VALUES ('Prevoz', 5000);
INSERT INTO DodatnaUsluga(Naziv, Cena) VALUES ('Sklapanje', 2000);

INSERT INTO Akcija (Pocetak, Kraj, Popust) VALUES ('1/1/2016', '1/1/2018', 20);

INSERT INTO Prodaja (DatumProdaje, Kupac) VALUES ('1/1/2001', 'Kupac1');
INSERT INTO Prodaja (DatumProdaje, Kupac) VALUES ('2/2/2002', 'Kupac2');
INSERT INTO Prodaja (DatumProdaje, Kupac) VALUES ('3/3/2003', 'Kupac3');

INSERT INTO ProdatNamestaj (NamestajId, ProdajaId, Kolicina) VALUES (1, 1, 50);
INSERT INTO ProdatNamestaj (NamestajId, ProdajaId, Kolicina) VALUES (1, 2, 50);
INSERT INTO ProdatNamestaj (NamestajId, ProdajaId, Kolicina) VALUES (2, 1, 100);
INSERT INTO ProdatNamestaj (NamestajId, ProdajaId, Kolicina) VALUES (3, 2, 50);
INSERT INTO ProdatNamestaj (NamestajId, ProdajaId, Kolicina) VALUES (3, 3, 200);

INSERT INTO NamestajNaAkciji (NamestajId, AkcijaId) VALUES (1, 1);

INSERT INTO IzvrsenaDodatnaUsluga (DodatnaUslugaId, ProdajaId) VALUES (1, 1);
INSERT INTO IzvrsenaDodatnaUsluga (DodatnaUslugaId, ProdajaId) VALUES (1, 2);
INSERT INTO IzvrsenaDodatnaUsluga (DodatnaUslugaId, ProdajaId) VALUES (1, 3);
INSERT INTO IzvrsenaDodatnaUsluga (DodatnaUslugaId, ProdajaId) VALUES (2, 2);

INSERT INTO Salon (Naziv, Adresa, Mail, Sajt, Telefon, PIB, MatBr, ZiroRacun) 
	VALUES ('Naziv', 'Adresa', 'Mail', 'Sajt', 'Telefon', 'PIB', 'MatBr', 'ZiroRacun');