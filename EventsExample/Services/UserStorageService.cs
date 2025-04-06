using EventsExample.Models;

namespace EventsExample.Services;

public class UserStorageService
{
    private List<User> _users;

    public UserStorageService()
    {
        _users = new List<User>();
    }

    public User CreateUser(string username)
    {
        var user = new User(username);
        _users.Add(user);
        
        return user;
    }

    public bool DeleteUser(Guid userId)
    {
        var user = _users.FirstOrDefault(u => u.Id == userId);
        
        return user is not null && _users.Remove(user);
    }

    public User? GetUser(Guid userId)
    {
        return _users.FirstOrDefault(user => user.Id == userId);
    }
}