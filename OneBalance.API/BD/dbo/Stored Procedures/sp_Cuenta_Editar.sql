
CREATE PROCEDURE [dbo].[sp_Cuenta_Editar] (@idCuenta UNIQUEIDENTIFIER, @nombre NVARCHAR(100), @descripcion NVARCHAR(MAX), @permitirSalarioNegativo BIT, @fechaUltimaModificacion DATETIME, @estado BIT, @idCategoria UNIQUEIDENTIFIER)
AS
BEGIN
    UPDATE Cuenta SET nombre = @nombre, descripcion = @descripcion, permitirSalarioNegativo = @permitirSalarioNegativo, fechaUltimaModificacion = @fechaUltimaModificacion,
        estado = @estado, idCategoria = @idCategoria WHERE idCuenta = @idCuenta;
	select @idCuenta;
END;