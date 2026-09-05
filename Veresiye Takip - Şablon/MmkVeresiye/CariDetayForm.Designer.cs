namespace MmkVeresiye
{
    partial class CariDetayForm
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
            lblMusteriAdi = new Label();
            dgvHareketler = new DataGridView();
            colTarih = new DataGridViewTextBoxColumn();
            colAciklama = new DataGridViewTextBoxColumn();
            colBorc = new DataGridViewTextBoxColumn();
            colTahsilat = new DataGridViewTextBoxColumn();
            colBakiye = new DataGridViewTextBoxColumn();
            pnlIslem = new Panel();
            label2 = new Label();
            label1 = new Label();
            txtIslemTutar = new TextBox();
            txtIslemAciklama = new TextBox();
            lblIslemBaslik = new Label();
            btnHareketSil = new Button();
            btnExcel = new Button();
            btnPdf = new Button();
            btnBorcEkle = new Button();
            btnTahsilatYap = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvHareketler).BeginInit();
            pnlIslem.SuspendLayout();
            SuspendLayout();
            // 
            // lblMusteriAdi
            // 
            lblMusteriAdi.AutoSize = true;
            lblMusteriAdi.ForeColor = SystemColors.Control;
            lblMusteriAdi.Location = new Point(358, 9);
            lblMusteriAdi.Margin = new Padding(5, 0, 5, 0);
            lblMusteriAdi.Name = "lblMusteriAdi";
            lblMusteriAdi.Size = new Size(56, 21);
            lblMusteriAdi.TabIndex = 0;
            lblMusteriAdi.Text = "label1";
            // 
            // dgvHareketler
            // 
            dgvHareketler.BackgroundColor = Color.FromArgb(26, 45, 91);
            dgvHareketler.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvHareketler.Columns.AddRange(new DataGridViewColumn[] { colTarih, colAciklama, colBorc, colTahsilat, colBakiye });
            dgvHareketler.Location = new Point(43, 87);
            dgvHareketler.Margin = new Padding(5, 4, 5, 4);
            dgvHareketler.Name = "dgvHareketler";
            dgvHareketler.ReadOnly = true;
            dgvHareketler.RowHeadersWidth = 51;
            dgvHareketler.Size = new Size(967, 597);
            dgvHareketler.TabIndex = 1;
            // 
            // colTarih
            // 
            colTarih.HeaderText = "Tarih/Saat";
            colTarih.MinimumWidth = 6;
            colTarih.Name = "colTarih";
            colTarih.ReadOnly = true;
            colTarih.Width = 125;
            // 
            // colAciklama
            // 
            colAciklama.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colAciklama.HeaderText = "Açıklama";
            colAciklama.MinimumWidth = 6;
            colAciklama.Name = "colAciklama";
            colAciklama.ReadOnly = true;
            // 
            // colBorc
            // 
            colBorc.HeaderText = "Borç";
            colBorc.MinimumWidth = 6;
            colBorc.Name = "colBorc";
            colBorc.ReadOnly = true;
            colBorc.Width = 125;
            // 
            // colTahsilat
            // 
            colTahsilat.HeaderText = "Tahsilat";
            colTahsilat.MinimumWidth = 6;
            colTahsilat.Name = "colTahsilat";
            colTahsilat.ReadOnly = true;
            colTahsilat.Width = 125;
            // 
            // colBakiye
            // 
            colBakiye.HeaderText = "Bakiye";
            colBakiye.MinimumWidth = 6;
            colBakiye.Name = "colBakiye";
            colBakiye.ReadOnly = true;
            colBakiye.Width = 125;
            // 
            // pnlIslem
            // 
            pnlIslem.Controls.Add(label2);
            pnlIslem.Controls.Add(label1);
            pnlIslem.Controls.Add(txtIslemTutar);
            pnlIslem.Controls.Add(txtIslemAciklama);
            pnlIslem.Controls.Add(lblIslemBaslik);
            pnlIslem.Location = new Point(1033, 87);
            pnlIslem.Margin = new Padding(5, 4, 5, 4);
            pnlIslem.Name = "pnlIslem";
            pnlIslem.Padding = new Padding(12);
            pnlIslem.Size = new Size(335, 298);
            pnlIslem.TabIndex = 2;
            pnlIslem.Visible = false;
            pnlIslem.Paint += pnlIslem_Paint;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = SystemColors.Control;
            label2.Location = new Point(55, 88);
            label2.Name = "label2";
            label2.Size = new Size(98, 21);
            label2.TabIndex = 5;
            label2.Text = "AÇIKLAMA";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = SystemColors.Control;
            label1.Location = new Point(55, 182);
            label1.Name = "label1";
            label1.Size = new Size(65, 21);
            label1.TabIndex = 4;
            label1.Text = "TUTAR";
            // 
            // txtIslemTutar
            // 
            txtIslemTutar.BorderStyle = BorderStyle.FixedSingle;
            txtIslemTutar.Location = new Point(55, 221);
            txtIslemTutar.Margin = new Padding(5, 4, 5, 4);
            txtIslemTutar.Name = "txtIslemTutar";
            txtIslemTutar.Size = new Size(228, 29);
            txtIslemTutar.TabIndex = 3;
            txtIslemTutar.KeyDown += txtIslemTutar_KeyDown;
            // 
            // txtIslemAciklama
            // 
            txtIslemAciklama.BorderStyle = BorderStyle.FixedSingle;
            txtIslemAciklama.Location = new Point(55, 127);
            txtIslemAciklama.Margin = new Padding(5, 4, 5, 4);
            txtIslemAciklama.Name = "txtIslemAciklama";
            txtIslemAciklama.Size = new Size(228, 29);
            txtIslemAciklama.TabIndex = 2;
            txtIslemAciklama.KeyDown += txtIslemAciklama_KeyDown;
            // 
            // lblIslemBaslik
            // 
            lblIslemBaslik.AutoSize = true;
            lblIslemBaslik.Font = new Font("Segoe UI Black", 18F, FontStyle.Bold, GraphicsUnit.Point, 162);
            lblIslemBaslik.ForeColor = SystemColors.Control;
            lblIslemBaslik.Location = new Point(59, 25);
            lblIslemBaslik.Margin = new Padding(5, 0, 5, 0);
            lblIslemBaslik.Name = "lblIslemBaslik";
            lblIslemBaslik.Size = new Size(81, 32);
            lblIslemBaslik.TabIndex = 0;
            lblIslemBaslik.Text = "label1";
            // 
            // btnHareketSil
            // 
            btnHareketSil.Location = new Point(910, 710);
            btnHareketSil.Name = "btnHareketSil";
            btnHareketSil.Size = new Size(100, 46);
            btnHareketSil.TabIndex = 3;
            btnHareketSil.Text = "SİL";
            btnHareketSil.UseVisualStyleBackColor = true;
            btnHareketSil.Click += btnHareketSil_Click;
            // 
            // btnExcel
            // 
            btnExcel.Location = new Point(44, 710);
            btnExcel.Name = "btnExcel";
            btnExcel.Size = new Size(147, 46);
            btnExcel.TabIndex = 4;
            btnExcel.Text = "EXCEL AT";
            btnExcel.UseVisualStyleBackColor = true;
            btnExcel.Click += btnExcel_Click;
            // 
            // btnPdf
            // 
            btnPdf.Location = new Point(197, 710);
            btnPdf.Name = "btnPdf";
            btnPdf.Size = new Size(147, 46);
            btnPdf.TabIndex = 5;
            btnPdf.Text = "PDF AL";
            btnPdf.UseVisualStyleBackColor = true;
            btnPdf.Click += btnPdf_Click;
            // 
            // btnBorcEkle
            // 
            btnBorcEkle.Location = new Point(598, 708);
            btnBorcEkle.Name = "btnBorcEkle";
            btnBorcEkle.Size = new Size(121, 46);
            btnBorcEkle.TabIndex = 6;
            btnBorcEkle.Text = "F5 Borç";
            btnBorcEkle.UseVisualStyleBackColor = true;
            btnBorcEkle.Click += btnBorcEkle_Click;
            // 
            // btnTahsilatYap
            // 
            btnTahsilatYap.Location = new Point(747, 710);
            btnTahsilatYap.Name = "btnTahsilatYap";
            btnTahsilatYap.Size = new Size(136, 44);
            btnTahsilatYap.TabIndex = 7;
            btnTahsilatYap.Text = "F6 Tahsilat";
            btnTahsilatYap.UseVisualStyleBackColor = true;
            btnTahsilatYap.Click += btnTahsilatYap_Click;
            // 
            // CariDetayForm
            // 
            AutoScaleDimensions = new SizeF(10F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(26, 45, 91);
            ClientSize = new Size(1403, 808);
            Controls.Add(btnTahsilatYap);
            Controls.Add(btnBorcEkle);
            Controls.Add(btnPdf);
            Controls.Add(btnExcel);
            Controls.Add(btnHareketSil);
            Controls.Add(pnlIslem);
            Controls.Add(dgvHareketler);
            Controls.Add(lblMusteriAdi);
            Font = new Font("Segoe UI Black", 12F, FontStyle.Bold, GraphicsUnit.Point, 162);
            KeyPreview = true;
            Margin = new Padding(5, 4, 5, 4);
            Name = "CariDetayForm";
            Text = "Cari Detay";
            Load += CariDetayForm_Load;
            KeyDown += CariDetayForm_KeyDown;
            ((System.ComponentModel.ISupportInitialize)dgvHareketler).EndInit();
            pnlIslem.ResumeLayout(false);
            pnlIslem.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblMusteriAdi;
        private DataGridView dgvHareketler;
        private DataGridViewTextBoxColumn colTarih;
        private DataGridViewTextBoxColumn colAciklama;
        private DataGridViewTextBoxColumn colBorc;
        private DataGridViewTextBoxColumn colTahsilat;
        private DataGridViewTextBoxColumn colBakiye;
        private Panel pnlIslem;
        private TextBox txtIslemTutar;
        private TextBox txtIslemAciklama;
        private Label lblIslemBaslik;
        private Button btnHareketSil;
        private Button btnExcel;
        private Button btnPdf;
        private Button btnBorcEkle;
        private Button btnTahsilatYap;
        private Label label1;
        private Label label2;
    }
}