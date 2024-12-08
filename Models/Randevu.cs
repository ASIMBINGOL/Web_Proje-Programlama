using System.ComponentModel.DataAnnotations;

namespace Web_Proje.Models
{
    public class Randevu
    {
        public int RandevuID { get; set; }
        public int MusteriID { get; set; }
        public int CalisanID { get; set; }
        public int IslemID { get; set; }


        [DataType(DataType.Date)]//Sadece tarih
        [DisplayFormat(DataFormatString ="{0:dd-MM-yyyy}",ApplyFormatInEditMode =false)]
        [Display(Name ="Randevu Tarihi")]
        [Required(ErrorMessage ="Lütfen Tarih Seçiniz")]
        public DateTime Tarih { get; set; }
                                                 //Müşteri ile işikilendrilip randevu alma ekrnaında kullanılcak
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString ="{0:HH-mm}",ApplyFormatInEditMode = false)]
        [Display(Name ="Randevu Saati")]
        [Required(ErrorMessage ="Lütfen Saat Seçiniz")]
        public TimeSpan Saat{ get; set; }
    }
}