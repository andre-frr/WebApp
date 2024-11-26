create table item_carrinho (
    item_carrinho_id int primary key identity(1,1),
    created_at datetime2 default sysdatetime(),
    modified_at datetime2,
    carrinho_id int not null,
    bilhete_id int,
    foreign key (carrinho_id) references carrinho(carrinho_id) on delete cascade,
    foreign key (bilhete_id) references bilhete(bilhete_id)
);
GO
create trigger trg_modified_at_item_carrinho
on item_carrinho
after update
as
begin
    set nocount on;
    update item_carrinho
    set modified_at = sysdatetime()
    from inserted
    where item_carrinho.item_carrinho_id = inserted.item_carrinho_id;
end;