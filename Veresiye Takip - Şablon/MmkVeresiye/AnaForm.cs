#nullable disable
using System;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Text;
using System.IO.Compression;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;

namespace MmkVeresiye
{
    public partial class AnaForm : Form
    {
        private string cariListeDosyasi = "cariler.mmk";
        private Button btnF2DuzenleKodIle;
        private Button btnHizliFisYazdir;

        // 🎨 Yedekleme Panelinin İç Elemanları
        private Label lblYedekUyari;
        private GroupBox gbIndir;
        private Label lblIndirAciklama;
        private Button btnIndirZip;
        private GroupBox gbYukle;
        private Label lblYukleAciklama;
        private Button btnYukleMmk;

        public static string AktifKullanici = "Yönetici";

        public AnaForm()
        {
            InitializeComponent();
            dgvListe.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvListe.MultiSelect = true;
            this.KeyPreview = true;

            MenuElemanlariniOlustur();
            SifreSor();
        }

        private void MenuElemanlariniOlustur()
        {
            try
            {
                ToolStripMenuItem duzenleMenu = null;
                foreach (ToolStripItem item in menuStrip1.Items)
                {
                    if (item.Text.Contains("Düzenle") || item.Name.ToLower().Contains("duzenle"))
                    {
                        duzenleMenu = (ToolStripMenuItem)item;
                        break;
                    }
                }

                if (duzenleMenu == null)
                {
                    duzenleMenu = new ToolStripMenuItem("Düzenle");
                    menuStrip1.Items.Insert(1, duzenleMenu);
                }

                ToolStripMenuItem tsFirma = new ToolStripMenuItem("Firma Bilgilerini Düzenle");
                tsFirma.Click += (s, e) => { FirmaBilgisiDuzenle Pencere = new FirmaBilgisiDuzenle(); Pencere.ShowDialog(); };
                duzenleMenu.DropDownItems.Add(tsFirma);

                duzenleMenu.DropDownItems.Add(new ToolStripSeparator());

                ToolStripMenuItem tsKullanici = new ToolStripMenuItem("Personel / Kullanıcı Ayarları");
                tsKullanici.Click += (s, e) => { KullaniciYonetimForm Pencere = new KullaniciYonetimForm(); Pencere.ShowDialog(); };
                duzenleMenu.DropDownItems.Add(tsKullanici);
            }
            catch { }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F2) { CariKartiDuzenlePencerisiniAc(); return true; }
            if (keyData == Keys.F3) { GunlukHareketRaporu(); return true; }
            if (keyData == Keys.F4)
            {
                if (pnlYedek != null)
                {
                    pnlYedek.Visible = !pnlYedek.Visible;
                    if (pnlYedek.Visible) pnlYedek.BringToFront();
                }
                return true;
            }
            if (keyData == Keys.Escape) { if (pnlYedek != null) pnlYedek.Visible = false; return true; }

            if (keyData == Keys.Enter)
            {
                if (cmbHizliCari.Focused) { txtHizliAciklama.Focus(); return true; }
                else if (txtHizliAciklama.Focused) { txtHizliTutar.Focus(); return true; }
                else if (txtHizliTutar.Focused) { HızlıIslemYap(false); return true; }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void SifreSor()
        {
            using (LoginForm login = new LoginForm())
            {
                login.ShowDialog();
                if (!login.GirisBasarili) Environment.Exit(0);
            }
        }

        private void AnaForm_Load(object sender, EventArgs e)
        {
            this.Font = new Font("Segoe UI Black", 12, FontStyle.Bold);
            dgvListe.DefaultCellStyle.Font = new Font("Segoe UI Black", 12, FontStyle.Bold);
            dgvListe.ReadOnly = true;
            dgvListe.Columns[0].ValueType = typeof(int);

            // 🎯 REYİSTEN KAÇAN F2 DÜZENLE BUTONUNU YENİDEN OLUŞTURUP FORMA EKLEYELİM
            // 🎯 F2 DÜZENLE BUTONUNU CARİ SİL BUTONUNUN SOLUNA YERLEŞTİRİYORUZ
            // 🎯 F2 DÜZENLE BUTONUNU KOD BAĞIMSIZ SABİT KOORDİNATLA YERLEŞTİRİYORUZ
            if (btnF2DuzenleKodIle == null)
            {
                btnF2DuzenleKodIle = new Button()
                {
                    Text = "✏️ Düzenle (F2)",
                    Size = new Size(130, 40),

                    // 📍 Üstteki arama kutularının hizasında, sil butonunun hemen soluna sabitlendi
                    Location = new Point(650, 50),

                    BackColor = Color.FromArgb(52, 152, 219), // Şık Mavi
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI Black", 10, FontStyle.Bold), // Tasarımla uyumlu kalın font
                    Cursor = Cursors.Hand
                };
                btnF2DuzenleKodIle.FlatAppearance.BorderSize = 0;
                btnF2DuzenleKodIle.Click += (s, ev) => CariKartiDuzenlePencerisiniAc();

                this.Controls.Add(btnF2DuzenleKodIle);
                btnF2DuzenleKodIle.BringToFront();
            }

            // 🎯 Yedekleme Paneli Tasarımı
            if (pnlYedek != null)
            {
                pnlYedek.Visible = false;
                YedekPaneliniTasarla();
            }

            string firmaYol = Path.Combine(Application.StartupPath, "firma.txt");
            if (File.Exists(firmaYol))
            {
                string firmaAdi = File.ReadAllText(firmaYol, Encoding.UTF8);
                if (!string.IsNullOrWhiteSpace(firmaAdi))
                {
                    this.Text = $"MMK Veresiye Takip Programı - [{firmaAdi.Trim()}]";
                }
            }

            CarileriYukle();
            OtomatikKodVer();
            TekDosyayaYedekle();
            GenelBakiyeHesapla();
            HizliIslemFisSecenekleriEkle();
            HizliCariListesiGuncelle();
        }

        // 🧾 EKLENDİ: Hızlı İşlem panelinde "Bilgi Fişi Yazdır ve Kaydet" butonu
        private void HizliIslemFisSecenekleriEkle()
        {
            if (btnHizliFisYazdir == null && groupBox1 != null)
            {
                groupBox1.Height += 55;

                btnHizliFisYazdir = new Button()
                {
                    Text = "🧾 Fiş Yazdır ve Kaydet",
                    Location = new Point(28, 292),
                    Size = new Size(227, 40),
                    BackColor = Color.FromArgb(155, 89, 182),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 9, FontStyle.Bold),
                    Cursor = Cursors.Hand
                };
                btnHizliFisYazdir.FlatAppearance.BorderSize = 0;
                btnHizliFisYazdir.Click += (s, ev) => HızlıIslemYap(true);

                groupBox1.Controls.Add(btnHizliFisYazdir);
                btnHizliFisYazdir.BringToFront();
            }
        }

