
CREATE PROCEDURE sp_CuentaCategoria_Eliminar (@idCategoria UNIQUEIDENTIFIER, @idCuenta UNIQUEIDENTIFIER)
AS
BEGIN
    DELETE FROM CuentaCategoria WHERE idCategoria = @idCategoria AND idCuenta = @idCuenta;
	select @idCategoria, @idCuenta;
END;