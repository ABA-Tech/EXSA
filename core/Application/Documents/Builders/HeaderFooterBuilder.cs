using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.ExtendedProperties;
using DocumentFormat.OpenXml;
using Domain.Models;

namespace Application.Documents.Builders
{

    public static class HeaderFooterBuilder
    {
        public static Header BuildHeader(Intervention intervention)
        {
            var left = PartsFactory.Cell(new OpenXmlElement[]
            {
            PartsFactory.TextParagraph("{{NOM_SOCIETE}}", 28, Colors.White, true),
            PartsFactory.TextParagraph("{{ADRESSE_SOCIETE}}", 16, Colors.WhiteSoft)
            }, 5000, Colors.HeaderBg, noBorder: true, top: 120, bottom: 120, left: 160, right: 160);

            var right = PartsFactory.Cell(new OpenXmlElement[]
            {
            PartsFactory.TextParagraph("RAPPORT D'INTERVENTION", 16, Colors.WhiteSoft, false, false, "Right"),
            PartsFactory.TextParagraph("{{REFERENCE}}", 26, Colors.White, true, false, "Right"),
            new Paragraph(
                new ParagraphProperties(new Justification { Val = JustificationValues.Right }),
                new Run(Styles.ArialRun(18, Colors.WhiteSoft),
                    new Text("Statut : ") { Space = SpaceProcessingModeValues.Preserve }),
                new Run(Styles.ArialRun(18, Colors.Accent, true),
                    new Text("{{STATUT}}") { Space = SpaceProcessingModeValues.Preserve })
            )
            }, 4060, Colors.HeaderBg, noBorder: true, center: true, top: 120, bottom: 120, left: 160, right: 160);

            var table = PartsFactory.TableFixed(9060,
                PartsFactory.Row(left, right)
            );

            return new Header(table);
        }

        public static Footer BuildFooter(Intervention intervention)
        {
            var p = new Paragraph(
                new ParagraphProperties(
                    new ParagraphBorders(new TopBorder { Val = BorderValues.Single, Size = 4, Color = Colors.BorderLight }),
                    new SpacingBetweenLines { Before = "80" },
                    new Tabs(
                        new TabStop { Val = TabStopValues.Center, Position = 4680 },
                        new TabStop { Val = TabStopValues.Right, Position = 9060 }
                    )
                ),
                new Run(Styles.ArialRun(16, Colors.TextSecondary),
                    new Text(intervention.LocataireNavigation?.Nom) { Space = SpaceProcessingModeValues.Preserve }),
                new Run(Styles.ArialRun(16, Colors.TextSecondary),
                    new Text("\t"+ intervention.Reference + "  ·  Page ") { Space = SpaceProcessingModeValues.Preserve }),
                new SimpleField { Instruction = "PAGE" },
                new Run(Styles.ArialRun(16, Colors.TextSecondary),
                    new Text("\tExporté le " + DateTime.Now.ToShortDateString()) { Space = SpaceProcessingModeValues.Preserve })
            );

            return new Footer(p);
        }
    }

}