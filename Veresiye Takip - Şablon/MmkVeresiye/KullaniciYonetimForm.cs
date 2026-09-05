using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

namespace MmkVeresiye
{
    public class KullaniciYonetimForm : Form
    {
        private ListBox lstKullanicilar;
        private string dosyaYol = Path.Combine(Application.StartupPath, "kullanicilar.mmk");

        public KullaniciYonetimForm()
        {
            this.Width = 450;
            this.Height = 350;
            this.Text = "Personel & Kullanıcı Yönetim Paneli";
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.KeyPreview = true;
            this.KeyDown += (s, e) => { if (e.KeyCode == Keys.Escape) this.Close(); };

            Font f = new Font("Segoe UI", 10, FontStyle.Bold);

            Label lblListe = new Label() { Text = "Kayıtlı Personeller:", Location = new Point(20, 15), Size = new Size(150, 20), Font = f };
            lstKullanicilar = new ListBox() { Location = new Point(20, 40), Size = new Size(180, 200), Font = f };

            Label lblKullanici = new Label() { Text = "Kullanıcı Adı:", Location = new Point(220, 40), Size = new Size(180, 20), Font = f };
            TextBox txtKullanici = new TextBox() { Location = new Point(220, 65), Size = new Size(180, 25), Font = f };

            Label lblSifre = new Label() { Text = "Giriş Şifresi:", Location = new Point(220, 105), Size = new Size(180, 20), Font = f };
            TextBox txtSifre = new TextBox() { Location = new Point(220, 130), Size = new Size(180, 25), Font = f };

            Button btnEkle = new Button() { Text = "Yeni Personel Ekle", Location = new Point(220, 175), Size = new Size(180, 35), BackColor = Color.FromArgb(46, 204, 113), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = f };
            btnEkle.FlatAppearance.BorderSize = 0;

            Button btnSil = new Button() { Text = "Seçili Personeli Sil", Location = new Point(20, 250), Size = new Size(180, 35), BackColor = Color.FromArgb(231, 76, 60), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = f };
            btnSil.FlatAppearance.BorderSize = 0;

            this.Controls.AddRange(new Control[] { lblListe, lstKullanicilar, lblKullanici, txtKullanici, lblSifre, txtSifre, btnEkle, btnSil });

            ListeyiYenile();

            btnEkle.Click += (s, e) =>
            {
                string kAdi = txtKullanici.Text.Trim();
                string kSifre = txtSifre.Text.Trim();

                if (string.IsNullOrEmpty(kAdi) || string.IsNullOrEmpty(kSifre))
                {
                    MessageBox.Show("Lütfen tüm alanları doldurun!", "Uyarı");
                    return;
                }

                if (kAdi.Contains("|"))
                {
                    MessageBox.Show("Kullanıcı adında '|' karakteri kullanılamaz!", "Hata");
                    return;
                }

                File.AppendAllLines(dosyaYol, new string[] { $"{kAdi}|{kSifre}" });
                txtKullanici.Clear();
                txtSifre.Clear();
                ListeyiYenile();
                MessageBox.Show("Personel başarıyla sisteme tanımlandı. Bir sonraki girişte kullanıcı adı istenecektir.", "Başarılı");
            };

            btnSil.Click += (s, e) =>
            {
                if (lstKullanicilar.SelectedIndex == -1)
                {
                    MessageBox.Show("Lütfen silmek istediğiniz personeli listeden seçin.");
                    return;
                }

                string secilen = lstKullanicilar.SelectedItem.ToString();
                var satirlar = new List<string>(File.ReadAllLines(dosyaYol));

                satirlar.RemoveAll(x => x.StartsWith(secilen + "|"));
                File.WriteAllLines(dosyaYol, satirlar.ToArray());

                ListeyiYenile();
                MessageBox.Show("Kullanıcı silindi.", "Başarılı");
            };
        }

        private void ListeyiYenile()
        {
            lstKullanicilar.Items.Clear();
            if (File.Exists(dosyaYol))
            {
                foreach (string satir in File.ReadAllLines(dosyaYol))
                {
                    string[] p = satir.Split('|');
                    if (p.Length > 0 && !string.IsNullOrWhiteSpace(p[0]))
                    {
                        lstKullanicilar.Items.Add(p[0]);
                    }
                }
            }
        }
    }
}