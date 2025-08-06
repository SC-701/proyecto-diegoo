CREATE TABLE [dbo].[Categoria] (
    [idCategoria]   UNIQUEIDENTIFIER NOT NULL,
    [nombre]        NVARCHAR (100)   NULL,
    [descripcion]   NVARCHAR (MAX)   NULL,
    [fechaCreacion] DATETIME         NULL,
    [estado]        BIT              NULL,
    PRIMARY KEY CLUSTERED ([idCategoria] ASC)
);

