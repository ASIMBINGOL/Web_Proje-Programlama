using System.ComponentModel.DataAnnotations;

namespace Web_Proje.Models
{
    public class CalismaMesai
    {
        public int CalismaMesaiID { get; set; }
        public int CalisanID { get; set; }
        public DayOfWeek Gun { get; set; }  //admin girecek müşteriye bilgi verilecek bilgi
        public TimeSpan SaatBaslangic { get; set; } //Bu şekilde olmalı
        public TimeSpan SaatBitis { get; set; }
         public Calisan? Calisan { get; set; }
    }
}