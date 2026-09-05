namespace MmkVeresiye
{
    partial class AnaForm
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
            components = new System.ComponentModel.Container();
            label1 = new Label();
            label2 = new Label();
            txtCariKod = new TextBox();
            txtCariUnvan = new TextBox();
            dgvListe = new DataGridView();
            ColumnCariKod = new DataGridViewTextBoxColumn();
            ColumnCariUnvan = new DataGridViewTextBoxColumn();
            ColumnTutar = new DataGridViewTextBoxColumn();
            btnExcelAktar = new Button();
            btnSil = new Button();
            label3 = new Label();
            txtAra = new TextBox();
            lblGenelBakiye = new Label();
            pnlYedek = new Panel();
            btnGeriYukle = new Button();
            btnYedekle = new Button();
            menuStrip1 = new MenuStrip();
            dosyaToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator = new ToolStripSeparator();
            toolStripSeparator1 = new ToolStripSeparator();
            toolStripSeparator2 = new ToolStripSeparator();
            exitToolStripMenuItem = new ToolStripMenuItem();
            düzenleToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator3 = new ToolStripSeparator();
            toolStripSeparator4 = new ToolStripSeparator();
            sifreDegistirToolStripMenuItem = new ToolStripMenuItem();
            araçlarToolStripMenuItem = new ToolStripMenuItem();
            gunlukRaporToolStripMenuItem = new ToolStripMenuItem();
            yardımToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator5 = new ToolStripSeparator();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            groupBox1 = new GroupBox();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            rbEksi = new RadioButton();
            rbArtı = new RadioButton();
            txtHizliTutar = new TextBox();
            txtHizliAciklama = new TextBox();
            cmbHizliCari = new ComboBox();
            timerSaat = new System.Windows.Forms.Timer(components);
            monthCalendar1 = new MonthCalendar();
            lblCanliSaat = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvListe).BeginInit();
            pnlYedek.SuspendLayout();
            menuStrip1.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = SystemColors.Control;
            label1.Location = new Point(696, 699);
            label1.Margin = new Padding(5, 0, 5, 0);
            label1.Name = "label1";
            label1.Size = new Size(88, 21);
            label1.TabIndex = 0;
            label1.Text = "CARİ KOD";
            label1.Visible = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = SystemColors.Control;
            label2.Location = new Point(14, 63);
            label2.Margin = new Padding(5, 0, 5, 0);
            label2.Name = "label2";
            label2.Size = new Size(114, 21);
            label2.TabIndex = 1;
            label2.Text = "CARİ ÜNVAN";
            // 
            // txtCariKod
            // 
            txtCariKod.Location = new Point(817, 696);
            txtCariKod.Margin = new Padding(5, 4, 5, 4);
            txtCariKod.Name = "txtCariKod";
            txtCariKod.ReadOnly = true;
            txtCariKod.Size = new Size(105, 29);
            txtCariKod.TabIndex = 3;
            txtCariKod.Visible = false;
            txtCariKod.KeyDown += txtCariKod_KeyDown;
            // 
            // txtCariUnvan
            // 
            txtCariUnvan.Location = new Point(138, 58);
            txtCariUnvan.Margin = new Padding(5, 4, 5, 4);
            txtCariUnvan.Name = "txtCariUnvan";
            txtCariUnvan.Size = new Size(142, 29);
            txtCariUnvan.TabIndex = 4;
            txtCariUnvan.KeyDown += txtCariUnvan_KeyDown;
            // 
            // dgvListe
            // 
            dgvListe.BackgroundColor = Color.FromArgb(26, 45, 91);
            dgvListe.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvListe.Columns.AddRange(new DataGridViewColumn[] { ColumnCariKod, ColumnCariUnvan, ColumnTutar });
            dgvListe.Location = new Point(14, 125);
            dgvListe.Margin = new Padding(5, 4, 5, 4);
            dgvListe.Name = "dgvListe";
            dgvListe.RowHeadersWidth = 51;
            dgvListe.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvListe.Size = new Size(948, 561);
            dgvListe.TabIndex = 6;
            dgvListe.CellDoubleClick += dgvListe_CellDoubleClick;
            dgvListe.KeyDown += dgvListe_KeyDown;
            // 
            // ColumnCariKod
            // 
            ColumnCariKod.HeaderText = "Cari Kod";
            ColumnCariKod.MinimumWidth = 6;
            ColumnCariKod.Name = "ColumnCariKod";
            ColumnCariKod.Width = 125;
            // 
            // ColumnCariUnvan
            // 
            ColumnCariUnvan.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            ColumnCariUnvan.HeaderText = "Cari Ünvan";
            ColumnCariUnvan.MinimumWidth = 6;
            ColumnCariUnvan.Name = "ColumnCariUnvan";
            // 
            // ColumnTutar
            // 
            ColumnTutar.HeaderText = "Tutar";
            ColumnTutar.MinimumWidth = 6;
            ColumnTutar.Name = "ColumnTutar";
            ColumnTutar.Width = 125;
            // 
            // btnExcelAktar
            // 
            btnExcelAktar.Location = new Point(14, 699);
            btnExcelAktar.Margin = new Padding(5, 4, 5, 4);
            btnExcelAktar.Name = "btnExcelAktar";
            btnExcelAktar.Size = new Size(153, 41);
            btnExcelAktar.TabIndex = 8;
            btnExcelAktar.Text = "Excel Aktar";
            btnExcelAktar.UseVisualStyleBackColor = true;
            btnExcelAktar.Click += btnExcel_Click;
            // 
            // btnSil
            // 
            btnSil.Location = new Point(817, 52);
            btnSil.Margin = new Padding(5, 4, 5, 4);
            btnSil.Name = "btnSil";
            btnSil.Size = new Size(133, 43);
            btnSil.TabIndex = 9;
            btnSil.Text = "CARİ SİL";
            btnSil.UseVisualStyleBackColor = true;
            btnSil.Click += btnSil_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = SystemColors.Control;
            label3.Location = new Point(290, 61);
            label3.Margin = new Padding(5, 0, 5, 0);
            label3.Name = "label3";
            label3.Size = new Size(122, 21);
            label3.TabIndex = 10;
            label3.Text = "MÜŞTERİ ARA";
            // 
            // txtAra
            // 
            txtAra.Location = new Point(422, 58);
            txtAra.Margin = new Padding(5, 4, 5, 4);
            txtAra.Name = "txtAra";
            txtAra.Size = new Size(148, 29);
            txtAra.TabIndex = 11;
            txtAra.TextChanged += txtAra_TextChanged;
            txtAra.KeyDown += txtAra_KeyDown;
            // 
            // lblGenelBakiye
            // 
            lblGenelBakiye.AutoSize = true;
            lblGenelBakiye.Font = new Font("Segoe UI Black", 36F, FontStyle.Bold, GraphicsUnit.Point, 162);
            lblGenelBakiye.ForeColor = Color.Red;
            lblGenelBakiye.Location = new Point(991, 667);
            lblGenelBakiye.Margin = new Padding(5, 0, 5, 0);
            lblGenelBakiye.Name = "lblGenelBakiye";
            lblGenelBakiye.Size = new Size(204, 65);
            lblGenelBakiye.TabIndex = 12;
            lblGenelBakiye.Text = "BAKİYE";
            // 
            // pnlYedek
            // 
            pnlYedek.BackColor = Color.FromArgb(26, 45, 91);
            pnlYedek.BorderStyle = BorderStyle.FixedSingle;
            pnlYedek.Controls.Add(btnGeriYukle);
            pnlYedek.Controls.Add(btnYedekle);
            pnlYedek.Location = new Point(327, 395);
            pnlYedek.Name = "pnlYedek";
            pnlYedek.Size = new Size(168, 130);
            pnlYedek.TabIndex = 13;
            // 
            // btnGeriYukle
            // 
            btnGeriYukle.Location = new Point(16, 81);
            btnGeriYukle.Name = "btnGeriYukle";
            btnGeriYukle.Size = new Size(130, 41);
            btnGeriYukle.TabIndex = 2;
            btnGeriYukle.Text = "Geri Yükle";
            btnGeriYukle.UseVisualStyleBackColor = true;
            btnGeriYukle.Click += btnGeriYukle_Click;
            // 
            // btnYedekle
            // 
            btnYedekle.Location = new Point(16, 17);
            btnYedekle.Name = "btnYedekle";
            btnYedekle.Size = new Size(130, 42);
            btnYedekle.TabIndex = 0;
            btnYedekle.Text = "Yedekle";
            btnYedekle.UseVisualStyleBackColor = true;
            btnYedekle.Click += btnYedekle_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { dosyaToolStripMenuItem, düzenleToolStripMenuItem, araçlarToolStripMenuItem, yardımToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1344, 24);
            menuStrip1.TabIndex = 14;
            menuStrip1.Text = "menuStrip1";
            // 
            // dosyaToolStripMenuItem
            // 
            dosyaToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { toolStripSeparator, toolStripSeparator1, toolStripSeparator2, exitToolStripMenuItem });
            dosyaToolStripMenuItem.Name = "dosyaToolStripMenuItem";
            dosyaToolStripMenuItem.Size = new Size(51, 20);
            dosyaToolStripMenuItem.Text = "&Dosya";
            // 
            // toolStripSeparator
            // 
            toolStripSeparator.Name = "toolStripSeparator";
            toolStripSeparator.Size = new Size(96, 6);
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(96, 6);
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(96, 6);
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(99, 22);
            exitToolStripMenuItem.Text = "Çı&kış";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // düzenleToolStripMenuItem
            // 
            düzenleToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { toolStripSeparator3, toolStripSeparator4, sifreDegistirToolStripMenuItem });
            düzenleToolStripMenuItem.Name = "düzenleToolStripMenuItem";
            düzenleToolStripMenuItem.Size = new Size(61, 20);
            düzenleToolStripMenuItem.Text = "&Düzenle";
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(137, 6);
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(137, 6);
            // 
            // sifreDegistirToolStripMenuItem
            // 
            sifreDegistirToolStripMenuItem.Name = "sifreDegistirToolStripMenuItem";
            sifreDegistirToolStripMenuItem.Size = new Size(140, 22);
            sifreDegistirToolStripMenuItem.Text = "Şifre Değiştir";
            sifreDegistirToolStripMenuItem.Click += sifreDegistirToolStripMenuItem_Click;
            // 
            // araçlarToolStripMenuItem
            // 
            araçlarToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { gunlukRaporToolStripMenuItem });
            araçlarToolStripMenuItem.Name = "araçlarToolStripMenuItem";
            araçlarToolStripMenuItem.Size = new Size(56, 20);
            araçlarToolStripMenuItem.Text = "&Araçlar";
            // 
            // gunlukRaporToolStripMenuItem
            // 
            gunlukRaporToolStripMenuItem.Name = "gunlukRaporToolStripMenuItem";
            gunlukRaporToolStripMenuItem.Size = new Size(161, 22);
            gunlukRaporToolStripMenuItem.Text = "Günlük Rapor F3";
            gunlukRaporToolStripMenuItem.Click += gunlukRaporToolStripMenuItem_Click;
            // 
            // yardımToolStripMenuItem
            // 
            yardımToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { toolStripSeparator5, aboutToolStripMenuItem });
            yardımToolStripMenuItem.Name = "yardımToolStripMenuItem";
            yardımToolStripMenuItem.Size = new Size(56, 20);
            yardımToolStripMenuItem.Text = "&Yardım";
            // 
            // toolStripSeparator5
            // 
            toolStripSeparator5.Name = "toolStripSeparator5";
            toolStripSeparator5.Size = new Size(94, 6);
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new Size(97, 22);
            aboutToolStripMenuItem.Text = "Bilgi";
            aboutToolStripMenuItem.Click += aboutToolStripMenuItem_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(rbEksi);
            groupBox1.Controls.Add(rbArtı);
            groupBox1.Controls.Add(txtHizliTutar);
            groupBox1.Controls.Add(txtHizliAciklama);
            groupBox1.Controls.Add(cmbHizliCari);
            groupBox1.ForeColor = SystemColors.Control;
            groupBox1.Location = new Point(993, 294);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(336, 312);
            groupBox1.TabIndex = 15;
            groupBox1.TabStop = false;
            groupBox1.Text = "HIZLI İŞLEM";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(28, 54);
            label6.Name = "label6";
            label6.Size = new Size(88, 21);
            label6.TabIndex = 7;
            label6.Text = "CARİ ARA";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(28, 219);
            label5.Name = "label5";
            label5.Size = new Size(65, 21);
            label5.TabIndex = 6;
            label5.Text = "TUTAR";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(28, 138);
            label4.Name = "label4";
            label4.Size = new Size(98, 21);
            label4.TabIndex = 5;
            label4.Text = "AÇIKLAMA";
            // 
            // rbEksi
            // 
            rbEksi.AutoSize = true;
            rbEksi.ForeColor = Color.Red;
            rbEksi.Location = new Point(203, 22);
            rbEksi.Name = "rbEksi";
            rbEksi.Size = new Size(35, 25);
            rbEksi.TabIndex = 4;
            rbEksi.TabStop = true;
            rbEksi.Text = "-";
            rbEksi.UseVisualStyleBackColor = true;
            // 
            // rbArtı
            // 
            rbArtı.AutoSize = true;
            rbArtı.Checked = true;
            rbArtı.ForeColor = Color.Olive;
            rbArtı.Location = new Point(150, 22);
            rbArtı.Name = "rbArtı";
            rbArtı.Size = new Size(39, 25);
            rbArtı.TabIndex = 3;
            rbArtı.TabStop = true;
            rbArtı.Text = "+";
            rbArtı.UseVisualStyleBackColor = true;
            // 
            // txtHizliTutar
            // 
            txtHizliTutar.Location = new Point(28, 255);
            txtHizliTutar.Name = "txtHizliTutar";
            txtHizliTutar.Size = new Size(227, 29);
            txtHizliTutar.TabIndex = 2;
            // 
            // txtHizliAciklama
            // 
            txtHizliAciklama.Location = new Point(28, 176);
            txtHizliAciklama.Name = "txtHizliAciklama";
            txtHizliAciklama.Size = new Size(227, 29);
            txtHizliAciklama.TabIndex = 1;
            // 
            // cmbHizliCari
            // 
            cmbHizliCari.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbHizliCari.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbHizliCari.FormattingEnabled = true;
            cmbHizliCari.Location = new Point(28, 94);
            cmbHizliCari.Name = "cmbHizliCari";
            cmbHizliCari.Size = new Size(227, 29);
            cmbHizliCari.TabIndex = 0;
            // 
            // timerSaat
            // 
            timerSaat.Enabled = true;
            timerSaat.Interval = 1000;
            timerSaat.Tick += timerSaat_Tick;
            // 
            // monthCalendar1
            // 
            monthCalendar1.Location = new Point(991, 101);
            monthCalendar1.Name = "monthCalendar1";
            monthCalendar1.TabIndex = 16;
            // 
            // lblCanliSaat
            // 
            lblCanliSaat.AutoSize = true;
            lblCanliSaat.Font = new Font("Segoe UI Black", 18F, FontStyle.Bold, GraphicsUnit.Point, 162);
            lblCanliSaat.ForeColor = SystemColors.ControlLightLight;
            lblCanliSaat.Location = new Point(993, 37);
            lblCanliSaat.Name = "lblCanliSaat";
            lblCanliSaat.Size = new Size(84, 32);
            lblCanliSaat.TabIndex = 17;
            lblCanliSaat.Text = "label4";
            // 
            // AnaForm
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(26, 45, 91);
            ClientSize = new Size(1344, 764);
            Controls.Add(lblCanliSaat);
            Controls.Add(monthCalendar1);
            Controls.Add(groupBox1);
            Controls.Add(pnlYedek);
            Controls.Add(lblGenelBakiye);
            Controls.Add(txtAra);
            Controls.Add(label3);
            Controls.Add(btnSil);
            Controls.Add(btnExcelAktar);
            Controls.Add(dgvListe);
            Controls.Add(txtCariUnvan);
            Controls.Add(txtCariKod);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(menuStrip1);
            Font = new Font("Segoe UI Black", 12F, FontStyle.Bold, GraphicsUnit.Point, 162);
            MainMenuStrip = menuStrip1;
            Margin = new Padding(5, 4, 5, 4);
            Name = "AnaForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MMK Veresiye Takip Programı";
            Load += AnaForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvListe).EndInit();
            pnlYedek.ResumeLayout(false);
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox txtCariKod;
        private TextBox txtCariUnvan;
        private DataGridView dgvListe;
        private Button btnExcelAktar;
        private Button btnSil;
        private Label label3;
        private TextBox txtAra;
        private Label lblGenelBakiye;
        private Panel pnlYedek;
        private Button btnYedekle;
        private Button btnGeriYukle;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem dosyaToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem düzenleToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripMenuItem sifreDegistirToolStripMenuItem;
        private ToolStripMenuItem araçlarToolStripMenuItem;
        private ToolStripMenuItem gunlukRaporToolStripMenuItem;
        private ToolStripMenuItem yardımToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private GroupBox groupBox1;
        private TextBox txtHizliTutar;
        private TextBox txtHizliAciklama;
        private ComboBox cmbHizliCari;
        private RadioButton rbEksi;
        private RadioButton rbArtı;
        private System.Windows.Forms.Timer timerSaat;
        private MonthCalendar monthCalendar1;
        private Label lblCanliSaat;
        private Label label5;
        private Label label4;
        private Label label6;
        private DataGridViewTextBoxColumn ColumnCariKod;
        private DataGridViewTextBoxColumn ColumnCariUnvan;
        private DataGridViewTextBoxColumn ColumnTutar;
    }
}