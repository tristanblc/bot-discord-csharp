using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModuleBotClassLibrary.Services;
using ServiceClassLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotApi.ServiceClassLibrary
{
    [TestClass]
    public class ImageServiceTest
    {


        private IImageService imageService { get; set; }

        public ImageServiceTest()
        {
            imageService = new ImageService();

        }

        [TestMethod]
        public void TestSaveImage()
        {

            var urlImageTest = "https://volcan.puy-de-dome.fr/fileadmin/_processed_/csm_Sommet-du-puy-de-Dome-J_4760626cb6.jpg";
            var filename = "puy-de-dome.jpg";

            var path = Path.Combine(Directory.GetCurrentDirectory(), "images");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);


            var pathFile = Path.Join(path, filename);

            imageService.SaveImage(urlImageTest, filename);



            int fCount = Directory.GetFiles(pathFile, "*", SearchOption.AllDirectories).Length;


            Assert.IsNotNull(fCount);

            Assert.AreEqual(1, fCount);

        }

        [TestMethod]
        public void TestDeleteImage()
        {
            var filename = "puy-de-dome.jpg";

            var path = Path.Combine(Directory.GetCurrentDirectory(), "images");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);


            var pathFile = Path.Join(path, filename);

            imageService.DeleteImage(pathFile);



            int fCount = Directory.GetFiles(pathFile, "*", SearchOption.AllDirectories).Length;


            Assert.IsNotNull(fCount);

            Assert.AreEqual(0, fCount);

        }
    }
}
