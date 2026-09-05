using System;
using System.IO;
using System.Windows.Forms;

namespace MmkVeresiye
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            if (Environment.OSVersion.Version.Major >= 6) SetProcessDPIAware();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(true);

            // 1. Önce lisans dosyası var mı bak
            if (File.Exists("license.dat"))
            {
                // Dosya varsa direkt ana ekranı aç
                // ÖNEMLİ: Senin ana ekranının adı 'Form1' ise burayı Form1() yap!
                Application.Run(new AnaForm());
            }
            else
            {
                // Dosya yoksa Lisans ekranını aç
                using (LisansForm lform = new LisansForm())
                {
                    if (lform.ShowDialog() == DialogResult.OK)
                    {
                        // Lisans formunda 'Aktif Et'e basıldıysa ana ekranı aç
                        Application.Run(new AnaForm());
                    }
                    else
                    {
                        Application.Exit();
                    }
                }
            }
        }
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();
    }
}