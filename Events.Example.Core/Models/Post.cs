using EventsExample.Interfaces;
using EventsExample.Models.Abstractions;

namespace EventsExample.Models;

public class Post : BaseEntity, IContent, ITimeTrackable
{
    public DateTime CreatedAt { get; protected set; }
    public string Content { get; set; }
    public string Preview => Content.Length > 50 ? Content.Substring(0, 50) + "..." : Content;
    public int DaysAgoCreated => DaysAgoCreatedFrom(DateTime.Now);
    public Guid EventId { get; set; }

    public Post(User author, string content, Guid eventId, DateTime? createdAt = null)
    {
        CreatedAt = createdAt ?? DateTime.Now;
        Author = author;
        Content = content;
        CreatedAt = createdAt ?? CreatedAt;
        EventId = eventId;
    }
    
    public int DaysAgoCreatedFrom(DateTime fromDate)
    {
        return (fromDate - CreatedAt).Days;
    }

}
