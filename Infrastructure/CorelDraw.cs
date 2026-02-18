using CorelDRAW;
using Domain.Agreggates;
using Domain.Infrastructure;
using Domain.Value_Objects;
using VGCore;
using Page = Domain.Agreggates.Page;

namespace Infrastructure
{
    public class CorelDraw : ICorelDraw
    {
        private CorelDRAW.Application CorelApp;
        private VGCore.Document BibliotecaDePartidas;
        private VGCore.Document ProjetoEletrico;
        private FilePath pathProjetoEletrico;
        private FilePath pathBibliotecaDePartidas;

        public string NomeArquivoProjetoEletrico()
        {
            try
            {
                if (ProjetoEletrico == null || string.IsNullOrWhiteSpace(ProjetoEletrico.Name))
                    return "Nenhum arquivo definido";

                return ProjetoEletrico.Name;
            }
            catch
            {
                return "Erro ao obter nome do arquivo";
            }
        }

        public string NomeArquivoBiblioteca()
        {
            try
            {
                if (BibliotecaDePartidas == null || string.IsNullOrWhiteSpace(BibliotecaDePartidas.Name))
                    return "Nenhum arquivo definido";

                return BibliotecaDePartidas.Name;
            }
            catch
            {
                return "Erro ao obter nome do arquivo";
            }
        }

        public CorelDraw(FilePath pathBibliotecaDePartidas, FilePath pathProjetoEletrico)
        {
            CorelApp = new CorelDRAW.Application();
            CorelApp.Visible = true;
            this.pathProjetoEletrico = pathProjetoEletrico;
            this.pathBibliotecaDePartidas = pathBibliotecaDePartidas;
        }

        public void Init()
        {
            if(BibliotecaDePartidas is null) 
                OpenDocumentExistBiblioteca(pathBibliotecaDePartidas);

            if (ProjetoEletrico is null)
                OpenDocumentExistProjeto(pathProjetoEletrico);
        }

        public VGCore.Document CreateFile(FilePath filePath)
        {
            var Document = CorelApp.Application.CreateDocument();
            Document.SaveAs(filePath.ToString());
            return Document;
        }

        public VGCore.Page CreateNewPage(VGCore.Document Document)
        {
            var newPage = Document.AddPages(1);
            newPage.Orientation = VGCore.cdrPageOrientation.cdrPortrait;
            return newPage;
        }

        public void CreateNewPageProjetoEletrico()
        {
            var newPage = ProjetoEletrico.AddPages(1);
            newPage.Orientation = VGCore.cdrPageOrientation.cdrPortrait;
        }

        public void MakeProject(Project Project, int index)
        {
            var ListPages = new List<VGCore.Page>();
            var filePathCopy = new FilePath(@"C:/Users/maqui/Desktop/Biblioteca de Partida - DEFINITIVA.cdr");
            var filePathCopied = new FilePath(@"C:/Users/maqui/Desktop/Testes CorelDraw.cdr");
            VGCore.Document Library;
            VGCore.Document Document;
            if (!DocumentIsOpen(filePathCopy.GetFileName()))
            {
                Library = OpenFile(filePathCopy);
            }
            else
            {
                Library = GetDocument(filePathCopy.GetFileName());
            }

            if (!DocumentIsOpen(filePathCopied.GetFileName()))
            {
                Document = CreateFile(filePathCopied);
            }
            else
            {
                Document = GetDocument(filePathCopied.GetFileName());
            }

            BibliotecaDePartidas = Document;

            foreach (var pagina in Project.Paginas)
            {
                int.TryParse(pagina.DescriptionPage.PageReference, out int CopyPageNumer);

                var CopyPage = Library.Pages[CopyPageNumer];

                VGCore.Page Page;
                if (Project.Paginas.FirstOrDefault().Equals(pagina) && index == 1)
                {
                    Page = Document.Pages[1];
                }
                else
                {
                    Page = CreateNewPage(Document);
                }

                DuplicatePageBetweenFiles(CopyPage, Page);
                InsertDataInPage(Page, pagina.GetPageData());
                InsertDataInPage(Page, Project.GetProjectInfos());
                UpdateShapes(Page, pagina);
            }
        }
        public void UpdateShapesOnPage(Page pageProject)
        {
            foreach (var shape in pageProject.Shapes)
            {
                var foundShapes = ProjetoEletrico.Pages[pageProject.PageNumber].Shapes.FindShapes(Name: shape.Name);

                foreach (VGCore.Shape shapeCorel in foundShapes)
                {
                    try
                    {
                        shapeCorel.Text.Contents = shape.Text;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Não foi possível escrever o texto:{shape.Text} no shape:{shape.Name}");
                        PaintShapeRed(shapeCorel);
                    }
                }
            }
        }



