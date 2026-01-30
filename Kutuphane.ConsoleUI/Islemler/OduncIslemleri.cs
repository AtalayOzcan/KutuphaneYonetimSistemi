using Kutuphane.DataAccess;
using Kutuphane.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kutuphane.ConsoleUI.Islemler
{
    public class OduncIslemleri
    {
        public void OduncIslemleriMetot()
        {
            YardimciMetotlar yardimciMetot = new YardimciMetotlar();
            Console.Clear();
            var db = new KutuphaneContext();
            Console.WriteLine("=== ÖDÜNÇ İŞLEMLERİ SİSTEMİ ===");
            Console.WriteLine("1. Kitap Ödünç Verme");
            Console.WriteLine("2. Kitap İade İşlemi");
            Console.WriteLine("3. Geciken Kitapları Görüntüleme (14 günden fazla iade edilmemiş");
            Console.WriteLine("4. Kişiye Ait Ödünç Geçmişini Görüntüleme");
            Console.WriteLine("5. Ödünç Süresi Uzat");
            Console.Write("Yapmak istediğiniz işlem nedir ==>");
            if (!int.TryParse(Console.ReadLine().Trim(), out int secim))
            {
                Console.WriteLine("Lütfen Sayısal bir değer Giriniz!");
                yardimciMetot.Bekle();
                return;
            }

            if (secim == 1)
            {
                Console.Clear();
                Console.WriteLine("=== KİTAP ÖDÜNÇ ALMA SİSTEMİ ===");
                Console.Write("Üye TC Kimlik Numarası:");
                string tcNo = Console.ReadLine().Trim();
                if (string.IsNullOrEmpty(tcNo))
                {
                    Console.WriteLine("Lütfen TC Kimlik Numarasını Boş Bırakmayınız!");
                    yardimciMetot.Bekle();
                    return;
                }

                var uyeVarMi = db.Uyeler
                    .Include(u => u.OduncIslemler)
                    .ThenInclude(o => o.Kitap)
                    .FirstOrDefault(x => x.TcNo == tcNo);
                if (uyeVarMi == null)
                {
                    Console.WriteLine("Bu TC'ye kayıtlı üye bulunamadı!");
                    yardimciMetot.Bekle();
                    return;
                }

                var aktifOduncler = uyeVarMi.OduncIslemler.Where(x => !x.IadeEdildiMi).ToList();// İade Edilmeyenlerin Listelemesi bool değer ! = false

                var gecikmeVarMi = aktifOduncler.Where(x => DateTime.Now > x.SonIadeTarihi).ToList();

                //GECİKME KONTROLÜ KISMI
                if (gecikmeVarMi.Count > 0)
                {
                    Console.WriteLine("Bu Üyenin Geciken Kitabı Var! Geciken Kitabı İade Ettikten Sonra Alabilir!");
                    Console.WriteLine("\nGeciken Kitaplar: ");
                    foreach (var x in aktifOduncler)
                    {
                        var gecikmeZamani = (DateTime.Now - x.SonIadeTarihi).Days;//ilk başta x.AlısTarihiydi Fakat Geciktiği günü alıyorum sadece
                        Console.WriteLine($" {x.Kitap.Baslik} Adlı Kitap {gecikmeZamani} Gündür Gecikme");
                    }
                    yardimciMetot.Bekle();
                    return;
                }

                //MAKSİMUM AYNI ANDA 3 KİTAP ALABİLİR KONTROLÜ
                if (aktifOduncler.Count >= 3)
                {
                    Console.WriteLine($"Üyede Zaten {aktifOduncler.Count} Adet Kitap Var!");
                    Console.WriteLine("Maksimum Aynı Anda 3 Kitap Alınabilir! İade Edilmeli!");
                    Console.WriteLine("Hali Hazırda Aldığı Kitaplar: ");
                    foreach (var x in aktifOduncler)
                    {
                        Console.WriteLine($"Kitap Adı: {x.Kitap.Baslik}");
                    }
                    yardimciMetot.Bekle();
                    return;
                }

                Console.Write("Hangi Kitapı Ödünç Almak İstersiniz(Kitap Adı veya ISBN): ");
                string kitapSorgu = Console.ReadLine().Trim();
                int.TryParse(kitapSorgu, out int isbnNumara); // ISBN int tuttuğum için Parse etmek zorunda kaldım!

                if (string.IsNullOrEmpty(kitapSorgu))
                {
                    Console.WriteLine("Kitap Adını Boş Bırakmayın!");
                    yardimciMetot.Bekle();
                    return;
                }

                var kitap = db.Kitaplar.FirstOrDefault(x => x.Baslik.Contains(kitapSorgu) || x.ISBN == isbnNumara);

                if (kitap == null)
                {
                    Console.WriteLine("Sistemimizde Böyle Bir Kitap Yok!");
                    yardimciMetot.Bekle();
                    return;
                }

                if (kitap.Stok <= 0)
                {
                    Console.WriteLine($"'{kitap.Baslik}' Kitabının Stoğu Yok!");
                    yardimciMetot.Bekle();
                    return;
                }

                Console.WriteLine("\n=== ÖDÜNÇ BİLGİLERİ ===");
                Console.WriteLine($"Üye: {uyeVarMi.Ad} {uyeVarMi.Soyad} - ({uyeVarMi.TcNo})");
                Console.WriteLine($"Kitap: {kitap.Baslik}");
                Console.WriteLine($"Yazar: {kitap.Yazar}");
                Console.WriteLine($"Alma Tarihi: {DateTime.Now:dd.MM.yyyy}");
                Console.WriteLine($"Son İade Tarihi: {DateTime.Now.AddDays(14):dd.MM.yyyy}");
                Console.Write("\nOnaylıyor musunuz? (E/H): ");

                if (Console.ReadLine().Trim().ToUpper() != "E")
                {
                    Console.WriteLine("İşlem iptal edildi.");
                    yardimciMetot.Bekle();
                    return;
                }
                var yeniOdunc = new OduncIslem
                {
                    KitapId = kitap.Id,
                    UyeId = uyeVarMi.Id,
                    AlmaTarihi = DateTime.UtcNow,
                    SonIadeTarihi = DateTime.UtcNow.AddDays(14),
                    IadeTarihi = null,
                    IadeEdildiMi = false,
                    GecikmeGunSayisi = 0
                };

                kitap.Stok = kitap.Stok - 1;

                db.OduncIslemler.Add(yeniOdunc);
                db.SaveChanges();

                Console.WriteLine("Kitap Başarıyla Ödünç Verildi!");
                Console.WriteLine($"Kalan Stok: {kitap.Stok}");
                yardimciMetot.Bekle();
            }
            else if (secim == 2)
            {
                Console.Clear();
                Console.WriteLine("=== KİTAP İADE SİSTEMİ ===");
                Console.Write("Üye TC Kimlik Numarası: ");
                string tcNo = Console.ReadLine().Trim();

                if (string.IsNullOrEmpty(tcNo))
                {
                    Console.WriteLine("TC Kimlik numarası boş olamaz!");
                    yardimciMetot.Bekle();
                    return;
                }

                var uyeVarMi = db.Uyeler
                    .Include(x => x.OduncIslemler)
                    .ThenInclude(x => x.Kitap)
                    .FirstOrDefault(x => x.TcNo == tcNo);

                if (uyeVarMi == null)
                {
                    Console.WriteLine($"{tcNo} TC'li üye bulunamadı!");
                    yardimciMetot.Bekle();
                    return;
                }

                var aktifOduncler = uyeVarMi.OduncIslemler.Where(x => !x.IadeEdildiMi).ToList();

                if (aktifOduncler.Count == 0)
                {
                    Console.WriteLine("Bu üyenin iade edilecek kitabı yok!");
                    yardimciMetot.Bekle();
                    return;
                }

                Console.WriteLine($"\n{uyeVarMi.Ad} {uyeVarMi.Soyad} adlı üyenin iade edilmemiş kitapları:");
                Console.WriteLine("================================================");

                int sayac = 1;
                foreach (var x in aktifOduncler)
                {
                    var kalanGun = (x.SonIadeTarihi - DateTime.UtcNow).Days;

                    if (kalanGun < 0)
                    {
                        Console.WriteLine($"{x.Kitap.Baslik} Adlı Kitabın {Math.Abs(kalanGun)} Gündür Gecikmiş!");
                    }

                    Console.WriteLine($"{sayac}. {x.Kitap.Baslik}");
                    Console.WriteLine($"   Yazar: {x.Kitap.Yazar}");
                    Console.WriteLine($"   ISBN: {x.Kitap.ISBN}");
                    Console.WriteLine($"   Alış: {x.AlmaTarihi:dd.MM.yyyy}");
                    Console.WriteLine();
                    sayac++;
                }

                Console.Write("İade edilecek kitabın adı veya ISBN: ");
                string kitapSorgu = Console.ReadLine().Trim();

                if (string.IsNullOrEmpty(kitapSorgu))
                {
                    Console.WriteLine("Kitap adı veya ISBN boş olamaz!");
                    yardimciMetot.Bekle();
                    return;
                }

                int.TryParse(kitapSorgu, out int isbnNo);

                var iadeEdilecekOdunc = aktifOduncler.FirstOrDefault(o => o.Kitap.Baslik.ToLower().Contains(kitapSorgu.ToLower()) || o.Kitap.ISBN == isbnNo);

                if (iadeEdilecekOdunc == null)
                {
                    Console.WriteLine($"Bu üyede '{kitapSorgu}' adlı/ISBN'li ödünç kitap bulunamadı!");
                    yardimciMetot.Bekle();
                    return;
                }

                var gecikmeGunSayisi = 0;
                if (DateTime.Now > iadeEdilecekOdunc.SonIadeTarihi)
                {
                    gecikmeGunSayisi = (DateTime.Now - iadeEdilecekOdunc.SonIadeTarihi).Days;
                }

                Console.WriteLine("\n=== İADE BİLGİLERİ ===");
                Console.WriteLine($"Üye: {uyeVarMi.Ad} {uyeVarMi.Soyad} ({uyeVarMi.TcNo})");
                Console.WriteLine($"Kitap: {iadeEdilecekOdunc.Kitap.Baslik}");
                Console.WriteLine($"Yazar: {iadeEdilecekOdunc.Kitap.Yazar}");
                Console.WriteLine($"Alış Tarihi: {iadeEdilecekOdunc.AlmaTarihi:dd.MM.yyyy}");
                Console.WriteLine($"Son İade Tarihi: {iadeEdilecekOdunc.SonIadeTarihi:dd.MM.yyyy}");
                Console.WriteLine($"İade Tarihi: {DateTime.Now:dd.MM.yyyy}");

                if (gecikmeGunSayisi > 0)
                {
                    Console.WriteLine($"GECİKME: {gecikmeGunSayisi} gün");
                }
                else
                {
                    Console.WriteLine("Zamanında iade");
                }

                Console.Write("\nİadeyi onaylıyor musunuz? (E/H): ");

                if (Console.ReadLine().Trim().ToUpper() != "E")
                {
                    Console.WriteLine("İşlem iptal edildi.");
                    yardimciMetot.Bekle();
                    return;
                }

                try
                {
                    iadeEdilecekOdunc.IadeTarihi = DateTime.UtcNow;
                    iadeEdilecekOdunc.IadeEdildiMi = true;
                    iadeEdilecekOdunc.GecikmeGunSayisi = gecikmeGunSayisi;

                    iadeEdilecekOdunc.Kitap.Stok++;

                    db.SaveChanges();

                    Console.WriteLine("\nKitap başarıyla iade edildi!");
                    Console.WriteLine($"Mevcut Stok: {iadeEdilecekOdunc.Kitap.Stok}");

                    if (gecikmeGunSayisi > 0)
                    {
                        Console.WriteLine($"{gecikmeGunSayisi} gün gecikme kaydedildi.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Hata: {ex.Message}");
                }

                yardimciMetot.Bekle();
            }
            else if (secim == 3)
            {
                Console.Clear();
                Console.WriteLine("=== GECİKEN KİTAPLARI LİSTELEME SİSTEMİ ===");
                Console.Write("Üye TC Kimlik Numarası: ");
                string tcNo = Console.ReadLine().Trim();

                if (string.IsNullOrEmpty(tcNo))
                {
                    Console.WriteLine("Üye TC Kimlik Numarasını Boş Bırakmayınız!");
                    yardimciMetot.Bekle();
                    return;
                }

                var uyeVarMi = db.Uyeler
                    .Include(x => x.OduncIslemler)
                    .ThenInclude(x => x.Kitap)
                    .FirstOrDefault(x => x.TcNo == tcNo);

                if (uyeVarMi == null)
                {
                    Console.WriteLine($"Sistemimizde {tcNo} TC Kimlik Numaralı Üye Kaydı Yoktur!");
                    yardimciMetot.Bekle();
                    return;
                }

                var aktifOduncler = uyeVarMi.OduncIslemler
                    .Where(x => !x.IadeEdildiMi)
                    .ToList();

                var gecikenKitap = aktifOduncler
                    .Where(x => DateTime.UtcNow > x.SonIadeTarihi)
                    .ToList();

                if (gecikenKitap.Count == 0)
                {
                    Console.WriteLine($"{uyeVarMi.TcNo} TC Kimlik Numaralı Kişiye Ait Geciken Kitap Yoktur!");
                    yardimciMetot.Bekle();
                    return;
                }
                else
                {
                    Console.WriteLine($"\n{uyeVarMi.Ad} {uyeVarMi.Soyad} Adlı Üyenin Gecikmiş kitapları:");
                    Console.WriteLine("================================================");

                    var sayac = 0;
                    foreach (var x in gecikenKitap)
                    {
                        sayac++;
                        var kalanGun = (x.SonIadeTarihi - DateTime.UtcNow).Days;

                        if (kalanGun < 0)
                        {
                            Console.WriteLine($"{x.Kitap.Baslik} Adlı Kitabın {Math.Abs(kalanGun)} Gündür Gecikmiş!");
                        }
                    }
                }

                yardimciMetot.Bekle();
            }
            else if (secim == 4)
            {
                Console.Clear();
                Console.WriteLine("=== BİR ÜYENİN ALDIĞI KİTAPLARI GÖRÜNTÜLEME SİSTEMİ ===");
                Console.Write("TC Kimlik Numarası: ");
                string tcNo = Console.ReadLine().Trim();

                if (string.IsNullOrEmpty(tcNo))
                {
                    Console.WriteLine("Üye TC Kimlik Numarasını Boş Bırakmayınız!");
                    yardimciMetot.Bekle();
                    return;
                }

                var uyeVarMi = db.Uyeler
                    .Include(x => x.OduncIslemler)
                    .ThenInclude(x => x.Kitap)
                    .FirstOrDefault(x => x.TcNo == tcNo);

                if (uyeVarMi == null)
                {
                    Console.WriteLine($"Sistemimizde {tcNo} TC Kimlik Numaralı Üye Kaydı Yoktur!");
                    yardimciMetot.Bekle();
                    return;
                }

                var gecmisIslemler = uyeVarMi.OduncIslemler.Where(x => x.IadeEdildiMi).ToList();

                Console.Clear();
                Console.WriteLine($"\n{uyeVarMi.Ad} {uyeVarMi.Soyad} Adlı Üyenin Ödünç Geçmişi:");
                Console.WriteLine("================================================");

                if (gecmisIslemler.Count > 0)
                {
                    int sayac = 1;
                    foreach (var x in gecmisIslemler)
                    {
                        Console.WriteLine($"{sayac}. {x.Kitap.Baslik} ({x.Kitap.Yazar}) \n     İade Tarihi: {x.IadeTarihi?.ToString("dd.MM.yyyy")}");
                        Console.WriteLine("------------------------------------------------------");
                        sayac++;
                    }
                }
                else
                {
                    Console.WriteLine($"{uyeVarMi.TcNo} TC Kimlik Numaralı Kişiye Ait Ödünç İşlem Yoktur!");
                    yardimciMetot.Bekle();
                    return;
                }

                yardimciMetot.Bekle();
            }
            else if (secim == 5)
            {
                Console.Clear();
                Console.WriteLine("=== ÖDÜNÇ SÜRESİ UZATMA SİSTEMİ ===");
                Console.Write("TC Kimlik Numarası: ");
                string tcNo = Console.ReadLine().Trim();

                if (string.IsNullOrEmpty(tcNo))
                {
                    Console.WriteLine("Üye TC Kimlik Numarasını Boş Bırakmayınız!");
                    yardimciMetot.Bekle();
                    return;
                }

                var uyeVarMi = db.Uyeler
                    .Include(x => x.OduncIslemler)
                    .ThenInclude(x => x.Kitap)
                    .FirstOrDefault(x => x.TcNo == tcNo);

                if (uyeVarMi == null)
                {
                    Console.WriteLine($"Sistemimizde {tcNo} TC Kimlik Numaralı Üye Kaydı Yoktur!");
                    yardimciMetot.Bekle();
                    return;
                }

                var aktifOduncler = uyeVarMi.OduncIslemler.Where(x => !x.IadeEdildiMi).ToList();

                if (aktifOduncler.Count == 0)
                {
                    Console.WriteLine($"{uyeVarMi.TcNo} TC Kimlik Numaralı Kişiye Ait Ödünç İşlem Yoktur!");
                    yardimciMetot.Bekle();
                    return;
                }

                Console.Clear();
                Console.WriteLine($"\n{uyeVarMi.Ad} {uyeVarMi.Soyad} adlı üyenin iade edilmemiş kitapları:");
                Console.WriteLine("================================================");

                int sayac = 1;
                foreach (var x in aktifOduncler)
                {
                    var kalanGun = (x.SonIadeTarihi - DateTime.UtcNow).Days;
                    if (kalanGun < 0)
                    {
                        Console.WriteLine($"{x.Kitap.Baslik} Adlı Kitabın {Math.Abs(kalanGun)} Gündür Gecikmiş!");
                    }
                    Console.WriteLine($"{sayac}. {x.Kitap.Baslik}");
                    Console.WriteLine($"   Yazar: {x.Kitap.Yazar}");
                    Console.WriteLine($"   ISBN: {x.Kitap.ISBN}");
                    Console.WriteLine($"   Alış: {x.AlmaTarihi:dd.MM.yyyy}");
                    Console.WriteLine();
                    sayac++;
                }

                Console.Write("Ödünç Süresi Uzatılacak Kitabın ISBN Numarası: ");
                if (!int.TryParse(Console.ReadLine().Trim(), out int isbnNo))
                {
                    Console.WriteLine("Lütfen ISBN Numarasını Tam Sayı Değeri Olarak Giriniz!");
                    yardimciMetot.Bekle();
                    return;
                }

                var isbnKontrol = aktifOduncler.FirstOrDefault(x => x.Kitap.ISBN == isbnNo);

                if (isbnKontrol == null)
                {
                    Console.WriteLine($"{isbnNo} ISBN Numaralı Kitap sistemimizde Yoktur!");
                    yardimciMetot.Bekle();
                    return;
                }

                int oduncEkle = 0;
                while (true)
                {
                    Console.Write("Ödünç Süresini Kaç Gün Uzatmak İstiyorsunuz: ");
                    if (!int.TryParse(Console.ReadLine().Trim(), out oduncEkle))
                    {
                        Console.WriteLine("Lütfen Ödünç Gün Değerini Tam Sayı Değeri Olarak Giriniz!");
                        continue;
                    }

                    if (oduncEkle < 0)
                    {
                        Console.WriteLine("Gün Sayısı Pozitif Olmalıdır!");
                        continue;
                    }

                    if (oduncEkle > 14)
                    {
                        Console.WriteLine("Gün Uzatma Süresi Maksimum 14 Gün Olabilir!");
                        continue;
                    }

                    break;
                }

                var eskiTarih = isbnKontrol.SonIadeTarihi;

                isbnKontrol.SonIadeTarihi = isbnKontrol.SonIadeTarihi.AddDays(oduncEkle);

                db.SaveChanges();

                Console.WriteLine("\n Ödünç süresi başarıyla uzatıldı!");
                Console.WriteLine($"Kitap: {isbnKontrol.Kitap.Baslik}");
                Console.WriteLine($"Eski Son İade: {eskiTarih:dd.MM.yyyy}");
                Console.WriteLine($"Yeni Son İade: {isbnKontrol.SonIadeTarihi:dd.MM.yyyy}");
                Console.WriteLine($"Uzatılan Süre: {oduncEkle} gün");
                yardimciMetot.Bekle();
            }
        }
    }
}
