
CREATE PROCEDURE sp_Movimiento_ObtenerUltimos5PorCuenta
    @idCuenta UNIQUEIDENTIFIER
AS
BEGIN
    SELECT *
    FROM Movimiento
    WHERE idCuenta = @idCuenta
    ORDER BY Fecha DESC;
END;