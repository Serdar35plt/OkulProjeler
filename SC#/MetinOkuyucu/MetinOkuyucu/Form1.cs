using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace MetinOkuyucu
{
    public partial class Form1 : Form
    {
        // Masaüstü dosya yolu
        private readonly string dosyaYolu = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            "metin.txt"
        );

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                string dosyaYolu = Path.Combine(Application.StartupPath, "metin.txt");
                string metin = textBox1.Text;

                if (string.IsNullOrWhiteSpace(metin))
                {
                    MessageBox.Show("Lütfen bir metin girin.", "Uyarı",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Önce var olan dosyayı sil (varsa)
                if (File.Exists(dosyaYolu))
                {
                    File.Delete(dosyaYolu);
                }

                // Yeni dosyayı oluştur ve yaz
                File.WriteAllText(dosyaYolu, metin);

                // Kullanıcıya bilgi ver
                MessageBox.Show("✅ Yeni metin başarıyla kaydedildi!\n\nKonum:\n" + dosyaYolu,
                    "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Dosyayı Notepad ile bir kere aç (açma hatası olursa sessizce yakala)
                try
                {
                    System.Diagnostics.Process.Start("notepad.exe", "\"" + dosyaYolu + "\"");
                }
                catch
                {
                    // İstersen burada kullanıcıya açma hatasını gösterebiliriz; şu an sessizce geçiyoruz.
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kaydetme sırasında hata oluştu:\n" + ex.Message,
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                string dosyaYolu = Path.Combine(Application.StartupPath, "metin.txt");

                if (File.Exists(dosyaYolu))
                {
                    string icerik = File.ReadAllText(dosyaYolu);
                    label1.Text = "Okunan metin: " + icerik;

                    MessageBox.Show("📖 Dosya başarıyla okundu!",
                        "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("❌ Dosya bulunamadı!\n\nLütfen önce kaydedin.",
                        "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Okuma hatası:\n" + ex.Message,
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
    }
}
