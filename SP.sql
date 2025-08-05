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
GO

-- EDITAR
CREATE PROCEDURE sp_Usuario_Editar (@idUsuario UNIQUEIDENTIFIER, @nombre NVARCHAR(100), @primerApellido NVARCHAR(100), @segundoApellido NVARCHAR(100),@email NVARCHAR(200), @password NVARCHAR(200), @monedaPrincipal NVARCHAR(50),
    @fechaUltimoAcceso DATETIME, @estado BIT)
AS
BEGIN
    UPDATE Usuario SET nombre = @nombre, primerApellido = @primerApellido, segundoApellido = @segundoApellido, email = @email, password = @password, monedaPrincipal = @monedaPrincipal, fechaUltimoAcceso = @fechaUltimoAcceso, 
	estado = @estado WHERE idUsuario = @idUsuario;
	select @idUsuario;
END;
GO

-- ELIMINAR
CREATE PROCEDURE sp_Usuario_Eliminar (@idUsuario UNIQUEIDENTIFIER)
AS
BEGIN
    DELETE FROM Usuario WHERE idUsuario = @idUsuario;
	select @idUsuario;
END;
GO


-- OBTENER TODOS
CREATE PROCEDURE sp_Usuario_ObtenerTodos
AS
BEGIN
    SELECT * FROM Usuario;
END;
GO

-- OBTENER POR ID
CREATE PROCEDURE sp_Usuario_ObtenerPorId (@idUsuario UNIQUEIDENTIFIER)
AS
BEGIN
    SELECT * FROM Usuario WHERE idUsuario = @idUsuario;
END;
GO

---------------------------
-- SP CUENTA --
---------------------------

-- CREAR
CREATE PROCEDURE sp_Cuenta_Crear (@idCuenta UNIQUEIDENTIFIER, @idUsuario UNIQUEIDENTIFIER, @nombre NVARCHAR(100), @descripcion NVARCHAR(MAX), @idCategoria UNIQUEIDENTIFIER, @permitirSalarioNegativo BIT, @fechaCreacion DATETIME,
    @fechaUltimaModificacion DATETIME, @estado BIT)
AS
BEGIN
    INSERT INTO Cuenta VALUES (@idCuenta, @idUsuario, @nombre, @descripcion, idCategoria, @permitirSalarioNegativo, @fechaCreacion, @fechaUltimaModificacion, @estado);
	select @idCuenta;
END;
GO

-- EDITAR
CREATE PROCEDURE sp_Cuenta_Editar (@idCuenta UNIQUEIDENTIFIER, @nombre NVARCHAR(100), @descripcion NVARCHAR(MAX), @idCategoria UNIQUEIDENTIFIER, @permitirSalarioNegativo BIT, @fechaUltimaModificacion DATETIME, @estado BIT)
AS
BEGIN
    UPDATE Cuenta SET nombre = @nombre, descripcion = @descripcion, idCategoria = @idCategoria, permitirSalarioNegativo = @permitirSalarioNegativo, fechaUltimaModificacion = @fechaUltimaModificacion,
        estado = @estado WHERE idCuenta = @idCuenta;
	select @idCuenta;
END;
GO

-- ELIMINAR
CREATE PROCEDURE sp_Cuenta_Eliminar (@idCuenta UNIQUEIDENTIFIER)
AS
BEGIN
    DELETE FROM Cuenta WHERE idCuenta = @idCuenta;
	select @idCuenta;
END;
GO

-- OBTENER TODOS
CREATE PROCEDURE sp_Cuenta_ObtenerTodos
AS
BEGIN
    SELECT * FROM Cuenta;
END;
GO

-- OBTENER POR ID
CREATE PROCEDURE sp_Cuenta_ObtenerPorId (@idCuenta UNIQUEIDENTIFIER)
AS
BEGIN
    SELECT * FROM Cuenta WHERE idCuenta = @idCuenta;
END;
GO

---------------------------
-- SP CATEGORIA --
---------------------------

-- CREAR
CREATE PROCEDURE sp_Categoria_Crear (@idCategoria UNIQUEIDENTIFIER, @nombre NVARCHAR(100), @descripcion NVARCHAR(MAX), @fechaCreacion DATETIME, @estado BIT)
AS
BEGIN
    INSERT INTO Categoria VALUES (@idCategoria, @nombre, @descripcion, @fechaCreacion, @estado);
	SELECT @idCategoria;
END;
GO

