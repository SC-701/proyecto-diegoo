CREATE TABLE [dbo].[Movimiento] (
    [idMovimiento]   UNIQUEIDENTIFIER NOT NULL,
    [idCuenta]       UNIQUEIDENTIFIER NULL,
    [idCategoria]    UNIQUEIDENTIFIER NULL,
    [idTipoPago]     UNIQUEIDENTIFIER NULL,
    [descripcion]    NVARCHAR (MAX)   NULL,
    [montoOriginal]  DECIMAL (18, 2)  NULL,
    [fecha]          DATETIME         NULL,    
    [estado]         BIT              NULL,
    PRIMARY KEY CLUSTERED ([idMovimiento] ASC),
    FOREIGN KEY ([idCategoria]) REFERENCES [dbo].[Categoria] ([idCategoria]),
    FOREIGN KEY ([idCuenta]) REFERENCES [dbo].[Cuenta] ([idCuenta]),
    FOREIGN KEY ([idTipoPago]) REFERENCES [dbo].[TipoPago] ([idTipoPago])
);

