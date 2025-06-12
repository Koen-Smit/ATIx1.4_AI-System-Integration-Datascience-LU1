public interface IHolidayService
{
    Task<bool> IsHolidayAsync(DateTime date, string countryCode);
}