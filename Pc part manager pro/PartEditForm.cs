using Pc_part_manager_pro.Models;
using Pc_part_manager_pro.Models.PcParts;

namespace Pc_part_manager_pro
{
    public partial class PartEditForm : Form
    {
        public ComputerPart CreatedPart { get; private set; }
        private readonly bool _isEditMode = false;

        // Конструктор за ДОБАВЯНЕ на нов компонент
        public PartEditForm()
        {
            InitializeComponent();
            _isEditMode = false;

            HideAllSpecificFields();
            SetupNumericAutoSelect();

            if (cmbType.Items.Count > 0)
            {
                cmbType.SelectedIndex = 0;
            }
            ApplyCurrentTheme();
            // Закачаме събитието, което се изпълнява ВЕДНАГА след като формата се покаже
            this.Shown += PartEditForm_Shown;
        }

        // Конструктор за РЕДАКТИРАНЕ на съществуващ компонент
        public PartEditForm(ComputerPart partToEdit)
        {
            InitializeComponent();
            _isEditMode = true;
            CreatedPart = partToEdit;

            HideAllSpecificFields();
            SetupNumericAutoSelect();

            txtName.Text = partToEdit.Name;
            txtManufacturer.Text = partToEdit.Manufacturer;
            numPrice.Value = partToEdit.Price;
            numQuantity.Value = partToEdit.Quantity;

            string typeForCombo = partToEdit.PartType.ToUpper();
            if (typeForCombo == "MOTHERBOARD") typeForCombo = "Motherboard";

            cmbType.SelectedItem = typeForCombo;
            cmbType.Enabled = false;

            UpdateSpecificPanelVisibility(typeForCombo);
            PopulateSpecificFields(partToEdit);
            ApplyCurrentTheme();
            // Закачаме събитието, което се изпълнява ВЕДНАГА след като формата се покаже
            this.Shown += PartEditForm_Shown;
        }

