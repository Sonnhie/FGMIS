using FGScanner.DTOs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;

namespace FGScanner.Services
{
    public sealed class CheckTagPrintService
    {
        private const int ColumnsPerPage = 2;
        private const int RowsPerPage = 3;
        private const int TagsPerPage = ColumnsPerPage * RowsPerPage;
        private const int CellGap = 8;

        private int _tagIndex;

        public void Reset() => _tagIndex = 0;

        public void PrintPage(IReadOnlyList<InventoryCheckTagData> tags, PrintPageEventArgs e)
        {
            if (tags == null || _tagIndex >= tags.Count)
            {
                e.HasMorePages = false;
                return;
            }

            int cellWidth = (e.MarginBounds.Width - CellGap) / ColumnsPerPage;
            int cellHeight = (e.MarginBounds.Height - (CellGap * 2)) / RowsPerPage;

            for (int slot = 0; slot < TagsPerPage && _tagIndex < tags.Count; slot++)
            {
                int column = slot % ColumnsPerPage;
                int row = slot / ColumnsPerPage;
                var bounds = new Rectangle(
                    e.MarginBounds.Left + column * (cellWidth + CellGap),
                    e.MarginBounds.Top + row * (cellHeight + CellGap),
                    cellWidth,
                    cellHeight);

                DrawTag(e.Graphics, bounds, tags[_tagIndex]);
                _tagIndex++;
            }

            e.HasMorePages = _tagIndex < tags.Count;
        }

        private static void DrawTag(Graphics graphics, Rectangle bounds, InventoryCheckTagData tag)
        {
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            using Pen borderPen = new(Color.Black, 1.2F);
            using Pen linePen = new(Color.Black, 0.75F);
            using Font companyFont = new("Arial", 6.5F, FontStyle.Regular);
            using Font titleFont = new("Arial", 11F, FontStyle.Bold);
            using Font tagFont = new("Arial", 12F, FontStyle.Bold);
            using Font labelFont = new("Arial", 6.5F, FontStyle.Regular);
            using Font valueFont = new("Arial", 8F, FontStyle.Bold);
            using Font tableFont = new("Arial", 7F, FontStyle.Regular);
            using Font smallFont = new("Arial", 5.5F, FontStyle.Regular);
            using Brush blackBrush = new SolidBrush(Color.Black);
            using Brush redBrush = new SolidBrush(Color.FromArgb(210, 45, 35));
            using StringFormat center = new()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center,
                Trimming = StringTrimming.EllipsisCharacter,
                FormatFlags = StringFormatFlags.NoWrap
            };
            using StringFormat left = new()
            {
                Alignment = StringAlignment.Near,
                LineAlignment = StringAlignment.Center,
                Trimming = StringTrimming.EllipsisCharacter,
                FormatFlags = StringFormatFlags.NoWrap
            };

            int x = bounds.Left + 3;
            int y = bounds.Top + 3;
            int width = bounds.Width - 6;
            int availableHeight = bounds.Height - 6;

            graphics.DrawRectangle(borderPen, x, y, width - 1, availableHeight - 1);

            graphics.DrawString(
                "NIDEC INSTRUMENTS (PHILIPPINES) CORPORATION",
                companyFont,
                blackBrush,
                new RectangleF(x + 4, y + 1, width - 8, 13),
                left);
            y += 13;

            const int titleHeight = 24;
            graphics.DrawString("INVENTORY CHECK TAG", titleFont, blackBrush,
                new RectangleF(x + 3, y, width * 0.64F, titleHeight), left);
            graphics.DrawString(tag.TagNumber, tagFont, redBrush,
                new RectangleF(x + width * 0.64F, y, width * 0.35F - 3, titleHeight), center);
            y += titleHeight;

            const int detailRowHeight = 25;
            DrawTwoColumnRow(graphics, borderPen, linePen, labelFont, valueFont, blackBrush, left, center,
                x, y, width, detailRowHeight,
                "ERP Location:", tag.ErpLocation,
                "Date:", tag.CutoffDate.ToString("MM-dd-yy"));
            y += detailRowHeight;

            DrawTwoColumnRow(graphics, borderPen, linePen, labelFont, valueFont, blackBrush, left, center,
                x, y, width, detailRowHeight,
                "Location Code:", tag.RackLocation,
                "Description:", tag.LocationDescription);
            y += detailRowHeight;

