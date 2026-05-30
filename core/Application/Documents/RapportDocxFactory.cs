using Application.Documents.Builders;
using DocumentFormat.OpenXml.Wordprocessing;

namespace Application.Documents
{
    public static class DocxFactory
    {
        //public static void Build(Body body)
        //{
        //    AddTitle(body);

        //    AddSection(body, "1. Informations générales");
        //    Tables.InfoTable(body);

        //    AddSection(body, "2. Équipe terrain");
        //    Tables.TeamTable(body);

        //    AddSection(body, "3. Travaux réalisés");
        //    Tables.TextBlock(body, "DESCRIPTION");
        //    Tables.TextBlock(body, "COMPTE_RENDU");

        //    AddSection(body, "4. Pièces consommées");
        //    Tables.StockTable(body);

        //    AddSection(body, "5. Signatures");
        //    Tables.SignatureTable(body);

        //    AddSection(body, "6. Historique");
        //    Tables.HistoryTable(body);
        //}

        //private static void AddTitle(Body body)
        //{
        //    body.Append(new Paragraph(
        //        new Run(new Text("RAPPORT D'INTERVENTION {{REFERENCE}}"))
        //    ));
        //}

        //private static void AddSection(Body body, string title)
        //{
        //    body.Append(new Paragraph(
        //        new Run(new Text(title.ToUpper()))
        //    ));
        //}
    }
}
