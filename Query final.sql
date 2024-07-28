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

INSERT INTO catalogo_codigo_pais VALUES (57,'Colombia',1,GETDATE(),GETDATE())
INSERT INTO catalogo_tipo_identificacion VALUES (1,'Cedula de Ciudadania',1,GETDATE(),GETDATE())

--Selects
Select * from catalogo_codigo_pais
Select * from catalogo_tipo_identificacion
select * from clientes
select * from vw_clientes
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

