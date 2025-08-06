CREATE TABLE [dbo].[Presupuestos] (
    [idPresupuesto] UNIQUEIDENTIFIER NOT NULL,
    [idCuenta]      UNIQUEIDENTIFIER NULL,
    [idCategoria]   UNIQUEIDENTIFIER NULL,
    [nombre]        NVARCHAR (100)   NULL,
    [montoLimite]   DECIMAL (18, 2)  NULL,
    [montoGastado]  DECIMAL (18, 2)  NULL,
    [fechaCreacion] DATETIME         NULL,
    [estado]        BIT              NULL,
    PRIMARY KEY CLUSTERED ([idPresupuesto] ASC),
    FOREIGN KEY ([idCategoria]) REFERENCES [dbo].[Categoria] ([idCategoria]),
    FOREIGN KEY ([idCuenta]) REFERENCES [dbo].[Cuenta] ([idCuenta])
);

