create table evento (
    evento_id int primary key identity(1,1),
    titulo varchar(100) not null,
    tipo varchar(30),
    classificacao varchar(30),
    data_hora datetime2 not null,
    local_evento varchar(100),
    descricao varchar(max),
    preco decimal(7,2) not null check (preco >= 0),
    lotacao int check (lotacao >= 0),
    created_at datetime2 default sysdatetime(),
    modified_at datetime2
);
GO
create trigger trg_modified_at_evento
on evento
after update
as
begin
    set nocount on;
    update evento
    set modified_at = sysdatetime()
    from inserted
    where evento.evento_id = inserted.evento_id;
end;