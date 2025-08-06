CREATE PROCEDURE [dbo].[sp_Cuenta_Crear] (@idCuenta UNIQUEIDENTIFIER, @idUsuario UNIQUEIDENTIFIER, @nombre NVARCHAR(100), @descripcion NVARCHAR(MAX), @permitirSalarioNegativo BIT, 
@fechaCreacion DATETIME,
    @fechaUltimaModificacion DATETIME, @estado BIT, @idCategoria UNIQUEIDENTIFIER)
AS
BEGIN
    INSERT INTO Cuenta VALUES (@idCuenta, @idUsuario, @nombre, @descripcion, @permitirSalarioNegativo, @fechaCreacion, @fechaUltimaModificacion, @estado, @idCategoria);
	select @idCuenta;
END;