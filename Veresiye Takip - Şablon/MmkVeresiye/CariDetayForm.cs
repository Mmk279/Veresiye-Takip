using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Linq;
using ExcelDataReader;
using System.Data;

namespace MmkVeresiye
{
    public partial class CariDetayForm : Form
    {
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string CariKod { get; set; } = "";

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string CariUnvan { get; set; } = "";

        private string dosyaYolu = "";
        private Color cerceveRengi = Color.Red;
        private bool duzenlemeModu = false;
        private int duzenlenecekSatirIndex = -1;

        private Button? btnF2HareketDuzenle;
        private Button? btnExceldenAlKodIle;
        private Button? btnBilgiFisiYazdir;
        private Panel? pnlBakiyeKutusu;
        private Label? lblBakiyeBaslik;
        private Label? lblGuncelCariBakiye;

        private DateTimePicker? dtpIslemTarihi;
        private Label? lblTarihEtiket;

        // Sabit kültür — tüm parse/format işlemlerinde kullanılır
        private static readonly System.Globalization.CultureInfo _trKultur =
            new System.Globalization.CultureInfo("tr-TR");

        public CariDetayForm()
        {
            InitializeComponent();
            this.KeyPreview = true;
        }

        private void CariDetayForm_Load(object sender, EventArgs e)
        {
            lblMusteriAdi.Text = CariUnvan;
            dosyaYolu = $"detay_{CariKod}.mmk";
            pnlIslem.Visible = false;

            dgvHareketler.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHareketler.MultiSelect = true;

            // ÖNEMLİ: Sütun başlığına tıklanınca sıralama değişmesin
            foreach (DataGridViewColumn col in dgvHareketler.Columns)
                col.SortMode = DataGridViewColumnSortMode.NotSortable;

            DinamikKontrolleriOlustur();
            IslemPaneliTasariminiDuzenle();
            GridYapısınıDuzenle();
            HareketleriYukle();
        }

        private void DinamikKontrolleriOlustur()
        {
            if (btnF2HareketDuzenle == null)
            {
                btnF2HareketDuzenle = new Button()
                {
                    Text = "✏️ Düzenle (F2)",
                    Size = new Size(130, 40),
                    Location = new Point(this.Width - 160, 20),
                    BackColor = Color.FromArgb(52, 152, 219),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI Black", 10, FontStyle.Bold),
                    Cursor = Cursors.Hand
                };
                btnF2HareketDuzenle.FlatAppearance.BorderSize = 0;
                btnF2HareketDuzenle.Click += (s, ev) => SeciliHareketiDuzenle();
                this.Controls.Add(btnF2HareketDuzenle);
                btnF2HareketDuzenle.BringToFront();
            }

            if (btnExceldenAlKodIle == null)
            {
                btnExceldenAlKodIle = new Button()
                {
                    Text = "📥 Excel'den Al",
                    Size = new Size(130, 40),
                    Location = new Point(btnF2HareketDuzenle.Left - 140, btnF2HareketDuzenle.Top),
                    BackColor = Color.FromArgb(46, 204, 113),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI Black", 10, FontStyle.Bold),
                    Cursor = Cursors.Hand
                };
                btnExceldenAlKodIle.FlatAppearance.BorderSize = 0;
                btnExceldenAlKodIle.Click += (s, ev) => btnExceldenAl_Click(s ?? this, ev ?? EventArgs.Empty);
                this.Controls.Add(btnExceldenAlKodIle);
                btnExceldenAlKodIle.BringToFront();
            }

            if (btnBilgiFisiYazdir == null)
            {
                btnBilgiFisiYazdir = new Button()
                {
                    Text = "🧾 Seçilenleri Yazdır",
                    Size = new Size(160, 40),
                    Location = new Point(btnExceldenAlKodIle.Left - 170, btnExceldenAlKodIle.Top),
                    BackColor = Color.FromArgb(155, 89, 182),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI Black", 10, FontStyle.Bold),
                    Cursor = Cursors.Hand
                };
                btnBilgiFisiYazdir.FlatAppearance.BorderSize = 0;
                btnBilgiFisiYazdir.Click += (s, ev) => SeciliHareketIcinBilgiFisiYazdir();
                this.Controls.Add(btnBilgiFisiYazdir);
                btnBilgiFisiYazdir.BringToFront();
            }

            if (pnlBakiyeKutusu == null)
            {
                pnlBakiyeKutusu = new Panel()
                {
                    Size = new Size(280, 85),
                    Location = new Point(this.Width - 310, this.Height - 140),
                    Anchor = AnchorStyles.Bottom | AnchorStyles.Right,
                    BackColor = Color.FromArgb(24, 30, 54),
                    BorderStyle = BorderStyle.FixedSingle
                };

                lblBakiyeBaslik = new Label()
                {
                    Text = "GÜNCEL BAKİYE",
                    Location = new Point(10, 8),
                    Size = new Size(200, 15),
                    Font = new Font("Segoe UI", 9, FontStyle.Bold),
                    ForeColor = Color.FromArgb(155, 168, 184)
                };

                lblGuncelCariBakiye = new Label()
                {
                    Text = "0,00",
                    Location = new Point(10, 28),
                    Size = new Size(260, 45),
                    TextAlign = ContentAlignment.MiddleRight,
                    Font = new Font("Segoe UI Black", 24, FontStyle.Bold),
                    ForeColor = Color.White
                };

                pnlBakiyeKutusu.Controls.Add(lblBakiyeBaslik);
                pnlBakiyeKutusu.Controls.Add(lblGuncelCariBakiye);
                this.Controls.Add(pnlBakiyeKutusu);
                pnlBakiyeKutusu.BringToFront();
            }
        }

