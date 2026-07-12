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

            // Включваме DoubleBuffering за DataGridView,
            // за да предотвратим премигването (flickering) на таблицата при динамично филтриране в реално време
            typeof(DataGridView).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.SetProperty,
                null, dgvParts, new object[] { true });
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            RefreshGrid();
            LoadCategories();
            LoadSortOptions(); 
        }

        // Помощен метод, който пълни DataGridView от базата данни
        private void RefreshGrid()
        {
            try
            {
                dgvParts.AutoGenerateColumns = false;

                colPrice.DefaultCellStyle.FormatProvider = System.Globalization.CultureInfo.InvariantCulture;
                colPrice.DefaultCellStyle.Format = "#,##0.00 '€'";

                // Взимаме данните от базата и ги кешираме в паметта
                _allParts = _repo.GetAll();

                // Прилагаме филтрите и сортирането веднага
                ApplyFilters();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Грешка при зареждане на данните: {ex.Message}", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // МЕТОД ЗА ДИНАМИЧНО ФИЛТРИРАНЕ И СОРТИРАНЕ НА ДАННИТЕ В ПАМЕТТА (LINQ)
        private void ApplyFilters()
        {
            if (_allParts == null) return;

            string searchText = txtSearch.Text.Trim().ToLower();
            string selectedCategory = cmbCategory.SelectedItem?.ToString() ?? "Всички";
            string selectedSort = cmbSort.SelectedItem?.ToString() ?? "Няма";

            // --- 1. ФИЛТРИРАНЕ ---
            var filteredList = _allParts.AsEnumerable();

            // Филтър по КАТЕГОРИЯ (от падащото меню)
            if (selectedCategory != "Всички")
            {
                filteredList = filteredList.Where(p => p.PartType.Equals(selectedCategory, StringComparison.OrdinalIgnoreCase));
            }

            // Динамичен филтър по ИМЕ или ПРОИЗВОДИТЕЛ (от текстовата кутия)
            if (!string.IsNullOrEmpty(searchText))
            {
                filteredList = filteredList.Where(p =>
                    (p.Name != null && p.Name.ToLower().Contains(searchText)) ||
                    (p.Manufacturer != null && p.Manufacturer.ToLower().Contains(searchText))
                );
            }

            // --- 2. СОРТИРАНЕ  ---
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
                default:
                    // "Няма" - оставя оригиналната подредба от базата данни
                    break;
            }

            // --- 3. ВИЗУАЛИЗАЦИЯ ---
            dgvParts.DataSource = null;
            dgvParts.DataSource = filteredList.ToList();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void cmbSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        // Помощен метод за пълнене на ComboBox-а за категории
        private void LoadCategories()
        {
            cmbCategory.Items.Clear();
            cmbCategory.Items.Add("Всички");
            cmbCategory.Items.Add("CPU");
            cmbCategory.Items.Add("GPU");
            cmbCategory.Items.Add("RAM");
            cmbCategory.Items.Add("Motherboard");
            cmbCategory.Items.Add("SSD");

            cmbCategory.SelectedIndex = 0; // По подразбиране да е на "Всички"
        }

        // Помощен метод за пълнене на ComboBox-а за сортиране
        private void LoadSortOptions()
        {
            cmbSort.Items.Clear();
            cmbSort.Items.Add("Няма");
            cmbSort.Items.Add("Цена: Възходяща");
            cmbSort.Items.Add("Цена: Низходяща");
            cmbSort.Items.Add("Наличност: Възходяща");
            cmbSort.Items.Add("Наличност: Низходяща");

            cmbSort.SelectedIndex = 0; // По подразбиране да е на "Няма"
        }

        private void dgvParts_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Проверяваме дали кликът е върху валиден ред (а не върху заглавката на колоните)
            if (e.RowIndex >= 0)
            {
                btnEdit_Click(sender, e); // Директно извикваме логиката на бутона за редакция
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvParts.CurrentRow == null)
            {
                MessageBox.Show("Моля, изберете част от таблицата за редактиране!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ComputerPart selectedPart = dgvParts.CurrentRow.DataBoundItem as ComputerPart;

            if (selectedPart == null)
            {
                MessageBox.Show("Неуспешно извличане на данните за избраната част.", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (PartEditForm editForm = new PartEditForm(selectedPart))
            {
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        _repo.Update(editForm.CreatedPart);
                        RefreshGrid(); // Презарежда от DB и автоматично пре-филтрира списъка
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
                        RefreshGrid(); // Презарежда от DB и показва новия елемент
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
            if (dgvParts.CurrentRow == null)
            {
                MessageBox.Show("Моля, изберете част от таблицата за изтриване!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ComputerPart selectedPart = dgvParts.CurrentRow.DataBoundItem as ComputerPart;

            if (selectedPart == null)
            {
                MessageBox.Show("Неуспешно извличане на данните за избраната част.", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult confirmResult = MessageBox.Show(
                $"Сигурни ли сте, че искате да изтриете компонента: \"{selectedPart.Name}\"?",
                "Потвърждение за изтриване",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    _repo.Delete(selectedPart.Id);
                    RefreshGrid(); // Презарежда и обновява изгледа веднага
                    MessageBox.Show("Компонентът беше изтрит успешно!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Грешка при изтриване от базата: {ex.Message}", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}