using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;

namespace Application.Documents.Builders
{
    public static class Sections
    {
        public static Paragraph SectionHeader(string text, string bgColor, string textColor)
        {
            return new Paragraph(
                new ParagraphProperties(
                    new SpacingBetweenLines { Before = "140", After = "80" },
                    new Shading { Fill = bgColor, Color = "auto", Val = ShadingPatternValues.Clear },
                    new Indentation { Left = "80", Right = "80" }
                ),
                new Run(Styles.ArialRun(18, textColor, true), new Text(text.ToUpper()))
            );
        }

        public static Paragraph Title(Domain.Models.Intervention intervention)
        {
            return PartsFactory.TextParagraph($"{intervention.Reference} - { intervention.Titre }", 28, Colors.TextPrimary, true, false, "Left", 120, 60);
        }

        public static Paragraph Subtitle(Domain.Models.Intervention intervention)
        {
            return new Paragraph(
                new ParagraphProperties(new SpacingBetweenLines { Before = "0", After = "120" }),
                new Run(Styles.ArialRun(17, Colors.TextSecondary),
                    new Text("Généré le ") { Space = SpaceProcessingModeValues.Preserve }),
                new Run(Styles.ArialRun(17, Colors.TextSecondary),
                    new Text(DateTime.Now.ToShortDateString()) { Space = SpaceProcessingModeValues.Preserve }),
                new Run(Styles.ArialRun(17, Colors.TextSecondary),
                    new Text("   ·   Validée par : ") { Space = SpaceProcessingModeValues.Preserve }),
                new Run(Styles.ArialRun(17, Colors.Accent, true),
                    new Text(intervention.IdValidateur.ToString()) { Space = SpaceProcessingModeValues.Preserve })
            );
        }

        public static Paragraph BlockLabel(string text)
            => PartsFactory.TextParagraph(text, 18, Colors.TextSecondary, false, true, "Left", 0, 60);

        public static Paragraph PhotosIntro()
        {
            return new Paragraph(
                new ParagraphProperties(new SpacingBetweenLines { Before = "0", After = "60" }),
                new Run(Styles.ArialRun(18, Colors.TextSecondary),
                    new Text("{{NB_PHOTOS}} photo(s) jointe(s) — GPS : ") { Space = SpaceProcessingModeValues.Preserve }),
                new Run(Styles.ArialRun(18, Colors.Primary),
                    new Text("{{GPS_LATITUDE}}°N, {{GPS_LONGITUDE}}°E") { Space = SpaceProcessingModeValues.Preserve }),
                new Run(Styles.ArialRun(18, Colors.TextSecondary),
                    new Text("  ·  Horodatage serveur certifié") { Space = SpaceProcessingModeValues.Preserve })
            );
        }
    }
}
