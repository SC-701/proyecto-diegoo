
-- EDITAR
CREATE PROCEDURE sp_Movimiento_Editar (@idMovimiento UNIQUEIDENTIFIER, @idCategoria UNIQUEIDENTIFIER, @idTipoPago UNIQUEIDENTIFIER, @descripcion NVARCHAR(MAX), 
    @montoOriginal DECIMAL(18,2), @estado BIT)
AS
BEGIN
    UPDATE Movimiento SET idCategoria = @idCategoria, idTipoPago = @idTipoPago, descripcion = @descripcion, montoOriginal = @montoOriginal, 
        estado = @estado WHERE idMovimiento = @idMovimiento;
	select @idMovimiento;
END;