namespace CLI.Commands
{
    static public class SelecionarPastaScroll
    {
        const int NUMBER_SHOW_FOLDERS = 15;
        static readonly string BASE_PATH = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        static string CurrentPath = BASE_PATH;

        static int pointerStartShowFolder = 0;
        static int pointerEndShowFolder = NUMBER_SHOW_FOLDERS;
        static int indexSelectFolder = 0;
        static int indexRelativeSelectFolder = 0;
        static List<string> AllFoldersForCurrentPath = GetAllFolders(CurrentPath);
        public static string? Execute()
        {
            while (true)
            {
                Console.Clear();

                if (isEmptyFolder(AllFoldersForCurrentPath))
                {
                    Console.WriteLine("Nenhuma pasta encontrada.");
                }
                var selectedFolder = AllFoldersForCurrentPath.ElementAtOrDefault(indexSelectFolder);
                ShowHeader();

                var renderedFolders = GetRenderedFolders();

                for (int i = 0; i < renderedFolders.Count; i++)
                {
                    var isSelectedFolder = renderedFolders[i] == AllFoldersForCurrentPath[indexSelectFolder];
                    if (isSelectedFolder)
                        RenderHighlightedFolder(renderedFolders, i);
                    else
                        RenderNormalFolder(renderedFolders, i);
                }

                var currentKey = Console.ReadKey(true);

                if (currentKey.Key == ConsoleKey.UpArrow && indexSelectFolder > 0)
                {
                    indexSelectFolder--;
                    indexRelativeSelectFolder--;
                    var isBorderUp = indexRelativeSelectFolder == (pointerStartShowFolder - 1);
                    if (isBorderUp)
                    {
                        pointerStartShowFolder--;
                        pointerEndShowFolder--;
                    }
                }
                else if (currentKey.Key == ConsoleKey.DownArrow && indexSelectFolder < AllFoldersForCurrentPath.Count - 1)
                {
                    indexSelectFolder++;
                    indexRelativeSelectFolder++;

                    var isBoderDown = indexRelativeSelectFolder == (pointerEndShowFolder);
                    if (isBoderDown)
                    {
                        pointerEndShowFolder++;
                        pointerStartShowFolder++;
                    }
                }
                else if (currentKey.Key == ConsoleKey.RightArrow)
                {
                    CurrentPath += "\\" + selectedFolder;
                    indexSelectFolder = 0;
                    indexRelativeSelectFolder = 0;
                    AllFoldersForCurrentPath = GetAllFolders(CurrentPath);

                    pointerStartShowFolder = 0;
                    pointerEndShowFolder = NUMBER_SHOW_FOLDERS;
                }
                else if (currentKey.Key == ConsoleKey.LeftArrow)
                {
                    var parent = Directory.GetParent(CurrentPath);

                    if (parent != null)
                    {
                        CurrentPath = parent.FullName;
                        indexSelectFolder = 0;
                        AllFoldersForCurrentPath = GetAllFolders(CurrentPath);
                    }

                    pointerStartShowFolder = 0;
                    pointerEndShowFolder = NUMBER_SHOW_FOLDERS;
                }
                else if (currentKey.Key == ConsoleKey.Enter)
                    return CurrentPath += "\\" + selectedFolder;
            }
        }

        private static void RenderNormalFolder(List<string?> renderedFolders, int i)
        {
            Console.WriteLine($"  {renderedFolders[i]}".PadRight(Console.WindowWidth));
        }

        private static void RenderHighlightedFolder(List<string?> renderedFolders, int i)
        {
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine($"> {renderedFolders[i]}".PadRight(Console.WindowWidth));
            Console.ResetColor();
        }

        private static List<string> GetRenderedFolders()
        {
            var renderedFolders = AllFoldersForCurrentPath
                    .Skip(pointerStartShowFolder)
                    .Take(pointerEndShowFolder - pointerStartShowFolder)
                    .ToList();

            if(renderedFolders.Count == 0 )
            {
                renderedFolders.Add("Pasta Vazia");
            }

            return renderedFolders; 
        }

        private static void ShowHeader()
        {
            Console.WriteLine($"Pasta Atual: {CurrentPath}\n");
            Console.WriteLine("Selecione uma pasta (↑ ↓ Enter):\n");
        }

        private static bool isEmptyFolder(List<string> folders)
        {
            return folders.Count == 0;
        }
        private static List<string> GetAllFolders(string path)
        {
            string desktop;
            List<string?> folders;
            folders = Directory.GetDirectories(path)
                                  .Select(Path.GetFileName)
                                  .ToList();
            return folders;
        }
    }
}
