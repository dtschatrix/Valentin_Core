
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Valentin_Core
{
    public class ParseMessage
    {
        #region Private Constant Fields

        //All commands.
        private List<string> existCommandsBot = new List<string>()
            {"/paste", "/d20", "/d8", "/d10", "/d100", "/d4", "/d12", "/otec", "Compod", "/help", "/test", "/fresco"};

        #endregion

        #region Public Properties

        public string MessageText { get; set; }

        public string BotCommand { get; set; }

        public List<string> SeparatedMessages { get; }
        public List<string> MessageTextList { get; set; }

        public bool HasArgument { get; set; }
        public bool HasMessage { get; set; }

        public List<string> ExistBotCommands { get; }
        public bool CommandExecuted { get; set; }
        public int Argument { get; set; }

        #endregion

        #region Constructors

        public ParseMessage()
        {
            this.ExistBotCommands = existCommandsBot;
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
            this.MessageText = CollectMessage(received); // and also set argument(!bad!)
            this.BotCommand = SeparatedMessages[0];
            this.MessageTextList = MessageTextToList(this.MessageText);
        }

        #endregion

        #region Private Methods

        //TODO probably move to string helpers(?)
        //Rewrite because command can be only
        private string CollectMessage(string received)
        {
            if (received.Length != 0)
            {
                String[] separator = {" "};
                StringBuilder sb = new StringBuilder();
                var tempString = received.Split(separator, 4096,
                    StringSplitOptions
                        .RemoveEmptyEntries); // 4096 max length of message on Telegram. according to github conversation
                foreach (var substring in tempString)
                {
                    SeparatedMessages.Add(substring);
                    if (substring != SeparatedMessages[0])
                    {
                        HasMessage = true;
                        // I really don't like this, but zero idea how to make it better
                        //UPD did it, but still have doubt :\ 
                        bool successString = int.TryParse(substring, out int stringresult);

                        if (successString && substring == SeparatedMessages[1])
                        {
                            HasArgument = true;
                            Argument = stringresult;
                            continue;
                        }

                        sb.Append(substring + " ");
                    }

                }

                var formatMessage = HasMessage ? StringHelpers.ItalicMessage(sb.ToString()) : sb.ToString();

                return formatMessage;
            }

            throw new Exception();
        }

       
        private List<string> MessageTextToList(string message)
        {
            String[] separator = {" "};
            List<string> result = new List<string>();
            var tempString = message.Split(separator, 4096,
                StringSplitOptions
                    .RemoveEmptyEntries); // 4096 max length of message on Telegram. according to github conversation
            foreach (var substring in tempString)
            {
                result.Add(substring);
            }

            return result;
        }

        #endregion

    }
}
