CREATE TABLE [dbo].[CuentaCategoria] (
    [idCategoria] UNIQUEIDENTIFIER NOT NULL,
    [idCuenta]    UNIQUEIDENTIFIER NOT NULL,
    PRIMARY KEY CLUSTERED ([idCategoria] ASC, [idCuenta] ASC),
    FOREIGN KEY ([idCategoria]) REFERENCES [dbo].[Categoria] ([idCategoria]),
    FOREIGN KEY ([idCuenta]) REFERENCES [dbo].[Cuenta] ([idCuenta])
);

