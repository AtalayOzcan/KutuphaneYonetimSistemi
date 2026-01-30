using System;
using System.Collections.Generic;
using System.Text;

namespace Kutuphane.Entities
{
    public class OduncIslem
    {
        public int Id { get; set; }

        public int KitapId { get; set; }
        public int UyeId { get; set; }

        public DateTime AlmaTarihi { get; set; }
        public DateTime SonIadeTarihi { get; set; }
        public DateTime? IadeTarihi { get; set; } //Null Olabilir Artık

        public bool IadeEdildiMi { get; set; }
        public int GecikmeGunSayisi { get; set; }

        public virtual Kitap Kitap { get; set; }
        public virtual Uye Uye { get; set; }
    }
}