        public void UpdateShapes(VGCore.Page page, Page pageProject)
        {
            foreach (var shape in pageProject.Shapes)
            {
                var foundShapes = page.Shapes.FindShapes(Name: shape.Name);

                foreach (VGCore.Shape shapeCorel in foundShapes)
                {
                    try
                    {
                        shapeCorel.Text.Contents = shape.Text;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Não foi possível escrever o texto:{shape.Text} no shape:{shape.Name}");
                        PaintShapeRed(shapeCorel);
                    }
                }
            }
        }

        public void InsertDataInPage(Page pagina)
        {           
            foreach (var value in pagina.GetPageData())
            {
                var Shapes = ProjetoEletrico.Pages[pagina.PageNumber].FindShapes(value.Key);

                for (int i = 1; i <= Shapes.Count; i++)
                {
                    try
                    {
                        if (value.Value == string.Empty)
                        {
                            Shapes[i].Text.Contents = "null";
                            continue;
                        }
                        Shapes[i].Text.Contents = value.Value;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Não foi possível escrever o valor do objeto com o Name: {Shapes[i].Name}");
                        PaintShapeRed(Shapes[i]);
                    }

                }
            }
        }

        private void InsertDataInPage(VGCore.Page Page, Dictionary<string, string> Data)
        {
            foreach (var value in Data)
            {
                var Shapes = Page.FindShapes(value.Key);

                for (int i = 1; i <= Shapes.Count; i++)
                {
                    try
                    {
                        if (value.Value == string.Empty)
                        {
                            Shapes[i].Text.Contents = "null";
                            continue;
                        }
                        Shapes[i].Text.Contents = value.Value;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Não foi possível escrever o valor do objeto com o Name: {Shapes[i].Name}");
                        PaintShapeRed(Shapes[i]);
                    }

                }
            }
        }

        private void PaintShapeRed(VGCore.Shape shape)
        {
            try
            {
                shape.Fill.UniformColor.RGBAssign(255, 0, 0);
                shape.Outline.Color.RGBAssign(255, 0, 0);
            }
            catch
            {
                Console.WriteLine($"Não foi pintar de vermelho o shape: {shape.Name}");
            }
        }

        public void OpenDocumentExistBiblioteca(FilePath filePath)
        {
            var nomeArquivo = filePath.GetFileName();
            if (DocumentIsOpen(nomeArquivo))
            {
                BibliotecaDePartidas = GetDocument(nomeArquivo);
            }
            else
            {
                BibliotecaDePartidas = CorelApp.Application.OpenDocument(filePath.ToString());
            }

        }

        public void OpenDocumentExistProjeto(FilePath filePath)
        {
            var nomeArquivo = filePath.GetFileName();
            if (DocumentIsOpen(nomeArquivo))
            {
                ProjetoEletrico = GetDocument(nomeArquivo);
            }
            else
            {
                ProjetoEletrico = CorelApp.Application.OpenDocument(filePath.ToString());
            }
        }
        public VGCore.Document OpenFile(FilePath FilePath)
        {
            return CorelApp.Application.OpenDocument(FilePath.ToString());
        }

