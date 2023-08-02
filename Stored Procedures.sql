
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
