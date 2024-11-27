create procedure [dbo].[sp_utilizador_insert]
	@nome nvarchar(max),
	@apelido nvarchar(max),
	@email nvarchar(max),
	@pass nvarchar(max)
as
begin
	insert into utilizador(nome,apelido,email,pass)
		values (@nome,@apelido,@email,@pass)
end