        // 🎨 GÖNDERDİĞİNİZ GÖRSELDEKİ BİREBİR TASARIM KALIBI
        private void YedekPaneliniTasarla()
        {
            pnlYedek.Controls.Clear();
            pnlYedek.BackColor = Color.FromArgb(15, 32, 67); // Şık ve koyu bir arka plan

            // 📐 Panelin boyutunu zorunlu olarak genişletiyoruz ki elemanlar sığsın
            pnlYedek.Width = 550;
            pnlYedek.Height = 420;

            // Paneli formun tam ortasına konumlandırmak istersen (isteğe bağlı):
            if (pnlYedek.Parent != null)
            {
                pnlYedek.Location = new Point(
                    (pnlYedek.Parent.Width - pnlYedek.Width) / 2,
                    (pnlYedek.Parent.Height - pnlYedek.Height) / 2
                );
            }

            Font fontGrupBaslik = new Font("Segoe UI", 11, FontStyle.Bold);
            Font fontAciklama = new Font("Segoe UI", 9f, FontStyle.Regular);
            Font fontButon = new Font("Segoe UI", 10, FontStyle.Bold);

            // ⚠️ Üst Sarı Uyarı Metni (Sığmama sorununu çözmek için genişlik ayarlandı)
            lblYedekUyari = new Label()
            {
                Text = "⚠️ Henüz bu oturumda yedek indirilmedi. Lütfen bilgisayarınıza yedek alın!",
                Location = new Point(15, 15),
                Size = new Size(pnlYedek.Width - 30, 30),
                ForeColor = Color.FromArgb(241, 196, 15),
                Font = fontAciklama,
                TextAlign = ContentAlignment.MiddleCenter
            };

            // 🔽 1. GRUP: BİLGİSAYARA İNDİR
            gbIndir = new GroupBox() { Text = "  💻 Bilgisayara İndir (ZIP/MMK)", Location = new Point(20, 55), Size = new Size(pnlYedek.Width - 40, 135), ForeColor = Color.FromArgb(52, 152, 219), Font = fontGrupBaslik };

            lblIndirAciklama = new Label()
            {
                Text = "Tüm cari listesini, hareket detaylarını, kullanıcı ve firma ayarlarını hem .mmk hem .zip olarak bilgisayarınıza paketler. Güvenliğiniz için bu işlemi düzenli yapınız.",
                Location = new Point(15, 25),
                Size = new Size(gbIndir.Width - 30, 45),
                ForeColor = Color.LightGray,
                Font = fontAciklama
            };

            btnIndirZip = new Button()
            {
                Text = "📦 BİLGİSAYARA İNDİR (YEDEK AL)",
                Location = new Point(15, 75),
                Size = new Size(gbIndir.Width - 30, 45),
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = fontButon,
                Cursor = Cursors.Hand
            };
            btnIndirZip.FlatAppearance.BorderSize = 0;
            btnIndirZip.Click += btnYedekle_Click;
            gbIndir.Controls.AddRange(new Control[] { lblIndirAciklama, btnIndirZip });

            // 🔼 2. GRUP: YEDEĞİ GERİ YÜKLE
            gbYukle = new GroupBox() { Text = "  🔄 Yedeği Sistemden Geri Yükle", Location = new Point(20, 205), Size = new Size(pnlYedek.Width - 40, 135), ForeColor = Color.FromArgb(231, 76, 60), Font = fontGrupBaslik };

            lblYukleAciklama = new Label()
            {
                Text = "Daha önce almış olduğunuz '.mmk' veya '.zip' uzantılı yedek dosyasını seçerek sisteme geri yüklersiniz.\n⚠️ Dikkat! Mevcut tüm güncel verileriniz silinerek yedekteki veriler yazılır.",
                Location = new Point(15, 25),
                Size = new Size(gbYukle.Width - 30, 45),
                ForeColor = Color.LightGray,
                Font = fontAciklama
            };

            btnYukleMmk = new Button()
            {
                Text = "📁 YEDEKTEN GERİ YÜKLE (.MMK / .ZIP SEÇ)",
                Location = new Point(15, 75),
                Size = new Size(gbYukle.Width - 30, 45),
                BackColor = Color.FromArgb(192, 41, 43),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = fontButon,
                Cursor = Cursors.Hand
            };
            btnYukleMmk.FlatAppearance.BorderSize = 0;
            btnYukleMmk.Click += btnGeriYukle_Click;
            gbYukle.Controls.AddRange(new Control[] { lblYukleAciklama, btnYukleMmk });

            // ❌ Paneli Kapat Butonu
            Button btnPanelKapat = new Button()
            {
                Text = "PANELİ GİZLE (ESC)",
                Location = new Point(pnlYedek.Width - 180, pnlYedek.Height - 50),
                Size = new Size(160, 35),
                BackColor = Color.FromArgb(44, 62, 80),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9f, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnPanelKapat.FlatAppearance.BorderSize = 0;
            btnPanelKapat.Click += (s, e) => pnlYedek.Visible = false;

            pnlYedek.Controls.AddRange(new Control[] { lblYedekUyari, gbIndir, gbYukle, btnPanelKapat });
        }

        private void CariKartiDuzenlePencerisiniAc()
        {
            if (dgvListe.CurrentRow == null || dgvListe.CurrentRow.IsNewRow)
            {
                MessageBox.Show("Lütfen düzenlemek için listeden bir müşteri seçin.", "Uyarı");
                return;
            }

            int seciliSira = dgvListe.CurrentRow.Index;
            string eskiKod = dgvListe.CurrentRow.Cells[0].Value?.ToString() ?? "";
            string eskiUnvan = dgvListe.CurrentRow.Cells[1].Value?.ToString() ?? "";

            string tel1 = "", tel2 = "", adres = "", notlar = "";
            string kartYolu = Path.Combine(Application.StartupPath, $"kart_{eskiKod}.mmk");

            if (File.Exists(kartYolu))
            {
                string[] veriler = File.ReadAllLines(kartYolu);
                if (veriler.Length >= 4) { tel1 = veriler[0]; tel2 = veriler[1]; adres = veriler[2]; notlar = veriler[3]; }
            }

            Form frm = new Form() { Width = 400, Height = 520, Text = "Müşteri Kartı Düzenle (F2)", StartPosition = FormStartPosition.CenterParent, FormBorderStyle = FormBorderStyle.FixedDialog, MaximizeBox = false, MinimizeBox = false, BackColor = Color.FromArgb(245, 246, 250) };

            frm.KeyPreview = true;
            frm.KeyDown += (s, e) => { if (e.KeyCode == Keys.Escape) frm.Close(); };

            Font fontGenel = new Font("Segoe UI", 10, FontStyle.Bold);

            Label lblK = new Label() { Text = "Cari Kod:", Location = new Point(20, 20), Size = new Size(100, 25), Font = fontGenel };
            TextBox txtK = new TextBox() { Text = eskiKod, Location = new Point(130, 18), Size = new Size(220, 25), Font = fontGenel };

            Label lblU = new Label() { Text = "Müşteri Ünvanı:", Location = new Point(20, 60), Size = new Size(110, 25), Font = fontGenel };
            TextBox txtU = new TextBox() { Text = eskiUnvan, Location = new Point(130, 58), Size = new Size(220, 25), Font = fontGenel };

            Label lblT1 = new Label() { Text = "Cep Telefonu:", Location = new Point(20, 100), Size = new Size(100, 25), Font = fontGenel };
            TextBox txtT1 = new TextBox() { Text = tel1, Location = new Point(130, 98), Size = new Size(220, 25), Font = fontGenel };

            Label lblT2 = new Label() { Text = "Sabit Telefon:", Location = new Point(20, 140), Size = new Size(100, 25), Font = fontGenel };
            TextBox txtT2 = new TextBox() { Text = tel2, Location = new Point(130, 138), Size = new Size(220, 25), Font = fontGenel };

            Label lblA = new Label() { Text = "Adres Bilgisi:", Location = new Point(20, 180), Size = new Size(100, 25), Font = fontGenel };
            TextBox txtA = new TextBox() { Text = adres, Location = new Point(130, 178), Size = new Size(220, 80), Multiline = true, Font = fontGenel };

            Label lblN = new Label() { Text = "Özel Notlar:", Location = new Point(20, 270), Size = new Size(100, 25), Font = fontGenel };
            TextBox txtN = new TextBox() { Text = notlar, Location = new Point(130, 268), Size = new Size(220, 60), Multiline = true, Font = fontGenel };

            Button btnKaydet = new Button() { Text = "DEĞİŞİKLİKLERİ KAYDET", Location = new Point(20, 360), Size = new Size(330, 45), BackColor = Color.FromArgb(46, 204, 113), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 11, FontStyle.Bold) };
            btnKaydet.FlatAppearance.BorderSize = 0;

            frm.Controls.AddRange(new Control[] { lblK, txtK, lblU, txtU, lblT1, txtT1, lblT2, txtT2, lblA, txtA, lblN, txtN, btnKaydet });

            btnKaydet.Click += (s, ev) =>
            {
                string yeniKod = txtK.Text.Trim();
                string yeniUnvan = txtU.Text.Trim();

                if (string.IsNullOrEmpty(yeniKod) || string.IsNullOrEmpty(yeniUnvan))
                {
                    MessageBox.Show("Cari Kod ve Ünvan alanları boş bırakılamaz!", "Hata");
                    return;
                }

                string yeniKartYolu = Path.Combine(Application.StartupPath, $"kart_{yeniKod}.mmk");
                File.WriteAllLines(yeniKartYolu, new string[] { txtT1.Text, txtT2.Text, txtA.Text, txtN.Text });

                if (eskiKod != yeniKod)
                {
                    string eskiDetay = Path.Combine(Application.StartupPath, $"detay_{eskiKod}.mmk");
                    string yeniDetay = Path.Combine(Application.StartupPath, $"detay_{yeniKod}.mmk");
                    if (File.Exists(eskiDetay)) { if (File.Exists(yeniDetay)) File.Delete(yeniDetay); File.Move(eskiDetay, yeniDetay); }
                    if (File.Exists(kartYolu) && eskiKod != yeniKod) File.Delete(kartYolu);
                }

                dgvListe.Rows[seciliSira].Cells[0].Value = int.Parse(yeniKod);
                dgvListe.Rows[seciliSira].Cells[1].Value = yeniUnvan;

                CarileriKaydet();
                CarileriYukle();
                HizliCariListesiGuncelle();

                MessageBox.Show("Müşteri kartı başarıyla güncellendi.", "Başarılı");
                frm.DialogResult = DialogResult.OK;
                frm.Close();
            };

            frm.ShowDialog();
        }

