namespace AnagramSolver.Contracts.Interfaces;

public interface IFileService
{
    bool ProcessFile(ProcessFileResponse response, string name);
}