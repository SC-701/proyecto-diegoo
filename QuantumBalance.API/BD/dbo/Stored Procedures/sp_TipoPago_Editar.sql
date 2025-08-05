
-- EDITAR
CREATE PROCEDURE sp_TipoPago_Editar (@idTipoPago UNIQUEIDENTIFIER, @nombre NVARCHAR(100), @descripcion NVARCHAR(MAX), @activestado BIT)
AS
BEGIN
    UPDATE TipoPago SET nombre = @nombre, descripcion = @descripcion, activestado = @activestado WHERE idTipoPago = @idTipoPago;
	select @idTipoPago;
END;