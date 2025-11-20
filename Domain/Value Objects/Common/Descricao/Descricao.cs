using Domain.Services;
using Domain.Value_Objects.Descricao.Handles;

namespace Domain.Value_Objects.Descricao;

public class Descricao
{
    public string Value;

    public Descricao(string value)
    {
        var hanlder = FactoryDescricaoHandler.Create();
        Value = hanlder.Handle(CapitalizeString.Execute(value));
    }

    public string GetValue()
    {
        var hanlder = FactoryDescricaoHandler.Create();
        return hanlder.Handle(Value);
    }
}