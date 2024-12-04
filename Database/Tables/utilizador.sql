create table utilizador (
    userID int primary key identity(1,1),
    nome varchar(50) not null,
    apelido varchar(50),
    email varchar(100) unique not null,
    pass varchar(30) not null,
    dt_nascimento date,
    morada varchar(100),
    nif int check (nif between 100000000 and 999999999),
    cidade varchar(50),
    cod_postal char(8),
    created_at datetime2 default sysdatetime(),
    modified_at datetime2
);
GO
create trigger trg_modified_at_utilizador
on utilizador
after update
as
begin
    set nocount on;
    update utilizador
    set modified_at = sysdatetime()
    from inserted
    where utilizador.userID = inserted.userID;
end;