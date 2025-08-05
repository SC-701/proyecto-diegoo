---------------------------
-- SP USUARIO --
---------------------------

-- CREAR
CREATE PROCEDURE sp_Usuario_Crear (@idUsuario UNIQUEIDENTIFIER, @nombre NVARCHAR(100), @primerApellido NVARCHAR(100), @segundoApellido NVARCHAR(100), @email NVARCHAR(200), @password NVARCHAR(200), @monedaPrincipal NVARCHAR(50),
    @fechaCreacion DATETIME, @fechaUltimoAcceso DATETIME, @estado BIT)
AS
BEGIN
    INSERT INTO Usuario VALUES (@idUsuario, @nombre, @primerApellido, @segundoApellido, @email, @password, @monedaPrincipal, @fechaCreacion, @fechaUltimoAcceso, @estado);
	select @idUsuario;
END;