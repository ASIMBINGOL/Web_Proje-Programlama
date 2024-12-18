using System.ComponentModel.DataAnnotations;

public class RandevuViewModel
{
    [Required(ErrorMessage = "Tarih alanı zorunludur.")]
    [DataType(DataType.Date)]
    public DateTime Tarih { get; set; } // Randevu Tarihi

    [Required(ErrorMessage = "Çalışan seçimi zorunludur.")]
    public int CalisanID { get; set; } // Çalışan ID

    [Required(ErrorMessage = "Hizmet seçimi zorunludur.")]
    public int IslemID { get; set; } // Hizmet ID

    [Required(ErrorMessage = "Saat seçimi zorunludur.")]
    public string UygunSaat { get; set; } // Uygun Saat (string, "HH:mm" formatında)
}