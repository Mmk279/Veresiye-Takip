using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace MmkVeresiye
{
    public static class BilgiFisiYardimci
    {
        // Tek hareket için bilgi fişi (Hızlı İşlem'de kullanılıyor)
        public static void Yazdir(string cariUnvan, string tarih, string aciklama, string tutar)
        {
            YazdirCoklu(cariUnvan, new List<(string Tarih, string Aciklama, string Tutar)>
            {
                (tarih, aciklama, tutar)
            });
        }

        // 🆕 EKLENDİ: Birden fazla seçili hareketi tek fişte toplayıp yazdırır (Cari Detay'da kullanılıyor)
        public static void YazdirCoklu(string cariUnvan, List<(string Tarih, string Aciklama, string Tutar)> hareketler)
        {
            try
            {
                if (hareketler == null || hareketler.Count == 0) return;

                string firmaAdi = "Firma";
                string firmaYol = Path.Combine(Application.StartupPath, "firma.txt");
                if (File.Exists(firmaYol))
                {
                    string okunan = File.ReadAllText(firmaYol, Encoding.UTF8).Trim();
                    if (!string.IsNullOrWhiteSpace(okunan)) firmaAdi = okunan;
                }

                string klasor = Path.Combine(Application.StartupPath, "BilgiFisleri");
                if (!Directory.Exists(klasor)) Directory.CreateDirectory(klasor);

                string guvenliCari = string.Join("_", (cariUnvan ?? "Cari").Split(Path.GetInvalidFileNameChars()));
                string dosyaAdi = Path.Combine(klasor, $"Fis_{guvenliCari}_{DateTime.Now.ToString("dd_MM_yyyy_HHmmss")}.html");

                double toplam = 0;
                foreach (var h in hareketler)
                {
                    double.TryParse((h.Tutar ?? "0").Replace(".", "").Replace(",", "."),
                        System.Globalization.NumberStyles.Any,
                        System.Globalization.CultureInfo.InvariantCulture,
                        out double t);
                    toplam += t;
                }

                StringBuilder html = new StringBuilder();
                html.Append("<html><head><meta charset='utf-8'><style>");
                html.Append("body{font-family:Arial; padding:25px; width:360px;}");
                html.Append("h2{text-align:center; margin-bottom:5px;}");
                html.Append(".firma{text-align:center; font-size:14px; color:#555; margin-bottom:20px;}");
                html.Append(".cari{text-align:center; font-size:15px; font-weight:bold; margin-bottom:15px;}");
                html.Append("table{width:100%; border-collapse:collapse;}");
                html.Append("th,td{padding:6px 4px; border-bottom:1px dashed #999; font-size:13px; text-align:left;}");
                html.Append("th{border-bottom:1px solid #333;}");
                html.Append(".tutar-hucre{text-align:right;}");
                html.Append(".toplam{font-size:20px; font-weight:bold; text-align:center; margin-top:18px; border-top:2px solid #333; padding-top:10px;}");
                html.Append("</style></head><body onload='window.print()'>");
                html.Append("<h2>BİLGİ FİŞİ</h2>");
                html.Append($"<div class='firma'>{firmaAdi}</div>");
                html.Append($"<div class='cari'>{cariUnvan}</div>");

                html.Append("<table><tr><th>Tarih</th><th>Açıklama</th><th class='tutar-hucre'>Tutar</th></tr>");
                foreach (var h in hareketler)
                {
                    html.Append($"<tr><td>{h.Tarih}</td><td>{h.Aciklama}</td><td class='tutar-hucre'>{h.Tutar} TL</td></tr>");
                }
                html.Append("</table>");

                if (hareketler.Count > 1)
                {
                    html.Append($"<div class='toplam'>TOPLAM: {toplam.ToString("N2")} TL</div>");
                }
                else
                {
                    html.Append($"<div class='toplam'>{hareketler[0].Tutar} TL</div>");
                }

                html.Append("</body></html>");

                File.WriteAllText(dosyaAdi, html.ToString(), Encoding.UTF8);
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(dosyaAdi) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bilgi fişi oluşturulurken hata oluştu: " + ex.Message, "Hata");
            }
        }
    }
}