using Application.DTO;
using Application.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Builders;

namespace Tests
{
    public class CreateProjectTest
    {
        public void Sucess()
        {
            var RepositoryBuilder =  new ExcelRepositoryBuilder();
            var CorelDraw = CorelDrawBuilder.Build();

            var Repository = RepositoryBuilder.Build();

            var UseCase = new CreateProject(Repository, CorelDraw);

            var Input = new InputCreateProject("CCM-1A");

            UseCase.Execute(Input);

            
        }
    }
}
