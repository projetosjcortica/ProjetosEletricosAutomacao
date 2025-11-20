using Domain.Agreggates;
using Domain.Services.ProjectServices;
using Domain.Value_Objects.Partidas;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainTestes.Services
{
    public class FreioElevadorServiceTest
    {
        [Fact]
        public void Sucess_Case_Page_Is_Freio_Elevador_Service()
        {
            var acionamento1 = new Acionamento("EL-1", "ACT", "Elevador 1", "16-DO", "1A-CT-1.1", "1A-ACT-1", "RL01", "30", "", "", "", 1);
            var acionamento2 = new Acionamento("FR-EL-1", "ACT", "Freio Elevador 1", "16-DO", "1A-CT-1.1", "1A-ACT-1", "RL01", "30", "", "", "", 1);
            var acionamento3 = new Acionamento("EL-2", "ACT", "Elevador 2", "16-DO", "1A-CT-1.1", "1A-ACT-1", "RL01", "30", "", "", "", 1);
            var acionamento4 = new Acionamento("FR-EL-2", "ACT", "Freio Elevador 2", "16-DO", "1A-CT-1.1", "1A-ACT-1", "RL01", "30", "", "", "", 1);

            var pageData1 = new PageData();
            pageData1.InsertPageData(acionamento1);

            var pageData2 = new PageData();
            pageData2.InsertPageData(acionamento2);


            var pageData3 = new PageData();
            pageData3.InsertPageData(acionamento3);


            var pageData4 = new PageData();
            pageData4.InsertPageData(acionamento4);


            var pagina1 = new Page(1, new DescriptionPage("EL-1", "1"), pageData1);
            var pagina2 = new Page(1, new DescriptionPage("FR-EL-1", "1"), pageData2);
            var pagina3 = new Page(1, new DescriptionPage("EL-2", "1"), pageData3);
            var pagina4 = new Page(1, new DescriptionPage("FR-EL-2", "1"), pageData4);


            var project = new Project(new List<ProjectInfo>());

            project.AddPage(pagina1);
            project.AddPage(pagina2);
            project.AddPage(pagina3);
            project.AddPage(pagina4);


            var service = new FreioElevadorService();

            service.Execute(project);


            var shapes = project.Paginas.SelectMany(p => p.Shapes).Where(shape => shape.Name == "k_fr_el").ToList();
            shapes.Should().HaveCount(2);

            var shapesDescricao = project.Paginas.SelectMany(p => p.Shapes).Where(shape => shape.Name == "descricao_freio").ToList();
            shapesDescricao.Should().HaveCount(2);

            shapes[0].Text.Should().Be("K-EL-1");
            shapes[1].Text.Should().Be("K-EL-2");

            shapesDescricao[0].Text.Should().Be("Freio do Motor\r\nElevador 1");
            shapesDescricao[1].Text.Should().Be("Freio do Motor\r\nElevador 2");
        }
    }
}
