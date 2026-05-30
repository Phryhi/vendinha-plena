# Vendinha Plena - Sistema de Controle de Clientes e Dívidas

## Descrição

O Vendinha Plena é uma aplicação de terminal desenvolvida em C# .NET Core e PostgreSQL para gerenciamento de clientes e suas dívidas.É possível cadastrar, editar, excluir e consultar clientes, além de registrar e controlar dívidas associadas a eles.

# Funcionalidades

## Clientes

* Cadastrar cliente
* Editar cliente
* Excluir cliente
* Buscar cliente por nome
* Listar clientes com paginação
* Ordenar clientes pelo maior valor de dívida

## Dívidas

* Cadastrar dívida
* Pagar dívida
* Buscar dívida por cliente

# Regras de Negócio

## Clientes

### CPF único

Não é permitido cadastrar dois clientes com o mesmo CPF.

## Dívidas

### Apenas uma dívida em aberto por cliente

Cada cliente pode possuir:

* Várias dívidas pagas
* Apenas uma dívida em aberto

### Pagamento de dívida

O pagamento é realizado informando o ID do cliente.
# Estrutura do Projeto

## Models

Representam as entidades da aplicação.

### Cliente

Campos principais:

* id
* nome
* cpf
* DataNascimento
* Email

Relacionamentos:

* Um cliente possui várias dívidas

### Dividas

Campos principais:

* id_divida
* valor
* situacao
* dataCriacao
* dataPagamento
* idCliente

Relacionamentos:

* Uma dívida pertence a um cliente

## Services

Responsavel pelas regras de negócio.

### ClienteService

Responsabilidades:

* Validação
* Cadastro
* Edição
* Exclusão
* Busca
* Paginação

### DividaService

Responsabilidades:

* Cadastro de dívida
* Pagamento
* Consulta de dívida

## Data

Contém:

* VendaDbContext
* Configuração das entidades
* Relacionamentos

# Tecnologias Utilizadas

* C#
* .NET
* Entity Framework Core
* PostgreSQL
* LINQ