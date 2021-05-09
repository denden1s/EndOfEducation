
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
      this.перейтиВРазделРедактированияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.справкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.выйтиИзУчётнойЗаписиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabPage1 = new System.Windows.Forms.TabPage();
      this.tabPage2 = new System.Windows.Forms.TabPage();
      this.HoldingDocsDatagridView = new System.Windows.Forms.DataGridView();
      this.HoldingDocumentID = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.ProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.MovingTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.ProductsCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.WorkerID = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Location = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.menuStrip1.SuspendLayout();
      this.tabControl1.SuspendLayout();
      this.tabPage2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.HoldingDocsDatagridView)).BeginInit();
      this.SuspendLayout();
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
      // tabControl1
      // 
      this.tabControl1.Controls.Add(this.tabPage1);
      this.tabControl1.Controls.Add(this.tabPage2);
      this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabControl1.Font = new System.Drawing.Font("Malgun Gothic", 12F);
      this.tabControl1.Location = new System.Drawing.Point(0, 24);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(993, 530);
      this.tabControl1.TabIndex = 5;
      // 
      // tabPage1
      // 
      this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
      this.tabPage1.Location = new System.Drawing.Point(4, 30);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage1.Size = new System.Drawing.Size(985, 496);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.Text = "Статистика";
      // 
      // tabPage2
      // 
      this.tabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
      this.tabPage2.Controls.Add(this.HoldingDocsDatagridView);
      this.tabPage2.Location = new System.Drawing.Point(4, 30);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage2.Size = new System.Drawing.Size(985, 496);
      this.tabPage2.TabIndex = 1;
      this.tabPage2.Text = "Складской учёт";
      // 
      // HoldingDocsDatagridView
      // 
      this.HoldingDocsDatagridView.AllowUserToAddRows = false;
      this.HoldingDocsDatagridView.AllowUserToDeleteRows = false;
      this.HoldingDocsDatagridView.AllowUserToOrderColumns = true;
      this.HoldingDocsDatagridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.HoldingDocsDatagridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
      this.HoldingDocsDatagridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.HoldingDocsDatagridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.HoldingDocumentID,
            this.ProductName,
            this.MovingTime,
            this.Status,
            this.ProductsCount,
            this.WorkerID,
            this.Location});
      this.HoldingDocsDatagridView.Location = new System.Drawing.Point(3, 3);
      this.HoldingDocsDatagridView.Margin = new System.Windows.Forms.Padding(2);
      this.HoldingDocsDatagridView.Name = "HoldingDocsDatagridView";
      this.HoldingDocsDatagridView.ReadOnly = true;
      this.HoldingDocsDatagridView.RowHeadersWidth = 51;
      this.HoldingDocsDatagridView.RowTemplate.Height = 24;
      this.HoldingDocsDatagridView.Size = new System.Drawing.Size(979, 150);
      this.HoldingDocsDatagridView.TabIndex = 1;
      // 
      // HoldingDocumentID
      // 
      this.HoldingDocumentID.HeaderText = "ID";
      this.HoldingDocumentID.MinimumWidth = 6;
      this.HoldingDocumentID.Name = "HoldingDocumentID";
      this.HoldingDocumentID.ReadOnly = true;
      this.HoldingDocumentID.Visible = false;
      this.HoldingDocumentID.Width = 125;
      // 
      // ProductName
      // 
      this.ProductName.HeaderText = "Наименование товара";
      this.ProductName.MinimumWidth = 6;
      this.ProductName.Name = "ProductName";
      this.ProductName.ReadOnly = true;
      this.ProductName.Width = 600;
      // 
      // MovingTime
      // 
      this.MovingTime.HeaderText = "Время проведения";
      this.MovingTime.MinimumWidth = 6;
      this.MovingTime.Name = "MovingTime";
      this.MovingTime.ReadOnly = true;
      this.MovingTime.Width = 200;
      // 
      // Status
      // 
      this.Status.HeaderText = "Статус";
      this.Status.MinimumWidth = 6;
      this.Status.Name = "Status";
      this.Status.ReadOnly = true;
      this.Status.Width = 200;
      // 
      // ProductsCount
      // 
      this.ProductsCount.HeaderText = "Кол-во";
      this.ProductsCount.MinimumWidth = 6;
      this.ProductsCount.Name = "ProductsCount";
      this.ProductsCount.ReadOnly = true;
      this.ProductsCount.Width = 150;
      // 
      // WorkerID
      // 
      this.WorkerID.HeaderText = "ФИО работника";
      this.WorkerID.MinimumWidth = 6;
      this.WorkerID.Name = "WorkerID";
      this.WorkerID.ReadOnly = true;
      this.WorkerID.Width = 150;
      // 
      // Location
      // 
      this.Location.HeaderText = "Расположение";
      this.Location.MinimumWidth = 6;
      this.Location.Name = "Location";
      this.Location.ReadOnly = true;
      this.Location.Width = 200;
      // 
      // AuthorizedForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
      this.ClientSize = new System.Drawing.Size(993, 554);
      this.Controls.Add(this.tabControl1);
      this.Controls.Add(this.menuStrip1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MainMenuStrip = this.menuStrip1;
      this.Margin = new System.Windows.Forms.Padding(2);
      this.MaximizeBox = false;
      this.Name = "AuthorizedForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
      this.Text = "Computer house";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AuthorizedForm_FormClosed);
      this.Load += new System.EventHandler(this.AuthorizedForm_Load);
      this.Enter += new System.EventHandler(this.AuthorizedForm_Enter);
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      this.tabControl1.ResumeLayout(false);
      this.tabPage2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.HoldingDocsDatagridView)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem настроитьIPToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem перейтиВРазделРедактированияToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выйтиИзУчётнойЗаписиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem справкаToolStripMenuItem;
    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabPage1;
    private System.Windows.Forms.TabPage tabPage2;
    internal System.Windows.Forms.DataGridView HoldingDocsDatagridView;
    private System.Windows.Forms.DataGridViewTextBoxColumn HoldingDocumentID;
    private System.Windows.Forms.DataGridViewTextBoxColumn ProductName;
    private System.Windows.Forms.DataGridViewTextBoxColumn MovingTime;
    private System.Windows.Forms.DataGridViewTextBoxColumn Status;
    private System.Windows.Forms.DataGridViewTextBoxColumn ProductsCount;
    private System.Windows.Forms.DataGridViewTextBoxColumn WorkerID;
    private System.Windows.Forms.DataGridViewTextBoxColumn Location;
  }
}