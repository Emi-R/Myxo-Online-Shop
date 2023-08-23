Use DBCARRITO
Go

Insert into USUARIO (Nombres, Apellidos, Correo, Clave) Values 
	('TestNombre', 'TestApellido', 'test@example', 'ecd71870d1963316a97e3ac3408c9835ad8cf0f3c1bc703527c30265534f75ae'),
	('Test 02', 'User 02', 'user02@example', 'e1c10806027637577653a25120e4a8437be5207c2e250181823a4fc323eaee39')
Go

Insert into CATEGORIA(Descripcion) Values 
	('Automóviles'),
	('Mascotas'),
	('Juguetes'),
	('Libros'),
	('Deportes'),
	('Hogar'),
	('Tecnologia'),
	('Muebles'),
	('Deportes')
Go

Insert into MARCA(Descripcion) Values 
	('Sony'),
	('HP'),
	('LG'),
	('Hyundai'),
	('Canon'),
	('Phillips'),
	('Panasonic')
Go

Insert into PROVINCIA(IdProvincia, Descripcion) values 
	(1, 'Buenos Aires'),
	(2, 'Cordoba'),
	(3, 'Mendoza'),
	(4, 'Santa Fe'),
	(5, 'Tucuman'),
	(6, 'Entre Rios'),
	(7, 'Salta'),
	(8, 'Chaco'),
	(9, 'Corrientes'),
	(10, 'Misiones'),
	(12, 'San Juan'),
	(13, 'Jujuy'),
	(14, 'Rio Negro'),
	(15, 'Neuquen'),
	(16, 'Chubut')
Go

Insert into LOCALIDAD(IdLocalidad, Descripcion, IdProvincia) Values
	(1, 'Palermo', 1),
	(2, 'Belgrano', 1),
	(3, 'San Fernando', 1),
	(4, 'Recoleta', 1),
	(5, 'Flores', 1),
	(6, 'Almagro', 1),
	(7, 'Colegiales', 1)
Go

insert into PRODUCTO (Nombre, Descripcion, IdMarca, IdCategoria, Precio, Stock) values ('Bicicleta Mountain Bike', 'Rodado 29 Alum. F/disco Slp 10 2023', 1018, 1008, 445.09, 50);
insert into PRODUCTO (Nombre, Descripcion, IdMarca, IdCategoria, Precio, Stock) values ('Pelota Yoga Gadnic', '65cm Esferodinamia Gym Pilates + Inflador', 1019, 4, 35.07, 58);
insert into PRODUCTO (Nombre, Descripcion, IdMarca, IdCategoria, Precio, Stock) values ('Colchon Sommier', 'Backcare Hotel Bilt 2 Plazas 200x160', 1023, 2, 444.45, 64);
insert into PRODUCTO (Nombre, Descripcion, IdMarca, IdCategoria, Precio, Stock) values ('Alfombra Vintage', 'Tipo Persa Gris 100x150 Cm Kreatex', 1020, 4, 385.0, 19);
insert into PRODUCTO (Nombre, Descripcion, IdMarca, IdCategoria, Precio, Stock) values ('Notebook Dell Inspiron 3520', 'Intel Core I3 256gb Ssd 8gb Ram', 1020, 4, 870.08, 30);
insert into PRODUCTO (Nombre, Descripcion, IdMarca, IdCategoria, Precio, Stock) values ('Monitor Philips V', '193V5LHSB2 LCD 18.5" negro 100V/240V', 1017, 1004, 323.75, 88);
insert into PRODUCTO (Nombre, Descripcion, IdMarca, IdCategoria, Precio, Stock) values ('Joystick inalambrico PS5', 'FI-ZCT1 Color Midnight Black', 1023, 2, 334.29, 53);
insert into PRODUCTO (Nombre, Descripcion, IdMarca, IdCategoria, Precio, Stock) values ('Auriculares Gamer Razer Kraken Ultimate', 'Usb 7.1 Chroma Rgb', 1021, 1, 891.9, 9);
insert into PRODUCTO (Nombre, Descripcion, IdMarca, IdCategoria, Precio, Stock) values ('Caloventor electrico', 'FH400 Color negro 220V-240V', 1018, 1005, 856.78, 84);
insert into PRODUCTO (Nombre, Descripcion, IdMarca, IdCategoria, Precio, Stock) values ('Smart TV Samsung Series 4', 'N32J4300DGCZB LED HD 32" 220V', 1018, 1007, 978.04, 78);
insert into PRODUCTO (Nombre, Descripcion, IdMarca, IdCategoria, Precio, Stock) values ('Celular Moto G32', '128gb 6gb Ram Rosa', 1018, 1005, 172.93, 97);
insert into PRODUCTO (Nombre, Descripcion, IdMarca, IdCategoria, Precio, Stock) values ('Celular Apple iPhone 13', '(128 GB) - Azul medianoche', 1018, 1005, 708.15, 8);
insert into PRODUCTO (Nombre, Descripcion, IdMarca, IdCategoria, Precio, Stock) values ('Zapatillas Urbanas Hombre', 'Zonazero Oakley Westcliff 75', 1020, 1003, 995.02, 70);
insert into PRODUCTO (Nombre, Descripcion, IdMarca, IdCategoria, Precio, Stock) values ('Zapatilla Hombre Lacoste', 'Tela Run Spin Evo Negro', 1017, 1003, 866.12, 72);
insert into PRODUCTO (Nombre, Descripcion, IdMarca, IdCategoria, Precio, Stock) values ('Harry Potter 3 - El Prisionero De Azkabán', 'Harry aguarda el inicio del tercer curso en el Colegio Hogwarts de Magia y Hechicería.', 1020, 1005, 828.24, 97);
insert into PRODUCTO (Nombre, Descripcion, IdMarca, IdCategoria, Precio, Stock) values ('Oxford Advanced Learners Dictionary', '10th Edition', 1017, 1008, 465.12, 67);

insert into PRODUCTO (Nombre, Descripcion, IdMarca, IdCategoria, Precio, Stock) values ('Reloj Montreal Mujer', 'Ml1546 Calendario Malla Acero Inoxid.', 1017, 1003, 317.78, 65);
insert into PRODUCTO (Nombre, Descripcion, IdMarca, IdCategoria, Precio, Stock) values ('Reloj Hombre Casio', 'Ws-2100h Ø48.1mm - Impacto Online', 1020, 1006, 638.86, 55);
insert into PRODUCTO (Nombre, Descripcion, IdMarca, IdCategoria, Precio, Stock) values ('Guitarra Electrica Fender Stratocaster', 'American Professional II Stratocaster Estados Unidos Stratocaster - Diestro - Brillante', 1022, 2, 740.29, 3);
insert into PRODUCTO (Nombre, Descripcion, IdMarca, IdCategoria, Precio, Stock) values ('Teclado musical Yamaha PSR', 'Series PSR-E373 61 teclas negro', 1017, 1004, 202.01, 41);