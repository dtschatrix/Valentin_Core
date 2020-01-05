using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Types;

namespace Valentin_Core
{
    public enum CubeTypeNumber
    {
        d4,
        d8,
        d10,
        d20,
        d100
    }
    public class DND
    {
        #region Public Properties

        public int CubeResult { get; set; }

        public string MessageText { get; set; }

        public int CubeType { get; set; }
        #endregion

        #region Private Fields




        #endregion

        #region Public Methods

        public void SetCubeType(CubeTypeNumber cube)
        {
            switch (cube)
            {
                case CubeTypeNumber.d4:
                    CubeType = 4;
                    break;
                case CubeTypeNumber.d8:
                    CubeType = 8;
                    break;
                case CubeTypeNumber.d10:
                    CubeType = 10;
                    break;
                case CubeTypeNumber.d20:
                    CubeType = 20;
                    break;
                case CubeTypeNumber.d100:
                    CubeType = 100;
                    break;
               
            }
            
        }

        public void GetCubeResult(DND dndcube)
        {
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            switch (dndcube.CubeType)
            {
                case 4:
                    dndcube.CubeResult = rand.Next(1, 4);
                    break;
                case 8:
                    dndcube.CubeResult = rand.Next(1, 8);
                    break;
                case 10:
                    dndcube.CubeResult = rand.Next(1, 10);
                    break;
                case 20:
                    dndcube.CubeResult = rand.Next(1, 20);
                    break;
                case 100:
                    dndcube.CubeResult = rand.Next(1, 20);
                    break;
            }


        }

        public string SomeResult(string receviedmessage)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Кидаю d" + $"{CubeType}" + $" и думаю, что у меня получилось {receviedmessage}");
            MessageText = sb.ToString();
            return MessageText;
        }

        public string FinalResult(DND dnd, string receviedmessage)
        {
            StringBuilder sb = new StringBuilder();
            if (dnd.CubeType == dnd.CubeResult)
            {
                sb.Append($"Критический разрыв ебала на {dnd.CubeResult}, вы точно: {receviedmessage} ");
                MessageText = sb.ToString();
            }
            else if (dnd.CubeResult == 1)
            {
                sb.Append($"Вы обосрались конкретно тупа на {dnd.CubeResult}, пошёл нахуй!'");
            }

            else if (dnd.CubeType == 20)
            {
            
                switch (dnd.CubeResult)
                    {
                        case int n when (n < 6):
                            sb.Append($"Вы не смогли {receviedmessage} куб показывает {dnd.CubeResult}");
                            MessageText = sb.ToString();
                            break;
                        case int n when (n >= 6 && n < 11):
                            sb.Append($"Не могу точно сказать смогли ли вы {receviedmessage}, куб показывает {dnd.CubeResult}, кинь ещё раз");
                            MessageText = sb.ToString();
                            break;
                        case int n when (n >=11 && n <=19):
                            sb.Append($"Вы смогли {receviedmessage} куб показывает {dnd.CubeResult}");
                            MessageText = sb.ToString();
                            break;
                    }
            }
            else
            {
                sb.Append($"Куб показывает {dnd.CubeResult}");
                MessageText = sb.ToString();
            }


            return MessageText;

        }
        #endregion


    }
}
