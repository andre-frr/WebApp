create procedure [dbo].[sp_carrinho_create]
	@userID int = null
as
begin
	insert into carrinho(total, userID)
	values (0.00, @userID)
	select scope_identity() as cartID
end