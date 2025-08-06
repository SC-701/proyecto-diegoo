CREATE TABLE [dbo].[Movimiento] (
    [idMovimiento]   UNIQUEIDENTIFIER NOT NULL,
    [idCuenta]       UNIQUEIDENTIFIER NULL,
    [idCategoria]    UNIQUEIDENTIFIER NULL,
    [idTipoPago]     UNIQUEIDENTIFIER NULL,
    [descripcion]    NVARCHAR (MAX)   NULL,
    [montoOriginal]  DECIMAL (18, 2)  NULL,
    [monedaOriginal] NVARCHAR (50)    NULL,
    [montoCrc]       DECIMAL (18, 2)  NULL,
    [tasaCambio]     DECIMAL (18, 6)  NULL,
    [fecha]          DATETIME         NULL,
    [comprobanteUrl] NVARCHAR (MAX)   NULL,
    [estado]         BIT              NULL,
    PRIMARY KEY CLUSTERED ([idMovimiento] ASC),
    FOREIGN KEY ([idCategoria]) REFERENCES [dbo].[Categoria] ([idCategoria]),
    FOREIGN KEY ([idCuenta]) REFERENCES [dbo].[Cuenta] ([idCuenta]),
    FOREIGN KEY ([idTipoPago]) REFERENCES [dbo].[TipoPago] ([idTipoPago])
);

