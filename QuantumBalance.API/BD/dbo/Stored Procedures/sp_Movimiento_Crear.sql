-- CREAR
CREATE PROCEDURE sp_Movimiento_Crear (@idMovimiento UNIQUEIDENTIFIER, @idCuenta UNIQUEIDENTIFIER, @idCategoria UNIQUEIDENTIFIER, @idTipoPago UNIQUEIDENTIFIER, @descripcion NVARCHAR(MAX), @montoOriginal DECIMAL(18,2),
    @monedaOriginal NVARCHAR(50), @montoCrc DECIMAL(18,2), @tasaCambio DECIMAL(18,6), @fecha DATETIME, @comprobanteUrl NVARCHAR(MAX), @estado BIT)
AS
BEGIN
    INSERT INTO Movimiento VALUES (@idMovimiento, @idCuenta, @idCategoria, @idTipoPago, @descripcion, @montoOriginal, @monedaOriginal, @montoCrc, @tasaCambio, @fecha, @comprobanteUrl, @estado);
	select @idMovimiento;
END;