using Application.Documents.Builders;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Domain.Models;
//using FieldOps.Report.Builders;

using Tables = Application.Documents.Builders.Tables;

namespace Application.Documents
{
    public static class ReportDocumentBuilder
    {
        public static void Create(string path, Intervention intervention)
        {
            using var doc = WordprocessingDocument.Create(path, WordprocessingDocumentType.Document);

            var main = doc.AddMainDocumentPart();
            main.Document = new Document();
            var body = new Body();

            AddStyles(main);
            AddHeader(main, intervention);
            AddFooter(main, intervention);
            body.Append(BuildBody(intervention));
            body.Append(SectionProperties());

            main.Document.Append(body);
            main.Document.Save();
        }

        static IEnumerable<OpenXmlElement> BuildBody(Intervention intervention)
        {
            var elements = new List<OpenXmlElement>
        {
            Sections.Title(intervention),
            Sections.Subtitle(intervention),

            Sections.SectionHeader("1. Informations générales", Colors.PrimaryLight, Colors.Primary),
            PartsFactory.Spacer(60),
            Builders.Tables.InfoTable(intervention),
            PartsFactory.Spacer(200),

            Sections.SectionHeader("2. Équipe terrain", Colors.PrimaryLight, Colors.Primary),
            PartsFactory.Spacer(60),
            Tables.TeamTable(intervention),
            PartsFactory.Spacer(200),

            Sections.SectionHeader("3. Travaux réalisés", Colors.PrimaryLight, Colors.Primary),
            PartsFactory.Spacer(60),
            Sections.BlockLabel("Description initiale :"),
            Tables.TextBlock(intervention.Description ?? "Aucune description"),
            PartsFactory.Spacer(100),
            Sections.BlockLabel("Compte-rendu technicien :"),
            Tables.TextBlock("COMPTE_RENDU"),
            PartsFactory.Spacer(200),

            Sections.SectionHeader("4. Pièces & matériaux consommés", Colors.GreenLight, Colors.Green),
            PartsFactory.Spacer(60),
            Tables.StockTable(),
            PartsFactory.Spacer(200),

            Sections.SectionHeader("5. Preuves terrain — photos", Colors.GreenLight, Colors.Green),
            PartsFactory.Spacer(60),
            Sections.PhotosIntro(),
            BuildPhotosTable(),
            PartsFactory.Spacer(200),

            Sections.SectionHeader("6. Signatures et validation", Colors.GrayLight, Colors.TextPrimary),
            PartsFactory.Spacer(100),
            Tables.SignatureTable(),
            PartsFactory.Spacer(200),

            Sections.SectionHeader("7. Historique des statuts", Colors.GrayLight, Colors.TextPrimary),
            PartsFactory.Spacer(60),
            Tables.HistoryTable(),
            PartsFactory.Spacer(160),

            new Paragraph(
                new ParagraphProperties(
                    new ParagraphBorders(new TopBorder { Val = BorderValues.Single, Size = 4, Color = Colors.BorderLight }),
                    new SpacingBetweenLines { Before = "100" }
                ),
                new Run(Styles.ArialRun(16, Colors.TextSecondary),
                    new Text("Document généré automatiquement par FieldOps Africa — ") { Space = SpaceProcessingModeValues.Preserve }),
                new Run(Styles.ArialRun(16, Colors.TextSecondary),
                    new Text("{{NOM_SOCIETE}}") { Space = SpaceProcessingModeValues.Preserve }),
                new Run(Styles.ArialRun(16, Colors.TextSecondary),
                    new Text(" — Réf. : {{REFERENCE}}") { Space = SpaceProcessingModeValues.Preserve })
            )
        };

            return elements;
        }

        static Table BuildPhotosTable()
        {
            return PartsFactory.TableFixed(Tables.ColFull,
                PartsFactory.Row(
                    PhotoCell("[ PHOTO AVANT ]", "PHOTO_1_HORODATAGE", Colors.Primary, Colors.PrimaryLight, 2980),
                    PartsFactory.EmptyCell(80),
                    PhotoCell("[ PHOTO PENDANT ]", "PHOTO_2_HORODATAGE", Colors.TextSecondary, Colors.GrayLight, 2980),
                    PartsFactory.EmptyCell(80),
                    PhotoCell("[ PHOTO APRÈS ]", "PHOTO_3_HORODATAGE", Colors.Green, Colors.GreenLight, 2940)
                )
            );
        }

        static TableCell PhotoCell(string title, string horodatagePh, string titleColor, string fill, int width)
        {
            return PartsFactory.Cell(new OpenXmlElement[]
            {
            PartsFactory.TextParagraph(title, 18, titleColor, false, true, "Center"),
            new Paragraph(
                new ParagraphProperties(new Justification { Val = JustificationValues.Center, }, new SpacingBetweenLines { Before = "60" }),
                new Run(Styles.ArialRun(16, Colors.TextSecondary),
                    new Text($"{{{{{horodatagePh}}}}}") { Space = SpaceProcessingModeValues.Preserve })
            )
            }, width, fill);
        }