        private void IslemPaneliTasariminiDuzenle()
        {
            if (dtpIslemTarihi == null)
            {
                lblTarihEtiket = new Label()
                {
                    Text = "İŞLEM TARİHİ VE SAATİ",
                    Font = new Font("Segoe UI", 9, FontStyle.Bold),
                    ForeColor = Color.White,
                    Location = new Point(txtIslemAciklama.Left, 65),
                    Size = new Size(200, 18)
                };

                dtpIslemTarihi = new DateTimePicker()
                {
                    Format = DateTimePickerFormat.Custom,
                    CustomFormat = "dd.MM.yyyy HH:mm",
                    Width = txtIslemAciklama.Width,
                    Location = new Point(txtIslemAciklama.Left, 88),
                    Font = new Font("Segoe UI", 11, FontStyle.Regular),
                    BackColor = Color.White,
                    ForeColor = Color.Black
                };

                pnlIslem.Controls.Add(lblTarihEtiket);
                pnlIslem.Controls.Add(dtpIslemTarihi);
            }

            txtIslemAciklama.Location = new Point(txtIslemAciklama.Left, 155);

            foreach (Control ctrl in pnlIslem.Controls)
            {
                if (ctrl is Label && ctrl.Text.Contains("AÇIKLAMA"))
                    ctrl.Location = new Point(txtIslemAciklama.Left, 132);
                if (ctrl is Label && (ctrl.Text.Contains("TUTAR") || ctrl.Text.Contains("TL")))
                    ctrl.Location = new Point(txtIslemTutar.Left, 202);
            }

            txtIslemTutar.Location = new Point(txtIslemTutar.Left, 225);

            if (lblTarihEtiket != null) lblTarihEtiket.BringToFront();
            if (dtpIslemTarihi != null) dtpIslemTarihi.BringToFront();
            txtIslemAciklama.BringToFront();
            txtIslemTutar.BringToFront();
        }

        private void GridYapısınıDuzenle()
        {
            if (dgvHareketler.Columns.Count > 4)
                dgvHareketler.Columns[4].Visible = false;
        }

        private void btnBorcEkle_Click(object sender, EventArgs e)
        {
            duzenlemeModu = false;
            if (dtpIslemTarihi != null) dtpIslemTarihi.Value = DateTime.Now;
            BorcPaneliniAc();
        }

        private void btnTahsilatYap_Click(object sender, EventArgs e)
        {
            duzenlemeModu = false;
            if (dtpIslemTarihi != null) dtpIslemTarihi.Value = DateTime.Now;
            TahsilatPaneliniAc();
        }

