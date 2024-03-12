﻿using Database008;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

var dbContext = new DbContext008();

bool running = true;

while (running)
{
    Console.Clear();
    Console.OutputEncoding = Encoding.UTF8;
    Console.CursorVisible = false;
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("Bem-vindo ao mundo do C#!");
    Console.ResetColor();
    Console.WriteLine("\nUse ⬆️  e ⬇️  para navegar e pressione \u001b[32mEnter/Return\u001b[0m para selecionar:");
    (int left, int top) = Console.GetCursorPosition();
    var option = 1;
    var decorator = "✅ \u001b[32m";
    ConsoleKeyInfo key;
    bool isSelected = false;

    while (!isSelected)
    {
        Console.SetCursorPosition(left, top);

        Console.WriteLine($"{(option == 1 ? decorator : "   ")}Listar usuários\u001b[0m");
        Console.WriteLine($"{(option == 2 ? decorator : "   ")}Listar grupos\u001b[0m");
        Console.WriteLine($"{(option == 3 ? decorator : "   ")}Inserir usuário\u001b[0m");
        Console.WriteLine($"{(option == 4 ? decorator : "   ")}Atualizar usuário\u001b[0m");
        Console.WriteLine($"{(option == 5 ? decorator : "   ")}Deletar usuário\u001b[0m");
        Console.WriteLine($"{(option == 6 ? decorator : "   ")}Criar grupo\u001b[0m");
        Console.WriteLine($"{(option == 7 ? decorator : "   ")}Atualizar grupo\u001b[0m");
        Console.WriteLine($"{(option == 8 ? decorator : "   ")}Deletar grupo\u001b[0m");
        Console.WriteLine($"{(option == 9 ? decorator : "   ")}Sair\u001b[0m");

        key = Console.ReadKey(false);

        switch (key.Key)
        {
            case ConsoleKey.UpArrow:
                option = option == 1 ? 9 : option - 1;
                break;

            case ConsoleKey.DownArrow:
                option = option == 9 ? 1 : option + 1;
                break;

            case ConsoleKey.Escape:
                running = false;
                return;

            case ConsoleKey.Enter:
                isSelected = true;
                break;
        }
    }

    Console.WriteLine($"\n{decorator}Você selecionou a opção {option}");

    switch (option)
    {
        case 1:
            Console.WriteLine("Lista de Usuários: ");

            if (!dbContext.Users.Any())
            {
                Console.WriteLine("Nenhum usuário encontrado");
            }
            else
            {
                dbContext.Users.ToList().ForEach(user =>
                {
                    Console.WriteLine($"{user.Id} {user.Name} {user.Password} {user?.Group?.Name}");
                });
            }

            Console.WriteLine("Pressione Enter para sair da lista");
            Console.ReadLine();
            Console.ResetColor();
            break;
        case 2:
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Lista de Grupos:");

            if (!dbContext.Groups.Any())
            {
                Console.WriteLine("Nenhum grupo encontrado");
            }
            else
            {
                dbContext.Groups.ToList().ForEach(group =>
                {
                    Console.WriteLine($"{group.Id} {group.Name}");
                    Console.WriteLine("Pressione Enter para sair da lista");
                });
            }

            Console.ReadLine();

            Console.ResetColor();
            break;
        case 3:
            Console.Clear();
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Red;

            Console.Write("Digite o nome do usuário: ");
            string nome = Console.ReadLine() ?? string.Empty;

            Console.Write("Digite a senha do usuário: ");
            string senha = Console.ReadLine() ?? string.Empty;

            if (string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(senha))
            {
                Console.WriteLine("Nome e senha são obrigatórios!");
                Console.ReadLine();
                break;
            }

            Console.Write("Selecione o grupo do usuário: ");

            int groupId = 0;

            bool groupSelected = false;

            var groups = dbContext.Groups.ToList();

            Console.ForegroundColor = ConsoleColor.White;

            while (!groupSelected)
            {
                Console.SetCursorPosition(left, top);

                Console.WriteLine("Lista de Grupos:");
                groups.ForEach(group =>
                {
                    Console.WriteLine($"{(groupId == group.Id ? decorator : "   ")}{group.Id} {group.Name}\u001b[0m");
                });

                key = Console.ReadKey(false);

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        groupId = groupId == 1 ? groups.Count() : groupId - 1;
                        break;

                    case ConsoleKey.DownArrow:
                        groupId = groupId == groups.Count() ? 1 : groupId + 1;
                        break;

                    case ConsoleKey.Enter:
                        groupSelected = true;
                        break;
                }
            }

            dbContext.Add(new User
            {
                Name = nome,
                Password = senha,
                GroupId = groupId
            });

            dbContext.SaveChanges();

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.ResetColor(); 
            Console.WriteLine("Usuário inserido com sucesso!");
            break;

        case 4:
            Console.Clear();

            var users = dbContext.Users.Include(u => u.Group).ToList();

            if (!users.Any())
            {
                Console.WriteLine("Nenhum usuário disponível para atualização.");
                break;
            }

            Console.WriteLine("Selecione um usuário para atualizar:");
            for (int i = 0; i < users.Count; i++)
            {
                string groupName = users[i].Group != null ? users[i].Group.Name : "Sem Grupo";
                Console.WriteLine($"{i + 1}. ID: {users[i].Id}, Nome: {users[i].Name}, Grupo: {groupName}");
            }

            int selectedUserIndex = Convert.ToInt32(Console.ReadLine()) - 1;
            User selectedUser = users[selectedUserIndex];

            Console.Write("Digite o novo nome do usuário (deixe em branco para não alterar): ");
            string nomeUpdate = Console.ReadLine();
            if (!string.IsNullOrEmpty(nomeUpdate))
            {
                selectedUser.Name = nomeUpdate;
            }

            Console.Write("Digite a nova senha do usuário (deixe em branco para não alterar): ");
            string senhaUpdate = Console.ReadLine();
            if (!string.IsNullOrEmpty(senhaUpdate))
            {
                selectedUser.Password = senhaUpdate;
            }

            dbContext.SaveChanges();

            Console.WriteLine("Usuário atualizado com sucesso!");
            break;

        case 5:
            Console.Write("Digite o ID do usuário: ");
            int idDelete = Convert.ToInt32(Console.ReadLine());

            var userDelete = dbContext.Users.Find(idDelete);

            if (userDelete != null)
            {
                dbContext.Remove(userDelete);
                dbContext.SaveChanges();
                Console.WriteLine("Usuário removido com sucesso!");
            }
            else
            {
                Console.WriteLine("Usuário não encontrado!");
            }

            break;

        case 6:
            Console.Write("Digite o nome do grupo: ");

            string nomeGrupo = Console.ReadLine() ?? string.Empty;

            if (string.IsNullOrEmpty(nomeGrupo))
            {
                Console.WriteLine("Nome do grupo é obrigatório!");
                Console.ReadLine();
                break;
            }

            dbContext.Add(new Group
            {
                Name = nomeGrupo
            });

            dbContext.SaveChanges();

            Console.WriteLine("Grupo inserido com sucesso!");

            break;
        case 7:
            Console.Write("Digite o ID do grupo: ");
            int idGrupo = Convert.ToInt32(Console.ReadLine());

            Console.Write("Digite o nome do grupo: ");
            string nomeGrupoUpdate = Console.ReadLine() ?? string.Empty;

            if (string.IsNullOrEmpty(nomeGrupoUpdate))
            {
                Console.WriteLine("Nome do grupo é obrigatório!");
                Console.ReadLine();
                break;
            }

            var grupo = dbContext.Groups.Find(idGrupo);

            if (grupo != null)
            {
                grupo.Name = nomeGrupoUpdate;
                dbContext.SaveChanges();
                Console.WriteLine("Grupo atualizado com sucesso!");
            }
            else
            {
                Console.WriteLine("Grupo não encontrado!");
            }

            break;
        case 8:
            Console.Write("Digite o ID do grupo: ");
            int idGrupoDelete = Convert.ToInt32(Console.ReadLine());

            if (idGrupoDelete <= 0)
            {
                Console.WriteLine("ID é obrigatório");
                Console.ReadLine();
                break;
            }

            var grupoDelete = dbContext.Groups.Find(idGrupoDelete);

            if (grupoDelete != null)
            {
                dbContext.Remove(grupoDelete);
                dbContext.SaveChanges();
                Console.WriteLine("Grupo removido com sucesso!");
            }
            else
            {
                Console.WriteLine("Grupo não encontrado!");
            }

            break;
        default:
            Console.WriteLine("Escolha inválida");
            break;
    }
}
