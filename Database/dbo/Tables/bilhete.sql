create table bilhete (
    bilhete_id int primary key identity(1,1),
    lugar varchar(10),
    estado int default 1 check (estado in (0, 1)) not null,
    evento_id int not null,
    foreign key (evento_id) references evento(evento_id)
);