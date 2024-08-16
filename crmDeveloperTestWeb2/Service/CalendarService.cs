using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Globalization;
using System.Linq;
using crmDeveloperTestWeb2.Models;

namespace crmDeveloperTestWeb2 {
    public class CalendarService {
        private readonly CalendarContext _context;
        public CalendarService(CalendarContext context) {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task FetchAndStoreData(int year) {
            string url = $"https://isdayoff.ru/api/getdata?year={year}&pre=1";

            using (HttpClient client = new HttpClient()) {
                try {
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    string content = await response.Content.ReadAsStringAsync();
                    ProcessAndStoreData(content, year);
                } catch (HttpRequestException e) {
                    Console.WriteLine($"Request error: {e.Message}");
                }
            }
        }
        private void ProcessAndStoreData(string data, int year) {
            var startDate = new DateTime(year, 1, 1);
            var daysInYear = DateTime.IsLeapYear(year) ? 366 : 365;
            for (int i = 0; i < data.Length; i++) {
                if (i >= daysInYear)
                    break;
                char dayTypeChar = data[i];
                var date = startDate.AddDays(i);
                string dayType = dayTypeChar switch {
                    '0' => "рабочий",
                    '1' => "выходной",
                    '2' => "сокращенный",
                    _ => "неизвестно"
                };
                var calendarDay = new CalendarDay {
                    Date = date,
                    Weekday = date.ToString("dddd", new CultureInfo("ru-RU")),
                    DayType = dayType
                };
                var existingDay = _context.CalendarDays.Find(date);
                if (existingDay == null) {
                    _context.CalendarDays.Add(calendarDay);
                } else {
                    existingDay.Weekday = calendarDay.Weekday;
                    existingDay.DayType = calendarDay.DayType;
                }
            }
            _context.SaveChanges();
        }
    }
}