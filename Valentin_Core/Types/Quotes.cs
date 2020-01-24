using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
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
            PointF location = new PointF(y: 70f, x: 40f);
            string imagepath = "..\\..\\..\\resources\\img\\fresco.jpg";
            using (Stream BitmapStream = File.Open(imagepath, FileMode.Open))
            {
                Image img = Image.FromStream(BitmapStream);
                var mBitmap = new Bitmap(img);

                //Bitmap bitmap = (Bitmap) Image.FromFile(imagepath);
                using (Graphics graphics = Graphics.FromImage(mBitmap))
                {
                    using (Font arialFont = new Font("Arial", 12))
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
                StringBuilder part = new StringBuilder();
                List<string> text = new List<string>();
                foreach (var token in pm.MessageTextList)
                {
                    if (pm.MessageTextList.LastOrDefault().Equals(token))
                    {
                        part.Append(token);
                        text.Add(part.ToString());
                        continue;
                    }

                    if (part.Length < 35)
                    {
                        part.Append(token + " ");
                        continue;
                    }
                    if (part.Length >= 35)
                    {
                        text.Add(part.ToString());
                        part.Clear();
                        if (token != null)
                        {
                            part.Append(token + " ");
                        }
                    }
                }



                using (Stream BitmapStream = File.Open(imagepath, FileMode.Open))
                {

                    PointF location = new PointF(y: 30f, x: 20f);
                    Image img = Image.FromStream(BitmapStream);
                    var mBitmap = new Bitmap(img);

                    //Bitmap bitmap = (Bitmap) Image.FromFile(imagepath);
                    using (Graphics graphics = Graphics.FromImage(mBitmap))
                    {
                        using (Font arialFont = new Font("Arial", 12))
                        {
                            foreach (var token in text)
                            {
                                graphics.DrawString(token, arialFont, Brushes.Black, location);
                                location.Y += 20f;
                            }

                        }
                    }

                    mBitmap.Save("..\\..\\..\\resources\\img\\Quotes\\quotes.bpm", ImageFormat.Bmp);
                    return mBitmap;
                }
                
            }
            else
            {
                return CreateFrescoQuotes(); //rewrite this
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



       
        #endregion





    }
}
