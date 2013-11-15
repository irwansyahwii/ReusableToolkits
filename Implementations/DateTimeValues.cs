using System;

namespace ReusableToolkits.Implementations
{
  public class DateTimeValues
  {
    public static readonly DateTime EmptyValue = DateTime.MinValue;

    public static DateTime ToDateTime(DateTime? lastupdate)
    {
      if (lastupdate.HasValue)
      {
        return lastupdate.Value;
      }
      else
      {
        return EmptyValue;
      }
    }

    public static DateTime GetValue(DateTime? nullableValue)
    {
      if (nullableValue.HasValue)
      {
        return nullableValue.Value;
      }
      else
      {
        return EmptyValue;
      }
    }
  }
}