        // 🆕 GÜNCELLENDİ: Her cari için ekstre oluşturup, otomatik yedeği DosyalariPaketle ile alır
        private void TekDosyayaYedekle()
        {
            try
            {
                TumCarilerIcinEkstreOlustur();

                string klasörYolu = Path.Combine(Application.StartupPath, "OtomatikYedekler");
                if (!Directory.Exists(klasörYolu)) Directory.CreateDirectory(klasörYolu);
                string dosyaAdi = "Otomatik_Yedek_" + DateTime.Now.ToString("dd_MM_yyyy") + ".mmk";
                string tamYol = Path.Combine(klasörYolu, dosyaAdi);

                DosyalariPaketle(tamYol);
            }
            catch { }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            try { TekDosyayaYedekle(); } catch { }
            base.OnFormClosing(e);
        }

        // 📅 Hızlı İşlem ile eklenen kayıttan sonra ilgili carinin hareket dosyasını
        // tarihe göre (en yeni en üstte) yeniden sıralar.
        private void DetayDosyasiniTarihGoreSirala(string cariKod)
        {
            string yol = $"detay_{cariKod}.mmk";
            if (!File.Exists(yol)) return;

            var siraliSatirlar = File.ReadAllLines(yol, Encoding.UTF8)
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .Select(s => new { Satir = s, Parcalar = s.Split('|') })
                .Where(x => x.Parcalar.Length >= 4)
                .Select(x => new
                {
                    x.Satir,
                    Tarih = DateTime.TryParse(x.Parcalar[0], out DateTime dt) ? dt : DateTime.MinValue
                })
                .OrderByDescending(x => x.Tarih)
                .Select(x => x.Satir);

            File.WriteAllLines(yol, siraliSatirlar, Encoding.UTF8);
        }

