using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace FGScanner.UI
{
    /// <summary>
    /// Applies a consistent, high-density visual language to existing WinForms controls.
    /// This class intentionally changes presentation only; it does not alter control events,
    /// data binding, validation, or application workflows.
    /// </summary>
    public static class EnterpriseTheme
    {
        private static readonly ConditionalWeakTable<Control, ThemeMarker> ThemedControls = new();

        private static readonly Font StandardFont = new("Segoe UI", 9F, FontStyle.Regular);
        private static readonly Font EmphasisFont = new("Segoe UI Semibold", 9F, FontStyle.Bold);

        private static readonly Color Canvas = Color.FromArgb(245, 247, 250);
        private static readonly Color Surface = Color.White;
        private static readonly Color Primary = Color.FromArgb(29, 78, 145);
        private static readonly Color PrimaryHover = Color.FromArgb(21, 63, 118);
        private static readonly Color Secondary = Color.FromArgb(232, 237, 245);
        private static readonly Color SecondaryHover = Color.FromArgb(217, 226, 239);
        private static readonly Color Danger = Color.FromArgb(185, 28, 28);
        private static readonly Color DangerHover = Color.FromArgb(153, 27, 27);
        private static readonly Color TextPrimary = Color.FromArgb(31, 41, 55);
        private static readonly Color TextMuted = Color.FromArgb(75, 85, 99);
        private static readonly Color Border = Color.FromArgb(218, 226, 237);

        /// <summary>
        /// Styles a form or user control and any present or future child controls.
        /// It is safe to call this more than once for the same root.
        /// </summary>
        public static void Apply(Control root)
        {
            if (root == null || root.IsDisposed)
            {
                return;
            }

            root.SuspendLayout();
            try
            {
                ApplyRecursive(root);
            }
            finally
            {
                root.ResumeLayout(false);
            }
        }

        private static void ApplyRecursive(Control control)
        {
            if (IsRackElement(control))
            {
                return;
            }

            ApplyControl(control);

            foreach (Control child in control.Controls)
            {
                ApplyRecursive(child);
            }
        }

        private static void ApplyControl(Control control)
        {
            if (IsRackElement(control))
            {
                return;
            }

            if (ThemedControls.TryGetValue(control, out _))
            {
                return;
            }

            ThemedControls.Add(control, new ThemeMarker());
            control.ControlAdded += ControlAdded;

            if (control is Form or UserControl)
            {
                control.BackColor = Canvas;
                control.Font = StandardFont;
            }

            if (control.Font.Name.Equals("Bahnschrift Condensed", StringComparison.OrdinalIgnoreCase))
            {
                control.Font = StandardFont;
            }

            switch (control)
            {
                case Button button:
                    StyleButton(button);
                    break;
                case DataGridView grid:
                    StyleGrid(grid);
                    break;
                case TextBox textBox:
                    StyleTextBox(textBox);
                    break;
                case ComboBox comboBox:
                    StyleComboBox(comboBox);
                    break;
                case DateTimePicker dateTimePicker:
                    StyleDateTimePicker(dateTimePicker);
                    break;
                case NumericUpDown numericUpDown:
                    StyleNumericUpDown(numericUpDown);
                    break;
                case GroupBox groupBox:
                    groupBox.ForeColor = TextPrimary;
                    groupBox.Font = EmphasisFont;
                    break;
                case FlowLayoutPanel flowPanel:
                    flowPanel.BackColor = Canvas;
                    break;
                case TableLayoutPanel tablePanel:
                    tablePanel.BackColor = Canvas;
                    break;
                case Panel panel:
                    StylePanel(panel);
                    break;
                case MenuStrip menuStrip:
                    StyleMenuStrip(menuStrip);
                    break;
                case StatusStrip statusStrip:
                    StyleStatusStrip(statusStrip);
                    break;
                case ToolStrip toolStrip:
                    StyleToolStrip(toolStrip);
                    break;
                case Chart chart:
                    StyleChart(chart);
                    break;
                case Label label:
                    StyleLabel(label);
                    break;
                case PictureBox pictureBox:
                    StyleActionPictureBox(pictureBox);
                    break;
            }

            AddAccessibilityMetadata(control);
        }

        private static void ControlAdded(object sender, ControlEventArgs eventArgs)
        {
            if (eventArgs.Control != null && !IsRackElement(eventArgs.Control))
            {
                ApplyRecursive(eventArgs.Control);
            }
        }

        private static void StyleButton(Button button)
        {
            if (IsRackButton(button))
            {
                return;
            }

            // Don't restyle buttons with images (like custom icon buttons)
            if (button.Image != null)
            {
                return;
            }

            if (!HasNeutralBackground(button.BackColor))
            {
                return;
            }

            button.UseVisualStyleBackColor = false;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Font = EmphasisFont;
            button.Cursor = Cursors.Hand;
            button.MinimumSize = new Size(Math.Min(button.Width, 96), Math.Max(button.Height, 32));
            button.Padding = new Padding(10, 0, 10, 0);

            string text = button.Text ?? string.Empty;
            string name = button.Name ?? string.Empty;
            string token = $"{text} {name}".ToUpperInvariant();

            if (token.Contains("CANCEL") || token.Contains("DELETE") || token.Contains("DEDUCT") || token.Equals("OUT"))
            {
                button.BackColor = Danger;
                button.ForeColor = Color.White;
                button.FlatAppearance.MouseOverBackColor = DangerHover;
                button.FlatAppearance.MouseDownBackColor = DangerHover;
                return;
            }

            if (token.Contains("CLEAR") || token.Contains("PREV") || token.Contains("NEXT") || token.Contains("SELECT FILE") || token.Contains("CLOSE"))
            {
                button.BackColor = Secondary;
                button.ForeColor = Primary;
                button.FlatAppearance.MouseOverBackColor = SecondaryHover;
                button.FlatAppearance.MouseDownBackColor = SecondaryHover;
                return;
            }

            button.BackColor = Primary;
            button.ForeColor = Color.White;
            button.FlatAppearance.MouseOverBackColor = PrimaryHover;
            button.FlatAppearance.MouseDownBackColor = PrimaryHover;
        }

        private static void StyleTextBox(TextBox textBox)
        {
            textBox.BackColor = textBox.ReadOnly ? Color.FromArgb(249, 250, 251) : Surface;
            textBox.ForeColor = TextPrimary;
            textBox.BorderStyle = BorderStyle.FixedSingle;
        }

        private static void StyleComboBox(ComboBox comboBox)
        {
            comboBox.BackColor = Surface;
            comboBox.ForeColor = TextPrimary;
            comboBox.FlatStyle = FlatStyle.Flat;
        }

        private static void StyleDateTimePicker(DateTimePicker dateTimePicker)
        {
            dateTimePicker.CalendarForeColor = TextPrimary;
            dateTimePicker.CalendarMonthBackground = Surface;
            dateTimePicker.CalendarTitleBackColor = Primary;
            dateTimePicker.CalendarTitleForeColor = Color.White;
        }

        private static void StyleNumericUpDown(NumericUpDown numericUpDown)
        {
            numericUpDown.BackColor = Surface;
            numericUpDown.ForeColor = TextPrimary;
            numericUpDown.BorderStyle = BorderStyle.FixedSingle;
        }

        private static void StyleGrid(DataGridView grid)
        {
            grid.BackgroundColor = Surface;
            grid.BorderStyle = BorderStyle.None;
            grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            grid.GridColor = Border;
            grid.EnableHeadersVisualStyles = false;
            grid.RowHeadersVisible = false;
            grid.ScrollBars = ScrollBars.Both;
            grid.AllowUserToResizeColumns = true;
            // Preserve each screen's existing selection behavior. Some reports use
            // multi-cell selection to calculate totals in their current event handlers.
            grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            grid.RowTemplate.Height = Math.Max(grid.RowTemplate.Height, 30);
            grid.DefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Surface,
                ForeColor = TextPrimary,
                SelectionBackColor = Color.FromArgb(219, 234, 254),
                SelectionForeColor = TextPrimary,
                Padding = new Padding(6, 0, 6, 0),
                Font = StandardFont
            };
            grid.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(249, 250, 251),
                ForeColor = TextPrimary,
                SelectionBackColor = Color.FromArgb(219, 234, 254),
                SelectionForeColor = TextPrimary,
                Padding = new Padding(6, 0, 6, 0),
                Font = StandardFont
            };
            grid.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Primary,
                ForeColor = Color.White,
                SelectionBackColor = Primary,
                SelectionForeColor = Color.White,
                Alignment = DataGridViewContentAlignment.MiddleLeft,
                Font = EmphasisFont,
                Padding = new Padding(6, 0, 6, 0)
            };
            grid.ColumnHeadersHeight = Math.Max(grid.ColumnHeadersHeight, 34);

            // Ensure tables with many columns remain scrollable horizontally and readable
            grid.DataBindingComplete += (sender, e) =>
            {
                if (grid.IsHandleCreated)
                {
                    grid.BeginInvoke(new Action(() => ConfigureGridScrollability(grid)));
                }
                else
                {
                    ConfigureGridScrollability(grid);
                }
            };

            grid.ColumnAdded += (sender, e) =>
            {
                if (e.Column != null)
                {
                    e.Column.MinimumWidth = Math.Max(e.Column.MinimumWidth, GetColumnComfortableMinWidth(e.Column));
                }
            };

            grid.Resize += (sender, e) =>
            {
                if (grid.IsHandleCreated)
                {
                    grid.BeginInvoke(new Action(() => ConfigureGridScrollability(grid)));
                }
            };

            ConfigureGridScrollability(grid);
        }

        private static void ConfigureGridScrollability(DataGridView grid)
        {
            if (grid == null || grid.IsDisposed || grid.Columns.Count == 0)
            {
                return;
            }

            grid.ScrollBars = ScrollBars.Both;
            grid.AllowUserToResizeColumns = true;

            int totalComfortableWidth = 0;
            int visibleCount = 0;

            foreach (DataGridViewColumn col in grid.Columns)
            {
                if (!col.Visible) continue;

                visibleCount++;
                int minColWidth = GetColumnComfortableMinWidth(col);
                col.MinimumWidth = Math.Max(col.MinimumWidth, minColWidth);
                totalComfortableWidth += col.MinimumWidth;
            }

            int availableWidth = grid.ClientSize.Width;

            // When columns total more than available width, or when there are 6+ columns,
            // ensure 'Fill' doesn't crush columns into tiny unreadable slivers.
            // By switching to None or retaining minimum widths, horizontal scrolling activates!
            if (totalComfortableWidth > availableWidth || visibleCount >= 6)
            {
                foreach (DataGridViewColumn col in grid.Columns)
                {
                    if (!col.Visible) continue;

                    if (col.AutoSizeMode == DataGridViewAutoSizeColumnMode.Fill)
                    {
                        col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                        col.Width = Math.Max(col.Width, col.MinimumWidth);
                    }
                    else if (col.Width < col.MinimumWidth)
                    {
                        col.Width = col.MinimumWidth;
                    }
                }
            }
        }

        private static int GetColumnComfortableMinWidth(DataGridViewColumn col)
        {
            string name = (col.HeaderText ?? col.Name ?? string.Empty).ToUpperInvariant();

            if (name.Contains("DATE") || name.Contains("TIME"))
            {
                return 120;
            }
            if (name.Contains("CONTROL") || name.Contains("TRANSACTION") || name.Contains("SHIPMENT") || name.Contains("RETURN"))
            {
                return 145;
            }
            if (name.Contains("PART NUMBER") || name.Contains("PARTNUMBER") || name.Contains("PART NO"))
            {
                return 130;
            }
            if (name.Contains("PART NAME") || name.Contains("PARTNAME") || name.Contains("CUSTOMER"))
            {
                return 130;
            }
            if (name.Contains("CLASSIFICATION") || name.Contains("REMARKS") || name.Contains("STATUS") || name.Contains("LOCATION"))
            {
                return 125;
            }
            if (name.Contains("QUANTITY") || name.Contains("QTY") || name.Contains("BOX") || name.Contains("PPS") || name.Contains("STOCK"))
            {
                return 95;
            }
            if (name.Contains("VERSION") || name.Contains("VER"))
            {
                return 95;
            }
            if (col is DataGridViewButtonColumn)
            {
                return 90;
            }

            return 105;
        }

        private static void StylePanel(Panel panel)
        {
            if (!HasNeutralBackground(panel.BackColor))
            {
                return;
            }

            // Do not alter main docking/container panels
            if (panel.Dock == DockStyle.Fill || panel.Name == "panel1" || panel.Name == "panel2")
            {
                return;
            }

            panel.BackColor = Surface;
            panel.Paint += PaintSurfaceBorder;
        }

        private static void PaintSurfaceBorder(object sender, PaintEventArgs eventArgs)
        {
            if (sender is not Panel panel || panel.Width < 2 || panel.Height < 2)
            {
                return;
            }

            ControlPaint.DrawBorder(eventArgs.Graphics, panel.ClientRectangle, Border, ButtonBorderStyle.Solid);
        }

        private static void StyleMenuStrip(MenuStrip menuStrip)
        {
            menuStrip.BackColor = Surface;
            menuStrip.ForeColor = TextPrimary;
            menuStrip.Font = EmphasisFont;
            menuStrip.Padding = new Padding(12, 4, 12, 4);
            menuStrip.Renderer = new ToolStripProfessionalRenderer(new EnterpriseColorTable());
            StyleToolStripItems(menuStrip.Items);
        }

        private static void StyleStatusStrip(StatusStrip statusStrip)
        {
            statusStrip.BackColor = Surface;
            statusStrip.ForeColor = TextMuted;
            statusStrip.SizingGrip = false;
            statusStrip.Renderer = new ToolStripProfessionalRenderer(new EnterpriseColorTable());
            StyleToolStripItems(statusStrip.Items);
        }

        private static void StyleToolStrip(ToolStrip toolStrip)
        {
            toolStrip.BackColor = Surface;
            toolStrip.ForeColor = TextPrimary;
            toolStrip.Renderer = new ToolStripProfessionalRenderer(new EnterpriseColorTable());
            StyleToolStripItems(toolStrip.Items);
        }

        private static void StyleToolStripItems(ToolStripItemCollection items)
        {
            foreach (ToolStripItem item in items)
            {
                item.ForeColor = TextPrimary;
                item.Font = StandardFont;
                item.Padding = new Padding(4, 1, 4, 1);

                if (item is ToolStripDropDownItem dropDownItem)
                {
                    dropDownItem.DropDown.BackColor = Surface;
                    dropDownItem.DropDown.ForeColor = TextPrimary;
                    dropDownItem.DropDown.Renderer = new ToolStripProfessionalRenderer(new EnterpriseColorTable());
                    StyleToolStripItems(dropDownItem.DropDownItems);
                }
            }
        }

        private static void StyleChart(Chart chart)
        {
            chart.BackColor = Surface;
            chart.BorderlineColor = Border;
            chart.BorderlineDashStyle = ChartDashStyle.Solid;

            foreach (ChartArea area in chart.ChartAreas)
            {
                area.BackColor = Surface;
                area.BorderColor = Border;
                area.AxisX.LineColor = Border;
                area.AxisY.LineColor = Border;
                area.AxisX.MajorGrid.LineColor = Color.FromArgb(235, 238, 242);
                area.AxisY.MajorGrid.LineColor = Color.FromArgb(235, 238, 242);
                area.AxisX.LabelStyle.ForeColor = TextMuted;
                area.AxisY.LabelStyle.ForeColor = TextMuted;
            }
        }

        private static void StyleLabel(Label label)
        {
            if (!HasNeutralBackground(label.BackColor))
            {
                return;
            }

            if (label.ForeColor.ToArgb() == Color.Black.ToArgb() ||
                label.ForeColor.ToArgb() == SystemColors.ControlText.ToArgb())
            {
                label.ForeColor = TextPrimary;
            }
        }

        private static void StyleActionPictureBox(PictureBox pictureBox)
        {
            string token = pictureBox.Name ?? string.Empty;
            if (!token.Contains("close", StringComparison.OrdinalIgnoreCase) &&
                !token.Contains("minimize", StringComparison.OrdinalIgnoreCase) &&
                !token.Contains("max", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            pictureBox.Cursor = Cursors.Hand;
            pictureBox.AccessibleRole = AccessibleRole.PushButton;
            pictureBox.AccessibleName = token.Contains("close", StringComparison.OrdinalIgnoreCase)
                ? "Close window"
                : token.Contains("minimize", StringComparison.OrdinalIgnoreCase)
                    ? "Minimize window"
                    : "Maximize or restore window";
        }

        private static void AddAccessibilityMetadata(Control control)
        {
            if (IsRackElement(control) || (control is Button b && IsRackButton(b)))
            {
                return;
            }

            if (!string.IsNullOrWhiteSpace(control.AccessibleName))
            {
                return;
            }

            if (control is Button button && !string.IsNullOrWhiteSpace(button.Text))
            {
                control.AccessibleName = button.Text;
            }
            else if (control is TextBox or ComboBox or DateTimePicker or NumericUpDown)
            {
                control.AccessibleName = ToWords(control.Name);
            }
            else if (control is DataGridView)
            {
                control.AccessibleName = ToWords(control.Name);
            }
        }

        private static string ToWords(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return "Input";
            }

            return System.Text.RegularExpressions.Regex.Replace(value, "([a-z])([A-Z])", "$1 $2")
                .Replace("cmb", "", StringComparison.OrdinalIgnoreCase)
                .Replace("txt", "", StringComparison.OrdinalIgnoreCase)
                .Trim();
        }

        private static bool HasNeutralBackground(Color color)
        {
            return color.IsEmpty ||
                   color.ToArgb() == SystemColors.Control.ToArgb() ||
                   color.ToArgb() == SystemColors.ButtonFace.ToArgb() ||
                   color.ToArgb() == SystemColors.Window.ToArgb() ||
                   color.ToArgb() == Color.White.ToArgb() ||
                   color.ToArgb() == Color.WhiteSmoke.ToArgb();
        }

        private static bool IsRackElement(Control control)
        {
            if (control == null)
            {
                return false;
            }

            if (control is FlowLayoutPanel flow && flow.Name == "flowLayoutPanel1")
            {
                return true;
            }

            Control current = control;
            while (current != null)
            {
                if (current is FlowLayoutPanel flowParent && flowParent.Name == "flowLayoutPanel1")
                {
                    return true;
                }

                if (current is Panel panel && panel.Tag is string tag && !string.IsNullOrEmpty(tag))
                {
                    return true;
                }

                current = current.Parent;
            }

            return false;
        }

        private static bool IsRackButton(Button button)
        {
            if (IsRackElement(button))
            {
                return true;
            }

            if (button.Parent is Panel p && p.Tag is string)
            {
                return true;
            }

            if (System.Text.RegularExpressions.Regex.IsMatch(button.Text ?? string.Empty, @"^[A-Z0-9-]+-d{2}$"))
            {
                return true;
            }

            return false;
        }

        private sealed class ThemeMarker
        {
        }

        private sealed class EnterpriseColorTable : ProfessionalColorTable
        {
            public override Color MenuStripGradientBegin => Surface;
            public override Color MenuStripGradientEnd => Surface;
            public override Color ToolStripDropDownBackground => Surface;
            public override Color ImageMarginGradientBegin => Surface;
            public override Color ImageMarginGradientMiddle => Surface;
            public override Color ImageMarginGradientEnd => Surface;
            public override Color MenuItemSelected => Color.FromArgb(219, 234, 254);
            public override Color MenuItemSelectedGradientBegin => Color.FromArgb(219, 234, 254);
            public override Color MenuItemSelectedGradientEnd => Color.FromArgb(219, 234, 254);
            public override Color MenuItemBorder => Color.FromArgb(191, 219, 254);
            public override Color ButtonSelectedHighlight => Color.FromArgb(219, 234, 254);
            public override Color ButtonSelectedHighlightBorder => Color.FromArgb(191, 219, 254);
            public override Color StatusStripGradientBegin => Surface;
            public override Color StatusStripGradientEnd => Surface;
        }
    }
}
