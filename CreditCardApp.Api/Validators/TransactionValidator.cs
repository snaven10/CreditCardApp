using FluentValidation;
using CreditCardApp.Api.DTOs;

namespace CreditCardApp.Api.Validators;

public class TransactionValidator : AbstractValidator<TransactionDto>
{
    public TransactionValidator()
    {
        RuleFor(transaction => transaction.Amount)
            .NotEmpty().WithMessage("El monto de la transacción es obligatorio.")
            .GreaterThan(0).WithMessage("El monto de la transacción debe ser mayor a 0.");

        RuleFor(transaction => transaction.TransactionType)
            .IsInEnum().WithMessage("El tipo de transacción es inválido.");

        RuleFor(transaction => transaction.TransactionDate)
            .NotEmpty().WithMessage("La fecha de la transacción es obligatoria.")
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("La fecha de la transacción no puede ser en el futuro.");
    }
}
