using Domain.Agreggates;
using Domain.Value_Objects;
using Domain.Value_Objects.Partidas;

namespace Domain.Repositories
{
    public interface IExcelRepository
    {
        string GetPanelName();
        void SetFilePath(string filePath);
        void SetPanelName(string panelName);
        PageData GetPageDataByNomenclatura(string Nomenclatura);
        List<DescriptionPage> GetDescriptionPages();
        List<ProjectInfo> GetInformacoesEspeciais();

        List<Fusivel> GetFusiveis();
    }
}
