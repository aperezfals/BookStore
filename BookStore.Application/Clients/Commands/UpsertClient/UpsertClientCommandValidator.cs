using FluentValidation;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace BookStore.Application.Clients.Commands.UpsertClient
{
    public class UpsertClientCommandValidator : AbstractValidator<UpsertClientCommand>
    {
        public UpsertClientCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(200);
        }
    }
}