        private void BorcPaneliniAc()
        {
            cerceveRengi = Color.Red;
            lblIslemBaslik.Text = "BORÇ GİRİŞİ";
            lblIslemBaslik.ForeColor = Color.Red;
            pnlIslem.Visible = true;
            pnlIslem.Invalidate();
            txtIslemAciklama.Focus();
        }

        private void TahsilatPaneliniAc()
        {
            cerceveRengi = Color.Green;
            lblIslemBaslik.Text = "TAHSİLAT GİRİŞİ";
            lblIslemBaslik.ForeColor = Color.Green;
            pnlIslem.Visible = true;
            pnlIslem.Invalidate();
            txtIslemAciklama.Focus();
        }

        // Tarihi güvenli parse eden yardımcı metod
        private DateTime TarihParse(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return DateTime.MinValue;

            // Önce tr-TR formatını dene
            string[] formatlar = {
                "dd.MM.yyyy HH:mm",
                "dd.MM.yyyy H:mm",
                "dd.MM.yyyy",
                "d.M.yyyy HH:mm",
                "d.M.yyyy",
                "yyyy-MM-dd HH:mm:ss",
                "yyyy-MM-dd HH:mm",
                "yyyy-MM-dd",
                "MM/dd/yyyy HH:mm",
                "MM/dd/yyyy"
            };

            foreach (var fmt in formatlar)
            {
                if (DateTime.TryParseExact(s, fmt, _trKultur,
                    System.Globalization.DateTimeStyles.None, out DateTime dt))
                    return dt;
            }

            // Son çare: genel parse
            if (DateTime.TryParse(s, _trKultur,
                System.Globalization.DateTimeStyles.None, out DateTime sonuc))
                return sonuc;

            return DateTime.MinValue;
        }

        // Tutarı güvenli parse eden yardımcı metod (1.234,56 veya 1234.56 her iki format)
        private double TutarParse(string? s)
        {
            if (string.IsNullOrWhiteSpace(s)) return 0;
            s = s.Trim().Replace(" ", "").Replace("TL", "").Replace("₺", "");

            // tr-TR: binlik ayraç nokta, ondalık virgül → 1.234,56
            if (double.TryParse(s, System.Globalization.NumberStyles.Any, _trKultur, out double v1))
                return v1;

            // en-US: binlik ayraç virgül, ondalık nokta → 1,234.56
            if (double.TryParse(s, System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture, out double v2))
                return v2;

            return 0;
        }

