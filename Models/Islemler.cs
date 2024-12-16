using System.ComponentModel.DataAnnotations;

namespace Web_Proje.Models
{
    public class Islemler
    {
        [Key]//anlamadığım biçimde bunu yazmadan primary key hatası verip migrations oluşturmadı
        public int IslemID { get; set; }
        [Display(Name ="İşlem Adı")]
        [Required(ErrorMessage ="Lütfen İşlem Seçiniz")] //kurs desek
        public string? IslemAdi { get; set; }

        [Display(Name ="Ücret")]                                  //Daha çok Admin ekleme ve müşteriye bilgi verme amaçlı
        [Required(ErrorMessage ="Lütfen Ücret Bilgsini Gİriniz")]
        public int Ucret { get; set; }
        

        public ICollection<Calisan>? calisans { get; set; }


       
    }
}