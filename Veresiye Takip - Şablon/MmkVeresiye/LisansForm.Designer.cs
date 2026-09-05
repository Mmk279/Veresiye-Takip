namespace MmkVeresiye
{
    partial class LisansForm
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
            pictureBox1 = new PictureBox();
            label1 = new Label();
            txtCihazID = new TextBox();
            label2 = new Label();
            txtLisansAnahtari = new TextBox();
            btnAktifEt = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.Gemini_Generated_Image_fxevrvfxevrvfxev;
            pictureBox1.Location = new Point(75, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(301, 138);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Black", 12F, FontStyle.Bold, GraphicsUnit.Point, 162);
            label1.ForeColor = SystemColors.Control;
            label1.Location = new Point(23, 191);
            label1.Name = "label1";
            label1.Size = new Size(104, 28);
            label1.TabIndex = 1;
            label1.Text = "CİHAZ ID";
            // 
            // txtCihazID
            // 
            txtCihazID.Location = new Point(228, 191);
            txtCihazID.Name = "txtCihazID";
            txtCihazID.ReadOnly = true;
            txtCihazID.Size = new Size(125, 27);
            txtCihazID.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Black", 12F, FontStyle.Bold, GraphicsUnit.Point, 162);
            label2.ForeColor = SystemColors.Control;
            label2.Location = new Point(23, 236);
            label2.Name = "label2";
            label2.Size = new Size(193, 28);
            label2.TabIndex = 3;
            label2.Text = "LİSANS ANAHTAR";
            // 
            // txtLisansAnahtari
            // 
            txtLisansAnahtari.Location = new Point(228, 236);
            txtLisansAnahtari.Name = "txtLisansAnahtari";
            txtLisansAnahtari.Size = new Size(125, 27);
            txtLisansAnahtari.TabIndex = 4;
            // 
            // btnAktifEt
            // 
            btnAktifEt.Font = new Font("Segoe UI Black", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 162);
            btnAktifEt.ForeColor = Color.Green;
            btnAktifEt.Location = new Point(86, 287);
            btnAktifEt.Name = "btnAktifEt";
            btnAktifEt.Size = new Size(219, 51);
            btnAktifEt.TabIndex = 5;
            btnAktifEt.Text = "LİSANS AKTİF ET";
            btnAktifEt.UseVisualStyleBackColor = true;
            btnAktifEt.Click += this.btnAktifEt_Click;
            // 
            // LisansForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(26, 45, 91);
            ClientSize = new Size(471, 407);
            Controls.Add(btnAktifEt);
            Controls.Add(txtLisansAnahtari);
            Controls.Add(label2);
            Controls.Add(txtCihazID);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            Name = "LisansForm";
            Text = "LİSANS EKRANI";
            Load += LisansForm_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Label label1;
        private TextBox txtCihazID;
        private Label label2;
        private TextBox txtLisansAnahtari;
        private Button btnAktifEt;
    }
}