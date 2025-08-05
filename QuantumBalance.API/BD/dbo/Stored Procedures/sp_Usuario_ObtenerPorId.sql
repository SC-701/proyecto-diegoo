
-- OBTENER POR ID
CREATE PROCEDURE sp_Usuario_ObtenerPorId (@idUsuario UNIQUEIDENTIFIER)
AS
BEGIN
    SELECT * FROM Usuario WHERE idUsuario = @idUsuario;
END;