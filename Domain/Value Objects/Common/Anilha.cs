using System;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Domain.Value_Objects;

public class Anilha
{
    public string Value { get; }
    public Cartao Cartao { get; set; }

    public Anilha(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            Value = string.Empty;
            return;
        }
        var result = value.ToUpper();
        Value = result;
    }
    public Anilha SetCartao(Cartao cartao)
    {
        Cartao = cartao;
        return this;
    }

    public string GetPainel()
    {
        var indexPanel = Value.Split('-')[0];
        return $"CCM-{indexPanel}";
    }

    public static Anilha CreateAnilhaFonteAtuadorPositiva(string painel, int index)
    {
        var anilha = $"1A-24V-FDC{index}";
        var regexPattern = @"CCM-(\d[A-Z])";

        var panel = Regex.Replace(painel, regexPattern, "$1");

        var result = anilha.Replace("1A", panel);

        return new Anilha(result);
    }

    public static Anilha CreateAnilhaFonteAtuadorNegativa(string painel, int index)
    {
        var anilha = $"1A-0V-FDC{index}";
        var regexPattern = @"CCM-(\d[A-Z])";

        var panel = Regex.Replace(painel, regexPattern, "$1");

        var result = anilha.Replace("1A", panel);

        return new Anilha(result);
    }

    public static Anilha CreateAnilhaSoftStarter(string painel,int indexSoftStarter)
    {
        var anilha = "R.SS.1";
        var regexPattern = @"SS.(\d+)";

        var regexPatternPainel = @"CCM-(\d[A-Z])";

        var panel = Regex.Replace(painel, regexPatternPainel, "$1");

        var result = Regex.Replace(anilha, regexPattern, match =>
        {
            return "SS." + indexSoftStarter;
        });

        return new Anilha($"{panel}-{result}");
    }

    public static Anilha CreateAnilhaCInversor(string painel, int index)
    {
        var anilha = $"1A-INV{index}.C";
        var regexPattern = @"CCM-(\d[A-Z])";

        var panel = Regex.Replace(painel, regexPattern, "$1");

        var result = anilha.Replace("1A", panel);

        return new Anilha(result);
    }

    public static Anilha CreateAnilhaRInversor(string painel, int index)
    {
        var anilha = $"1A-R\r\nINV{index}";
        var regexPattern = @"CCM-(\d[A-Z])";

        var panel = Regex.Replace(painel, regexPattern, "$1");

        var result = anilha.Replace("1A", panel);

        return new Anilha(result);
    }


    public static Anilha CreateAnilhaSInversor(string painel, int index)
    {
        var anilha = $"1A-S\r\nINV{index}";
        var regexPattern = @"CCM-(\d[A-Z])";

        var panel = Regex.Replace(painel, regexPattern, "$1");

        var result = anilha.Replace("1A", panel);

        return new Anilha(result);
    }

    public static Anilha CreateAnilhaTInversor(string painel, int index)
    {
        var anilha = $"1A-T\r\nINV{index}";
        var regexPattern = @"CCM-(\d[A-Z])";

        var panel = Regex.Replace(painel, regexPattern, "$1");

        var result = anilha.Replace("1A", panel);

        return new Anilha(result);
    }

    public static Anilha CreateAnilhaUInversor(string painel, int index)
    {
        var anilha = $"1A-U\r\nINV{index}";
        var regexPattern = @"CCM-(\d[A-Z])";

        var panel = Regex.Replace(painel, regexPattern, "$1");

        var result = anilha.Replace("1A", panel);

        return new Anilha(result);
    }

    public static Anilha CreateAnilhaVInversor(string painel, int index)
    {
        var anilha = $"1A-V\r\nINV{index}";
        var regexPattern = @"CCM-(\d[A-Z])";

        var panel = Regex.Replace(painel, regexPattern, "$1");

        var result = anilha.Replace("1A", panel);

        return new Anilha(result);
    }
    public static Anilha CreateAnilhaWInversor(string painel, int index)
    {
        var anilha = $"1A-W\r\nINV{index}";
        var regexPattern = @"CCM-(\d[A-Z])";

        var panel = Regex.Replace(painel, regexPattern, "$1");

        var result = anilha.Replace("1A", panel);

        return new Anilha(result);
    }

    public static Anilha CreateAnilhaUInversorSemQuebra(string painel, int index)
    {
        var anilha = $"1A-U-INV{index}";
        var regexPattern = @"CCM-(\d[A-Z])";

        var panel = Regex.Replace(painel, regexPattern, "$1");

        var result = anilha.Replace("1A", panel);

        return new Anilha(result);
    }

    public static Anilha CreateAnilhaVInversorSemQuebra(string painel, int index)
    {
        var anilha = $"1A-V-INV{index}";
        var regexPattern = @"CCM-(\d[A-Z])";

        var panel = Regex.Replace(painel, regexPattern, "$1");

        var result = anilha.Replace("1A", panel);

        return new Anilha(result);
    }
    public static Anilha CreateAnilhaWInversorSemQuebra(string painel, int index)
    {
        var anilha = $"1A-W-INV{index}";
        var regexPattern = @"CCM-(\d[A-Z])";

        var panel = Regex.Replace(painel, regexPattern, "$1");

        var result = anilha.Replace("1A", panel);

        return new Anilha(result);
    }


    public static Anilha CreateAnilhaReversao(string nomenclatura)
    {
        var result = $"L2-{nomenclatura.Replace("FR-","")}";

        return new Anilha(result);
    }

    public string GetNumeroCartao()
    {
        var splitedValueForHyphen = Value.Split('-');
        var lastPart = splitedValueForHyphen[splitedValueForHyphen.Length - 1];
        if (!lastPart.Contains('.')) return string.Empty;
        var splitedLastPartForDot = lastPart.Split('.');
        var result = splitedLastPartForDot[0];
        return IsNumber(result) ? result : string.Empty;
    }

    private string GetNumeroSaidaCartaoSemFormatacao()
    {
        var splitedValueForHyphen = Value.Split('-');
        var lastPart = splitedValueForHyphen[splitedValueForHyphen.Length - 1];
        if (!lastPart.Contains('.')) return string.Empty;
        var splitedLastPartForDot = lastPart.Split('.');
        var numberOutCartao = splitedLastPartForDot[splitedLastPartForDot.Length - 1];
        if (!IsNumber(numberOutCartao)) return string.Empty;
        return numberOutCartao.ToString();
    }

    public string GetNumeroSaidaCartao()
    {
        var splitedValueForHyphen = Value.Split('-');
        var lastPart = splitedValueForHyphen[splitedValueForHyphen.Length - 1];
        if (!lastPart.Contains('.')) return string.Empty;
        var splitedLastPartForDot = lastPart.Split('.');
        var numberOutCartao = splitedLastPartForDot[splitedLastPartForDot.Length - 1];
        if (!IsNumber(numberOutCartao)) return string.Empty;


        if (Cartao.IsDigital())
        {
            var resultIsLastThanTen = int.Parse(numberOutCartao) < 10;
            if (resultIsLastThanTen) return "0" + numberOutCartao;
            return numberOutCartao;
        }
        
        if(Cartao.Is8AO())
        {
            int numeroCartao = int.Parse(numberOutCartao);
            return numeroCartao % 2 == 0 ? "-" : Math.Ceiling((double)numeroCartao/ 2) + "+";
        } 

        if(Cartao.Is8AIO())
        {
            switch(numberOutCartao)
            {
                case "1": return "1+";
                case "2": return "1-";
                case "3": return "2+";
                case "4": return "2-";
                case "5": return "3+";
                case "6": return "3-";
                case "7": return "4+";
                case "8": return "4-";
                case "9": return "R";
                case "10": return "R";
                case "11": return "R";
                case "12": return "R";
                case "13": return "1+";
                case "14": return "-";
                case "15": return "2+";
                case "16": return "-";
                case "17": return "3+";
                case "18": return "-";
                case "19": return "4+";
                case "20": return "-";
                default: return string.Empty;
            }
        }

        return string.Empty;
    }

    public string GetSecaoCartao()
    {
        var result = GetNumeroSaidaCartaoSemFormatacao();
        if (string.IsNullOrEmpty(result)) return string.Empty;
        var match = Regex.Match(result, @"^\d+");
        if (!match.Success) return string.Empty;
        var numberCartao = int.Parse(match.Value);
        if (numberCartao is >= 1 and <= 4)
            return "1";
        if (numberCartao is >= 5 and <= 8)
            return "2";
        if (numberCartao is >= 9 and <= 12)
            return "3";
        if (numberCartao is >= 12 and <= 16)
            return "4";
        if (numberCartao is >= 17 and <= 20)
            return "5";
        return string.Empty;
    }

    private bool IsNumber(string value) => value.All(char.IsDigit);
}