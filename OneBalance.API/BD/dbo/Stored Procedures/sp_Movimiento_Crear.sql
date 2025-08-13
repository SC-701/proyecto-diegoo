-- CREAR
CREATE PROCEDURE sp_Movimiento_Crear (@idMovimiento UNIQUEIDENTIFIER, @idCuenta UNIQUEIDENTIFIER, @idCategoria UNIQUEIDENTIFIER, @idTipoPago UNIQUEIDENTIFIER,
    @descripcion NVARCHAR(MAX), @montoOriginal DECIMAL(18,2), @fecha DATETIME, @estado BIT)
AS
BEGIN
    INSERT INTO Movimiento VALUES (@idMovimiento, @idCuenta, @idCategoria, @idTipoPago, @descripcion, @montoOriginal, @fecha, @estado);
	select @idMovimiento;
END;