using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ReusableToolkits.Implementations
{
  public static class WpfImageExtensions
  {
    
    public static BitmapImage ToBitmapImage(this Bitmap image)
    {
      
      
      //using( MemoryStream memory = new MemoryStream() )
      //{        
      //  image.Save( memory, ImageFormat.Bmp );
      //  memory.Position = 0;
      //  BitmapImage bitmapImage = new BitmapImage();
      //  //bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
      //  bitmapImage.BeginInit();
      //  bitmapImage.StreamSource = memory;        
      //  bitmapImage.EndInit();
        
      //  return bitmapImage;
      //}
       
       
      MemoryStream ms = new MemoryStream();
      
      image.Save(ms, ImageFormat.Bmp);
      //ms.Seek(0, SeekOrigin.Begin);
      BitmapImage bi = new BitmapImage();


      
      bi.BeginInit();
      //bi.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
      bi.CacheOption = BitmapCacheOption.OnLoad;

      bi.StreamSource = ms;
      bi.EndInit();
      if( bi.CanFreeze )
      {
        bi.Freeze();
      }

      return bi;
    }

  }

  public static class ImageExtensions
  {
    /// <summary>
    /// Converts a <see cref="System.Drawing.Image"> into a <see cref="BitmapSource">.
    /// </see></see></summary>
    /// <param name="source">The source image./// <returns>A <see cref="BitmapSource"> containing the same image.</see></returns>
    public static BitmapSource ToBitmapSource( this System.Drawing.Image source )
    {
      System.Drawing.Bitmap bitmap = source as System.Drawing.Bitmap;
      if( bitmap != null ) return bitmap.ToBitmapSource();

      bitmap = new System.Drawing.Bitmap( source );
      try
      {
        return bitmap.ToBitmapSource();
      }
      finally
      {
        bitmap.Dispose();
      }
    }

    /// <summary>
    /// Converts a <see cref="System.Drawing.Bitmap"> into a <see cref="BitmapSource">.
    /// </see></see></summary>
    /// <param name="source">The source bitmap./// <remarks>
    /// Uses GDI to do the conversion. Hence the call to the marshalled DeleteObject.
    /// </remarks>
    /// <returns>A <see cref="BitmapSource"> containing the same image.</see></returns>
    public static BitmapSource ToBitmapSource( this System.Drawing.Bitmap source )
    {

      var hBitmap = source.GetHbitmap();

      try
      {        
        return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
            hBitmap,
            IntPtr.Zero,
            Int32Rect.Empty,
            BitmapSizeOptions.FromWidthAndHeight( source.Width, source.Height ) );
            //BitmapSizeOptions.FromEmptyOptions() );
      }
      catch( Win32Exception )
      {
        return null;
      }
      finally
      {
        NativeMethods.DeleteObject( hBitmap );
        source.Dispose();
      }
    }
  }

  public class NativeMethods
  {
    [DllImport( "gdi32.dll" )]
    public static extern bool DeleteObject( IntPtr hObject );

  }

}
