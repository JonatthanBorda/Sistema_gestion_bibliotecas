--								Script de creación de tablas

-- Se verifica si la base de datos ya existe y se elimina:
IF DB_ID('db_LibrarySystem') IS NOT NULL
DROP DATABASE db_LibrarySystem;
GO

-- Creación de la base de datos:
CREATE DATABASE db_LibrarySystem;
GO

USE db_LibrarySystem;
GO

-- Creación de tabla TipoDocto
CREATE TABLE TipoDocto (
    Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    Tipo NVARCHAR(50) NOT NULL
);
GO

-- Creacicón de tabla Autor:
CREATE TABLE Autor (
    Id INT PRIMARY KEY IDENTITY(10001,1) NOT NULL,
	Nombre NVARCHAR(100) NOT NULL,
	Apellido NVARCHAR(100) NOT NULL,
	IdTipoDocto INT NOT NULL,
	NumDocto INT NOT NULL,
	FechaNacimiento DATE NOT NULL,
	Bibliografia NVARCHAR(MAX),
	-- Auditoria
	FechaCreacion DATETIME NOT NULL,
	UsuarioCreacion NVARCHAR(255) NOT NULL,
	FechaModificacion DATETIME,
	UsuarioModificacion NVARCHAR(255),

	FOREIGN KEY (IdTipoDocto) REFERENCES TipoDocto(Id)
);
GO

-- Creacicón de tabla Libro:
CREATE TABLE Libro (
    Id INT PRIMARY KEY IDENTITY(20001,1) NOT NULL,
	Titulo NVARCHAR(255) NOT NULL,
	NumPaginas INT NOT NULL,
	FechaPublicacion DATE NOT NULL,
	Disponible BIT NOT NULL,
	IdAutor INT NOT NULL,   
	-- Auditoria
	FechaCreacion DATETIME NOT NULL,
	UsuarioCreacion NVARCHAR(255) NOT NULL,
	FechaModificacion DATETIME,
	UsuarioModificacion NVARCHAR(255),

	FOREIGN KEY (IdAutor) REFERENCES Autor(Id)
);
GO

-- Inserciones necesarias para las tablas base:

INSERT INTO TipoDocto (Tipo) VALUES ('Cédula de ciudadanía')
INSERT INTO TipoDocto (Tipo) VALUES ('NIT')
INSERT INTO TipoDocto (Tipo) VALUES ('Cédula de extranjería')