        private void HızlıIslemYap(bool fisYazdirilsinMi = false)
        {
            if (cmbHizliCari.SelectedIndex == -1 && string.IsNullOrEmpty(cmbHizliCari.Text))
            { MessageBox.Show("Lütfen bir cari seçin!"); return; }

            if (string.IsNullOrWhiteSpace(txtHizliTutar.Text))
            { MessageBox.Show("Lütfen bir tutar girin!"); return; }

            if (double.TryParse(txtHizliTutar.Text.Replace(".", ","), out double tutar))
            {
                string seciliUnvan = cmbHizliCari.Text;
                string cariKod = "";
                int satirIndeks = -1;

                foreach (DataGridViewRow row in dgvListe.Rows)
                {
                    if (row.Cells[1].Value?.ToString() == seciliUnvan)
                    {
                        cariKod = row.Cells[0].Value.ToString();
                        satirIndeks = row.Index;
                        break;
                    }
                }

                if (string.IsNullOrEmpty(cariKod)) { MessageBox.Show("Cari bulunamadı!"); return; }

                string tarih = DateTime.Now.ToString("dd.MM.yyyy HH:mm");
                string aciklama = string.IsNullOrWhiteSpace(txtHizliAciklama.Text) ? "Hızlı İşlem" : txtHizliAciklama.Text;
                string borc = rbArtı.Checked ? tutar.ToString("N2") : "0,00";
                string tahsilat = rbEksi.Checked ? tutar.ToString("N2") : "0,00";
                string yeniSatir = $"{tarih}|{aciklama}|{borc}|{tahsilat}";

                try
                {
                    File.AppendAllLines($"detay_{cariKod}.mmk", new[] { yeniSatir });
                    DetayDosyasiniTarihGoreSirala(cariKod); // 📅 Dosyayı tarihe göre (en yeni en üstte) sıralar
                    BakiyeGuncelle(satirIndeks, cariKod);
                    GenelBakiyeHesapla();
                    CarileriKaydet();

                    // 🧾 EKLENDİ: "Fiş Yazdır ve Kaydet" butonuna basıldıysa, kayıttan sonra fişi yazdır
                    if (fisYazdirilsinMi)
                    {
                        BilgiFisiYardimci.Yazdir(seciliUnvan, tarih, aciklama, tutar.ToString("N2"));
                    }

                    txtHizliTutar.Clear();
                    txtHizliAciklama.Clear();
                    cmbHizliCari.Text = "";
                    cmbHizliCari.Focus();
                }
                catch (Exception ex) { MessageBox.Show("Hata: " + ex.Message); }
            }
        }

