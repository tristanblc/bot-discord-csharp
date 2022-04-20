using System.Drawing;

namespace EffectClassLibrary
{
    public class Effect
    {
   
        public Effect()
        {
           
        }



        public Bitmap SetToGray(Bitmap bitmap)
        {
            for(var x = 0; x < bitmap.Width; x++){
                for (var y = 0; y< bitmap.Width; y++)
                {
                    var color = bitmap.GetPixel(x, y);
                    if (color != null)
                    {
                        var red = color.R;
                        var green = color.G;
                        var blue = color.B;


                        var gray = (red + green + blue) / 3;


                        var new_color = Color.FromArgb(gray);
                        bitmap.SetPixel(x, y,new_color);

                    }


                }
            }

            return bitmap;


        }



        public Bitmap SetToBlackAndWhite(Bitmap bitmap)
        {

            for (var x = 0; x < bitmap.Width; x++)
            {
                for (var y = 0; y < bitmap.Width; y++)
                {
                    var color = bitmap.GetPixel(x, y);
                    if (color != null)
                    {
                        var red = color.R;
                        var green = color.G;
                        var blue = color.B;


                        var gray = (red + green + blue) / 3;


                        if(gray < 126)
                             bitmap.SetPixel(x, y, Color.Black);
                        else
                            bitmap.SetPixel(x, y, Color.White);

                    }


                }
            }


            return bitmap;
        }




        public Bitmap  PermutColor(Bitmap bitmap)
        {
            for (var x = 0; x < bitmap.Width; x++)
            {
                for (var y = 0; y < bitmap.Width; y++)
                {
                    var color = bitmap.GetPixel(x, y);
                    if (color != null)
                    {
                        var red = color.R;
                        var green = color.G;
                        var blue = color.B;


                        bitmap.SetPixel(x, y, Color.FromArgb(blue, green, red));

                    }


                }
            }


            return bitmap;
        }


    }
}