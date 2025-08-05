
-- OBTENER POR ID
CREATE PROCEDURE sp_Presupuestos_ObtenerPorId (@idPresupuesto UNIQUEIDENTIFIER)
AS
BEGIN
    SELECT * FROM Presupuestos WHERE idPresupuesto = @idPresupuesto;
END;