using EventsExample.Models;
using EventsExample.Services;

namespace TestProject1.EventServiceTests;

public class PostServiceTests
    {
        private readonly User _author;
        private readonly User _participant;
        private readonly User _nonParticipant;
        private readonly EventService _eventService;
        private readonly PostService _postService;

        public PostServiceTests()
        {
            _author = new User("author");
            _participant = new User("participant");
            _nonParticipant = new User("nonParticipant");
            
            _eventService = new EventService();
            var ev = _eventService.CreateEvent(_author, "Test Event", DateTime.Now.AddDays(7), 5);
            _eventService.RegisterParticipant(ev, _participant);
            
            _postService = new PostService(_eventService);
        }

        [Fact]
        public void CreatePost_ShouldAllowForEventParticipants()
        {
            // Act
            var post = _postService.CreatePost(_participant, "Test content");
            
            // Assert
            Assert.Single(_postService.GetAllPosts());
            Assert.Equal("Test content", post.Content);
        }

        [Fact]
        public void CreatePost_ShouldThrowForNonParticipants()
        {
            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => 
                _postService.CreatePost(_nonParticipant, "Test content"));
        }

        [Fact]
        public void AddComment_ShouldAllowForEventParticipants()
        {
            // Arrange
            var post = _postService.CreatePost(_participant, "Test post");
            
            // Act
            var comment = _postService.AddComment(_participant, post, "Test comment");
            
            // Assert
            Assert.Single(_postService.GetCommentsForPost(post));
            Assert.Equal("Test comment", comment.Content);
        }

        [Fact]
        public void AddComment_ShouldThrowForNonParticipants()
        {
            // Arrange
            var post = _postService.CreatePost(_participant, "Test post");
            
            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => 
                _postService.AddComment(_nonParticipant, post, "Test comment"));
        }

        [Fact]
        public void GetCommentsForPost_ShouldReturnOnlyRelatedComments()
        {
            // Arrange
            var post1 = _postService.CreatePost(_participant, "Post 1");
            var post2 = _postService.CreatePost(_participant, "Post 2");
            
            _postService.AddComment(_participant, post1, "Comment 1");
            _postService.AddComment(_participant, post2, "Comment 2");
            
            // Act
            var commentsForPost1 = _postService.GetCommentsForPost(post1);
            var commentsForPost2 = _postService.GetCommentsForPost(post2);
            
            // Assert
            Assert.Single(commentsForPost1);
            Assert.Single(commentsForPost2);
            Assert.Equal("Comment 1", commentsForPost1.First().Content);
            Assert.Equal("Comment 2", commentsForPost2.First().Content);
        }
    }