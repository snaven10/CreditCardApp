using FluentValidation;
using CreditCardApp.Api.DTOs;

namespace CreditCardApp.Api.Validators;

public class PurchaseValidator : AbstractValidator<PurchaseDto>
{
    public PurchaseValidator()
    {
        RuleFor(purchase => purchase.Description)
            .NotEmpty().WithMessage("La descripción no puede estar vacía.")
            .MaximumLength(200).WithMessage("La descripción no puede exceder los 200 caracteres.");

        RuleFor(purchase => purchase.Amount)
            .GreaterThan(0).WithMessage("El monto de la compra debe ser mayor a 0.");

        RuleFor(purchase => purchase.PurchaseDate)
            .NotEmpty().WithMessage("La fecha de la compra es obligatoria.")
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("La fecha de la compra no puede ser en el futuro.");
        RuleFor(purchase => purchase.CardId)
            .GreaterThan(0).WithMessage("Se debe seleccionar una tarjeta.");
    }
}
