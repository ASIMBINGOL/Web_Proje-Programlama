using System.ComponentModel.DataAnnotations;

namespace Web_Proje.Models
{
    public class Calisan
    {
        public int CalisanID { get; set; }
     
        [Display(Name ="Çalışan Adı")]
        [Required(ErrorMessage ="Lütfen Adını Giriniz")]     
        public string? CalisanAd { get; set; }
                                                              //Çalışan ekleme müşteri bilgi verme
        [Display(Name ="Çalışan Soyadı")]
        [Required(ErrorMessage ="Lütfen Soyadını Giriniz")]
        public string? CalisanSoyad { get; set; }

        public string AdSoyad
        {
            get
            {
                return CalisanAd+" "+CalisanSoyad;
            }
        }

        [Display(Name ="Çalışan Telefon")]
        [Required(ErrorMessage ="Lütfen Çalışan Telefonu Giriniz")]
        [DataType(DataType.PhoneNumber)]
        public string? CalisanTelefon { get; set; }

        [Display(Name ="Çalışan Email")]
        [Required(ErrorMessage ="Lütfen Çalışan Email Giriniz")]
        [EmailAddress(ErrorMessage ="Lütfen Geçerli Bir Mail Adresi Giriniz")]
        public string? CalisanEmail { get; set; }
        [Display(Name ="Çalışan Uzmanlık Alanı")]
        [Required(ErrorMessage ="Lütfen Çalışanın Uzmanlık Alanını Giriniz")]

        public string? Aciklama { get; set; }

        public int IslemID { get; set; }
        public ICollection<Islemler> Islemlers {get;set;}=new List<Islemler>();
    
    }
}