using Application.UseCases;
using Domain.Value_Objects;
using Infrastructure;
using Infrastructure.Repositories;

class Program
{
    static void Main()
    {
        var excel = new ExcelRepository(
            @"C:\Users\User\Documents\Painel CCM-1A.xlsx",
            "CCM-1A"
        );

        var filePathBiblioteca = new FilePath("\"C:\\Users\\User\\Desktop\\Biblioteca de Partida - DEFINITIVA.cdr\"");
        var filePathProjetoEletrico = new FilePath("\"C:\\Users\\User\\Desktop\\Testes CorelDraw.cdr\"");
        var corel = new CorelDraw(filePathBiblioteca, filePathProjetoEletrico);

        MainMenu(excel, corel);
    }

    static void MainMenu(ExcelRepository excel, CorelDraw corel)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("====================================");
            Console.WriteLine("     SISTEMA DE PROJETO ELÉTRICO    ");
            Console.WriteLine("====================================");
            Console.WriteLine("1 - Criar projeto elétrico");
            Console.WriteLine("2 - Abrir projeto existente");
            Console.WriteLine("3 - Ferramentas");
            Console.WriteLine("4 - Listar arquivos abertos");
            Console.WriteLine("5 - Definir pasta de Biblioteca");
            Console.WriteLine("6 - Definir pasta de Projetos");
            Console.WriteLine("7 - Sair");
            Console.Write("Escolha uma opção: ");

            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    CriarProjeto(excel, corel);
                    break;

                case "2":
                    AbrirProjetoExistente(corel);
                    break;

                case "3":
                    MenuFerramentas(corel);
                    break;

                case "4":
                    ListarArquivos(corel);
                    break;

                case "5":
                    SetarBibliotecaInterface(corel);
                    break;

                case "6":
                    SetarProjetoInterface(corel);
                    break;

                case "7":
                    Console.WriteLine("Saindo...");
                    Thread.Sleep(500);
                    return;

