using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReusableToolkits.Interfaces
{
  public interface IUShortToBitArray
  {
    BitArray GetBitArray(ushort sourceValue);
  }

  public class DefaultUShortToBitArray : IUShortToBitArray
  {
    private int GetFromBits( ushort bits, int offset )
    {
      return ( bits >> ( offset ) ) & 0xF;
    }

    public BitArray GetBitArray(ushort sourceValue)
    {
      //BitArray result = new BitArray( 16 );
      //for( int i = 0; i < result.Length; i++ )
      //{
      //  result.Set( i, GetFromBits( sourceValue, i ) == 1 );
      //}

      //return result;      

      BitArray result = new BitArray(new [] {(int)sourceValue});

      return result;
    }
  }
}
