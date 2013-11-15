using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReusableToolkits.Interfaces
{
  public interface IOSVersionInfo
  {
    int BuildVersion { get; }
    string Edition { get; }
    int MajorVersion { get; }
    int MinorVersion { get; }
    string Name { get; }
    ReusableToolkits.Implementations.JCS.OSVersionInfo.SoftwareArchitecture OSBits { get; }
    ReusableToolkits.Implementations.JCS.OSVersionInfo.ProcessorArchitecture ProcessorBits { get; }
    ReusableToolkits.Implementations.JCS.OSVersionInfo.SoftwareArchitecture ProgramBits { get; }
    int RevisionVersion { get; }
    Version Version { get; }
  }
}
