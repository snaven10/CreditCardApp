using CreditCardApp.Application.DTOs;
using FluentValidation;

namespace CreditCardApp.Application.Validators;

public class CardValidator : AbstractValidator<CardDto>
{
    public CardValidator()
    {
        RuleFor(card => card.CardHolderName)
            .NotEmpty().WithMessage("El nombre del titular no puede estar vacío.")
            .MaximumLength(100).WithMessage("El nombre del titular no puede exceder los 100 caracteres.");

        RuleFor(card => card.CardNumber)
            .CreditCard().WithMessage("El número de tarjeta no es válido.");

        RuleFor(card => card.CreditLimit)
            .GreaterThan(0).WithMessage("El límite de la tarjeta debe ser mayor a 0.");

        RuleFor(card => card.CurrentBalance)
            .GreaterThanOrEqualTo(0).WithMessage("El saldo actual no puede ser negativo.");
    }
}
