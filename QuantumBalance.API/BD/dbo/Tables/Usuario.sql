CREATE TABLE [dbo].[Usuario] (
    [idUsuario]         UNIQUEIDENTIFIER NOT NULL,
    [nombre]            NVARCHAR (100)   NULL,
    [primerApellido]    NVARCHAR (100)   NULL,
    [segundoApellido]   NVARCHAR (100)   NULL,
    [email]             NVARCHAR (200)   NULL,
    [password]          NVARCHAR (200)   NULL,
    [monedaPrincipal]   NVARCHAR (50)    NULL,
    [fechaCreacion]     DATETIME         NULL,
    [fechaUltimoAcceso] DATETIME         NULL,
    [estado]            BIT              NULL,
    PRIMARY KEY CLUSTERED ([idUsuario] ASC)
);

