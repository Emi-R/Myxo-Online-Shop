--------------------------------------------
------------------- USERS ------------------
--------------------------------------------

-- Creates user
Create Proc sp_RegistrarUsuario (
	@Nombres varchar(100),
	@Apellidos varchar(100),
	@Correo varchar(100),
	@Clave varchar(100),
	@Activo bit,
	@Mensaje varchar (500) output,
	@Resultado int output
)
As
Begin 
	Set @Resultado = 0

	If Not Exists (Select * From USUARIO Where Correo = @Correo)
	Begin
		Insert into USUARIO (Nombres,Apellidos,Correo,Clave,Activo) Values
		(@Nombres, @Apellidos, @Correo, @Clave, @Activo)

		Set @Resultado = SCOPE_IDENTITY()
	End
	Else
		Set @Mensaje = 'El correo del usuario ya existe'
end
Go

-- Modifies user
Create Proc sp_EditarUsuario (
	@IdUsuario int,
	@Nombres varchar(100),
	@Apellidos varchar(100),
	@Correo varchar(100),
	@Activo bit,
	@Mensaje varchar (500) output,
	@Resultado bit output
)
As
Begin 
	Set @Resultado = 0

	If Not Exists (Select * From USUARIO Where Correo = @Correo And IdUsuario != @IdUsuario)
	Begin
		Update top(1) USUARIO set 
		Nombres = @Nombres,
		Apellidos = @Apellidos,
		Correo = @Correo,
		Activo = @Activo
		Where IdUsuario = @IdUsuario

		Set @Resultado = 1

	End
	Else
		Set @Mensaje = 'El correo del usuario ya existe'
end
Go

--------------------------------------------
---------------- CATEGORIES ----------------
--------------------------------------------

-- Creates category
Create Proc sp_RegistrarCategoria(
	@Descripcion varchar(100),
	@Activo bit,
	@Mensaje varchar(500) output,
	@Resultado int output
)
As
Begin
	Set @Resultado = 0

	If Not Exists (Select * From CATEGORIA Where Descripcion = @Descripcion)
	Begin
		Insert Into CATEGORIA (Descripcion, Activo)
		Values (@Descripcion, @Activo)

		Set @Resultado = SCOPE_IDENTITY()
	End
	Else
		Set @Mensaje = 'La Categoria ya existe'

End
Go

-- Modifies category
Create Proc sp_EditarCategoria(
	@IdCategoria int,
	@Descripcion varchar(100),
	@Activo bit,
	@Mensaje varchar(500) output,
	@Resultado bit output
)
As
Begin
	Set @Resultado = 0

	If Not Exists (Select * From CATEGORIA Where Descripcion = @Descripcion And IdCategoria != @IdCategoria)
	Begin
		Update top(1) CATEGORIA Set Descripcion = @Descripcion,
		Activo = @Activo
		Where IdCategoria = @IdCategoria

		Set @Resultado = 1
	End
	Else
		Set @Mensaje = 'La Categoria ya existe'

End
Go

-- Deletes category
Create Proc sp_EliminarCategoria(
	@IdCategoria int,
	@Mensaje varchar(500) output,
	@Resultado bit output
)
As
Begin
	Set @Resultado = 0

	If Not Exists (Select * From PRODUCTO p 
					Inner Join CATEGORIA c on c.IdCategoria = p.IdCategoria 
					Where p.IdCategoria = @IdCategoria)
	Begin
		Delete top(1) From CATEGORIA Where IdCategoria = @IdCategoria
		Set @Resultado = 1
	End
	Else
		Set @Mensaje = 'La Categoria se encuentra relacionada a un producto'

End
Go


--------------------------------------------
------------------- BRANDS -----------------
--------------------------------------------