-- EDITAR
CREATE PROCEDURE sp_Categoria_Editar (@idCategoria UNIQUEIDENTIFIER, @nombre NVARCHAR(100), @descripcion NVARCHAR(MAX), @estado BIT)
AS
BEGIN
    UPDATE Categoria SET nombre = @nombre, descripcion = @descripcion, estado = @estado WHERE idCategoria = @idCategoria;
	SELECT @idCategoria;
END;
GO

-- ELIMINAR
CREATE PROCEDURE sp_Categoria_Eliminar (@idCategoria UNIQUEIDENTIFIER)
AS
BEGIN
    DELETE FROM Categoria WHERE idCategoria = @idCategoria;
	select @idCategoria;
END;
GO

-- OBTENER TODOS
CREATE PROCEDURE sp_Categoria_ObtenerTodos
AS
BEGIN
    SELECT * FROM Categoria;
END;
GO

-- OBTENER POR ID
CREATE PROCEDURE sp_Categoria_ObtenerPorId (
    @idCategoria UNIQUEIDENTIFIER
)
AS
BEGIN
    SELECT * FROM Categoria WHERE idCategoria = @idCategoria;
END;
GO

---------------------------
-- SP TIPO DE PAGO --
---------------------------

-- CREAR
CREATE PROCEDURE sp_TipoPago_Crear (@idTipoPago UNIQUEIDENTIFIER, @nombre NVARCHAR(100), @descripcion NVARCHAR(MAX), @activestado BIT)
AS
BEGIN
    INSERT INTO TipoPago VALUES (@idTipoPago, @nombre, @descripcion, @activestado);
	select @idTipoPago;
END;
GO

-- EDITAR
CREATE PROCEDURE sp_TipoPago_Editar (@idTipoPago UNIQUEIDENTIFIER, @nombre NVARCHAR(100), @descripcion NVARCHAR(MAX), @activestado BIT)
AS
BEGIN
    UPDATE TipoPago SET nombre = @nombre, descripcion = @descripcion, activestado = @activestado WHERE idTipoPago = @idTipoPago;
	select @idTipoPago;
END;
GO

-- ELIMINAR
CREATE PROCEDURE sp_TipoPago_Eliminar (@idTipoPago UNIQUEIDENTIFIER)
AS
BEGIN
    DELETE FROM TipoPago WHERE idTipoPago = @idTipoPago;
	select @idTipoPago;
END;
GO

-- OBTENER TODOS
CREATE PROCEDURE sp_TipoPago_ObtenerTodos
AS
BEGIN
    SELECT * FROM TipoPago;
END;
GO

-- OBTENER POR ID
CREATE PROCEDURE sp_TipoPago_ObtenerPorId (
    @idTipoPago UNIQUEIDENTIFIER
)
AS
BEGIN
    SELECT * FROM TipoPago WHERE idTipoPago = @idTipoPago;
END;
GO

---------------------------
-- SP MOVIMIENTO --
---------------------------

-- CREAR
CREATE PROCEDURE sp_Movimiento_Crear (@idMovimiento UNIQUEIDENTIFIER, @idCuenta UNIQUEIDENTIFIER, @idCategoria UNIQUEIDENTIFIER, @idTipoPago UNIQUEIDENTIFIER, @descripcion NVARCHAR(MAX), @montoOriginal DECIMAL(18,2),
    @monedaOriginal NVARCHAR(50), @montoCrc DECIMAL(18,2), @tasaCambio DECIMAL(18,6), @fecha DATETIME, @comprobanteUrl NVARCHAR(MAX), @estado BIT)
AS
BEGIN
    INSERT INTO Movimiento VALUES (@idMovimiento, @idCuenta, @idCategoria, @idTipoPago, @descripcion, @montoOriginal, @monedaOriginal, @montoCrc, @tasaCambio, @fecha, @comprobanteUrl, @estado);
	select @idMovimiento;
END;
GO

-- EDITAR
CREATE PROCEDURE sp_Movimiento_Editar (@idMovimiento UNIQUEIDENTIFIER, @idCategoria UNIQUEIDENTIFIER, @idTipoPago UNIQUEIDENTIFIER, @descripcion NVARCHAR(MAX), @montoOriginal DECIMAL(18,2),
    @monedaOriginal NVARCHAR(50), @montoCrc DECIMAL(18,2), @tasaCambio DECIMAL(18,6), @estado BIT)
AS
BEGIN
    UPDATE Movimiento SET idCategoria = @idCategoria, idTipoPago = @idTipoPago, descripcion = @descripcion, montoOriginal = @montoOriginal, monedaOriginal = @monedaOriginal, montoCrc = @montoCrc,
        tasaCambio = @tasaCambio, estado = @estado WHERE idMovimiento = @idMovimiento;
	select @idMovimiento;
