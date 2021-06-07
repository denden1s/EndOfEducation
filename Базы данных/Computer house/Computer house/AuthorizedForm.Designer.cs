
namespace Computer_house
{
    partial class AuthorizedForm
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AuthorizedForm));
      this.panel1 = new System.Windows.Forms.Panel();
      this.SDViewRadio = new System.Windows.Forms.RadioButton();
      this.PSUViewRadio = new System.Windows.Forms.RadioButton();
      this.CoolingSystemViewRadio = new System.Windows.Forms.RadioButton();
      this.RAMViewRadio = new System.Windows.Forms.RadioButton();
      this.ResetFilters = new System.Windows.Forms.Button();
      this.CasesViewRadio = new System.Windows.Forms.RadioButton();
      this.MothersViewRadio = new System.Windows.Forms.RadioButton();
      this.GPUViewRadio = new System.Windows.Forms.RadioButton();
      this.CPUViewRadio = new System.Windows.Forms.RadioButton();
      this.label2 = new System.Windows.Forms.Label();
      this.SearchInfo = new System.Windows.Forms.TextBox();
      this.panel2 = new System.Windows.Forms.Panel();
      this.AddProduct = new System.Windows.Forms.NumericUpDown();
      this.AllProductInfo = new System.Windows.Forms.RichTextBox();
      this.Move = new System.Windows.Forms.Button();
      this.AllInfoDatagridView = new System.Windows.Forms.DataGridView();
      this.menuStrip1 = new System.Windows.Forms.MenuStrip();
      this.настроитьIPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.перейтиВРазделРедактированияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.справкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.выйтиИзУчётнойЗаписиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.label1 = new System.Windows.Forms.Label();
      this.Product_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Product_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Count = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.panel1.SuspendLayout();
      this.panel2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.AddProduct)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.AllInfoDatagridView)).BeginInit();
      this.menuStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // panel1
      // 
      this.panel1.Anchor = System.Windows.Forms.AnchorStyles.Left;
      this.panel1.Controls.Add(this.SDViewRadio);
      this.panel1.Controls.Add(this.PSUViewRadio);
      this.panel1.Controls.Add(this.CoolingSystemViewRadio);
      this.panel1.Controls.Add(this.RAMViewRadio);
      this.panel1.Controls.Add(this.ResetFilters);
      this.panel1.Controls.Add(this.CasesViewRadio);
      this.panel1.Controls.Add(this.MothersViewRadio);
      this.panel1.Controls.Add(this.GPUViewRadio);
      this.panel1.Controls.Add(this.CPUViewRadio);
      this.panel1.Controls.Add(this.label2);
      this.panel1.Location = new System.Drawing.Point(12, 31);
      this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(200, 640);
      this.panel1.TabIndex = 0;
      // 
      // SDViewRadio
      // 
      this.SDViewRadio.Anchor = System.Windows.Forms.AnchorStyles.None;
      this.SDViewRadio.AutoSize = true;
      this.SDViewRadio.Font = new System.Drawing.Font("Malgun Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.SDViewRadio.Location = new System.Drawing.Point(13, 550);
      this.SDViewRadio.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.SDViewRadio.Name = "SDViewRadio";
      this.SDViewRadio.Size = new System.Drawing.Size(145, 32);
      this.SDViewRadio.TabIndex = 26;
      this.SDViewRadio.TabStop = true;
      this.SDViewRadio.Text = "Накопители";
      this.SDViewRadio.UseVisualStyleBackColor = true;
      this.SDViewRadio.CheckedChanged += new System.EventHandler(this.SDViewRadio_CheckedChanged);
      // 
      // PSUViewRadio
      // 
      this.PSUViewRadio.Anchor = System.Windows.Forms.AnchorStyles.None;
      this.PSUViewRadio.AutoSize = true;
      this.PSUViewRadio.Font = new System.Drawing.Font("Malgun Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.PSUViewRadio.Location = new System.Drawing.Point(13, 480);
      this.PSUViewRadio.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.PSUViewRadio.Name = "PSUViewRadio";
      this.PSUViewRadio.Size = new System.Drawing.Size(60, 32);
      this.PSUViewRadio.TabIndex = 25;
      this.PSUViewRadio.TabStop = true;
      this.PSUViewRadio.Text = "БП";
      this.PSUViewRadio.UseVisualStyleBackColor = true;
      this.PSUViewRadio.CheckedChanged += new System.EventHandler(this.PSUViewRadio_CheckedChanged);
      // 
      // CoolingSystemViewRadio
      // 
      this.CoolingSystemViewRadio.Anchor = System.Windows.Forms.AnchorStyles.None;
      this.CoolingSystemViewRadio.AutoSize = true;
      this.CoolingSystemViewRadio.Font = new System.Drawing.Font("Malgun Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.CoolingSystemViewRadio.Location = new System.Drawing.Point(16, 410);
      this.CoolingSystemViewRadio.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.CoolingSystemViewRadio.Name = "CoolingSystemViewRadio";
      this.CoolingSystemViewRadio.Size = new System.Drawing.Size(150, 32);
      this.CoolingSystemViewRadio.TabIndex = 24;
      this.CoolingSystemViewRadio.TabStop = true;
      this.CoolingSystemViewRadio.Text = "Охлаждение";
      this.CoolingSystemViewRadio.UseVisualStyleBackColor = true;
      this.CoolingSystemViewRadio.CheckedChanged += new System.EventHandler(this.CoolingSystemViewRadio_CheckedChanged);
      // 
      // RAMViewRadio
      // 
      this.RAMViewRadio.Anchor = System.Windows.Forms.AnchorStyles.None;
      this.RAMViewRadio.AutoSize = true;
      this.RAMViewRadio.Font = new System.Drawing.Font("Malgun Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.RAMViewRadio.Location = new System.Drawing.Point(16, 340);
      this.RAMViewRadio.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.RAMViewRadio.Name = "RAMViewRadio";
      this.RAMViewRadio.Size = new System.Drawing.Size(71, 32);
      this.RAMViewRadio.TabIndex = 23;
      this.RAMViewRadio.TabStop = true;
      this.RAMViewRadio.Text = "ОЗУ";
      this.RAMViewRadio.UseVisualStyleBackColor = true;
      this.RAMViewRadio.CheckedChanged += new System.EventHandler(this.RAMViewRadio_CheckedChanged);
      // 
      // ResetFilters
      // 
      this.ResetFilters.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.ResetFilters.Font = new System.Drawing.Font("Malgun Gothic", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ResetFilters.Location = new System.Drawing.Point(3, 593);
      this.ResetFilters.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.ResetFilters.Name = "ResetFilters";
      this.ResetFilters.Size = new System.Drawing.Size(195, 44);
      this.ResetFilters.TabIndex = 22;
      this.ResetFilters.Text = "Сбросить";
      this.ResetFilters.UseVisualStyleBackColor = true;
      this.ResetFilters.Click += new System.EventHandler(this.ResetFilters_Click);
      // 
      // CasesViewRadio
      // 
      this.CasesViewRadio.Anchor = System.Windows.Forms.AnchorStyles.None;
      this.CasesViewRadio.AutoSize = true;
      this.CasesViewRadio.Font = new System.Drawing.Font("Malgun Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.CasesViewRadio.Location = new System.Drawing.Point(16, 270);
      this.CasesViewRadio.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.CasesViewRadio.Name = "CasesViewRadio";
      this.CasesViewRadio.Size = new System.Drawing.Size(110, 32);
      this.CasesViewRadio.TabIndex = 21;
      this.CasesViewRadio.TabStop = true;
      this.CasesViewRadio.Text = "Корпуса";
      this.CasesViewRadio.UseVisualStyleBackColor = true;
      this.CasesViewRadio.CheckedChanged += new System.EventHandler(this.CasesViewRadio_CheckedChanged);
      // 
      // MothersViewRadio
      // 
      this.MothersViewRadio.Anchor = System.Windows.Forms.AnchorStyles.None;
      this.MothersViewRadio.AutoSize = true;
      this.MothersViewRadio.Font = new System.Drawing.Font("Malgun Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.MothersViewRadio.Location = new System.Drawing.Point(16, 199);
      this.MothersViewRadio.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.MothersViewRadio.Name = "MothersViewRadio";
      this.MothersViewRadio.Size = new System.Drawing.Size(135, 32);
      this.MothersViewRadio.TabIndex = 20;
      this.MothersViewRadio.TabStop = true;
      this.MothersViewRadio.Text = "Мат. платы";
      this.MothersViewRadio.UseVisualStyleBackColor = true;
      this.MothersViewRadio.CheckedChanged += new System.EventHandler(this.MothersViewRadio_CheckedChanged);
      // 
      // GPUViewRadio
      // 
      this.GPUViewRadio.Anchor = System.Windows.Forms.AnchorStyles.None;
      this.GPUViewRadio.AutoSize = true;
      this.GPUViewRadio.Font = new System.Drawing.Font("Malgun Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.GPUViewRadio.Location = new System.Drawing.Point(16, 129);
      this.GPUViewRadio.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.GPUViewRadio.Name = "GPUViewRadio";
      this.GPUViewRadio.Size = new System.Drawing.Size(145, 32);
      this.GPUViewRadio.TabIndex = 19;
      this.GPUViewRadio.TabStop = true;
      this.GPUViewRadio.Text = "Видеокарты";
      this.GPUViewRadio.UseVisualStyleBackColor = true;
      this.GPUViewRadio.CheckedChanged += new System.EventHandler(this.GPUViewRadio_CheckedChanged);
      // 
      // CPUViewRadio
      // 
      this.CPUViewRadio.Anchor = System.Windows.Forms.AnchorStyles.None;
      this.CPUViewRadio.AutoSize = true;
      this.CPUViewRadio.Font = new System.Drawing.Font("Malgun Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.CPUViewRadio.Location = new System.Drawing.Point(16, 59);
      this.CPUViewRadio.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.CPUViewRadio.Name = "CPUViewRadio";
      this.CPUViewRadio.Size = new System.Drawing.Size(151, 32);
      this.CPUViewRadio.TabIndex = 18;
      this.CPUViewRadio.TabStop = true;
      this.CPUViewRadio.Text = "Процессоры";
      this.CPUViewRadio.UseVisualStyleBackColor = true;
      this.CPUViewRadio.CheckedChanged += new System.EventHandler(this.CPUViewRadio_CheckedChanged);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Font = new System.Drawing.Font("Malgun Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label2.Location = new System.Drawing.Point(45, 9);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(103, 28);
      this.label2.TabIndex = 6;
      this.label2.Text = "Фильтры";
      // 
      // SearchInfo
      // 
      this.SearchInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.SearchInfo.Font = new System.Drawing.Font("Malgun Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.SearchInfo.Location = new System.Drawing.Point(373, 38);
      this.SearchInfo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.SearchInfo.Name = "SearchInfo";
      this.SearchInfo.Size = new System.Drawing.Size(383, 30);
      this.SearchInfo.TabIndex = 1;
      this.SearchInfo.TextChanged += new System.EventHandler(this.SearchInfo_TextChanged);
      // 
      // panel2
      // 
      this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.panel2.Controls.Add(this.AddProduct);
      this.panel2.Controls.Add(this.AllProductInfo);
      this.panel2.Controls.Add(this.Move);
      this.panel2.Location = new System.Drawing.Point(793, 30);
      this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(531, 652);
      this.panel2.TabIndex = 2;
      // 
      // AddProduct
      // 
      this.AddProduct.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.AddProduct.Font = new System.Drawing.Font("Malgun Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.AddProduct.Location = new System.Drawing.Point(3, 606);
      this.AddProduct.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.AddProduct.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.AddProduct.Name = "AddProduct";
      this.AddProduct.Size = new System.Drawing.Size(340, 34);
      this.AddProduct.TabIndex = 6;
      this.AddProduct.ThousandsSeparator = true;
      this.AddProduct.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
      // 
      // AllProductInfo
      // 
      this.AllProductInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.AllProductInfo.Font = new System.Drawing.Font("Malgun Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.AllProductInfo.Location = new System.Drawing.Point(3, 4);
      this.AllProductInfo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.AllProductInfo.Name = "AllProductInfo";
      this.AllProductInfo.ReadOnly = true;
      this.AllProductInfo.Size = new System.Drawing.Size(521, 584);
      this.AllProductInfo.TabIndex = 4;
      this.AllProductInfo.Text = "";
      // 
      // Move
      // 
      this.Move.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.Move.Font = new System.Drawing.Font("Malgun Gothic", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.Move.Location = new System.Drawing.Point(359, 593);
      this.Move.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.Move.Name = "Move";
      this.Move.Size = new System.Drawing.Size(169, 57);
      this.Move.TabIndex = 3;
      this.Move.Text = "Провести";
      this.Move.UseVisualStyleBackColor = true;
      this.Move.Click += new System.EventHandler(this.Move_Click);
      // 
      // AllInfoDatagridView
      // 
      this.AllInfoDatagridView.AllowUserToAddRows = false;
      this.AllInfoDatagridView.AllowUserToDeleteRows = false;
      this.AllInfoDatagridView.AllowUserToOrderColumns = true;
      this.AllInfoDatagridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.AllInfoDatagridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
      this.AllInfoDatagridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.AllInfoDatagridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Product_ID,
            this.Product_name,
            this.Count});
      this.AllInfoDatagridView.Location = new System.Drawing.Point(261, 89);
      this.AllInfoDatagridView.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.AllInfoDatagridView.MultiSelect = false;
      this.AllInfoDatagridView.Name = "AllInfoDatagridView";
      this.AllInfoDatagridView.ReadOnly = true;
      this.AllInfoDatagridView.RowHeadersWidth = 51;
      this.AllInfoDatagridView.RowTemplate.Height = 24;
      this.AllInfoDatagridView.Size = new System.Drawing.Size(493, 582);
      this.AllInfoDatagridView.TabIndex = 3;
      this.AllInfoDatagridView.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_RowEnter);
      // 
      // menuStrip1
      // 
      this.menuStrip1.Font = new System.Drawing.Font("Malgun Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
      this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.настроитьIPToolStripMenuItem,
            this.перейтиВРазделРедактированияToolStripMenuItem,
            this.справкаToolStripMenuItem,
            this.выйтиИзУчётнойЗаписиToolStripMenuItem});
      this.menuStrip1.Location = new System.Drawing.Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
      this.menuStrip1.Size = new System.Drawing.Size(1324, 28);
      this.menuStrip1.TabIndex = 4;
      this.menuStrip1.Text = "menuStrip1";
      // 
      // настроитьIPToolStripMenuItem
      // 
      this.настроитьIPToolStripMenuItem.Name = "настроитьIPToolStripMenuItem";
      this.настроитьIPToolStripMenuItem.Size = new System.Drawing.Size(114, 24);
      this.настроитьIPToolStripMenuItem.Text = "Настроить IP";
      this.настроитьIPToolStripMenuItem.Click += new System.EventHandler(this.настроитьIPToolStripMenuItem_Click);
      // 
      // перейтиВРазделРедактированияToolStripMenuItem
      // 
      this.перейтиВРазделРедактированияToolStripMenuItem.Name = "перейтиВРазделРедактированияToolStripMenuItem";
      this.перейтиВРазделРедактированияToolStripMenuItem.Size = new System.Drawing.Size(270, 24);
      this.перейтиВРазделРедактированияToolStripMenuItem.Text = "Перейти в раздел редактирования";
      this.перейтиВРазделРедактированияToolStripMenuItem.Click += new System.EventHandler(this.перейтиВРазделРедактированияToolStripMenuItem_Click);
      // 
      // справкаToolStripMenuItem
      // 
      this.справкаToolStripMenuItem.Name = "справкаToolStripMenuItem";
      this.справкаToolStripMenuItem.Size = new System.Drawing.Size(83, 24);
      this.справкаToolStripMenuItem.Text = "Справка";
      this.справкаToolStripMenuItem.Click += new System.EventHandler(this.справкаToolStripMenuItem_Click);
      // 
      // выйтиИзУчётнойЗаписиToolStripMenuItem
      // 
      this.выйтиИзУчётнойЗаписиToolStripMenuItem.Name = "выйтиИзУчётнойЗаписиToolStripMenuItem";
      this.выйтиИзУчётнойЗаписиToolStripMenuItem.Size = new System.Drawing.Size(204, 24);
      this.выйтиИзУчётнойЗаписиToolStripMenuItem.Text = "Выйти из учётной записи";
      this.выйтиИзУчётнойЗаписиToolStripMenuItem.Click += new System.EventHandler(this.выйтиИзУчётнойЗаписиToolStripMenuItem_Click);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Malgun Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(257, 38);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(78, 28);
      this.label1.TabIndex = 5;
      this.label1.Text = "Поиск:";
      // 
      // Product_ID
      // 
      this.Product_ID.HeaderText = "Product_ID";
      this.Product_ID.MinimumWidth = 6;
      this.Product_ID.Name = "Product_ID";
      this.Product_ID.ReadOnly = true;
      this.Product_ID.Visible = false;
      this.Product_ID.Width = 125;
      // 
      // Product_name
      // 
      this.Product_name.HeaderText = "Имя";
      this.Product_name.MinimumWidth = 6;
      this.Product_name.Name = "Product_name";
      this.Product_name.ReadOnly = true;
      this.Product_name.Width = 600;
      // 
      // Count
      // 
      this.Count.HeaderText = "Кол-во ";
      this.Count.MinimumWidth = 6;
      this.Count.Name = "Count";
      this.Count.ReadOnly = true;
      this.Count.Width = 200;
      // 
      // AuthorizedForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
      this.ClientSize = new System.Drawing.Size(1324, 682);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.AllInfoDatagridView);
      this.Controls.Add(this.panel2);
      this.Controls.Add(this.SearchInfo);
      this.Controls.Add(this.panel1);
      this.Controls.Add(this.menuStrip1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MainMenuStrip = this.menuStrip1;
      this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.MaximizeBox = false;
      this.Name = "AuthorizedForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
      this.Text = "Computer house";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AuthorizedForm_FormClosed);
      this.Load += new System.EventHandler(this.AuthorizedForm_Load);
      this.Enter += new System.EventHandler(this.AuthorizedForm_Enter);
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.AddProduct)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.AllInfoDatagridView)).EndInit();
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox SearchInfo;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView AllInfoDatagridView;
        private System.Windows.Forms.Button Move;
        private System.Windows.Forms.RichTextBox AllProductInfo;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem настроитьIPToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem перейтиВРазделРедактированияToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выйтиИзУчётнойЗаписиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem справкаToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown AddProduct;
    internal System.Windows.Forms.RadioButton GPUViewRadio;
    internal System.Windows.Forms.RadioButton CPUViewRadio;
    internal System.Windows.Forms.RadioButton MothersViewRadio;
    internal System.Windows.Forms.RadioButton RAMViewRadio;
    private System.Windows.Forms.Button ResetFilters;
    internal System.Windows.Forms.RadioButton CasesViewRadio;
    internal System.Windows.Forms.RadioButton CoolingSystemViewRadio;
    internal System.Windows.Forms.RadioButton PSUViewRadio;
    internal System.Windows.Forms.RadioButton SDViewRadio;
    private System.Windows.Forms.DataGridViewTextBoxColumn Product_ID;
    private System.Windows.Forms.DataGridViewTextBoxColumn Product_name;
    private System.Windows.Forms.DataGridViewTextBoxColumn Count;
  }
}