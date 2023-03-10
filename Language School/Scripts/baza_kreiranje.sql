CREATE database SkolaJezika

CREATE TABLE dbo.Adresa (
	Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	Ulica VARCHAR(50),
	Broj VARCHAR(10),
	Grad VARCHAR(50),
	Drzava VARCHAR (50)
)

CREATE TABLE dbo.Skola (
	Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	Naziv VARCHAR(50),
	Jezici VARCHAR(100),
	AdresaId INT,
	CONSTRAINT FK_Skola_Adresa
	FOREIGN KEY (AdresaId) REFERENCES dbo.Adresa (Id)
)
CREATE TABLE dbo.Korisnik
(
	Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	Ime		VARCHAR(50) NOT NULL,
	Prezime VARCHAR(50) NOT NULL,
	Jmbg VARCHAR(13) UNIQUE NOT NULL,
	Email VARCHAR(50) UNIQUE NOT NULL,
	Password VARCHAR(50) NOT NULL,
	Pol VARCHAR(10) NOT NULL,
	Tip VARCHAR(20) NOT NULL,
	Aktivan BIT NOT NULL,
	AdresaId INT,
	CONSTRAINT User_Adresa
	FOREIGN KEY (AdresaId) REFERENCES dbo.Adresa (Id)

)

CREATE TABLE dbo.Profesor
(
 Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
 UserId INT NOT NULL UNIQUE,
 SkolaId INT NOT NULL,
 Jezici VARCHAR(100),
 Casovi VARCHAR(100)
 CONSTRAINT FK_Korisnik_Profesor
 FOREIGN KEY (UserId) REFERENCES dbo.Korisnik (Id),
 CONSTRAINT FK_Skola_Profesor
 FOREIGN KEY (SkolaId) REFERENCES dbo.Skola (Id)
)

CREATE TABLE dbo.Student
(
 Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
 UserId INT NOT NULL UNIQUE,
 Casovi VARCHAR(100),
 CONSTRAINT FK_Korisnik_Student
 FOREIGN KEY (UserId) REFERENCES dbo.Korisnik (Id)
)

CREATE TABLE dbo.Cas
(
 Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
 ProfesorId INT NOT NULL,
 DatumVreme DATETIME,
 Trajanje INT,
 Status VARCHAR(10) NOT NULL,
 Obrisan BIT NOT NULL,
 StudentId INT,
 CONSTRAINT FK_Cas_Profesor
 FOREIGN KEY (ProfesorId) REFERENCES dbo.Profesor (Id),
 CONSTRAINT FK_Cas_Student
 FOREIGN KEY (StudentId) REFERENCES dbo.Student (Id)
)