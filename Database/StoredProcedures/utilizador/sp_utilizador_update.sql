create procedure [dbo].[sp_utilizador_update]
    @userID int,
    @newPassword nvarchar(max) = null,
    @nome nvarchar(max) = null,
    @apelido nvarchar(max) = null,
    @email nvarchar(max) = null,
    @dt_nascimento date = null,
    @morada nvarchar(max) = null,
    @nif int = null,
    @cidade nvarchar(max) = null,
    @cod_postal nvarchar(max) = null
as
begin

    -- Update password
    if @newPassword is not null
    begin
        update utilizador
        set pass = @newPassword
        where userID = @userID;
    end

    -- Update user
    if @nome is not null or @apelido is not null or @email is not null or 
       @dt_nascimento is not null or @morada is not null or @nif is not null or 
       @cidade is not null or @cod_postal is not null
    begin
        update utilizador
        set nome = coalesce(@nome, nome),
            apelido = coalesce(@apelido, apelido),
            email = coalesce(@email, email),
            dt_nascimento = coalesce(@dt_nascimento, dt_nascimento),
            morada = coalesce(@morada, morada),
            nif = coalesce(@nif, nif),
            cidade = coalesce(@cidade, cidade),
            cod_postal = coalesce(@cod_postal, cod_postal)
        where userID = @userID;
    end
end
