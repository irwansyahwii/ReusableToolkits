namespace ReusableToolkits.Interfaces
{
  public interface IFileSystem
  {
    bool IsExists(string filePath);
    string ReadAllText(string filePath);
  }
}