
-- EDITAR
CREATE PROCEDURE sp_Categoria_Editar (@idCategoria UNIQUEIDENTIFIER, @nombre NVARCHAR(100), @descripcion NVARCHAR(MAX), @estado BIT)
AS
BEGIN
    UPDATE Categoria SET nombre = @nombre, descripcion = @descripcion, estado = @estado WHERE idCategoria = @idCategoria;
	SELECT @idCategoria;
END;