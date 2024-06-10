using System.Collections.Generic;

namespace Sube2.HelloMvc.Models
{
    public class Ogrenci
    {
        public int Ogrenciid { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public int Numara { get; set; }
        public ICollection<Ders> Dersler { get; set; } // İlişki ekledik

        //public override string ToString()
        //{
        //    return $"Ad:{this.Ad}-Soyad:{Soyad}-Numara:{Numara}";
        //}
    }
}
