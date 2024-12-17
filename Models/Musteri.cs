using System.ComponentModel.DataAnnotations;

namespace Web_Proje.Models
{
    public class Musteri
    {
        public int MUsteriID { get; set; }

        [Display(Name ="Adınız")]
        [Required(ErrorMessage ="Lütfen Adınızı Giriniz")]
        public string MUsteriAd { get; set; }=null!;
                                                           //Müşteri ekleme kayıt olma ve login
        [Display(Name ="Soyadınız")]
        [Required(ErrorMessage ="Lütfen Soyadınızı Giriniz")]
        public string MUsteriSoyad { get; set; }=null!;

        [Display(Name ="Telefon")]
        [Required(ErrorMessage ="Lütfen Telefon Numaranızı Giriniz")]
        [DataType(DataType.PhoneNumber)]
        
        public string AdSoyad{
        get
        {
            return MUsteriAd+" "+MUsteriSoyad;
        }}
        public string MUsteriTelefon { get; set; }=null!;
        [Display(Name ="Email")]
        [Required(ErrorMessage ="Lütfen Email Giriniz")]
        [EmailAddress(ErrorMessage ="Lütfen Geçerli Bir Mail Adresi Gİriniz")]
        public string MUsteriEmail { get; set; }=null!;

        [Display(Name ="Şifre")]
        [Required(ErrorMessage ="Lütfen Şifre Giriniz")]
        [DataType(DataType.Password)]
        public string MUsteriSifre { get; set; }=null!;

        [Display(Name ="Kayıt Tarihi")]
        [DataType(DataType.Date)]//Sadece tarih
        [DisplayFormat(DataFormatString ="{0:dd-MM-yyyy}",ApplyFormatInEditMode =true)]
        public DateTime KayitTarihi { get; set; }
    }
}