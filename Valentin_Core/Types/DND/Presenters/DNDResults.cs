using System;
using System.Text;
using Telegram.Bot.Types;

namespace Valentin_Core
{
    public class DNDResults
    {
        #region Public Properties

        public string TextMessage { get; set; }

        #endregion
        
        #region Public Methods


        public void SomeResult(ParseMessage pm, DND cube)
        {
           //extremly bad practice should think about it
            if (cube is D4){cube.SetCubeResult();}
            else if(cube is D8){ cube.SetCubeResult(); }
            else if (cube is D10) { cube.SetCubeResult(); }
            else if (cube is D12) { cube.SetCubeResult(); }
            else if (cube is D20) { cube.SetCubeResult(); }
            else if (cube is D100) { cube.SetCubeResult(); }
            //TODO think about the message huh?
            if (pm.HasArgument && !pm.HasMessage)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Кидаю d" + $"{cube.CubeType}" + " " + $"{pm.Argument} раз"); 
                TextMessage = sb.ToString();
            }

            if (pm.HasMessage && !pm.HasArgument)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Кидаю d" + $"{cube.CubeType}" + " " + $"с надеждой, что {pm.MessageText}");
                TextMessage = sb.ToString();
            }

            if (!pm.HasMessage && !pm.HasArgument)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Кидаю d" + $"{cube.CubeType}");
                TextMessage = sb.ToString();
            }

            if (pm.HasMessage && pm.HasArgument)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Кидаю d" + $"{cube.CubeType}" + " " + $"{pm.Argument} раз " + $"c надеждой что {pm.MessageText}");
                TextMessage = sb.ToString();
            }

           
        }

        public void FinalResult(ParseMessage pm, DND cube)
        {
            StringBuilder sb = new StringBuilder();
            if (cube is D20){d20Result(pm, cube);}
            else {dResult(pm, cube);}
            pm.CommandExecuted = true;
            

        }
        #endregion

        #region Private Methods

        private void d20Result(ParseMessage pm, DND cube)
        {
            StringBuilder sb = new StringBuilder();
            if (pm.HasMessage && pm.HasArgument)
            {
                var listValues = DndCaluclation.GetValues(cube, pm.Argument);
                sb.Append($"{pm.MessageText}" + " с такими результатами:\n");
                foreach (var value in listValues)
                {
                    sb.Append(value + " ");
                }
                TextMessage = sb.ToString();
            }
            else if (!pm.HasMessage && pm.HasArgument)
            {
                var listValues = DndCaluclation.GetValues(cube, pm.Argument);
                sb.Append(" с такими результатами:\n");
                foreach (var value in listValues)
                {
                    sb.Append(value + " ");
                }

                TextMessage = sb.ToString();
            }

            else if (pm.HasMessage && !pm.HasArgument)
            {
                switch (cube.CubeResult)
                {
                    case int n when (n == 20):
                        sb.Append($"Критический разрыв ебала на {cube.CubeResult}" + "," + $" вы точно: {pm.MessageText} ");
                        TextMessage = sb.ToString();
                        break;
                    case int n when (n == 1):
                        sb.Append($"Вы обосрались конкретно тупа на {cube.CubeResult}" + "," + " пошёл нахуй!'");
                        TextMessage = sb.ToString();
                        break;
                    case int n when (n < 7):
                        sb.Append($"Вы не смогли {pm.MessageText}," + $" куб показывает {cube.CubeResult}");
                        TextMessage = sb.ToString();
                        break;
                    case int n when (n >= 7 && n < 11):
                        sb.Append($"Не могу точно сказать смогли ли вы {pm.MessageText}," + $" куб показывает {cube.CubeResult}, кинь ещё раз");
                        TextMessage = sb.ToString();
                        break;
                    case int n when (n >= 11 && n <= 19):
                        sb.Append($"Вы смогли {pm.MessageText}," + $" куб показывает: {cube.CubeResult}");
                        TextMessage = sb.ToString();
                        break;
                }
                
            }
            else if (!pm.HasMessage && !pm.HasArgument)
            {
                sb.Append($"Куб показывает {cube.CubeResult}");
                TextMessage = sb.ToString();
            }
            
        }

        private void dResult(ParseMessage pm, DND cube)
        {
            StringBuilder sb = new StringBuilder();
            if (pm.HasArgument && pm.HasMessage)
            {
                var listValues = DndCaluclation.GetValues(cube, pm.Argument);
                sb.Append($"{pm.MessageText}" + " с такими результатами:\n");
                foreach (var value in listValues)
                {
                    sb.Append(value + " ");
                }
                TextMessage = sb.ToString();
            }
            else if (!pm.HasArgument && pm.HasMessage || !pm.HasMessage)
            {
                sb.Append($"Куб показывает {cube.CubeResult}");
                TextMessage = sb.ToString(); ;
            }
            
        }
        #endregion



    }
}