-- Creates brand
Create Proc sp_RegistrarMarca(
	@Descripcion varchar(100),
	@Activo bit,
	@Mensaje varchar(500) output,
	@Resultado int output
)
As
Begin
	Set @Resultado = 0

	If Not Exists (Select * From MARCA Where Descripcion = @Descripcion)
	Begin
		Insert Into MARCA (Descripcion, Activo)
		Values (@Descripcion, @Activo)

		Set @Resultado = SCOPE_IDENTITY()
	End
	Else
		Set @Mensaje = 'La marca ya existe'

End
Go

-- Modifies category
Create Proc sp_EditarMarca(
	@IdMarca int,
	@Descripcion varchar(100),
	@Activo bit,
	@Mensaje varchar(500) output,
	@Resultado bit output
)
As
Begin
	Set @Resultado = 0

	If Not Exists (Select * From MARCA Where Descripcion = @Descripcion And IdMarca != @IdMarca)
	Begin
		Update top(1) MARCA Set Descripcion = @Descripcion,
		Activo = @Activo
		Where IdMarca = @IdMarca

		Set @Resultado = 1
	End
	Else
		Set @Mensaje = 'La marca ya existe'

End
Go

-- Deletes category
Create Proc sp_EliminarMarca(
	@IdMarca int,
	@Mensaje varchar(500) output,
	@Resultado bit output
)
As
Begin
	Set @Resultado = 0

	If Not Exists (Select * From PRODUCTO p 
					Inner Join MARCA m on m.IdMarca = p.IdMarca
					Where p.IdMarca = @IdMarca)
	Begin
		Delete top(1) From MARCA Where IdMarca = @IdMarca
		Set @Resultado = 1
	End
	Else
		Set @Mensaje = 'La marca se encuentra relacionada a un producto'

End
Go

--------------------------------------------
------------------ PRODUCTS ----------------
--------------------------------------------

-- Creates new product

Create Proc sp_RegistrarProducto(
	@Nombre varchar(100),
	@Descripcion varchar(100),
	@IdMarca varchar(100),
	@IdCategoria varchar(100),
	@Precio decimal(10,2),
	@Stock int,
	@Activo bit,
	@Mensaje varchar(500) output,
	@Resultado int output
)
As
Begin
	Set @Resultado = 0

	If Not Exists (Select * From PRODUCTO Where Nombre = @Nombre)
	Begin
		Insert Into PRODUCTO(Nombre, Descripcion, IdMarca, IdCategoria, Precio, Stock, Activo) Values
		(@Nombre, @Descripcion, @IdMarca, @IdCategoria, @Precio, @Stock, @Activo)

		Set @Resultado = SCOPE_IDENTITY()
	end
	else
		Set @Mensaje = 'El producto ya existe'
End
Go

-- Modifies product

Create Proc sp_ModificarProducto(
	@IdProducto int,
	@Nombre varchar(100),
	@Descripcion varchar(100),
	@IdMarca varchar(100),
	@IdCategoria varchar(100),
	@Precio decimal(10,2),
	@Stock int,
	@Activo bit,
	@Mensaje varchar(500) output,
	@Resultado bit output
)
As
Begin
	Set @Resultado = 0

	If Not Exists (Select * From PRODUCTO Where Nombre = @Nombre and IdProducto != @IdProducto)
	Begin
		Update PRODUCTO set
		Nombre = @Nombre,
		Descripcion = @Descripcion,
		IdMarca = @IdMarca,
		IdCategoria = @IdCategoria,
		Precio = @Precio,
		Stock = @Stock,
		Activo = @Activo
		Where IdProducto = @IdProducto

		Set @Resultado = 1
	End
	Else
		Set @Mensaje = 'El producto ya existe'
End
Go

-- Deletes product

Create Proc sp_EliminarProducto(
	@IdProducto int,
	@Mensaje varchar(500) output,
	@Resultado bit output
)
As
Begin
	Set @Resultado = 0

	If Not Exists (Select * From DETALLE_VENTA dv
					Inner Join PRODUCTO p on p.IdProducto = dv.IdProducto
					Where p.IdProducto = @IdProducto)
	Begin
		Delete Top(1) from PRODUCTO where IdProducto = @IdProducto
		Set @Resultado = 1
	End
	Else
		Set @Mensaje = 'El producto se encuenta relacionado a una venta'
