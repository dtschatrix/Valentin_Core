
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Valentin_Core
{
    public class ParseMessage
    {
        #region Public Properties

        public Regex CurrentRegex { get; set; }

        public string MessageText { get; set; }

        public string BotCommand { get; set; }

        public List<string> SeparatedMessages { get; }
        #endregion
        //TODO refactor this for any dices and cubes and commands ofc
        //TODO rewrite regex

        #region Constructors
        public ParseMessage()
        {
            this.CurrentRegex = new Regex(@"^\/d20[a-zA-Z0-9_ ]*");
            this.SeparatedMessages = new List<string>();
        }


        #endregion

        #region Public Methods
        //TODO refactor this shitty types 
        /// <summary>
        /// Getting message right via parsing all parameters
        /// </summary>
        /// <param name="received">raw string from message"/>/></param>
        public void ParseUserMessage(string received)
        {
            this.MessageText = CollectMessage(received);
            this.BotCommand = SeparatedMessages[0];
        }

        #endregion

        #region Private Methods

        

       
        //TODO probably move to string helpers(?)
        private string CollectMessage(string received)
        {
            String[] separator = { " ", ",", ", " };
            StringBuilder sb = new StringBuilder();
            var tempString = received.Split(separator, 4096, StringSplitOptions.RemoveEmptyEntries); // 4096 max length of message on Telegram. according to github conversation
            foreach (var substring in tempString)
            {
                SeparatedMessages.Add(substring);
                if (substring != SeparatedMessages[0])
                {
                    sb.Append(substring + " ");
                }
            }

            string formatMessage = StringHelpers.CodeMessage(sb.ToString());
            return formatMessage;




        }
        #endregion

    }

}