END;
GO

-- ELIMINAR
CREATE PROCEDURE sp_Movimiento_Eliminar (@idMovimiento UNIQUEIDENTIFIER)
AS
BEGIN
    DELETE FROM Movimiento WHERE idMovimiento = @idMovimiento;
END;
GO

-- OBTENER TODOS
CREATE PROCEDURE sp_Movimiento_ObtenerTodos
AS
BEGIN
    SELECT * FROM Movimiento;
END;
GO

-- OBTENER POR ID
CREATE PROCEDURE sp_Movimiento_ObtenerPorId (@idMovimiento UNIQUEIDENTIFIER)
AS
BEGIN
    SELECT * FROM Movimiento WHERE idMovimiento = @idMovimiento;
END;
GO

---------------------------
-- SP PRESUPUESTO --
---------------------------

-- CREAR 
CREATE PROCEDURE sp_Presupuestos_Crear (@idPresupuesto UNIQUEIDENTIFIER, @idCuenta UNIQUEIDENTIFIER, @idCategoria UNIQUEIDENTIFIER, @nombre NVARCHAR(100), @montoLimite DECIMAL(18,2), @montoGastado DECIMAL(18,2), @fechaCreacion DATETIME,
    @estado BIT)
AS
BEGIN
    INSERT INTO Presupuestos VALUES (@idPresupuesto, @idCuenta, @idCategoria, @nombre, @montoLimite, @montoGastado, @fechaCreacion, @estado);
	select @idPresupuesto;
END;
GO

-- EDITAR
CREATE PROCEDURE sp_Presupuestos_Editar (@idPresupuesto UNIQUEIDENTIFIER, @idCuenta UNIQUEIDENTIFIER, @idCategoria UNIQUEIDENTIFIER, @nombre NVARCHAR(100), @montoLimite DECIMAL(18,2), @montoGastado DECIMAL(18,2), @estado BIT)
AS
BEGIN
    UPDATE Presupuestos SET idCuenta = @idCuenta, idCategoria = @idCategoria, nombre = @nombre, montoLimite = @montoLimite, montoGastado = @montoGastado, estado = @estado WHERE idPresupuesto = @idPresupuesto;
	select @idPresupuesto;
END;
GO

-- ELIMINAR
CREATE PROCEDURE sp_Presupuestos_Eliminar (@idPresupuesto UNIQUEIDENTIFIER)
AS
BEGIN
    DELETE FROM Presupuestos WHERE idPresupuesto = @idPresupuesto;
	select @idPresupuesto;
END;
GO

-- OBTENER TODOS
CREATE PROCEDURE sp_Presupuestos_ObtenerTodos
AS
BEGIN
    SELECT * FROM Presupuestos;
END;
GO

-- OBTENER POR ID
CREATE PROCEDURE sp_Presupuestos_ObtenerPorId (@idPresupuesto UNIQUEIDENTIFIER)
AS
BEGIN
    SELECT * FROM Presupuestos WHERE idPresupuesto = @idPresupuesto;
END;
GO

---------------------------
-- SP CUENTA CATEGORIA --
---------------------------

CREATE PROCEDURE sp_CuentaCategoria_Crear (@idCategoria UNIQUEIDENTIFIER, @idCuenta UNIQUEIDENTIFIER)
AS
BEGIN
    INSERT INTO CuentaCategoria VALUES (@idCategoria, @idCuenta);
	select @idCategoria, @idCuenta;
END;
GO

CREATE PROCEDURE sp_CuentaCategoria_Eliminar (@idCategoria UNIQUEIDENTIFIER, @idCuenta UNIQUEIDENTIFIER)
AS
BEGIN
    DELETE FROM CuentaCategoria WHERE idCategoria = @idCategoria AND idCuenta = @idCuenta;
	select @idCategoria, @idCuenta;
END;
GO

CREATE PROCEDURE sp_CuentaCategoria_ObtenerTodos
AS
BEGIN
    SELECT * FROM CuentaCategoria;
END;
GO

CREATE PROCEDURE sp_CuentaCategoria_ObtenerPorId (@idCategoria UNIQUEIDENTIFIER,@idCuenta UNIQUEIDENTIFIER
)
AS
BEGIN
    SELECT * FROM CuentaCategoria WHERE idCategoria = @idCategoria AND idCuenta = @idCuenta;
END;
GO
