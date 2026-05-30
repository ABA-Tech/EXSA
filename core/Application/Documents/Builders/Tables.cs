using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;
using Domain.Models;

namespace Application.Documents.Builders
{
    public static class Tables
    {
        public const int ColFull = 9060;
        public const int ColHalf = 4530;

        public static Table InfoTable(Domain.Models.Intervention intervention)
        {
            return PartsFactory.TableFixed(ColFull,
                PartsFactory.Row(
                    FieldCell("Client", intervention.NomClient, ColHalf),
                    FieldCell("Type d'intervention", intervention.Type, ColHalf)
                ),
                PartsFactory.Row(
                    FieldCell("Site / Adresse", intervention.Adresse, ColHalf),
                    FieldCell("Priorité", intervention.Priorite.ToString(), ColHalf)
                ),
                PartsFactory.Row(
                    FieldCell("Date planifiée", intervention.DatePlanifiee?.ToShortDateString(), ColHalf),
                    FieldCell("Démarrée le", intervention.DateDebut?.ToShortDateString(), ColHalf)
                ),
                PartsFactory.Row(
                    FieldCell("Terminée le", intervention.DateFin?.ToShortDateString(), ColHalf),
                    FieldCell("Durée totale", (intervention.DateFin.Value - intervention.DateDebut.Value).Days.ToString() + " Jours", ColHalf)
                )
            );
        }

        public static Table InfoTable(TableRow[] tableRows)
        {
            return PartsFactory.TableFixed(ColFull, tableRows);
        }

        static TableCell FieldCell(string label, string placeholder, int width)
        {
            return PartsFactory.Cell(new OpenXmlElement[]
            {
            PartsFactory.TextParagraph(label.ToUpper(), 14, Colors.TextSecondary),
            PartsFactory.PlaceholderParagraph(placeholder, 20, Colors.Primary, true)
            }, width, Colors.GrayLight);
        }

        public static Table TeamTable(Intervention intervention)
        {
            var employees = new List<TableRow>() { TeamHeaderRow() };
            foreach(var t in intervention.AffectationInterventions)
            {
                employees.Add(TeamRow(t.Technicien.NomComplet, t.Technicien.Role, t.Technicien?.TokenFcm, t.Technicien.Telephone));
            }

            return PartsFactory.TableFixed(ColFull,
                employees.ToArray()
            );
        }

        static TableRow TeamHeaderRow()
        {
            return PartsFactory.Row(
                HeaderCell("Technicien", 3500),
                HeaderCell("Rôle / Spécialité", 3060),
                HeaderCell("Statut", 1500),
                HeaderCell("N° Employé", 1000)
            );
        }

        static TableCell HeaderCell(string text, int width)
        {
            return PartsFactory.Cell(new OpenXmlElement[]
            {
            PartsFactory.TextParagraph(text, 18, Colors.Primary, true)
            }, width, Colors.PrimaryLight);
        }

        static TableRow TeamRow(string a, string b, string c, string d)
        {
            return PartsFactory.Row(
                PartsFactory.CellPlaceholder(a, 3500),
                PartsFactory.CellPlaceholder(b, 3060),
                PartsFactory.CellPlaceholder(c, 1500),
                PartsFactory.CellPlaceholder(d, 1000)
            );
        }

        public static Table TextBlock(string placeholderKey)
        {
            return PartsFactory.TableFixed(ColFull,
                PartsFactory.Row(
                    PartsFactory.Cell(new OpenXmlElement[]
                    {
                    PartsFactory.PlaceholderParagraph(placeholderKey, 20, Colors.TextPrimary, false)
                    }, ColFull, Colors.GrayLight)
                )
            );
        }

        public static Table StockTable()
        {
            return PartsFactory.TableFixed(ColFull,
                PartsFactory.Row(
                    StockHeader("Désignation", 3500, Colors.Green, Colors.GreenLight),
                    StockHeader("Référence", 2000, Colors.Green, Colors.GreenLight),
                    StockHeader("Qté", 1000, Colors.Green, Colors.GreenLight),
                    StockHeader("Unité", 1000, Colors.Green, Colors.GreenLight),
                    StockHeader("PU (XAF)", 1560, Colors.Green, Colors.GreenLight)
                ),
                StockRow(1),
                StockRow(2),
                StockRow(3),
                TotalRow()
            );
        }

        static TableCell StockHeader(string text, int width, string textColor, string fill)
        {
            return PartsFactory.Cell(new OpenXmlElement[]
            {
            PartsFactory.TextParagraph(text, 18, textColor, true)
            }, width, fill);
        }

        static TableRow StockRow(int n)
        {
            return PartsFactory.Row(
                PartsFactory.CellPlaceholder($"PIECE_{n}_NOM", 3500),
                PartsFactory.CellPlaceholder($"PIECE_{n}_REF", 2000),
                PartsFactory.CellPlaceholder($"PIECE_{n}_QTE", 1000),
                PartsFactory.CellPlaceholder($"PIECE_{n}_UNITE", 1000),
                PartsFactory.CellPlaceholder($"PIECE_{n}_PU_XAF", 1560)
            );
        }

