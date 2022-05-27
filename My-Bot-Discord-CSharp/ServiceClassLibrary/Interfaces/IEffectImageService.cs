using System.Drawing;
using System.Drawing.Imaging;

namespace ServiceClassLibrary.Interfaces
{
    public interface IEffectImageService
    {
        Bitmap DrawAsGrayscale(Bitmap sourceImage);
        Bitmap DrawAsNegative(Bitmap sourceImage);
        Bitmap DrawAsSepiaTone(Bitmap sourceImage);
        Bitmap DrawWithTransparency(Bitmap sourceImage);
        Bitmap GetArgbCopy(Bitmap sourceImage);
        Bitmap DrawBlackAndWhite(Bitmap sourceImage);
        void DrawFilp(Bitmap sourceImage, string path);
    }
}