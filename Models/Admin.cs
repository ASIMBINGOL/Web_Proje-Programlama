using System.ComponentModel.DataAnnotations;

namespace Web_Proje.Models
{
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }
        public string? AdminEmail { get; set; }
        public string? AdminSifre { get; set; }
    }
}