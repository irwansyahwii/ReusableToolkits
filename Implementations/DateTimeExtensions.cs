using System;

namespace ReusableToolkits.Implementations
{
  public static class DateTimeExtensions
  {
    public static bool BetweenDates(this DateTime theDate, DateTime startDate, DateTime endDate)
    {
      return (theDate >= startDate) && (theDate <= endDate);
    }
  }
}