        // Премахва първоначалния автоматичен фокус от абсолютно всички контроли
        private void PartEditForm_Shown(object sender, EventArgs e)
        {
            this.ActiveControl = null;
        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbType.SelectedItem != null)
            {
                UpdateSpecificPanelVisibility(cmbType.SelectedItem.ToString());
            }
        }

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

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // --- 1. ПЪЛНА ВАЛИДАЦИЯ ПРЕДИ ЗАТВАРЯНЕ НА ФОРМАТА (Задържа потребителя тук при грешка) ---
                if (string.IsNullOrWhiteSpace(txtName.Text))
                {
                    MessageBox.Show("Името на компонента не може да бъде празно!", "Валидация", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtName.Focus();
                    return; // Спира изпълнението и ОСТАВЯ потребителя във формата
                }

                if (string.IsNullOrWhiteSpace(txtManufacturer.Text))
                {
                    MessageBox.Show("Производителят не може да бъде празен!", "Валидация", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtManufacturer.Focus();
                    return;
                }

                if (numPrice.Value <= 0)
                {
                    MessageBox.Show("Цената трябва да бъде по-голяма от 0.00 €!", "Валидация", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    numPrice.Focus();
                    return;
                }

                if (numQuantity.Value < 0)
                {
                    MessageBox.Show("Количеството не може да бъде отрицателно число!", "Валидация", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    numQuantity.Focus();
                    return;
                }

                // --- 2. СЪЗДАВАНЕ НА ОБЕКТ (АКО НЕ СМЕ В РЕЖИМ НА РЕДАКЦИЯ) ---
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

                // --- 3. ЗАПИС НА БАЗОВИТЕ СВОЙСТВА ---
                CreatedPart.Name = txtName.Text.Trim();
                CreatedPart.Manufacturer = txtManufacturer.Text.Trim();
                CreatedPart.Price = numPrice.Value;
                CreatedPart.Quantity = (int)numQuantity.Value;

                // --- 4. ЗАПИС НА СПЕЦИФИЧНИТЕ СВОЙСТВА (ТВОЯ ОРИГИНАЛЕН КОД) ---
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

                // Едва сега, когато всичко е успешно попълнено и валидирано, затваряме формата с OK
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Грешка при събиране на данните: {ex.Message}", "Валидация", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void SetupNumericAutoSelect()
        {
            foreach (Control ctrl in this.Controls)
            {
                SubscribeNumeric(ctrl);
            }

            if (this.Controls.Find("pnlSpecifics", true).Length > 0)
            {
                Control pnl = this.Controls.Find("pnlSpecifics", true)[0];
                foreach (Control ctrl in pnl.Controls)
                {
                    SubscribeNumeric(ctrl);
                }
            }
        }

        private void SubscribeNumeric(Control ctrl)
        {
            if (ctrl is NumericUpDown num)
            {
                num.Enter += (s, e) =>
                {
                    this.BeginInvoke(new Action(() => num.Select(0, num.Text.Length)));
                };
            }
        }

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
        private void ApplyCurrentTheme()
        {
            Color darkBg = Color.FromArgb(54, 57, 63);
            Color inputBg = Color.FromArgb(47, 49, 54);

            if (MainForm.IsDarkMode)
            {
                ApplyEditFormTheme(this, darkBg, Color.White, inputBg);

                // Директен фикс за ComboBox-а във формата за редактиране/добавяне
                cmbType.FlatStyle = FlatStyle.Flat;
                cmbType.BackColor = inputBg;
                cmbType.ForeColor = Color.White;
                cmbType.Invalidate();
            }
            else
            {
                ApplyEditFormTheme(this, SystemColors.Control, SystemColors.ControlText, SystemColors.Window);

                cmbType.FlatStyle = FlatStyle.Standard;
                cmbType.BackColor = SystemColors.Window;
                cmbType.ForeColor = SystemColors.ControlText;
            }
        }

        private void ApplyEditFormTheme(Control root, Color backColor, Color foreColor, Color inputBackColor)
        {
            root.BackColor = backColor;

            foreach (Control child in root.Controls)
            {
                // ИЗРИЧНО ЗАПАЗВАНЕ НА ЦВЕТОВЕТЕ НА БУТОНИТЕ ЗА ЗАПАЗВАНЕ И ОТКАЗ (без бели кантове)
                if (child is Button btnCheck && (btnCheck.Name == "btnSave" || btnCheck.Name == "btnCancel"))
                {
                    btnCheck.FlatStyle = MainForm.IsDarkMode ? FlatStyle.Flat : FlatStyle.Standard;
                    btnCheck.FlatAppearance.BorderSize = MainForm.IsDarkMode ? 0 : 1;
                    btnCheck.BackColor = btnCheck.Name == "btnSave" ? Color.LightGreen : Color.Coral;
                    btnCheck.ForeColor = Color.Black;
                    continue;
                }

                if (child is TextBox || child is NumericUpDown)
                {
                    child.BackColor = MainForm.IsDarkMode ? inputBackColor : SystemColors.Window;
                    child.ForeColor = MainForm.IsDarkMode ? foreColor : SystemColors.ControlText;
                }
                else if (child is ComboBox cmb)
                {
                    cmb.FlatStyle = MainForm.IsDarkMode ? FlatStyle.Flat : FlatStyle.Standard;
                    cmb.BackColor = MainForm.IsDarkMode ? inputBackColor : SystemColors.Window;
                    cmb.ForeColor = MainForm.IsDarkMode ? foreColor : SystemColors.ControlText;
                    cmb.Invalidate();
                }
                else if (child is Label)
                {
                    child.ForeColor = foreColor;
                }
                else if (child is Button btn)
                {
                    btn.FlatStyle = MainForm.IsDarkMode ? FlatStyle.Flat : FlatStyle.Standard;
                    btn.FlatAppearance.BorderSize = MainForm.IsDarkMode ? 0 : 1;
                    btn.BackColor = MainForm.IsDarkMode ? inputBackColor : SystemColors.Control;
                    btn.ForeColor = MainForm.IsDarkMode ? foreColor : SystemColors.ControlText;
                }

                // Продължаваме обхождането и в pnlSpecifics
                if (child.HasChildren && !(child is NumericUpDown) && !(child is ComboBox))
                {
                    ApplyEditFormTheme(child, backColor, foreColor, inputBackColor);
                }
            }
        }

    }
}