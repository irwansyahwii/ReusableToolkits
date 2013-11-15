using System.IO;
using ReusableToolkits.Interfaces;

namespace ReusableToolkits.Implementations
{
  public class DefaultFileSystem : IFileSystem
  {
    #region IFileSystem Members

    public bool IsExists(string filePath)
    {
      return File.Exists(filePath);
    }

    public string ReadAllText(string filePath)
    {
      if (IsExists(filePath))
      {
        return File.ReadAllText(filePath);
      }
      return string.Empty;
    }

    #endregion
  }
}