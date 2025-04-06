using EventsExample.Models;

namespace TestProject1.EventServiceTests;

public class EventTests
{
    [Fact]
    public void WillHappen_ShouldReturnFalseWhenNotEnoughParticipants()
    {
        // Arrange
        var user = new User("organizer");
        var ev = new Event(user, "Test Event", DateTime.Now.AddDays(7), 3);
            
        // Act & Assert
        Assert.False(ev.WillHappen());
    }

    [Fact]
    public void WillHappen_ShouldReturnTrueWhenEnoughParticipants()
    {
        // Arrange
        var user = new User("organizer");
        var participant1 = new User("participant1");
        var participant2 = new User("participant2");
        var participant3 = new User("participant3");
            
        var ev = new Event(user, "Test Event", DateTime.Now.AddDays(7), 3);
            
        // Act
        ev.AddParticipant(participant1);
        ev.AddParticipant(participant2);
        ev.AddParticipant(participant3);
            
        // Assert
        Assert.True(ev.WillHappen());
    }

    [Fact]
    public void DaysAgoCreated_ShouldCalculateCorrectly()
    {
        // Arrange
        var user = new User("organizer");
        var testDate = DateTime.Now.AddDays(1);
        var ev = new Event(user, "Test Event", testDate, 3);
            
        // Act
        var daysAgo = ev.DaysAgoCreated();
            
        // Assert
        Assert.Equal(0, daysAgo);
    }
    
    [Fact]
    public void DaysAgoCreatedFrom_ShouldCalculateCorrectly()
    {
        // Arrange
        var user = new User("organizer");
        var testDate = DateTime.Now.AddDays(7);
        var ev = new Event(user, "Test Event", testDate, 3);
        var daysFromCreation = ev.CreatedAt.AddDays(7);
            
        // Act
        var daysAgo = ev.DaysAgoCreatedFrom(daysFromCreation);
            
        // Assert
        Assert.Equal(7, daysAgo);
    }
}