        private void HizliCariListesiGuncelle()
        {
            cmbHizliCari.Items.Clear();
            foreach (DataGridViewRow r in dgvListe.Rows)
                if (!r.IsNewRow && r.Cells[1].Value != null)
                    cmbHizliCari.Items.Add(r.Cells[1].Value.ToString());
        }

        private void GunlukHareketRaporu()
        {
            try
            {
                Form tarihForm = new Form() { Width = 320, Height = 350, Text = "Rapor Tarihi Seçin", StartPosition = FormStartPosition.CenterScreen, FormBorderStyle = FormBorderStyle.FixedDialog, MaximizeBox = false, MinimizeBox = false, BackColor = Color.White };
                MonthCalendar takvim = new MonthCalendar() { Location = new Point(25, 15), MaxSelectionCount = 1 };
                Button btnGetir = new Button() { Text = "RAPORU HAZIRLA", Size = new Size(227, 45), Location = new Point(40, 240), BackColor = Color.FromArgb(46, 204, 113), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
                btnGetir.FlatAppearance.BorderSize = 0;
                tarihForm.Controls.Add(takvim);
                tarihForm.Controls.Add(btnGetir);

                btnGetir.Click += (s, e) => { tarihForm.DialogResult = DialogResult.OK; tarihForm.Close(); };

                if (tarihForm.ShowDialog() == DialogResult.OK)
                {
                    string secilenTarih = takvim.SelectionStart.ToString("dd.MM.yyyy");
                    StringBuilder html = new StringBuilder();
                    html.Append("<html><head><meta charset='utf-8'><style>body{font-family:Arial; padding:20px;} table{width:100%; border-collapse:collapse; margin-top:20px;} th,td{border:1px solid #ddd; padding:12px; text-align:left;} th{background:#2c3e50; color:white;} .borc{color:red; font-weight:bold;} .tahsilat{color:green; font-weight:bold;} .toplam{background:#f9f9f9; font-weight:bold;}</style></head><body>");
                    html.Append($"<h2 style='color:#2c3e50;'>Günlük Hareket Raporu</h2><p><b>Sorgulanan Tarih:</b> {secilenTarih}</p><table><tr><th>Kod</th><th>Müşteri Ünvanı</th><th>Açıklama</th><th>Borç (+)</th><th>Tahsilat (-)</th></tr>");

                    double tB = 0, tT = 0; bool kayitVar = false;

                    foreach (DataGridViewRow r in dgvListe.Rows)
                    {
                        if (r.Cells[0].Value == null) continue;
                        string k = r.Cells[0].Value.ToString();
                        string u = r.Cells[1].Value.ToString();
                        string dosyaYolu = Path.Combine(Application.StartupPath, $"detay_{k}.mmk");

                        if (File.Exists(dosyaYolu))
                        {
                            foreach (string satir in File.ReadAllLines(dosyaYolu))
                            {
                                if (string.IsNullOrWhiteSpace(satir)) continue;
                                if (satir.Length >= 10 && satir.Substring(0, 10) == secilenTarih)
                                {
                                    string[] p = satir.Split('|');
                                    if (p.Length >= 4)
                                    {
                                        html.Append($"<tr><td>{k}</td><td>{u}</td><td>{p[1]}</td><td class='borc'>{p[2]}</td><td class='tahsilat'>{p[3]}</td></tr>");
                                        double b = 0, t = 0;
                                        double.TryParse(p[2].Replace(".", ","), out b);
                                        double.TryParse(p[3].Replace(".", ","), out t);
                                        tB += b; tT += t; kayitVar = true;
                                    }
                                }
                            }
                        }
                    }

                    if (!kayitVar) { MessageBox.Show($"{secilenTarih} tarihine ait bir hareket kaydı bulunamadı.", "Bilgi"); return; }
                    html.Append($"<tr class='toplam'><td colspan='3' align='right'>GÜNLÜK TOPLAM:</td><td class='borc'>{tB:N2}</td><td class='tahsilat'>{tT:N2}</td></tr></table></body></html>");
                    string raporDosya = Path.Combine(Application.StartupPath, "GunlukRapor.html");
                    File.WriteAllText(raporDosya, html.ToString(), Encoding.UTF8);
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(raporDosya) { UseShellExecute = true });
                }
            }
            catch (Exception ex) { MessageBox.Show("Rapor oluşturulurken bir hata oluştu: " + ex.Message, "Hata"); }
        }

