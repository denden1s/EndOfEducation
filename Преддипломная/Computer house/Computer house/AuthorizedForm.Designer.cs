
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
            this.SearchInfo = new System.Windows.Forms.TextBox();
            this.Product_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Product_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CountInShop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CountInSh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SellingOtherComponentsPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AddProduct)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AllInfoDatagridView)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
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
            this.настроитьIPToolStripMenuItem.Size = new System.Drawing.Size(112, 24);
            this.настроитьIPToolStripMenuItem.Text = "Настроить IP";
            this.настроитьIPToolStripMenuItem.Click += new System.EventHandler(this.настроитьIPToolStripMenuItem_Click);
            // 
            // справкаToolStripMenuItem
            // 
            this.справкаToolStripMenuItem.Name = "справкаToolStripMenuItem";
            this.справкаToolStripMenuItem.Size = new System.Drawing.Size(81, 24);
            this.справкаToolStripMenuItem.Text = "Справка";
            this.справкаToolStripMenuItem.Click += new System.EventHandler(this.справкаToolStripMenuItem_Click);
            // 
            // выйтиИзУчётнойЗаписиToolStripMenuItem
            // 
            this.выйтиИзУчётнойЗаписиToolStripMenuItem.Name = "выйтиИзУчётнойЗаписиToolStripMenuItem";
            this.выйтиИзУчётнойЗаписиToolStripMenuItem.Size = new System.Drawing.Size(200, 24);
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
            this.tabControl1.Size = new System.Drawing.Size(1328, 665);
            this.tabControl1.TabIndex = 5;
            // 
            // PCConfigurationPage
            // 
            this.PCConfigurationPage.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.PCConfigurationPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.PCConfigurationPage.Location = new System.Drawing.Point(4, 37);
            this.PCConfigurationPage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PCConfigurationPage.Name = "PCConfigurationPage";
            this.PCConfigurationPage.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PCConfigurationPage.Size = new System.Drawing.Size(1320, 624);
            this.PCConfigurationPage.TabIndex = 0;
            this.PCConfigurationPage.Text = "Конфигуратор";
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
            this.SellingOtherComponentsPage.Size = new System.Drawing.Size(1320, 624);
            this.SellingOtherComponentsPage.TabIndex = 1;
            this.SellingOtherComponentsPage.Text = "Продажа отдельных комплектующих";
            // 
            // PriceLabel
            // 
            this.PriceLabel.AutoSize = true;
            this.PriceLabel.Font = new System.Drawing.Font("Malgun Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PriceLabel.Location = new System.Drawing.Point(1123, 59);
            this.PriceLabel.Name = "PriceLabel";
            this.PriceLabel.Size = new System.Drawing.Size(84, 28);
            this.PriceLabel.TabIndex = 43;
            this.PriceLabel.Text = "$$$$$$";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Malgun Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(1016, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(193, 28);
            this.label4.TabIndex = 42;
            this.label4.Text = "Общая стоимость:";
            // 
            // SellComponents
            // 
            this.SellComponents.Font = new System.Drawing.Font("Malgun Gothic", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SellComponents.Location = new System.Drawing.Point(1021, 556);
            this.SellComponents.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SellComponents.Name = "SellComponents";
            this.SellComponents.Size = new System.Drawing.Size(291, 47);
            this.SellComponents.TabIndex = 41;
            this.SellComponents.Text = "Оформить покупку";
            this.SellComponents.UseVisualStyleBackColor = true;
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
            this.RequestComponents.Size = new System.Drawing.Size(375, 38);
            this.RequestComponents.TabIndex = 37;
            this.RequestComponents.Text = "Запросить товар";
            this.RequestComponents.UseVisualStyleBackColor = true;
            this.RequestComponents.Click += new System.EventHandler(this.RequestComponents_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Malgun Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(575, 15);
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
            this.label1.Location = new System.Drawing.Point(7, 12);
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
            // SearchInfo
            // 
            this.SearchInfo.Font = new System.Drawing.Font("Malgun Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SearchInfo.Location = new System.Drawing.Point(91, 9);
            this.SearchInfo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SearchInfo.Name = "SearchInfo";
            this.SearchInfo.Size = new System.Drawing.Size(464, 30);
            this.SearchInfo.TabIndex = 9;
            this.SearchInfo.TextChanged += new System.EventHandler(this.SearchInfo_TextChanged);
            // 
            // Product_ID
            // 
            this.Product_ID.HeaderText = "Product_ID";
            this.Product_ID.MinimumWidth = 6;
            this.Product_ID.Name = "Product_ID";
            this.Product_ID.ReadOnly = true;
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
            // AuthorizedForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1336, 698);
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
    }
}