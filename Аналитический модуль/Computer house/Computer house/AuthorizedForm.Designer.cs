
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
      System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
      System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AuthorizedForm));
      this.menuStrip1 = new System.Windows.Forms.MenuStrip();
      this.настроитьIPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.справкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.выйтиИзУчётнойЗаписиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabPage1 = new System.Windows.Forms.TabPage();
      this.panel1 = new System.Windows.Forms.Panel();
      this.button2 = new System.Windows.Forms.Button();
      this.label8 = new System.Windows.Forms.Label();
      this.EndPeriod = new System.Windows.Forms.MaskedTextBox();
      this.StartOfPeriodTextBox = new System.Windows.Forms.MaskedTextBox();
      this.label7 = new System.Windows.Forms.Label();
      this.DateLabel = new System.Windows.Forms.Label();
      this.label6 = new System.Windows.Forms.Label();
      this.tabControl2 = new System.Windows.Forms.TabControl();
      this.tabPage3 = new System.Windows.Forms.TabPage();
      this.tabPage4 = new System.Windows.Forms.TabPage();
      this.tabControl3 = new System.Windows.Forms.TabControl();
      this.tabPage6 = new System.Windows.Forms.TabPage();
      this.DemandedChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
      this.tabPage7 = new System.Windows.Forms.TabPage();
      this.DemandedTable = new System.Windows.Forms.DataGridView();
      this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.tabPage5 = new System.Windows.Forms.TabPage();
      this.tabPage2 = new System.Windows.Forms.TabPage();
      this.button1 = new System.Windows.Forms.Button();
      this.BuyingPrice = new System.Windows.Forms.NumericUpDown();
      this.label5 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.SelectedItem = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.MarkUpPercent = new System.Windows.Forms.NumericUpDown();
      this.label2 = new System.Windows.Forms.Label();
      this.UnsetPrice = new System.Windows.Forms.ListBox();
      this.label1 = new System.Windows.Forms.Label();
      this.SetingPrice = new System.Windows.Forms.ListBox();
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
      this.tabPage1.SuspendLayout();
      this.panel1.SuspendLayout();
      this.tabControl2.SuspendLayout();
      this.tabPage4.SuspendLayout();
      this.tabControl3.SuspendLayout();
      this.tabPage6.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.DemandedChart)).BeginInit();
      this.tabPage7.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.DemandedTable)).BeginInit();
      this.tabPage2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.BuyingPrice)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.MarkUpPercent)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.HoldingDocsDatagridView)).BeginInit();
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
      this.menuStrip1.Size = new System.Drawing.Size(1324, 30);
      this.menuStrip1.TabIndex = 4;
      this.menuStrip1.Text = "menuStrip1";
      // 
      // настроитьIPToolStripMenuItem
      // 
      this.настроитьIPToolStripMenuItem.Name = "настроитьIPToolStripMenuItem";
      this.настроитьIPToolStripMenuItem.Size = new System.Drawing.Size(114, 26);
      this.настроитьIPToolStripMenuItem.Text = "Настроить IP";
      this.настроитьIPToolStripMenuItem.Click += new System.EventHandler(this.настроитьIPToolStripMenuItem_Click);
      // 
      // справкаToolStripMenuItem
      // 
      this.справкаToolStripMenuItem.Name = "справкаToolStripMenuItem";
      this.справкаToolStripMenuItem.Size = new System.Drawing.Size(83, 26);
      this.справкаToolStripMenuItem.Text = "Справка";
      this.справкаToolStripMenuItem.Click += new System.EventHandler(this.справкаToolStripMenuItem_Click);
      // 
      // выйтиИзУчётнойЗаписиToolStripMenuItem
      // 
      this.выйтиИзУчётнойЗаписиToolStripMenuItem.Name = "выйтиИзУчётнойЗаписиToolStripMenuItem";
      this.выйтиИзУчётнойЗаписиToolStripMenuItem.Size = new System.Drawing.Size(204, 26);
      this.выйтиИзУчётнойЗаписиToolStripMenuItem.Text = "Выйти из учётной записи";
      this.выйтиИзУчётнойЗаписиToolStripMenuItem.Click += new System.EventHandler(this.выйтиИзУчётнойЗаписиToolStripMenuItem_Click);
      // 
      // tabControl1
      // 
      this.tabControl1.Controls.Add(this.tabPage1);
      this.tabControl1.Controls.Add(this.tabPage2);
      this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabControl1.Font = new System.Drawing.Font("Malgun Gothic", 12F);
      this.tabControl1.Location = new System.Drawing.Point(0, 30);
      this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(1324, 652);
      this.tabControl1.TabIndex = 5;
      // 
      // tabPage1
      // 
      this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
      this.tabPage1.Controls.Add(this.panel1);
      this.tabPage1.Controls.Add(this.tabControl2);
      this.tabPage1.Location = new System.Drawing.Point(4, 37);
      this.tabPage1.Margin = new System.Windows.Forms.Padding(4);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new System.Windows.Forms.Padding(4);
      this.tabPage1.Size = new System.Drawing.Size(1316, 611);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.Text = "Статистика";
      // 
      // panel1
      // 
      this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.panel1.Controls.Add(this.button2);
      this.panel1.Controls.Add(this.label8);
      this.panel1.Controls.Add(this.EndPeriod);
      this.panel1.Controls.Add(this.StartOfPeriodTextBox);
      this.panel1.Controls.Add(this.label7);
      this.panel1.Controls.Add(this.DateLabel);
      this.panel1.Controls.Add(this.label6);
      this.panel1.Location = new System.Drawing.Point(7, 429);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(1302, 172);
      this.panel1.TabIndex = 1;
      // 
      // button2
      // 
      this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.button2.Font = new System.Drawing.Font("Malgun Gothic", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.button2.Location = new System.Drawing.Point(973, 9);
      this.button2.Name = "button2";
      this.button2.Size = new System.Drawing.Size(325, 157);
      this.button2.TabIndex = 13;
      this.button2.Text = "Составить представления";
      this.button2.UseVisualStyleBackColor = true;
      this.button2.Click += new System.EventHandler(this.button2_Click);
      // 
      // label8
      // 
      this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.label8.AutoSize = true;
      this.label8.Font = new System.Drawing.Font("Malgun Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label8.Location = new System.Drawing.Point(804, 75);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(21, 28);
      this.label8.TabIndex = 9;
      this.label8.Text = "/";
      // 
      // EndPeriod
      // 
      this.EndPeriod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.EndPeriod.Font = new System.Drawing.Font("Malgun Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.EndPeriod.Location = new System.Drawing.Point(840, 76);
      this.EndPeriod.Mask = "0000-00";
      this.EndPeriod.Name = "EndPeriod";
      this.EndPeriod.Size = new System.Drawing.Size(100, 30);
      this.EndPeriod.TabIndex = 8;
      this.EndPeriod.ValidatingType = typeof(System.DateTime);
      // 
      // StartOfPeriodTextBox
      // 
      this.StartOfPeriodTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.StartOfPeriodTextBox.Font = new System.Drawing.Font("Malgun Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.StartOfPeriodTextBox.Location = new System.Drawing.Point(689, 76);
      this.StartOfPeriodTextBox.Mask = "0000-00";
      this.StartOfPeriodTextBox.Name = "StartOfPeriodTextBox";
      this.StartOfPeriodTextBox.Size = new System.Drawing.Size(100, 30);
      this.StartOfPeriodTextBox.TabIndex = 7;
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Font = new System.Drawing.Font("Malgun Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label7.Location = new System.Drawing.Point(16, 76);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(650, 28);
      this.label7.TabIndex = 6;
      this.label7.Text = "Введите период за который необходимо собрать информацию";
      // 
      // DateLabel
      // 
      this.DateLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
      this.DateLabel.AutoSize = true;
      this.DateLabel.Font = new System.Drawing.Font("Malgun Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.DateLabel.Location = new System.Drawing.Point(773, 12);
      this.DateLabel.Name = "DateLabel";
      this.DateLabel.Size = new System.Drawing.Size(55, 28);
      this.DateLabel.TabIndex = 5;
      this.DateLabel.Text = "date";
      // 
      // label6
      // 
      this.label6.Anchor = System.Windows.Forms.AnchorStyles.Top;
      this.label6.AutoSize = true;
      this.label6.Font = new System.Drawing.Font("Malgun Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label6.Location = new System.Drawing.Point(260, 12);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(507, 28);
      this.label6.TabIndex = 4;
      this.label6.Text = "Текущий график отражает информацию по дате:";
      // 
      // tabControl2
      // 
      this.tabControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.tabControl2.Controls.Add(this.tabPage3);
      this.tabControl2.Controls.Add(this.tabPage4);
      this.tabControl2.Controls.Add(this.tabPage5);
      this.tabControl2.Location = new System.Drawing.Point(7, 3);
      this.tabControl2.Multiline = true;
      this.tabControl2.Name = "tabControl2";
      this.tabControl2.SelectedIndex = 0;
      this.tabControl2.Size = new System.Drawing.Size(1302, 357);
      this.tabControl2.TabIndex = 0;
      // 
      // tabPage3
      // 
      this.tabPage3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
      this.tabPage3.Location = new System.Drawing.Point(4, 37);
      this.tabPage3.Name = "tabPage3";
      this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage3.Size = new System.Drawing.Size(1294, 313);
      this.tabPage3.TabIndex = 0;
      this.tabPage3.Text = "График затрат и доходов";
      this.tabPage3.Enter += new System.EventHandler(this.tabPage3_Enter);
      // 
      // tabPage4
      // 
      this.tabPage4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
      this.tabPage4.Controls.Add(this.tabControl3);
      this.tabPage4.Location = new System.Drawing.Point(4, 37);
      this.tabPage4.Name = "tabPage4";
      this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage4.Size = new System.Drawing.Size(1294, 316);
      this.tabPage4.TabIndex = 1;
      this.tabPage4.Text = "График востребованности";
      this.tabPage4.Enter += new System.EventHandler(this.tabPage4_Enter);
      // 
      // tabControl3
      // 
      this.tabControl3.Alignment = System.Windows.Forms.TabAlignment.Bottom;
      this.tabControl3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.tabControl3.Controls.Add(this.tabPage6);
      this.tabControl3.Controls.Add(this.tabPage7);
      this.tabControl3.Font = new System.Drawing.Font("Malgun Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tabControl3.Location = new System.Drawing.Point(6, 6);
      this.tabControl3.Multiline = true;
      this.tabControl3.Name = "tabControl3";
      this.tabControl3.SelectedIndex = 0;
      this.tabControl3.Size = new System.Drawing.Size(1282, 307);
      this.tabControl3.TabIndex = 1;
      // 
      // tabPage6
      // 
      this.tabPage6.Controls.Add(this.DemandedChart);
      this.tabPage6.Location = new System.Drawing.Point(4, 4);
      this.tabPage6.Name = "tabPage6";
      this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage6.Size = new System.Drawing.Size(1274, 271);
      this.tabPage6.TabIndex = 0;
      this.tabPage6.Text = "График";
      this.tabPage6.UseVisualStyleBackColor = true;
      // 
      // DemandedChart
      // 
      this.DemandedChart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      chartArea1.Name = "ChartArea1";
      this.DemandedChart.ChartAreas.Add(chartArea1);
      legend1.Font = new System.Drawing.Font("Malgun Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      legend1.IsTextAutoFit = false;
      legend1.Name = "Legend1";
      this.DemandedChart.Legends.Add(legend1);
      this.DemandedChart.Location = new System.Drawing.Point(3, 6);
      this.DemandedChart.Name = "DemandedChart";
      this.DemandedChart.Size = new System.Drawing.Size(1265, 259);
      this.DemandedChart.TabIndex = 0;
      this.DemandedChart.Text = "chart1";
      // 
      // tabPage7
      // 
      this.tabPage7.Controls.Add(this.DemandedTable);
      this.tabPage7.Location = new System.Drawing.Point(4, 4);
      this.tabPage7.Name = "tabPage7";
      this.tabPage7.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage7.Size = new System.Drawing.Size(1274, 289);
      this.tabPage7.TabIndex = 1;
      this.tabPage7.Text = "Таблица";
      this.tabPage7.UseVisualStyleBackColor = true;
      // 
      // DemandedTable
      // 
      this.DemandedTable.AllowUserToAddRows = false;
      this.DemandedTable.AllowUserToDeleteRows = false;
      this.DemandedTable.AllowUserToOrderColumns = true;
      this.DemandedTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.DemandedTable.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
      this.DemandedTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.DemandedTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn5});
      this.DemandedTable.Location = new System.Drawing.Point(7, 5);
      this.DemandedTable.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.DemandedTable.Name = "DemandedTable";
      this.DemandedTable.ReadOnly = true;
      this.DemandedTable.RowHeadersWidth = 51;
      this.DemandedTable.RowTemplate.Height = 24;
      this.DemandedTable.Size = new System.Drawing.Size(1261, 255);
      this.DemandedTable.TabIndex = 2;
      // 
      // dataGridViewTextBoxColumn2
      // 
      this.dataGridViewTextBoxColumn2.HeaderText = "Наименование товара";
      this.dataGridViewTextBoxColumn2.MinimumWidth = 6;
      this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
      this.dataGridViewTextBoxColumn2.ReadOnly = true;
      this.dataGridViewTextBoxColumn2.Width = 600;
      // 
      // dataGridViewTextBoxColumn5
      // 
      this.dataGridViewTextBoxColumn5.HeaderText = "Кол-во";
      this.dataGridViewTextBoxColumn5.MinimumWidth = 6;
      this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
      this.dataGridViewTextBoxColumn5.ReadOnly = true;
      this.dataGridViewTextBoxColumn5.Width = 150;
      // 
      // tabPage5
      // 
      this.tabPage5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
      this.tabPage5.Location = new System.Drawing.Point(4, 37);
      this.tabPage5.Name = "tabPage5";
      this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage5.Size = new System.Drawing.Size(1294, 313);
      this.tabPage5.TabIndex = 2;
      this.tabPage5.Text = "График эффективности труда";
      this.tabPage5.Enter += new System.EventHandler(this.tabPage5_Enter);
      // 
      // tabPage2
      // 
      this.tabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
      this.tabPage2.Controls.Add(this.button1);
      this.tabPage2.Controls.Add(this.BuyingPrice);
      this.tabPage2.Controls.Add(this.label5);
      this.tabPage2.Controls.Add(this.label4);
      this.tabPage2.Controls.Add(this.SelectedItem);
      this.tabPage2.Controls.Add(this.label3);
      this.tabPage2.Controls.Add(this.MarkUpPercent);
      this.tabPage2.Controls.Add(this.label2);
      this.tabPage2.Controls.Add(this.UnsetPrice);
      this.tabPage2.Controls.Add(this.label1);
      this.tabPage2.Controls.Add(this.SetingPrice);
      this.tabPage2.Controls.Add(this.HoldingDocsDatagridView);
      this.tabPage2.Location = new System.Drawing.Point(4, 37);
      this.tabPage2.Margin = new System.Windows.Forms.Padding(4);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Padding = new System.Windows.Forms.Padding(4);
      this.tabPage2.Size = new System.Drawing.Size(1316, 611);
      this.tabPage2.TabIndex = 1;
      this.tabPage2.Text = "Складской учёт";
      // 
      // button1
      // 
      this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.button1.Font = new System.Drawing.Font("Malgun Gothic", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.button1.Location = new System.Drawing.Point(752, 543);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(541, 56);
      this.button1.TabIndex = 12;
      this.button1.Text = "Сохранить";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // BuyingPrice
      // 
      this.BuyingPrice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.BuyingPrice.Font = new System.Drawing.Font("Malgun Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.BuyingPrice.Location = new System.Drawing.Point(1172, 406);
      this.BuyingPrice.Maximum = new decimal(new int[] {
            -1773790777,
            2,
            0,
            0});
      this.BuyingPrice.Name = "BuyingPrice";
      this.BuyingPrice.Size = new System.Drawing.Size(120, 34);
      this.BuyingPrice.TabIndex = 11;
      // 
      // label5
      // 
      this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.label5.AutoSize = true;
      this.label5.Font = new System.Drawing.Font("Malgun Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label5.Location = new System.Drawing.Point(746, 412);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(234, 28);
      this.label5.TabIndex = 10;
      this.label5.Text = "Закупочная стоимость";
      // 
      // label4
      // 
      this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.label4.AutoSize = true;
      this.label4.Font = new System.Drawing.Font("Malgun Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label4.Location = new System.Drawing.Point(747, 483);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(198, 28);
      this.label4.TabIndex = 9;
      this.label4.Text = "Процент надбавки";
      // 
      // SelectedItem
      // 
      this.SelectedItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.SelectedItem.AutoSize = true;
      this.SelectedItem.Font = new System.Drawing.Font("Malgun Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.SelectedItem.Location = new System.Drawing.Point(951, 342);
      this.SelectedItem.Name = "SelectedItem";
      this.SelectedItem.Size = new System.Drawing.Size(0, 28);
      this.SelectedItem.TabIndex = 8;
      // 
      // label3
      // 
      this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.label3.AutoSize = true;
      this.label3.Font = new System.Drawing.Font("Malgun Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label3.Location = new System.Drawing.Point(746, 342);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(199, 28);
      this.label3.TabIndex = 7;
      this.label3.Text = "Выбранный товар:";
      // 
      // MarkUpPercent
      // 
      this.MarkUpPercent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.MarkUpPercent.Font = new System.Drawing.Font("Malgun Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.MarkUpPercent.Location = new System.Drawing.Point(1172, 477);
      this.MarkUpPercent.Name = "MarkUpPercent";
      this.MarkUpPercent.Size = new System.Drawing.Size(120, 34);
      this.MarkUpPercent.TabIndex = 6;
      // 
      // label2
      // 
      this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label2.AutoSize = true;
      this.label2.Font = new System.Drawing.Font("Malgun Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label2.Location = new System.Drawing.Point(391, 299);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(255, 28);
      this.label2.TabIndex = 5;
      this.label2.Text = "Не установленные цены";
      // 
      // UnsetPrice
      // 
      this.UnsetPrice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.UnsetPrice.FormattingEnabled = true;
      this.UnsetPrice.ItemHeight = 28;
      this.UnsetPrice.Location = new System.Drawing.Point(396, 343);
      this.UnsetPrice.Name = "UnsetPrice";
      this.UnsetPrice.Size = new System.Drawing.Size(318, 256);
      this.UnsetPrice.TabIndex = 4;
      this.UnsetPrice.SelectedIndexChanged += new System.EventHandler(this.UnsetPrice_SelectedIndexChanged);
      // 
      // label1
      // 
      this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Malgun Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(22, 299);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(177, 28);
      this.label1.TabIndex = 3;
      this.label1.Text = "Цены на товары";
      // 
      // SetingPrice
      // 
      this.SetingPrice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.SetingPrice.FormattingEnabled = true;
      this.SetingPrice.ItemHeight = 28;
      this.SetingPrice.Location = new System.Drawing.Point(27, 343);
      this.SetingPrice.Name = "SetingPrice";
      this.SetingPrice.Size = new System.Drawing.Size(318, 256);
      this.SetingPrice.TabIndex = 2;
      this.SetingPrice.SelectedIndexChanged += new System.EventHandler(this.SetingPrice_SelectedIndexChanged);
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
      this.HoldingDocsDatagridView.Location = new System.Drawing.Point(4, 4);
      this.HoldingDocsDatagridView.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.HoldingDocsDatagridView.Name = "HoldingDocsDatagridView";
      this.HoldingDocsDatagridView.ReadOnly = true;
      this.HoldingDocsDatagridView.RowHeadersWidth = 51;
      this.HoldingDocsDatagridView.RowTemplate.Height = 24;
      this.HoldingDocsDatagridView.Size = new System.Drawing.Size(1305, 185);
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
      this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
      this.ClientSize = new System.Drawing.Size(1324, 682);
      this.Controls.Add(this.tabControl1);
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
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      this.tabControl1.ResumeLayout(false);
      this.tabPage1.ResumeLayout(false);
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.tabControl2.ResumeLayout(false);
      this.tabPage4.ResumeLayout(false);
      this.tabControl3.ResumeLayout(false);
      this.tabPage6.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.DemandedChart)).EndInit();
      this.tabPage7.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.DemandedTable)).EndInit();
      this.tabPage2.ResumeLayout(false);
      this.tabPage2.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.BuyingPrice)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.MarkUpPercent)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.HoldingDocsDatagridView)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem настроитьIPToolStripMenuItem;
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
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ListBox SetingPrice;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.ListBox UnsetPrice;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.NumericUpDown BuyingPrice;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label SelectedItem;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.NumericUpDown MarkUpPercent;
    private System.Windows.Forms.TabControl tabControl2;
    private System.Windows.Forms.TabPage tabPage3;
    private System.Windows.Forms.TabPage tabPage4;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Label DateLabel;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.TabPage tabPage5;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.MaskedTextBox EndPeriod;
    private System.Windows.Forms.MaskedTextBox StartOfPeriodTextBox;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Button button2;
    private System.Windows.Forms.DataVisualization.Charting.Chart DemandedChart;
    private System.Windows.Forms.TabControl tabControl3;
    private System.Windows.Forms.TabPage tabPage6;
    private System.Windows.Forms.TabPage tabPage7;
    internal System.Windows.Forms.DataGridView DemandedTable;
    private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
    private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
  }
}