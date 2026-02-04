using FluentValidation;
using MyCookBook.Communication.Requests;
using MyCookBook.Exceptions;

namespace MyCookBook.Application.UseCases.User.Register
{
    public class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
    {
        public RegisterUserValidator()
        {
            RuleFor(user => user.Name).NotEmpty().WithMessage(ResourceMessagesException.NAME_EMPTY);
            RuleFor(user => user.Email).NotEmpty().EmailAddress().WithMessage(ResourceMessagesException.EMAIL_EMPTY);
            RuleFor(user => user.Password).NotEmpty().MinimumLength(6).WithMessage(ResourceMessagesException.PASSWORD_EMPTY);
        }
    }
}
