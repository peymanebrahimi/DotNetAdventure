using Microsoft.AspNetCore.Mvc;

namespace Webhook.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class OrdersController(InMemoryOrderRepository orderRepository): ControllerBase
{
   
   [HttpPost]
   public async Task<IActionResult> Post(Order order)
   {
      
   }
}

public record Order();

public class InMemoryOrderRepository
{
   public void Save(Order order)
   {
      
   }
}