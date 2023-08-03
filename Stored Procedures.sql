--------------------------------------------
---------------- USERS ---------------------
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
---------------- BRANDS ----------------
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
		Delete top(1) From MARCA Where @IdMarca = @IdMarca
		Set @Resultado = 1
	End
	Else
		Set @Mensaje = 'La marca se encuentra relacionada a un producto'

End
Go