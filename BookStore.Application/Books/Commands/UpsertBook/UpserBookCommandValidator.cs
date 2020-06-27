using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Application.Books.Commands.UpsertBook
{
    public class UpserBookCommandValidator : AbstractValidator<UpsertBookCommand>
    {
        public UpserBookCommandValidator()
        {
            RuleFor(x => x.ISBN)
                .NotEmpty()
                .MaximumLength(13)
                .MinimumLength(10);

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(200);
        }
    }
}
