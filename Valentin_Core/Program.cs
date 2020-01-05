using System;
using System.Threading;
using Microsoft.VisualBasic.CompilerServices;
using Telegram.Bot;
using Telegram.Bot.Args;


namespace Valentin_Core
{
    class Program
    {
        static ITelegramBotClient botClient;
        static void Main()
        {
            var bot = new TelegramBotClient("BOT API");
            botClient = bot;
            bot.OnMessage += Bot_OnMessage;
            bot.StartReceiving();
            Thread.Sleep(Int32.MaxValue);
        }


       static async void Bot_OnMessage(object sender, MessageEventArgs e)
        { 
            ParseMessage pm = new ParseMessage();
           
            if (e.Message.Text == "/paste"  && e.Message.Chat.Id == -332067211)
            {
                PasteDotaThread pdt = new PasteDotaThread();
                pdt.GetPasteFromNotepad();
                await botClient.SendTextMessageAsync(chatId: e.Message.Chat,
                   text: pdt.MessageText, replyToMessageId:e.Message.MessageId);
                Console.WriteLine($"chat ID: {e.Message.Chat.Id} ");
            }

            if (pm.CurrentRegex.IsMatch(e.Message.Text))
            {
                pm.ParseUserMessage(e.Message.Text);
                DND dnd = new DND();
                dnd.SetCubeType(CubeTypeNumber.d20);
                dnd.GetCubeResult(dnd);
                dnd.SomeResult("test");
                await botClient.SendTextMessageAsync(chatId: e.Message.Chat, text:
                    dnd.MessageText, replyToMessageId:e.Message.MessageId);
                Thread.Sleep(1000);
                dnd.FinalResult(dnd, "test");
                await botClient.SendTextMessageAsync(chatId: e.Message.Chat, text:
                    dnd.MessageText, replyToMessageId: e.Message.MessageId);
            }

        }
    }
}
