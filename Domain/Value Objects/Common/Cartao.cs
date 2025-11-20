using System.Text.RegularExpressions;

namespace Domain.Value_Objects;

public class Cartao
{
    public string Value { get; }

    public Cartao(string value)
    {
        Value = NormalizeCartao(value);
    }

    private string NormalizeCartao(string value)
    {
        return Regex.Replace(value, @"-?PCNT", "\r\nPCNT");
    }

    public bool IsDigital()
    {
        return !IsAnalogic();
    }

    public bool IsAnalogic()
    {
        return Value.Contains("AO") || Value.Contains("AIO");
    }

    public bool Is8AO()
    {
        return Value.Contains("AO");
    }

    public bool Is8AIO()
    {
        return Value.Contains("AIO");
    }
}