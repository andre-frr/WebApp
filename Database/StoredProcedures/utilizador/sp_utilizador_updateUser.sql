create procedure [dbo].[sp_utilizador_updateUser]
    @userID int,
    @nome nvarchar(max),
    @apelido nvarchar(max),
    @email nvarchar(max),
    @dt_nascimento date,
    @morada nvarchar(max),
    @nif int,
    @cidade nvarchar(max),
    @cod_postal nvarchar(max)
as
begin
    update utilizador
    set nome = @nome,
        apelido = @apelido,
        email = @email,
        dt_nascimento = @dt_nascimento,
        morada = @morada,
        nif = @nif,
        cidade = @cidade,
        cod_postal = @cod_postal
    where userID = @userID;
end

