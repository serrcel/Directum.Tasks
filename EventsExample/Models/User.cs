using EventsExample.Models.Abstractions;

namespace EventsExample.Models;

public class User: BaseEntity
{
    public string Username { get; set; }
        
    public User(string username)
    {
        Username = username;
    }

    public override string ToString()
    {
        return Username;
    }
}