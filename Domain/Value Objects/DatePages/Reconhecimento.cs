using Domain.Services;
using Domain.Value_Objects.Common;
using Domain.Value_Objects.DatePages.StrategyReconhecimento;
using Domain.Value_Objects.Reconhecimento.ReconhecimentoDescricaoHandler;

namespace Domain.Value_Objects.Partidas
{
    public class Reconhecimento : IDataPage
    {
        public Nomenclatura Nomenclatura { get; private set; }
        public string Tipo { get; private set; }
        public Descricao.Descricao Descricao { get; private set; }
        public Cartao Cartao { get; private set; }
        public Anilha Anilha { get; private set; }
        public Borne Borne { get; private set; }
        public string Fusivel { get; private set; }
        public int Index {  get; private set; }

        public Reconhecimento(string nomenclatura, string tipo, string descricao, string cartao, string anilhaCartao, string borne, string fusivel, int index)
        {
            Nomenclatura = new Nomenclatura(nomenclatura);
            Tipo = tipo;
            Descricao = new Descricao.Descricao(descricao);
            Cartao = new Cartao(cartao);
            Anilha = new Anilha(anilhaCartao);
            Borne = new Borne(borne);
            Fusivel = fusivel;
            Index = index;
            Anilha.SetCartao(Cartao);
        }

        public Dictionary<string, string> GetData()
        {
            var Result = new Dictionary<string, string>
            {
                { $"texto_cartao_reconhecimento_{Index}", $"XN-322-{Cartao.Value} \r\n Cartão {Anilha.GetNumeroCartao()} - X{Anilha.GetSecaoCartao()}" },
                { $"anilha_cartao_reconhecimento_{Index}", Anilha.Value },
                { $"numero_saida_cartao_reconhecimento_{Index}", Anilha.GetNumeroSaidaCartao() },
                { $"descricao_reconhecimento_{Index}", Descricao.GetValue()},
                { $"borne_{Index}", Borne.Value },
                { $"fusivel_{Index}", Fusivel },
                { $"painel_anilha_reconhecimento_{Index}",  Anilha.GetPainel()}
            };
            return Result;
        }

        public Descricao.Descricao GetDescricao()
        {
            return Descricao;
        }

        public Nomenclatura GetNomenclatura()
        {
            return Nomenclatura;
        }
    }
}
