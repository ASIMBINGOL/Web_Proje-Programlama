using Microsoft.EntityFrameworkCore;

namespace Web_Proje.Models
{
    public class KuaforContext:DbContext
    {
         public KuaforContext(DbContextOptions<KuaforContext> options):base(options){}//vderi tabanı bağlantısı
        public DbSet<Admin> Admin =>Set<Admin>();
        public DbSet<Calisan> Calisanlar =>Set<Calisan>();
        public DbSet<Musteri> Musteri =>Set<Musteri>();
        public DbSet<Randevu> Randevular=>Set<Randevu>();
        public DbSet<Islemler> Islemler=>Set<Islemler>();
        public DbSet<CalismaMesai> CalismaSaatleri =>Set<CalismaMesai>();
    }
}