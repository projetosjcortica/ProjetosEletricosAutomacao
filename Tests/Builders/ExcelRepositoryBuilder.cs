using Domain.Agreggates;
using Domain.Repositories;
using Domain.Value_Objects.Partidas;
using Moq;

namespace Tests.Builders
{
    public class ExcelRepositoryBuilder
    {
        private readonly Mock<IExcelRepository> _repository;
        public ExcelRepositoryBuilder()
        {
            _repository = new Mock<IExcelRepository>();
        }

        public void GetDescriptionPages(List<DescriptionPage> descriptionPages = null)
        {
            if(descriptionPages == null)
                descriptionPages = new List<DescriptionPage>
                {
                     new DescriptionPage("CAPA",  "13"),
                     new DescriptionPage("PT100-1", "P-SENS"),
                     new DescriptionPage("TV-01", "15"),
                     new DescriptionPage("TV-02", "15"),
                     new DescriptionPage("TV-03", "15")
                };



            _repository.Setup(repository => repository.GetDescriptionPages()).Returns(descriptionPages);
        }

        public void GetPageDataByNomenclatura(string Nomenclatura)
        {
            var PageData = new PageData();
            var DataAcionamentoPage = new Acionamento(Nomenclatura, "PD-2S", "Partida de Motor", "16DO-P05","1A-CT-1.1", "1A-ACT-1", "RL07", "2", "x1A", "Cabo PP 1x4mm2","",1);
            var DataReconhecimento = new Domain.Value_Objects.Partidas.Reconhecimento(Nomenclatura, "PD-2S", "Reconhecimento 1","16DO-P05","1A-CT-1.1","X1", " ", 3);

            PageData.Data.Add(DataAcionamentoPage);
            PageData.Data.Add(DataReconhecimento);

            _repository.Setup(repository => repository.GetPageDataByNomenclatura(Nomenclatura)).Returns(PageData);
        }

        public IExcelRepository Build() => _repository.Object;
    }
}
