using System.Data;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using Microsoft.Data.SqlClient;

namespace AnagramSolver.BusinessLogic.Repositories;

public class WordRepository : IWordRepository
{
    private const int Size = 255;

    private const string Connection =
        "data source=LT-LIT-SC-0597;initial catalog=AnagramDB;trusted_connection=true;TrustServerCertificate=true;MultipleActiveResultSets=True";

    public List<WordModel> GetWords()
    {
        var wordModelList = new List<WordModel>();

        using var connection = new SqlConnection(Connection);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT WordId, FirstForm, Form, SecondForm FROM Word";

        var reader = command.ExecuteReader();
        while (reader.Read())
        {
            var newWordModel = new WordModel
            {
                WordId = (int)reader["WordId"],
                FirstForm = (string)reader["FirstForm"],
                SecondForm = (string)reader["SecondForm"],
                Form = (string)reader["Form"]
            };
            wordModelList.Add(newWordModel);
        }

        return wordModelList;
    }

    public void AddWord(string word)
    {
        throw new NotImplementedException();
    }

    public List<WordModel> GetAnagramsFromCachedWord(string? word)
    {
        var wordModelList = new List<WordModel>();
        if (string.IsNullOrEmpty(word)) return wordModelList;

        using var connection = new SqlConnection(Connection);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT Anagram FROM CachedWord WHERE LOWER(Word) = LOWER(@Word)";
        //command.Parameters.AddWithValue("@sortedWord", word);
        command.Parameters.AddWithValue("@Word", word);

        var reader = command.ExecuteReader();
        while (reader.Read())
        {
            var newWordModel = new WordModel
            {
                FirstForm = word,
                SecondForm = (string)reader["Anagram"]
            };
            wordModelList.Add(newWordModel);
        }

        return wordModelList;
    }

    public List<WordModel> GetAllWordsBySortedForm(string? sortedWord, string originalWord)
    {
        if (string.IsNullOrEmpty(sortedWord)) return new List<WordModel>();


        var wordModelList = new List<WordModel>();

        using var connection = new SqlConnection(Connection);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText =
            "SELECT DISTINCT SecondForm FROM Word WHERE SortedForm = @sortedWord AND LOWER(SecondForm) <> LOWER(@word)";
        command.Parameters.AddWithValue("@sortedWord", sortedWord);
        command.Parameters.AddWithValue("@word", originalWord);

        var reader = command.ExecuteReader();
        while (reader.Read())
        {
            var newWordModel = new WordModel
            {
                SecondForm = (string)reader["SecondForm"]
            };
            wordModelList.Add(newWordModel);
        }

        return wordModelList;
    }

    public List<WordModel> GetAllWordsByWordPart(string? wordPart)
    {
        var wordModelList = new List<WordModel>();

        using var connection = new SqlConnection(Connection);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT WordId, FirstForm, Form, SecondForm FROM Word WHERE SecondForm LIKE @wordPart";
        command.Parameters.AddWithValue("@wordPart", "%" + wordPart + "%");

        var reader = command.ExecuteReader();
        while (reader.Read())
        {
            var newWordModel = new WordModel
            {
                WordId = (int)reader["WordId"],
                FirstForm = (string)reader["FirstForm"],
                SecondForm = (string)reader["SecondForm"],
                Form = (string)reader["Form"]
            };
            wordModelList.Add(newWordModel);
        }

        return wordModelList;
    }

    public void AddAllWordModels(List<WordModel> models)
    {
        using var connection = new SqlConnection(Connection);
        connection.Open();

        var query = "INSERT INTO Word (FirstForm, Form, SecondForm, SortedForm)" +
                    "VALUES (@FirstForm, @Form, @SecondForm, @SortedForm)";

        var command = new SqlCommand(query, connection);
        command.Parameters.Add("@FirstForm", SqlDbType.VarChar, Size);
        command.Parameters.Add("@Form", SqlDbType.VarChar, Size);
        command.Parameters.Add("@SecondForm", SqlDbType.VarChar, Size);
        command.Parameters.Add("@SortedForm", SqlDbType.VarChar, Size);

        foreach (var model in models)
        {
            command.Parameters["@FirstForm"].Value = model.FirstForm;
            command.Parameters["@Form"].Value = model.Form;
            command.Parameters["@SecondForm"].Value = model.SecondForm;

            var sortedArray = model.SecondForm.ToLower().ToArray();
            Array.Sort(sortedArray);

            command.Parameters["@SortedForm"].Value = new string(sortedArray);
            command.ExecuteNonQuery();
        }
    }

    public void InsertAnagramsCachedWord(string? word, List<WordModel> models)
    {
        using var connection = new SqlConnection(Connection);
        connection.Open();

        var query = "INSERT INTO CachedWord (Word, Anagram)" +
                    "VALUES (@Word, @Anagram)";

        if (models.Count == 0)
            models.Add(new WordModel
            {
                SecondForm = "No anagrams"
            });

        foreach (var model in models)
        {
            var command = new SqlCommand(query, connection);
            command.Parameters.Add("@Word", SqlDbType.VarChar, 255).Value = word;
            command.Parameters.Add("@Anagram", SqlDbType.VarChar, 255).Value = model.SecondForm;
            command.ExecuteNonQuery();
        }
    }

    public void ClearCachedWord()
    {
        using var connection = new SqlConnection(Connection);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "EXEC dbo.clearCachedWord";
        command.ExecuteNonQuery();
    }
}