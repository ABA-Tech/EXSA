using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Documents
{
    public static class Styles
    {
        public static RunProperties ArialRun(int size, string color, bool bold = false, bool italic = false)
            => new RunProperties(
                new RunFonts { Ascii = "Arial", HighAnsi = "Arial", EastAsia = "Arial", ComplexScript = "Arial" },
                new FontSize { Val = (size).ToString() },
                new Color { Val = color },
                bold ? new Bold() : null,
                italic ? new Italic() : null
            );

        public static ParagraphProperties ParagraphProps(JustificationValues? justification = null, int? before = null, int? after = null)
        {
            var p = new ParagraphProperties();
            if (justification != null) p.Justification = new Justification { Val = justification.Value };
            if (before != null || after != null)
                p.SpacingBetweenLines = new SpacingBetweenLines
                {
                    Before = before?.ToString(),
                    After = after?.ToString()
                };
            return p;
        }

        public static TableCellBorders DefaultCellBorders() => new TableCellBorders(
            new TopBorder { Val = BorderValues.Single, Size = 4, Color = Colors.BorderLight },
            new LeftBorder { Val = BorderValues.Single, Size = 4, Color = Colors.BorderLight },
            new BottomBorder { Val = BorderValues.Single, Size = 4, Color = Colors.BorderLight },
            new RightBorder { Val = BorderValues.Single, Size = 4, Color = Colors.BorderLight }
        );

        public static TableCellMargin StandardCellMargins(int top = 80, int bottom = 80, int left = 120, int right = 120)
            => new TableCellMargin(
                new TopMargin { Width = top.ToString(), Type = TableWidthUnitValues.Dxa },
                new BottomMargin { Width = bottom.ToString(), Type = TableWidthUnitValues.Dxa },
                new LeftMargin { Width = left.ToString(), Type = TableWidthUnitValues.Dxa },
                new RightMargin { Width = right.ToString(), Type = TableWidthUnitValues.Dxa }
            );

        public static Shading Fill(string color) => new Shading
        {
            Val = ShadingPatternValues.Clear,
            Color = "auto",
            Fill = color
        };

        public static TableWidth WidthDxa(int dxa) => new TableWidth { Width = dxa.ToString(), Type = TableWidthUnitValues.Dxa };

        public static TableLayout FixedLayout() => new TableLayout { Type = TableLayoutValues.Fixed };

        public static TableBorders NoBorders() => new TableBorders(
            new TopBorder { Val = BorderValues.Nil, Size = 0 },
            new LeftBorder { Val = BorderValues.Nil, Size = 0 },
            new BottomBorder { Val = BorderValues.Nil, Size = 0 },
            new RightBorder { Val = BorderValues.Nil, Size = 0 },
            new InsideHorizontalBorder { Val = BorderValues.Nil, Size = 0 },
            new InsideVerticalBorder { Val = BorderValues.Nil, Size = 0 }
        );
    }
}
