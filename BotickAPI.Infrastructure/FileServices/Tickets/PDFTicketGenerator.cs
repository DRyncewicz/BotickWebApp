using BotickAPI.Application.Common.Interfaces;
using BotickAPI.Domain.Entities;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Text;

namespace BotickAPI.Infrastructure.FileServices.Tickets
{
    public class PDFTicketGenerator : IPDFTicketGenerator
    {
        public byte[] GenerateTicket(Event @event)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                iTextSharp.text.Document document = new iTextSharp.text.Document(new iTextSharp.text.Rectangle(240, 400));
                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                document.Open();
                PdfPTable table = new PdfPTable(2);
                table.WidthPercentage = 100;

                PdfPCell cell;

                iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance("logo.jpg");
                logo.ScaleToFit(50f, 50f);
                cell = new PdfPCell(logo);
                cell.Border = PdfPCell.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Nr rezerwacji: 171471\nIlość biletów: 4"));
                cell.Border = PdfPCell.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("BILET INTERNETOWY", FontFactory.GetFont("Arial", 16, iTextSharp.text.Font.BOLD)));
                cell.Colspan = 2;
                cell.Border = PdfPCell.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.PaddingBottom = 10;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Koncert U2 w trójwymiarze\n12-04-2008, godz. 17:00\nKino Atlantik sala C\nWarszawa, ul. Chmielna 33"));
                cell.Colspan = 2;
                cell.BorderWidth = 1;
                cell.Padding = 10;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Właściciel biletu: Jan Kowalski"));
                cell.BorderWidth = 1;
                cell.Padding = 10;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("parter rząd: 4 miejsce: 5\nCena: 30 zł"));
                cell.BorderWidth = 1;
                cell.Padding = 10;
                table.AddCell(cell);

                Barcode128 barcode = new Barcode128();
                barcode.Code = "17147178432310";
                barcode.BarHeight = 40f;
                barcode.X = 1f;

                iTextSharp.text.Image barcodeImage = barcode.CreateImageWithBarcode(writer.DirectContent, BaseColor.BLACK, BaseColor.WHITE);
                barcodeImage.ScalePercent(100);

                PdfPCell barcodeCell = new PdfPCell(barcodeImage);
                barcodeCell.Colspan = 2;
                barcodeCell.HorizontalAlignment = Element.ALIGN_CENTER;
                barcodeCell.Border = PdfPCell.NO_BORDER;
                barcodeCell.PaddingTop = 10;
                barcodeCell.PaddingBottom = 10;

                table.AddCell(barcodeCell);

                document.Add(table);
                document.Close();

                return memoryStream.ToArray();
            }
        }
    }
}
