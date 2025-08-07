
CREATE PROCEDURE sp_CuentaCategoria_ObtenerPorId (@idCategoria UNIQUEIDENTIFIER,@idCuenta UNIQUEIDENTIFIER
)
AS
BEGIN
    SELECT * FROM CuentaCategoria WHERE idCategoria = @idCategoria AND idCuenta = @idCuenta;
END;