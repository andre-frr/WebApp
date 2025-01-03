create procedure [dbo].[sp_carrinho_update]
	@cartID int,
	@userID int
as
begin
	update carrinho
	set userID = @userID
	where carrinho_id = @cartID and userID is null;
end