                default:
                    Console.WriteLine("Opção inválida! Pressione qualquer tecla.");
                    Console.ReadKey();
                    break;
            }
        }
    }

    static void CriarProjeto(ExcelRepository excel, CorelDraw corel)
    {
        Console.Clear();
        var panels = new List<string>();

        while (true)
        {
            Console.WriteLine("Digite o nome do painel:");
            var panel = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(panel))
                panels.Add(panel);

            Console.Write("Adicionar outro painel? (1) Sim (2) Não: ");
            if (Console.ReadLine() == "2")
                break;
        }

        Console.WriteLine("\nGerando projeto elétrico...");
        var create = new CreateProjects(excel, corel);
        create.Execute(panels);

        Console.WriteLine("Projeto criado com sucesso!");
        Console.ReadKey();
    }
    static void AbrirProjetoExistente(CorelDraw corel)
    {
        Console.Clear();
        Console.Write("Digite o caminho do arquivo .cdr: ");
        var path = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(path))
        {
            Console.WriteLine("Caminho inválido!");
            Console.ReadKey();
            return;
        }

        Spinner(() =>
        {
            var filePath = new FilePath($"{path}");
            corel.OpenDocumentExistProjeto(filePath);
        }, "Abrindo Arquivo");

        Console.WriteLine("Arquivo aberto!");
        Console.ReadKey();
    }
    static void MenuFerramentas(CorelDraw corel)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("==== FERRAMENTAS ====");
            Console.WriteLine("1 - Paginar projeto");
            Console.WriteLine("2 - Inserir Vem/Vai");
            Console.WriteLine("3 - Voltar");
            Console.Write("Escolha: ");

            switch (Console.ReadLine())
            {
                case "1":
                    corel.SetAllPagesNumber();
                    Console.WriteLine("Paginação concluída!");
                    Console.ReadKey();
                    break;

                case "2":
                    corel.SetAllVemVai();
                    Console.WriteLine("Vem/Vai atualizado!");
                    Console.ReadKey();
                    break;

                case "3":
                    return;

                default:
                    Console.WriteLine("Opção inválida!");
                    Console.ReadKey();
                    break;
            }
        }
    }

    static void ListarArquivos(CorelDraw corel)
    {
        var arquivos = corel.GetNameOpenFiles();

        Console.Clear();
        Console.WriteLine("=== Arquivos abertos no CorelDRAW ===\n");

        if (arquivos.Count == 0)
        {
            Console.WriteLine("Nenhum arquivo está aberto no Corel.");
        }
        else
        {
            foreach (var nome in arquivos)
                Console.WriteLine("- " + nome);
        }

        Console.WriteLine("\nPressione qualquer tecla para voltar...");
        Console.ReadKey();
    }

    static void SetarBibliotecaInterface(CorelDraw corel)
    {
        var bibliotecas = corel.ListFiles();

        if (bibliotecas == null || bibliotecas.Count == 0)
        {
            Console.WriteLine("Nenhuma biblioteca encontrada no Corel.");
            Console.ReadKey();
            return;
        }

        int selectedIndex = 0;
        ConsoleKey key;

        do
        {
            Console.Clear();
            Console.WriteLine($"Biblioteca de Partidas Atual: {corel.NomeArquivoBiblioteca()}");
            Console.WriteLine("Use ↑ ↓ para navegar | Enter ou → para selecionar | Esc para sair\n");

            for (int i = 0; i < bibliotecas.Count; i++)
            {
                if (i == selectedIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"> {bibliotecas[i]}");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"  {bibliotecas[i]}");
                }
            }

            key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    selectedIndex = (selectedIndex == 0) ? bibliotecas.Count - 1 : selectedIndex - 1;
                    break;

                case ConsoleKey.DownArrow:
                    selectedIndex = (selectedIndex == bibliotecas.Count - 1) ? 0 : selectedIndex + 1;
                    break;
            }

        } while (key != ConsoleKey.Enter && key != ConsoleKey.RightArrow && key != ConsoleKey.Escape);

        if (key == ConsoleKey.Escape)
            return;

        var bibliotecaSelecionada = bibliotecas[selectedIndex];

        try
        {
            corel.SetBibliotecaDePaineis(bibliotecaSelecionada);
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Biblioteca definida com sucesso!");
            Console.ResetColor();
            Console.WriteLine($"Biblioteca selecionada: {bibliotecaSelecionada}");
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Erro ao definir biblioteca:");
            Console.WriteLine(ex.Message);
            Console.ResetColor();
        }

        Console.WriteLine("\nPressione qualquer tecla para continuar...");
        Console.ReadKey();
    }

    static void SetarProjetoInterface(CorelDraw corel)
    {
        var arquivos = corel.ListFiles();

        if (arquivos == null || arquivos.Count == 0)
        {
            Console.WriteLine("Nenhum arquivo encontrado no Corel.");
            Console.ReadKey();
            return;
        }

        int selectedIndex = 0;
        ConsoleKey key;

        do
        {
            Console.Clear();
            Console.WriteLine($"Projeto Elétrico Atual: {corel.NomeArquivoProjetoEletrico()}");
            Console.WriteLine("Use ↑ ↓ para navegar | Enter ou → para selecionar | Esc para sair\n");

            for (int i = 0; i < arquivos.Count; i++)
            {
                if (i == selectedIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"> {arquivos[i]}");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"  {arquivos[i]}");
                }
            }

            key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    selectedIndex = (selectedIndex == 0) ? arquivos.Count - 1 : selectedIndex - 1;
                    break;

                case ConsoleKey.DownArrow:
                    selectedIndex = (selectedIndex == arquivos.Count - 1) ? 0 : selectedIndex + 1;
                    break;
            }

        } while (key != ConsoleKey.Enter && key != ConsoleKey.RightArrow && key != ConsoleKey.Escape);

        if (key == ConsoleKey.Escape)
            return;

        var arquivoSelecionado = arquivos[selectedIndex];

        try
        {
            corel.SetProjetoEletrico(arquivoSelecionado);
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Projeto definido com sucesso!");
            Console.ResetColor();
            Console.WriteLine($"Projeto selecionado: {arquivoSelecionado}");
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Erro ao definir projeto:");
            Console.WriteLine(ex.Message);
            Console.ResetColor();
        }

        Console.WriteLine("\nPressione qualquer tecla para continuar...");
        Console.ReadKey();
    }



    static void Spinner(Action action, string message)
    {
        var spinner = new[] { "|", "/", "-", "\\" };
        int i = 0;

        string fullMessage = message + " ";

        Console.CursorVisible = false;
        Console.Write(fullMessage);

        bool running = true;

        var thread = new Thread(() =>
        {
            while (running)
            {
                Console.Write(spinner[i++ % spinner.Length]);
                Thread.Sleep(250);
                Console.Write("\b");
            }
        });

        thread.Start();
        action();
        running = false;
        thread.Join();

        int totalChars = fullMessage.Length + 1;

        Console.Write("\r");                         
        Console.Write(new string(' ', totalChars));   
        Console.Write("\r");                          

        Console.CursorVisible = true;
    }
}
