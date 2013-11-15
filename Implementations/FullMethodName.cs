using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReusableToolkits.Interfaces;

namespace ReusableToolkits.Implementations
{
  public class FullMethodName : IFullMethodName
  {
    protected string fullMethodName = string.Empty;

    public FullMethodName( GuardUtility guardUtility, string shortMethodName )
    {
      fullMethodName = guardUtility.GetFullMethodName( shortMethodName );
    }

    #region IFullMethodName Members

    public string GetFullMethodName()
    {
      return fullMethodName;
    }

    #endregion
  }
}
