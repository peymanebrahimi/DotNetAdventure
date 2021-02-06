using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using MediatR;
using Mediatr.AspMonsters.Handlers;

namespace Mediatr.AspMonsters.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IMediator _mediator;

        public IndexModel(ILogger<IndexModel> logger,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public void OnGet()
        {

        }

        public async Task OnPostAsync()
        {
            await _mediator.Send(new AddAddressCommand());
        }
    }
}
