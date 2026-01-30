using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Kutuphane.Entities
{
    public class Uye
    {
        public int Id { get; set; }
        [StringLength(11)]
        public string TcNo { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        [StringLength(11)]
        public string TelefonNumarasi { get; set; }
        public string Email { get; set; }
        public string Adres { get; set; }
        public DateTime KayitTarihi { get; set; }

        public virtual ICollection<OduncIslem> OduncIslemler { get; set; }
    }
}
