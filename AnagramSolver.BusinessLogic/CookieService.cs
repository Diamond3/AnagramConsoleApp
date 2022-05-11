using AnagramSolver.Contracts.Interfaces;

namespace AnagramSolver.BusinessLogic;

public class CookieService : ICookieService
{
    public const string CountKey = "Count";
    public const string ValueKey = "Value_";

    public bool CanAddValue(string? value, List<string> allValues)
    {
        if (string.IsNullOrEmpty(value)) return false;
        return !allValues.Contains(value);
    }

    public List<string> GetAllValues()
    {
        return new List<string>();
    }

    public int GetCount(string? str)
    {
        if (string.IsNullOrEmpty(str)) return 0;

        if (int.TryParse(str, out var number)) return number;
        return 0;
    }
}