        // 🆕 GÜNCELLENDİ: Hem .mmk hem .zip dosyası aynı anda oluşturuluyor
        private void btnYedekle_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog()
            {
                FileName = "Yedek_" + DateTime.Now.ToString("dd_MM_yyyy_HHmm"),
                Filter = "Yedek Dosyası (*.mmk)|*.mmk"
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                TumCarilerIcinEkstreOlustur();

                string klasor = Path.GetDirectoryName(sfd.FileName);
                string dosyaAdiUzantısız = Path.GetFileNameWithoutExtension(sfd.FileName);

                string mmkYolu = Path.Combine(klasor, dosyaAdiUzantısız + ".mmk");
                string zipYolu = Path.Combine(klasor, dosyaAdiUzantısız + ".zip");

                DosyalariPaketle(mmkYolu);
                DosyalariPaketle(zipYolu);

                if (lblYedekUyari != null)
                {
                    lblYedekUyari.Text = $"✅ Son Yedekleme Başarılı: {DateTime.Now.ToString("dd.MM.yyyy HH:mm")}";
                    lblYedekUyari.ForeColor = Color.LightGreen;
                }

                MessageBox.Show("Yedekleme başarıyla tamamlandı.\n\nHem .mmk hem .zip dosyası oluşturuldu:\n" + mmkYolu + "\n" + zipYolu, "İşlem Tamam");
            }
        }

        // 🆕 EKLENDİ: Yedek alırken her cari için otomatik ekstre (HTML) oluşturur
        private void TumCarilerIcinEkstreOlustur()
        {
            try
            {
                string klasor = Path.Combine(Application.StartupPath, "Ekstreler");
                if (!Directory.Exists(klasor)) Directory.CreateDirectory(klasor);

                foreach (DataGridViewRow r in dgvListe.Rows)
                {
                    if (r.IsNewRow || r.Cells[0].Value == null) continue;
                    string kod = r.Cells[0].Value.ToString();
                    string unvan = r.Cells[1].Value?.ToString() ?? kod;
                    string detayYolu = Path.Combine(Application.StartupPath, $"detay_{kod}.mmk");
                    if (!File.Exists(detayYolu)) continue;

                    StringBuilder html = new StringBuilder();
                    html.Append("<html><head><meta charset='utf-8'><style>table{width:100%; border-collapse:collapse;} th,td{border:1px solid black; padding:8px;} th{background:#eee;}</style></head><body>");
                    html.Append($"<h2>{unvan} - Müşteri Ekstresi</h2><table><tr><th>Tarih</th><th>Açıklama</th><th>Borç</th><th>Tahsilat</th></tr>");

                    foreach (string satir in File.ReadAllLines(detayYolu, Encoding.UTF8))
                    {
                        if (string.IsNullOrWhiteSpace(satir)) continue;
                        string[] p = satir.Split('|');
                        if (p.Length >= 4)
                            html.Append($"<tr><td>{p[0]}</td><td>{p[1]}</td><td>{p[2]}</td><td>{p[3]}</td></tr>");
                    }

                    html.Append("</table></body></html>");

                    string guvenliDosyaAdi = string.Join("_", unvan.Split(Path.GetInvalidFileNameChars()));
                    string dosyaYolu = Path.Combine(klasor, $"{guvenliDosyaAdi}_Ekstre.html");
                    File.WriteAllText(dosyaYolu, html.ToString(), Encoding.UTF8);
                }
            }
            catch { }
        }

