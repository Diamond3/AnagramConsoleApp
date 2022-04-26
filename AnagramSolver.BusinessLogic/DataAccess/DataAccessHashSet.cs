using System.Text;
using AnagramSolver.Contracts;
using AnagramSolver.Contracts.Interfaces;

namespace AnagramSolver.BusinessLogic.DataAccess;

public class DataAccessHashSet: IDataAccess<HashSet<string>>
{
    private readonly string _fileName = "zodynas.txt";
    
    public HashSet<string> ReadFile()
    {
        var hs = new HashSet<string>();
        try
        {
            string line;
            
            var sr = new StreamReader(_fileName, Encoding.UTF8);
            while ((line = sr.ReadLine()) != null)
            {
                var words = line.Split('\t');
                
                hs.Add(words[2].ToLower());
                hs.Add(words[0].ToLower());
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
        }
        return hs;
    }
}