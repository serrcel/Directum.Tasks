using EventsExample.Models;
using EventsExample.Services;

namespace TestProject1.EventServiceTests;

public class EventServiceTests
    {
        [Fact]
        public void RegisterParticipant_ShouldAddUserToEvent()
        {
            // Arrange
            var service = new EventService();
            var user = new User("testUser");
            var participant = new User("participant");
            var ev = service.CreateEvent(user, "Test Event", DateTime.Now.AddDays(7), 5);
            
            // Act
            service.RegisterParticipant(ev, participant);
            
            // Assert
            Assert.True(ev.IsParticipant(participant));
            Assert.Equal(1, ev.ParticipantsCount);
        }

        [Fact]
        public void IsUserParticipantInAnyEvent_ShouldReturnTrueForParticipant()
        {
            // Arrange
            var service = new EventService();
            var user = new User("testUser");
            var participant = new User("participant");
            var ev = service.CreateEvent(user, "Test Event", DateTime.Now.AddDays(7), 5);
            service.RegisterParticipant(ev, participant);
            
            // Act & Assert
            Assert.True(service.IsUserParticipant(participant));
        }

        [Fact]
        public void IsUserParticipantInAnyEvent_ShouldReturnFalseForNonParticipant()
        {
            // Arrange
            var service = new EventService();
            var user = new User("testUser");
            var nonParticipant = new User("nonParticipant");
            service.CreateEvent(user, "Test Event", DateTime.Now.AddDays(7), 5);
            
            // Act & Assert
            Assert.False(service.IsUserParticipant(nonParticipant));
        }
    }