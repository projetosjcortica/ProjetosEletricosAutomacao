using ClosedXML.Excel;
using Domain;
using Domain.Agreggates;
using Domain.Repositories;
using Domain.Value_Objects;
using Domain.Value_Objects.Partidas;

namespace Infrastructure.Repositories
{
    public class ExcelRepository : IExcelRepository
    {
        private FilePath FilePath { get; set; }
        private string PanelName { get; set; }

        public ExcelRepository(string filePath, string panelName)
        {
            FilePath = new FilePath(filePath);
            PanelName = panelName;
        }

        public ExcelRepository()
        {           
        }

        public List<DescriptionPage> GetDescriptionPages()
        {
            var result = new List<DescriptionPage>();
            using (var workbook = new XLWorkbook(FilePath.ToString()))
            {
                var worksheet = workbook.Worksheet($"Descrição de Projeto CCM-1A");

                var rowsWithValues = worksheet.RowsUsed().Skip(1);

                foreach (var row in rowsWithValues)
                {
                    var DescriptionPage = new DescriptionPage(row.Cell(1).Value.ToString(), row.Cell(2).Value.ToString());
                    result.Add(DescriptionPage);
                }
            }

            return result;
        }
        public PageData GetPageDataByNomenclatura(string Nomenclatura)
        {
            var PageData = new PageData();

            var DataExcelList = new[]
            {
                GetAcionamentoByNomenclatura(Nomenclatura),
                GetReconhecimentoByNomenclatura(Nomenclatura),
            };

            foreach (var DataExcel in DataExcelList)
            {
                foreach(var DataExcel2 in DataExcel)
                {
                    PageData.Data.Add(DataExcel2);
                }
            }

            return PageData;
        }

        private List<IDataPage> GetAcionamentoByNomenclatura(string Nomenclatura)
        {
            var Result = new List<IDataPage>();

            using (var workbook = new XLWorkbook(FilePath.ToString()))
            {
                var worksheet = workbook.Worksheet($"Acionamento CCM-1A");

                var rowsWithValues = worksheet.RowsUsed().Skip(1);

                foreach (var row in rowsWithValues)
                {
                    if (row.Cell(1).Value.ToString() == Nomenclatura)
                    {
                        var DataAcionamento = new Acionamento(
                            Nomenclatura,
                            row.Cell(2).Value.ToString(),
                            row.Cell(3).Value.ToString(),
                            row.Cell(4).Value.ToString(),
                            row.Cell(5).Value.ToString(),
                            row.Cell(6).Value.ToString(),
                            row.Cell(7).Value.ToString(),
                            row.Cell(8).Value.ToString(),
                            row.Cell(9).Value.ToString(),
                            row.Cell(10).Value.ToString(),
                            row.Cell(11).Value.ToString(),
                            Result.Count + 1);

                        Result.Add(DataAcionamento); 
                    }
                }
            }

            return Result;
        }

        private List<IDataPage> GetReconhecimentoByNomenclatura(string Nomenclatura)
        {
            var Result = new List<IDataPage>();
            using (var workbook = new XLWorkbook(FilePath.ToString()))
            {
                var worksheet = workbook.Worksheet($"Reconhecimento CCM-1A");

                var rowsWithValues = worksheet.RowsUsed().Skip(1);

                foreach (var row in rowsWithValues)
                {
                    if (row.Cell(1).Value.ToString() == Nomenclatura)
                    {
                        var DataReconhecimento = new Reconhecimento(
                            Nomenclatura,
                            row.Cell(2).Value.ToString(),
                            row.Cell(3).Value.ToString(),
                            row.Cell(4).Value.ToString(),
                            row.Cell(5).Value.ToString(),
                            row.Cell(6).Value.ToString(),
                            row.Cell(7).Value.ToString(),
                            Result.Count + 1);

                        Result.Add(DataReconhecimento);
                    }
                }
            }

            return Result;
        }

        public List<ProjectInfo> GetInformacoesEspeciais()
        {
            var ProjectInfos = new List<ProjectInfo>();
            using (var workbook = new XLWorkbook(FilePath.ToString()))
            {
                var worksheet = workbook.Worksheet($"Informações Especiais CCM-1A");

                var rowsWithValues = worksheet.RowsUsed().Skip(1);

                foreach (var row in rowsWithValues)
                {
                    var ProjectInfo = new ProjectInfo(row.Cell(1).Value.ToString(), row.Cell(2).Value.ToString());
                    ProjectInfos.Add(ProjectInfo);
                }
            }

            return ProjectInfos;
        }

        public void SetFilePath(string pathFile)
        {
            FilePath = new FilePath(pathFile);
        }

        public void SetPanelName(string panelName)
        {
            PanelName = panelName;
        }

        public List<Fusivel> GetFusiveis()
        {
            throw new NotImplementedException();
        }

        public string GetPanelName()
        {
            return string.IsNullOrWhiteSpace(PanelName) ? "CCM-1A" : PanelName;
        }
    }
}
