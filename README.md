# Vendinha Plena

Sistema CRUD de gerenciamento de clientes e dívidas desenvolvido em C# .NET Core e PostgreSQL.

## Requisitos

* .NET 8.0 
* PostgreSQL

## Clonar o projeto

```bash
git clone https://github.com/Phryhi/vendinha-plena.git
cd vendinhaPlena
```

## Configurar banco de dados

Crie um banco PostgreSQL.

```sql
crwate database vendinhaPlena;

create table cliente(
	id serial NOT NULL,
	nome VARCHAR(100) NOT NULL,
	cpf VARCHAR(11) UNIQUE NOT NULL,
	data_nascimento DATE NOT NULL,
	email VARCHAR(150) NOT NULL,
	primary key(id)
);

create table dividas(
	id_divida serial NOT NULL,
	valor DECIMAL(10, 2) NOT NULL,
	situacao INTEGER NOT NULL,
	data_criacao DATE NOT NULL,
	data_pagamento DATE NOT NULL,
	id_cliente INTEGER REFERENCES cliente(id),
	primary key(id_divida)
);
```

Configure a string de conexão no arquivo responsável pelo DbContext.

## Executar aplicação

```bash
dotnet run
```

## Funcionalidades disponíveis

### Clientes

* Cadastro
* Edição
* Exclusão
* Busca por nome
* Listagem paginada

### Dívidas

* Cadastro
* Pagamento
* Consulta

## Regras de negócio

* CPF deve ser único
* Apenas uma dívida em aberto por cliente
* Dívidas pagas permanecem registradas no histórico
* Clientes são ordenados por maior valor de dívida

## Autoras
- Grupo N

- Ana Julia Chiezi
- Camila Pereira Mattos
