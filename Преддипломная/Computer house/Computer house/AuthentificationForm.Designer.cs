namespace Computer_house
{
    partial class AuthentificationForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AuthentificationForm));
            this.BAuthentificate = new System.Windows.Forms.Button();
            this.LLogin = new System.Windows.Forms.Label();
            this.LoginInfo = new System.Windows.Forms.TextBox();
            this.PasswordInfo = new System.Windows.Forms.TextBox();
            this.LPassword = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.настроитьIPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BAuthentificate
            // 
            this.BAuthentificate.BackColor = System.Drawing.Color.White;
            this.BAuthentificate.Font = new System.Drawing.Font("Malgun Gothic", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BAuthentificate.Location = new System.Drawing.Point(12, 155);
            this.BAuthentificate.Name = "BAuthentificate";
            this.BAuthentificate.Size = new System.Drawing.Size(358, 60);
            this.BAuthentificate.TabIndex = 0;
            this.BAuthentificate.Text = "Войти";
            this.BAuthentificate.UseVisualStyleBackColor = false;
            this.BAuthentificate.Click += new System.EventHandler(this.BAuthentificate_Click);
            // 
            // LLogin
            // 
            this.LLogin.AutoSize = true;
            this.LLogin.Font = new System.Drawing.Font("Malgun Gothic", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LLogin.Location = new System.Drawing.Point(12, 38);
            this.LLogin.Name = "LLogin";
            this.LLogin.Size = new System.Drawing.Size(94, 32);
            this.LLogin.TabIndex = 1;
            this.LLogin.Text = "Логин:";
            // 
            // LoginInfo
            // 
            this.LoginInfo.Font = new System.Drawing.Font("Malgun Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoginInfo.Location = new System.Drawing.Point(121, 42);
            this.LoginInfo.Name = "LoginInfo";
            this.LoginInfo.Size = new System.Drawing.Size(249, 30);
            this.LoginInfo.TabIndex = 2;
            // 
            // PasswordInfo
            // 
            this.PasswordInfo.Font = new System.Drawing.Font("Malgun Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PasswordInfo.Location = new System.Drawing.Point(121, 100);
            this.PasswordInfo.Name = "PasswordInfo";
            this.PasswordInfo.PasswordChar = '*';
            this.PasswordInfo.Size = new System.Drawing.Size(249, 30);
            this.PasswordInfo.TabIndex = 4;
            // 
            // LPassword
            // 
            this.LPassword.AutoSize = true;
            this.LPassword.Font = new System.Drawing.Font("Malgun Gothic", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LPassword.Location = new System.Drawing.Point(6, 96);
            this.LPassword.Name = "LPassword";
            this.LPassword.Size = new System.Drawing.Size(109, 32);
            this.LPassword.TabIndex = 3;
            this.LPassword.Text = "Пароль:";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.настроитьIPToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(382, 28);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // настроитьIPToolStripMenuItem
            // 
            this.настроитьIPToolStripMenuItem.Name = "настроитьIPToolStripMenuItem";
            this.настроитьIPToolStripMenuItem.Size = new System.Drawing.Size(112, 24);
            this.настроитьIPToolStripMenuItem.Text = "Настроить IP";
            this.настроитьIPToolStripMenuItem.Click += new System.EventHandler(this.настроитьIPToolStripMenuItem_Click);
            // 
            // AuthentificationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(382, 227);
            this.Controls.Add(this.PasswordInfo);
            this.Controls.Add(this.LPassword);
            this.Controls.Add(this.LoginInfo);
            this.Controls.Add(this.LLogin);
            this.Controls.Add(this.BAuthentificate);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Malgun Gothic", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AuthentificationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Computer house";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AuthentificationForm_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BAuthentificate;
        private System.Windows.Forms.Label LLogin;
        private System.Windows.Forms.TextBox LoginInfo;
        private System.Windows.Forms.TextBox PasswordInfo;
        private System.Windows.Forms.Label LPassword;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem настроитьIPToolStripMenuItem;
    }
}

