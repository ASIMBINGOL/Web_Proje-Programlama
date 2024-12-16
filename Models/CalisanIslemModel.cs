using System.ComponentModel.DataAnnotations;

namespace Web_Proje.Models
{
    public class CalisanIslemModel
    {
        public int CalisanID { get; set; }
     
        [Display(Name ="Çalışan Adı")] 
        public string? CalisanAd { get; set; }
                                                              //Çalışan ekleme müşteri bilgi verme
        [Display(Name ="Çalışan Soyadı")]
        public string? CalisanSoyad { get; set; }

        public string AdSoyad
        {
            get
            {
                return CalisanAd+" "+CalisanSoyad;
            }
        }

        [Display(Name ="Çalışan Telefon")]

        public string? CalisanTelefon { get; set; }

        [Display(Name ="Çalışan Email")]
        public string? CalisanEmail { get; set; }
        [Display(Name ="Çalışan Uzmanlık Alanı")]
        public string? Aciklama { get; set; }

        public string? IslemAdi { get; set; }
    
    }
}