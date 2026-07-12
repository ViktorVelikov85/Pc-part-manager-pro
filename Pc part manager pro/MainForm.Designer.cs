namespace Pc_part_manager_pro
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            label1 = new Label();
            txtSearch = new TextBox();
            label2 = new Label();
            cmbCategory = new ComboBox();
            dgvParts = new DataGridView();
            colName = new DataGridViewTextBoxColumn();
            colManufacturer = new DataGridViewTextBoxColumn();
            colQuantity = new DataGridViewTextBoxColumn();
            colPrice = new DataGridViewTextBoxColumn();
            colType = new DataGridViewTextBoxColumn();
            btnAdd = new Button();
            btnEdit = new Button();
            btnDelete = new Button();
            btnClearSearch = new Button();
            label3 = new Label();
            cmbSort = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)dgvParts).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14.25F);
            label1.Location = new Point(29, 12);
            label1.Name = "label1";
            label1.Size = new Size(93, 25);
            label1.TabIndex = 0;
            label1.Text = "Търсене :";
            // 
            // txtSearch
            // 
            txtSearch.Font = new Font("Segoe UI", 14.25F);
            txtSearch.Location = new Point(29, 43);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Въведи текст за търсене";
            txtSearch.Size = new Size(361, 33);
            txtSearch.TabIndex = 1;
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 14.25F);
            label2.Location = new Point(487, 51);
            label2.Name = "label2";
            label2.Size = new Size(148, 25);
            label2.TabIndex = 2;
            label2.Text = "Филтър по тип :";
            // 
            // cmbCategory
            // 
            cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategory.Font = new Font("Segoe UI", 14.25F);
            cmbCategory.FormattingEnabled = true;
            cmbCategory.Location = new Point(641, 48);
            cmbCategory.Name = "cmbCategory";
            cmbCategory.Size = new Size(221, 33);
            cmbCategory.TabIndex = 3;
            cmbCategory.SelectedIndexChanged += cmbCategory_SelectedIndexChanged;
            // 
            // dgvParts
            // 
            dgvParts.AllowUserToAddRows = false;
            dgvParts.AllowUserToDeleteRows = false;
            dgvParts.AllowUserToResizeColumns = false;
            dgvParts.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvParts.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvParts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvParts.Columns.AddRange(new DataGridViewColumn[] { colName, colManufacturer, colQuantity, colPrice, colType });
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = SystemColors.Window;
            dataGridViewCellStyle4.Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle4.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.False;
            dgvParts.DefaultCellStyle = dataGridViewCellStyle4;
            dgvParts.Location = new Point(29, 90);
            dgvParts.MultiSelect = false;
            dgvParts.Name = "dgvParts";
            dgvParts.ReadOnly = true;
            dgvParts.RowHeadersVisible = false;
            dgvParts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvParts.Size = new Size(833, 421);
            dgvParts.TabIndex = 4;
            dgvParts.CellDoubleClick += dgvParts_CellDoubleClick;
            // 
            // colName
            // 
            colName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colName.DataPropertyName = "Name";
            colName.HeaderText = "Наименование";
            colName.Name = "colName";
            colName.ReadOnly = true;
            // 
            // colManufacturer
            // 
            colManufacturer.DataPropertyName = "Manufacturer";
            colManufacturer.HeaderText = "Производител";
            colManufacturer.Name = "colManufacturer";
            colManufacturer.ReadOnly = true;
            colManufacturer.Width = 200;
            // 
            // colQuantity
            // 
            colQuantity.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colQuantity.DataPropertyName = "Quantity";
            colQuantity.HeaderText = "Наличност (бр.)";
            colQuantity.Name = "colQuantity";
            colQuantity.ReadOnly = true;
            // 
            // colPrice
            // 
            colPrice.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colPrice.DataPropertyName = "Price";
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "C2";
            colPrice.DefaultCellStyle = dataGridViewCellStyle2;
            colPrice.HeaderText = "Цена (€)";
            colPrice.Name = "colPrice";
            colPrice.ReadOnly = true;
            // 
            // colType
            // 
            colType.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            colType.DataPropertyName = "PartType";
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colType.DefaultCellStyle = dataGridViewCellStyle3;
            colType.HeaderText = "Тип компонент";
            colType.Name = "colType";
            colType.ReadOnly = true;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.LightGreen;
            btnAdd.Font = new Font("Segoe UI", 14.25F);
            btnAdd.Location = new Point(487, 517);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(121, 54);
            btnAdd.TabIndex = 5;
            btnAdd.Text = "Добави";
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnEdit
            // 
            btnEdit.BackColor = Color.LightBlue;
            btnEdit.Font = new Font("Segoe UI", 14.25F);
            btnEdit.Location = new Point(614, 517);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(121, 54);
            btnEdit.TabIndex = 5;
            btnEdit.Text = "Редактирай";
            btnEdit.UseVisualStyleBackColor = false;
            btnEdit.Click += btnEdit_Click;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.Coral;
            btnDelete.Font = new Font("Segoe UI", 14.25F);
            btnDelete.Location = new Point(741, 517);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(121, 54);
            btnDelete.TabIndex = 5;
            btnDelete.Text = "Изтрий";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnClearSearch
            // 
            btnClearSearch.BackColor = Color.Coral;
            btnClearSearch.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnClearSearch.Location = new Point(396, 43);
            btnClearSearch.Name = "btnClearSearch";
            btnClearSearch.Size = new Size(33, 33);
            btnClearSearch.TabIndex = 6;
            btnClearSearch.Text = "X";
            btnClearSearch.UseVisualStyleBackColor = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 14.25F);
            label3.Location = new Point(510, 12);
            label3.Name = "label3";
            label3.Size = new Size(125, 25);
            label3.TabIndex = 7;
            label3.Text = "Подреди по :";
            // 
            // cmbSort
            // 
            cmbSort.Font = new Font("Segoe UI", 14.25F);
            cmbSort.FormattingEnabled = true;
            cmbSort.Location = new Point(641, 9);
            cmbSort.Name = "cmbSort";
            cmbSort.Size = new Size(221, 33);
            cmbSort.TabIndex = 8;
            cmbSort.SelectedIndexChanged += cmbSort_SelectedIndexChanged;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(884, 583);
            Controls.Add(cmbSort);
            Controls.Add(label3);
            Controls.Add(btnClearSearch);
            Controls.Add(btnDelete);
            Controls.Add(btnEdit);
            Controls.Add(btnAdd);
            Controls.Add(dgvParts);
            Controls.Add(cmbCategory);
            Controls.Add(label2);
            Controls.Add(txtSearch);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "PC Part Manager Pro";
            Load += MainForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvParts).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtSearch;
        private Label label2;
        private ComboBox cmbCategory;
        private DataGridView dgvParts;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnDelete;
        private DataGridViewTextBoxColumn colName;
        private DataGridViewTextBoxColumn colManufacturer;
        private DataGridViewTextBoxColumn colQuantity;
        private DataGridViewTextBoxColumn colPrice;
        private DataGridViewTextBoxColumn colType;
        private Button btnClearSearch;
        private Label label3;
        private ComboBox cmbSort;
    }
}