        static TableRow TotalRow()
        {
            var label = PartsFactory.Cell(new OpenXmlElement[]
            {
            PartsFactory.TextParagraph("TOTAL PIÈCES", 18, Colors.TextPrimary, true)
            }, 7500, Colors.GrayLight);

            var value = PartsFactory.CellPlaceholder("TOTAL_PIECES_XAF", 1560, Colors.PrimaryLight);

            var row = new TableRow();
            label.Append(new GridSpan { Val = 4 });
            row.Append(label);
            row.Append(value);
            return row;
        }

        public static Table SignatureTable()
        {
            var left = SignatureCell("Signature client", "CLIENT_SIGNATAIRE", "CLIENT_DATE_SIGNATURE", Colors.GreenLight);
            var right = SignatureCell("Visa superviseur", "SUPERVISEUR_NOM", "SUPERVISEUR_DATE_VISA", Colors.PrimaryLight);
            var gap = PartsFactory.EmptyCell(260, true);

            return PartsFactory.TableFixed(ColFull,
                PartsFactory.Row(left, gap, right)
            );
        }

        static TableCell SignatureCell(string title, string namePH, string datePH, string fill)
        {
            var p = new Paragraph(
                new ParagraphProperties(new Justification { Val = JustificationValues.Center }),
                new Run(Styles.ArialRun(18, Colors.TextSecondary, true), new Text(title))
            );

            var line = new Paragraph(
                new ParagraphProperties(new ParagraphBorders(
                    new BottomBorder { Val = BorderValues.Single, Size = 4, Color = Colors.BorderLight }
                )),
                new Run(new RunProperties(new FontSize { Val = "20" }), new Text("                                                            "))
            );

            return PartsFactory.Cell(new OpenXmlElement[]
            {
            p,
            PartsFactory.Spacer(80),
            line,
            PartsFactory.PlaceholderParagraph(namePH, 18, Colors.Primary, true, "Center", 60, 0),
            new Paragraph(
                new ParagraphProperties(new Justification { Val = JustificationValues.Center }),
                new Run(Styles.ArialRun(18, Colors.TextSecondary),
                    new Text("Date : ") { Space = SpaceProcessingModeValues.Preserve }),
                new Run(Styles.ArialRun(18, Colors.Primary, true),
                    new Text($"{{{{{datePH}}}}}") { Space = SpaceProcessingModeValues.Preserve })
            )
            }, 4400, fill, top: 120, bottom: 120, left: 140, right: 140);
        }

        public static Table HistoryTable()
        {
            return PartsFactory.TableFixed(ColFull,
                HistoryHeaderRow(),
                HistoryRow("●", Colors.TextSecondary, "HIST_1_DATE", "HIST_1_STATUT", "HIST_1_NOTE"),
                HistoryRow("●", Colors.Primary, "HIST_2_DATE", "HIST_2_STATUT", "HIST_2_NOTE"),
                HistoryRow("●", Colors.Accent, "HIST_3_DATE", "HIST_3_STATUT", "HIST_3_NOTE"),
                HistoryRow("●", Colors.Green, "HIST_4_DATE", "HIST_4_STATUT", "HIST_4_NOTE")
            );
        }

        static TableRow HistoryHeaderRow()
        {
            return PartsFactory.Row(
                HeaderCellNoBorder("", 900),
                HeaderCellNoBorder("Date & heure", 2200),
                HeaderCellNoBorder("Statut", 2400),
                HeaderCellNoBorder("Note / Agent", 3560)
            );
        }

        static TableCell HeaderCellNoBorder(string text, int width)
        {
            return PartsFactory.Cell(new OpenXmlElement[]
            {
            PartsFactory.TextParagraph(text, 18, Colors.TextSecondary, true)
            }, width, Colors.GrayLight, noBorder: true);
        }

        static TableRow HistoryRow(string icon, string color, string datePH, string statutPH, string notePH)
        {
            var dot = PartsFactory.Cell(new OpenXmlElement[]
            {
            PartsFactory.TextParagraph(icon, 20, color, true, false, "Center")
            }, 900, noBorder: true, center: true);

            var date = PartsFactory.Cell(new OpenXmlElement[]
            {
            PartsFactory.PlaceholderParagraph(datePH, 18, Colors.TextSecondary, false)
            }, 2200, noBorder: true);

            var statut = PartsFactory.Cell(new OpenXmlElement[]
            {
            PartsFactory.PlaceholderParagraph(statutPH, 18, Colors.TextPrimary, true)
            }, 2400, noBorder: true);

            var note = PartsFactory.Cell(new OpenXmlElement[]
            {
            PartsFactory.PlaceholderParagraph(notePH, 18, Colors.TextSecondary, false)
            }, 3560, noBorder: true);

            return PartsFactory.Row(dot, date, statut, note);
        }
    }

}
