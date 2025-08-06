
-- EDITAR
CREATE PROCEDURE sp_Presupuestos_Editar (@idPresupuesto UNIQUEIDENTIFIER, @idCuenta UNIQUEIDENTIFIER, @idCategoria UNIQUEIDENTIFIER, @nombre NVARCHAR(100), @montoLimite DECIMAL(18,2), @montoGastado DECIMAL(18,2), @estado BIT)
AS
BEGIN
    UPDATE Presupuestos SET idCuenta = @idCuenta, idCategoria = @idCategoria, nombre = @nombre, montoLimite = @montoLimite, montoGastado = @montoGastado, estado = @estado WHERE idPresupuesto = @idPresupuesto;
	select @idPresupuesto;
END;