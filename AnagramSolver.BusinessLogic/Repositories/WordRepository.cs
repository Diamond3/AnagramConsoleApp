using System.Data;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using Microsoft.Data.SqlClient;

namespace AnagramSolver.BusinessLogic.Repositories;

public class WordRepository: IWordRepository
{
    private const string Connection = "data source=LT-LIT-SC-0597;initial catalog=AnagramDB;trusted_connection=true;TrustServerCertificate=true;MultipleActiveResultSets=True";
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
            var newWordModel = new WordModel()
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

    public List<WordModel> GetAllWordsByWordPart(string? wordPart)
    {
        var wordModelList = new List<WordModel>();
        
        using var connection = new SqlConnection(Connection);
        connection.Open();
        
        var command = connection.CreateCommand();
        command.CommandText = "SELECT WordId, FirstForm, Form, SecondForm FROM Word WHERE SecondForm LIKE @wordPart";
        command.Parameters.AddWithValue("@wordPart","%" + wordPart + "%");
        
        var reader = command.ExecuteReader();
        while (reader.Read())
        {
            var newWordModel = new WordModel()
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
        
        var query = "INSERT INTO Word (FirstForm, Form, SecondForm)" +
                                "VALUES (@FirstForm, @Form, @SecondForm)";
        
        foreach (var model in models)
        {
            var command = new SqlCommand(query, connection);
            command.Parameters.Add("@FirstForm", SqlDbType.VarChar, 255).Value = model.FirstForm;
            command.Parameters.Add("@Form", SqlDbType.VarChar, 255).Value = model.Form;
            command.Parameters.Add("@SecondForm", SqlDbType.VarChar, 255).Value = model.SecondForm;
            command.ExecuteNonQuery();
        }
    }
}