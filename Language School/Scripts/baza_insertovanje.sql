use SkolaJezika
SELECT * FROM KORISNIK
SELECT * FROM ADRESA
SELECT * FROM SKOLA
SELECT * FROM PROFESOR
SELECT * FROM STUDENT
SELECT * FROM CAS
insert into dbo.Adresa(Ulica,Broj,Grad,Drzava) values ('Bulevar Vojvode Stepe','22','Novi Sad','Srbija')

insert into dbo.Adresa(Ulica,Broj,Grad,Drzava) values ('Jevrejska','9','Novi Sad','Srbija')

insert into dbo.Adresa(Ulica,Broj,Grad,Drzava) values ('Milana Raspopovica','10','Beograd','Srbija')

insert into dbo.Adresa(Ulica,Broj,Grad,Drzava) values ('Bulevar Zorana Djindjica','121','Beograd','Srbija')

insert into dbo.Adresa(Ulica,Broj,Grad,Drzava) values ('Kralja Petra','20','Kragujevac','Srbija')

insert into dbo.Adresa(Ulica,Broj,Grad,Drzava) values ('Makenzijeva','65a','Cacak','Srbija')

insert into dbo.Skola(Naziv,Jezici,AdresaId) values ('Language school abc','spanski,japanski,nemacki,litvanski,francuski',6)

insert into dbo.Skola(Naziv,Jezici,AdresaId) values ('Skola stranih jezika Fifi','spanski,engleski,nemacki',1)

insert into dbo.Skola(Naziv,Jezici,AdresaId) values ('Skola stranih jezika Milutin','spanski,japanski,nemacki,litvanski,francuski',2)

insert into dbo.Korisnik(Ime,Prezime,Jmbg,Email,Password,Pol,Tip,Aktivan,AdresaId) values ('Marija','Maricic','2208997523261','maki@gmail.com','sifra123','ZENSKI','PROFESOR',1,3)

insert into dbo.Korisnik(Ime,Prezime,Jmbg,Email,Password,Pol,Tip,Aktivan,AdresaId) values ('Ana','Anicic','1342977523261','ankaanicic@gmail.com','ana','ZENSKI','PROFESOR',1,4)

insert into dbo.Korisnik(Ime,Prezime,Jmbg,Email,Password,Pol,Tip,Aktivan,AdresaId) values ('Smiljana','Spasic','0408772831957','s.s@gmail.com','sifralaka','ZENSKI','STUDENT',1,5)

insert into dbo.Korisnik(Ime,Prezime,Jmbg,Email,Password,Pol,Tip,Aktivan,AdresaId) values ('Lazar','Dejanovic','030700725242','lazardejanovic@gmail.com','laki123','MUSKI','STUDENT',1,3)

insert into dbo.Korisnik(Ime,Prezime,Jmbg,Email,Password,Pol,Tip,Aktivan,AdresaId) values ('Tijana','Mitrovic','2704988768352','ticamit@gmail.com','tijana','ZENSKI','ADMINISTRATOR',1,5)

insert into dbo.Profesor(UserId,SkolaId,Jezici,Casovi) values (1,1,'spanski,engleski','1,2')
insert into dbo.Profesor(UserId,SkolaId,Jezici,Casovi) values (2,2,'nemacki,japanski,litvanski','3,4')

insert into dbo.Student(UserId,Casovi) values (3,'1')
insert into dbo.Student(UserId,Casovi) values (4,'3')

insert into dbo.Cas(ProfesorId,DatumVreme,Trajanje,Status,Obrisan,StudentID) values (1,'2022-02-02 08:00:00',45,'REZERVISAN',0,1)
insert into dbo.Cas(ProfesorId,DatumVreme,Trajanje,Status,Obrisan) values (1,'2023-09-12 14:00:00',30,'SLOBODAN',0)
insert into dbo.Cas(ProfesorId,DatumVreme,Trajanje,Status,Obrisan,StudentID) values (2,'2023-01-02 18:00:00',120,'REZERVISAN',0,2)
insert into dbo.Cas(ProfesorId,DatumVreme,Trajanje,Status,Obrisan) values (2,'2023-05-12 12:00:00',150,'SLOBODAN',0)