using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sube2.HelloMvc.Models
{
    public class Ders
    {
        public int Dersid { get; set; }

        [Required(ErrorMessage = "Ders Kodu gereklidir")]
        [StringLength(10, ErrorMessage = "Ders Kodu en fazla 10 karakter olabilir")]
        public string DersKodu { get; set; }

        [Required(ErrorMessage = "Ders Adı gereklidir")]
        [StringLength(100, ErrorMessage = "Ders Adı en fazla 100 karakter olabilir")]
        public string Dersad { get; set; }

        [Required(ErrorMessage = "Kredi gereklidir")]
        [Range(1, 10, ErrorMessage = "Kredi 1 ile 10 arasında olmalıdır")]
        public byte Kredi { get; set; }

        public ICollection<Ogrenci> Ogrenciler { get; set; } = new List<Ogrenci>();
    }
}
