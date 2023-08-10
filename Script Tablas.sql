Create Database DBCARRITO
GO

Use DBCARRITO
GO

Create Table CATEGORIA(
	IdCategoria int primary key identity,
	Descripcion varchar(100),
	Activo bit default 1,
	FechaRegistro datetime default getdate()
)
Go

Create Table MARCA(
	IdMarca int primary key identity,
	Descripcion varchar(100),
	Activo bit default 1,
	FechaRegistro datetime default getdate()
)
Go

Create Table PRODUCTO(
	IdProducto int primary key identity,
	Nombre varchar(100),
	Descripcion varchar(100),
	IdMarca int references MARCA(IdMarca),
	IdCategoria int references CATEGORIA(IdCategoria),
	Precio decimal (10,2) default 0,
	Stock int,
	RutaImagen varchar(100),
	NombreImagen varchar(100),
	Activo bit default 1,
	FechaRegistro datetime default getdate()
)
Go

Create Table CLIENTE(
	IdCliente int primary key identity,
	Nombres varchar(100),
	Apellidos varchar(100),
	Correo varchar(100),
	Clave varchar(100),
	Reestablecer bit default 0,
	FechaRegistro datetime default getdate()
)
Go

Create Table CARRITO(
	IdCarrito int primary key identity,
	IdCliente int references CLIENTE(IdCliente),
	IdProducto int references PRODUCTO(IdProducto),
	Cantidad int
)
Go

Create Table VENTA(
	IdVenta int primary key identity,
	IdCliente int references CLIENTE(IdCliente),
	TotalProducto int,
	MontoTotal decimal(10,2),
	Contacto varchar(50),
	IdLocalidad varchar(10),
	Telefono varchar(50),
	Direccion varchar(500),
	IdTransaccion varchar(50),
	FechaVenta datetime default getdate()
)
Go

Create Table DETALLE_VENTA(
	IdDetalleVenta int primary key identity,
	IdVenta int references Venta(IDVenta),
	IdProducto int references PRODUCTO(IdProducto),
	Cantidad int,
	Total decimal(10,2)
)
Go

Create Table USUARIO(
	IdUsuario int primary key identity,
	Nombres varchar(100),
	Apellidos varchar(100),
	Correo varchar(100),
	Clave varchar(100),
	Reestablecer bit default 1,
	Activo bit default 1,
	FechaRegistro datetime default getdate()
)
Go

Create Table LOCALIDAD (
	IdLocalidad varchar(4) NOT NULL,
	Descripcion varchar(45) NOT NULL,
	IdProvincia varchar(2) NOT NULL
)
Go

Create Table PROVINCIA (
	IdProvincia varchar(4) NOT NULL,
	Descripcion varchar(45) NOT NULL
)
Go
