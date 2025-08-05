create database QuantumBalance
go

use QuantumBalance
go

CREATE TABLE Usuario (
    idUsuario UNIQUEIDENTIFIER PRIMARY KEY,
    nombre NVARCHAR(100),
    primerApellido NVARCHAR(100),
    segundoApellido NVARCHAR(100),
    email NVARCHAR(200),
    password NVARCHAR(200),
    monedaPrincipal NVARCHAR(50),
    fechaCreacion DATETIME,
    fechaUltimoAcceso DATETIME,
    estado BIT
);

CREATE TABLE Cuenta (
    idCuenta UNIQUEIDENTIFIER PRIMARY KEY,
    idUsuario UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Usuario(idUsuario),
    nombre NVARCHAR(100),
    descripcion NVARCHAR(MAX),
    idCategoria UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Categoria(idCategoria),
    permitirSalarioNegativo BIT,
    fechaCreacion DATETIME,
    fechaUltimaModificacion DATETIME,
    estado BIT
);

CREATE TABLE Categoria (
    idCategoria UNIQUEIDENTIFIER PRIMARY KEY,
    nombre NVARCHAR(100),
    descripcion NVARCHAR(MAX),
    fechaCreacion DATETIME,
    estado BIT
);

CREATE TABLE TipoPago (
    idTipoPago UNIQUEIDENTIFIER PRIMARY KEY,
    nombre NVARCHAR(100),
    descripcion NVARCHAR(MAX),
    activestado BIT
);

CREATE TABLE Movimiento (
    idMovimiento UNIQUEIDENTIFIER PRIMARY KEY,
    idCuenta UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Cuenta(idCuenta),
    idCategoria UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Categoria(idCategoria),
    idTipoPago UNIQUEIDENTIFIER FOREIGN KEY REFERENCES TipoPago(idTipoPago),
    descripcion NVARCHAR(MAX),
    montoOriginal DECIMAL(18,2),
    monedaOriginal NVARCHAR(50),
    montoCrc DECIMAL(18,2),
    tasaCambio DECIMAL(18,6),
    fecha DATETIME,
    comprobanteUrl NVARCHAR(MAX),
    estado BIT
);

CREATE TABLE Presupuestos (
    idPresupuesto UNIQUEIDENTIFIER PRIMARY KEY,
    idCuenta UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Cuenta(idCuenta),
    idCategoria UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Categoria(idCategoria),
    nombre NVARCHAR(100),
    montoLimite DECIMAL(18,2),
    montoGastado DECIMAL(18,2),
    fechaCreacion DATETIME,
    estado BIT
);

CREATE TABLE CuentaCategoria (
    idCategoria UNIQUEIDENTIFIER,
    idCuenta UNIQUEIDENTIFIER,
    PRIMARY KEY (idCategoria, idCuenta),
    FOREIGN KEY (idCategoria) REFERENCES Categoria(idCategoria),
    FOREIGN KEY (idCuenta) REFERENCES Cuenta(idCuenta)
);