            DrawSingleRow(graphics, borderPen, linePen, labelFont, valueFont, blackBrush, left, center,
                x, y, width, detailRowHeight, "Material Code:", tag.MaterialCode, 0.27F);
            y += detailRowHeight;

            DrawMaterialNameRow(graphics, borderPen, linePen, labelFont, valueFont, blackBrush, left, center,
                x, y, width, detailRowHeight, tag.MaterialName, tag.Unit);
            y += detailRowHeight;

            const int classHeight = 30;
            graphics.DrawRectangle(borderPen, x, y, width, classHeight);
            graphics.DrawString("Class", labelFont, blackBrush,
                new RectangleF(x + 3, y, width * 0.16F, classHeight), left);

            string[] classLabels = { "Good", "Hold", "For Sorting" };
            for (int index = 0; index < classLabels.Length; index++)
            {
                RectangleF area = new(
                    x + width * (0.15F + index * 0.28F),
                    y,
                    width * 0.28F,
                    classHeight);
                var numberArea = new RectangleF(area.X + 2, area.Y, 18, area.Height);
                graphics.DrawString((index + 1).ToString(), tableFont, blackBrush, numberArea, center);
                graphics.DrawString(classLabels[index], tableFont, blackBrush,
                    new RectangleF(area.X + 20, area.Y, area.Width - 20, area.Height), center);
            }
            y += classHeight + 3;

            const int tableHeaderHeight = 20;
            const int tableRowHeight = 20;
            const int tableRows = 4;
            const int totalHeight = 22;
            float firstColumn = width * 0.34F;
            float secondColumn = width * 0.67F;
            int tableHeight = tableHeaderHeight + tableRows * tableRowHeight + totalHeight;

            graphics.DrawRectangle(borderPen, x, y, width, tableHeight);
            graphics.DrawString("Qty", tableFont, blackBrush,
                new RectangleF(x, y, firstColumn, tableHeaderHeight), center);
            graphics.DrawString("Qty of Boxes", tableFont, blackBrush,
                new RectangleF(x + firstColumn, y, secondColumn - firstColumn, tableHeaderHeight), center);
            graphics.DrawString("Total Qty", tableFont, blackBrush,
                new RectangleF(x + secondColumn, y, width - secondColumn, tableHeaderHeight), center);

            graphics.DrawLine(linePen, x, y + tableHeaderHeight, x + width, y + tableHeaderHeight);
            graphics.DrawLine(linePen, x + firstColumn, y, x + firstColumn, y + tableHeight);
            graphics.DrawLine(linePen, x + secondColumn, y, x + secondColumn, y + tableHeight);

            for (int row = 0; row < tableRows; row++)
            {
                int rowY = y + tableHeaderHeight + row * tableRowHeight;
                graphics.DrawLine(linePen, x, rowY + tableRowHeight, x + width, rowY + tableRowHeight);

                if (row == 0 && tag.PrefillSystemCount)
                {
                    int boxes = Math.Max(tag.SystemBoxes, 1);
                    decimal perBox = Math.Round((decimal)tag.SystemQuantity / boxes, 2);
                    string quantityText = perBox == decimal.Truncate(perBox)
                        ? decimal.Truncate(perBox).ToString("N0")
                        : perBox.ToString("N2");

                    graphics.DrawString(quantityText, valueFont, blackBrush,
                        new RectangleF(x, rowY, firstColumn, tableRowHeight), center);
                    graphics.DrawString(boxes.ToString("N0"), valueFont, blackBrush,
                        new RectangleF(x + firstColumn, rowY, secondColumn - firstColumn, tableRowHeight), center);
                    graphics.DrawString(tag.SystemQuantity.ToString("N0"), valueFont, blackBrush,
                        new RectangleF(x + secondColumn, rowY, width - secondColumn, tableRowHeight), center);
                }
            }

            int totalY = y + tableHeaderHeight + tableRows * tableRowHeight;
            graphics.DrawString("Total", tableFont, blackBrush,
                new RectangleF(x + firstColumn, totalY, secondColumn - firstColumn, totalHeight), left);
            if (tag.PrefillSystemCount)
            {
                graphics.DrawString(tag.SystemQuantity.ToString("N0"), valueFont, blackBrush,
                    new RectangleF(x + secondColumn, totalY, width - secondColumn, totalHeight), center);
            }
            y += tableHeight;

