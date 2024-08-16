using System.Linq;
using crmDeveloperTestWeb2;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]

public class CalendarController : ControllerBase {
    private readonly CalendarContext _context;
    private readonly CalendarService _calendarService;
    public CalendarController(CalendarContext context, CalendarService calendarService) {
        _context = context;
        _calendarService = calendarService;
    }
    [HttpGet("daytype")]
    public IActionResult GetDayType(DateTime date) {
        var calendarDay = _context.CalendarDays.Find(date);
        if (calendarDay == null) {
            return NotFound("Дата не найдена.");
        }
        return Ok(new {
            Date = calendarDay.Date.ToString("dd.MM.yyyy"),
            Weekday = calendarDay.Weekday,
            DayType = calendarDay.DayType
        });
    }
    [HttpPost("update")]
    public async Task<IActionResult> UpdateCalendarData(int year) {
        await _calendarService.FetchAndStoreData(year);
        return Ok($"Новый год - {year} разобран на дни");
    }
}
