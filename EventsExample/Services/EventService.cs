using EventsExample.Models;

namespace EventsExample.Services;

public class EventService
{
    private List<Event> _events = new List<Event>();
    
    public bool IsUserParticipant(Guid userId, Guid eventId)
    {
        var evt = _events.FirstOrDefault(evt => evt.Id == eventId);

        return evt != null && evt.IsParticipant(userId);
    }

    public Event CreateEvent(User author, string description, DateTime eventDate, int requiredParticipants, DateTime? extraCreationDate = null)
    {
        var ev = new Event(author, description, eventDate, requiredParticipants, extraCreationDate);
        _events.Add(ev);
        return ev;
    }

    public void RegisterParticipant(Guid eventId, Guid userId)
    {
        var evt = _events.FirstOrDefault(evt => evt.Id == eventId);
        if (evt is null)
        {
            throw new ArgumentException("Нет такого события");
        }
        evt.AddParticipant(userId);
    }

    public void DisplayEvents()
    {
        foreach (var ev in _events)
        {
            Console.WriteLine($"Мероприятие '{ev.Description}' от {ev.Author.Username}");
            Console.WriteLine($"Заплаинровано: {ev.EventDate.ToShortDateString()}");
            Console.WriteLine($"Участинки: {ev.ParticipantsCount}/{ev.RequiredParticipants}");
            Console.WriteLine("Состоится ли: {0}", ev.WillHappen() ? "Да" : "Нет");
            Console.WriteLine($"Создано {ev.DaysAgoCreated} дней газад");
            Console.WriteLine();
        }
    }
}