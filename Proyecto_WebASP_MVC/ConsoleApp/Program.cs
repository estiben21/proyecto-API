using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

Document.Create(dcument =>
{
    dcument.Page(page =>
    {
        page.Margin(30);

        page.Header().ShowOnce().Row(row =>
        {
            row.ConstantItem(200).Height(100).Placeholder();

            row.RelativeItem().Column(col =>
            {
                col.Item().Row(row =>
                {
                    row.ConstantItem(20); // Espacio vacío para mover hacia abajo
                    row.RelativeItem().AlignCenter().Text("REPORTE DE PRODUCTOS").Bold().FontSize(20);
                });
            });
        });

        page.Content().PaddingVertical(10).Column(col1 =>
        {

            col1.Item().LineHorizontal(0.5f);

            col1.Item().Table(tabla =>
            {
                tabla.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                });

                tabla.Header(header =>
                {
                    header.Cell().Background("#257272")
                    .Padding(1).AlignCenter().Text("ID").FontColor("#fff").FontSize(10);

                    header.Cell().Background("#257272")
                    .Padding(1).AlignCenter().Text("Producto").FontColor("#fff").FontSize(10);

                    header.Cell().Background("#257272")
                    .Padding(1).AlignCenter().Text("Descripciòn").FontColor("#fff").FontSize(10);

                    header.Cell().Background("#257272")
                    .Padding(1).AlignCenter().Text("Stock").FontColor("#fff").FontSize(10);

                    header.Cell().Background("#257272")
                    .Padding(1).AlignCenter().Text("Color").FontColor("#fff").FontSize(10);

                    header.Cell().Background("#257272")
                    .Padding(1).AlignCenter().Text("Talla").FontColor("#fff").FontSize(10);

                    header.Cell().Background("#257272")
                    .Padding(1).AlignCenter().Text("Precio").FontColor("#fff").FontSize(10);

                    header.Cell().Background("#257272")
                    .Padding(1).AlignCenter().Text("Categoria").FontColor("#fff").FontSize(10);

                    header.Cell().Background("#257272")
                    .Padding(1).AlignCenter().Text("Imagen").FontColor("#fff").FontSize(10);
                });
            });
        });
    });
}).ShowInPreviewer();
