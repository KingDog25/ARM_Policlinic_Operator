﻿namespace ARM_Policlinic_Operator
{
    partial class AutorizationForm
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
            this.labelComboBox = new System.Windows.Forms.Label();
            this.comboBoxAut = new System.Windows.Forms.ComboBox();
            this.buttonAutOK = new System.Windows.Forms.Button();
            this.labelAutPass = new System.Windows.Forms.Label();
            this.labelAutLogin = new System.Windows.Forms.Label();
            this.textBoxAutPass = new System.Windows.Forms.TextBox();
            this.textBoxAutLogin = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // labelComboBox
            // 
            this.labelComboBox.AutoSize = true;
            this.labelComboBox.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelComboBox.Location = new System.Drawing.Point(201, 31);
            this.labelComboBox.Name = "labelComboBox";
            this.labelComboBox.Size = new System.Drawing.Size(55, 26);
            this.labelComboBox.TabIndex = 23;
            this.labelComboBox.Text = "Тип:";
            // 
            // comboBoxAut
            // 
            this.comboBoxAut.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxAut.FormattingEnabled = true;
            this.comboBoxAut.Items.AddRange(new object[] {
            "Врач",
            "Заведующий/Главврач",
            "Специалист контакт-центра"});
            this.comboBoxAut.Location = new System.Drawing.Point(262, 28);
            this.comboBoxAut.Name = "comboBoxAut";
            this.comboBoxAut.Size = new System.Drawing.Size(258, 33);
            this.comboBoxAut.TabIndex = 22;
            this.comboBoxAut.Text = "Выберите тип";
            // 
            // buttonAutOK
            // 
            this.buttonAutOK.BackColor = System.Drawing.Color.LemonChiffon;
            this.buttonAutOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonAutOK.Location = new System.Drawing.Point(572, 64);
            this.buttonAutOK.Name = "buttonAutOK";
            this.buttonAutOK.Size = new System.Drawing.Size(110, 72);
            this.buttonAutOK.TabIndex = 21;
            this.buttonAutOK.Text = "Войти";
            this.buttonAutOK.UseVisualStyleBackColor = false;
            this.buttonAutOK.Click += new System.EventHandler(this.buttonAutOK_Click);
            // 
            // labelAutPass
            // 
            this.labelAutPass.AutoSize = true;
            this.labelAutPass.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelAutPass.Location = new System.Drawing.Point(210, 142);
            this.labelAutPass.Name = "labelAutPass";
            this.labelAutPass.Size = new System.Drawing.Size(88, 26);
            this.labelAutPass.TabIndex = 20;
            this.labelAutPass.Text = "Пароль:";
            // 
            // labelAutLogin
            // 
            this.labelAutLogin.AutoSize = true;
            this.labelAutLogin.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelAutLogin.Location = new System.Drawing.Point(201, 88);
            this.labelAutLogin.Name = "labelAutLogin";
            this.labelAutLogin.Size = new System.Drawing.Size(101, 26);
            this.labelAutLogin.TabIndex = 19;
            this.labelAutLogin.Text = "Телефон:";
            // 
            // textBoxAutPass
            // 
            this.textBoxAutPass.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxAutPass.Location = new System.Drawing.Point(317, 139);
            this.textBoxAutPass.Name = "textBoxAutPass";
            this.textBoxAutPass.Size = new System.Drawing.Size(203, 30);
            this.textBoxAutPass.TabIndex = 18;
            this.textBoxAutPass.Text = "test";
            this.textBoxAutPass.UseSystemPasswordChar = true;
            // 
            // textBoxAutLogin
            // 
            this.textBoxAutLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxAutLogin.Location = new System.Drawing.Point(317, 85);
            this.textBoxAutLogin.Name = "textBoxAutLogin";
            this.textBoxAutLogin.Size = new System.Drawing.Size(203, 30);
            this.textBoxAutLogin.TabIndex = 17;
            this.textBoxAutLogin.Text = "test";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::ARM_Policlinic_Operator.Properties.Resources.EsM1VtnXIAIOhjC;
            this.pictureBox1.Location = new System.Drawing.Point(8, 15);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(187, 172);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 16;
            this.pictureBox1.TabStop = false;
            // 
            // AutorizationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(694, 230);
            this.Controls.Add(this.labelComboBox);
            this.Controls.Add(this.comboBoxAut);
            this.Controls.Add(this.buttonAutOK);
            this.Controls.Add(this.labelAutPass);
            this.Controls.Add(this.labelAutLogin);
            this.Controls.Add(this.textBoxAutPass);
            this.Controls.Add(this.textBoxAutLogin);
            this.Controls.Add(this.pictureBox1);
            this.Name = "AutorizationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Авторизация";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelComboBox;
        private System.Windows.Forms.ComboBox comboBoxAut;
        private System.Windows.Forms.Button buttonAutOK;
        private System.Windows.Forms.Label labelAutPass;
        private System.Windows.Forms.Label labelAutLogin;
        private System.Windows.Forms.TextBox textBoxAutPass;
        private System.Windows.Forms.TextBox textBoxAutLogin;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}