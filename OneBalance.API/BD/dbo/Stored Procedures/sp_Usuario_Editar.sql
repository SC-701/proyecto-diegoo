
-- EDITAR
CREATE PROCEDURE [dbo].[sp_Usuario_Editar] (@idUsuario UNIQUEIDENTIFIER, @nombre NVARCHAR(100), @primerApellido NVARCHAR(100), @segundoApellido NVARCHAR(100),@email NVARCHAR(200), @password NVARCHAR(200), @monedaPrincipal NVARCHAR(50),
    @fechaUltimoAcceso DATETIME, @estado BIT)
AS
BEGIN
    UPDATE Usuario SET nombre = @nombre, primerApellido = @primerApellido, segundoApellido = @segundoApellido, email = @email, password = @password, monedaPrincipal = @monedaPrincipal, fechaUltimoAcceso = @fechaUltimoAcceso, estado = @estado WHERE idUsuario = @idUsuario;
	select @idUsuario;
END;