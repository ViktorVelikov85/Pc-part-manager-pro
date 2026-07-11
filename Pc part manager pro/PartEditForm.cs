using System;
using System.Windows.Forms;
using Pc_part_manager_pro.Models;
using Pc_part_manager_pro.Models.PcParts;

namespace Pc_part_manager_pro
{
    public partial class PartEditForm : Form
    {
        public ComputerPart CreatedPart { get; private set; }
        private readonly bool _isEditMode = false;

        // 1. Конструктор за ДОБАВЯНЕ (извиква се без параметри)
        public PartEditForm()
        {
            InitializeComponent();
            _isEditMode = false;

            // Първоначално скриваме всичко и избираме първия елемент (CPU)
            HideAllSpecificFields();
            if (cmbType.Items.Count > 0)
            {
                cmbType.SelectedIndex = 0;
            }
        }

        // 2. Конструктор за РЕДАКТИРАНЕ (подаваме му частта от таблицата)
        public PartEditForm(ComputerPart partToEdit)
        {
            InitializeComponent();
            _isEditMode = true;
            CreatedPart = partToEdit;

            // Скриваме всичко преди да заредим конкретните полета
            HideAllSpecificFields();

            // Зареждаме общите данни в контролите
            txtName.Text = partToEdit.Name;
            txtManufacturer.Text = partToEdit.Manufacturer;
            numPrice.Value = partToEdit.Price;
            numQuantity.Value = partToEdit.Quantity;

            // Конвертираме типа към главни букви, за да съвпадне с Items в ComboBox-а ("Cpu" -> "CPU")
            string typeForCombo = partToEdit.PartType.ToUpper();
            if (typeForCombo == "MOTHERBOARD") typeForCombo = "Motherboard"; // Ако в Items е записано с малки букви

            cmbType.SelectedItem = typeForCombo;
            cmbType.Enabled = false; // Заключваме типа хардуер при редакция

            // Ръчно задействаме показването на правилния панел
            UpdateSpecificPanelVisibility(typeForCombo);

            // Попълваме специфичните контроли според конкретния ООП клас
            PopulateSpecificFields(partToEdit);
        }

