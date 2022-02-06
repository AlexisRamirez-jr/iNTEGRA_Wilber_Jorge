
--Crear base de datos.

Create database Prueba_iNTEGRA
USE Prueba_iNTEGRA
GO

--tabla para registrar empleados.
create table empleados
(
	id_empleado int identity(1,1),
	apellidos varchar(60) not null,
	nombres varchar(60) not null,
	telefono varchar(20) not null unique,
	correo varchar(100) not null unique,
	foto nvarchar(150) not null,
	fecha_contratacion date not null,
	primary key (id_empleado)
)

-- select * from empleados
