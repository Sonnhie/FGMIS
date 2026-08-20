using FGScanner.Models;
using FGScanner.Repositories;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FGScanner.Services
{
    public class PrintService
    {
        private readonly Queries _queries;
        private List<InventoryCardData> _cardsToPrint;
        private int _transferRowIndex = 0;
        private int _currentRowIndex = 0;
        private int _currentCardIndex = 0;

        public PrintService(Queries queries)
        {
            _queries = queries;
        }

        public void Reset()
        {
            _currentCardIndex = 0;
            _currentRowIndex = 0;
        }
        private int DrawTransferSlip(Graphics g, int width, int height, int startX, int startY, PrintDocumentDTO data, string label)
        {
            // 1. EXECUTE THE LINQ QUERY AND GET THE SINGLE DOCUMENT
          

            // Safety check: if no data was passed, stop drawing
            if (data == null) return startY;

            string docnumber = data.DocNo;

            //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // --- PENS & FONTS ---
            Pen pen = new Pen(Color.Black, 1);
            Pen dashedPen = new Pen(Color.Black, 1) { DashPattern = new float[] { 4, 4 } };
            Font titleFont = new Font("Arial", 22, FontStyle.Bold);
            Font headerFont = new Font("Arial", 9, FontStyle.Bold);
            Font bodyFont = new Font("Arial", 10, FontStyle.Regular);
            Font smallFont = new Font("Arial", 8, FontStyle.Regular);
            Font labelFont = new Font("Arial", 10, FontStyle.Italic | FontStyle.Bold);

            StringFormat centerFmt = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            StringFormat leftFmt = new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center };
            StringFormat rightFmt = new StringFormat { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center };

            int y = startY;
            int docNoWidth = (int)(width * 0.15f);

            // --- 0. DRAW THE LABEL ---
            g.DrawString(label, labelFont, Brushes.DimGray, new Rectangle(startX, y - 15, width, 15), rightFmt);

            // --- 1. TITLE & DOCUMENT NO ROW ---
            int titleH = 50;
            g.DrawRectangle(pen, startX, y, width, titleH);
            g.DrawString("TRANSFER SLIP", titleFont, Brushes.Black, new Rectangle(startX, y, width - docNoWidth, titleH), centerFmt);
            g.DrawLine(pen, startX + width - docNoWidth, y, startX + width - docNoWidth, y + titleH + 40);
            g.DrawString("Document No.", headerFont, Brushes.Black, startX + width - docNoWidth + 5, y + 5);
            g.DrawString(docnumber, bodyFont, Brushes.Black, new Rectangle(startX + width - docNoWidth, y + 15, docNoWidth, titleH - 15), centerFmt);
            y += titleH;

            // --- 2. SHIFT ROW ---
            int shiftH = 30;
            g.DrawRectangle(pen, startX, y, width, shiftH);
            g.DrawString("Shift:", headerFont, Brushes.Black, startX + 5, y + 7);
            g.DrawLine(pen, startX + 60, y, startX + 60, y + shiftH);
            g.DrawLine(pen, startX + 100, y, startX + 100, y + shiftH);

            // NOTE: You will need to pass the Shift string into this method or pull it from the DB!
            string shiftValue = "";
            g.DrawString(shiftValue, bodyFont, Brushes.Black, new Rectangle(startX + 60, y, 40, shiftH), centerFmt);
            y += shiftH;

            // --- 3. ISSUE DATE & PERSONNEL ROW ---
            int persH = 50;
            g.DrawRectangle(pen, startX, y, width, persH);
            float[] topCols = { width * 0.15f, width * 0.25f, width * 0.15f, width * 0.15f, width * 0.15f, width * 0.15f };
            float cx = startX;

            string[] topLabels = { "Issue Date", "Location", "Prepared by:", "Checked by:", "Received by:", "Encoded by:" };

            // Updated to pull from our LINQ document!
            string[] topValues = {
        data.EntryDate.ToString("yyyy-MM-dd"), // Issue Date
        "", // Location
        data.PreparedBy, // Prepared by
        "", // Checked By (Fill this in if you have it!)
        "", // Received By (Fill this in if you have it!)
        ""  // Encoded By
    };

            for (int i = 0; i < topCols.Length; i++)
            {
                if (i > 0) g.DrawLine(pen, cx, y, cx, y + persH);
                if (i == 1)
                {
                    // 1. "Location" Header (Top 15 pixels, full width)
                    g.DrawString(topLabels[i], headerFont, Brushes.Black, new RectangleF(cx, y, topCols[i], 15), centerFmt);
                    g.DrawLine(pen, cx, y + 15, cx + topCols[i], y + 15);

                    // Calculate the halfway point for the vertical split
                    float halfLoc = topCols[i] / 2f;

                    // Draw the vertical line splitting "From" and "To" all the way to the bottom
                    g.DrawLine(pen, cx + halfLoc, y + 15, cx + halfLoc, y + persH);

                    // 2. "From" and "To" Sub-headers (Next 15 pixels)
                    // Using smallFont here so it doesn't take up too much room
                    g.DrawString("From", smallFont, Brushes.Black, new RectangleF(cx, y + 15, halfLoc, 15), centerFmt);
                    g.DrawString("To", smallFont, Brushes.Black, new RectangleF(cx + halfLoc, y + 15, halfLoc, 15), centerFmt);

                    // Draw a line under "From" and "To"
                    g.DrawLine(pen, cx, y + 30, cx + topCols[i], y + 30);

                    // 3. THE DATA AREA (The remaining height of the box)
                    // (You will need to pass these values in from your currentDocument DTO!)
                    string fromValue = data.FromLocation;
                    string toValue = data.ToLocation;

                    g.DrawString(fromValue, bodyFont, Brushes.Black, new RectangleF(cx, y + 30, halfLoc, persH - 30), centerFmt);
                    g.DrawString(toValue, bodyFont, Brushes.Black, new RectangleF(cx + halfLoc, y + 30, halfLoc, persH - 30), centerFmt);
                }
                else
                {
                    g.DrawString(topLabels[i], headerFont, Brushes.Black, new RectangleF(cx + 2, y + 2, topCols[i], 20), leftFmt);
                    g.DrawString(topValues[i], bodyFont, Brushes.Black, new RectangleF(cx, y + 20, topCols[i], persH - 20), centerFmt);
                }
                cx += topCols[i];
            }
            y += persH;

            // --- 4. MAIN TABLE HEADERS ---
            int headH = 50;
            g.DrawRectangle(pen, startX, y, width, headH);
            float[] colW = { width * 0.15f, width * 0.21f, width * 0.05f, width * 0.10f, width * 0.10f, width * 0.19f, width * 0.20f };
            string[] headers = { "Material Code", "Material Name", "Rev.\nNo", "Inspection\nDate", "Production\nDate", "Quantity", "Remarks" };

            cx = startX;
            for (int i = 0; i < colW.Length; i++)
            {
                if (i > 0) g.DrawLine(pen, cx, y, cx, y + headH);
                if (i == 5)
                {
                    g.DrawString(headers[i], headerFont, Brushes.Black, new RectangleF(cx, y, colW[i], headH / 2), centerFmt);
                    g.DrawLine(pen, cx, y + headH / 2, cx + colW[i], y + headH / 2);
                    float qW = colW[i] / 3;
                    g.DrawLine(pen, cx + qW, y + headH / 2, cx + qW, y + headH);
                    g.DrawLine(pen, cx + (qW * 2), y + headH / 2, cx + (qW * 2), y + headH);
                    g.DrawString("No.Box", smallFont, Brushes.Black, new RectangleF(cx, y + headH / 2, qW, headH / 2), centerFmt);
                    g.DrawString("PPS", smallFont, Brushes.Black, new RectangleF(cx + qW, y + headH / 2, qW, headH / 2), centerFmt);
                    g.DrawString("Pcs", smallFont, Brushes.Black, new RectangleF(cx + (qW * 2), y + headH / 2, qW, headH / 2), centerFmt);
                }
                else
                {
                    g.DrawString(headers[i], headerFont, Brushes.Black, new RectangleF(cx, y, colW[i], headH), centerFmt);
                }
                cx += colW[i];
            }
            y += headH;

            // --- 5. MAIN TABLE DATA ROWS ---
            int rowH = 40;
            int numRowsPerPage = 8;
            float[] dataColW = { colW[0], colW[1], colW[2], colW[3], colW[4], colW[5] / 3, colW[5] / 3, colW[5] / 3, colW[6] };

            for (int r = 0; r < numRowsPerPage; r++)
            {
                int currentRowIndex = _transferRowIndex + r;
                cx = startX;

                for (int c = 0; c < dataColW.Length; c++)
                {
                    if (c > 0) g.DrawLine(pen, cx, y, cx, y + rowH);

                    // FIXED: We check currentDocument.Items.Count instead of data.Rows.Count
                    if (data.Items != null && currentRowIndex < data.Items.Count)
                    {
                        var item = data.Items[currentRowIndex];

                        // Map our LINQ item properties directly to the columns!
                        string[] rowData = {
                    item.PartNumber,                                // Col 0: Material Code
                    item.PartName.ToString(),                                  // Col 1: Material Name
                    "",                                             // Col 2: Rev No (Fill in if needed)
                    "",                                             // Col 3: Inspection Date
                    item.ProductionDate.ToString("yyyy-MM-dd"),     // Col 4: Production Date
                    item.Box.ToString(),                            // Col 5: Box
                    item.PPS.ToString(),                            // Col 6: PPS
                    item.Quantity.ToString(),                       // Col 7: Pcs (Qty)
                    item.remarks.ToString()                                              // Col 8: Remarks
                };

                        g.DrawString(rowData[c], bodyFont, Brushes.Black, new RectangleF(cx, y, dataColW[c], rowH), centerFmt);
                    }
                    cx += dataColW[c];
                }

                g.DrawLine(pen, startX, y + rowH, startX + width, y + rowH);
                g.DrawLine(pen, startX, y, startX, y + rowH);
                g.DrawLine(pen, startX + width, y, startX + width, y + rowH);
                y += rowH;
            }

            // --- 6. FOOTER ---
            g.DrawString("CF-140 (Rev.00)", smallFont, Brushes.Black, startX, y + 5);
            y += 25;
            g.DrawLine(dashedPen, startX, y, startX + width, y);

            return y + 25;
        }
        public void PrintTransferSlip(PrintDocumentDTO data, PrintPageEventArgs e)
        {
            try
            {
                if (data == null)
                {
                    return;
                }

                // 1. Set a tiny margin (e.g., 20 units instead of 50)
                int margin = 50;

                // 2. Dynamically calculate the width of the paper minus the left & right margins
                int printableWidth = e.PageBounds.Width - (margin * 2);
                int printableHeight = e.PageBounds.Height - (margin * 3);

                int middleOffPage = DrawTransferSlip(
                        e.Graphics,
                        printableWidth,
                        printableHeight,
                        margin,
                        margin,
                        data,
                        "WAREHOUSE COPY"
                );

                DrawTransferSlip(
                         e.Graphics,
                        printableWidth,
                        printableHeight,
                        margin,
                        middleOffPage + 20,
                        data,
                        "SECTION COPY"
                );

                _transferRowIndex += 8;

                if (_transferRowIndex < data.Items.Count)
                {
                    e.HasMorePages = true;
                }
                else
                {
                    e.HasMorePages = false;
                    _transferRowIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        public void ProcessPrintPage(List<InventoryCardData> cardsToPrint, PrintPageEventArgs e, string userid)
        {
            _cardsToPrint = cardsToPrint;

            if (_cardsToPrint == null || _cardsToPrint.Count == 0 || _currentCardIndex >= _cardsToPrint.Count)
            {
                e.HasMorePages = false;
                return;
            }

            InventoryCardData cardForThisPage = _cardsToPrint[_currentCardIndex];

            DrawInventoryCard(e.Graphics, e.MarginBounds.Width, e.MarginBounds.Height,
                              e.MarginBounds.Left, e.MarginBounds.Top, cardForThisPage, e, userid);

            // If the drawing method finished all rows for this card
            if (!e.HasMorePages)
            {
                _currentCardIndex++; // Move to next card

                // If we have more cards, keep printing
                if (_currentCardIndex < _cardsToPrint.Count)
                {
                    e.HasMorePages = true;
                }
            }
        }

        private void DrawInventoryCard(Graphics g, int width, int height, int startX, int startY, InventoryCardData data, PrintPageEventArgs e, string _userId)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // --- SETUP FONTS & PENS ---
            using Pen borderPen = new Pen(Color.Black, 1);
            using Pen linePen = new Pen(Color.Black, 1);
            using Pen dashedPen = new Pen(Color.Black, 1) { DashPattern = new float[] { 4, 4 } };

            using Font smallFont = new Font("Arial", 8);
            using Font bodyFont = new Font("Arial", 18, FontStyle.Bold);
            using Font headerFont = new Font("Arial", 24, FontStyle.Bold);
            using Font largeDataFont = new Font("Arial Narrow", 48, FontStyle.Bold);

            using Brush textBrush = new SolidBrush(Color.Black);

            using StringFormat centerFmt = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            using StringFormat rightFmt = new StringFormat { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center };
            using StringFormat leftFmt = new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center };

            int stubHeight = 80;
            int mainHeight = height - stubHeight - 40;
            int maxRowsPerPage = 8;
            int rowsDrawnOnThisPage = 0;

            g.DrawRectangle(borderPen, startX, startY, width, mainHeight);

            int currentY = startY;
            int rowH = 50;
            g.DrawString("Inventory Card", bodyFont, textBrush, new Rectangle(startX, currentY, width, rowH), centerFmt);
            currentY += rowH; g.DrawLine(borderPen, startX, currentY, startX + width, currentY);

            rowH = 50;
            g.DrawString(data.MonthYear, headerFont, textBrush, new Rectangle(startX, currentY, width, rowH), centerFmt);
            currentY += rowH; g.DrawLine(linePen, startX, currentY, startX + width, currentY);

            rowH = 80;
            int midX = startX + (int)(width * 0.34);
            int midX2 = startX + (int)(width * 0.50);
            int midX3 = startX + (int)(width * 0.75);
            g.DrawString("ERP Location:", bodyFont, textBrush, new Rectangle(startX + 5, currentY, midX - startX, rowH), leftFmt);
            g.DrawString(data.ErpLocation, headerFont, textBrush, new Rectangle(midX, currentY, midX2 - midX, rowH), centerFmt);
            g.DrawString("Prepared by:", bodyFont, textBrush, new Rectangle(midX2 + 5, currentY, midX3 - midX2, rowH), leftFmt);
            g.DrawString(data.PreparedBy, headerFont, textBrush, new Rectangle(midX3, currentY, (startX + width) - midX3, rowH), centerFmt);

            g.DrawLine(linePen, midX, currentY, midX, currentY + rowH);
            g.DrawLine(linePen, midX2, currentY, midX2, currentY + rowH);
            g.DrawLine(linePen, midX3, currentY, midX3, currentY + rowH);
            currentY += rowH; g.DrawLine(linePen, startX, currentY, startX + width, currentY);

            rowH = 80;
            g.DrawString("Control no.", bodyFont, textBrush, new Rectangle(startX + 5, currentY, midX - startX, rowH), leftFmt);
            g.DrawString($"{data.ControlNo} - ({data.location})", largeDataFont, textBrush, new Rectangle(midX, currentY, (startX + width) - midX, rowH), centerFmt);
            g.DrawLine(linePen, midX, currentY, midX, currentY + rowH * 2);
            currentY += rowH; g.DrawLine(linePen, startX, currentY, startX + width, currentY);

            g.DrawString("Part No.", bodyFont, textBrush, new Rectangle(startX + 5, currentY, midX - startX, rowH), leftFmt);
            g.DrawString(data.PartNo, largeDataFont, textBrush, new Rectangle(midX, currentY, (startX + width) - midX, rowH), centerFmt);
            currentY += rowH; g.DrawLine(linePen, startX, currentY, startX + width, currentY);

            rowH = 70;
            float[] colW = { width * 0.34f, width * 0.16f, width * 0.22f, width * 0.28f };
            string[] cols = { "Lot No.", "No.\nof Boxes", "Quantity", "Total Qty." };
            float cx = startX;
            for (int i = 0; i < cols.Length; i++)
            {
                g.DrawString(cols[i], bodyFont, textBrush, new RectangleF(cx, currentY, colW[i], rowH), centerFmt);
                cx += colW[i];
                if (i < cols.Length - 1) g.DrawLine(linePen, cx, currentY, cx, startY + mainHeight - rowH);
            }
            currentY += rowH; g.DrawLine(linePen, startX, currentY, startX + width, currentY);

            while (_currentRowIndex < data.Rows.Count && rowsDrawnOnThisPage < maxRowsPerPage)
            {
                var row = data.Rows[_currentRowIndex];
                cx = startX;
                g.DrawString(row.LotNo, largeDataFont, textBrush, new RectangleF(cx, currentY, colW[0], rowH), centerFmt); cx += colW[0];
                g.DrawString(row.Boxes.ToString(), largeDataFont, textBrush, new RectangleF(cx, currentY, colW[1], rowH), centerFmt); cx += colW[1];
                g.DrawString(row.Quantity.ToString(), largeDataFont, textBrush, new RectangleF(cx, currentY, colW[2], rowH), centerFmt); cx += colW[2];
                g.DrawString(row.TotalQty.ToString("N0"), largeDataFont, textBrush, new RectangleF(cx, currentY, colW[3], rowH), centerFmt);

                currentY += rowH;
                g.DrawLine(linePen, startX, currentY, startX + width, currentY);

                _currentRowIndex++;
                rowsDrawnOnThisPage++;
            }

            bool isLastPage = (_currentRowIndex >= data.Rows.Count);
            if (isLastPage)
            {
                while (rowsDrawnOnThisPage < maxRowsPerPage)
                {
                    currentY += rowH;
                    g.DrawLine(linePen, startX, currentY, startX + width, currentY);
                    rowsDrawnOnThisPage++;
                }

                g.DrawString("Grand Total", headerFont, textBrush, new Rectangle(startX + 5, currentY, (int)colW[0], rowH), leftFmt);
                g.DrawString(data.GrandTotalBoxes.ToString(), largeDataFont, textBrush, new RectangleF(startX + colW[0], currentY, colW[1], rowH), centerFmt);
                g.DrawString(data.GrandTotalQuantity.ToString("N0"), largeDataFont, textBrush, new RectangleF(startX + colW[0] + colW[1] + colW[2], currentY, colW[3], rowH), centerFmt);
            }

            int footerY = startY + mainHeight + 2;
            g.DrawString("CF-260(Rev. 00)", smallFont, textBrush, startX, footerY);
            string pageInfo = isLastPage ? "" : "(Continued on next page...)";
            g.DrawString($"{pageInfo}  {DateTime.Now:dd/MM/yyyy HH:mm}", smallFont, textBrush, new Rectangle(startX, footerY, width, 15), rightFmt);

            if (isLastPage)
            {
                int cutLineY = footerY + 15;
                g.DrawLine(dashedPen, startX, cutLineY, startX + width, cutLineY);

                int stubY = cutLineY + 10;
                int stubW = (width / 2) - 10;

                for (int i = 0; i < 2; i++)
                {
                    int sx = startX + (i * (stubW + 20));
                    g.DrawRectangle(linePen, sx, stubY, stubW, stubHeight);
                    int labelW = 70;
                    int qrSize = stubHeight - 10;
                    int dataW = stubW - labelW - qrSize - 10;

                    g.DrawLine(linePen, sx + labelW, stubY, sx + labelW, stubY + stubHeight);
                    g.DrawLine(linePen, sx + labelW + dataW, stubY, sx + labelW + dataW, stubY + stubHeight);
                    g.DrawLine(linePen, sx, stubY + 26, sx + labelW + dataW, stubY + 26);
                    g.DrawLine(linePen, sx, stubY + 52, sx + labelW + dataW, stubY + 52);

                    g.DrawString("Control no.", smallFont, textBrush, sx + 2, stubY + 6);
                    g.DrawString("Part No.", smallFont, textBrush, sx + 2, stubY + 32);
                    g.DrawString("Quantity", smallFont, textBrush, sx + 2, stubY + 58);

                    g.DrawString(data.ControlNo.ToString(), smallFont, textBrush, new Rectangle(sx + labelW, stubY, dataW, 26), centerFmt);
                    g.DrawString(data.PartNo, smallFont, textBrush, new Rectangle(sx + labelW, stubY + 26, dataW, 26), centerFmt);
                    g.DrawString(data.GrandTotalQuantity.ToString(), smallFont, textBrush, new Rectangle(sx + labelW, stubY + 52, dataW, 26), centerFmt);

                    if (data.QrCode != null)
                        g.DrawImage(data.QrCode, sx + labelW + dataW + 5, stubY + 5, qrSize, qrSize);
                }

                e.HasMorePages = false;
                _currentRowIndex = 0; // Reset for next print job
            }
            else
            {
                e.HasMorePages = true;
            }
        }
    }
}
