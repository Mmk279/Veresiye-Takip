#nullable disable
using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace MmkVeresiye
{
    public partial class LoginForm : Form
    {
        public bool GirisBasarili { get; private set; } = false;
        private string kullaniciDosya = Path.Combine(Application.StartupPath, "kullanicilar.mmk");

        private Label lblKullaniciKod;
        private TextBox txtKullaniciKod;
        private Label lblSifreKod;
        private TextBox txtSifreKod;
        private Button btnGirisKod;
        private PictureBox pbLogo;
        private bool personelModu = false;

        public LoginForm()
        {
            InitializeComponent();
            OrijinalArayuzHazirla();
        }

        private void OrijinalArayuzHazirla()
        {
            this.Controls.Clear();

            // 🎨 Orijinal Koyu Lacivert Tema
            Color formArkaPlanRengi = Color.FromArgb(23, 43, 81);
            this.Text = "Mmk Veresiye - Güvenli Giriş";
            this.BackColor = formArkaPlanRengi;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.KeyPreview = true;

            Font fontEtiket = new Font("Segoe UI", 11, FontStyle.Regular);
            Font fontKutu = new Font("Segoe UI", 12, FontStyle.Regular);
            Font fontButon = new Font("Segoe UI", 12, FontStyle.Regular);

            // 🖼️ Logo Alanı
            pbLogo = new PictureBox()
            {
                Location = new Point(45, 15),
                Size = new Size(270, 105),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.Transparent
            };

            bool logoYuklendi = false;
            Image hamLogo = null;

            // 1. Yol: Proje kaynaklarından (Resources) kontrol etme
            try
            {
                var resourceManager = new System.Resources.ResourceManager("MmkVeresiye.Properties.Resources", typeof(LoginForm).Assembly);
                var resourceSet = resourceManager.GetResourceSet(System.Globalization.CultureInfo.InvariantCulture, true, true);
                if (resourceSet != null)
                {
                    foreach (System.Collections.DictionaryEntry entry in resourceSet)
                    {
                        if (entry.Value is Image && (entry.Key.ToString().ToLower().Contains("logo") || entry.Key.ToString().ToLower().Contains("mmk")))
                        {
                            hamLogo = (Image)entry.Value;
                            break;
                        }
                    }
                }
            }
            catch { }

            // 2. Yol: Klasördeki logo dosyasından çekme
            if (hamLogo == null)
            {
                string klasordekiLogoPng = Path.Combine(Application.StartupPath, "logo.png");
                string klasordekiLogoJpg = Path.Combine(Application.StartupPath, "logo.jpg");

                if (File.Exists(klasordekiLogoPng)) hamLogo = Image.FromFile(klasordekiLogoPng);
                else if (File.Exists(klasordekiLogoJpg)) hamLogo = Image.FromFile(klasordekiLogoJpg);
            }

            // 🎯 STANDART VE HATASIZ RENK DÖNÜŞTÜRÜCÜ (Siyahları Lacivert Yapar)
            if (hamLogo != null)
            {
                try
                {
                    // Resmi belleğe alıp piksellerini manipüle edilebilir hale getiriyoruz
                    Bitmap bmp = new Bitmap(hamLogo);

                    for (int y = 0; y < bmp.Height; y++)
                    {
                        for (int x = 0; x < bmp.Width; x++)
                        {
                            Color piksel = bmp.GetPixel(x, y);

                            // Piksel siyah mı veya siyaha çok yakın bir koyulukta mı? (RGB değerleri düşükse siyaha yakındır)
                            if (piksel.R < 45 && piksel.G < 45 && piksel.B < 45)
                            {
                                // Koyu pikselleri formun kendi laciverti ile değiştiriyoruz
                                bmp.SetPixel(x, y, formArkaPlanRengi);
                            }
                        }
                    }

                    pbLogo.Image = bmp;
                    logoYuklendi = true;
                }
                catch
                {
                    // Bir aksilik olursa orijinal halini bas, programın çökmesini engelle
                    pbLogo.Image = hamLogo;
                    logoYuklendi = true;
                }
            }

            // Yedek Başlık (Logo tamamen yoksa devreye girer)
            if (!logoYuklendi)
            {
                Label lblYedekLogo = new Label()
                {
                    Text = "MMK VERESİYE",
                    Location = new Point(45, 40),
                    Size = new Size(270, 50),
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 22, FontStyle.Bold),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                this.Controls.Add(lblYedekLogo);
            }
            else
            {
                this.Controls.Add(pbLogo);
            }

            // 🎯 Personel Kontrolü ve Dinamik Form Boyutlandırması
            bool personelVarMi = false;
            if (File.Exists(kullaniciDosya))
            {
                string[] satirlar = File.ReadAllLines(kullaniciDosya);
                foreach (string s in satirlar)
                {
                    if (!string.IsNullOrWhiteSpace(s) && s.Contains("|"))
                    {
                        personelVarMi = true;
                        break;
                    }
                }
            }

            if (personelVarMi)
            {
                personelModu = true;
                this.Size = new Size(360, 360);

                lblKullaniciKod = new Label() { Text = "Lütfen Kullanıcı Adınızı Giriniz:", Location = new Point(40, 130), Size = new Size(280, 22), ForeColor = Color.White, Font = fontEtiket, TextAlign = ContentAlignment.MiddleLeft };
                txtKullaniciKod = new TextBox() { Location = new Point(40, 155), Size = new Size(265, 29), Font = fontKutu, BackColor = Color.White, ForeColor = Color.Black };

                lblSifreKod = new Label() { Text = "Lütfen Giriş Şifrenizi Giriniz:", Location = new Point(40, 195), Size = new Size(280, 22), ForeColor = Color.White, Font = fontEtiket, TextAlign = ContentAlignment.MiddleLeft };
                txtSifreKod = new TextBox() { Location = new Point(40, 220), Size = new Size(265, 29), Font = fontKutu, PasswordChar = '*', BackColor = Color.White, ForeColor = Color.Black };

                btnGirisKod = new Button() { Text = "GİRİŞ YAP", Location = new Point(40, 265), Size = new Size(265, 42), BackColor = formArkaPlanRengi, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = fontButon };
                btnGirisKod.FlatAppearance.BorderSize = 1;
                btnGirisKod.FlatAppearance.BorderColor = Color.White;

                this.Controls.AddRange(new Control[] { lblKullaniciKod, txtKullaniciKod, lblSifreKod, txtSifreKod, btnGirisKod });
                txtKullaniciKod.Focus();
            }
            else
            {
                personelModu = false;
                this.Size = new Size(360, 310);

                lblSifreKod = new Label() { Text = "Lütfen Giriş Şifrenizi Giriniz:", Location = new Point(40, 135), Size = new Size(280, 22), ForeColor = Color.White, Font = fontEtiket, TextAlign = ContentAlignment.MiddleLeft };
                txtSifreKod = new TextBox() { Location = new Point(40, 165), Size = new Size(265, 29), Font = fontKutu, PasswordChar = '*', BackColor = Color.White, ForeColor = Color.Black };

                btnGirisKod = new Button() { Text = "GİRİŞ YAP", Location = new Point(40, 210), Size = new Size(265, 42), BackColor = formArkaPlanRengi, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = fontButon };
                btnGirisKod.FlatAppearance.BorderSize = 1;
                btnGirisKod.FlatAppearance.BorderColor = Color.White;

                this.Controls.AddRange(new Control[] { lblSifreKod, txtSifreKod, btnGirisKod });
                txtSifreKod.Focus();
            }

            this.KeyDown += (s, e) => { if (e.KeyCode == Keys.Escape) Application.Exit(); };

            btnGirisKod.Click += YenilenenGirisKontrolu;
            txtSifreKod.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) YenilenenGirisKontrolu(null, null); };

            if (txtKullaniciKod != null)
            {
                txtKullaniciKod.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) txtSifreKod.Focus(); };
            }
        }

        private void YenilenenGirisKontrolu(object sender, EventArgs e)
        {
            string girilenSifre = txtSifreKod != null ? txtSifreKod.Text.Trim() : "";
            string girilenKullanici = txtKullaniciKod != null ? txtKullaniciKod.Text.Trim().ToLower() : "yönetici";

            if (string.IsNullOrEmpty(girilenSifre))
            {
                MessageBox.Show("Lütfen şifrenizi girin!", "Uyarı");
                return;
            }

            if (personelModu)
            {
                bool dogrulandi = false;
                if (File.Exists(kullaniciDosya))
                {
                    string[] satirlar = File.ReadAllLines(kullaniciDosya);
                    foreach (string satirdosya in satirlar)
                    {
                        if (string.IsNullOrWhiteSpace(satirdosya)) continue;

                        string[] parca = satirdosya.Split('|');
                        if (parca.Length == 2)
                        {
                            if (parca[0].Trim().ToLower() == girilenKullanici && parca[1].Trim() == girilenSifre)
                            {
                                dogrulandi = true;
                                AnaForm.AktifKullanici = parca[0].Trim();
                                break;
                            }
                        }
                    }
                }

                if (dogrulandi)
                {
                    GirisBasarili = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Kullanıcı adı veya şifre hatalı!", "Giriş Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                string dosyaYolu = Path.Combine(Application.StartupPath, "sifre.txt");
                string gercekSifre = "123456";

                if (File.Exists(dosyaYolu))
                    gercekSifre = File.ReadAllText(dosyaYolu).Trim();

                if (girilenSifre == gercekSifre)
                {
                    GirisBasarili = true;
                    AnaForm.AktifKullanici = "Yönetici";
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Hatalı Giriş Şifresi!", "Giriş Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnGiris_Click(object sender, EventArgs e) { }
        private void txtSifre_KeyDown(object sender, KeyEventArgs e) { }
    }
}