End
Go

--------------------------------------------
------------------ CLIENTS -----------------
--------------------------------------------

-- Registers new client

Create Proc sp_RegistrarCliente(
	@Nombres varchar(100),
	@Apellidos varchar(100),
	@Correo varchar(100),
	@Clave varchar(100),
	@Mensaje varchar (500) output,
	@Resultado int output
)
As
Begin
	Set @Resultado = 0

	If Not Exists (Select * From CLIENTE Where Correo = @Correo)
	Begin
		Insert Into CLIENTE(Nombres,Apellidos,Correo,Clave,Reestablecer) Values
		(@Nombres, @Apellidos, @Correo, @Clave, 0)
		Set @Resultado = SCOPE_IDENTITY()
	end
	else
		Set @Mensaje = 'Ya existe un usuario registrado con ese correo'
End
Go


--------------------------------------------
------------------- CART ------------------
--------------------------------------------

Create Proc sp_ExisteCarrito(
	 @IdCliente int,
	 @IdProducto int,
	 @Resultado bit output
)
As
Begin
	If Exists(Select * From CARRITO Where IdCliente = @IdCliente And IdProducto = @IdProducto)
		Set @Resultado = 1
	Else
		Set @Resultado = 0
End
Go

Create Proc sp_OperacionCarrito(
	@IdCliente int,
	@IdProducto int,
	@Sumar bit,
	@Mensaje varchar(500) output,
	@Resultado bit output
)
AS
Begin
	Set @Resultado = 1
	set @Mensaje = ''

	Declare @existecarrito bit = iif(exists(Select * From CARRITO Where IdCliente = @IdCliente and IdProducto = @IdProducto),1,0)
	Declare @stockproducto int = (Select Stock From PRODUCTO Where IdProducto = @IdProducto)

	Begin Try 

		Begin Transaction Operacion

		if(@Sumar = 1)
		Begin

			if(@stockproducto > 0)
			Begin 

				if(@existecarrito = 1)
					Update CARRITO Set Cantidad = Cantidad + 1 Where IdCliente = @IdCliente And IdProducto = @IdProducto
				Else 
					Insert into Carrito(IdCliente, IdProducto, Cantidad) Values (@IdCliente, @IdProducto, 1)
				
					Update PRODUCTO Set Stock = Stock - 1 Where IdProducto = @IdProducto
				End
			Else 
				Begin 
					Set @Resultado = 0
					Set @Mensaje = 'El producto no cuenta con stock disponible por el momento'
				End
			End
		Else
		Begin 
			Update CARRITO Set Cantidad = Cantidad - 1 Where IdCliente = @IdCliente And IdProducto = @IdProducto
			Update PRODUCTO Set Stock = Stock + 1 Where IdProducto = @IdProducto
		End 

		Commit Transaction Operacion
	End Try 
	Begin Catch 
		Set @Resultado = 0
		Set @Mensaje = ERROR_MESSAGE()
		Rollback Transaction Operacion
	End Catch

End
Go

Create Proc sp_EliminarCarrito(
	 @IdCliente int,
	 @IdProducto int,
	 @Resultado bit output
)
As
Begin

	Set @Resultado = 1
	Declare @cantidadproducto int = (Select Cantidad From CARRITO Where IdCliente = @IdCliente and IdProducto = @IdProducto)

	Begin Try 
		Begin Transaction Operacion 

		Update PRODUCTO Set Stock = Stock + @cantidadproducto Where IdProducto = @IdProducto
		Delete Top(1) from CARRITO Where IdCliente = @IdCliente and IdProducto = @IdProducto

		Commit Transaction Operacion 
	End Try 
	Begin Catch 
		Set @Resultado = 0
		Rollback Transaction Operacion
	End Catch
End
Go

