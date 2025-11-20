using System.Text.RegularExpressions;

namespace Domain.Value_Objects;

public class Cabeamento
{
    public string Value { get; }

    public Cabeamento(string value, string CV = "1")
    {    
        if (string.IsNullOrWhiteSpace(value))
        {
            if (!double.TryParse(CV, out var CVAsNumber))
            {
                CVAsNumber = 1;
            }
            Value = NormalizeCable(GetCableBYCV(CVAsNumber));
            return;
        }
        Value = NormalizeCable(value);
    }

    private string NormalizeCable(string value)
    {
        var regexPattern = @"PP";
        return Regex.Replace(value, regexPattern, "PP\r\n");
    }
    private string GetCableBYCV(double CV)
    {
        var cabosPorCavalos = new Dictionary<double, string>
        {
            { 0.33, "Cabo PP 4x2,5mm²" },
            { 0.5, "Cabo PP 4x2,5mm²" },
            { 0.75, "Cabo PP 4x2,5mm²" },
            { 1.0, "Cabo PP 4x2,5mm²" },
            { 1.5, "Cabo PP 4x2,5mm²" },
            { 2.0, "Cabo PP 4x2,5mm²" },
            { 3.0, "Cabo PP 4x2,5mm²" },
            { 4.0, "Cabo PP 4x2,5mm²" },
            { 5.0, "Cabo PP 4x2,5mm²" },
            { 6.0, "Cabo PP 4x2,5mm²" },
            { 7.5, "Cabo PP 4x2,5mm²" },
            { 10.0, "Cabo PP 4x2,5mm²" },
            { 12.5, "Cabo PP 4x4mm²" },
            { 15.0, "Cabo PP 4x4mm²" },
            { 20.0, "Cabo PP 4x6mm²" },
            { 25.0, "Cabo PP 4x10mm²" },
            { 30.0, "Cabo PP 4x16mm²" },
            { 40.0, "Cabo PP 4x25mm²" },
            { 50.0, "Cabo PP 4x25mm²" },
            { 60.0, "Cabo PP 4x35mm²" }
        };

        return cabosPorCavalos[CV];
    }
}