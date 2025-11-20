using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Agreggates;
using Domain.Value_Objects;

namespace Domain.Infrastructure
{
    public interface ICorelDraw
    {
        void MakeProject(Project Project, int index);
        bool ExistShapeOnPage(int pageNumber, string shapeName);
        void DeleteShapeOnPage(int pageNumber, string shapeName);
        int FindFirstPageWithShape(string shapeName);
        List<int> FindPagesWithShape(string shapeName);
        void UngroupShapeOnPage(int pageNumber, string shapeName);
        void UpdateShapesOnPage(Page pagina);
        void InsertDataInPage(Page pagina);
        void InsertDataInPage(Page pagina, Dictionary<string, string> data);
        void DuplicatePageBetweenFiles(int numeroPaginaCopiada, int numeroPaginaNova);
        void CreateNewPageProjetoEletrico();
        void Init();
        void ChangeNameShapeOnPage(Page pagina, string currentName, string newName);
        void ChangeTextOnPage(Page pagina, string currentText, string newText);
    }
}
