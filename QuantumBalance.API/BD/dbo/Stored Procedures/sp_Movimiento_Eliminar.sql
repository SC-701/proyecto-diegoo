
-- ELIMINAR
CREATE PROCEDURE sp_Movimiento_Eliminar (@idMovimiento UNIQUEIDENTIFIER)
AS
BEGIN
    DELETE FROM Movimiento WHERE idMovimiento = @idMovimiento;
END;