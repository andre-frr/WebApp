create procedure [dbo].[sp_utilizador_updatePass]
    @userID int,
    @newPassword nvarchar(max)
as
begin
    update utilizador
    set pass = @newPassword
    where userID = @userID;
end
