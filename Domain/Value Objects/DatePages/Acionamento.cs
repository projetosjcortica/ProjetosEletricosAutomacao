using Domain.Value_Objects.Common;

namespace Domain.Value_Objects.Partidas
{
    public class Acionamento : IDataPage
    {
        public Nomenclatura Nomenclatura { get; private set; }
        public string Tipo { get; private set; }
        public Descricao.Descricao Descricao { get; private set; }
        public Cartao Cartao { get; private set; }
        public Anilha Anilha { get; private set; }
        public string AnilhaRele { get; private set; }
        public string Rele { get; private set; }
        public Cavalo Cavalo { get; private set; }
        public Borne Borne { get; private set; }
        public Cabeamento Cabeamento { get; private set; }
        public string Fusivel { get; private set; }
        public int Index {  get; private set; }

        public Acionamento(string nomenclatura, string tipo, string descricao, string cartao, string anilhaCartao, string anilhaRele, string rele, string cavalo, string borne, string cabeamento, string fusivel, int index)
        {
            Nomenclatura = new Nomenclatura(nomenclatura);
            Tipo = tipo;
            Descricao = new Descricao.Descricao(descricao);
            Cartao = new Cartao(cartao);
            Anilha = new Anilha(anilhaCartao);
            AnilhaRele = anilhaRele;
            Rele = rele;
            Cavalo = new Cavalo(cavalo);
            Borne = new Borne(borne);
            Cabeamento = new Cabeamento(cabeamento, cavalo);
            Fusivel = fusivel;
            Index = index;
            Anilha.SetCartao(Cartao);
        }

        public Dictionary<string, string> GetData()
        {
            var contator = new Contator(Nomenclatura.Value);
            var Result = new Dictionary<string, string>
            {
                { $"nomenclatura_{Index}", Nomenclatura.Value },
                { $"disjuntor_{Index}", GetDisjuntor()},
                { $"descricao_{Index}", Descricao.GetValue() },
                { $"contator_{Index}",  contator.Value},
                { $"texto_cartao_acionamento_{Index}", $"XN-322-{Cartao.Value} \r\n Cartão {Anilha.GetNumeroCartao()} - X{Anilha.GetSecaoCartao()}" },
                { $"rele_{Index}", Rele },
                { $"anilha_cartao_acionamento_{Index}", Anilha.Value },
                { $"anilha_rele_acionamento_{Index}", AnilhaRele },
                { $"anilha_rele_acionamento_{Index}_1", $"{AnilhaRele}.1" },
                { $"numero_saida_cartao_acionamento_{Index}", Anilha.GetNumeroSaidaCartao() },
                { $"borne_acionamento_{Index}", Borne.Value },
                { $"cavalo_{Index}", Cavalo.Value },
                { $"cabeamento_motor_{Index}", Cabeamento.Value },
                { $"fusivel_acionamento_{Index}", Fusivel },
                { $"painel_anilha_{Index}",  Anilha.GetPainel()}
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

        public string GetDisjuntor()
        {
            if(Nomenclatura.Value.Contains("CAR"))
            {
                return $"DM-{Nomenclatura.Value.Replace("CAR-", "CAR\r\n")}";
            }
            return $"DM-{ Nomenclatura.Value}";
        }
    }
}
