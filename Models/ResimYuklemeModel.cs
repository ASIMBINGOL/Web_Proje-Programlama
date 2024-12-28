namespace Web_Proje.Models
{
    public class ResimYuklemeModel
    {
        public IFormFile? Image { get; set; } // Yüklenecek fotoğraf
        public string? Prompt { get; set; } // Prompt açıklaması
    }
}
