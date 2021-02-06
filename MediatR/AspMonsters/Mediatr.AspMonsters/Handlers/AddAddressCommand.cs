using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Mediatr.AspMonsters.Handlers
{
    public class AddAddressCommand : IRequest<AddAddressResponse> //, IRequest<Unit>
    {
        public int UserId { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Street1 { get; set; }
    }

    public class AddAddressResponse
    {
    }

    public class AddAddressResponseHandler : IRequestHandler<AddAddressCommand, AddAddressResponse>
    {
        private readonly IMediator _mediator;

        public AddAddressResponseHandler(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<AddAddressResponse> Handle(AddAddressCommand request, CancellationToken cancellationToken)
        {
            await _mediator.Publish(new AddressAddedNotification(), cancellationToken);
            return new AddAddressResponse();
        }
    }

    public class AddressAddedNotification : INotification
    {

    }

    public class AddressAddedNotificationHandler1 : INotificationHandler<AddressAddedNotification>
    {
        public Task Handle(AddressAddedNotification notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }

    public class AddressAddedNotificationHandler2 : INotificationHandler<AddressAddedNotification>
    {
        public Task Handle(AddressAddedNotification notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }

}