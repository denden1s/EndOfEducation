﻿
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
      this.label2 = new System.Windows.Forms.Label();
      this.SearchInfo = new System.Windows.Forms.TextBox();
      this.panel2 = new System.Windows.Forms.Panel();
      this.AddProduct = new System.Windows.Forms.NumericUpDown();
      this.AllProductInfo = new System.Windows.Forms.RichTextBox();
      this.Move = new System.Windows.Forms.Button();
      this.AllInfoDatagridView = new System.Windows.Forms.DataGridView();
      this.Product_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Product_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Count = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.menuStrip1 = new System.Windows.Forms.MenuStrip();
      this.настроитьIPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.перейтиВРазделРедактированияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.справкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.выйтиИзУчётнойЗаписиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.label1 = new System.Windows.Forms.Label();
      this.panel1.SuspendLayout();
      this.panel2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.AddProduct)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.AllInfoDatagridView)).BeginInit();
      this.menuStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.label2);
      this.panel1.Location = new System.Drawing.Point(9, 25);
      this.panel1.Margin = new System.Windows.Forms.Padding(2);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(150, 519);
      this.panel1.TabIndex = 0;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Font = new System.Drawing.Font("Malgun Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label2.Location = new System.Drawing.Point(21, 164);
      this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(114, 21);
      this.label2.TabIndex = 6;
      this.label2.Text = "Coming Soon";
      // 
      // SearchInfo
      // 
      this.SearchInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.SearchInfo.Font = new System.Drawing.Font("Malgun Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.SearchInfo.Location = new System.Drawing.Point(280, 31);
      this.SearchInfo.Margin = new System.Windows.Forms.Padding(2);
      this.SearchInfo.Name = "SearchInfo";
      this.SearchInfo.Size = new System.Drawing.Size(288, 26);
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
      this.panel2.Location = new System.Drawing.Point(595, 24);
      this.panel2.Margin = new System.Windows.Forms.Padding(2);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(398, 530);
      this.panel2.TabIndex = 2;
      // 
      // AddProduct
      // 
      this.AddProduct.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.AddProduct.Font = new System.Drawing.Font("Malgun Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.AddProduct.Location = new System.Drawing.Point(2, 492);
      this.AddProduct.Margin = new System.Windows.Forms.Padding(2);
      this.AddProduct.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
      this.AddProduct.Name = "AddProduct";
      this.AddProduct.Size = new System.Drawing.Size(255, 29);
      this.AddProduct.TabIndex = 6;
      this.AddProduct.ThousandsSeparator = true;
      // 
      // AllProductInfo
      // 
      this.AllProductInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.AllProductInfo.Font = new System.Drawing.Font("Malgun Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.AllProductInfo.Location = new System.Drawing.Point(2, 3);
      this.AllProductInfo.Margin = new System.Windows.Forms.Padding(2);
      this.AllProductInfo.Name = "AllProductInfo";
      this.AllProductInfo.ReadOnly = true;
      this.AllProductInfo.Size = new System.Drawing.Size(392, 475);
      this.AllProductInfo.TabIndex = 4;
      this.AllProductInfo.Text = "";
      // 
      // Move
      // 
      this.Move.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.Move.Font = new System.Drawing.Font("Malgun Gothic", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.Move.Location = new System.Drawing.Point(269, 485);
      this.Move.Margin = new System.Windows.Forms.Padding(2);
      this.Move.Name = "Move";
      this.Move.Size = new System.Drawing.Size(127, 36);
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
      this.AllInfoDatagridView.Location = new System.Drawing.Point(196, 72);
      this.AllInfoDatagridView.Margin = new System.Windows.Forms.Padding(2);
      this.AllInfoDatagridView.MultiSelect = false;
      this.AllInfoDatagridView.Name = "AllInfoDatagridView";
      this.AllInfoDatagridView.ReadOnly = true;
      this.AllInfoDatagridView.RowHeadersWidth = 51;
      this.AllInfoDatagridView.RowTemplate.Height = 24;
      this.AllInfoDatagridView.Size = new System.Drawing.Size(370, 473);
      this.AllInfoDatagridView.TabIndex = 3;
      this.AllInfoDatagridView.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_RowEnter);
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
      this.Count.Width = 300;
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
      this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
      this.menuStrip1.Size = new System.Drawing.Size(993, 24);
      this.menuStrip1.TabIndex = 4;
      this.menuStrip1.Text = "menuStrip1";
      // 
      // настроитьIPToolStripMenuItem
      // 
      this.настроитьIPToolStripMenuItem.Name = "настроитьIPToolStripMenuItem";
      this.настроитьIPToolStripMenuItem.Size = new System.Drawing.Size(91, 20);
      this.настроитьIPToolStripMenuItem.Text = "Настроить IP";
      this.настроитьIPToolStripMenuItem.Click += new System.EventHandler(this.настроитьIPToolStripMenuItem_Click);
      // 
      // перейтиВРазделРедактированияToolStripMenuItem
      // 
      this.перейтиВРазделРедактированияToolStripMenuItem.Name = "перейтиВРазделРедактированияToolStripMenuItem";
      this.перейтиВРазделРедактированияToolStripMenuItem.Size = new System.Drawing.Size(211, 20);
      this.перейтиВРазделРедактированияToolStripMenuItem.Text = "Перейти в раздел редактирования";
      this.перейтиВРазделРедактированияToolStripMenuItem.Click += new System.EventHandler(this.перейтиВРазделРедактированияToolStripMenuItem_Click);
      // 
      // справкаToolStripMenuItem
      // 
      this.справкаToolStripMenuItem.Name = "справкаToolStripMenuItem";
      this.справкаToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
      this.справкаToolStripMenuItem.Text = "Справка";
      this.справкаToolStripMenuItem.Click += new System.EventHandler(this.справкаToolStripMenuItem_Click);
      // 
      // выйтиИзУчётнойЗаписиToolStripMenuItem
      // 
      this.выйтиИзУчётнойЗаписиToolStripMenuItem.Name = "выйтиИзУчётнойЗаписиToolStripMenuItem";
      this.выйтиИзУчётнойЗаписиToolStripMenuItem.Size = new System.Drawing.Size(161, 20);
      this.выйтиИзУчётнойЗаписиToolStripMenuItem.Text = "Выйти из учётной записи";
      this.выйтиИзУчётнойЗаписиToolStripMenuItem.Click += new System.EventHandler(this.выйтиИзУчётнойЗаписиToolStripMenuItem_Click);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Malgun Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(193, 31);
      this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(63, 21);
      this.label1.TabIndex = 5;
      this.label1.Text = "Поиск:";
      // 
      // AuthorizedForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
      this.ClientSize = new System.Drawing.Size(993, 554);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.AllInfoDatagridView);
      this.Controls.Add(this.panel2);
      this.Controls.Add(this.SearchInfo);
      this.Controls.Add(this.panel1);
      this.Controls.Add(this.menuStrip1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MainMenuStrip = this.menuStrip1;
      this.Margin = new System.Windows.Forms.Padding(2);
      this.MaximizeBox = false;
      this.Name = "AuthorizedForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Computer house";
      this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
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
    private System.Windows.Forms.DataGridViewTextBoxColumn Product_ID;
    private System.Windows.Forms.DataGridViewTextBoxColumn Product_name;
    private System.Windows.Forms.DataGridViewTextBoxColumn Count;
  }
}