# Kütüphane Yönetim Sistemi

C# konsol uygulaması ile kütüphane için basit ama etkili bir kütüphane yönetim sistemi yaptım.

Amacım OOP Mantığını oturtmak, LINQ Sorguları, Code First Yaklaşımı gibi kavramları uygulayabilmek ve ortaya güzel bir ürün çıkarmak istedim.

## Ne yapıyor?

- Kitap ekleme, silme, güncelleme
- Üye kayıt ve yönetimi
- Kitap ödünç verme ve iade alma
- Geciken kitapları gösterme
- Basit raporlar (en çok ödünç alınan kitaplar vs.)

## Kullandığım teknolojiler

- C#
- Entity Framework Core
- PostgreSQL

## Nasıl çalıştırılır?

1. PostgreSQL'de `KutuphaneDB` adında veritabanı oluşturun
2. `KutuphaneContext.cs` dosyasında bağlantı bilgilerinizi güncelleyin
3. Package Manager Console'da `Update-Database` komutunu çalıştırın
4. Projeyi başlatın

## Özellikler

### Kitap Yönetimi
- Kitap ekleme, güncelleme, silme
- ISBN numarası ile arama
- Yazar ve kategoriye göre listeleme
- Stok takibi

### Üye Yönetimi
- Yeni üye kaydı
- TC No ile benzersiz kayıt
- Üye bilgilerini güncelleme (telefon, email, adres)
- Üye silme (ödünç kaydı yoksa)

### Ödünç İşlemleri
- Kitap ödünç verme
- Kitap iade alma
- Gecikme kontrolü ve hesaplama
- Ödünç süresini uzatma (maksimum 14 gün)
- Üye başına maksimum 3 kitap sınırı

### Raporlama
- En çok ödünç alınan kitaplar (Top 10)
- En aktif üyeler
- Geciken kitapları görüntüleme

## Proje Yapısı
```
Kutuphane/
│
├── Kutuphane.Entities/          # Entity sınıfları (Kitap, Üye, OduncIslem)
├── Kutuphane.DataAccess/        # DbContext ve veritabanı konfigürasyonu
└── Kutuphane.ConsoleUI/         # Kullanıcı arayüzü ve iş mantığı
    └── Islemler/                # İşlem sınıfları (Kitap, Üye, Ödünç, Rapor)
```

## Kurulum ve Çalıştırma

### Gereksinimler
- .NET 6.0 veya üzeri
- PostgreSQL 12 veya üzeri

### Adımlar

1. **Projeyi klonlayın:**
```bash
git clone https://github.com/KULLANICI_ADIN/Kutuphane-Yonetim-Sistemi.git
cd Kutuphane-Yonetim-Sistemi
```

2. **PostgreSQL'de veritabanı oluşturun:**
```sql
CREATE DATABASE KutuphaneDB;
```

3. **Bağlantı bilgilerini güncelleyin:**
`KutuphaneContext.cs` dosyasında PostgreSQL bağlantı bilgilerinizi düzenleyin.

4. **Migration'ları çalıştırın:**
```bash
dotnet ef database update
```

5. **Uygulamayı başlatın:**
```bash
dotnet run --project Kutuphane.ConsoleUI
```

## Kullanım

Uygulama başladığında ana menü açılır:
```
=== KÜTÜPHANE YÖNETİM SİSTEMİ ===
1. Kitap İşlemleri
2. Üye İşlemleri
3. Ödünç İşlemleri
4. Raporlar
5. Çıkış
```

Her menüden ilgili işlemlere ulaşabilirsiniz.

## Önemli Kurallar

- Bir üye aynı anda **maksimum 3 kitap** alabilir
- Ödünç süresi **14 gündür**
- Geciken kitabı olan üye yeni kitap alamaz
- Ödünç süresi maksimum **14 gün** uzatılabilir
- TC No ve ISBN numaraları benzersiz olmalıdır

## Teknik Detaylar

- **Entity Framework Core** ile Code First yaklaşımı kullanıldı
- **Navigation Properties** ile ilişkisel sorgular optimize edildi
- **LINQ** ile GroupBy, OrderBy gibi karmaşık sorgular yapıldı
- **Cascade/Restrict** ile veri bütünlüğü sağlandı
- Try-catch blokları ile hata yönetimi uygulandı

## Veritabanı Şeması

**Kitaplar:**
- ISBN (Unique)
- Başlık, Yazar, Yayınevi, Kategori
- Sayfa sayısı, Yayın yılı
- Stok bilgisi

**Üyeler:**
- TC No (Unique)
- Ad, Soyad
- Telefon, Email, Adres
- Kayıt tarihi

**Ödünç İşlemler:**
- Kitap ve Üye ilişkisi (Foreign Keys)
- Alma tarihi, Son iade tarihi, İade tarihi
- Gecikme bilgisi

## Öğrendiklerim

Bu projeyi geliştirirken:
- Entity Framework Core ile ilişkisel veritabanı yönetimi
- Code First Migration kullanımı
- LINQ ile veri sorgulama ve gruplama
- Navigation Properties ve Include/ThenInclude
- Katmanlı mimari yapısı
- Konsol uygulaması kullanıcı deneyimi
