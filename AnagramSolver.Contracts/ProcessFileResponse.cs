namespace AnagramSolver.Contracts;

public class ProcessFileResponse
{
    public byte[] Bytes { get; set; }
    public string Type { get; set; }
    public string Name { get; set; }
}