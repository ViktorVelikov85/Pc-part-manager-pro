namespace Pc_part_manager_pro
{
    partial class PartEditForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            cmbType = new ComboBox();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            txtName = new TextBox();
            txtManufacturer = new TextBox();
            numPrice = new NumericUpDown();
            numQuantity = new NumericUpDown();
            pnlSpecifics = new Panel();
            lblSsdReadSpeed = new Label();
            lblSsdForm = new Label();
            lblSsdCapacity = new Label();
            lblMbRamGen = new Label();
            lblMbSocket = new Label();
            lblMbForm = new Label();
            lblMbChipset = new Label();
            lblRamCas = new Label();
            lblRamGen = new Label();
            lblRamSpeed = new Label();
            lblRamCapacity = new Label();
            lblGpuClock = new Label();
            lblGpuMemType = new Label();
            lblGpuVram = new Label();
            lblCpuTdp = new Label();
            lblCpuClock = new Label();
            lblCpuSocket = new Label();
            lblCpuCores = new Label();
            numSsdReadSpeed = new NumericUpDown();
            numCpuTdp = new NumericUpDown();
            numGpuClock = new NumericUpDown();
            numCpuClock = new NumericUpDown();
            numSsdCapacity = new NumericUpDown();
            numRamCapacity = new NumericUpDown();
            numRamSpeed = new NumericUpDown();
            numGpuVram = new NumericUpDown();
            numCpuCores = new NumericUpDown();
            txtMbRamGen = new TextBox();
            txtMbSocket = new TextBox();
            txtSsdForm = new TextBox();
            txtMbForm = new TextBox();
            txtMbChipset = new TextBox();
            txtRamCas = new TextBox();
            txtRamGen = new TextBox();
            txtGpuMemType = new TextBox();
            txtCpuSocket = new TextBox();
            btnSave = new Button();
            btnCancel = new Button();
            ((System.ComponentModel.ISupportInitialize)numPrice).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numQuantity).BeginInit();
            pnlSpecifics.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numSsdReadSpeed).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numCpuTdp).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numGpuClock).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numCpuClock).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numSsdCapacity).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numRamCapacity).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numRamSpeed).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numGpuVram).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numCpuCores).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14.25F);
            label1.Location = new Point(12, 79);
            label1.Name = "label1";
            label1.Size = new Size(153, 25);
            label1.TabIndex = 0;
            label1.Text = "Тип компонент :";
            // 
            // cmbType
            // 
            cmbType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbType.Font = new Font("Segoe UI", 14.25F);
            cmbType.FormattingEnabled = true;
            cmbType.Items.AddRange(new object[] { "CPU", "GPU", "RAM", "Motherboard", "SSD" });
            cmbType.Location = new Point(171, 76);
            cmbType.Name = "cmbType";
            cmbType.Size = new Size(173, 33);
            cmbType.TabIndex = 1;
            cmbType.SelectedIndexChanged += cmbType_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 14.25F);
            label2.Location = new Point(13, 30);
            label2.Name = "label2";
            label2.Size = new Size(152, 25);
            label2.TabIndex = 0;
            label2.Text = "Наименование :";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 14.25F);
            label3.Location = new Point(13, 127);
            label3.Name = "label3";
            label3.Size = new Size(146, 25);
            label3.TabIndex = 0;
            label3.Text = "Производител :";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 14.25F);
            label4.Location = new Point(13, 178);
            label4.Name = "label4";
            label4.Size = new Size(93, 25);
            label4.TabIndex = 0;
            label4.Text = "Цена (€) :";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 14.25F);
            label5.Location = new Point(12, 226);
            label5.Name = "label5";
            label5.Size = new Size(203, 25);
            label5.TabIndex = 0;
            label5.Text = "Налично количество :";
            // 
            // txtName
            // 
            txtName.Font = new Font("Segoe UI", 14.25F);
            txtName.Location = new Point(171, 27);
            txtName.Name = "txtName";
            txtName.Size = new Size(614, 33);
            txtName.TabIndex = 2;
            // 
            // txtManufacturer
            // 
            txtManufacturer.Font = new Font("Segoe UI", 14.25F);
            txtManufacturer.Location = new Point(171, 127);
            txtManufacturer.Name = "txtManufacturer";
            txtManufacturer.Size = new Size(173, 33);
            txtManufacturer.TabIndex = 2;
            // 
            // numPrice
            // 
            numPrice.DecimalPlaces = 2;
            numPrice.Font = new Font("Segoe UI", 14.25F);
            numPrice.Location = new Point(112, 176);
            numPrice.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            numPrice.Name = "numPrice";
            numPrice.Size = new Size(120, 33);
            numPrice.TabIndex = 3;
            // 
            // numQuantity
            // 
            numQuantity.Font = new Font("Segoe UI", 14.25F);
            numQuantity.Location = new Point(221, 225);
            numQuantity.Maximum = new decimal(new int[] { 2500, 0, 0, 0 });
            numQuantity.Name = "numQuantity";
            numQuantity.Size = new Size(120, 33);
            numQuantity.TabIndex = 3;
            // 
            // pnlSpecifics
            // 
            pnlSpecifics.Controls.Add(lblSsdReadSpeed);
            pnlSpecifics.Controls.Add(lblSsdForm);
            pnlSpecifics.Controls.Add(lblSsdCapacity);
            pnlSpecifics.Controls.Add(lblMbRamGen);
            pnlSpecifics.Controls.Add(lblMbSocket);
            pnlSpecifics.Controls.Add(lblMbForm);
            pnlSpecifics.Controls.Add(lblMbChipset);
            pnlSpecifics.Controls.Add(lblRamCas);
            pnlSpecifics.Controls.Add(lblRamGen);
            pnlSpecifics.Controls.Add(lblRamSpeed);
            pnlSpecifics.Controls.Add(lblRamCapacity);
            pnlSpecifics.Controls.Add(lblGpuClock);
            pnlSpecifics.Controls.Add(lblGpuMemType);
            pnlSpecifics.Controls.Add(lblGpuVram);
            pnlSpecifics.Controls.Add(lblCpuTdp);
            pnlSpecifics.Controls.Add(lblCpuClock);
            pnlSpecifics.Controls.Add(lblCpuSocket);
            pnlSpecifics.Controls.Add(lblCpuCores);
            pnlSpecifics.Controls.Add(numSsdReadSpeed);
            pnlSpecifics.Controls.Add(numCpuTdp);
            pnlSpecifics.Controls.Add(numGpuClock);
            pnlSpecifics.Controls.Add(numCpuClock);
            pnlSpecifics.Controls.Add(numSsdCapacity);
            pnlSpecifics.Controls.Add(numRamCapacity);
            pnlSpecifics.Controls.Add(numRamSpeed);
            pnlSpecifics.Controls.Add(numGpuVram);
            pnlSpecifics.Controls.Add(numCpuCores);
            pnlSpecifics.Controls.Add(txtMbRamGen);
            pnlSpecifics.Controls.Add(txtMbSocket);
            pnlSpecifics.Controls.Add(txtSsdForm);
            pnlSpecifics.Controls.Add(txtMbForm);
            pnlSpecifics.Controls.Add(txtMbChipset);
            pnlSpecifics.Controls.Add(txtRamCas);
            pnlSpecifics.Controls.Add(txtRamGen);
            pnlSpecifics.Controls.Add(txtGpuMemType);
            pnlSpecifics.Controls.Add(txtCpuSocket);
            pnlSpecifics.Location = new Point(394, 66);
            pnlSpecifics.Name = "pnlSpecifics";
            pnlSpecifics.Size = new Size(391, 219);
            pnlSpecifics.TabIndex = 4;
            // 
            // lblSsdReadSpeed
            // 
            lblSsdReadSpeed.AutoSize = true;
            lblSsdReadSpeed.Font = new Font("Segoe UI", 14.25F);
            lblSsdReadSpeed.Location = new Point(10, 109);
            lblSsdReadSpeed.Name = "lblSsdReadSpeed";
            lblSsdReadSpeed.Size = new Size(152, 25);
            lblSsdReadSpeed.TabIndex = 0;
            lblSsdReadSpeed.Text = "Скорост четене:";
            // 
            // lblSsdForm
            // 
            lblSsdForm.AutoSize = true;
            lblSsdForm.Font = new Font("Segoe UI", 14.25F);
            lblSsdForm.Location = new Point(10, 61);
            lblSsdForm.Name = "lblSsdForm";
            lblSsdForm.Size = new Size(132, 25);
            lblSsdForm.TabIndex = 1;
            lblSsdForm.Text = "Форм фактор:";
            // 
            // lblSsdCapacity
            // 
            lblSsdCapacity.AutoSize = true;
            lblSsdCapacity.Font = new Font("Segoe UI", 14.25F);
            lblSsdCapacity.Location = new Point(10, 13);
            lblSsdCapacity.Name = "lblSsdCapacity";
            lblSsdCapacity.Size = new Size(147, 25);
            lblSsdCapacity.TabIndex = 2;
            lblSsdCapacity.Text = "Капацитет (GB):";
            // 
            // lblMbRamGen
            // 
            lblMbRamGen.AutoSize = true;
            lblMbRamGen.Font = new Font("Segoe UI", 14.25F);
            lblMbRamGen.Location = new Point(10, 158);
            lblMbRamGen.Name = "lblMbRamGen";
            lblMbRamGen.Size = new Size(173, 25);
            lblMbRamGen.TabIndex = 3;
            lblMbRamGen.Text = "Поддържана RAM:";
            // 
            // lblMbSocket
            // 
            lblMbSocket.AutoSize = true;
            lblMbSocket.Font = new Font("Segoe UI", 14.25F);
            lblMbSocket.Location = new Point(10, 109);
            lblMbSocket.Name = "lblMbSocket";
            lblMbSocket.Size = new Size(66, 25);
            lblMbSocket.TabIndex = 4;
            lblMbSocket.Text = "Сокет:";
            // 
            // lblMbForm
            // 
            lblMbForm.AutoSize = true;
            lblMbForm.Font = new Font("Segoe UI", 14.25F);
            lblMbForm.Location = new Point(10, 61);
            lblMbForm.Name = "lblMbForm";
            lblMbForm.Size = new Size(132, 25);
            lblMbForm.TabIndex = 5;
            lblMbForm.Text = "Форм фактор:";
            // 
            // lblMbChipset
            // 
            lblMbChipset.AutoSize = true;
            lblMbChipset.Font = new Font("Segoe UI", 14.25F);
            lblMbChipset.Location = new Point(10, 13);
            lblMbChipset.Name = "lblMbChipset";
            lblMbChipset.Size = new Size(78, 25);
            lblMbChipset.TabIndex = 6;
            lblMbChipset.Text = "Чипсет:";
            // 
            // lblRamCas
            // 
            lblRamCas.AutoSize = true;
            lblRamCas.Font = new Font("Segoe UI", 14.25F);
            lblRamCas.Location = new Point(10, 158);
            lblRamCas.Name = "lblRamCas";
            lblRamCas.Size = new Size(154, 25);
            lblRamCas.TabIndex = 7;
            lblRamCas.Text = "CAS Латентност:";
            // 
            // lblRamGen
            // 
            lblRamGen.AutoSize = true;
            lblRamGen.Font = new Font("Segoe UI", 14.25F);
            lblRamGen.Location = new Point(10, 109);
            lblRamGen.Name = "lblRamGen";
            lblRamGen.Size = new Size(163, 25);
            lblRamGen.TabIndex = 8;
            lblRamGen.Text = "Генерация (DDR):";
            // 
            // lblRamSpeed
            // 
            lblRamSpeed.AutoSize = true;
            lblRamSpeed.Font = new Font("Segoe UI", 14.25F);
            lblRamSpeed.Location = new Point(10, 61);
            lblRamSpeed.Name = "lblRamSpeed";
            lblRamSpeed.Size = new Size(143, 25);
            lblRamSpeed.TabIndex = 9;
            lblRamSpeed.Text = "Скорост (MHz):";
            // 
            // lblRamCapacity
            // 
            lblRamCapacity.AutoSize = true;
            lblRamCapacity.Font = new Font("Segoe UI", 14.25F);
            lblRamCapacity.Location = new Point(10, 13);
            lblRamCapacity.Name = "lblRamCapacity";
            lblRamCapacity.Size = new Size(147, 25);
            lblRamCapacity.TabIndex = 10;
            lblRamCapacity.Text = "Капацитет (GB):";
            // 
            // lblGpuClock
            // 
            lblGpuClock.AutoSize = true;
            lblGpuClock.Font = new Font("Segoe UI", 14.25F);
            lblGpuClock.Location = new Point(10, 109);
            lblGpuClock.Name = "lblGpuClock";
            lblGpuClock.Size = new Size(141, 25);
            lblGpuClock.TabIndex = 11;
            lblGpuClock.Text = "Честота (MHz):";
            // 
            // lblGpuMemType
            // 
            lblGpuMemType.AutoSize = true;
            lblGpuMemType.Font = new Font("Segoe UI", 14.25F);
            lblGpuMemType.Location = new Point(10, 61);
            lblGpuMemType.Name = "lblGpuMemType";
            lblGpuMemType.Size = new Size(105, 25);
            lblGpuMemType.TabIndex = 12;
            lblGpuMemType.Text = "Тип памет:";
            // 
            // lblGpuVram
            // 
            lblGpuVram.AutoSize = true;
            lblGpuVram.Font = new Font("Segoe UI", 14.25F);
            lblGpuVram.Location = new Point(10, 13);
            lblGpuVram.Name = "lblGpuVram";
            lblGpuVram.Size = new Size(109, 25);
            lblGpuVram.TabIndex = 13;
            lblGpuVram.Text = "VRAM (GB):";
            // 
            // lblCpuTdp
            // 
            lblCpuTdp.AutoSize = true;
            lblCpuTdp.Font = new Font("Segoe UI", 14.25F);
            lblCpuTdp.Location = new Point(10, 158);
            lblCpuTdp.Name = "lblCpuTdp";
            lblCpuTdp.Size = new Size(134, 25);
            lblCpuTdp.TabIndex = 14;
            lblCpuTdp.Text = "Мощност (W):";
            // 
            // lblCpuClock
            // 
            lblCpuClock.AutoSize = true;
            lblCpuClock.Font = new Font("Segoe UI", 14.25F);
            lblCpuClock.Location = new Point(10, 109);
            lblCpuClock.Name = "lblCpuClock";
            lblCpuClock.Size = new Size(137, 25);
            lblCpuClock.TabIndex = 15;
            lblCpuClock.Text = "Честота (GHz):";
            // 
            // lblCpuSocket
            // 
            lblCpuSocket.AutoSize = true;
            lblCpuSocket.Font = new Font("Segoe UI", 14.25F);
            lblCpuSocket.Location = new Point(10, 61);
            lblCpuSocket.Name = "lblCpuSocket";
            lblCpuSocket.Size = new Size(66, 25);
            lblCpuSocket.TabIndex = 16;
            lblCpuSocket.Text = "Сокет:";
            // 
            // lblCpuCores
            // 
            lblCpuCores.AutoSize = true;
            lblCpuCores.Font = new Font("Segoe UI", 14.25F);
            lblCpuCores.Location = new Point(10, 13);
            lblCpuCores.Name = "lblCpuCores";
            lblCpuCores.Size = new Size(106, 25);
            lblCpuCores.TabIndex = 17;
            lblCpuCores.Text = "Брой ядра:";
            // 
            // numSsdReadSpeed
            // 
            numSsdReadSpeed.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            numSsdReadSpeed.Location = new Point(198, 109);
            numSsdReadSpeed.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            numSsdReadSpeed.Name = "numSsdReadSpeed";
            numSsdReadSpeed.Size = new Size(120, 33);
            numSsdReadSpeed.TabIndex = 0;
            // 
            // numCpuTdp
            // 
            numCpuTdp.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            numCpuTdp.Location = new Point(198, 158);
            numCpuTdp.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numCpuTdp.Name = "numCpuTdp";
            numCpuTdp.Size = new Size(120, 33);
            numCpuTdp.TabIndex = 0;
            // 
            // numGpuClock
            // 
            numGpuClock.DecimalPlaces = 2;
            numGpuClock.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            numGpuClock.Location = new Point(198, 109);
            numGpuClock.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numGpuClock.Name = "numGpuClock";
            numGpuClock.Size = new Size(120, 33);
            numGpuClock.TabIndex = 0;
            // 
            // numCpuClock
            // 
            numCpuClock.DecimalPlaces = 2;
            numCpuClock.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            numCpuClock.Location = new Point(198, 109);
            numCpuClock.Name = "numCpuClock";
            numCpuClock.Size = new Size(120, 33);
            numCpuClock.TabIndex = 0;
            // 
            // numSsdCapacity
            // 
            numSsdCapacity.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            numSsdCapacity.Location = new Point(198, 13);
            numSsdCapacity.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            numSsdCapacity.Name = "numSsdCapacity";
            numSsdCapacity.Size = new Size(120, 33);
            numSsdCapacity.TabIndex = 0;
            // 
            // numRamCapacity
            // 
            numRamCapacity.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            numRamCapacity.Location = new Point(198, 13);
            numRamCapacity.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numRamCapacity.Name = "numRamCapacity";
            numRamCapacity.Size = new Size(120, 33);
            numRamCapacity.TabIndex = 0;
            // 
            // numRamSpeed
            // 
            numRamSpeed.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            numRamSpeed.Location = new Point(198, 61);
            numRamSpeed.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numRamSpeed.Name = "numRamSpeed";
            numRamSpeed.Size = new Size(120, 33);
            numRamSpeed.TabIndex = 0;
            // 
            // numGpuVram
            // 
            numGpuVram.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            numGpuVram.Location = new Point(198, 13);
            numGpuVram.Name = "numGpuVram";
            numGpuVram.Size = new Size(120, 33);
            numGpuVram.TabIndex = 0;
            // 
            // numCpuCores
            // 
            numCpuCores.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            numCpuCores.Location = new Point(198, 13);
            numCpuCores.Name = "numCpuCores";
            numCpuCores.Size = new Size(120, 33);
            numCpuCores.TabIndex = 0;
            // 
            // txtMbRamGen
            // 
            txtMbRamGen.Font = new Font("Segoe UI", 14.25F);
            txtMbRamGen.Location = new Point(198, 158);
            txtMbRamGen.Name = "txtMbRamGen";
            txtMbRamGen.Size = new Size(173, 33);
            txtMbRamGen.TabIndex = 2;
            // 
            // txtMbSocket
            // 
            txtMbSocket.Font = new Font("Segoe UI", 14.25F);
            txtMbSocket.Location = new Point(198, 109);
            txtMbSocket.Name = "txtMbSocket";
            txtMbSocket.Size = new Size(173, 33);
            txtMbSocket.TabIndex = 2;
            // 
            // txtSsdForm
            // 
            txtSsdForm.Font = new Font("Segoe UI", 14.25F);
            txtSsdForm.Location = new Point(198, 61);
            txtSsdForm.Name = "txtSsdForm";
            txtSsdForm.Size = new Size(173, 33);
            txtSsdForm.TabIndex = 2;
            // 
            // txtMbForm
            // 
            txtMbForm.Font = new Font("Segoe UI", 14.25F);
            txtMbForm.Location = new Point(198, 61);
            txtMbForm.Name = "txtMbForm";
            txtMbForm.Size = new Size(173, 33);
            txtMbForm.TabIndex = 2;
            // 
            // txtMbChipset
            // 
            txtMbChipset.Font = new Font("Segoe UI", 14.25F);
            txtMbChipset.Location = new Point(198, 13);
            txtMbChipset.Name = "txtMbChipset";
            txtMbChipset.Size = new Size(173, 33);
            txtMbChipset.TabIndex = 2;
            // 
            // txtRamCas
            // 
            txtRamCas.Font = new Font("Segoe UI", 14.25F);
            txtRamCas.Location = new Point(198, 158);
            txtRamCas.Name = "txtRamCas";
            txtRamCas.Size = new Size(173, 33);
            txtRamCas.TabIndex = 2;
            // 
            // txtRamGen
            // 
            txtRamGen.Font = new Font("Segoe UI", 14.25F);
            txtRamGen.Location = new Point(198, 109);
            txtRamGen.Name = "txtRamGen";
            txtRamGen.Size = new Size(173, 33);
            txtRamGen.TabIndex = 2;
            // 
            // txtGpuMemType
            // 
            txtGpuMemType.Font = new Font("Segoe UI", 14.25F);
            txtGpuMemType.Location = new Point(198, 61);
            txtGpuMemType.Name = "txtGpuMemType";
            txtGpuMemType.Size = new Size(173, 33);
            txtGpuMemType.TabIndex = 2;
            // 
            // txtCpuSocket
            // 
            txtCpuSocket.Font = new Font("Segoe UI", 14.25F);
            txtCpuSocket.Location = new Point(198, 61);
            txtCpuSocket.Name = "txtCpuSocket";
            txtCpuSocket.Size = new Size(173, 33);
            txtCpuSocket.TabIndex = 2;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.LightGreen;
            btnSave.Font = new Font("Segoe UI", 14.25F);
            btnSave.Location = new Point(270, 301);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(115, 51);
            btnSave.TabIndex = 5;
            btnSave.Text = "Запази";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.Coral;
            btnCancel.Font = new Font("Segoe UI", 14.25F);
            btnCancel.Location = new Point(432, 301);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(115, 51);
            btnCancel.TabIndex = 5;
            btnCancel.Text = "Отказ";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // PartEditForm
            // 
            AcceptButton = btnSave;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(807, 396);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(pnlSpecifics);
            Controls.Add(numQuantity);
            Controls.Add(numPrice);
            Controls.Add(txtManufacturer);
            Controls.Add(txtName);
            Controls.Add(cmbType);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "PartEditForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Редакторане на компютърна част";
            ((System.ComponentModel.ISupportInitialize)numPrice).EndInit();
            ((System.ComponentModel.ISupportInitialize)numQuantity).EndInit();
            pnlSpecifics.ResumeLayout(false);
            pnlSpecifics.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numSsdReadSpeed).EndInit();
            ((System.ComponentModel.ISupportInitialize)numCpuTdp).EndInit();
            ((System.ComponentModel.ISupportInitialize)numGpuClock).EndInit();
            ((System.ComponentModel.ISupportInitialize)numCpuClock).EndInit();
            ((System.ComponentModel.ISupportInitialize)numSsdCapacity).EndInit();
            ((System.ComponentModel.ISupportInitialize)numRamCapacity).EndInit();
            ((System.ComponentModel.ISupportInitialize)numRamSpeed).EndInit();
            ((System.ComponentModel.ISupportInitialize)numGpuVram).EndInit();
            ((System.ComponentModel.ISupportInitialize)numCpuCores).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private ComboBox cmbType;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private TextBox txtName;
        private TextBox txtManufacturer;
        private NumericUpDown numPrice;
        private NumericUpDown numQuantity;
        private Panel pnlSpecifics;

        // CPU Labels
        private Label lblCpuCores;
        private Label lblCpuSocket;
        private Label lblCpuClock;
        private Label lblCpuTdp;

        // GPU Labels
        private Label lblGpuVram;
        private Label lblGpuMemType;
        private Label lblGpuClock;

        // RAM Labels
        private Label lblRamCapacity;
        private Label lblRamSpeed;
        private Label lblRamGen;
        private Label lblRamCas;

        // Motherboard Labels
        private Label lblMbChipset;
        private Label lblMbForm;
        private Label lblMbSocket;
        private Label lblMbRamGen;

        // SSD Labels
        private Label lblSsdCapacity;
        private Label lblSsdForm;
        private Label lblSsdReadSpeed;

        private NumericUpDown numCpuTdp;
        private NumericUpDown numCpuClock;
        private NumericUpDown numCpuCores;
        private TextBox txtCpuSocket;
        private NumericUpDown numGpuClock;
        private NumericUpDown numRamCapacity;
        private NumericUpDown numRamSpeed;
        private NumericUpDown numGpuVram;
        private TextBox txtRamGen;
        private TextBox txtGpuMemType;
        private NumericUpDown numSsdReadSpeed;
        private NumericUpDown numSsdCapacity;
        private TextBox txtMbRamGen;
        private TextBox txtMbSocket;
        private TextBox txtSsdForm;
        private TextBox txtMbForm;
        private TextBox txtMbChipset;
        private TextBox txtRamCas;
        private Button btnSave;
        private Button btnCancel;
    }
}