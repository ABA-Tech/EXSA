using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;

namespace Application.Documents
{
    public static class PartsFactory
    {
        private static JustificationValues GetJustification(string value)
        {
            switch (value.ToLower())
            {
                case "left":
                    return JustificationValues.Left;
                case "right":
                    return JustificationValues.Right;
                case "center":
                    return JustificationValues.Center;
                default:
                    return JustificationValues.Left;
            }
        }
        public static Paragraph TextParagraph(string text, int size, string color, bool bold = false, bool italic = false,
            string justification = "Left", int before = 0, int after = 0)
        {
            JustificationValues justifications = JustificationValues.Left;
            return new Paragraph(
                Styles.ParagraphProps(GetJustification(justification), before, after),
                new Run(Styles.ArialRun(size, color, bold, italic), new Text(text) { Space = SpaceProcessingModeValues.Preserve })
            );
        }

        public static Paragraph PlaceholderParagraph(string key, int size = 20, string color = null, bool bold = true,
            string justification = "Left", int before = 0, int after = 0)
        {
            color ??= Colors.Primary;
            return new Paragraph(
                Styles.ParagraphProps(GetJustification(justification), before, after),
                new Run(Styles.ArialRun(size, color, bold), new Text($"{key}") { Space = SpaceProcessingModeValues.Preserve })
            );
        }

        public static TableCell Cell(IEnumerable<OpenXmlElement> children, int width, string? fill = null,
            bool noBorder = false, bool center = false, int top = 80, int bottom = 80, int left = 120, int right = 120)
        {
            var props = new TableCellProperties(
                Styles.WidthDxa(width),
                noBorder ? Styles.NoBorders() : Styles.DefaultCellBorders(),
                Styles.StandardCellMargins(top, bottom, left, right)
            );
            if (fill != null) props.Append(Styles.Fill(fill));
            if (center) props.Append(new TableCellVerticalAlignment { Val = TableVerticalAlignmentValues.Center });
            //return new TableCell(props, children.ToArray());
            var table = new TableCell(children.ToArray());
            table.TableCellProperties = props;
            return table;
        }

        public static TableCell CellText(string text, int width, string color, string? fill = null, bool bold = false,
            string justification = "Left", bool noBorder = false)
        {
            return Cell(new[]
            {
            TextParagraph(text, 18, color, bold, false, justification)
        }, width, fill, noBorder);
        }

        public static TableCell CellPlaceholder(string key, int width, string? fill = null, string color = null,
            bool bold = true, bool noBorder = false)
        {
            color ??= Colors.Primary;
            return Cell(new[]
            {
            PlaceholderParagraph(key, 18, color, bold)
        }, width, fill, noBorder);
        }

        public static Paragraph Spacer(int after = 80) => new Paragraph(new ParagraphProperties(new SpacingBetweenLines { After = after.ToString() }));

        public static Table TableFixed(int width, params TableRow[] rows)
        {

            var table = new Table(rows);
            table.TableProperties = new TableProperties(
                    Styles.WidthDxa(width),
                    Styles.FixedLayout()
                );
            //return new Table(
            //    new TableProperties(
            //        Styles.WidthDxa(width),
            //        Styles.FixedLayout()
            //    ),
            //    rows
            //);

            return table;
        }

        public static TableRow Row(params TableCell[] cells) => new TableRow(cells);

        public static TableCell EmptyCell(int width, bool noBorder = true)
            => Cell(new[] { new Paragraph() }, width, null, noBorder);
    }
}
