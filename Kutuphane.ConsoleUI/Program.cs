using Kutuphane.DataAccess;
using Kutuphane.Entities;
using Microsoft.EntityFrameworkCore;
using Kutuphane.ConsoleUI.Islemler;

namespace Kutuphane.ConsoleUI
{
    public class Program
    {
        static void Main(string[] args)
        {
            YardimciMetotlar yardimciMetot = new YardimciMetotlar();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== KÜTÜPHANE YÖNETİM SİSTEMİ ===");
                Console.WriteLine("1. Kitap İşlemleri");
                Console.WriteLine("2. Üye İşlemleri");
                Console.WriteLine("3. Ödünç İşlemleri");
                Console.WriteLine("4. Raporlar");
                Console.WriteLine("5. Çıkış");
                Console.Write("Yapmak istediğiniz işlem nedir ==>");
                if (!int.TryParse(Console.ReadLine().Trim(), out int secim))
                {
                    Console.WriteLine("Hata: Seçim İşlemi Belirtilen (1-5) Gibi Sayı değeri Olmalıdır!");
                    yardimciMetot.Bekle();
                    continue;
                }

                if (secim < 1 || secim > 5)
                {
                    Console.WriteLine("Lütfen 1 ila 5 Arasında Tuşlama Yapınız!");
                    yardimciMetot.Bekle();
                    continue;
                }

                if (secim == 5)
                {
                    Console.WriteLine("Sistemden çıkılıyor...");
                    break;
                }
                using (var db = new KutuphaneContext())
                {
                    switch (secim)
                    {
                        case 1:
                            KitapIslemleri kitapIslem = new KitapIslemleri();
                            kitapIslem.KitapIslemleriMetot();
                            break;
                        case 2:
                            UyeIslemleri uyeIslem = new UyeIslemleri();
                            uyeIslem.UyeIslemleriMetot();
                            break;
                        case 3:
                            OduncIslemleri oduncIslem = new OduncIslemleri();
                            oduncIslem.OduncIslemleriMetot();
                            break;
                        case 4:
                            RaporIslemleri raporIslem = new RaporIslemleri();
                            raporIslem.RaporIslemleriMetot();
                            break;
                    }
                }
            }
        }
    }
}


