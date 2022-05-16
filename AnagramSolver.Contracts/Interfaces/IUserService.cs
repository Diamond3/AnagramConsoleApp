namespace AnagramSolver.Contracts.Interfaces;

public interface IUserService
{ 
    void AddUser(string ip);
    void DecreaseCount(string ip);
    void IncreaseCount(string ip);
    bool AbleToDoAction(string ip);
}