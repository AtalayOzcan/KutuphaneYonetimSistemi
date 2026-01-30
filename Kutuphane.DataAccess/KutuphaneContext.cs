using Kutuphane.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kutuphane.DataAccess
{
    public class KutuphaneContext : DbContext
    {
        public DbSet<Kitap> Kitaplar { get; set; }
        public DbSet<Uye> Uyeler { get; set; }
        public DbSet<OduncIslem> OduncIslemler { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Database=KutuphaneDB;Username=postgres;Password=*******");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // KITAPLAR
            modelBuilder.Entity<Kitap>().ToTable("Kitaplar");
            modelBuilder.Entity<Kitap>().Property(x => x.Id).HasColumnName("id");
            modelBuilder.Entity<Kitap>().Property(x => x.ISBN).HasColumnName("isbn");
            modelBuilder.Entity<Kitap>().Property(x => x.Baslik).HasColumnName("baslik");
            modelBuilder.Entity<Kitap>().Property(x => x.Yazar).HasColumnName("yazar");
            modelBuilder.Entity<Kitap>().Property(x => x.YayinEvi).HasColumnName("yayin_evi");
            modelBuilder.Entity<Kitap>().Property(x => x.Kategori).HasColumnName("kategori");
            modelBuilder.Entity<Kitap>().Property(x => x.SayfaSayisi).HasColumnName("sayfa_sayisi");
            modelBuilder.Entity<Kitap>().Property(x => x.YayinYili).HasColumnName("yayin_yili");
            modelBuilder.Entity<Kitap>().Property(x => x.Stok).HasColumnName("stok");

            modelBuilder.Entity<Kitap>().HasIndex(x => x.ISBN).IsUnique();

            // UYELER
            modelBuilder.Entity<Uye>().ToTable("Uyeler");
            modelBuilder.Entity<Uye>().Property(x => x.Id).HasColumnName("id");
            modelBuilder.Entity<Uye>().Property(x => x.TcNo).HasColumnName("tc_no");
            modelBuilder.Entity<Uye>().Property(x => x.Ad).HasColumnName("ad");
            modelBuilder.Entity<Uye>().Property(x => x.Soyad).HasColumnName("soyad");
            modelBuilder.Entity<Uye>().Property(x => x.TelefonNumarasi).HasColumnName("telefon_numarasi");
            modelBuilder.Entity<Uye>().Property(x => x.Email).HasColumnName("email");
            modelBuilder.Entity<Uye>().Property(x => x.Adres).HasColumnName("adres");
            modelBuilder.Entity<Uye>().Property(x => x.KayitTarihi).HasColumnName("kayit_tarihi");

            modelBuilder.Entity<Uye>().HasIndex(x => x.TcNo).IsUnique();

            // ÖDÜNÇ İŞLEMLER
            modelBuilder.Entity<OduncIslem>(entity =>
            {
                entity.ToTable("OduncIslemler");
                entity.Property(x => x.Id).HasColumnName("id");
                entity.Property(x => x.KitapId).HasColumnName("kitap_id");
                entity.Property(x => x.UyeId).HasColumnName("uye_id");
                entity.Property(x => x.AlmaTarihi).HasColumnName("alma_tarihi");
                entity.Property(x => x.SonIadeTarihi).HasColumnName("son_iade_tarihi");
                entity.Property(x => x.IadeTarihi).HasColumnName("iade_tarihi");
                entity.Property(x => x.IadeEdildiMi).HasColumnName("iade_edildi_mi");
                entity.Property(x => x.GecikmeGunSayisi).HasColumnName("gecikme_gun_sayisi");

                // İlişkiler
                entity.HasOne(o => o.Kitap)
                    .WithMany(k => k.OduncIslemler)
                    .HasForeignKey(o => o.KitapId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(o => o.Uye)
                    .WithMany(u => u.OduncIslemler)
                    .HasForeignKey(o => o.UyeId)
                    .OnDelete(DeleteBehavior.Restrict);//Anlamı: Ana kaydı silmeye çalışırsan, eğer bağlı kayıtlar varsa silemiyorsun!
            });
        }
    }
}

