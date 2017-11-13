/*  Procedimientos  */
use ProyectoBDI



/********** Insertar nuevo usuario **********/
create procedure insertUser
	@contraseņa nvarchar(40),
	@nombre nvarchar(25),
	@apellido nvarchar(25),
	@correo varchar(35),
	@perfilPic image
as
	declare 
		@nu int,
		@id varchar(10)
	
	--se extrae las tres primeras letras del nombre y del apellido para formar la base del id
	set @id = '@'+left(@nombre,3)+left(@apellido,3) 

	--se cuenta cuantos usuarios existentes poseen la misma base de id
	set @nu = (select 
			       count(*)+1
	           from Users s
			   where substring(s.id,1,7)=@id)
	
	--dependiendo de la cantidad de users con la misma base de id se le aņade un valor numerico al id
	if @nu<=9
		set @id=rtrim(@id)+'00'+ltrim(str(@nu))
	if @nu<=99
		set @id=rtrim(@id)+'0'+ltrim(str(@nu))
	if @nu<=9
		set @id=rtrim(@id)+ltrim(str(@nu))

	--se inserta el usuario a la tabla
	insert into Users(id, contraseņa, nombre, apellido, correo, perfilPic)
	values(@id, encryptbypassphrase(@contraseņa, @contraseņa), @nombre, @apellido, @correo, @perfilPic)


execute insertUser 'harold97', 'Harold', 'Espinoza', 'simple correo', null
select * from Users



/*************** Validar usuario ***************/
create procedure validaUser
	@user varchar(35),
	@password varchar(40)
as
	select id
	from Users
	where 
		(id=@user or correo=@user) and convert(varchar(40), Decryptbypassphrase(@password,contraseņa)) = @password

execute validaUser 'simple correo', 'harold97'



/*************** Borrar usuario ***************/
create procedure borrarUser
	@id varchar(10),
	@password varchar(40)
as
	delete  
	from Users
	where id=@id and convert(varchar(40), Decryptbypassphrase(@password,contraseņa)) = @password

execute borrarUser '@HarEsp001', 'harold97'