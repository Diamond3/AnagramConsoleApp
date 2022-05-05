using System.Diagnostics;
using AnagramSolver.Contracts;
using AnagramSolver.Contracts.Interfaces;

namespace AnagramSolver.BusinessLogic.DataAccess;

public class FileService: IFileService
{
    public bool ProcessFile(ProcessFileResponse response, string name)
    {
        if (string.IsNullOrEmpty(name)) return false;
        
        var filePath = $"{name}.txt";
        if (!File.Exists(filePath)) return false;
            
        response.Bytes = File.ReadAllBytes(filePath);
        response.Name = Path.GetFileName(filePath);
        response.Type = "text/plain";
        return true;
    }
}