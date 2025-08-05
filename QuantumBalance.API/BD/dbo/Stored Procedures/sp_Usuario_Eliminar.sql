
-- ELIMINAR
CREATE PROCEDURE sp_Usuario_Eliminar (@idUsuario UNIQUEIDENTIFIER)
AS
BEGIN
    DELETE FROM Usuario WHERE idUsuario = @idUsuario;
	select @idUsuario;
END;