CREATE TABLE [dbo].[TipoPago] (
    [idTipoPago]  UNIQUEIDENTIFIER NOT NULL,
    [nombre]      NVARCHAR (100)   NULL,
    [descripcion] NVARCHAR (MAX)   NULL,
    [activestado] BIT              NULL,
    PRIMARY KEY CLUSTERED ([idTipoPago] ASC)
);

