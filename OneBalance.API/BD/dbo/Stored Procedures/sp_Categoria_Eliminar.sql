
-- ELIMINAR
CREATE PROCEDURE sp_Categoria_Eliminar (@idCategoria UNIQUEIDENTIFIER)
AS
BEGIN
    DELETE FROM Categoria WHERE idCategoria = @idCategoria;
END;