        private void DosyalariPaketle(string hedefYol)
        {
            try
            {
                // Hedef ZIP zaten varsa ve kilitli değilse sil
                if (File.Exists(hedefYol))
                    File.Delete(hedefYol);

                string calismaDizini = Application.StartupPath;

                using (FileStream fs = new FileStream(hedefYol, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    using (ZipArchive zip = new ZipArchive(fs, ZipArchiveMode.Create))
                    {
                        // Tekil dosyaları tam yol (Application.StartupPath) belirterek ekliyoruz
                        ZipEkleGüvenli(zip, Path.Combine(calismaDizini, "cariler.mmk"), "cariler.mmk");
                        ZipEkleGüvenli(zip, Path.Combine(calismaDizini, "firma.txt"), "firma.txt");
                        ZipEkleGüvenli(zip, Path.Combine(calismaDizini, "kullanicilar.mmk"), "kullanicilar.mmk");

                        // detay_*.mmk dosyalarını ekle
                        foreach (string dosya in Directory.GetFiles(calismaDizini, "detay_*.mmk"))
                        {
                            ZipEkleGüvenli(zip, dosya, Path.GetFileName(dosya));
                        }

                        // kart_*.mmk dosyalarını ekle
                        foreach (string dosya in Directory.GetFiles(calismaDizini, "kart_*.mmk"))
                        {
                            ZipEkleGüvenli(zip, dosya, Path.GetFileName(dosya));
                        }

                        // Ekstreler (her cari için HTML) klasörünü de ekle
                        string ekstreKlasoru = Path.Combine(calismaDizini, "Ekstreler");
                        if (Directory.Exists(ekstreKlasoru))
                        {
                            foreach (string ekstreDosya in Directory.GetFiles(ekstreKlasoru, "*.html"))
                            {
                                ZipEkleGüvenli(zip, ekstreDosya, "Ekstreler/" + Path.GetFileName(ekstreDosya));
                            }
                            foreach (string pdfDosya in Directory.GetFiles(ekstreKlasoru, "*.pdf"))
                            {
                                ZipEkleGüvenli(zip, pdfDosya, "Ekstreler/" + Path.GetFileName(pdfDosya));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Paketleme sırasında bir hata oluştu:\n" + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 🛡️ Kilitli veya okunamayan dosyaların paketlemeyi tamamen durdurmasını engelleyen yardımcı metod
        private void ZipEkleGüvenli(ZipArchive zip, string kaynakDosyaYolu, string entryName)
        {
            if (File.Exists(kaynakDosyaYolu))
            {
                try
                {
                    // FileShare.ReadWrite ile dosya o an açık bile olsa kopyasını alarak zip'e ekler
                    using (FileStream sourceStream = new FileStream(kaynakDosyaYolu, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        ZipArchiveEntry entry = zip.CreateEntry(entryName);
                        using (Stream entryStream = entry.Open())
                        {
                            sourceStream.CopyTo(entryStream);
                        }
                    }
                }
                catch
                {
                    // Belirli bir dosya kilitliyse pas geçer, diğer dosyaları paketlemeye devam eder
                }
            }
        }

        private void btnGeriYukle_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Yedeği yüklediğinizde güncel tüm verileriniz SİLİNECEKTİR!\nDevam etmek istiyor musunuz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No) return;

            try
            {
                OpenFileDialog ofd = new OpenFileDialog() { Filter = "Yedek Dosyaları (*.mmk;*.zip)|*.mmk;*.zip|Tüm Dosyalar (*.*)|*.*", Title = "Lütfen yedek dosyasını seçin" };
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    foreach (string d in Directory.GetFiles(Application.StartupPath, "detay_*.mmk")) try { File.Delete(d); } catch { }
                    foreach (string d in Directory.GetFiles(Application.StartupPath, "kart_*.mmk")) try { File.Delete(d); } catch { }
                    ZipFile.ExtractToDirectory(ofd.FileName, Application.StartupPath, true);
                    MessageBox.Show("Yedek başarıyla yüklendi! Verilerin tazelenmesi için program yeniden başlatılıyor.", "İşlem Tamam");
                    Application.Restart();
                }
            }
            catch (Exception ex) { MessageBox.Show("Yedek yüklenirken bir sorun çıktı: " + ex.Message, "Hata"); }
        }

        private void OtomatikKodVer()
        {
            int enB = 0;
            foreach (DataGridViewRow r in dgvListe.Rows)
                if (!r.IsNewRow && r.Cells[0].Value != null)
                {
                    int.TryParse(r.Cells[0].Value.ToString(), out int k);
                    if (k > enB) enB = k;
                }
            txtCariKod.Text = (enB + 1).ToString();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog { Filter = "Excel CSV|*.csv" };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                StringBuilder sb = new StringBuilder(); sb.AppendLine("Kod;Unvan;Bakiye");
                foreach (DataGridViewRow r in dgvListe.Rows) if (!r.IsNewRow) sb.AppendLine($"{r.Cells[0].Value};{r.Cells[1].Value};{r.Cells[2].Value}");
                File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (dgvListe.SelectedRows.Count > 0 && MessageBox.Show("Silinsin mi?", "Onay", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                foreach (DataGridViewRow r in dgvListe.SelectedRows)
                {
                    if (!r.IsNewRow)
                    {
                        string k = r.Cells[0].Value.ToString();
                        if (File.Exists($"detay_{k}.mmk")) File.Delete($"detay_{k}.mmk");
                        if (File.Exists($"kart_{k}.mmk")) File.Delete($"kart_{k}.mmk");
                        dgvListe.Rows.Remove(r);
                    }
                }
                CarileriKaydet(); OtomatikKodVer(); GenelBakiyeHesapla(); HizliCariListesiGuncelle();
            }
        }

        private void txtCariUnvan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !string.IsNullOrWhiteSpace(txtCariUnvan.Text))
            {
                dgvListe.Rows.Add(int.Parse(txtCariKod.Text), txtCariUnvan.Text, "0,00");
                txtCariUnvan.Clear(); CarileriKaydet(); OtomatikKodVer(); txtCariUnvan.Focus(); GenelBakiyeHesapla();
                HizliCariListesiGuncelle();
            }
        }

        private void CarileriKaydet()
        {
            StringBuilder sb = new StringBuilder();
            foreach (DataGridViewRow r in dgvListe.Rows)
                if (!r.IsNewRow) sb.AppendLine($"{r.Cells[0].Value}|{r.Cells[1].Value}|{r.Cells[2].Value}");
            File.WriteAllText(cariListeDosyasi, sb.ToString());
        }

        private void CarileriYukle()
        {
            if (File.Exists(cariListeDosyasi))
            {
                dgvListe.Rows.Clear();
                foreach (string s in File.ReadAllLines(cariListeDosyasi))
                {
                    string[] p = s.Split('|');
                    if (p.Length == 3)
                    {
                        int cariKod = int.Parse(p[0]);
                        int n = dgvListe.Rows.Add(cariKod, p[1], p[2]);
                        double bakiye = 0; double.TryParse(p[2].Replace(".", ","), out bakiye);

                        if (bakiye < 0) dgvListe.Rows[n].DefaultCellStyle.ForeColor = Color.Blue;
                        else if (bakiye >= 0 && bakiye < 1000) dgvListe.Rows[n].DefaultCellStyle.ForeColor = Color.Black;
                        else if (bakiye >= 1000) dgvListe.Rows[n].DefaultCellStyle.ForeColor = Color.Red;
                    }
                }
                dgvListe.Sort(dgvListe.Columns[0], System.ComponentModel.ListSortDirection.Ascending);
            }
        }

        private void BakiyeGuncelle(int idx, string k)
        {
            if (File.Exists($"detay_{k}.mmk"))
            {
                double t = 0;
                foreach (string s in File.ReadAllLines($"detay_{k}.mmk"))
                {
                    string[] p = s.Split('|');
                    t += (double.Parse(p[2]) - double.Parse(p[3]));
                }
                dgvListe.Rows[idx].Cells[2].Value = t.ToString("N2");

                if (t < 0) dgvListe.Rows[idx].DefaultCellStyle.ForeColor = Color.Blue;
                else if (t >= 0 && t < 1000) dgvListe.Rows[idx].DefaultCellStyle.ForeColor = Color.Black;
                else if (t >= 1000) dgvListe.Rows[idx].DefaultCellStyle.ForeColor = Color.Red;
            }
        }

        private void GenelBakiyeHesapla()
        {
            double t = 0;
            foreach (DataGridViewRow r in dgvListe.Rows) if (!r.IsNewRow) t += double.Parse(r.Cells[2].Value.ToString());
            lblGenelBakiye.Text = t.ToString("N2");
        }

        private void txtAra_TextChanged(object sender, EventArgs e)
        {
            string a = txtAra.Text.ToLower(); dgvListe.CurrentCell = null;
            foreach (DataGridViewRow r in dgvListe.Rows) if (!r.IsNewRow) r.Visible = r.Cells[1].Value.ToString().ToLower().Contains(a);
        }

        private void txtCariKod_KeyDown(object sender, KeyEventArgs e) { if (e.KeyCode == Keys.Enter) txtCariUnvan.Focus(); }

        private void CariyiAc(int satirIndeks)
        {
            if (satirIndeks >= 0 && dgvListe.Rows[satirIndeks].Cells[0].Value != null)
            {
                CariDetayForm detayForm = new CariDetayForm();
                detayForm.CariKod = dgvListe.Rows[satirIndeks].Cells[0].Value?.ToString() ?? "";
                detayForm.CariUnvan = dgvListe.Rows[satirIndeks].Cells[1].Value?.ToString() ?? "";
                detayForm.ShowDialog();
                BakiyeGuncelle(satirIndeks, detayForm.CariKod);
                GenelBakiyeHesapla();
                CarileriKaydet();
            }
        }

        private void dgvListe_CellDoubleClick(object sender, DataGridViewCellEventArgs e) { CariyiAc(e.RowIndex); }

        private void txtAra_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down && dgvListe.Rows.Count > 0)
            {
                dgvListe.Focus();
                foreach (DataGridViewRow satir in dgvListe.Rows) if (satir.Visible) { dgvListe.CurrentCell = satir.Cells[0]; break; }
            }
            else if (e.KeyCode == Keys.Enter)
            {
                foreach (DataGridViewRow satir in dgvListe.Rows) if (satir.Visible && !satir.IsNewRow) { CariyiAc(satir.Index); e.SuppressKeyPress = true; break; }
            }
        }

        private void dgvListe_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && dgvListe.CurrentRow != null) { CariyiAc(dgvListe.CurrentRow.Index); e.SuppressKeyPress = true; }
        }

