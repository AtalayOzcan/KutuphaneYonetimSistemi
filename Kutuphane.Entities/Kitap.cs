using System;
using System.Collections.Generic;
using System.Text;

namespace Kutuphane.Entities
{
    public class Kitap
    {
        public int Id { get; set; }
        public int ISBN { get; set; }
        public string Baslik { get; set; }
        public string Yazar { get; set; }
        public string YayinEvi { get; set; }
        public string Kategori { get; set; }
        public int SayfaSayisi { get; set; }
        public int YayinYili { get; set; }
        public int Stok { get; set; }

        public virtual ICollection<OduncIslem> OduncIslemler { get; set; }
    }
}
