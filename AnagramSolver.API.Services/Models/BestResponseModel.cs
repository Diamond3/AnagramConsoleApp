using Newtonsoft.Json;

namespace AnagramSolver.API.Services.Models;

public class BestResponseModel
{
    [JsonProperty("best")]
    public List<string> Words { get; set; }

    public BestResponseModel()
    {
        
    }
}