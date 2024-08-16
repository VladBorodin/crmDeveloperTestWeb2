using crmDeveloperTestWeb2.Models;
using Microsoft.EntityFrameworkCore;
public class CalendarContext : DbContext {
    public DbSet<CalendarDay> CalendarDays { get; set; }
    public CalendarContext(DbContextOptions<CalendarContext> options) : base(options) { }

}
