using EventsExample.Models;

namespace EventsExample.Services;

public class PostService
{
    private readonly EventService _eventService;
    private readonly UserStorageService _userStorageService;
    private readonly List<Post> _posts = new();
    private readonly List<Comment> _comments = new();
    
    private IEnumerable<Comment> GetCommentsForPost(Post post)
    {
        return _comments.FindAll(c => c.RelatedPost == post);
    }
    
    public PostService(EventService eventService, UserStorageService userStorageService)
    {
        _eventService = eventService;
        _userStorageService = userStorageService;
    }

    public Post CreatePost(Guid authorId, Guid eventId, string content, DateTime? extraCreationDate = null)
    {
        if (!_eventService.IsUserParticipant(authorId, eventId))
        {
            throw new InvalidOperationException("Только участинки могут создавать посты");
        }
        
        var user = _userStorageService.GetUser(authorId);

        if (user is null)
        {
            throw new ArgumentException("Пользователя не существует");
        }
        
        var post = new Post(user, content, eventId, extraCreationDate);
        _posts.Add(post);
        return post;
    }

    public Comment AddComment(Guid authorId, Guid postId, string content, DateTime? extraCreationDate = null)
    {
        var post = _posts.FirstOrDefault(p => p.Id == postId);
        if (post is null)
        {
            throw new ArgumentException("Пост не найден");
        }
        
        if (!_eventService.IsUserParticipant(authorId, post.EventId))
        {
            throw new InvalidOperationException("Только участинки могут оставлять комментарии");
        }
        
        var user = _userStorageService.GetUser(authorId);
        if (user is null)
        {
            throw new ArgumentException("Пользователя не существует");
        }
        
        var comment = new Comment(user, post, content, extraCreationDate);
        _comments.Add(comment);
        return comment;
    }

    public IEnumerable<Post> GetAllPosts()
    {
        return _posts;
    }

    public void DisplayPostsWithComments()
    {
        foreach (var post in _posts)
        {
            Console.WriteLine($"Пост от {post.Author.Username} ({post.DaysAgoCreated} дней назад)");
            Console.WriteLine($"Кратко: {post.Preview}");
            Console.WriteLine("Комментарии:");

            foreach (var comment in GetCommentsForPost(post))
            {
                Console.WriteLine($"- {comment.Author.Username}: {comment.Content} ({comment.DaysAgoCreated} дней назад)");
            }
            Console.WriteLine();
        }
    }
}