        static void AddHeader(MainDocumentPart main, Intervention intervention)
        {
            var headerPart = main.AddNewPart<HeaderPart>();
            headerPart.Header = HeaderFooterBuilder.BuildHeader(intervention);

            var sectProps = main.Document.Body?.Elements<SectionProperties>().FirstOrDefault();
            if (sectProps == null) return;

            sectProps.RemoveAllChildren<HeaderReference>();
            sectProps.PrependChild(new HeaderReference { Type = HeaderFooterValues.Default, Id = main.GetIdOfPart(headerPart) });
        }

        static void AddFooter(MainDocumentPart main, Intervention intervention)
        {
            var footerPart = main.AddNewPart<FooterPart>();
            footerPart.Footer = HeaderFooterBuilder.BuildFooter(intervention);

            var sectProps = main.Document.Body?.Elements<SectionProperties>().FirstOrDefault();
            if (sectProps == null) return;

            sectProps.RemoveAllChildren<FooterReference>();
            sectProps.PrependChild(new FooterReference { Type = HeaderFooterValues.Default, Id = main.GetIdOfPart(footerPart) });
        }

        //static void AddStyles(MainDocumentPart main)
        //{
        //    var stylesPart = main.AddNewPart<StyleDefinitionsPart>();
        //    stylesPart.Styles = new DocumentFormat.OpenXml.Spreadsheet.Stylesheet(
        //        new Fonts(
        //            new Font(new DocumentFormat.OpenXml.Spreadsheet.FontName { Val = "Arial" }),
        //            new Font(new DocumentFormat.OpenXml.Spreadsheet.FontName { Val = "Arial" })
        //    ),
        //        new FontSize { Val = "20" },
        //        new DocumentFormat.OpenXml.Spreadsheet.Fills(),
        //        new DocumentFormat.OpenXml.Spreadsheet.Borders(),
        //        new DocumentFormat.OpenXml.Spreadsheet.CellStyleFormats(),
        //        new DocumentFormat.OpenXml.Spreadsheet.CellFormats(),
        //        new ParagraphStyles(
        //            new ParagraphStyle
        //            {
        //                StyleId = "Normal",
        //                Default = true,
        //                StyleName = new StyleName { Val = "Normal" }
        //            }
        //        )
        //    );
        //}
        static void AddStyles(MainDocumentPart main)
        {
            var stylesPart = main.AddNewPart<StyleDefinitionsPart>();

            stylesPart.Styles = new DocumentFormat.OpenXml.Wordprocessing.Styles(
                new DocDefaults(
                    new RunPropertiesDefault(
                        new RunPropertiesBaseStyle(
                            new RunFonts { Ascii = "Arial", HighAnsi = "Arial", EastAsia = "Arial", ComplexScript = "Arial" },
                            new FontSize { Val = "20" },
                            new Color { Val = Colors.TextPrimary }
                        )
                    ),
                    new ParagraphPropertiesDefault(
                        new ParagraphPropertiesBaseStyle(
                            new SpacingBetweenLines { Before = "0", After = "0" }
                        )
                    )
                ),

                new Style
                {
                    Type = StyleValues.Paragraph,
                    StyleId = "Normal",
                    Default = true,
                    StyleName = new StyleName { Val = "Normal" }
                },

                new Style
                {
                    Type = StyleValues.Paragraph,
                    StyleId = "Heading1",
                    StyleName = new StyleName { Val = "Heading 1" },
                    BasedOn = new BasedOn { Val = "Normal" },
                    NextParagraphStyle = new NextParagraphStyle { Val = "Normal" },
                    UIPriority = new UIPriority { Val = 9 },
                    PrimaryStyle = new PrimaryStyle(),
                    StyleRunProperties = new StyleRunProperties(
                        new RunFonts { Ascii = "Arial", HighAnsi = "Arial", EastAsia = "Arial", ComplexScript = "Arial" },
                        new FontSize { Val = "32" },
                        new Bold(),
                        new Color { Val = Colors.White }
                    ),
                    StyleParagraphProperties = new StyleParagraphProperties(
                        new SpacingBetweenLines { Before = "0", After = "120" },
                        new OutlineLevel { Val = 0 }
                    )
                },

                new Style
                {
                    Type = StyleValues.Paragraph,
                    StyleId = "Heading2",
                    StyleName = new StyleName { Val = "Heading 2" },
                    BasedOn = new BasedOn { Val = "Normal" },
                    NextParagraphStyle = new NextParagraphStyle { Val = "Normal" },
                    UIPriority = new UIPriority { Val = 10 },
                    PrimaryStyle = new PrimaryStyle(),
                    StyleRunProperties = new StyleRunProperties(
                        new RunFonts { Ascii = "Arial", HighAnsi = "Arial", EastAsia = "Arial", ComplexScript = "Arial" },
                        new FontSize { Val = "24" },
                        new Bold(),
                        new Color { Val = Colors.Primary }
                    ),
                    StyleParagraphProperties = new StyleParagraphProperties(
                        new SpacingBetweenLines { Before = "180", After = "80" },
                        new OutlineLevel { Val = 1 }
                    )
                }
            );
        }

        static SectionProperties SectionProperties()
        {
            return new SectionProperties(
                new PageSize { Width = 11906U, Height = 16838U },
                new PageMargin { Top = 720, Right = 900U, Bottom = 720, Left = 900U, Header = 450U, Footer = 450U, Gutter = 0U }
            );
        }
    }
}
