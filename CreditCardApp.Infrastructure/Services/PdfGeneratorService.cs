using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using CreditCardApp.Domain.Entities;
using iText.Kernel.Font;
using iText.IO.Font.Constants; // Para obtener fuentes predeterminadas

namespace CreditCardApp.Infrastructure.Services
{
    public class PdfGeneratorService
    {
        public byte[] GenerateStatementPdf(Card card)
        {
            using (var stream = new MemoryStream())
            {
                var writer = new PdfWriter(stream);
                var pdf = new PdfDocument(writer);
                var document = new Document(pdf);

                // Usar la fuente en negrita predeterminada
                var boldFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);

                // Título del documento
                document.Add(new Paragraph($"Estado de Cuenta - {card.CardHolderName}")
                    .SetFont(boldFont)); // Aplicar fuente en negrita
                document.Add(new Paragraph($"Número de Tarjeta: {card.CardNumber}"));
                document.Add(new Paragraph($"Saldo Actual: {card.CurrentBalance:C}"));
                document.Add(new Paragraph($"Límite de Crédito: {card.CreditLimit:C}"));
                document.Add(new Paragraph($"Saldo Disponible: {card.CreditLimit - card.CurrentBalance:C}"));

                // Movimientos
                document.Add(new Paragraph("\nMovimientos:")
                    .SetFont(boldFont)); // Aplicar fuente en negrita
                foreach (var purchase in card.Purchases)
                {
                    document.Add(new Paragraph($"Compra: {purchase.PurchaseDate.ToShortDateString()} - {purchase.Description} - {purchase.Amount:C}"));
                }

                foreach (var payment in card.Payments)
                {
                    document.Add(new Paragraph($"Pago: {payment.PaymentDate.ToShortDateString()} - {payment.Amount:C}"));
                }

                document.Close();

                return stream.ToArray();
            }
        }
    }
}
