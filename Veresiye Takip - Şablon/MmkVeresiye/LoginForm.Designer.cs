namespace MmkVeresiye
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            txtSifre = new TextBox();
            btnGiris = new Button();
            label1 = new Label();
            picLogo = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)picLogo).BeginInit();
            SuspendLayout();
            // 
            // txtSifre
            // 
            txtSifre.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold);
            txtSifre.Location = new Point(63, 194);
            txtSifre.Margin = new Padding(4, 5, 4, 5);
            txtSifre.Name = "txtSifre";
            txtSifre.PasswordChar = '*';
            txtSifre.Size = new Size(265, 34);
            txtSifre.TabIndex = 0;
            txtSifre.KeyDown += txtSifre_KeyDown;
            // 
            // btnGiris
            // 
            btnGiris.BackColor = Color.FromArgb(26, 45, 91);
            btnGiris.FlatStyle = FlatStyle.Flat;
            btnGiris.ForeColor = Color.White;
            btnGiris.Location = new Point(63, 251);
            btnGiris.Margin = new Padding(4, 5, 4, 5);
            btnGiris.Name = "btnGiris";
            btnGiris.Size = new Size(267, 62);
            btnGiris.TabIndex = 1;
            btnGiris.Text = "GİRİŞ YAP";
            btnGiris.UseVisualStyleBackColor = false;
            btnGiris.Click += btnGiris_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.ForeColor = Color.White;
            label1.Location = new Point(63, 153);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(156, 20);
            label1.TabIndex = 2;
            label1.Text = "Lütfen Şifrenizi Giriniz:";
            // 
            // picLogo
            // 
            picLogo.BackColor = Color.Transparent;
            picLogo.Image = Properties.Resources.Gemini_Generated_Image_fxevrvfxevrvfxev;
            picLogo.Location = new Point(63, 49);
            picLogo.Name = "picLogo";
            picLogo.Size = new Size(226, 88);
            picLogo.SizeMode = PictureBoxSizeMode.Zoom;
            picLogo.TabIndex = 3;
            picLogo.TabStop = false;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(26, 45, 91);
            ClientSize = new Size(391, 388);
            Controls.Add(picLogo);
            Controls.Add(label1);
            Controls.Add(btnGiris);
            Controls.Add(txtSifre);
            ForeColor = Color.Black;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(4, 5, 4, 5);
            Name = "LoginForm";
            ((System.ComponentModel.ISupportInitialize)picLogo).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.TextBox txtSifre;
        private System.Windows.Forms.Button btnGiris;
        private System.Windows.Forms.Label label1;
        private PictureBox picLogo;
    }
}