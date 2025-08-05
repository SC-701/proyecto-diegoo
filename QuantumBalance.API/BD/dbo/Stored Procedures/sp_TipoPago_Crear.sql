-- CREAR
CREATE PROCEDURE sp_TipoPago_Crear (@idTipoPago UNIQUEIDENTIFIER, @nombre NVARCHAR(100), @descripcion NVARCHAR(MAX), @activestado BIT)
AS
BEGIN
    INSERT INTO TipoPago VALUES (@idTipoPago, @nombre, @descripcion, @activestado);
	select @idTipoPago;
END;