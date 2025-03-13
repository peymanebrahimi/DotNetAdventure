namespace Webhook.Api.Repositories;

public class InMemoryOrderRepository
{
    public void Add(Order order)
    {
      
    }

    public IEnumerable<Order> GetAll()
    {
        return new List<Order>();
    }
}