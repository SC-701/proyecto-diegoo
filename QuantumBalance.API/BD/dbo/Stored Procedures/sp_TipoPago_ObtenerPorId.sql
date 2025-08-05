
-- OBTENER POR ID
CREATE PROCEDURE sp_TipoPago_ObtenerPorId (
    @idTipoPago UNIQUEIDENTIFIER
)
AS
BEGIN
    SELECT * FROM TipoPago WHERE idTipoPago = @idTipoPago;
END;