using ServiceClassLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceClassLibrary.Services
{
    public class EffectImageService : IEffectImageService
    {

        private Bitmap ApplyColorMatrix(Bitmap sourceImage, ColorMatrix colorMatrix)
        {
            Bitmap bmp32BppSource = GetArgbCopy(sourceImage);
            Bitmap bmp32BppDest = new Bitmap(bmp32BppSource.Width, bmp32BppSource.Height, PixelFormat.Format32bppArgb);


            using (Graphics graphics = Graphics.FromImage(bmp32BppDest))
            {
                ImageAttributes bmpAttributes = new ImageAttributes();
                bmpAttributes.SetColorMatrix(colorMatrix);

                graphics.DrawImage(bmp32BppSource, new Rectangle(0, 0, bmp32BppSource.Width, bmp32BppSource.Height),
                                    0, 0, bmp32BppSource.Width, bmp32BppSource.Height, GraphicsUnit.Pixel, bmpAttributes);


            }


            bmp32BppSource.Dispose();


            return bmp32BppDest;
        }


        public Bitmap GetArgbCopy(Bitmap sourceImage)
        {
            Bitmap bmpNew = sourceImage;

            using (Graphics graphics = Graphics.FromImage(sourceImage))
            {
                graphics.DrawImage(sourceImage, new Rectangle(0, 0, bmpNew.Width, bmpNew.Height), new Rectangle(0, 0, bmpNew.Width, bmpNew.Height), GraphicsUnit.Pixel);
                graphics.Flush();
            }


            return bmpNew;
        }

        public Bitmap DrawWithTransparency(Bitmap sourceImage)
        {
            ColorMatrix colorMatrix = new ColorMatrix(new float[][]
                                {
                            new float[]{1, 0, 0, 0, 0},
                            new float[]{0, 1, 0, 0, 0},
                            new float[]{0, 0, 1, 0, 0},
                            new float[]{0, 0, 0, 0.3f, 0},
                            new float[]{0, 0, 0, 0, 1}
                                });


            return ApplyColorMatrix(sourceImage, colorMatrix);
        }
        public Bitmap DrawAsGrayscale(Bitmap sourceImage)
        {
            ColorMatrix colorMatrix = new ColorMatrix(new float[][]
                                {
                            new float[]{.3f, .3f, .3f, 0, 0},
                            new float[]{.59f, .59f, .59f, 0, 0},
                            new float[]{.11f, .11f, .11f, 0, 0},
                            new float[]{0, 0, 0, 1, 0},
                            new float[]{0, 0, 0, 0, 1}
                                });


            return ApplyColorMatrix(sourceImage, colorMatrix);
        }
        public Bitmap DrawAsSepiaTone(Bitmap sourceImage)
        {
            ColorMatrix colorMatrix = new ColorMatrix(new float[][]
                       {
                        new float[]{.393f, .349f, .272f, 0, 0},
                        new float[]{.769f, .686f, .534f, 0, 0},
                        new float[]{.189f, .168f, .131f, 0, 0},
                        new float[]{0, 0, 0, 1, 0},
                        new float[]{0, 0, 0, 0, 1}
                       });


            return ApplyColorMatrix(sourceImage, colorMatrix);
        }

        public Bitmap DrawAsNegative(Bitmap sourceImage)
        {
            ColorMatrix colorMatrix = new ColorMatrix(new float[][]
                           {
                            new float[]{-1, 0, 0, 0, 0},
                            new float[]{0, -1, 0, 0, 0},
                            new float[]{0, 0, -1, 0, 0},
                            new float[]{0, 0, 0, 1, 0},
                            new float[]{1, 1, 1, 1, 1}
                           });


            return ApplyColorMatrix(sourceImage, colorMatrix);
        }

      

    }
}