        public void DuplicatePageBetweenFiles(VGCore.Page copyPage, VGCore.Page copiedPage)
        {
            var doc = copyPage.Application.ActiveDocument;

            // Limpa seleção atual
            doc.ClearSelection();

            // Mantém orientação igual
            copiedPage.Orientation = copyPage.Orientation;

            // Desbloqueia shapes temporariamente
            var lockedShapes = new List<VGCore.Shape>();
            foreach (VGCore.Shape s in copyPage.ActiveLayer.Shapes.All())
            {
                if (s.Locked)
                {
                    lockedShapes.Add(s);
                    s.Locked = false;
                }
            }

            try
            {
                // Copia todos os shapes da página
                var shapesFrom = copyPage.ActiveLayer.Shapes.All();
                shapesFrom.Copy();

                // Cola na página destino
                copiedPage.ActiveLayer.Paste();

                // Espera até que os shapes apareçam (robustez contra lag do PC)
                int attempts = 0;
                while (copiedPage.ActiveLayer.Shapes.Count < shapesFrom.Count && attempts < 15)
                {
                    System.Threading.Thread.Sleep(1000); // espera 50ms
                    attempts++;
                }
            }
            finally
            {
                // Restaura bloqueio original dos shapes
                foreach (var s in lockedShapes)
                {
                    s.Locked = true;
                }
            }
        }

        public void DuplicatePageBetweenFiles(int numeroPaginaCopiada, int numeroPaginaNova)
        {
            var copyPage = BibliotecaDePartidas.Pages[numeroPaginaCopiada];
            var copiedPage = ProjetoEletrico.Pages[numeroPaginaNova];

            var doc = copyPage.Application.ActiveDocument;

            // Limpa seleção atual
            doc.ClearSelection();

            // Mantém orientação igual
            copiedPage.Orientation = copyPage.Orientation;

            // Desbloqueia shapes temporariamente
            var lockedShapes = new List<VGCore.Shape>();
            foreach (VGCore.Shape s in copyPage.ActiveLayer.Shapes.All())
            {
                if (s.Locked)
                {
                    lockedShapes.Add(s);
                    s.Locked = false;
                }
            }

            try
            {
                // Copia todos os shapes da página
                var shapesFrom = copyPage.ActiveLayer.Shapes.All();
                shapesFrom.Copy();

                // Cola na página destino
                copiedPage.ActiveLayer.Paste();

                // Espera até que os shapes apareçam (robustez contra lag do PC)
                int attempts = 0;
                while (copiedPage.ActiveLayer.Shapes.Count < shapesFrom.Count && attempts < 15)
                {
                    System.Threading.Thread.Sleep(1000); // espera 50ms
                    attempts++;
                }
            }
            finally
            {
                // Restaura bloqueio original dos shapes
                foreach (var s in lockedShapes)
                {
                    s.Locked = true;
                }
            }
        }

        private bool DocumentIsOpen(string DocumentName)
        {
            for (int i = 1; i <= CorelApp.Application.Documents.Count; i++)
            {
                var Documento = CorelApp.Application.Documents[i];
                if (Documento.Name == DocumentName) return true;
            }

            return false;
        }

        private VGCore.Document GetDocument(string DocumentName)
        {
            for (int i = 1; i <= CorelApp.Application.Documents.Count; i++)
            {
                var Document = CorelApp.Application.Documents[i];
                if (Document.Name == DocumentName)
                    return Document;
            }

            return null;
        }

        public void SetAllPagesNumber()
        {
            for (int i = 0; i <= ProjetoEletrico.Pages.Count; i++)
            {
                var Page = ProjetoEletrico.Pages[i];
                var PaginaShape = Page.FindShape("pagina");
                if (PaginaShape != null)
                {
                    PaginaShape.Text.Contents =(i).ToString();
                }
            }
        }