        private void yedekleF4ToolStripMenuItem_Click(object sender, EventArgs e) { if (pnlYedek != null) { pnlYedek.Visible = true; pnlYedek.BringToFront(); } }

        private void sifreDegistirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string yeniSifre = Microsoft.VisualBasic.Interaction.InputBox("Lütfen yeni ana şifreyi belirleyin:", "Şifre", "");
            if (!string.IsNullOrWhiteSpace(yeniSifre)) { File.WriteAllText(Path.Combine(Application.StartupPath, "sifre.txt"), yeniSifre); MessageBox.Show("Şifreniz başarıyla güncellendi!"); }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) { Application.Exit(); }
        private void gunlukRaporToolStripMenuItem_Click(object sender, EventArgs e) { GunlukHareketRaporu(); }
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string mesaj = "MMK Veresiye Kısayolları:\n\nF2: Müşteri Kartı Düzenle\nF3: Günlük Rapor\nF4: Yedekleme Paneli\nESC: Kapat / Panel Gizle\nENTER: Hızlı İşlem Kaydet\n\n";
            MessageBox.Show(mesaj, "Hakkında & Kısayollar", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void AnaForm_KeyDown(object sender, KeyEventArgs e) { }
        private void txtCariUnvan_TextChanged(object sender, EventArgs e) { }
        private void timerSaat_Tick(object sender, EventArgs e) { lblCanliSaat.Text = DateTime.Now.ToString("dd.MM.yyyy | HH:mm:ss"); }
        private void label4_Click(object sender, EventArgs e) { }
    }
}