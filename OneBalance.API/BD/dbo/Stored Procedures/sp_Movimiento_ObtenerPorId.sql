
-- OBTENER POR ID
CREATE PROCEDURE sp_Movimiento_ObtenerPorId (@idMovimiento UNIQUEIDENTIFIER)
AS
BEGIN
    SELECT * FROM Movimiento WHERE idMovimiento = @idMovimiento;
END;