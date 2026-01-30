using Kutuphane.DataAccess;
using Kutuphane.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kutuphane.ConsoleUI.Islemler
{
    public class UyeIslemleri
    {
        public void UyeIslemleriMetot()
        {
            YardimciMetotlar yardimciMetot = new YardimciMetotlar();
            Console.Clear();
            var db = new KutuphaneContext();
            Console.WriteLine("1. Üye Kaydı");
            Console.WriteLine("2. Üye Listeleme");
            Console.WriteLine("3. Üye Arama");
            Console.WriteLine("4. Üye Bilgilerini Güncelleme");
            Console.WriteLine("5. Üye Silme");
            Console.Write("Yapmak istediğiniz işlem nedir ==>");
            if (!int.TryParse(Console.ReadLine().Trim(), out int secim))
            {
                Console.WriteLine("Hata: Seçim İşlemi Belirtilen (1-5) Gibi Sayı değeri Olmalıdır!");
                yardimciMetot.Bekle();
                return;
            }

            if (secim == 1)
            {
                Console.Clear();
                Console.WriteLine("=== ÜYE KAYDI SİSTEMİ ===");
                try
                {
                    Console.Write("Üye TC Numarası: ");
                    string tcNo = Console.ReadLine().Trim();
                    if (string.IsNullOrEmpty(tcNo))
                    {
                        Console.WriteLine("Hata: TC Numarasını Boş Bıraktınız!");
                        yardimciMetot.Bekle();
                        return;
                    }
                    Console.Write("Üye Ad: ");
                    string ad = Console.ReadLine().Trim();
                    if (string.IsNullOrEmpty(ad))
                    {
                        Console.WriteLine("Hata: Üye Adını Boş Bıraktınız!");
                        yardimciMetot.Bekle();
                        return;
                    }
                    Console.Write("Üye Soyad: ");
                    string soyad = Console.ReadLine().Trim();
                    if (string.IsNullOrEmpty(soyad))
                    {
                        Console.WriteLine("Hata: Üye Soyadını Boş Bıraktınız!");
                        yardimciMetot.Bekle();
                        return;
                    }
                    Console.Write("Üye Telefon Numarası: ");
                    string telNo = Console.ReadLine().Trim();
                    if (string.IsNullOrEmpty(telNo))
                    {
                        Console.WriteLine("Hata: Üye Telefon Numarasını Boş Bıraktınız!");
                        yardimciMetot.Bekle();
                        return;
                    }
                    Console.Write("Üye e-mail: ");
                    string email = Console.ReadLine().Trim();
                    if (string.IsNullOrEmpty(email))
                    {
                        Console.WriteLine("Hata: Üye Email Adresini Boş Bıraktınız!");
                        yardimciMetot.Bekle();
                        return;
                    }
                    Console.Write("Üye Açık Adres: ");
                    string adres = Console.ReadLine().Trim();
                    if (string.IsNullOrEmpty(adres))
                    {
                        Console.WriteLine("Hata: Üye Adres Bilgisini Boş Bıraktınız!");
                        yardimciMetot.Bekle();
                        return;
                    }

                    Uye yeniUye = new Uye();
                    yeniUye.TcNo = tcNo;
                    yeniUye.Ad = ad;
                    yeniUye.Soyad = soyad;
                    yeniUye.TelefonNumarasi = telNo;
                    yeniUye.Email = email;
                    yeniUye.Adres = adres;
                    yeniUye.KayitTarihi = DateTime.UtcNow; //EfCore Datatime PostreSql hatası almamak için UtcNow 

                    db.Add(yeniUye);
                    db.SaveChanges();
                    Console.WriteLine("Kayıt Başarılı!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Hata Mesajı: {ex}");
                }
                yardimciMetot.Bekle();
            }
            else if (secim == 2)
            {
                Console.Clear();
                Console.WriteLine("=== ÜYE LİSTELEME SİSTEMİ ===");
                Console.WriteLine("1. Tüm Üyeleri Listeleme");
                Console.WriteLine("2. TC Numarası İle Arama");
                Console.Write("Yapmak istediğiniz işlem nedir ==>");
                if (!int.TryParse(Console.ReadLine().Trim(), out int secim2))
                {
                    Console.WriteLine("Hata: Seçim İşlemi Belirtilen (1-2) Gibi Sayı değeri Olmalıdır!");
                    yardimciMetot.Bekle();
                    return;
                }

                if (secim2 == 1)
                {
                    Console.Clear();
                    var tumUyeler2 = db.Uyeler.Count();
                    Console.WriteLine($"\nKayıtlı Üyeler (Toplam: {tumUyeler2})");
                    var tumUyeler = db.Uyeler.ToList();
                    int sayac = 1;
                    foreach (var t in tumUyeler)
                    {
                        Console.WriteLine($"{sayac}. TC: {t.TcNo} | {t.Ad} {t.Soyad} | {t.TelefonNumarasi}");
                        sayac++;
                    }
                    yardimciMetot.Bekle();
                }
                else if (secim2 == 2)
                {
                    Console.Clear();
                    Console.Write("Üye TC Numarasını Giriniz: ");
                    string tcNo = Console.ReadLine().Trim();
                    if (string.IsNullOrEmpty(tcNo))
                    {
                        Console.WriteLine("Hata: TC Kimlik Numarasını Boş Bıraktınız!");
                        yardimciMetot.Bekle();
                        return;
                    }

                    var uyeDetay = db.Uyeler.FirstOrDefault(x => x.TcNo == tcNo);
                    if (uyeDetay != null)
                    {
                        Console.WriteLine("Üye Detayları");
                        Console.WriteLine("=====================================");
                        Console.WriteLine($"TC No: {uyeDetay.TcNo}");
                        Console.WriteLine($"Ad Soyad: {uyeDetay.Ad} {uyeDetay.Soyad}");
                        Console.WriteLine($"Telefon: {uyeDetay.TelefonNumarasi}");
                        Console.WriteLine($"Email: {uyeDetay.Email}");
                        Console.WriteLine($"Adres: {uyeDetay.Adres}");
                        Console.WriteLine($"Kayıt Tarihi: {uyeDetay.KayitTarihi}");
                        Console.WriteLine("ŞU AN ÖDÜNÇ ALDIĞI KİTAPLAR");
                        Console.WriteLine("EKLENECEK...");
                        Console.WriteLine("GEÇMİŞ İŞLEMLER (SON 10)");
                        Console.WriteLine("EKLENECEK...");
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("TC Kimlik Numarası Sistemimizde Kayıtlı Değildir!");
                    }
                    yardimciMetot.Bekle();
                }
            }
            else if (secim == 3)
            {
                Console.Clear();
                Console.WriteLine("ÜYE ARAMA SİSTEMİ");
                Console.WriteLine("1. TC Kimlik Numarası İle Arama");
                Console.WriteLine("2. Ad - Soyad İle Arama");
                Console.Write("Yapmak istediğiniz işlem nedir ==>");
                if (!int.TryParse(Console.ReadLine().Trim(), out var secim2))
                {
                    Console.WriteLine("Lütfen Sayısal Değer Girin!");
                    yardimciMetot.Bekle();
                    return;
                }

                if (secim2 == 1)
                {
                    Console.Clear();
                    Console.Write("Üye TC Numarasını Giriniz: ");
                    string tcNo = Console.ReadLine().Trim();
                    if (string.IsNullOrEmpty(tcNo))
                    {
                        Console.WriteLine("Hata: TC Kimlik Numarasını Boş Bıraktınız!");
                        yardimciMetot.Bekle();
                        return;
                    }

                    var uyeDetay = db.Uyeler.FirstOrDefault(x => x.TcNo == tcNo);
                    if (uyeDetay != null)
                    {
                        Console.Clear();
                        Console.WriteLine("Üye Detayları");
                        Console.WriteLine("=====================================");
                        Console.WriteLine($"TC No: {uyeDetay.TcNo}");
                        Console.WriteLine($"Ad Soyad: {uyeDetay.Ad} {uyeDetay.Soyad}");
                        Console.WriteLine($"Telefon: {uyeDetay.TelefonNumarasi}");
                        Console.WriteLine($"Email: {uyeDetay.Email}");
                        Console.WriteLine($"Adres: {uyeDetay.Adres}");
                        Console.WriteLine($"Kayıt Tarihi: {uyeDetay.KayitTarihi}");
                        Console.WriteLine("ŞU AN ÖDÜNÇ ALDIĞI KİTAPLAR");
                        Console.WriteLine("EKLENECEK...");
                        Console.WriteLine("GEÇMİŞ İŞLEMLER (SON 10)");
                        Console.WriteLine("EKLENECEK...");
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("TC Kimlik Numarası Sistemimizde Kayıtlı Değildir!");
                    }
                    yardimciMetot.Bekle();
                }
                else if (secim2 == 2)
                {
                    Console.Clear();
                    Console.Write("Üye Ad Giriniz: ");
                    string ad = Console.ReadLine().Trim();
                    if (string.IsNullOrEmpty(ad))
                    {
                        Console.WriteLine("Üye Adı Boş Bırakılamaz!");
                        yardimciMetot.Bekle();
                        return;
                    }

                    Console.Write("Üye Soyad Giriniz: ");
                    string soyad = Console.ReadLine().Trim();
                    if (string.IsNullOrEmpty(soyad))
                    {
                        Console.WriteLine("Üye Soyadı Boş Bırakılamaz!");
                        yardimciMetot.Bekle();
                        return;
                    }

                    var adSoyadArama = db.Uyeler.Where(x => x.Ad == ad && x.Soyad == soyad).ToList();
                    if (adSoyadArama.Any())
                    {
                        foreach (var x in adSoyadArama)
                        {
                            Console.WriteLine($"Ad: {x.Ad} - Soyad: {x.Soyad} - Email: {x.Email} - Telefon Numarası: {x.TelefonNumarasi}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Sistemimizde {ad} {soyad} İsimli Kişi Yoktur!");
                    }
                    yardimciMetot.Bekle();
                    return;
                }
                else
                {
                    Console.WriteLine("(1) veya (2) diye tuşlama yapınız!");
                    yardimciMetot.Bekle();
                    return;
                }
            }
            else if (secim == 4)
            {
                Console.Clear();
                Console.WriteLine("=== ÜYE BİLGİLERİNİ GÜNCELLEME SİSTEMİ ===");
                Console.Write("Güncellenecek Kişinin TC Kimlik Numarasını Giriniz: ");
                string tcNo = Console.ReadLine().Trim();
                if (string.IsNullOrEmpty(tcNo))
                {
                    Console.WriteLine("TC Kimlik Numarasını Boş Bırakamazsınız!");
                    yardimciMetot.Bekle();
                    return;
                }
                else
                {
                    Console.Clear();
                    var tcNoKisisi = db.Uyeler.FirstOrDefault(x => x.TcNo == tcNo);

                    if (tcNoKisisi != null)
                    {
                        Console.WriteLine($"{tcNoKisisi.TcNo} TC Numaralı Kişinin Hangi Bilgisinde Güncelleme Yapmak İstiyorsunuz?");
                        Console.WriteLine("1. Telefon Numarası Değişikliği");
                        Console.WriteLine("2. Email Adresi Değişikliği");
                        Console.WriteLine("3. Açık Adres Değişikliği");
                        Console.Write("Yapmak istediğiniz işlem nedir ==>");
                        if (!int.TryParse(Console.ReadLine().Trim(), out int secim2))
                        {
                            Console.WriteLine("Sayısal Değer Girilmelidir!");
                            yardimciMetot.Bekle();
                            return;
                        }

                        if (secim2 == 1)
                        {
                            Console.Clear();
                            Console.Write("Yeni Telefon Numaranızı Giriniz: ");
                            string yeniTelNo = Console.ReadLine().Trim();
                            if (string.IsNullOrEmpty(yeniTelNo))
                            {
                                Console.WriteLine("Lütfen Telefon Numarası Alanını Boş bırakmayınız!");
                                yardimciMetot.Bekle();
                                return;
                            }

                            if (yeniTelNo == tcNoKisisi.TelefonNumarasi)
                            {
                                Console.WriteLine("Zaten Sistemde Kayıtlı Olan Telefon Numaranız ile Girdiğiniz Telefon Numarası Aynıdır!");
                                yardimciMetot.Bekle();
                                return;
                            }
                            else
                            {
                                string eskiTelNo = tcNoKisisi.TelefonNumarasi;
                                tcNoKisisi.TelefonNumarasi = yeniTelNo;
                                Console.WriteLine("Tebrikler Sistemimizde Telefon Numaranız Güncellenmiştir!");
                                Console.WriteLine($"Eski Telefon Numarası: {eskiTelNo} ===> Yeni Telefon Numarası: {tcNoKisisi.TelefonNumarasi}");
                                db.SaveChanges();
                                yardimciMetot.Bekle();
                                return;
                            }

                        }
                        else if (secim2 == 2)
                        {
                            Console.Clear();
                            Console.Write("Yeni Email Adresini Giriniz: ");
                            string yeniEmail = Console.ReadLine().Trim();
                            if (string.IsNullOrEmpty(yeniEmail))
                            {
                                Console.WriteLine("Lütfen Email Adresi Alanını Boş bırakmayınız!");
                                yardimciMetot.Bekle();
                                return;
                            }

                            if (yeniEmail == tcNoKisisi.Email)
                            {
                                Console.WriteLine("Zaten Sistemde Kayıtlı Olan Email Adresiniz ile Girdiğiniz Email Adresi Aynıdır!");
                                yardimciMetot.Bekle();
                                return;
                            }
                            else
                            {
                                string eskiEmail = tcNoKisisi.Email;
                                tcNoKisisi.Email = yeniEmail;
                                Console.WriteLine("Tebrikler Sistemimizde Telefon Numaranız Güncellenmiştir!");
                                Console.WriteLine($"Eski Email Adresi: {eskiEmail} ===> Yeni Email Adresi: {tcNoKisisi.Email}");
                                db.SaveChanges();
                                yardimciMetot.Bekle();
                                return;
                            }
                        }
                        else if (secim2 == 3)
                        {
                            Console.Clear();
                            Console.Write("Yeni Açık Adresinizi Giriniz: ");
                            string yeniAdres = Console.ReadLine().Trim();
                            if (string.IsNullOrEmpty(yeniAdres))
                            {
                                Console.WriteLine("Lütfen Email Adresi Alanını Boş bırakmayınız!");
                                yardimciMetot.Bekle();
                                return;
                            }

                            if (yeniAdres == tcNoKisisi.Adres)
                            {
                                Console.WriteLine("Zaten Sistemde Kayıtlı Olan Açık Adresiniz ile Girdiğiniz Açık Adres Aynıdır!");
                                yardimciMetot.Bekle();
                                return;
                            }
                            else
                            {
                                string eskiAdres = tcNoKisisi.Adres;
                                tcNoKisisi.Adres = yeniAdres;
                                Console.WriteLine("Tebrikler Sistemimizde Telefon Numaranız Güncellenmiştir!");
                                Console.WriteLine($"Eski Email Adresi: {eskiAdres} ===> Yeni Email Adresi: {tcNoKisisi.Adres}");
                                db.SaveChanges();
                                yardimciMetot.Bekle();
                                return;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Lütfen Belirtilen Kategorilere Göre Tuşlama Yapınız. (1), (2)...");
                            yardimciMetot.Bekle();
                            return;
                        }
                    }
                }
            }
            else if (secim == 5)
            {
                Console.Clear();
                Console.WriteLine("=== ÜYE SİLME SİSTEMİ ===");
                Console.Write("Silinecek Üye TC Kimlik Numarası");
                string tcNo = Console.ReadLine().Trim();
                if (string.IsNullOrEmpty(tcNo))
                {
                    Console.WriteLine("Lütfen TC Kimlik Numarasını Boş Bırakmayınız!");
                    yardimciMetot.Bekle();
                    return;
                }

                var uyeVarMi = db.Uyeler
                    .Include(x => x.OduncIslemler)
                    .ThenInclude(x => x.Kitap)
                    .FirstOrDefault(x => x.TcNo == tcNo);

                if (uyeVarMi == null)
                {
                    Console.WriteLine("Bu TC'ye kayıtlı üye bulunamadı!");
                    yardimciMetot.Bekle();
                    return;
                }

                var aktifOduncler = uyeVarMi.OduncIslemler.Where(x => !x.IadeEdildiMi).ToList();

                if (aktifOduncler.Any())
                {
                    Console.WriteLine("Üyeye Ait İade Edilmemiş Ödünç İşlem Vardır!");

                    foreach (var x in aktifOduncler)
                    {
                        Console.WriteLine($"Kitap Adı: {x.Kitap.Baslik}");
                        Console.WriteLine($"Yazar Adı: {x.Kitap.Yazar}");
                        Console.WriteLine($"ISBN Numarası: {x.Kitap.ISBN}");
                    }
                    Console.WriteLine("\nLütfen Üyelik Silmek İçin İade İşlemini Tamamlayınız!");
                    yardimciMetot.Bekle();
                    return;
                }

                while (true)
                {
                    Console.WriteLine($"Ad: {uyeVarMi.Ad}");
                    Console.WriteLine($"Soyad: {uyeVarMi.Soyad}");
                    Console.WriteLine($"TC: {uyeVarMi.TcNo}");
                    Console.WriteLine("Bu Kişiyi Silmek İstediğinize Emin Misiniz?(E/H)");
                    Console.Write("Cevabınız: ");
                    string cevap = Console.ReadLine().Trim();
                    if (string.IsNullOrEmpty(cevap))
                    {
                        Console.WriteLine("Lütfen Cevabınızı Metin Değeri Olarak Veriniz!");
                        yardimciMetot.Bekle();
                        continue;
                    }

                    if (cevap.Trim().ToUpper() != "E")
                    {
                        Console.WriteLine("İşlem iptal edildi.");
                        yardimciMetot.Bekle();
                        return;
                    }
                    break;
                }

                //Bunu Denedim
                //db.OduncIslemler.RemoveRange(uyeVarMi.OduncIslemler); Olmadı GPT Dedidiki(Önce silinecek geçmişi ayrı bir liste olarak hafızaya al)

                //IDLER EŞİT Mİ BAKTI VARSA LİSTELEDİ
                var silinecekGecmis = db.OduncIslemler.Where(x => x.UyeId == uyeVarMi.Id).ToList();

                if (silinecekGecmis.Any())
                {
                    db.OduncIslemler.RemoveRange(silinecekGecmis);
                }

                ////Uye silecektim fakat Hata Aldım
                //Unhandled exception. System.InvalidOperationException: The association between entity types 'Uye' and 'OduncIslem' has been severed
                db.Uyeler.Remove(uyeVarMi);
                db.SaveChanges();
                Console.WriteLine("Üye Başarıyla Silindi!");
                yardimciMetot.Bekle();
            }
        }
    }
}
