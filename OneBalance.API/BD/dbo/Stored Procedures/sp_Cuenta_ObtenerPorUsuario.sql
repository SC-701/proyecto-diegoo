CREATE PROCEDURE sp_Cuenta_ObtenerPorUsuario
    @idUsuario UNIQUEIDENTIFIER
AS
BEGIN
    SELECT *
    FROM Cuenta
    WHERE idUsuario = @idUsuario;
END;