create table carrinho (
    carrinho_id int primary key identity(1,1),
    total decimal(7,2) not null check (total >= 0),
    created_at datetime2 default sysdatetime(),
    modified_at datetime2,
    userID int,
    foreign key (userID) references utilizador(userID) on delete cascade
);
GO
create trigger trg_modified_at_carrinho
on carrinho
after update
as
begin
    set nocount on;
    update carrinho
    set modified_at = sysdatetime()
    from inserted
    where carrinho.carrinho_id = inserted.carrinho_id;
end;