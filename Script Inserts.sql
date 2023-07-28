Use DBCARRITO
Go

Insert into USUARIO (Nombres, Apellidos, Correo, Clave) Values 
	('TestNombre', 'TestApellido', 'test@example', 'ecd71870d1963316a97e3ac3408c9835ad8cf0f3c1bc703527c30265534f75ae'),
	('Test 02', 'User 02', 'user02@example', 'e1c10806027637577653a25120e4a8437be5207c2e250181823a4fc323eaee39')
Go

Insert into CATEGORIA(Descripcion) Values 
	('Tecnologia'),
	('Muebles'),
	('Domitorio'),
	('Deportes')
Go

Insert into MARCA(Descripcion) Values 
	('Sony'),
	('HP'),
	('LG'),
	('Hyundai'),
	('Canon'),
	('Roberta Allente')
Go

Insert into PROVINCIA(IdProvincia, Descripcion) values 
	(1, 'Buenos Aires'),
	(2, 'Córdoba'),
	(3, 'Mendoza'),
	(4, 'Santa Fe'),
	(5, 'Tucumán'),
	(6, 'Entre Ríos'),
	(7, 'Salta'),
	(8, 'Chaco'),
	(9, 'Corrientes'),
	(10, 'Misiones'),
	(12, 'San Juan'),
	(13, 'Jujuy'),
	(14, 'Río Negro'),
	(15, 'Neuquén'),
	(16, 'Chubut')
Go

Insert into LOCALIDAD(IdLocalidad, Descripcion, IdProvincia) Values
	(1, 'Palermo', 1),
	(2, 'Belgrano', 1),
	(3, 'San Fernando', 1)
Go