
-- ELIMINAR
CREATE PROCEDURE sp_TipoPago_Eliminar (@idTipoPago UNIQUEIDENTIFIER)
AS
BEGIN
    DELETE FROM TipoPago WHERE idTipoPago = @idTipoPago;
	select @idTipoPago;
END;