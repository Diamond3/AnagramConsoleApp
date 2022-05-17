using AnagramSolver.Contracts.Interfaces;

namespace AnagramSolver.BusinessLogic.Services;

public class UserService : IUserService
{
    private const int WordCount = 3;
    private readonly Dictionary<string, int> _users = new();

    public void AddUser(string ip)
    {
        _users[ip] = WordCount;
    }

    public void DecreaseCount(string ip)
    {
        if (!_users.ContainsKey(ip))
            AddUser(ip);
        _users[ip]--;
    }

    public void IncreaseCount(string ip)
    {
        if (!_users.ContainsKey(ip))
            AddUser(ip);
        _users[ip]++;
    }

    public bool AbleToDoAction(string ip)
    {
        if (!_users.ContainsKey(ip))
            AddUser(ip);
        return _users[ip] > 0;
    }
}