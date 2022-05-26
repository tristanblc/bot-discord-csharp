using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceClassLibrary.Interfaces
{
    public interface IImageService
    {
        void SaveImage(string imageUrl, string filename);
        void SaveImage(Bitmap image, string path);
        void DeleteImage(string filename);

    }
}
