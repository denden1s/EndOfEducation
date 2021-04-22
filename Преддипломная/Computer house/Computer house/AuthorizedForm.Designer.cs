
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.настроитьIPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.справкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выйтиИзУчётнойЗаписиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.PCConfigurationPage = new System.Windows.Forms.TabPage();
            this.CreateConfiguration = new System.Windows.Forms.Button();
            this.EnterPurchaseWindow = new System.Windows.Forms.Button();
            this.PCConfigsDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RAM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cooling = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PSU = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Case = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label14 = new System.Windows.Forms.Label();
            this.SelectedComponentInfoTextBox = new System.Windows.Forms.RichTextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.SelectedConfigIntemsListBox = new System.Windows.Forms.ListBox();
            this.Case_ComboBox = new System.Windows.Forms.ComboBox();
            this.StorageDevice_ComboBox = new System.Windows.Forms.ComboBox();
            this.PSU_ComboBox = new System.Windows.Forms.ComboBox();
            this.CoolingSystem_ComboBox = new System.Windows.Forms.ComboBox();
            this.RAM_ComboBox = new System.Windows.Forms.ComboBox();
            this.Motherboard_ComboBox = new System.Windows.Forms.ComboBox();
            this.GPU_ComboBox = new System.Windows.Forms.ComboBox();
            this.CPU_ComboBox = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SellingOtherComponentsPage = new System.Windows.Forms.TabPage();
            this.PriceLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SellComponents = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.AllProductInfo = new System.Windows.Forms.RichTextBox();
            this.AddProduct = new System.Windows.Forms.NumericUpDown();
            this.RequestComponents = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SelectedItemsListBox = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.AllInfoDatagridView = new System.Windows.Forms.DataGridView();
            this.Product_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Product_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CountInShop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CountInSh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SearchInfo = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.PCConfigurationPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PCConfigsDataGridView)).BeginInit();
            this.SellingOtherComponentsPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AddProduct)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AllInfoDatagridView)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Malgun Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.настроитьIPToolStripMenuItem,
            this.справкаToolStripMenuItem,
            this.выйтиИзУчётнойЗаписиToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1336, 28);
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
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.PCConfigurationPage);
            this.tabControl1.Controls.Add(this.SellingOtherComponentsPage);
            this.tabControl1.Font = new System.Drawing.Font("Malgun Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(3, 27);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1328, 648);
            this.tabControl1.TabIndex = 5;
            // 
            // PCConfigurationPage
            // 
            this.PCConfigurationPage.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.PCConfigurationPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.PCConfigurationPage.Controls.Add(this.CreateConfiguration);
            this.PCConfigurationPage.Controls.Add(this.EnterPurchaseWindow);
            this.PCConfigurationPage.Controls.Add(this.PCConfigsDataGridView);
            this.PCConfigurationPage.Controls.Add(this.label14);
            this.PCConfigurationPage.Controls.Add(this.SelectedComponentInfoTextBox);
            this.PCConfigurationPage.Controls.Add(this.label13);
            this.PCConfigurationPage.Controls.Add(this.SelectedConfigIntemsListBox);
            this.PCConfigurationPage.Controls.Add(this.Case_ComboBox);
            this.PCConfigurationPage.Controls.Add(this.StorageDevice_ComboBox);
            this.PCConfigurationPage.Controls.Add(this.PSU_ComboBox);
            this.PCConfigurationPage.Controls.Add(this.CoolingSystem_ComboBox);
            this.PCConfigurationPage.Controls.Add(this.RAM_ComboBox);
            this.PCConfigurationPage.Controls.Add(this.Motherboard_ComboBox);
            this.PCConfigurationPage.Controls.Add(this.GPU_ComboBox);
            this.PCConfigurationPage.Controls.Add(this.CPU_ComboBox);
            this.PCConfigurationPage.Controls.Add(this.label12);
            this.PCConfigurationPage.Controls.Add(this.label11);
            this.PCConfigurationPage.Controls.Add(this.label10);
            this.PCConfigurationPage.Controls.Add(this.label9);
            this.PCConfigurationPage.Controls.Add(this.label8);
            this.PCConfigurationPage.Controls.Add(this.label7);
            this.PCConfigurationPage.Controls.Add(this.label6);
            this.PCConfigurationPage.Controls.Add(this.label5);
            this.PCConfigurationPage.Location = new System.Drawing.Point(4, 37);
            this.PCConfigurationPage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PCConfigurationPage.Name = "PCConfigurationPage";
            this.PCConfigurationPage.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PCConfigurationPage.Size = new System.Drawing.Size(1320, 607);
            this.PCConfigurationPage.TabIndex = 0;
            this.PCConfigurationPage.Text = "Конфигуратор";
            this.PCConfigurationPage.Enter += new System.EventHandler(this.PCConfigurationPage_Enter);
            // 
            // CreateConfiguration
            // 
            this.CreateConfiguration.Font = new System.Drawing.Font("Malgun Gothic", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CreateConfiguration.Location = new System.Drawing.Point(892, 478);
            this.CreateConfiguration.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CreateConfiguration.Name = "CreateConfiguration";
            this.CreateConfiguration.Size = new System.Drawing.Size(422, 47);
            this.CreateConfiguration.TabIndex = 45;
            this.CreateConfiguration.Text = "Сохранить конфигурацию";
            this.CreateConfiguration.UseVisualStyleBackColor = true;
            this.CreateConfiguration.Click += new System.EventHandler(this.CreateConfiguration_Click);
            // 
            // EnterPurchaseWindow
            // 
            this.EnterPurchaseWindow.Font = new System.Drawing.Font("Malgun Gothic", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EnterPurchaseWindow.Location = new System.Drawing.Point(892, 543);
            this.EnterPurchaseWindow.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.EnterPurchaseWindow.Name = "EnterPurchaseWindow";
            this.EnterPurchaseWindow.Size = new System.Drawing.Size(422, 47);
            this.EnterPurchaseWindow.TabIndex = 44;
            this.EnterPurchaseWindow.Text = "Оформить покупку";
            this.EnterPurchaseWindow.UseVisualStyleBackColor = true;
            // 
            // PCConfigsDataGridView
            // 
            this.PCConfigsDataGridView.AllowUserToAddRows = false;
            this.PCConfigsDataGridView.AllowUserToDeleteRows = false;
            this.PCConfigsDataGridView.AllowUserToOrderColumns = true;
            this.PCConfigsDataGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.PCConfigsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.PCConfigsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.RAM,
            this.Cooling,
            this.PSU,
            this.SD,
            this.Case});
            this.PCConfigsDataGridView.Location = new System.Drawing.Point(892, 27);
            this.PCConfigsDataGridView.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PCConfigsDataGridView.MultiSelect = false;
            this.PCConfigsDataGridView.Name = "PCConfigsDataGridView";
            this.PCConfigsDataGridView.ReadOnly = true;
            this.PCConfigsDataGridView.RowHeadersWidth = 51;
            this.PCConfigsDataGridView.RowTemplate.Height = 24;
            this.PCConfigsDataGridView.Size = new System.Drawing.Size(422, 421);
            this.PCConfigsDataGridView.TabIndex = 43;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Product_ID";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Visible = false;
            this.dataGridViewTextBoxColumn1.Width = 6;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "CPU";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 250;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "GPU";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 125;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Motherboard";
            this.dataGridViewTextBoxColumn4.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 125;
            // 
            // RAM
            // 
            this.RAM.HeaderText = "RAM";
            this.RAM.MinimumWidth = 6;
            this.RAM.Name = "RAM";
            this.RAM.ReadOnly = true;
            this.RAM.Width = 125;
            // 
            // Cooling
            // 
            this.Cooling.HeaderText = "Cooling";
            this.Cooling.MinimumWidth = 6;
            this.Cooling.Name = "Cooling";
            this.Cooling.ReadOnly = true;
            this.Cooling.Width = 125;
            // 
            // PSU
            // 
            this.PSU.HeaderText = "PSU";
            this.PSU.MinimumWidth = 6;
            this.PSU.Name = "PSU";
            this.PSU.ReadOnly = true;
            this.PSU.Width = 125;
            // 
            // SD
            // 
            this.SD.HeaderText = "Storage";
            this.SD.MinimumWidth = 6;
            this.SD.Name = "SD";
            this.SD.ReadOnly = true;
            this.SD.Width = 125;
            // 
            // Case
            // 
            this.Case.HeaderText = "Case";
            this.Case.MinimumWidth = 6;
            this.Case.Name = "Case";
            this.Case.ReadOnly = true;
            this.Case.Width = 125;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Malgun Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(574, 241);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(178, 28);
            this.label14.TabIndex = 42;
            this.label14.Text = "Характеристики:";
            // 
            // SelectedComponentInfoTextBox
            // 
            this.SelectedComponentInfoTextBox.Font = new System.Drawing.Font("Malgun Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SelectedComponentInfoTextBox.Location = new System.Drawing.Point(570, 270);
            this.SelectedComponentInfoTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SelectedComponentInfoTextBox.Name = "SelectedComponentInfoTextBox";
            this.SelectedComponentInfoTextBox.ReadOnly = true;
            this.SelectedComponentInfoTextBox.Size = new System.Drawing.Size(305, 315);
            this.SelectedComponentInfoTextBox.TabIndex = 41;
            this.SelectedComponentInfoTextBox.Text = "";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Malgun Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(573, 26);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(262, 28);
            this.label13.TabIndex = 32;
            this.label13.Text = "Выбранные компоненты:";
            // 
            // SelectedConfigIntemsListBox
            // 
            this.SelectedConfigIntemsListBox.FormattingEnabled = true;
            this.SelectedConfigIntemsListBox.ItemHeight = 28;
            this.SelectedConfigIntemsListBox.Location = new System.Drawing.Point(570, 57);
            this.SelectedConfigIntemsListBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SelectedConfigIntemsListBox.Name = "SelectedConfigIntemsListBox";
            this.SelectedConfigIntemsListBox.Size = new System.Drawing.Size(305, 172);
            this.SelectedConfigIntemsListBox.TabIndex = 31;
            // 
            // Case_ComboBox
            // 
            this.Case_ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Case_ComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Case_ComboBox.Font = new System.Drawing.Font("Malgun Gothic", 10F);
            this.Case_ComboBox.FormattingEnabled = true;
            this.Case_ComboBox.Items.AddRange(new object[] {
            "Модельный ряд процессора",
            "Кодовое название процессора",
            "Сокет",
            "Чипсет",
            "Каналы памяти",
            "Частота ОЗУ",
            "Форм-фактор",
            "Тип памяти",
            "Интерфейсы",
            "Разъёмы питания"});
            this.Case_ComboBox.Location = new System.Drawing.Point(127, 554);
            this.Case_ComboBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Case_ComboBox.Name = "Case_ComboBox";
            this.Case_ComboBox.Size = new System.Drawing.Size(423, 31);
            this.Case_ComboBox.TabIndex = 30;
            // 
            // StorageDevice_ComboBox
            // 
            this.StorageDevice_ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.StorageDevice_ComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.StorageDevice_ComboBox.Font = new System.Drawing.Font("Malgun Gothic", 10F);
            this.StorageDevice_ComboBox.FormattingEnabled = true;
            this.StorageDevice_ComboBox.Items.AddRange(new object[] {
            "Модельный ряд процессора",
            "Кодовое название процессора",
            "Сокет",
            "Чипсет",
            "Каналы памяти",
            "Частота ОЗУ",
            "Форм-фактор",
            "Тип памяти",
            "Интерфейсы",
            "Разъёмы питания"});
            this.StorageDevice_ComboBox.Location = new System.Drawing.Point(169, 478);
            this.StorageDevice_ComboBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.StorageDevice_ComboBox.Name = "StorageDevice_ComboBox";
            this.StorageDevice_ComboBox.Size = new System.Drawing.Size(381, 31);
            this.StorageDevice_ComboBox.TabIndex = 29;
            // 
            // PSU_ComboBox
            // 
            this.PSU_ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PSU_ComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.PSU_ComboBox.Font = new System.Drawing.Font("Malgun Gothic", 10F);
            this.PSU_ComboBox.FormattingEnabled = true;
            this.PSU_ComboBox.Items.AddRange(new object[] {
            "Модельный ряд процессора",
            "Кодовое название процессора",
            "Сокет",
            "Чипсет",
            "Каналы памяти",
            "Частота ОЗУ",
            "Форм-фактор",
            "Тип памяти",
            "Интерфейсы",
            "Разъёмы питания"});
            this.PSU_ComboBox.Location = new System.Drawing.Point(192, 402);
            this.PSU_ComboBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PSU_ComboBox.Name = "PSU_ComboBox";
            this.PSU_ComboBox.Size = new System.Drawing.Size(358, 31);
            this.PSU_ComboBox.TabIndex = 28;
            // 
            // CoolingSystem_ComboBox
            // 
            this.CoolingSystem_ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CoolingSystem_ComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CoolingSystem_ComboBox.Font = new System.Drawing.Font("Malgun Gothic", 10F);
            this.CoolingSystem_ComboBox.FormattingEnabled = true;
            this.CoolingSystem_ComboBox.Items.AddRange(new object[] {
            "Модельный ряд процессора",
            "Кодовое название процессора",
            "Сокет",
            "Чипсет",
            "Каналы памяти",
            "Частота ОЗУ",
            "Форм-фактор",
            "Тип памяти",
            "Интерфейсы",
            "Разъёмы питания"});
            this.CoolingSystem_ComboBox.Location = new System.Drawing.Point(169, 326);
            this.CoolingSystem_ComboBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CoolingSystem_ComboBox.Name = "CoolingSystem_ComboBox";
            this.CoolingSystem_ComboBox.Size = new System.Drawing.Size(381, 31);
            this.CoolingSystem_ComboBox.TabIndex = 27;
            // 
            // RAM_ComboBox
            // 
            this.RAM_ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.RAM_ComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.RAM_ComboBox.Font = new System.Drawing.Font("Malgun Gothic", 10F);
            this.RAM_ComboBox.FormattingEnabled = true;
            this.RAM_ComboBox.Items.AddRange(new object[] {
            "Модельный ряд процессора",
            "Кодовое название процессора",
            "Сокет",
            "Чипсет",
            "Каналы памяти",
            "Частота ОЗУ",
            "Форм-фактор",
            "Тип памяти",
            "Интерфейсы",
            "Разъёмы питания"});
            this.RAM_ComboBox.Location = new System.Drawing.Point(261, 250);
            this.RAM_ComboBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.RAM_ComboBox.Name = "RAM_ComboBox";
            this.RAM_ComboBox.Size = new System.Drawing.Size(289, 31);
            this.RAM_ComboBox.TabIndex = 26;
            // 
            // Motherboard_ComboBox
            // 
            this.Motherboard_ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Motherboard_ComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Motherboard_ComboBox.Font = new System.Drawing.Font("Malgun Gothic", 10F);
            this.Motherboard_ComboBox.FormattingEnabled = true;
            this.Motherboard_ComboBox.Items.AddRange(new object[] {
            "Модельный ряд процессора",
            "Кодовое название процессора",
            "Сокет",
            "Чипсет",
            "Каналы памяти",
            "Частота ОЗУ",
            "Форм-фактор",
            "Тип памяти",
            "Интерфейсы",
            "Разъёмы питания"});
            this.Motherboard_ComboBox.Location = new System.Drawing.Point(248, 174);
            this.Motherboard_ComboBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Motherboard_ComboBox.Name = "Motherboard_ComboBox";
            this.Motherboard_ComboBox.Size = new System.Drawing.Size(302, 31);
            this.Motherboard_ComboBox.TabIndex = 25;
            // 
            // GPU_ComboBox
            // 
            this.GPU_ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.GPU_ComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.GPU_ComboBox.Font = new System.Drawing.Font("Malgun Gothic", 10F);
            this.GPU_ComboBox.FormattingEnabled = true;
            this.GPU_ComboBox.Items.AddRange(new object[] {
            "Модельный ряд процессора",
            "Кодовое название процессора",
            "Сокет",
            "Чипсет",
            "Каналы памяти",
            "Частота ОЗУ",
            "Форм-фактор",
            "Тип памяти",
            "Интерфейсы",
            "Разъёмы питания"});
            this.GPU_ComboBox.Location = new System.Drawing.Point(160, 103);
            this.GPU_ComboBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.GPU_ComboBox.Name = "GPU_ComboBox";
            this.GPU_ComboBox.Size = new System.Drawing.Size(390, 31);
            this.GPU_ComboBox.TabIndex = 24;
            this.GPU_ComboBox.SelectedIndexChanged += new System.EventHandler(this.GPU_ComboBox_SelectedIndexChanged);
            // 
            // CPU_ComboBox
            // 
            this.CPU_ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CPU_ComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CPU_ComboBox.Font = new System.Drawing.Font("Malgun Gothic", 10F);
            this.CPU_ComboBox.FormattingEnabled = true;
            this.CPU_ComboBox.Items.AddRange(new object[] {
            "Модельный ряд процессора",
            "Кодовое название процессора",
            "Сокет",
            "Чипсет",
            "Каналы памяти",
            "Частота ОЗУ",
            "Форм-фактор",
            "Тип памяти",
            "Интерфейсы",
            "Разъёмы питания"});
            this.CPU_ComboBox.Location = new System.Drawing.Point(159, 32);
            this.CPU_ComboBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CPU_ComboBox.Name = "CPU_ComboBox";
            this.CPU_ComboBox.Size = new System.Drawing.Size(391, 31);
            this.CPU_ComboBox.TabIndex = 23;
            this.CPU_ComboBox.SelectedIndexChanged += new System.EventHandler(this.CPU_ComboBox_SelectedIndexChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Malgun Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(20, 554);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(88, 28);
            this.label12.TabIndex = 21;
            this.label12.Text = "Корпус:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Malgun Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(20, 478);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(134, 28);
            this.label11.TabIndex = 20;
            this.label11.Text = "Накопитель:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Malgun Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(20, 402);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(153, 28);
            this.label10.TabIndex = 19;
            this.label10.Text = "Блок питания:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Malgun Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(20, 326);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(143, 28);
            this.label9.TabIndex = 18;
            this.label9.Text = "Охлаждение:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Malgun Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(18, 250);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(224, 28);
            this.label8.TabIndex = 17;
            this.label8.Text = "Оперативная память:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Malgun Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(20, 174);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(210, 28);
            this.label7.TabIndex = 16;
            this.label7.Text = "Материнская плата:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Malgun Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(20, 103);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(133, 28);
            this.label6.TabIndex = 15;
            this.label6.Text = "Видеокарта:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Malgun Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(20, 32);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(126, 28);
            this.label5.TabIndex = 14;
            this.label5.Text = "Процессор:";
            // 
            // SellingOtherComponentsPage
            // 
            this.SellingOtherComponentsPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.SellingOtherComponentsPage.Controls.Add(this.PriceLabel);
            this.SellingOtherComponentsPage.Controls.Add(this.label4);
            this.SellingOtherComponentsPage.Controls.Add(this.SellComponents);
            this.SellingOtherComponentsPage.Controls.Add(this.label3);
            this.SellingOtherComponentsPage.Controls.Add(this.AllProductInfo);
            this.SellingOtherComponentsPage.Controls.Add(this.AddProduct);
            this.SellingOtherComponentsPage.Controls.Add(this.RequestComponents);
            this.SellingOtherComponentsPage.Controls.Add(this.label2);
            this.SellingOtherComponentsPage.Controls.Add(this.SelectedItemsListBox);
            this.SellingOtherComponentsPage.Controls.Add(this.label1);
            this.SellingOtherComponentsPage.Controls.Add(this.AllInfoDatagridView);
            this.SellingOtherComponentsPage.Controls.Add(this.SearchInfo);
            this.SellingOtherComponentsPage.Location = new System.Drawing.Point(4, 37);
            this.SellingOtherComponentsPage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SellingOtherComponentsPage.Name = "SellingOtherComponentsPage";
            this.SellingOtherComponentsPage.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SellingOtherComponentsPage.Size = new System.Drawing.Size(1320, 607);
            this.SellingOtherComponentsPage.TabIndex = 1;
            this.SellingOtherComponentsPage.Text = "Продажа отдельных комплектующих";
            this.SellingOtherComponentsPage.Enter += new System.EventHandler(this.SellingOtherComponentsPage_Enter);
            // 
            // PriceLabel
            // 
            this.PriceLabel.AutoSize = true;
            this.PriceLabel.Font = new System.Drawing.Font("Malgun Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PriceLabel.Location = new System.Drawing.Point(1018, 97);
            this.PriceLabel.Name = "PriceLabel";
            this.PriceLabel.Size = new System.Drawing.Size(24, 28);
            this.PriceLabel.TabIndex = 43;
            this.PriceLabel.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Malgun Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(1018, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(249, 28);
            this.label4.TabIndex = 42;
            this.label4.Text = "Общая стоимость (Б.Р.):";
            // 
            // SellComponents
            // 
            this.SellComponents.Font = new System.Drawing.Font("Malgun Gothic", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SellComponents.Location = new System.Drawing.Point(1023, 553);
            this.SellComponents.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SellComponents.Name = "SellComponents";
            this.SellComponents.Size = new System.Drawing.Size(291, 47);
            this.SellComponents.TabIndex = 41;
            this.SellComponents.Text = "Оформить покупку";
            this.SellComponents.UseVisualStyleBackColor = true;
            this.SellComponents.Click += new System.EventHandler(this.SellComponents_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Malgun Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(575, 230);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(178, 28);
            this.label3.TabIndex = 40;
            this.label3.Text = "Характеристики:";
            // 
            // AllProductInfo
            // 
            this.AllProductInfo.Font = new System.Drawing.Font("Malgun Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AllProductInfo.Location = new System.Drawing.Point(580, 258);
            this.AllProductInfo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.AllProductInfo.Name = "AllProductInfo";
            this.AllProductInfo.ReadOnly = true;
            this.AllProductInfo.Size = new System.Drawing.Size(416, 341);
            this.AllProductInfo.TabIndex = 39;
            this.AllProductInfo.Text = "";
            // 
            // AddProduct
            // 
            this.AddProduct.Font = new System.Drawing.Font("Malgun Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddProduct.Location = new System.Drawing.Point(12, 565);
            this.AddProduct.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.AddProduct.Name = "AddProduct";
            this.AddProduct.Size = new System.Drawing.Size(164, 34);
            this.AddProduct.TabIndex = 38;
            this.AddProduct.ThousandsSeparator = true;
            // 
            // RequestComponents
            // 
            this.RequestComponents.Font = new System.Drawing.Font("Malgun Gothic", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RequestComponents.Location = new System.Drawing.Point(181, 562);
            this.RequestComponents.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.RequestComponents.Name = "RequestComponents";
            this.RequestComponents.Size = new System.Drawing.Size(388, 38);
            this.RequestComponents.TabIndex = 37;
            this.RequestComponents.Text = "Запросить товар";
            this.RequestComponents.UseVisualStyleBackColor = true;
            this.RequestComponents.Click += new System.EventHandler(this.RequestComponents_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Malgun Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(575, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(262, 28);
            this.label2.TabIndex = 13;
            this.label2.Text = "Выбранные компоненты:";
            // 
            // SelectedItemsListBox
            // 
            this.SelectedItemsListBox.FormattingEnabled = true;
            this.SelectedItemsListBox.ItemHeight = 28;
            this.SelectedItemsListBox.Location = new System.Drawing.Point(580, 59);
            this.SelectedItemsListBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SelectedItemsListBox.Name = "SelectedItemsListBox";
            this.SelectedItemsListBox.Size = new System.Drawing.Size(416, 144);
            this.SelectedItemsListBox.TabIndex = 12;
            this.SelectedItemsListBox.DoubleClick += new System.EventHandler(this.SelectedItemsListBox_DoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Malgun Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(20, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 28);
            this.label1.TabIndex = 11;
            this.label1.Text = "Поиск:";
            // 
            // AllInfoDatagridView
            // 
            this.AllInfoDatagridView.AllowUserToAddRows = false;
            this.AllInfoDatagridView.AllowUserToDeleteRows = false;
            this.AllInfoDatagridView.AllowUserToOrderColumns = true;
            this.AllInfoDatagridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.AllInfoDatagridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.AllInfoDatagridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Product_ID,
            this.Product_name,
            this.CountInShop,
            this.CountInSh});
            this.AllInfoDatagridView.Location = new System.Drawing.Point(25, 59);
            this.AllInfoDatagridView.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.AllInfoDatagridView.MultiSelect = false;
            this.AllInfoDatagridView.Name = "AllInfoDatagridView";
            this.AllInfoDatagridView.ReadOnly = true;
            this.AllInfoDatagridView.RowHeadersWidth = 51;
            this.AllInfoDatagridView.RowTemplate.Height = 24;
            this.AllInfoDatagridView.Size = new System.Drawing.Size(544, 492);
            this.AllInfoDatagridView.TabIndex = 10;
            this.AllInfoDatagridView.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.AllInfoDatagridView_CellContentDoubleClick);
            this.AllInfoDatagridView.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.AllInfoDatagridView_RowEnter);
            // 
            // Product_ID
            // 
            this.Product_ID.HeaderText = "Product_ID";
            this.Product_ID.MinimumWidth = 6;
            this.Product_ID.Name = "Product_ID";
            this.Product_ID.ReadOnly = true;
            this.Product_ID.Visible = false;
            this.Product_ID.Width = 6;
            // 
            // Product_name
            // 
            this.Product_name.HeaderText = "Наименование товара";
            this.Product_name.MinimumWidth = 6;
            this.Product_name.Name = "Product_name";
            this.Product_name.ReadOnly = true;
            this.Product_name.Width = 250;
            // 
            // CountInShop
            // 
            this.CountInShop.HeaderText = "Кол-во на складе";
            this.CountInShop.MinimumWidth = 6;
            this.CountInShop.Name = "CountInShop";
            this.CountInShop.ReadOnly = true;
            this.CountInShop.Width = 125;
            // 
            // CountInSh
            // 
            this.CountInSh.HeaderText = "Кол-во в магазине";
            this.CountInSh.MinimumWidth = 6;
            this.CountInSh.Name = "CountInSh";
            this.CountInSh.ReadOnly = true;
            this.CountInSh.Width = 125;
            // 
            // SearchInfo
            // 
            this.SearchInfo.Font = new System.Drawing.Font("Malgun Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SearchInfo.Location = new System.Drawing.Point(111, 9);
            this.SearchInfo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SearchInfo.Name = "SearchInfo";
            this.SearchInfo.Size = new System.Drawing.Size(458, 30);
            this.SearchInfo.TabIndex = 9;
            this.SearchInfo.TextChanged += new System.EventHandler(this.SearchInfo_TextChanged);
            // 
            // AuthorizedForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1336, 684);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "AuthorizedForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Computer house";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AuthorizedForm_FormClosed);
            this.Load += new System.EventHandler(this.AuthorizedForm_Load);
            this.Enter += new System.EventHandler(this.AuthorizedForm_Enter);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.PCConfigurationPage.ResumeLayout(false);
            this.PCConfigurationPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PCConfigsDataGridView)).EndInit();
            this.SellingOtherComponentsPage.ResumeLayout(false);
            this.SellingOtherComponentsPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AddProduct)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AllInfoDatagridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem настроитьIPToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выйтиИзУчётнойЗаписиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem справкаToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage PCConfigurationPage;
        private System.Windows.Forms.TabPage SellingOtherComponentsPage;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox SelectedItemsListBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView AllInfoDatagridView;
        private System.Windows.Forms.TextBox SearchInfo;
        private System.Windows.Forms.RichTextBox AllProductInfo;
        private System.Windows.Forms.NumericUpDown AddProduct;
        internal System.Windows.Forms.Button RequestComponents;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label PriceLabel;
        private System.Windows.Forms.Label label4;
        internal System.Windows.Forms.Button SellComponents;
        private System.Windows.Forms.DataGridViewTextBoxColumn Product_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Product_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn CountInShop;
        private System.Windows.Forms.DataGridViewTextBoxColumn CountInSh;
        private System.Windows.Forms.DataGridView PCConfigsDataGridView;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.RichTextBox SelectedComponentInfoTextBox;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ListBox SelectedConfigIntemsListBox;
        internal System.Windows.Forms.ComboBox Case_ComboBox;
        internal System.Windows.Forms.ComboBox StorageDevice_ComboBox;
        internal System.Windows.Forms.ComboBox PSU_ComboBox;
        internal System.Windows.Forms.ComboBox CoolingSystem_ComboBox;
        internal System.Windows.Forms.ComboBox RAM_ComboBox;
        internal System.Windows.Forms.ComboBox Motherboard_ComboBox;
        internal System.Windows.Forms.ComboBox GPU_ComboBox;
        internal System.Windows.Forms.ComboBox CPU_ComboBox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn RAM;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cooling;
        private System.Windows.Forms.DataGridViewTextBoxColumn PSU;
        private System.Windows.Forms.DataGridViewTextBoxColumn SD;
        private System.Windows.Forms.DataGridViewTextBoxColumn Case;
        internal System.Windows.Forms.Button CreateConfiguration;
        internal System.Windows.Forms.Button EnterPurchaseWindow;
    }
}