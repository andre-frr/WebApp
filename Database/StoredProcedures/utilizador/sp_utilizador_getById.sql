create procedure [dbo].[sp_utilizador_getById]
	@userID int
as
begin
	select * from utilizador where userID = @userID
end