        // ⚡ СЪБИТИЕ ПРИ СМЯНА НА ИЗБОРА В COMBOBOX-А
        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbType.SelectedItem != null)
            {
                UpdateSpecificPanelVisibility(cmbType.SelectedItem.ToString());
            }
        }

        // Управление на видимостта според избрания тип
        private void UpdateSpecificPanelVisibility(string selectedType)
        {
            HideAllSpecificFields();

            switch (selectedType)
            {
                case "CPU": ToggleCpuFields(true); break;
                case "GPU": ToggleGpuFields(true); break;
                case "RAM": ToggleRamFields(true); break;
                case "Motherboard": ToggleMbFields(true); break;
                case "SSD": ToggleSsdFields(true); break;
            }
        }

        private void PopulateSpecificFields(ComputerPart part)
        {
            if (part is Cpu cpu)
            {
                numCpuCores.Value = cpu.Cores;
                txtCpuSocket.Text = cpu.Socket;
                numCpuClock.Value = cpu.ClockGhz;
                numCpuTdp.Value = cpu.TdpWatts;
            }
            else if (part is Gpu gpu)
            {
                numGpuVram.Value = gpu.VramSizeGb;
                txtGpuMemType.Text = gpu.MemoryType;
                numGpuClock.Value = gpu.ClockMhz;
            }
            else if (part is Ram ram)
            {
                numRamCapacity.Value = ram.CapacityGb;
                numRamSpeed.Value = ram.SpeedMhz;
                txtRamGen.Text = ram.Generation;
                txtRamCas.Text = ram.CasLatency;
            }
            else if (part is Motherboard mb)
            {
                txtMbChipset.Text = mb.Chipset;
                txtMbForm.Text = mb.FormFactor;
                txtMbSocket.Text = mb.Socket;
                txtMbRamGen.Text = mb.SupportedRamGen;
            }
            else if (part is Ssd ssd)
            {
                numSsdCapacity.Value = ssd.CapacityGb;
                txtSsdForm.Text = ssd.FormFactor;
                numSsdReadSpeed.Value = ssd.ReadSpeedMb;
            }
        }

        // ⚡ СЪБИТИЕ НА БУТОНА ЗАПАЗИ
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtManufacturer.Text))
                {
                    MessageBox.Show("Моля, попълнете наименованието и производителя!", "Валидация", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Ако сме в режим на добавяне, създаваме нов обект според селекцията
                if (!_isEditMode)
                {
                    string selectedType = cmbType.SelectedItem.ToString();
                    switch (selectedType)
                    {
                        case "CPU": CreatedPart = new Cpu(); CreatedPart.PartCategory.Id = 1; break;
                        case "GPU": CreatedPart = new Gpu(); CreatedPart.PartCategory.Id = 2; break;
                        case "RAM": CreatedPart = new Ram(); CreatedPart.PartCategory.Id = 3; break;
                        case "Motherboard": CreatedPart = new Motherboard(); CreatedPart.PartCategory.Id = 4; break;
                        case "SSD": CreatedPart = new Ssd(); CreatedPart.PartCategory.Id = 5; break;
                    }
                }

                // Пълнене на общите Properties
                CreatedPart.Name = txtName.Text;
                CreatedPart.Manufacturer = txtManufacturer.Text;
                CreatedPart.Price = numPrice.Value;
                CreatedPart.Quantity = (int)numQuantity.Value;

                // Пълнене на специфичните Properties
                if (CreatedPart is Cpu cpu)
                {
                    cpu.Cores = (int)numCpuCores.Value;
                    cpu.Socket = txtCpuSocket.Text;
                    cpu.ClockGhz = numCpuClock.Value;
                    cpu.TdpWatts = (int)numCpuTdp.Value;
                }
                else if (CreatedPart is Gpu gpu)
                {
                    gpu.VramSizeGb = (int)numGpuVram.Value;
                    gpu.MemoryType = txtGpuMemType.Text;
                    gpu.ClockMhz = (int)numGpuClock.Value;
                }
                else if (CreatedPart is Ram ram)
                {
                    ram.CapacityGb = (int)numRamCapacity.Value;
                    ram.SpeedMhz = (int)numRamSpeed.Value;
                    ram.Generation = txtRamGen.Text;
                    ram.CasLatency = txtRamCas.Text;
                }
                else if (CreatedPart is Motherboard mb)
                {
                    mb.Chipset = txtMbChipset.Text;
                    mb.FormFactor = txtMbForm.Text;
                    mb.Socket = txtMbSocket.Text;
                    mb.SupportedRamGen = txtMbRamGen.Text;
                }
                else if (CreatedPart is Ssd ssd)
                {
                    ssd.CapacityGb = (int)numSsdCapacity.Value;
                    ssd.FormFactor = txtSsdForm.Text;
                    ssd.ReadSpeedMb = (int)numSsdReadSpeed.Value;
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Грешка при събиране на данните: {ex.Message}", "Валидация", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // ⚡ СЪБИТИЕ НА БУТОНА ОТКАЗ
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        // ========================================================
        // 🛠️ МЕТОДИ ЗА СКРИВАНЕ И ПОКАЗВАНЕ НА СПЕЦИФИЧНИТЕ ПОЛЕТА
        // ========================================================
        private void HideAllSpecificFields()
        {
            ToggleCpuFields(false);
            ToggleGpuFields(false);
            ToggleRamFields(false);
            ToggleMbFields(false);
            ToggleSsdFields(false);
        }

        private void ToggleCpuFields(bool visible)
        {
            numCpuCores.Visible = visible;
            txtCpuSocket.Visible = visible;
            numCpuClock.Visible = visible;
            numCpuTdp.Visible = visible;

            lblCpuCores.Visible = visible;
            lblCpuSocket.Visible = visible;
            lblCpuClock.Visible = visible;
            lblCpuTdp.Visible = visible;
        }

        private void ToggleGpuFields(bool visible)
        {
            numGpuVram.Visible = visible;
            txtGpuMemType.Visible = visible;
            numGpuClock.Visible = visible;

            lblGpuVram.Visible = visible;
            lblGpuMemType.Visible = visible;
            lblGpuClock.Visible = visible;
        }

        private void ToggleRamFields(bool visible)
        {
            numRamCapacity.Visible = visible;
            numRamSpeed.Visible = visible;
            txtRamGen.Visible = visible;
            txtRamCas.Visible = visible;

            lblRamCapacity.Visible = visible;
            lblRamSpeed.Visible = visible;
            lblRamGen.Visible = visible;
            lblRamCas.Visible = visible;
        }

        private void ToggleMbFields(bool visible)
        {
            txtMbChipset.Visible = visible;
            txtMbForm.Visible = visible;
            txtMbSocket.Visible = visible;
            txtMbRamGen.Visible = visible;

            lblMbChipset.Visible = visible;
            lblMbForm.Visible = visible;
            lblMbSocket.Visible = visible;
            lblMbRamGen.Visible = visible;
        }

        private void ToggleSsdFields(bool visible)
        {
            numSsdCapacity.Visible = visible;
            txtSsdForm.Visible = visible;
            numSsdReadSpeed.Visible = visible;

            lblSsdCapacity.Visible = visible;
            lblSsdForm.Visible = visible;
            lblSsdReadSpeed.Visible = visible;
        }
    }
}