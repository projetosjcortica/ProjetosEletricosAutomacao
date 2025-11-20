using Domain.Value_Objects.Common;
using Domain.Value_Objects.Descricao;

namespace Domain
{
    public interface IDataPage
    {
        Dictionary<string, string> GetData(); 

        public Descricao GetDescricao();
        public Nomenclatura GetNomenclatura();
    }
}
