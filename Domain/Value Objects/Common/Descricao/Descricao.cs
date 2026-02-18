using Domain.Services;
using Domain.Value_Objects.Descricao.Handles;
using System.Text.RegularExpressions;

namespace Domain.Value_Objects.Descricao;

public class Descricao
{
    public string Value;

    public Descricao(string value)
    {
        var hanlder = FactoryDescricaoHandler.Create();

        // 1. Remove $ e cria quebra de linha
        var normalized = Regex.Replace(value, @"\s*\$\s*", "\r\n");

        // 2. Capitaliza CADA linha separadamente
        var linhas = normalized
            .Split(new[] { "\r\n" }, StringSplitOptions.None)
            .Select(l => CapitalizeString.Execute(l))
            .ToArray();

        var resultValue = string.Join("\r\n", linhas);

        // 3. Aplica handlers finais
        Value = hanlder.Handle(resultValue);
    }

    public string GetValue()
    {
        var hanlder = FactoryDescricaoHandler.Create();
        return hanlder.Handle(Value);
    }
}