using System;
using System.Management; // NuGet'ten yüklediğimiz kütüphane
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MmkVeresiye
{
    public partial class LisansForm : Form
    {
        public LisansForm()
        {
            InitializeComponent();
        }
        private string CihazIDAl()
        {
            string cpuInfo = "Bilinmiyor"; // Başlangıç değeri vererek null riskini bitir
            try
            {
                using (ManagementClass mc = new ManagementClass("win32_processor"))
                {
                    using (ManagementObjectCollection moc = mc.GetInstances())
                    {
                        foreach (ManagementObject mo in moc)
                        {
                            // Değer null ise boş string döndür diyerek hatayı kesiyoruz
                            cpuInfo = mo.Properties["ProcessorId"]?.Value?.ToString() ?? "Bilinmiyor";
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cihaz ID alınamadı: " + ex.Message);
            }
            return cpuInfo;
        }
        private void LisansForm_Load(object sender, EventArgs e)
        {
            txtCihazID.Text = CihazIDAl(); // Tasarımdaki isimlendirmene göre yazdım
        }
        private void btnAktifEt_Click(object sender, EventArgs e)
        {
            // Senin Cihaz ID'n + Gizli Kelimen
            string beklenenAnahtar = txtCihazID.Text + "AMASYA05";

            if (txtLisansAnahtari.Text == beklenenAnahtar)
            {
                // Lisans dosyasını oluştur (Bir dahaki sefere sormasın diye)
                File.WriteAllText("license.dat", txtLisansAnahtari.Text);

                MessageBox.Show("Lisans Başarıyla Aktif Edildi!", "MMK Veresiye");

                this.DialogResult = DialogResult.OK; // İŞTE KRİTİK SATIR BURASI!
                this.Close();
            }
            else
            {
                MessageBox.Show("Hatalı Anahtar!");
            }
        }
    }
}
