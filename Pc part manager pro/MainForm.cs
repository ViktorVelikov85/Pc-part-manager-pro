using Pc_part_manager_pro.DAL;
using Pc_part_manager_pro.Models;

namespace Pc_part_manager_pro
{
    public partial class MainForm : Form
    {
        // Инстанция към нашето репозитори
        private readonly PartRepository _repo = new PartRepository();

        // Пазим оригиналния пълен списък в паметта за бързо динамично филтриране
        private List<ComputerPart> _allParts = new List<ComputerPart>();

        public MainForm()
        {
            InitializeComponent();

            // Включваме DoubleBuffering за DataGridView
            typeof(DataGridView).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.SetProperty,
                null, dgvParts, new object[] { true });

            // Закачаме собствено рисуване за ComboBox-овете, за да убием белия цвят веднъж завинаги
            cmbCategory.DrawMode = DrawMode.OwnerDrawFixed;
            cmbSort.DrawMode = DrawMode.OwnerDrawFixed;
            cmbCategory.DrawItem += ComboBox_DrawItem;
            cmbSort.DrawItem += ComboBox_DrawItem;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            RefreshGrid();
            LoadCategories();
            LoadSortOptions();
            ApplyTheme();
        }

        private void RefreshGrid()
        {
            try
            {
                dgvParts.AutoGenerateColumns = false;
                colPrice.DefaultCellStyle.FormatProvider = System.Globalization.CultureInfo.InvariantCulture;
                colPrice.DefaultCellStyle.Format = "#,##0.00 '€'";

                _allParts = _repo.GetAll();
                ApplyFilters();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Грешка при зареждане на данните: {ex.Message}", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyFilters()
        {
            if (_allParts == null) return;

            string searchText = txtSearch.Text.Trim().ToLower();
            string selectedCategory = cmbCategory.SelectedItem?.ToString() ?? "Всички";
            string selectedSort = cmbSort.SelectedItem?.ToString() ?? "Няма";

            var filteredList = _allParts.AsEnumerable();

            if (selectedCategory != "Всички")
            {
                filteredList = filteredList.Where(p => p.PartType.Equals(selectedCategory, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(searchText))
            {
                filteredList = filteredList.Where(p =>
                    (p.Name != null && p.Name.ToLower().Contains(searchText)) ||
                    (p.Manufacturer != null && p.Manufacturer.ToLower().Contains(searchText))
                );
            }

            switch (selectedSort)
            {
                case "Цена: Възходяща":
                    filteredList = filteredList.OrderBy(p => p.Price);
                    break;
                case "Цена: Низходяща":
                    filteredList = filteredList.OrderByDescending(p => p.Price);
                    break;
                case "Наличност: Възходяща":
                    filteredList = filteredList.OrderBy(p => p.Quantity);
                    break;
                case "Наличност: Низходяща":
                    filteredList = filteredList.OrderByDescending(p => p.Quantity);
                    break;
            }

            dgvParts.DataSource = null;
            dgvParts.DataSource = filteredList.ToList();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e) => ApplyFilters();
        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e) => ApplyFilters();
        private void cmbSort_SelectedIndexChanged(object sender, EventArgs e) => ApplyFilters();

        private void LoadCategories()
        {
            cmbCategory.Items.Clear();
            cmbCategory.Items.Add("Всички");
            cmbCategory.Items.Add("CPU");
            cmbCategory.Items.Add("GPU");
            cmbCategory.Items.Add("RAM");
            cmbCategory.Items.Add("Motherboard");
            cmbCategory.Items.Add("SSD");
            cmbCategory.SelectedIndex = 0;
        }

        private void LoadSortOptions()
        {
            cmbSort.Items.Clear();
            cmbSort.Items.Add("Няма");
            cmbSort.Items.Add("Цена: Възходяща");
            cmbSort.Items.Add("Цена: Низходяща");
            cmbSort.Items.Add("Наличност: Възходяща");
            cmbSort.Items.Add("Наличност: Низходяща");
            cmbSort.SelectedIndex = 0;
        }

        private void dgvParts_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) btnEdit_Click(sender, e);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvParts.CurrentRow == null)
            {
                MessageBox.Show("Моля, изберете част от таблицата за редактиране!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ComputerPart selectedPart = dgvParts.CurrentRow.DataBoundItem as ComputerPart;
            if (selectedPart == null) return;

            using (PartEditForm editForm = new PartEditForm(selectedPart))
            {
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        _repo.Update(editForm.CreatedPart);
                        RefreshGrid();
                        MessageBox.Show("Успешно редактиране!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Грешка при запис: {ex.Message}", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (PartEditForm addForm = new PartEditForm())
            {
                if (addForm.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        _repo.Add(addForm.CreatedPart);
                        RefreshGrid();
                        MessageBox.Show("Новият компонент беше добавен успешно!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Грешка при добавяне в базата: {ex.Message}", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvParts.CurrentRow == null) return;
            ComputerPart selectedPart = dgvParts.CurrentRow.DataBoundItem as ComputerPart;
            if (selectedPart == null) return;

            DialogResult confirmResult = MessageBox.Show(
                $"Сигурни ли сте, че искате да изтриете компонента: \"{selectedPart.Name}\"?",
                "Потвърждение за изтриване", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    _repo.Delete(selectedPart.Id);
                    RefreshGrid();
                    MessageBox.Show("Компонентът беше изтрит успешно!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Грешка при изтриване: {ex.Message}", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        // ЛОГИКА ЗА ТЕМАТА (DARK / LIGHT MODE)
 
        public static bool IsDarkMode { get; private set; } = false;

        private static readonly Color DiscordDarkBg = Color.FromArgb(54, 57, 63);
        private static readonly Color DiscordInputBg = Color.FromArgb(47, 49, 54);

        private void btnThemeToggle_Click(object sender, EventArgs e)
        {
            IsDarkMode = !IsDarkMode;
            ApplyTheme();
        }

        public void ApplyTheme()
        {
            if (IsDarkMode)
            {
                btnThemeToggle.Text = "☀️";
                ApplyThemeToControl(this, DiscordDarkBg, Color.White, DiscordInputBg);

                dgvParts.EnableHeadersVisualStyles = false;
                dgvParts.BackgroundColor = Color.FromArgb(32, 34, 37);
                dgvParts.GridColor = Color.FromArgb(42, 44, 47);
                dgvParts.DefaultCellStyle.BackColor = DiscordInputBg;
                dgvParts.DefaultCellStyle.ForeColor = Color.White;
                dgvParts.DefaultCellStyle.SelectionBackColor = Color.FromArgb(114, 137, 218);
                dgvParts.DefaultCellStyle.SelectionForeColor = Color.White;
                dgvParts.ColumnHeadersDefaultCellStyle.BackColor = DiscordDarkBg;
                dgvParts.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            }
            else
            {
                btnThemeToggle.Text = "🌙";
                ApplyThemeToControl(this, SystemColors.Control, SystemColors.ControlText, SystemColors.Window);

                dgvParts.EnableHeadersVisualStyles = true;
                dgvParts.BackgroundColor = SystemColors.AppWorkspace;
                dgvParts.DefaultCellStyle.BackColor = SystemColors.Window;
                dgvParts.DefaultCellStyle.ForeColor = SystemColors.ControlText;
            }

            // Форсираме пълно прерисуване на двата обекта веднага след смяна на темата
            cmbCategory.Invalidate();
            cmbSort.Invalidate();
        }

        private void ApplyThemeToControl(Control root, Color backColor, Color foreColor, Color inputBackColor)
        {
            root.BackColor = backColor;

            foreach (Control child in root.Controls)
            {
                if (child is Button btn)
                {
                    btn.FlatAppearance.BorderSize = IsDarkMode ? 0 : 1;
                    btn.FlatStyle = IsDarkMode ? FlatStyle.Flat : FlatStyle.Standard;

                    if (btn != btnThemeToggle && (btn.BackColor == Color.LightGreen || btn.BackColor == Color.LightBlue || btn.BackColor == Color.Coral))
                    {
                        btn.ForeColor = Color.Black;
                        continue;
                    }
                    btn.BackColor = IsDarkMode ? inputBackColor : SystemColors.Control;
                    btn.ForeColor = IsDarkMode ? foreColor : SystemColors.ControlText;
                }
                else if (child is TextBox || child is NumericUpDown)
                {
                    child.BackColor = IsDarkMode ? inputBackColor : SystemColors.Window;
                    child.ForeColor = IsDarkMode ? foreColor : SystemColors.ControlText;
                }
                else if (child is ComboBox cmb)
                {
                    cmb.FlatStyle = IsDarkMode ? FlatStyle.Flat : FlatStyle.Standard;
                    cmb.BackColor = IsDarkMode ? inputBackColor : SystemColors.Window;
                    cmb.ForeColor = IsDarkMode ? foreColor : SystemColors.ControlText;
                }
                else if (child is Label)
                {
                    child.ForeColor = foreColor;
                }

                if (child.HasChildren && !(child is DataGridView) && !(child is NumericUpDown) && !(child is ComboBox))
                {
                    ApplyThemeToControl(child, backColor, foreColor, inputBackColor);
                }
            }
        }

        // ЖЕЛЕЗНИЯТ МЕТОД: Ръчно рисуване на текстовете и елементите вътре в ComboBox
        private void ComboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;
            ComboBox combo = sender as ComboBox;

            Color bg = IsDarkMode ? DiscordInputBg : SystemColors.Window;
            Color text = IsDarkMode ? Color.White : SystemColors.ControlText;

            // Ако мишката е върху елемента в падащото меню
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                bg = IsDarkMode ? Color.FromArgb(114, 137, 218) : SystemColors.Highlight;
                text = Color.White;
            }

            using (SolidBrush bgBrush = new SolidBrush(bg))
            using (SolidBrush textBrush = new SolidBrush(text))
            {
                e.Graphics.FillRectangle(bgBrush, e.Bounds);
                e.Graphics.DrawString(combo.Items[e.Index].ToString(), e.Font, textBrush, e.Bounds);
            }
            e.DrawFocusRectangle();
        }
    }
}