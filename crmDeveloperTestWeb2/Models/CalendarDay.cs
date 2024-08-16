using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace crmDeveloperTestWeb2.Models {
    public class CalendarDay {
        [Key]
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }
        public string Weekday { get; set; }
        public string DayType { get; set; }
    }
}
