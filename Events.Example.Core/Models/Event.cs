using EventsExample.Interfaces;
using EventsExample.Models.Abstractions;

namespace EventsExample.Models;

public class Event : BaseEntity, ITimeTrackable
{
    private List<Guid> _eventPostsIds;
    private List<Guid> _participantsIds;
    public DateTime CreatedAt { get; }
    public DateTime EventDate { get; }
    public string Description { get; }
    
    public int ParticipantsCount => _participantsIds.Count;
    public int RequiredParticipants { get;}
    public int DaysAgoCreated => DaysAgoCreatedFrom(DateTime.Now);
    

    public Event(User author, string description, DateTime eventDate, int requiredParticipants, DateTime? createdAt = null)
    {
        CreatedAt = createdAt ?? DateTime.Now;
        Author = author;
        Description = description;
        EventDate = eventDate;
        RequiredParticipants = requiredParticipants;
        _participantsIds = [];
        _eventPostsIds = [];
    }

    public Guid AddParticipant(Guid userId)
    {
        if (!IsParticipant(userId))
        {
            _participantsIds.Add(userId);
        }

        return _participantsIds.Last();
    }

    public bool RemoveParticipant(Guid userId)
    {
        return _participantsIds.Remove(userId);
    }

    public bool IsParticipant(Guid userId)
    {
        return _participantsIds.Contains(userId);
    }

    public bool WillHappen()
    {
        return ParticipantsCount >= RequiredParticipants;
    }
    
    public int DaysAgoCreatedFrom(DateTime fromDate)
    {
        return (fromDate - CreatedAt).Days;
    }
}