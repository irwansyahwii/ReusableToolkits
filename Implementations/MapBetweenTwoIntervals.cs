using ReusableToolkits.Interfaces;

namespace ReusableToolkits.Implementations
{
  public class MapBetweenTwoIntervals : IMapBetweenTwoIntervals
  {
    public double MapToDestinationIntervalValue( float sourceValue, float sourceValueMinimum, float sourceValueMaximum,
      float destinationValueMinimum, float destinationValueMaximum )
    {
      if( sourceValue >= sourceValueMaximum ) return destinationValueMaximum;
      if( sourceValue <= sourceValueMinimum ) return destinationValueMinimum;
      return destinationValueMinimum + ( sourceValue - sourceValueMinimum ) / ( sourceValueMaximum - sourceValueMinimum )
             * ( destinationValueMaximum - destinationValueMinimum );
    }
  }
}