create table encomenda (
    encomenda_id int primary key identity(1,1),
    total decimal(7,2) not null check (total >= 0),
    estado_pagamento varchar(10) check (estado_pagamento in ('pendente', 'concluído')) not null,
    created_at datetime2 default sysdatetime(),
    userID int not null,
    carrinho_id int not null,
    foreign key (userID) references utilizador(userID) on delete cascade,
    foreign key (carrinho_id) references carrinho(carrinho_id)
);