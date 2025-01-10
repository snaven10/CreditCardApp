using FluentValidation;
using CreditCardApp.Api.DTOs;

namespace CreditCardApp.Api.Validators;

public class PaymentValidator : AbstractValidator<PaymentDto>
{
    public PaymentValidator()
    {
        RuleFor(payment => payment.Amount)
            .GreaterThan(0).WithMessage("El monto del pago debe ser mayor a 0.");

        RuleFor(payment => payment.PaymentDate)
            .NotEmpty().WithMessage("La fecha del pago es obligatoria.")
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("La fecha del pago no puede ser en el futuro.");

        RuleFor(payment => payment.CardId)
            .GreaterThan(0).WithMessage("El ID de la tarjeta asociado es obligatorio.");
    }
}
