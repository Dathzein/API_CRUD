CREATE DATABASE Api_Crud
USE Api_Crud

--Tablas

CREATE TABLE catalogo_tipo_identificacion(
	id INT IDENTITY PRIMARY KEY,
	codigo_identificacion INT NOT NULL ,
	tipo_identificacion NVARCHAR(30)  NOT NULL,
	active BIT NOT NULL,
	fecha_control DATETIME2 NOT NULL,
	fecha_actualizacion DATETIME2,
)

CREATE INDEX idx_tipo_identificacion ON catalogo_tipo_identificacion(tipo_identificacion)

CREATE TABLE catalogo_codigo_pais(
	id INT IDENTITY PRIMARY KEY,
	codigo_pais INT NOT NULL ,
	pais NVARCHAR(50)  NOT NULL,
	active BIT NOT NULL,
	fecha_control DATETIME2 NOT NULL,
	fecha_actualizacion DATETIME2,
)

CREATE INDEX idx_pais ON catalogo_codigo_pais(pais)

CREATE TABLE clientes(
	id INT IDENTITY PRIMARY KEY,
	nombre NVARCHAR(MAX) NOT NULL,
	codigo_identificacion INT NOT NULL,
	numero_identificacion nvarchar(100) NOT NULL,
	correo_electronico NVARCHAR(MAX) NOT NULL,
	edad INT NOT NULL,
	codigo_pais INT NOT NULL,
	numero_telefono NVARCHAR(13) NOT NULL,
	active BIT NOT NULL,
	fecha_control DATETIME2 NOT NULL,
	fecha_actualizacion DATETIME2,
	FOREIGN KEY (codigo_identificacion) REFERENCES catalogo_tipo_identificacion(id),
	FOREIGN KEY (codigo_pais) REFERENCES catalogo_codigo_pais(id)
)

CREATE INDEX idx_numero_identificacion ON clientes(numero_identificacion)

CREATE TABLE roles(
	id INT IDENTITY PRIMARY KEY,
	role_nombre NVARCHAR(100) NOT NULL,
	active BIT NOT NULL,
	fecha_control DATETIME2 NOT NULL,
	fecha_actualizacion DATETIME2,
)

CREATE INDEX idx_role_nombre ON roles(role_nombre)

CREATE TABLE usuario(
	id INT IDENTITY PRIMARY KEY,
	usuario NVARCHAR(MAX) NOT NULL,
	contrasena NVARCHAR(MAX)NOT NULL,
	codigo_identificacion INT NOT NULL,
	numero_identificacion NVARCHAR(100) NOT NULL,
	active BIT NOT NULL,
	id_role INT NOT NULL,
	fecha_control DATETIME2 NOT NULL,
	fecha_actualizacion DATETIME2,
	FOREIGN KEY (codigo_identificacion) REFERENCES catalogo_tipo_identificacion(id),
	FOREIGN KEY (id_role) REFERENCES roles(id),
)
CREATE INDEX idx_numero_identificacion ON usuario(numero_identificacion)

CREATE TABLE logs(
	id INT IDENTITY PRIMARY KEY,
	id_usuario INT NOT NULL,
	metodo NVARCHAR(100) NOT NULL,
	excepcion NVARCHAR(MAX),
	fecha_control DATETIME2 NOT NULL,
	FOREIGN KEY (id_usuario) REFERENCES usuario(id),
)

--vistas
CREATE VIEW vw_clientes
AS
	SELECT 
	a.id,
	a.nombre, 
	c.tipo_identificacion,
	a.numero_identificacion,
	a.correo_electronico,
	a.edad,
	b.pais,
	a.numero_telefono,
	CASE WHEN a.active = 1 THEN 'Activo' ELSE 'Inactivo' END as Estado
	FROM clientes a 
	INNER JOIN catalogo_codigo_pais b ON b.id = a.codigo_pais
	INNER JOIN catalogo_tipo_identificacion c ON c.codigo_identificacion = a.codigo_identificacion WHERE a.active = 1
Select * from catalogo_codigo_pais
Select * from catalogo_tipo_identificacion
select * from roles
select * from usuario
select * from logs
select * from clientes
select * from vw_clientes

--INSERTS
INSERT INTO catalogo_codigo_pais VALUES (57,'Colombia',1,GETDATE(),GETDATE())
INSERT INTO catalogo_tipo_identificacion VALUES (1,'Cedula de Ciudadania',1,GETDATE(),GETDATE())
INSERT INTO roles VALUES ('administrador',1,GETDATE(),GETDATE())
INSERT INTO roles VALUES ('registrador',1,GETDATE(),GETDATE())

--USUARIOS
INSERT INTO usuario VALUES ('adm','12345',1,'123465',1,1,GETDATE(),GETDATE())
INSERT INTO usuario VALUES ('prueba1','12346',1,'123456',1,2,GETDATE(),GETDATE())

--CLIENTES
INSERT INTO usuario VALUES ('JUAN GONZALES', 1, 102345609876, 'PRUEBAS@PRUEBAS.COM',25,1,'30055566699',1,,GETDATE(),GETDATE())
INSERT INTO usuario VALUES ('JUAN PEREZ', 1, 10987612345, 'PRUEBAS@PRUEBAS.COM',20,1,'30055566699',1,GETDATE(),GETDATE())


