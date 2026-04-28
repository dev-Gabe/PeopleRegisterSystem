# Sistema de Cadastro de Pessoas

Aplicação desktop desenvolvida em WPF (.NET) que utiliza uma API REST hospedada no Azure para gerenciamento de contatos. O sistema permite cadastrar, visualizar, atualizar e excluir registros de pessoas, com validações de dados no cliente e integração com banco de dados em nuvem.

---

## Descrição

Este projeto foi desenvolvido com o objetivo de demonstrar a construção de uma aplicação completa, envolvendo:

* Interface desktop (WPF)
* API REST com ASP.NET Core
* Deploy em ambiente de nuvem (Azure)

A aplicação segue um fluxo simples de CRUD (Create, Read, Update, Delete), com comunicação via HTTP entre o cliente WPF e a API.

---

## Funcionalidades

* Cadastro de pessoas
* Listagem de registros
* Atualização de dados existentes
* Exclusão de registros
* Validação de entrada de dados:

* Campos obrigatórios
* Telefone aceita apenas números
* Telefone com exatamente 11 dígitos
* Integração com API hospedada no Azure

---

## Tecnologias Utilizadas

* C# (.NET)
* WPF
* ASP.NET Core Web API
* Entity Framework Core
* Azure App Service
* Azure SQL Database

---

## Arquitetura

A aplicação é dividida em duas partes principais:

### 1. Cliente (WPF)

Responsável pela interface do usuário e interação com a API.

### 2. API (ASP.NET Core)

Responsável pelas regras de negócio e acesso ao banco de dados.

A comunicação entre cliente e servidor é feita via requisições HTTP.

---

## API

A API está hospedada no Azure e disponível no seguinte endpoint:

https://apiregistro-h4epbvcmend2cvcq.brazilsouth-01.azurewebsites.net/api/persons

---

## Estrutura do Projeto

* AppWPF
  Interface do usuário e consumo da API

* Api
  Endpoints REST e lógica de negócio

* Models
  Classes de domínio (ex: Pessoa)

---

## Validações Implementadas

* Bloqueio de caracteres não numéricos no campo telefone
* Validação de tamanho (11 dígitos)
* Verificação de campos obrigatórios

## Autor

Gabriel Rocha
