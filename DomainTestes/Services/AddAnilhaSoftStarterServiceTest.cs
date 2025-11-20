using Domain
    .Agreggates;
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
    public class AddAnilhaSoftStarterServiceTest
    {
        [Fact]
        public void Sucess_Case_Page_Is_Soft_Starter()
        {
            var acionamento1 = new Acionamento("SS-CIC-1", "ACT", "Ciclone", "16-DO", "1A-CT-1.1", "1A-ACT-1", "RL01", "30", "", "", "", 1);
            var acionamento2 = new Acionamento("SS-CIC-2", "ACT", "Ciclone", "16-DO", "1A-CT-1.1", "1A-ACT-1", "RL01", "30", "", "", "", 1);
            var acionamento3 = new Acionamento("SS-CIC-3", "ACT", "Ciclone", "16-DO", "1A-CT-1.1", "1A-ACT-1", "RL01", "30", "", "", "", 1);
            var acionamento4 = new Acionamento("TV-1", "ACT", "Transporte Vibratório 1", "16-DO", "1A-CT-1.1", "1A-ACT-1", "RL01", "30", "", "", "", 1);

            var pageData1 = new PageData();
            pageData1.InsertPageData(acionamento1);

            var pageData2 = new PageData();
            pageData2.InsertPageData(acionamento2);


            var pageData3 = new PageData();
            pageData3.InsertPageData(acionamento3);


            var pageData4 = new PageData();
            pageData4.InsertPageData(acionamento4);


            var pagina1 = new Page(1, new DescriptionPage("SS-CIC-1", "1"), pageData1);
            var pagina2 = new Page(1, new DescriptionPage("SS-CIC-2", "1"), pageData2);
            var pagina3 = new Page(1, new DescriptionPage("SS-CIC-3", "1"), pageData3);
            var pagina4 = new Page(1, new DescriptionPage("TV-1", "1"), pageData4);


            var project = new Project(new List<ProjectInfo>());
            project.SetPainel("CCM-1A");

            project.AddPage(pagina1);
            project.AddPage(pagina2);
            project.AddPage(pagina3);
            project.AddPage(pagina4);


            var service = new AnilhaSoftStarterService();

            service.Execute(project);


            var shapes = project.Paginas.SelectMany(p => p.Shapes).Where(shape => shape.Name == "r_1_softstarter").ToList();

            shapes.Should().HaveCount(3);

            shapes[0].Text.Should().Be("1A-R.1.SS1");
            shapes[1].Text.Should().Be("1A-R.1.SS2");
            shapes[2].Text.Should().Be("1A-R.1.SS3");

        }
    }
}
