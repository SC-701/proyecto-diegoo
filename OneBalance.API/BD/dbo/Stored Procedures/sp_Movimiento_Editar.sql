
-- EDITAR
CREATE PROCEDURE sp_Movimiento_Editar (@idMovimiento UNIQUEIDENTIFIER, @idCategoria UNIQUEIDENTIFIER, @idTipoPago UNIQUEIDENTIFIER, @descripcion NVARCHAR(MAX), @montoOriginal DECIMAL(18,2),
    @monedaOriginal NVARCHAR(50), @montoCrc DECIMAL(18,2), @tasaCambio DECIMAL(18,6), @estado BIT)
AS
BEGIN
    UPDATE Movimiento SET idCategoria = @idCategoria, idTipoPago = @idTipoPago, descripcion = @descripcion, montoOriginal = @montoOriginal, monedaOriginal = @monedaOriginal, montoCrc = @montoCrc,
        tasaCambio = @tasaCambio, estado = @estado WHERE idMovimiento = @idMovimiento;
	select @idMovimiento;
END;