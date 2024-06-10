using Microsoft.EntityFrameworkCore;

namespace Sube2.HelloMvc.Models
{
    public class OkulDbContext : DbContext
    {
        public OkulDbContext(DbContextOptions<OkulDbContext> options) : base(options) { }

        public DbSet<Ogrenci> Ogrenciler { get; set; }
        public DbSet<Ders> Dersler { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Ogrenci>().ToTable("tblOgrenciler");
            modelBuilder.Entity<Ogrenci>().Property(o => o.Ad).HasColumnType("varchar").HasMaxLength(30).IsRequired();
            modelBuilder.Entity<Ogrenci>().Property(o => o.Soyad).HasColumnType("varchar").HasMaxLength(40).IsRequired();
            modelBuilder.Entity<Ogrenci>()
                .HasMany(o => o.Dersler)
                .WithMany(d => d.Ogrenciler)
                .UsingEntity(j => j.ToTable("OgrenciDersler"));

            modelBuilder.Entity<Ders>().ToTable("tblDersler");
            modelBuilder.Entity<Ders>().Property(d => d.DersKodu).HasColumnType("varchar").HasMaxLength(10).IsRequired();
            modelBuilder.Entity<Ders>().Property(d => d.Dersad).HasColumnType("varchar").HasMaxLength(100).IsRequired();
            modelBuilder.Entity<Ders>().Property(d => d.Kredi).IsRequired();
        }
    }
}