            string auditReference = string.IsNullOrWhiteSpace(tag.BatchCode)
                ? "SF-28-AC003 (Rev. 00)"
                : $"SF-28-AC003 (Rev. 00) | {tag.BatchCode}";
            graphics.DrawString(auditReference, smallFont, blackBrush,
                new RectangleF(x + 3, y, width - 6, 12), left);
            y += 11;

            const int signatureHeight = 35;
            float signatureWidth = width / 3F;
            string[] counters = { "FIRST COUNTER", "SECOND COUNTER", "THIRD COUNTER" };
            for (int index = 0; index < counters.Length; index++)
            {
                float signatureX = x + index * signatureWidth;
                graphics.DrawLine(linePen,
                    signatureX + 6,
                    y + 18,
                    signatureX + signatureWidth - 6,
                    y + 18);
                graphics.DrawString(counters[index], smallFont, blackBrush,
                    new RectangleF(signatureX, y + 18, signatureWidth, 12), center);
            }

            graphics.DrawString($"By: {tag.GeneratedBy}", smallFont, blackBrush,
                new RectangleF(x + 3, y + signatureHeight - 7, width - 6, 9), left);
        }

        private static void DrawTwoColumnRow(
            Graphics graphics, Pen borderPen, Pen linePen, Font labelFont, Font valueFont,
            Brush brush, StringFormat left, StringFormat center,
            int x, int y, int width, int height,
            string leftLabel, string leftValue, string rightLabel, string rightValue)
        {
            int middle = x + width / 2;
            int labelWidth = (int)(width * 0.22F);
            graphics.DrawRectangle(borderPen, x, y, width, height);
            graphics.DrawLine(linePen, middle, y, middle, y + height);
            graphics.DrawString(leftLabel, labelFont, brush,
                new RectangleF(x + 3, y, labelWidth - 3, height), left);
            graphics.DrawString(leftValue ?? string.Empty, valueFont, brush,
                new RectangleF(x + labelWidth, y, middle - x - labelWidth, height), center);
            graphics.DrawString(rightLabel, labelFont, brush,
                new RectangleF(middle + 3, y, labelWidth - 3, height), left);
            graphics.DrawString(rightValue ?? string.Empty, valueFont, brush,
                new RectangleF(middle + labelWidth, y, x + width - middle - labelWidth, height), center);
        }

        private static void DrawSingleRow(
            Graphics graphics, Pen borderPen, Pen linePen, Font labelFont, Font valueFont,
            Brush brush, StringFormat left, StringFormat center,
            int x, int y, int width, int height, string label, string value, float labelRatio)
        {
            int labelEnd = x + (int)(width * labelRatio);
            graphics.DrawRectangle(borderPen, x, y, width, height);
            graphics.DrawLine(linePen, labelEnd, y, labelEnd, y + height);
            graphics.DrawString(label, labelFont, brush,
                new RectangleF(x + 3, y, labelEnd - x - 3, height), left);
            graphics.DrawString(value ?? string.Empty, valueFont, brush,
                new RectangleF(labelEnd, y, x + width - labelEnd, height), center);
        }

        private static void DrawMaterialNameRow(
            Graphics graphics, Pen borderPen, Pen linePen, Font labelFont, Font valueFont,
            Brush brush, StringFormat left, StringFormat center,
            int x, int y, int width, int height, string materialName, string unit)
        {
            int labelEnd = x + (int)(width * 0.27F);
            int unitStart = x + (int)(width * 0.82F);
            graphics.DrawRectangle(borderPen, x, y, width, height);
            graphics.DrawLine(linePen, labelEnd, y, labelEnd, y + height);
            graphics.DrawLine(linePen, unitStart, y, unitStart, y + height);
            graphics.DrawString("Material Name:", labelFont, brush,
                new RectangleF(x + 3, y, labelEnd - x - 3, height), left);
            graphics.DrawString(materialName ?? string.Empty, valueFont, brush,
                new RectangleF(labelEnd, y, unitStart - labelEnd, height), center);
            graphics.DrawString($"Unit {unit}", labelFont, brush,
                new RectangleF(unitStart, y, x + width - unitStart, height), center);
        }
    }
}
