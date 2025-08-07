-- CREAR 
CREATE PROCEDURE sp_Presupuestos_Crear (@idPresupuesto UNIQUEIDENTIFIER, @idCuenta UNIQUEIDENTIFIER, @idCategoria UNIQUEIDENTIFIER, @nombre NVARCHAR(100), @montoLimite DECIMAL(18,2), @montoGastado DECIMAL(18,2), @fechaCreacion DATETIME,
    @estado BIT)
AS
BEGIN
    INSERT INTO Presupuestos VALUES (@idPresupuesto, @idCuenta, @idCategoria, @nombre, @montoLimite, @montoGastado, @fechaCreacion, @estado);
	select @idPresupuesto;
END;