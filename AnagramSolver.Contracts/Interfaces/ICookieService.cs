namespace AnagramSolver.Contracts.Interfaces;

public interface ICookieService
{
    bool CanAddValue(string? value, List<string> allValues);
    List<string> GetAllValues();
    int GetCount(string? str);
}