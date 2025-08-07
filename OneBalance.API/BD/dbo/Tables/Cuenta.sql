CREATE TABLE [dbo].[Cuenta] (
    [idCuenta]                UNIQUEIDENTIFIER NOT NULL,
    [idUsuario]               UNIQUEIDENTIFIER NULL,
    [nombre]                  NVARCHAR (100)   NULL,
    [descripcion]             NVARCHAR (MAX)   NULL,
    [permitirSalarioNegativo] BIT              NULL,
    [fechaCreacion]           DATETIME         NULL,
    [fechaUltimaModificacion] DATETIME         NULL,
    [estado]                  BIT              NULL,
    [idCategoria]             UNIQUEIDENTIFIER NULL,
    PRIMARY KEY CLUSTERED ([idCuenta] ASC),
    FOREIGN KEY ([idUsuario]) REFERENCES [dbo].[Usuario] ([idUsuario]),
    CONSTRAINT [fk_Cuenta_Categoria] FOREIGN KEY ([idCategoria]) REFERENCES [dbo].[Categoria] ([idCategoria])
);

