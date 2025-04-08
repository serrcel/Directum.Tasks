using EventsExample.Interfaces;
using EventsExample.Models.Abstractions;

namespace EventsExample.Models;

public class Comment : BaseEntity, ITimeTrackable
{
    public string Content { get; set; }
    public Post RelatedPost { get; set; }
    public DateTime CreatedAt { get; }
    public int DaysAgoCreated => DaysAgoCreatedFrom(DateTime.Now);

    public Comment(User author, Post post, string content, DateTime? createdAt = null)
    {
        CreatedAt = createdAt ?? DateTime.Now;
        Author = author;
        RelatedPost = post;
        Content = content;
    }
    
    public int DaysAgoCreatedFrom(DateTime fromDate)
    {
        return (fromDate - CreatedAt).Days;
    }
}