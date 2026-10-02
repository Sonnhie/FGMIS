using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FGScanner.UI
{
    internal static class CinemaRackLayout
    {
        private const int PreferredSeatWidth = 22;
        private const int PreferredRackHeight = 140;
        private const int MaximumRackColumns = 3;
        private static readonly Font FullSeatFont = new Font("Segoe UI", 7F, FontStyle.Regular);
        private static readonly Font CompactSeatFont = new Font("Segoe UI", 6.5F, FontStyle.Regular);
        private static readonly Font NumberSeatFont = new Font("Segoe UI", 7F, FontStyle.Regular);

        public static void Build(
            FlowLayoutPanel host,
            IEnumerable<string> rackIds,
            IReadOnlyDictionary<string, (int rows, int cols)> rackConfig,
            IDictionary<string, Button> rackButtons,
            EventHandler locationClickHandler)
        {
            string[] racks = rackIds.ToArray();
            var toolTip = new ToolTip
            {
                InitialDelay = 250,
                ReshowDelay = 100,
                AutoPopDelay = 5000,
                ShowAlways = true
            };

            host.SuspendLayout();
            host.Controls.Clear();
            host.AutoScroll = true;
            host.WrapContents = false;
            host.FlowDirection = FlowDirection.TopDown;
            host.BackColor = Color.FromArgb(238, 242, 247);
            rackButtons.Clear();

            int maximumSeatColumns = rackConfig.Count == 0
                ? 7
                : rackConfig.Values.Max(config => config.cols);
            float dpiScale = Math.Max(1F, host.DeviceDpi / 96F);

            var cinemaGrid = new TableLayoutPanel
            {
                Name = "CinemaRackGrid",
                BackColor = Color.FromArgb(238, 242, 247),
                Margin = Padding.Empty,
                Padding = new Padding(4),
                GrowStyle = TableLayoutPanelGrowStyle.FixedSize
            };

            int initialWidth = Math.Max(1, host.ClientSize.Width - host.Padding.Horizontal);
            ConfigureGrid(
                cinemaGrid,
                CalculateColumnCount(racks.Length, initialWidth, maximumSeatColumns, dpiScale),
                racks.Length);

            foreach (string rackId in racks)
            {
                cinemaGrid.Controls.Add(CreateRackSection(
                    rackId,
                    rackConfig.TryGetValue(rackId, out var config) ? config : (3, 7),
                    rackButtons,
                    locationClickHandler,
                    toolTip));
            }

            // Keep the ToolTip alive for as long as the generated grid is alive.
            cinemaGrid.Tag = toolTip;
            host.Controls.Add(cinemaGrid);

            bool isFittingGrid = false;
            void FitGridToHost()
            {
                if (isFittingGrid)
                {
                    return;
                }

                isFittingGrid = true;
                try
                {
                    ViewerPresentation.PerformAtomicLayout(host, () =>
                    {
                        int width = Math.Max(1, host.ClientSize.Width - host.Padding.Horizontal);
                        if (host.VerticalScroll.Visible)
                        {
                            width += SystemInformation.VerticalScrollBarWidth;
                        }

                        int height = Math.Max(1, host.ClientSize.Height - host.Padding.Vertical);
                        int columns = CalculateColumnCount(
                            racks.Length,
                            width,
                            maximumSeatColumns,
                            dpiScale);
                        int rows = (int)Math.Ceiling((double)racks.Length / columns);
                        int minimumRackHeight = Math.Max(90, (int)Math.Round(PreferredRackHeight * dpiScale));
                        bool needsVerticalScroll = rows * minimumRackHeight > height;

                        if (needsVerticalScroll)
                        {
                            width = Math.Max(1, width - SystemInformation.VerticalScrollBarWidth - 2);
                            columns = CalculateColumnCount(
                                racks.Length,
                                width,
                                maximumSeatColumns,
                                dpiScale);
                            rows = (int)Math.Ceiling((double)racks.Length / columns);
                        }

                        int gridHeight = Math.Max(height, rows * minimumRackHeight);
                        host.AutoScrollMinSize = needsVerticalScroll
                            ? new Size(0, gridHeight)
                            : Size.Empty;
                        cinemaGrid.Size = new Size(width, gridHeight);

                        ConfigureGrid(cinemaGrid, columns, racks.Length);
                    });
                }
                finally
                {
                    isFittingGrid = false;
                }
            }

            host.SizeChanged += (_, _) => FitGridToHost();
            FitGridToHost();
            ViewerPresentation.EnableDoubleBuffering(cinemaGrid);
            host.ResumeLayout(true);
        }

        private static Control CreateRackSection(
            string rackId,
            (int rows, int cols) config,
            IDictionary<string, Button> rackButtons,
            EventHandler locationClickHandler,
            ToolTip toolTip)
        {
            var section = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(4),
                Padding = new Padding(2),
                BackColor = Color.FromArgb(37, 55, 77),
                CellBorderStyle = TableLayoutPanelCellBorderStyle.Single,
                ColumnCount = 1,
                RowCount = 2
            };
            section.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            section.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            section.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            var title = new Label
            {
                Text = $"RACK {rackId}",
                Dock = DockStyle.Fill,
                Margin = Padding.Empty,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            };

            var seats = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(2),
                Padding = Padding.Empty,
                BackColor = Color.FromArgb(214, 222, 232),
                ColumnCount = config.cols,
                RowCount = config.rows,
                GrowStyle = TableLayoutPanelGrowStyle.FixedSize
            };

            for (int column = 0; column < config.cols; column++)
            {
                seats.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F / config.cols));
            }

            for (int row = 0; row < config.rows; row++)
            {
                seats.RowStyles.Add(new RowStyle(SizeType.Percent, 100F / config.rows));
                for (int column = 0; column < config.cols; column++)
                {
                    int rowNumber = row + 1;
                    int columnNumber = column + 1;
                    string location = $"{rackId}{rowNumber}-{columnNumber:D2}";
                    var seat = new Button
                    {
                        AccessibleName = location,
                        BackColor = Color.White,
                        Cursor = Cursors.Hand,
                        Dock = DockStyle.Fill,
                        FlatStyle = FlatStyle.Flat,
                        Font = FullSeatFont,
                        ForeColor = Color.Black,
                        Margin = new Padding(1),
                        MinimumSize = Size.Empty,
                        Padding = Padding.Empty,
                        Tag = location,
                        Text = location,
                        UseVisualStyleBackColor = false
                    };
                    seat.FlatAppearance.BorderColor = Color.FromArgb(122, 136, 153);
                    seat.FlatAppearance.BorderSize = 1;
                    seat.Click += locationClickHandler;
                    seat.SizeChanged += (_, _) => UpdateSeatCaption(seat, rowNumber, columnNumber, location);
                    toolTip.SetToolTip(seat, location);

                    seats.Controls.Add(seat, column, row);
                    rackButtons[location] = seat;
                }
            }

            section.Controls.Add(title, 0, 0);
            section.Controls.Add(seats, 0, 1);
            return section;
        }

        private static void UpdateSeatCaption(Button seat, int row, int column, string location)
        {
            if (seat.Width >= 62 && seat.Height >= 24)
            {
                seat.Text = location;
                seat.Font = FullSeatFont;
            }
            else if (seat.Width >= 27 && seat.Height >= 17)
            {
                seat.Text = $"{row}-{column}";
                seat.Font = CompactSeatFont;
            }
            else
            {
                seat.Text = column.ToString();
                seat.Font = NumberSeatFont;
            }
        }

        private static int CalculateColumnCount(
            int rackCount,
            int width,
            int maximumSeatColumns,
            float dpiScale)
        {
            if (rackCount <= 1)
            {
                return 1;
            }

            int minimumRackWidth = Math.Max(
                (int)Math.Round(280 * dpiScale),
                (int)Math.Round((maximumSeatColumns * PreferredSeatWidth + 24) * dpiScale));
            int columns = Math.Max(1, width / minimumRackWidth);
            return Math.Clamp(columns, 1, Math.Min(rackCount, MaximumRackColumns));
        }

        private static void ConfigureGrid(TableLayoutPanel grid, int columns, int itemCount)
        {
            int rows = (int)Math.Ceiling((double)itemCount / columns);
            if (grid.ColumnCount == columns && grid.RowCount == rows && grid.ColumnStyles.Count > 0)
            {
                return;
            }

            grid.SuspendLayout();
            grid.ColumnStyles.Clear();
            grid.RowStyles.Clear();
            grid.ColumnCount = columns;
            grid.RowCount = rows;

            for (int column = 0; column < columns; column++)
            {
                grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F / columns));
            }

            for (int row = 0; row < rows; row++)
            {
                grid.RowStyles.Add(new RowStyle(SizeType.Percent, 100F / rows));
            }

            for (int index = 0; index < grid.Controls.Count; index++)
            {
                grid.SetCellPosition(grid.Controls[index], new TableLayoutPanelCellPosition(
                    index % columns,
                    index / columns));
            }

            grid.ResumeLayout(true);
        }
    }
}
