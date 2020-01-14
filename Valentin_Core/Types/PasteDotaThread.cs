using System;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;

namespace Valentin_Core
{
    public class PasteDotaThread
    {
        #region Private Fields

        private const char CR = '\r';
        private const char LF = '\n';
        private const char NULL = (char)0;

        #endregion

        #region Public Properties

        public string MessageText { get; private set; }


        #endregion

        #region Public Methods
        //TODO scraping from dotathread posts

        //TODO rewrite this
        public void GetPasteFromNotepad(ParseMessage pm)
        {
            Random rand = new Random(Guid.NewGuid().GetHashCode()); // impressive random :)
            string pathToFile = "D:\\projects\\Valentin_Core\\Valentin_Core\\resources\\text\\notepadPaste.txt"; // you can easily read from your file
            using (StreamReader reader = new StreamReader(pathToFile))
            {
                var lines = GetLinesFromText(reader.BaseStream);
                var certainLine = rand.Next(0, (int)lines); // that's how you shouldn't
                var paste = File.ReadAllLines(pathToFile, Encoding.UTF8).Skip(certainLine - 1).Take(1).First();
                MessageText = paste;
                pm.CommandExecuted = true;
            }

        }

        public void GetPasteFromOtecNotepad(ParseMessage pm)
        {
            Random rand = new Random(Guid.NewGuid().GetHashCode()); // impressive random :)
            string pathToFile = @"D:\projects\Valentin_Core\Valentin_Core\resources\text\otecPaste.txt"; // you can easily read from your file
            using (StreamReader reader = new StreamReader(pathToFile))
            {
                var lines = GetLinesFromText(reader.BaseStream);
                var certainLine = rand.Next(0, (int)lines); // that's how you shouldn't
                var paste = File.ReadAllLines(pathToFile, Encoding.UTF8).Skip(certainLine - 1).Take(1).First();
                MessageText = paste;
                pm.CommandExecuted = true;
            }

        }

        #endregion

        #region Private Methods
        //TODO probably rewrite and move to stringhelpers.cs(?)
        private long GetLinesFromText(Stream stream)
        {
            long linecount = 0L;

            var byteBuffer = new byte[1024 * 1024];

            var prevChar = NULL;
            var pendingTermination = false;

            int bytesRead;

            while ((bytesRead = stream.Read(byteBuffer, 0, byteBuffer.Length)) > 0)
            {
                for (int i = 0; i < bytesRead; i++)
                {
                    var currentChar = (char)byteBuffer[i];
                    switch (currentChar)
                    {
                        case NULL:
                        case LF when prevChar == CR:
                            continue;
                        case CR:
                        case LF when prevChar != CR:
                            linecount++;
                            pendingTermination = false;
                            break;
                        default:
                            if (!pendingTermination) pendingTermination = true;
                            break;
                    }

                    prevChar = currentChar;



                }

            }

            if (pendingTermination) linecount++;
            return linecount;
        }


        #endregion

    }
}
