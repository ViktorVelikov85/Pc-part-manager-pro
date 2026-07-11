using Pc_part_manager_pro.DAL;
using Pc_part_manager_pro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

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
        }

        // Това събитие се задейства при стартиране на формата
        private void MainForm_Load(object sender, EventArgs e)
        {
            RefreshGrid();
            LoadCategories();
        }

        // Помощен метод, който пълни DataGridView-то от базата данни
        private void RefreshGrid()
        {
            try
            {
                dgvParts.AutoGenerateColumns = false;
                colPrice.DefaultCellStyle.Format = "#,##0.00 '€'";

                // Подравняваме цената вдясно, за да изглежда професионално и прегледно
                colPrice.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                // Взимаме данните от базата и ги кешираме в паметта
                _allParts = _repo.GetAll();

                // Прилагаме филтрите (за търсачката и категорията)
                ApplyFilters();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Грешка при зареждане на данните: {ex.Message}", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // МЕТОД ЗА ДИНАМИЧНО ФИЛТРИРАНЕ НА ДАННИТЕ В ПАМЕТТА (LINQ)
        private void ApplyFilters()
        {
            if (_allParts == null) return;

            string searchText = txtSearch.Text.Trim().ToLower();
            string selectedCategory = cmbCategory.SelectedItem?.ToString() ?? "Всички";

            // Започваме филтрирането от пълния списък
            var filteredList = _allParts.AsEnumerable();

            // 1. Филтър по КАТЕГОРИЯ (от падащото меню)
            if (selectedCategory != "Всички")
            {
                filteredList = filteredList.Where(p => p.PartType.Equals(selectedCategory, StringComparison.OrdinalIgnoreCase));
            }

            // 2. Динамичен филтур по ИМЕ или ПРОИЗВОДИТЕЛ (от текстовата кутия)
            if (!string.IsNullOrEmpty(searchText))
            {
                filteredList = filteredList.Where(p =>
                    (p.Name != null && p.Name.ToLower().Contains(searchText)) ||
                    (p.Manufacturer != null && p.Manufacturer.ToLower().Contains(searchText))
                );
            }

            // 3. Закачаме филтрирания резултат към таблицата на екрана
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

        // Помощен метод за пълнене на ComboBox-а
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