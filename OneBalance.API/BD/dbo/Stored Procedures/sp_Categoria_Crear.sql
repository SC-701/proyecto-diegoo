CREATE PROCEDURE sp_Categoria_Crear (@idCategoria UNIQUEIDENTIFIER, @nombre NVARCHAR(100), @descripcion NVARCHAR(MAX), @fechaCreacion DATETIME, @estado BIT)
AS
BEGIN
    INSERT INTO Categoria VALUES (@idCategoria, @nombre, @descripcion, @fechaCreacion, @estado);
	SELECT @idCategoria;
END;