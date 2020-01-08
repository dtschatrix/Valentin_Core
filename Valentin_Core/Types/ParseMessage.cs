
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

        public bool HasArgument { get; set; } = false;
        public bool HasMessage { get; set; } = false;

        public bool CommandExecuted { get; set; }
        public int Argument { get; set; }
        #endregion
        //TODO refactor this for any dices and cubes and commands ofc

        #region Private Fields

        List<string> regList = new List<string>();

        #endregion

        #region Constructors
        public ParseMessage()
        {
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
            this.CurrentRegex = GetRegex(regList,received);
            
        }



        #endregion

        #region Private Methods
        private List<string> GetListOfRegex()
        {
            List<string> lregex = new List<string>();
            lregex.Add(@"^\/d4{1}(?!\s\/)(?!\/)[a-zA-Z0-9_ @]*");
            lregex.Add(@"^\/d8{1}(?!\s\/)(?!\/)[a-zA-Z0-9_ @]*");
            lregex.Add(@"^\/d10{1}(?!\s\/)(?!\/)[a-zA-Z0-9_ @]*");
            lregex.Add(@"^\/d12{1}(?!\s\/)(?!\/)[a-zA-Z0-9_ @]*");
            lregex.Add(@"^\/d20{1}(?!\s\/)(?!\/)[a-zA-Z0-9_ @]*");
            lregex.Add(@"^\/d100{1}(?!\s\/)(?!\/)[a-zA-Z0-9_ @]*");
            lregex.Add(@"^(\/paste)");
            return lregex;
        }

        private Regex GetRegex(List<string> lrgex, string received)
        {
            if (lrgex.Count == 0)
            {
                lrgex = GetListOfRegex();
            }

            foreach (var match in lrgex)
            {
                if (Regex.IsMatch(received, match))
                {
                    return new Regex(match);
                }
            }
            throw new Exception();
        }


        //TODO probably move to string helpers(?)
        //Rewrite because command can be only
        private string CollectMessage(string received)
        {
            if (received.Length != 0)
            {
                String[] separator = {" ", ",", ", "};
                StringBuilder sb = new StringBuilder();
                var tempString = received.Split(separator, 4096,
                        StringSplitOptions.RemoveEmptyEntries); // 4096 max length of message on Telegram. according to github conversation
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
                string formatMessage = "";
                if (HasMessage)
                {
                     formatMessage = StringHelpers.ItalicMessage(sb.ToString());
                }
                else
                {
                    formatMessage = sb.ToString();
                }

                return formatMessage;
            }

            throw new Exception();
        }
        #endregion

    }

}
