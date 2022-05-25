using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceClassLibrary.Interfaces
{
    public interface IImageService
    {
        void SaveImage(string imageUrl, string filename);
        void DeleteImage(string filename);

    }
}
