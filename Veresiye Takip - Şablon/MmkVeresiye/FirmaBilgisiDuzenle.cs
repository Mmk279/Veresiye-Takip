using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Text;

namespace MmkVeresiye
{
    public class FirmaBilgisiDuzenle : Form
    {
        public FirmaBilgisiDuzenle()
        {
            this.Width = 400;
            this.Height = 180;
            this.Text = "Firma Bilgisi Tanımlama";
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.KeyPreview = true;
            this.KeyDown += (s, e) => { if (e.KeyCode == Keys.Escape) this.Close(); };

            Font f = new Font("Segoe UI", 10, FontStyle.Bold);

            Label lbl = new Label() { Text = "Firma / İşletme Adı:", Location = new Point(20, 25), Size = new Size(150, 25), Font = f };
            TextBox txt = new TextBox() { Location = new Point(160, 23), Size = new Size(200, 25), Font = f };

            string yol = Path.Combine(Application.StartupPath, "firma.txt");
            if (File.Exists(yol)) txt.Text = File.ReadAllText(yol, Encoding.UTF8);

            Button btn = new Button() { Text = "BAŞLIĞI KAYDET", Location = new Point(20, 70), Size = new Size(340, 40), BackColor = Color.FromArgb(52, 152, 219), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = f };
            btn.FlatAppearance.BorderSize = 0;

            btn.Click += (s, e) =>
            {
                File.WriteAllText(yol, txt.Text.Trim(), Encoding.UTF8);
                MessageBox.Show("Firma adı kaydedildi! Program bir sonraki açılışta veya yenilendiğinde güncellenecektir.", "Başarılı");
                this.Close();
            };

            this.Controls.AddRange(new Control[] { lbl, txt, btn });
        }
    }
}