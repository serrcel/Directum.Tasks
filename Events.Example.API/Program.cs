using EventsExample.Models;
using EventsExample.Services;

namespace EventsExample;

class Program
{
    static void Main(string[] args)
    {
        var userStorage = new UserStorageService();

        var user1 = userStorage.CreateUser("John Doe");
        var user2 = userStorage.CreateUser("Jane Smith");
        var user3 = userStorage.CreateUser("John Cena"); // Нигде не участвует
        
        var eventService = new EventService();
        var postService = new PostService(eventService, userStorage);
        
        // Создаем мероприятие
        var workshop = eventService.CreateEvent(user1, "C# Workshop", DateTime.Now.AddDays(14), 1, DateTime.Now.AddDays(-7));
        var workshop2 = eventService.CreateEvent(user1, "C++ Workshop", DateTime.Now.AddDays(2), 11, DateTime.Now.AddDays(-5));
        
        // Регистрируем участников
        eventService.RegisterParticipant(workshop.Id, user1.Id);
        eventService.RegisterParticipant(workshop.Id, user2.Id);
        
        eventService.DisplayEvents();
        
        Console.WriteLine("----------------");
        
        // user1 и user2 могут создавать посты и комментарии
        var post = postService.CreatePost(user1.Id, workshop.Id, "I started a project in .NET 6 just to get acquainted with new tech.", DateTime.Now.AddDays(-5));
        postService.AddComment(user2.Id, post.Id, "Зачем меня добавили?!", post.CreatedAt.AddDays(1));
        postService.AddComment(user1.Id, post.Id, "Подучишь инфы о .NET 6)",post.CreatedAt.AddDays(2));
        postService.AddComment(user2.Id, post.Id, "Так уже 9 давно вышел!");
        
        try
        {
            // user3 не участник
            postService.CreatePost(user3.Id, workshop.Id, "Я смогу написать пост?");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"[Error]: {ex.Message}");
        }
        
        try
        {
            // user3 не может комментировать
            postService.AddComment(user3.Id, post.Id, "Фигня все это");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"[Error]: {ex.Message}");
        }
        
        // Выводим результаты
        postService.DisplayPostsWithComments();
    }
}