        public void SetAllVemVai()
        {
            var listCables = new List<string> { "pe", "24v1", "0v1", "rst", "24v2", "0v2", "r1", "s1", "24vfdc1", "n", "r2", "s2", "dp", "s0", "anilha_atuador_positivo_fdc_1", "anilha_atuador_negativo_fdc_1", "anilha_atuador_positivo_2_fdc_1", "anilha_atuador_negativo_2_fdc_1", "atuador_positivo", "atuador_negativo" };

            foreach (var cable in listCables)
            {
                try
                {
                    SetCableOnPages(cable);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        public void SetCableOnPages(string cable)
        {
            var ListPagesWith24V = new List<VGCore.Page>();
            for (int i = 1; i <= ProjetoEletrico.Pages.Count; i++)
            {
                var Page = ProjetoEletrico.Pages[i];
                var Vem24V = Page.FindShape($"vem_{cable}");
                var Vai24V = Page.FindShape($"vai_{cable}");
                if (Vem24V != null || Vai24V != null)
                {
                    ListPagesWith24V.Add(Page);
                }
            }

            var ListPagesNumberWith24V = ListPagesWith24V
                                            .Select(item => item.Index.ToString())
                                            .ToList();

            var ListGroupPagesNumber = GroupPagesNumber(ListPagesNumberWith24V);

            for (int i = 0; i < ListPagesWith24V.Count; i++)
            {
                var shapesLeft = ListPagesWith24V[i].FindShapes($"vem_{cable}");
                var shapesRight = ListPagesWith24V[i].FindShapes($"vai_{cable}");

                var valueShapeLeft = $"(Vem da fl. {ListGroupPagesNumber[i][0].PadLeft(2, '0')})";
                var valueShapeRight = $"(Vai p/ fl. {ListGroupPagesNumber[i][1].PadLeft(2, '0')})";

                if (ListGroupPagesNumber[i][0] != string.Empty)
                {
                    foreach (VGCore.Shape s in shapesLeft)
                    {
                        try
                        {
                            if (s.Type == VGCore.cdrShapeType.cdrTextShape)
                            {
                                s.Text.Contents = valueShapeLeft;
                            }
                        }
                        catch
                        {
                            Console.WriteLine("Erro ao inserir texto:" + s.Name);
                        }
                    }
                }

                if (ListGroupPagesNumber[i][1] != string.Empty)
                {
                    foreach (VGCore.Shape s in shapesRight)
                    {
                        try
                        {
                            if (s.Type == VGCore.cdrShapeType.cdrTextShape)
                            {
                                s.Text.Contents = valueShapeRight;
                            }
                        }
                        catch
                        {
                            Console.WriteLine("Erro ao inserir texto:" + s.Name);
                        }
                    }
                }
            }
        }

        public List<List<string>> GroupPagesNumber(List<string> input)
        {
            var Result = new List<List<string>>();

            for (int i = 0; i < input.Count; i++)
            {
                var currentNumber = i - 1;
                var nextNumber = i + 1;

                var currentPageNumber = "";
                var nextPageNumber = "";
                var GroupPages = new List<string>();

                if (currentNumber < 0)
                {
                    nextPageNumber = input[nextNumber];
                    GroupPages = new List<string> { currentPageNumber, nextPageNumber };
                    Result.Add(GroupPages);
                    continue;
                }

                if (nextNumber == input.Count)
                {
                    currentPageNumber = input[currentNumber];
                    GroupPages = new List<string> { currentPageNumber, nextPageNumber };
                    Result.Add(GroupPages);
                    continue;
                }

                currentPageNumber = input[currentNumber];
                nextPageNumber = input[nextNumber];
                GroupPages = new List<string> { currentPageNumber, nextPageNumber };

                Result.Add(GroupPages);
            }

            return Result;
        }

        public void DeleteShapes(Project project)
        {
            var indexPage = 2;
            foreach (var page in project.Paginas)
            {
                var deletedShapes = page.Shapes.Where(s => s.IsDeletable);

                foreach (var shape in deletedShapes)
                {
                    var deleteShape = ProjetoEletrico.Pages[indexPage].FindShape(shape.Name);

                    if (deleteShape != null)
                        deleteShape.Delete();
                }
                indexPage++;
            }
        }

        public bool ExistShapeOnPage(int pageNumber, string shapeName)
        {
            var pageNumberIndex = pageNumber;
            if (pageNumberIndex < 0 || pageNumberIndex > ProjetoEletrico.Pages.Count)
            {
                return false;
            }
            var shape = ProjetoEletrico.Pages[pageNumberIndex].FindShape(shapeName);
            if (shape != null)
                return true;
            return false;
        }

        public void DeleteShapeOnPage(int pageNumber, string shapeName)
        {
            var pageNumberIndex = pageNumber;
            if (pageNumberIndex < 0 || pageNumberIndex > ProjetoEletrico.Pages.Count)
            {
                return;
            }
            var shapes = ProjetoEletrico.Pages[pageNumberIndex].FindShapes(shapeName);


            for (int i = 1; i <= shapes.Count; i++)
            {
                var shape = shapes[i];
                shape?.Delete();
            }
        }

        public int FindFirstPageWithShape(string shapeName)
        {
            for (int i = 1; i <= ProjetoEletrico.Pages.Count; i++)
            {
                var page = ProjetoEletrico.Pages[i];
                var shape = page.FindShape(shapeName);

                if (shape != null)
                    return i;
            }

            return 0;
        }

        public void UngroupShapeOnPage(int pageNumber, string shapeName)
        {
            var pageNumberIndex = pageNumber;
            if (pageNumberIndex < 0 || pageNumberIndex > ProjetoEletrico.Pages.Count)
            {
                return;
            }
            var shapes = ProjetoEletrico.Pages[pageNumberIndex].FindShapes(shapeName);


            for (int i = 1; i <= shapes.Count; i++)
            {
                var shape = shapes[i];
                if (shape != null && shape.Type == VGCore.cdrShapeType.cdrGroupShape)
                {
                    shape.Ungroup();
                }

            }
        }

        public void InsertDataInPage(int pageNumber, Page page)
        {
            var Data = page.GetPageData();
            var pageCorel = ProjetoEletrico.Pages[pageNumber - 1];
            InsertDataInPage(pageCorel, Data);
        }


        public void Renomear24V1(Project projeto)
        {
            if (projeto.Painel != "CCM-1D")
                return;

            foreach (var pagina in projeto.Paginas)
            {
                var currentPage = ProjetoEletrico.Pages[pagina.PageNumber];
                if (currentPage == null)
                    continue;

                // Encontrar shapes por nome
                var anilhas24VVem = currentPage.FindShapes("vem_24v1");
                var anilhas24VVai = currentPage.FindShapes("vai_24v1");
                var anilhas0VVem = currentPage.FindShapes("vem_0v1");
                var anilhas0VVai = currentPage.FindShapes("vai_0v1");

                // Renomear shapes
                foreach (VGCore.Shape shape in anilhas24VVem)
                    shape.Name = "vem_anilha_atuador_positivo_fdc_1";

                foreach (VGCore.Shape shape in anilhas24VVai)
                    shape.Name = "vai_anilha_atuador_positivo_fdc_1";

                foreach (VGCore.Shape shape in anilhas0VVem)
                    shape.Name = "vem_anilha_atuador_negativo_fdc_1";

                foreach (VGCore.Shape shape in anilhas0VVai)
                    shape.Name = "vai_anilha_atuador_negativo_fdc_1";

                // Agora substitui o texto visível "24V.1" -> "1D-24V-FDC1"
                foreach (VGCore.Shape shape in currentPage.Shapes)
                {
                    try
                    {
                        // Só tenta se for shape de texto
                        if (shape.Type == VGCore.cdrShapeType.cdrTextShape)
                        {
                            var texto = shape.Text.Contents.Trim();

                            if (texto == "24V.1")
                                shape.Text.Contents = "1D-24V-FDC1";
                            else if (texto == "0V.1")
                                shape.Text.Contents = "1D-0V-FDC1";
                        }
                    }
                    catch
                    {
                        // Ignora shapes que não suportam texto (evita COMException)
                        continue;
                    }
                }
            }
        }
        public void Renomear24V1Gerado()
        {

            foreach (VGCore.Page pagina in ProjetoEletrico.Pages)
            {
                VGCore.Page currentPage = pagina;

                var anilhas24VVem = currentPage.FindShapes("vem_24v1");
                var anilhas24VVai = currentPage.FindShapes("vai_24v1");
                var anilhas0VVem = currentPage.FindShapes("vem_0v1");
                var anilhas0VVai = currentPage.FindShapes("vai_0v1");

                foreach (VGCore.Shape shape in anilhas24VVem)
                    shape.Name = "vem_anilha_atuador_positivo_fdc_1";

                foreach (VGCore.Shape shape in anilhas24VVai)
                    shape.Name = "vai_anilha_atuador_positivo_fdc_1";

                foreach (VGCore.Shape shape in anilhas0VVem)
                    shape.Name = "vem_anilha_atuador_negativo_fdc_1";

                foreach (VGCore.Shape shape in anilhas0VVai)
                    shape.Name = "vai_anilha_atuador_negativo_fdc_1";

                foreach (VGCore.Shape shape in currentPage.Shapes)
                {
                    try
                    {
                        if (shape.Type == VGCore.cdrShapeType.cdrTextShape)
                        {
                            var texto = shape.Text.Contents.Trim();

                            if (texto == "24V.1")
                                shape.Text.Contents = "1D-24V-FDC1";
                            else if (texto == "0V.1")
                                shape.Text.Contents = "1D-0V-FDC1";
                        }
                    }
                    catch
                    {

                        continue;
                    }
                }
            }
        }
        public void Renomear24V2Gerado()
        {

            foreach (VGCore.Page pagina in ProjetoEletrico.Pages)
            {
                VGCore.Page currentPage = pagina;

                var anilhas24VVem = currentPage.FindShapes("vem_24v2");
                var anilhas24VVai = currentPage.FindShapes("vai_24v2");
                var anilhas0VVem = currentPage.FindShapes("vem_0v2");
                var anilhas0VVai = currentPage.FindShapes("vai_0v2");

                foreach (VGCore.Shape shape in anilhas24VVem)
                    shape.Name = "vem_anilha_atuador_positivo_2_fdc_1";

                foreach (VGCore.Shape shape in anilhas24VVai)
                    shape.Name = "vai_anilha_atuador_positivo_2_fdc_1";

                foreach (VGCore.Shape shape in anilhas0VVem)
                    shape.Name = "vem_anilha_atuador_negativo_2_fdc_1";

                foreach (VGCore.Shape shape in anilhas0VVai)
                    shape.Name = "vai_anilha_atuador_negativo_2_fdc_1";

                foreach (VGCore.Shape shape in currentPage.Shapes)
                {
                    try
                    {
                        if (shape.Type == VGCore.cdrShapeType.cdrTextShape)
                        {
                            var texto = shape.Text.Contents.Trim();

                            if (texto == "24V.2")
                                shape.Text.Contents = "1D-24V.2-FDC1";
                            else if (texto == "0V.2")
                                shape.Text.Contents = "1D-0V.2-FDC1";
                        }
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
        }



        public void Renomear24V2(Project projeto)
        {
            if (projeto.Painel != "CCM-1D")
                return;

            foreach (var pagina in projeto.Paginas)
            {
                var currentPage = ProjetoEletrico.Pages[pagina.PageNumber];
                if (currentPage == null)
                    continue;

                var anilhas24VVem = currentPage.FindShapes("vem_24v2");
                var anilhas24VVai = currentPage.FindShapes("vai_24v2");
                var anilhas0VVem = currentPage.FindShapes("vem_0v2");
                var anilhas0VVai = currentPage.FindShapes("vai_0v2");

                foreach (VGCore.Shape shape in anilhas24VVem)
                    shape.Name = "vem_anilha_atuador_positivo_2_fdc_1";

                foreach (VGCore.Shape shape in anilhas24VVai)
                    shape.Name = "vai_anilha_atuador_positivo_2_fdc_1";

                foreach (VGCore.Shape shape in anilhas0VVem)
                    shape.Name = "vem_anilha_atuador_negativo_2_fdc_1";

                foreach (VGCore.Shape shape in anilhas0VVai)
                    shape.Name = "vai_anilha_atuador_negativo_2_fdc_1";

                foreach (VGCore.Shape shape in currentPage.Shapes)
                {
                    try
                    {
                        if (shape.Type == VGCore.cdrShapeType.cdrTextShape)
                        {
                            var texto = shape.Text.Contents.Trim();

                            if (texto == "24V.2")
                                shape.Text.Contents = "1D-24V.2-FDC1";

                            else if (texto == "0V.2")
                                shape.Text.Contents = "1D-0V.2-FDC1";
                        }
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
        }

        public List<int> FindPagesWithShape(string shapeName)
        {
            var list = new List<int>();
            for (int i = 1; i <= ProjetoEletrico.Pages.Count; i++)
            {
                var page = ProjetoEletrico.Pages[i];
                var shape = page.FindShape(shapeName);

                if (shape != null)
                    list.Add(i);
            }

            return list;
        }

        public void ChangeNameShapeOnPage(Page pagina, string currentName, string newName)
        {
            var currentPage = ProjetoEletrico.Pages[pagina.PageNumber];

            var shapes = currentPage.FindShapes(currentName);

            foreach (VGCore.Shape shape in shapes)
            {
                try
                {
                    shape.Name = newName;
                }
                catch
                {
                    Console.WriteLine($"Não foi possivel trocar o nome do Shape: {shape.Name} por {newName}");
                }
            }
        }

        public void ChangeTextOnPage(Page pagina, string currentText, string newText)
        {
            var currentPage = ProjetoEletrico.Pages[pagina.PageNumber];

            var shapes = currentPage.FindShapes(
               Type: VGCore.cdrShapeType.cdrTextShape,
               Name: null
           );

            foreach (VGCore.Shape shape in shapes)
            {
                if (shape.Text != null && shape.Text.Contents == currentText)
                {
                    shape.Text.Contents = newText;
                }
            }
        }

        public void InsertDataInPage(Page pagina, Dictionary<string, string> data)
        {
            foreach (var value in data)
            {
                var Shapes = ProjetoEletrico.Pages[pagina.PageNumber].FindShapes(value.Key);

                for (int i = 1; i <= Shapes.Count; i++)
                {
                    try
                    {
                        if (value.Value == string.Empty)
                        {
                            Shapes[i].Text.Contents = "null";
                            continue;
                        }
                        Shapes[i].Text.Contents = value.Value;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Não foi possível escrever o valor do objeto com o Name: {Shapes[i].Name}");
                        PaintShapeRed(Shapes[i]);
                    }

                }
            }
        }

        public List<string> GetNameOpenFiles()
        {
            var names = new List<string>();

            foreach (VGCore.Document doc in CorelApp.Documents)
            {
                names.Add(doc.Name);
            }

            return names; 
        }

        public void SetBibliotecaDePaineis(string nomeArquivo)
        {
            // Verifica se o documento está aberto
            if (!DocumentIsOpen(nomeArquivo))
                throw new Exception($"O arquivo '{nomeArquivo}' não está aberto no Corel.");

            // Obtém o documento que já está aberto
            BibliotecaDePartidas = GetDocument(nomeArquivo);
        }

        public void SetProjetoEletrico(string nomeArquivo)
        {
            if (!DocumentIsOpen(nomeArquivo))
                throw new Exception($"O arquivo '{nomeArquivo}' não está aberto no Corel.");

            ProjetoEletrico = GetDocument(nomeArquivo);
        }


        public List<string> ListFiles()
        {
            var listFiles = new List<string>();

            foreach (VGCore.Document document in CorelApp.Documents)
            {
                listFiles.Add(document.Name);
            }

            return listFiles;
        }

    }
}
