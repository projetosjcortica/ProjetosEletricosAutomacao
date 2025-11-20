using Domain.Agreggates;
using Domain.Value_Objects.Partidas;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainTestes.ValueObjects
{
    public class PageTest
    {
        [Fact]
        public void Sucess_Case_Page_Is_Soft_Starter()
        {
            var acionamento = new Acionamento("SS-CIC", "ACT", "Ciclone", "16-DO", "1A-CT-1.1", "1A-ACT-1", "RL01", "30", "", "", "", 1);
            var pageData = new PageData();
            pageData.InsertPageData(acionamento);
            var pagina = new Page(1, new DescriptionPage("SS-CIC", "1"), pageData);

            pagina.IsSoftStarterPage().Should().BeTrue();
        }

        [Fact]
        public void Fail_Case_Page_Is_Soft_Starter()
        {
            var acionamento = new Acionamento("TV-1", "ACT", "Transporte Vibratório 1", "16-DO", "1A-CT-1.1", "1A-ACT-1", "RL01", "30", "", "", "", 1);
            var pageData = new PageData();
            pageData.InsertPageData(acionamento);
            var pagina = new Page(1, new DescriptionPage("TV-1", "1"), pageData);

            pagina.IsSoftStarterPage().Should().BeFalse();
        }

        [Fact]
        public void Sucess_Case_Page_Is_Elevador()
        {
            var acionamento = new Acionamento("EL-1", "ACT", "Elevador 1", "16-DO", "1A-CT-1.1", "1A-ACT-1", "RL01", "30", "", "", "", 1);
            var pageData = new PageData();
            pageData.InsertPageData(acionamento);
            var pagina = new Page(1, new DescriptionPage("EL-1", "1"), pageData);

            pagina.IsElevadorPage().Should().BeTrue();
        }

        [Fact]
        public void Fail_Case_Page_Is_Elevador()
        {
            var acionamento = new Acionamento("FR-EL-1", "ACT", "Elevador 1", "16-DO", "1A-CT-1.1", "1A-ACT-1", "RL01", "30", "", "", "", 1);
            var pageData = new PageData();
            pageData.InsertPageData(acionamento);
            var pagina = new Page(1, new DescriptionPage("FR-EL-1", "1"), pageData);

            pagina.IsElevadorPage().Should().BeFalse();
        }

        [Fact]
        public void Sucess_Case_Page_Is_Freio_Elevador()
        {
            var acionamento = new Acionamento("K-FR-EL-1", "ACT", "Elevador 1", "16-DO", "1A-CT-1.1", "1A-ACT-1", "RL01", "30", "", "", "", 1);
            var pageData = new PageData();
            pageData.InsertPageData(acionamento);
            var pagina = new Page(1, new DescriptionPage("K-FR-EL-1", "1"), pageData);

            pagina.IsFreioElevadorPage().Should().BeTrue();
        }


        [Fact]
        public void Fail_Case_Page_Is_Freio_Elevador()
        {
            var acionamento = new Acionamento("EL-1", "ACT", "Elevador 1", "16-DO", "1A-CT-1.1", "1A-ACT-1", "RL01", "30", "", "", "", 1);
            var pageData = new PageData();
            pageData.InsertPageData(acionamento);
            var pagina = new Page(1, new DescriptionPage("EL-1", "1"), pageData);

            pagina.IsFreioElevadorPage().Should().BeFalse();
        }
    }
}
