using Kutuphane.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kutuphane.ConsoleUI.Islemler
{
    public class RaporIslemleri
    {
        public void RaporIslemleriMetot()
        {
            Console.Clear();
            YardimciMetotlar yardimciMetot = new YardimciMetotlar();
            var db = new KutuphaneContext();
            Console.WriteLine("1. En çok ödünç alınan kitaplar (Top 10)");
            Console.WriteLine("2. En aktif üyeler (en çok kitap alan)");
            Console.WriteLine("3. Çıkış Yap");
            Console.Write("Yapmak istediğiniz işlem nedir ==>");
            if (!int.TryParse(Console.ReadLine().Trim(), out int secim))
            {
                Console.WriteLine("Lütfen Tam Sayı Değeri Giriniz!");
                yardimciMetot.Bekle();
                return;
            }

            if (secim == 1)
            {
                Console.Clear();
                Console.WriteLine("En Çok Ödünç Alınan Kitaplar (Top 10)");
                Console.WriteLine("-------------------------------------------------------");

                var oduncKitaplar = db.OduncIslemler
                    .Include(x => x.Kitap)
                    .GroupBy(x => x.KitapId)
                    .Select(grup => new
                    {
                        KitapId = grup.Key,
                        Kitap = grup.First().Kitap,
                        OduncSayisi = grup.Count()
                    })
                    .OrderByDescending(x => x.OduncSayisi)
                    .Take(10)
                    .ToList();

                int sira = 1;
                foreach (var x in oduncKitaplar)
                {
                    Console.WriteLine($"{sira}. {x.Kitap.Baslik}");
                    Console.WriteLine($"   Yazar: {x.Kitap.Yazar}");
                    Console.WriteLine($"   Ödünç Sayısı: {x.OduncSayisi} kez");
                    Console.WriteLine();
                    sira++;
                }
            }
            else if (secim == 2)
            {
                Console.Clear();
                Console.WriteLine("En aktif üyeler (en çok kitap alan)");
                Console.WriteLine("-------------------------------------------------------");

                var aktifUyeler = db.OduncIslemler
                    .Include(x => x.Uye)
                    .GroupBy(x => x.UyeId)
                    .Select(grup => new
                    {
                        KitapId = grup.Key,
                        Uye = grup.First().Uye,
                        OduncSayisi = grup.Count()
                    })
                    .OrderByDescending(x => x.OduncSayisi)
                    .Take(10)
                    .ToList();

                int sayac = 1;
                foreach (var x in aktifUyeler)
                {
                    Console.WriteLine($"{sayac}. {x.Uye.Ad} {x.Uye.Soyad}");
                    Console.WriteLine($"Toplamda {x.OduncSayisi} Kadar Ödünç İşlem Yapmış!");
                    Console.WriteLine("---------------------------------------------------------------");
                    sayac++;
                }
            }
            else if(secim == 3)
            {
                Console.Clear();
                return;
            }

            yardimciMetot.Bekle();
        }
    }
}
