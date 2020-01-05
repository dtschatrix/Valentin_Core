
using System;
using System.Text.RegularExpressions;

namespace Valentin_Core
{
    public class ParseMessage
    {
        #region Public Properties

        public Regex CurrentRegex { get; set; }

        public string MessageText { get; set; }

        public string UserCommand { get; set; }

        public string BotCommand { get; set; }
        #endregion
        //TODO refactor this for any dices and cubes and commands ofc
        public ParseMessage()
        {
          this.CurrentRegex = new Regex(@"^\/d20[a-zA-Z0-9_ ]*");   
        }

        //TODO refactor this shitty types 
        public void ParseUserMessage(string recevied)
        {
            String[] separator = {" ", ",", ", "};

            string tempString = recevied.Split(separator, 0, StringSplitOptions.RemoveEmptyEntries).ToString();

            this.BotCommand = tempString[0].ToString();

        }
        
    }
}
