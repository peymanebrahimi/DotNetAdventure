/*
 * Problem: You have a set of objects that interact with each other in complex ways. This creates tight coupling between the objects, making it difficult to understand and maintain the system.
 *
 * Imagine a chat room application where users send messages to each other. Without a mediator, each user object would need to know about all other user objects to send and receive messages. This leads to a tangled web of dependencies.
 */
namespace ConsoleApp8.Mediator
{
    // Mediator interface
    public interface IChatMediator
    {
        void SendMessage(string message, User user);
        void RegisterUser(User user);
    }

// Concrete mediator
    public class ChatMediator : IChatMediator
    {
        private List<User> _users = new List<User>();

        public void SendMessage(string message, User user)
        {
            foreach (var u in _users)
            {
                if (u != user) // Don't send message to the sender
                {
                    u.ReceiveMessage(message);
                }
            }
        }

        public void RegisterUser(User user)
        {
            _users.Add(user);
        }
    }

// Colleague class
    public class User
    {
        private IChatMediator _mediator;
        public string Name { get; }

        public User(IChatMediator mediator, string name)
        {
            _mediator = mediator;
            Name = name;
            _mediator.RegisterUser(this); // Register with mediator
        }

        public void Send(string message)
        {
            Console.WriteLine($"{Name} sends: {message}");
            _mediator.SendMessage(message, this);
        }

        public void ReceiveMessage(string message)
        {
            Console.WriteLine($"{Name} receives: {message}");
        }
    }

// Usage
    class Consumer
    {
        void Run()
        {
            var mediator = new ChatMediator();
            var user1 = new User(mediator, "Alice");
            var user2 = new User(mediator, "Bob");
            user1.Send("Hello Bob!");
            user2.Send("Hi Alice!");
        }
    }
}