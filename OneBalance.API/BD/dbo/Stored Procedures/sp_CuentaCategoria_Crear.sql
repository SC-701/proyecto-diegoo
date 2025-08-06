CREATE PROCEDURE sp_CuentaCategoria_Crear (@idCategoria UNIQUEIDENTIFIER, @idCuenta UNIQUEIDENTIFIER)
AS
BEGIN
    INSERT INTO CuentaCategoria VALUES (@idCategoria, @idCuenta);
	select @idCategoria, @idCuenta;
END;