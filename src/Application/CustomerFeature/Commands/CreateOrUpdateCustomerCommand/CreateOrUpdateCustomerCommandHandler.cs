using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Services;
using MediatR;


namespace Application.Customer.Commands.CreateOrUpdateCustomerCommand
{
    public class CreateOrUpdateCustomerCommandHandler : IRequestHandler<CreateOrUpdateCustomerCommand, Unit>
    {
        private readonly ICustomerService _customerService;
        public CreateOrUpdateCustomerCommandHandler(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<Unit> Handle(CreateOrUpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            await _customerService.CreateOrUpdateCustomerAsync(request);
            throw new NotImplementedException();
        }
    }
}