Create Function fn_obtenerCarritoCliente(
	@idcliente int
)
returns table 
as 
return(
	Select p.IdProducto,
	m.Descripcion[DesMarca],
	p.Nombre,
	p.Precio,
	p.Stock,
	c.Cantidad,
	p.RutaImagen,
	p.NombreImagen
	From CARRITO c 
	Inner Join PRODUCTO p on p.IdProducto = c.IdProducto
	Inner Join MARCA m on p.IdMarca = m .IdMarca
	Where c.IdCliente = @idcliente
)
Go


--------------------------------------------
------------------ REPORTS -----------------
--------------------------------------------


Create Proc sp_ReporteDashboard
as
Begin

Select
(Select COUNT(*) from CLIENTE) [TotalCliente],
(Select ISNULL(sum(cantidad),0) From DETALLE_VENTA) [TotalVenta],
(Select COUNT(*) From PRODUCTO) [TotalProducto]

End
Go

Create Proc sp_ReporteVentas(
	@fechainicio varchar(10),
	@fechafin varchar(10),
	@idtransaccion varchar(50)
)
as
Begin

	Set dateformat dmy;

	Select CONVERT (char(10), v.FechaVenta, 103) [FechaVenta], Concat(c.Nombres,' ', c.Apellidos)[Cliente],
			p.Nombre[Producto], p.Precio, dv.Cantidad, dv.Total, v.IdTransaccion
	from DETALLE_VENTA dv
	inner join PRODUCTO p on p.IdProducto = dv.IdProducto
	inner join VENTA v on v.IdVenta = dv.IdVenta
	inner join CLIENTE c on v.IdCliente = c.IdCliente
	Where convert(date, v.FechaVenta) Between @fechainicio and @fechafin
	And v.IdTransaccion = iif(@idtransaccion = '', v.IdTransaccion, @idtransaccion)

End
Go

Create Type [dbo].[EDetalle_Venta] as Table(
	[IdProducto] int Null,
	[Cantidad] int Null,
	[Total] decimal(18, 2) Null
)
Go


--------------------------------------------
------------------- SALES ------------------
--------------------------------------------

Create Proc usp_RegistrarVenta(
	@IdCliente int,
	@TotalProducto int,
	@MontoTotal decimal(18,2),
	@Contacto varchar(100),
	@IdLocalidad varchar(4),
	@Telefono varchar(10),
	@Direccion varchar(100),
	@IdTransaccion varchar(50),
	@DetalleVenta [EDetalle_Venta] READONLY,
	@Resultado bit output,
	@Mensaje varchar(500) output
)
as
Begin 

	Begin Try

		Declare @idventa int = 0
		Set @Resultado = 1
		Set @Mensaje = ''

		Begin Transaction registro 

		Insert Into Venta(IdCliente,TotalProducto,MontoTotal,Contacto,IdLocalidad,Telefono,Direccion,IdTransaccion)
		Values(@IdCliente, @TotalProducto, @MontoTotal, @Contacto, @IdLocalidad, @Telefono, @Direccion, @IdTransaccion)

		Set @idventa = SCOPE_IDENTITY()

		Insert into DETALLE_VENTA(IdVenta,IdProducto,Cantidad,Total)
		Select @idventa, IdProducto, Cantidad, Total from @DetalleVenta

		Delete from CARRITO Where IdCliente = @IdCliente

		Commit Transaction registro
	End Try 
	Begin Catch 
		Set @Resultado = 0
		Set @Mensaje = ERROR_MESSAGE()
		Rollback Transaction registro
	End Catch 

End
Go

Create Function fn_ListarCompra(
	@idcliente int
)
returns table 
as 
return(
		Select p.RutaImagen, 
		p.NombreImagen,
		p.Nombre,
		p.Precio,
		dv.Cantidad,
		dv.Total,
		v.IdTransaccion From DETALLE_VENTA DV 
		Inner Join PRODUCTO P On P.IdProducto = DV.IdProducto
		Inner Join VENTA V On V.IdVenta = DV.IdVenta
		Where V.IdCliente = @idcliente
)
Go

