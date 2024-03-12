# Gerenciador de Usuários e Grupos via Terminal

Este é um aplicativo gerenciador de usuários e grupos que permite ao usuário realizar operações CRUD (Create, Read, Update, Delete) via terminal. Ele foi desenvolvido utilizando Entity Framework Core e In-Memory Database.

## Entidades

O aplicativo possui duas entidades principais:

### User
- **Id**: Identificador único do usuário
- **Name**: Nome do usuário
- **Password**: Senha do usuário
- **GroupId**: Identificador do grupo ao qual o usuário pertence

### Group
- **Id**: Identificador único do grupo
- **Name**: Nome do grupo

## Requisitos

O gerenciador permite ao usuário selecionar opções utilizando as setas do teclado para navegação no terminal. A navegação com teclado é baseada na referência fornecida neste [repositório](https://github.com/username/repo).

As principais funcionalidades do gerenciador incluem:

- Operações CRUD nas entidades Group e User
- Ao adicionar ou atualizar um usuário, o campo GroupId mostra os grupos cadastrados para que o usuário possa escolher utilizando a navegação com as setas do teclado.

## Como usar

1. Clone o repositório para sua máquina local.
2. Abra o projeto em um ambiente de desenvolvimento de sua escolha.
3. Compile e execute o projeto.
4. Siga as instruções no terminal para navegar e utilizar as funcionalidades do aplicativo.

## Tela via Console
```bash
Bem-vindo ao mundo do C#!

Use ⬆️ e ⬇️ para navegar e pressione Enter/Return para selecionar:
✅ Listar usuários
Listar grupos
Inserir usuário
Atualizar usuário
Deletar usuário
Criar grupo
Atualizar grupo
Deletar grupo
Sair

Você selecionou a opção 1

Lista de Usuários:
1 John Doe *******
2 Jane Smith *******
3 Bob Johnson *******
4 Pressione Enter para sair da lista
```

