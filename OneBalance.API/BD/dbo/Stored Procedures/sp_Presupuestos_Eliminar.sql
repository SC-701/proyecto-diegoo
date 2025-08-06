
-- ELIMINAR
CREATE PROCEDURE sp_Presupuestos_Eliminar (@idPresupuesto UNIQUEIDENTIFIER)
AS
BEGIN
    DELETE FROM Presupuestos WHERE idPresupuesto = @idPresupuesto;
	select @idPresupuesto;
END;