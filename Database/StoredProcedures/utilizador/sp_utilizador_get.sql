create procedure [dbo].[sp_utilizador_get]
    @userID int = null,
    @email nvarchar(max) = null,
    @pass nvarchar(max) = null
as
begin
    set nocount on;
    -- userID
    if @userID is not null
    begin
        select * 
        from utilizador
        where userID = @userID;
    end
    -- Check if email and password are provided
    else if @email is not null and @pass is not null
    begin
        select * 
        from utilizador
        where email = @email AND pass = @pass;
    end
end