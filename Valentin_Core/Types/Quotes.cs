using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;

namespace Valentin_Core
{
    public class Quotes
    {
        #region Public Properties

        public Bitmap ImageFresco { get; set; }
        public Stream IOF { get; set; }
        public string PathToFile { get; set; }

        #endregion

        #region Private Fields

        private float fontSize;

        #endregion


        #region Constructors

        public Quotes(ParseMessage pm)
        {
            this.ImageFresco = CreateFrescoQuotes(pm);
            this.IOF = CreateIOF();
        }

        #endregion

        #region Private Methods

        private Bitmap CreateFrescoQuotes()
        {
           
            string text = "А текст мне самому придумать?";
            PointF location = new PointF(y: 70f, x: 50f);
            string imagepath = "..\\..\\..\\resources\\img\\fresco.jpg";
            using (Stream BitmapStream = File.Open(imagepath, FileMode.Open))
            {
                Image img = Image.FromStream(BitmapStream);
                var mBitmap = new Bitmap(img);

                //Bitmap bitmap = (Bitmap) Image.FromFile(imagepath);
                using (Graphics graphics = Graphics.FromImage(mBitmap))
                {
                    using (Font arialFont = new Font("Arial", 14))
                    {
                        graphics.DrawString(text, arialFont, Brushes.Black, location);
                    }
                }

                mBitmap.Save("..\\..\\..\\resources\\img\\Quotes\\quotes.bpm", ImageFormat.Bmp);
                return mBitmap;
            }
        }

        //TODO create a pillow(lockbits)
        private Bitmap CreateFrescoQuotes(ParseMessage pm)
        {
            string imagepath = "..\\..\\..\\resources\\img\\fresco.jpg";
            if (pm.HasMessage)
            {
                
                //RectangleF rectangle = new RectangleF(20f, 15f, 290f, 175f);//size of fresco rectangle text
                var text =  StringHelpers.QuotesMessage(pm.MessageTextList);
                var textlength = text.Length;
                if (textlength <= 10){fontSize = GetFontSize(TextSize.VerySmallTextSize);}
                else if (textlength > 10 && textlength <= 70) { fontSize = GetFontSize(TextSize.SmallTextSize); }
                else if (textlength > 70 && textlength <= 100) { fontSize = GetFontSize(TextSize.NormalTextSize); }
                else if (textlength > 100 && textlength <= 200) { fontSize = GetFontSize(TextSize.LargeTextSize); }
                else if (textlength > 200 && textlength <= 460) { fontSize = GetFontSize(TextSize.VeryLargeTextSize); }
                else if (textlength > 460 && textlength <= 620) { fontSize = GetFontSize(TextSize.VastTextSize); }
                else if (textlength > 620 && textlength <= 900) { fontSize = GetFontSize(TextSize.VeryVastTextSize); }
                else if (textlength > 900 && textlength <= 1300) { fontSize = GetFontSize(TextSize.EnormousLargeTextSize);}

                RectangleF rectangle = setRectangle(fontSize);




                using (Stream BitmapStream = File.Open(imagepath, FileMode.Open))
                {
                    Image img = Image.FromStream(BitmapStream);
                    var mBitmap = new Bitmap(img);
                    using (Graphics graphics = Graphics.FromImage(mBitmap))
                    {
                        using (Font comicSans = new Font("Comic Sans MS", fontSize))
                        {
                            graphics.DrawString(text, comicSans,Brushes.Black,rectangle);
                           
                        }
                    } 

                    mBitmap.Save("..\\..\\..\\resources\\img\\Quotes\\quotes.bpm", ImageFormat.Bmp);
                    return mBitmap;
                }
                
            }
            return CreateFrescoQuotes(); //rewrite this
        }

        private RectangleF setRectangle(float font)
        {
            switch (font)
            {
                case 5.5f:
                    return new RectangleF(20f,15f,300f,175f); 
                case 6.5f:
                    return new RectangleF(20f, 30f, 300f, 175f); //right
                case 7f:
                    return new RectangleF(25f, 35f, 300f, 175f); //right
                case 9f:
                    return new RectangleF(20f, 35f, 300f, 175f); //right 
                case 11f:
                    return new RectangleF(35f, 75f, 300f, 175f); //right
                case 12f:
                    return new RectangleF(35f, 80f, 300f, 175f); //right
                case 16f:
                    return new RectangleF(60f, 50f, 275f, 175f); //right
                case 24f:
                    return new RectangleF(95f, 100f, 300f, 175f); //right
                default:
                    throw new Exception();
            }

        }

        private Stream CreateIOF()
        {
            try
            {
                string imagepath = "..\\..\\..\\resources\\img\\Quotes\\quotes.bpm";
                PathToFile = imagepath;
                using var file = File.OpenRead(imagepath);
                return file;
            }
            catch
            {
                Console.WriteLine("some shit happend");
            }

            return IOF;
        }

        private float GetFontSize(TextSize textsize) =>
            textsize switch
            {
                TextSize.EnormousLargeTextSize =>5.5f,
                TextSize.VeryVastTextSize => 6.5f,
                TextSize.VastTextSize => 7f,
                TextSize.VeryLargeTextSize => 9f,
                TextSize.LargeTextSize => 11f,
                TextSize.NormalTextSize => 12f,
                TextSize.SmallTextSize => 16f,
                TextSize.VerySmallTextSize => 24f,
                _ => throw new Exception("how is that even happend?")
            };




        #endregion





    }
}
