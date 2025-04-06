namespace EventsExample.Interfaces;

public interface ITimeTrackable
{
    DateTime CreatedAt { get; }
    int DaysAgoCreated { get; }
    int DaysAgoCreatedFrom(DateTime fromDate);
}