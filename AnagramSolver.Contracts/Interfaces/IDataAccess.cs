namespace AnagramSolver.Contracts.Interfaces;

public interface IDataAccess<T>
{
    T ReadFile(string filePath);
    void AddWordToFile(string filePath, string word);
}