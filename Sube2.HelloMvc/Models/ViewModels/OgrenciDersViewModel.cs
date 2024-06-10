namespace Sube2.HelloMvc.Models
{
    public class OgrenciDersViewModel
    {
        public int Ogrenciid { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public int Numara { get; set; }
        public ICollection<Ders> Dersler { get; set; }
        public int SelectedDersId { get; set; }
    }
}
