
using Domain.Value_Objects.Common;

namespace Domain.Value_Objects.Partidas
{
    public class ProjectInfo : IDataPage
    {
        public ProjectInfo(string nome, string value)
        {
            Nome = nome;
            Value = value;
        }

        public string Nome { get; set; }
        public string Value { get; set; }
        public Dictionary<string, string> GetData()
        {
            var Result = new Dictionary<string, string>();

            Result.Add($"Nome", Value);

            return Result;
        }

        public Descricao.Descricao GetDescricao()
        {
            throw new NotImplementedException();
        }

        public Nomenclatura GetNomenclatura()
        {
            throw new NotImplementedException();
        }
    }

}
