using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.Customer.Commands.CreateOrUpdateCustomerCommand
{
    public class CreateOrUpdateCustomerCommandValidator
    {
        public class ValidateCustomerInfo : AbstractValidator<CreateOrUpdateCustomerCommand>
        {
            private readonly string _empty = $"{0} is not null or empty";
            public ValidateCustomerInfo() 
            { 
                RuleFor(req => req.EmailAddress).Must(email => string.IsNullOrEmpty(email)).WithMessage(string.Format("Email", _empty));
            }
        }
    }
}
