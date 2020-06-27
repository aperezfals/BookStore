using FluentValidation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BookStore.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.Ammount)
                .GreaterThan(1);
        }
    }
}