        private void btnExceldenAl_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "Excel Dosyası|*.xlsx;*.xls", Multiselect = false })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        int eklenenAdet = 0;

                        using (var stream = File.Open(ofd.FileName, FileMode.Open, FileAccess.Read))
                        using (var reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            var result = reader.AsDataSet();
                            DataTable? dtTablo = result.Tables.Count > 0 ? result.Tables[0] : null;

                            if (dtTablo != null)
                            {
                                for (int i = 2; i < dtTablo.Rows.Count; i++)
                                {
                                    DataRow satir = dtTablo.Rows[i];

                                    if (satir[0] == DBNull.Value && satir[1] == DBNull.Value
                                        && satir[3] == DBNull.Value) continue;

                                    string kontrolMetni = satir[4]?.ToString() ?? "";
                                    if (kontrolMetni.Contains("Liste") || kontrolMetni.Contains("Bakiye")) continue;

                                    // Tarih parse
                                    string tarihSaat = "";
                                    if (satir[0] != DBNull.Value)
                                    {
                                        DateTime dtTarih = DateTime.MinValue;
                                        string tarihStr = satir[0]?.ToString() ?? "";

                                        // Excel OLE Automation Date: tam sayı kısmı gün, ondalık kısmı saat
                                        // Örnek: 46024.5 = 12.06.2026 12:00
                                        // Sadece 1'den büyük değerler tarih olabilir (0.x = sadece saat, tarih yok)
                                        if (double.TryParse(tarihStr, System.Globalization.NumberStyles.Any,
                                            System.Globalization.CultureInfo.InvariantCulture, out double oleDate)
                                            && oleDate > 1)
                                        {
                                            // OADate'in hem tarih hem saat bilgisini doğrudan al
                                            dtTarih = DateTime.FromOADate(oleDate);
                                        }
                                        else
                                        {
                                            dtTarih = TarihParse(tarihStr);
                                        }

                                        if (dtTarih != DateTime.MinValue)
                                        {
                                            // Saat ayrı sütunda (satir[1]) varsa ve dtTarih'in saati 00:00 ise ekle
                                            // (OADate zaten saati içeriyorsa satir[1]'i yoksay)
                                            string saatStr = satir[1] != DBNull.Value
                                                ? (satir[1]?.ToString() ?? "").Trim() : "";

                                            TimeSpan ekSaat = TimeSpan.Zero;
                                            bool saatAyriSutundaVar = false;

                                            if (!string.IsNullOrWhiteSpace(saatStr))
                                            {
                                                // OLE saat değeri: 0 ile 1 arasında double (örn: 0.5 = 12:00)
                                                if (double.TryParse(saatStr, System.Globalization.NumberStyles.Any,
                                                    System.Globalization.CultureInfo.InvariantCulture, out double oleSaat)
                                                    && oleSaat >= 0 && oleSaat < 1)
                                                {
                                                    ekSaat = TimeSpan.FromDays(oleSaat);
                                                    saatAyriSutundaVar = true;
                                                }
                                                // "HH:mm" veya "HH:mm:ss" metin formatı
                                                else if (TimeSpan.TryParse(saatStr, out TimeSpan tsText))
                                                {
                                                    ekSaat = tsText;
                                                    saatAyriSutundaVar = true;
                                                }
                                            }

                                            // Eğer OADate'in saati 00:00 ise ve ayrı sütunda saat varsa uygula
                                            // (31.12 sorunu: OADate'de saat 0 geliyorsa, gerçek saat satir[1]'de)
                                            if (saatAyriSutundaVar && dtTarih.TimeOfDay == TimeSpan.Zero)
                                                tarihSaat = dtTarih.Date.Add(ekSaat).ToString("dd.MM.yyyy HH:mm", _trKultur);
                                            else
                                                tarihSaat = dtTarih.ToString("dd.MM.yyyy HH:mm", _trKultur);
                                        }
                                    }

                                    string aciklama = satir[3] != DBNull.Value
                                        ? (satir[3]?.ToString() ?? "").Trim() : "";
                                    if (string.IsNullOrEmpty(aciklama)) aciklama = "Dışarıdan Aktarılan İşlem";

                                    double borcMiktar = TutarParse(satir[4]?.ToString());
                                    double tahsilatMiktar = TutarParse(satir[5]?.ToString());

                                    string borc = borcMiktar.ToString("N2", _trKultur);
                                    string tahsilat = tahsilatMiktar.ToString("N2", _trKultur);

                                    int n = dgvHareketler.Rows.Add(tarihSaat, aciklama, borc, tahsilat);
                                    dgvHareketler.Rows[n].DefaultCellStyle.ForeColor =
                                        borcMiktar > 0 ? Color.Red : Color.Green;

                                    eklenenAdet++;
                                }
                            }
                        }

                        SatirlariTarihGoreSirala();
                        HareketleriDosyayaYaz();
                        HareketleriYukle();

                        MessageBox.Show($"{eklenenAdet} adet hareket aktarıldı.",
                            "Aktarım Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Excel dosyası okunurken hata: " + ex.Message,
                            "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnPdf_Click(object sender, EventArgs e)
        {
            try
            {
                string klasor = Path.Combine(Application.StartupPath, "Ekstreler");
                if (!Directory.Exists(klasor)) Directory.CreateDirectory(klasor);
                string dosyaAdi = Path.Combine(klasor, $"{CariUnvan}_Ekstre.html");

                StringBuilder html = new StringBuilder();
                html.Append("<html><head><meta charset='utf-8'><style>table{width:100%;border-collapse:collapse;}th,td{border:1px solid black;padding:8px;}th{background:#eee;}</style></head><body>");
                html.Append($"<h2>{CariUnvan} - Müşteri Ekstresi</h2><table><tr><th>Tarih</th><th>Açıklama</th><th>Borç</th><th>Tahsilat</th></tr>");
                foreach (DataGridViewRow satir in dgvHareketler.Rows)
                    if (!satir.IsNewRow)
                        html.Append($"<tr><td>{satir.Cells[0].Value ?? ""}</td><td>{satir.Cells[1].Value ?? ""}</td><td>{satir.Cells[2].Value ?? ""}</td><td>{satir.Cells[3].Value ?? ""}</td></tr>");
                html.Append("</table></body></html>");
                File.WriteAllText(dosyaAdi, html.ToString(), Encoding.UTF8);
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(dosyaAdi) { UseShellExecute = true });
            }
            catch (Exception ex) { MessageBox.Show("Hata: " + ex.Message); }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog { Filter = "Excel Dosyası|*.csv", FileName = $"{CariUnvan}_Hareketleri" };
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("Tarih;Aciklama;Borc;Tahsilat");
                    foreach (DataGridViewRow satir in dgvHareketler.Rows)
                        if (!satir.IsNewRow)
                            sb.AppendLine($"{satir.Cells[0].Value ?? ""};{satir.Cells[1].Value ?? ""};{satir.Cells[2].Value ?? ""};{satir.Cells[3].Value ?? ""}");
                    File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);
                    MessageBox.Show("Excel başarıyla oluşturuldu.");
                }
            }
            catch (Exception ex) { MessageBox.Show("Excel Hatası: " + ex.Message); }
        }

        private void btnHareketSil_Click(object sender, EventArgs e)
        {
            if (dgvHareketler.SelectedRows.Count > 0 &&
                MessageBox.Show("Seçili hareketler silinsin mi?", "Onay",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                foreach (DataGridViewRow satir in dgvHareketler.SelectedRows)
                    if (!satir.IsNewRow) dgvHareketler.Rows.Remove(satir);
                HareketleriDosyayaYaz();
                HareketleriYukle();
            }
        }

        private void SeciliHareketiDuzenle()
        {
            if (dgvHareketler.CurrentRow != null && !dgvHareketler.CurrentRow.IsNewRow)
            {
                duzenlemeModu = true;
                duzenlenecekSatirIndex = dgvHareketler.CurrentRow.Index;

                DataGridViewRow satir = dgvHareketler.Rows[duzenlenecekSatirIndex];
                txtIslemAciklama.Text = satir.Cells[1].Value?.ToString() ?? "";

                string mevcutTarihStr = satir.Cells[0].Value?.ToString() ?? "";
                DateTime mevcutTarih = TarihParse(mevcutTarihStr);
                if (dtpIslemTarihi != null)
                    dtpIslemTarihi.Value = mevcutTarih != DateTime.MinValue ? mevcutTarih : DateTime.Now;

                double borc = TutarParse(satir.Cells[2].Value?.ToString());
                double tahsilat = TutarParse(satir.Cells[3].Value?.ToString());

                if (borc > 0)
                {
                    BorcPaneliniAc();
                    txtIslemTutar.Text = borc.ToString("N2", _trKultur);
                }
                else
                {
                    TahsilatPaneliniAc();
                    txtIslemTutar.Text = tahsilat.ToString("N2", _trKultur);
                }
                lblIslemBaslik.Text = "HAREKET GÜNCELLE";
            }
            else
            {
                MessageBox.Show("Lütfen düzenlemek istediğiniz harekete tıklayın.",
                    "İşlem Seçilemedi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void SeciliHareketIcinBilgiFisiYazdir()
        {
            var seciliSatirlar = new System.Collections.Generic.List<DataGridViewRow>();
            foreach (DataGridViewRow r in dgvHareketler.SelectedRows)
                if (!r.IsNewRow) seciliSatirlar.Add(r);

            if (seciliSatirlar.Count == 0 && dgvHareketler.CurrentRow != null && !dgvHareketler.CurrentRow.IsNewRow)
                seciliSatirlar.Add(dgvHareketler.CurrentRow);

            if (seciliSatirlar.Count == 0)
            {
                MessageBox.Show("Lütfen fiş yazdırmak istediğiniz hareket(ler)i seçin.",
                    "Hareket Seçilmedi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            seciliSatirlar.Sort((a, b) => a.Index.CompareTo(b.Index));

            var hareketler = new System.Collections.Generic.List<(string Tarih, string Aciklama, string Tutar)>();
            foreach (DataGridViewRow satir in seciliSatirlar)
            {
                string tarih = satir.Cells[0].Value?.ToString() ?? "";
                string aciklama = satir.Cells[1].Value?.ToString() ?? "";
                double borc = TutarParse(satir.Cells[2].Value?.ToString());
                double tahsilat = TutarParse(satir.Cells[3].Value?.ToString());
                string tutar = (borc > 0 ? borc : tahsilat).ToString("N2", _trKultur);
                hareketler.Add((tarih, aciklama, tutar));
            }

            BilgiFisiYardimci.YazdirCoklu(CariUnvan, hareketler);
        }

        // ÖNEMLİ: Tarihe göre ESKİDEN YENİYE (artan) sıralar.
        // Kullanıcı en eski işlemi üstte, en yeni işlemi altta görmek ister.
        private void SatirlariTarihGoreSirala()
        {
            var veriler = new System.Collections.Generic.List<(DateTime tarih, string tarihStr, string aciklama, string borc, string tahsilat, Color renk)>();

            foreach (DataGridViewRow satir in dgvHareketler.Rows)
            {
                if (satir.IsNewRow) continue;

                string tarihStr = satir.Cells[0].Value?.ToString() ?? "";
                DateTime tarih = TarihParse(tarihStr);

                veriler.Add((
                    tarih,
                    tarihStr,
                    satir.Cells[1].Value?.ToString() ?? "",
                    satir.Cells[2].Value?.ToString() ?? "0,00",
                    satir.Cells[3].Value?.ToString() ?? "0,00",
                    satir.DefaultCellStyle.ForeColor
                ));
            }

            // YENİDEN ESKİYE sırala (OrderByDescending = en yeni en üstte)
            var sirali = veriler.OrderByDescending(v => v.tarih).ToList();

            dgvHareketler.Rows.Clear();
            foreach (var v in sirali)
            {
                int n = dgvHareketler.Rows.Add(v.tarihStr, v.aciklama, v.borc, v.tahsilat);
                dgvHareketler.Rows[n].DefaultCellStyle.ForeColor = v.renk;
            }
        }

        private void HareketleriDosyayaYaz()
        {
            StringBuilder sb = new StringBuilder();
            foreach (DataGridViewRow satir in dgvHareketler.Rows)
                if (!satir.IsNewRow)
                    sb.AppendLine($"{satir.Cells[0].Value ?? ""}|{satir.Cells[1].Value ?? ""}|{satir.Cells[2].Value ?? ""}|{satir.Cells[3].Value ?? ""}");
            File.WriteAllText(dosyaYolu, sb.ToString(), Encoding.UTF8);
        }

        private void HareketleriDosyayaYazYeniden() => HareketleriDosyayaYaz();

        private void BakiyeHesapla()
        {
            double toplamBakiye = 0;
            foreach (DataGridViewRow satir in dgvHareketler.Rows)
            {
                if (!satir.IsNewRow)
                {
                    toplamBakiye += TutarParse(satir.Cells[2].Value?.ToString())
                                  - TutarParse(satir.Cells[3].Value?.ToString());
                }
            }

            if (lblGuncelCariBakiye != null && pnlBakiyeKutusu != null)
            {
                lblGuncelCariBakiye.Text = toplamBakiye.ToString("N2", _trKultur);

                if (toplamBakiye > 0)
                {
                    lblGuncelCariBakiye.ForeColor = Color.FromArgb(231, 76, 60);
                    pnlBakiyeKutusu.ForeColor = Color.FromArgb(231, 76, 60);
                }
                else if (toplamBakiye < 0)
                {
                    lblGuncelCariBakiye.ForeColor = Color.FromArgb(46, 204, 113);
                    pnlBakiyeKutusu.ForeColor = Color.FromArgb(46, 204, 113);
                }
                else
                {
                    lblGuncelCariBakiye.ForeColor = Color.White;
                    pnlBakiyeKutusu.ForeColor = Color.White;
                }
            }
        }

        private void HareketleriYukle()
        {
            dgvHareketler.Rows.Clear();
            if (File.Exists(dosyaYolu))
            {
                string[] tumSatirlar = File.ReadAllLines(dosyaYolu, Encoding.UTF8);
                foreach (string s in tumSatirlar)
                {
                    if (string.IsNullOrWhiteSpace(s)) continue;
                    string[] p = s.Split('|');
                    if (p.Length >= 4) HareketiTabloyaEkle(p[0], p[1], p[2], p[3]);
                }
                SatirlariTarihGoreSirala();
                BakiyeHesapla();
            }
        }

        private void HareketiTabloyaEkle(string? tarih, string? aciklama, string? borc, string? tahsilat)
        {
            int n = dgvHareketler.Rows.Add(tarih ?? "", aciklama ?? "", borc ?? "0,00", tahsilat ?? "0,00");
            double b = TutarParse(borc);
            dgvHareketler.Rows[n].DefaultCellStyle.ForeColor = b > 0 ? Color.Red : Color.Green;
        }

        private void CariDetayForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (pnlIslem.Visible) pnlIslem.Visible = false;
                else this.Close();
            }
            if (e.KeyCode == Keys.F5) btnBorcEkle.PerformClick();
            if (e.KeyCode == Keys.F6) btnTahsilatYap.PerformClick();
            if (e.KeyCode == Keys.F2) SeciliHareketiDuzenle();
        }

        private void txtIslemAciklama_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { txtIslemTutar.Focus(); e.SuppressKeyPress = true; }
        }

        private void txtIslemTutar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                double tutar = TutarParse(txtIslemTutar.Text);

                if (tutar > 0)
                {
                    string secilenTarih = dtpIslemTarihi != null
                        ? dtpIslemTarihi.Value.ToString("dd.MM.yyyy HH:mm", _trKultur)
                        : DateTime.Now.ToString("dd.MM.yyyy HH:mm", _trKultur);

                    bool borcMu;
                    if (duzenlemeModu && duzenlenecekSatirIndex != -1)
                    {
                        DataGridViewRow mevcutSatir = dgvHareketler.Rows[duzenlenecekSatirIndex];
                        double eskiBorc = TutarParse(mevcutSatir.Cells[2].Value?.ToString());
                        borcMu = (eskiBorc > 0);
                    }
                    else
                    {
                        borcMu = lblIslemBaslik.Text.Contains("BORÇ");
                    }

                    string borc = borcMu ? tutar.ToString("N2", _trKultur) : "0,00";
                    string tahsilat = !borcMu ? tutar.ToString("N2", _trKultur) : "0,00";

                    if (duzenlemeModu && duzenlenecekSatirIndex != -1)
                    {
                        DataGridViewRow satir = dgvHareketler.Rows[duzenlenecekSatirIndex];
                        satir.Cells[0].Value = secilenTarih;
                        satir.Cells[1].Value = txtIslemAciklama.Text;
                        satir.Cells[2].Value = borc;
                        satir.Cells[3].Value = tahsilat;
                        satir.DefaultCellStyle.ForeColor = borc != "0,00" ? Color.Red : Color.Green;

                        duzenlemeModu = false;
                        duzenlenecekSatirIndex = -1;
                    }
                    else
                    {
                        int n = dgvHareketler.Rows.Add(secilenTarih, txtIslemAciklama.Text, borc, tahsilat);
                        dgvHareketler.Rows[n].DefaultCellStyle.ForeColor = borc != "0,00" ? Color.Red : Color.Green;
                    }

                    // Her işlem sonrası tarihe göre sırala ve kaydet
                    SatirlariTarihGoreSirala();
                    HareketleriDosyayaYaz();

                    pnlIslem.Visible = false;
                    txtIslemAciklama.Clear();
                    txtIslemTutar.Clear();
                    BakiyeHesapla();
                    e.SuppressKeyPress = true;
                }
                else
                {
                    MessageBox.Show("Lütfen geçerli bir tutar giriniz.",
                        "Geçersiz Tutar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void pnlIslem_Paint(object sender, PaintEventArgs e)
        {
            int kalinlik = 8;
            using (Pen kalem = new Pen(cerceveRengi, kalinlik))
            {
                int ofset = kalinlik / 2;
                e.Graphics.DrawRectangle(kalem, ofset, ofset, pnlIslem.Width - kalinlik, pnlIslem.Height - kalinlik);
            }
        }
    }
}