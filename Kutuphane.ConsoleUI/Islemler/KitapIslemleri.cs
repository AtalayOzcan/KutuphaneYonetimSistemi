using Kutuphane.DataAccess;
using Kutuphane.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kutuphane.ConsoleUI.Islemler
{
    public class KitapIslemleri
    {
        public void KitapIslemleriMetot()
        {
            YardimciMetotlar yardimciMetot = new YardimciMetotlar();
            Console.Clear();
            var db = new KutuphaneContext();
            Console.WriteLine("=== KİTAP İŞLEMLERİ SİSTEMİ ===");
            Console.WriteLine("1. Kitap Ekleme");
            Console.WriteLine("2. Kitap listeleme");
            Console.WriteLine("3. Kitap Arama");
            Console.WriteLine("4. Kitap Güncelleme ve silme");
            Console.WriteLine("5. Kitap Stok Durumu Görüntüleme");
            Console.Write("Yapmak istediğiniz işlem nedir ==>");
            if (!int.TryParse(Console.ReadLine().Trim(), out int secim2))
            {
                Console.WriteLine("Lütfen Sayısal Değer Girin!");
                yardimciMetot.Bekle();
                return;
            }

            if (secim2 == 1)
            {
                Console.Clear();
                Console.WriteLine("=== KİTAP EKLEME SİSTEMİ ===");

                try
                {
                    //ISBN Kontrolü 
                    Console.Write("ISBN NO: ");
                    if (!int.TryParse(Console.ReadLine().Trim(), out int isbn))
                    {
                        Console.WriteLine("Hata : ISBN Numarası Sayı Olmalıdır!");
                        yardimciMetot.Bekle();
                        return;
                    }

                    var isbnVarMi = db.Kitaplar.Any(x => x.ISBN == isbn);
                    if (isbnVarMi)
                    {
                        Console.WriteLine($"Hata: {isbn} ISBN Numaralı kitap sistemde zaten halihazırda vardır!");
                        yardimciMetot.Bekle();
                        return;
                    }

                    Console.Write("Başlık: ");
                    string baslik = Console.ReadLine().Trim();
                    if (string.IsNullOrEmpty(baslik))
                    {
                        Console.WriteLine("Hata Başlık Boş bırakılamaz!");
                        yardimciMetot.Bekle();
                        return;
                    }

                    Console.Write("Yazar: ");
                    string yazar = Console.ReadLine().Trim();
                    if (string.IsNullOrEmpty(yazar))
                    {
                        Console.WriteLine("Hata Yazar Adı Boş bırakılamaz!");
                        yardimciMetot.Bekle();
                        return;
                    }

                    Console.Write("Yayın Evi: ");
                    string yayinEvi = Console.ReadLine().Trim();
                    if (string.IsNullOrEmpty(yayinEvi))
                    {
                        Console.WriteLine("Hata Yayın Evi Boş bırakılamaz!");
                        yardimciMetot.Bekle();
                        return;
                    }

                    Console.Write("Kategori: ");
                    string kategori = Console.ReadLine().Trim();
                    if (string.IsNullOrEmpty(kategori))
                    {
                        Console.WriteLine("Hata Kategori Boş bırakılamaz!");
                        yardimciMetot.Bekle();
                        return;
                    }

                    Console.Write("Sayfa Sayısı: ");
                    if (!int.TryParse(Console.ReadLine(), out int sayfaSayisi) || sayfaSayisi <= 0)
                    {
                        Console.WriteLine("Hata Sayfa Sayısı Sıfırdan(0) Fazla Olmalıdır!");
                        yardimciMetot.Bekle();
                        return;
                    }

                    Console.Write("Yayın Yılı: ");
                    if (!int.TryParse(Console.ReadLine().Trim(), out int yayinYili) || yayinYili <= 1 || yayinYili > DateTime.Now.Year)
                    {
                        Console.WriteLine($"Hata: Yayın Yılı 1 ile {DateTime.Now.Year} arasında olmak zorunda!");
                        yardimciMetot.Bekle();
                        return;
                    }

                    Console.Write("Stok Adeti: ");
                    if (!int.TryParse(Console.ReadLine().Trim(), out int stokAdet) || stokAdet < 0)
                    {
                        Console.WriteLine("Hata: Stok Adeti Pozitif Değer Olmalı!");
                        yardimciMetot.Bekle();
                        return;
                    }

                    var yeniKitap = new Kitap
                    {
                        ISBN = isbn,
                        Baslik = baslik,
                        Yazar = yazar,
                        YayinEvi = yayinEvi,
                        Kategori = kategori,
                        SayfaSayisi = sayfaSayisi,
                        YayinYili = yayinYili,
                        Stok = stokAdet
                    };
                    db.Kitaplar.Add(yeniKitap);
                    db.SaveChanges();

                    Console.WriteLine("Kitap Başarıyla Kaydedildi!");
                    Console.WriteLine("\nEklenen Kitap:");
                    Console.WriteLine($"ISBN: {isbn} | {baslik} - {yazar}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Hata Mesajı {ex.Message}");
                }

                yardimciMetot.Bekle();
            }
            else if (secim2 == 2)
            {
                Console.Clear();
                Console.WriteLine("=== KİTAP LİSTELEME SİSTEMİ ===");
                Console.WriteLine("Nasıl Arama Yapmak İstiyorsunuz: ");
                Console.WriteLine("1. Yazara Göre");
                Console.WriteLine("2. Kategoriye Göre");
                Console.WriteLine("3. Tüm Kitaplar");
                Console.Write("Yapmak istediğiniz işlem nedir ==>");
                if (!int.TryParse(Console.ReadLine().Trim(), out int islem))
                {
                    Console.WriteLine("Hata: Seçim İşlemi Belirtilen (1-5) Gibi Sayı değeri Olmalıdır!");
                    yardimciMetot.Bekle();
                    return;
                }

                if (islem == 1)
                {
                    Console.Clear();
                    Console.Write("Hangi Yazarı Arıyorsunuz : ");
                    string yazarAd = Console.ReadLine().Trim();
                    if (string.IsNullOrEmpty(yazarAd))
                    {
                        Console.WriteLine("Yazar Adı Boş bırakmayın");
                        yardimciMetot.Bekle();
                        return;
                    }

                    var listelenenKitap = db.Kitaplar.Where(x => x.Yazar.ToLower() == yazarAd.ToLower()).ToList();

                    if (listelenenKitap.Any())
                    {
                        Console.WriteLine($"\n'{yazarAd}' için {listelenenKitap.Count} kitap bulundu:\n");
                        foreach (var x in listelenenKitap)
                        {
                            Console.WriteLine($"---------------");
                            Console.WriteLine($"ISBN: {x.ISBN}");
                            Console.WriteLine($"Başlık: {x.Baslik}");
                            Console.WriteLine($"Yazar: {x.Yazar}");
                            Console.WriteLine($"Kategori: {x.Kategori}");
                            Console.WriteLine($"Stok: {x.Stok}");
                            Console.WriteLine($"---------------\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"\nSistemimizde '{yazarAd}' isimli yazara ait kitap bulunamadı!");
                    }
                }
                else if (islem == 2)
                {
                    Console.Clear();
                    Console.Write("Hangi Kategoride Arama Yapmak İstiyorsunuz: ");
                    string kategori = Console.ReadLine().Trim();
                    if (string.IsNullOrEmpty(kategori))
                    {
                        Console.WriteLine("Hata: Kategoriyi Boş Bıraktınız!");
                        yardimciMetot.Bekle();
                        return;
                    }

                    var kategoriListelenen = db.Kitaplar.Where(x => x.Kategori.ToLower() == kategori.ToLower()).ToList();

                    if (kategoriListelenen.Any())
                    {
                        Console.WriteLine($"\n'{kategori}' kategorisinde {kategoriListelenen.Count} kitap bulundu:\n");
                        foreach (var x in kategoriListelenen)
                        {
                            Console.WriteLine($"---------------");
                            Console.WriteLine($"ISBN: {x.ISBN}");
                            Console.WriteLine($"Başlık: {x.Baslik}");
                            Console.WriteLine($"Yazar: {x.Yazar}");
                            Console.WriteLine($"Kategori: {x.Kategori}");
                            Console.WriteLine($"Stok: {x.Stok}");
                            Console.WriteLine($"---------------\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"'{kategori}' kategorisinde kitap bulunamadı!");
                    }
                }
                else if (islem == 3)
                {
                    Console.Clear();
                    var tumKitaplarListelenen = db.Kitaplar.ToList();

                    if (tumKitaplarListelenen.Any())
                    {
                        Console.WriteLine($"\nToplam {tumKitaplarListelenen.Count} kitap listeleniyor:\n");
                        foreach (var x in tumKitaplarListelenen)
                        {
                            Console.WriteLine($"---------------");
                            Console.WriteLine($"ISBN: {x.ISBN}");
                            Console.WriteLine($"Başlık: {x.Baslik}");
                            Console.WriteLine($"Yazar: {x.Yazar}");
                            Console.WriteLine($"Kategori: {x.Kategori}");
                            Console.WriteLine($"Stok: {x.Stok}");
                            Console.WriteLine($"---------------\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Sistemde hiç kitap kayıtlı değil!");
                    }
                }
                yardimciMetot.Bekle();
            }
            else if (secim2 == 3)
            {
                Console.Clear();
                Console.WriteLine("=== KİTAP ARAMA SİSTEMİ ===");
                Console.WriteLine("Nasıl Arama Yapmak İstiyorsunuz: ");
                Console.WriteLine("1. İsme Göre");
                Console.WriteLine("2. Yazara Göre");
                Console.WriteLine("3. ISBN NO İLE");
                Console.Write("Yapmak istediğiniz işlem nedir ==>");
                if (!int.TryParse(Console.ReadLine().Trim(), out int islem2))
                {
                    Console.WriteLine("Hata: Seçim İşlemi Belirtilen (1-5) Gibi Sayı değeri Olmalıdır!");
                    yardimciMetot.Bekle();
                    return;
                }

                if (islem2 == 1)
                {
                    Console.Clear();
                    Console.Write("Kitap İsmini Giriniz: ");
                    string kitapAd = Console.ReadLine().Trim();
                    if (string.IsNullOrEmpty(kitapAd))
                    {
                        Console.WriteLine("Hata: Kitap İsmini Boş Bıraktınız!");
                        yardimciMetot.Bekle();
                        return;
                    }

                    var kitabaGore = db.Kitaplar.Where(x => x.Baslik.ToLower() == kitapAd.ToLower()).ToList();
                    if (kitabaGore.Any())
                    {
                        foreach (var x in kitabaGore)
                        {
                            Console.WriteLine($"\n---------------\nISBN: {x.ISBN}\nBaşlık: {x.Baslik}\nYazar: {x.Yazar},\nKategori: {x.Kategori},\nStok: {x.Stok}\n---------------\n");
                        }
                    }
                    else Console.WriteLine($"Sistemimizde {kitapAd} İsminde Bir Kitap Bulunamamıştır.");

                }
                else if (islem2 == 2)
                {
                    Console.Clear();
                    Console.Write("Yazar Adını Giriniz: ");
                    string yazarAd = Console.ReadLine().Trim();
                    if (string.IsNullOrEmpty(yazarAd))
                    {
                        Console.WriteLine("Hata: Yazar Adını Boş Bıraktınız!");
                        yardimciMetot.Bekle();
                        return;
                    }

                    var yazaraGore = db.Kitaplar.Where(x => x.Yazar == yazarAd).ToList();

                    if (yazaraGore.Any())
                    {
                        Console.WriteLine($"\n{yazarAd} adlı yazara ait {yazaraGore.Count} kitap bulundu:\n");
                        foreach (var x in yazaraGore)
                        {
                            Console.WriteLine($"\n---------------\nISBN: {x.ISBN}\nBaşlık: {x.Baslik}\nYazar: {x.Yazar},\nKategori: {x.Kategori},\nStok: {x.Stok}\n---------------\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Sistemimizde {yazarAd} İsimli Yazara Ait Kitap Bulunamamıştır!");
                    }
                }
                else if (islem2 == 3)
                {
                    Console.Clear();
                    Console.Write("ISBN No Giriniz: ");
                    if (!int.TryParse(Console.ReadLine().Trim(), out int isbnNo))
                    {
                        Console.WriteLine("Hata: ISBN Numarası Sayı Olmalıdır!");
                        yardimciMetot.Bekle();
                        return;
                    }

                    var isbnGore = db.Kitaplar.FirstOrDefault(x => x.ISBN == isbnNo);
                    if (isbnGore != null)
                    {
                        Console.WriteLine($"\n---------------");
                        Console.WriteLine($"ISBN: {isbnGore.ISBN}");
                        Console.WriteLine($"Başlık: {isbnGore.Baslik}");
                        Console.WriteLine($"Yazar: {isbnGore.Yazar}");
                        Console.WriteLine($"Kategori: {isbnGore.Kategori}");
                        Console.WriteLine($"Stok: {isbnGore.Stok}");
                        Console.WriteLine($"---------------\n");
                    }
                    else
                    {
                        Console.WriteLine("Böyle Bir ISBN Numaralı Kitap Bulunamadı!");
                    }
                }
                yardimciMetot.Bekle();
            }
            else if (secim2 == 4)
            {
                Console.Clear();
                Console.WriteLine("=== KİTAP GÜNCELLEME VE SİLME SİSTEMİ ===");
                Console.WriteLine("1. Kitap Güncelleme");
                Console.WriteLine("2. Kitap Silme");
                Console.Write("Yapmak istediğiniz işlem nedir ==>");
                if (!int.TryParse(Console.ReadLine().Trim(), out int islem3))
                {
                    Console.WriteLine("Hata: Seçim İşlemi Belirtilen (1-2) Gibi Sayı değeri Olmalıdır!");
                    yardimciMetot.Bekle();
                    return;
                }

                if (islem3 == 1)
                {
                    Console.Clear();
                    Console.Write("Güncellemek İstediğiniz Kitabın ISBN Numarasını Giriniz: ");
                    if (!int.TryParse(Console.ReadLine().Trim(), out int isbnNo))
                    {
                        Console.WriteLine("Hata: ISBN Numarası Sayı Olmalıdır!");
                        yardimciMetot.Bekle();
                        return;
                    }

                    var isbnGore = db.Kitaplar.FirstOrDefault(x => x.ISBN == isbnNo);
                    if (isbnGore != null)
                    {
                        Console.WriteLine($"Seçilen Kitap Adı: {isbnGore.Baslik} | Stok Adeti: {isbnGore.Stok}");
                        Console.Write("Yeni Stok Adeti: ");
                        if (!int.TryParse(Console.ReadLine().Trim(), out int yeniAdet))
                        {
                            Console.WriteLine("Hata: Adet Değeri Sayı Olmalıdır!");
                            yardimciMetot.Bekle();
                            return;
                        }

                        if (yeniAdet >= 0)
                        {
                            isbnGore.Stok = yeniAdet;
                        }
                        else
                        {
                            Console.WriteLine("Eksi Değer Girilemez Stok Aynı Şekilde Güncellencektir.");
                        }
                        db.SaveChanges();
                        Console.WriteLine($"Stok {isbnGore.Stok} Olarak Yenilendi");
                    }
                    else Console.WriteLine("Böyle Bir ISBN Numaralı Kitap Bulunamadı!");
                    yardimciMetot.Bekle();
                }
                else if (islem3 == 2)
                {
                    Console.Clear();
                    Console.Write("Silmek İstediğiniz Kitabın ISBN Numarasını Giriniz: ");
                    if (!int.TryParse(Console.ReadLine().Trim(), out int isbnNo))
                    {
                        Console.WriteLine("Hata: ISBN Numarası Sayı Olmalıdır!");
                        yardimciMetot.Bekle();
                        return;
                    }

                    var silinecekKitap = db.Kitaplar.FirstOrDefault(x => x.ISBN == isbnNo);
                    if (silinecekKitap != null)
                    {
                        db.Remove(silinecekKitap);
                        Console.WriteLine("Kitap Başarıyla Silindi!");
                    }
                    else Console.WriteLine("Böyle Bir ISBN Numaralı Kitap Bulunamadı!");
                    db.SaveChanges();
                }
                else Console.WriteLine("Sadece 1 veya 2 Diye Tuşlama Yapınız!");
                yardimciMetot.Bekle();
            }

            else if (secim2 == 5)
            {
                Console.Clear();
                Console.WriteLine("=== KİTAP STOK GÖRÜNTÜLEME SİSTEMİ ===");
                Console.Write("Kitap ISBN Numarası: ");
                if (!int.TryParse(Console.ReadLine().Trim(), out int isbnNo))
                {
                    Console.WriteLine("Kitap ISBN Numarasını Lütfen Tam Sayı Değeri Olarak Giriniz!");
                    yardimciMetot.Bekle();
                    return;
                }

                Console.Clear();

                var kitapSorgu = db.Kitaplar.FirstOrDefault(x => x.ISBN == isbnNo);

                if (kitapSorgu == null)
                {
                    Console.WriteLine($"{isbnNo} ISBN Numaralı Kitap Sistemimizde yoktur!");
                    yardimciMetot.Bekle();
                    return;
                }
                Console.WriteLine("\nKitap Stok Durumu Görüntüleme");
                Console.WriteLine("------------------------------------------------------");
                Console.WriteLine($"Kitap Adı: {kitapSorgu.Baslik}");
                Console.WriteLine($"Kitap Yazar: {kitapSorgu.Yazar}");
                Console.WriteLine($"Kitap ISBN Numarası: {kitapSorgu.ISBN}");
                Console.WriteLine($"Kitap Stok Durumu: {kitapSorgu.Stok}");

                yardimciMetot.Bekle();
            }

            else
            {
                Console.WriteLine("Lütfen Belirtilen Kategori Numarasına Göre Tuşlayınız! Örnek: (1), (2)...");
                yardimciMetot.Bekle();
                return;
            }
        }
    }
}
