CREATE PROCEDURE [dbo].[sp_utilizador_get]
    @email nvarchar(max),
    @pass nvarchar(max)
AS
BEGIN
    SELECT *
    FROM utilizador
    WHERE email = @email AND pass = @pass;
END

