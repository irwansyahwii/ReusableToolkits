namespace ReusableToolkits.Interfaces
{
  public interface IMapBetweenTwoIntervals
  {
    double MapToDestinationIntervalValue( float sourceValue
      , float sourceValueMinimum
      , float sourceValueMaximum
      , float destinationValueMinimum
      , float destinationValueMaximum );
  }
}