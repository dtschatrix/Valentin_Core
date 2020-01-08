using System;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;


namespace Valentin_Core
{
    class Program 
    {
        static ITelegramBotClient botClient;
        static void Main()
        {

            var bot = new TelegramBotClient("BOT_API");
            botClient = bot;
            bot.OnMessage += Bot_OnMessage;
            
            bot.StartReceiving();
            Thread.Sleep(Int32.MaxValue);
        }


        static async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            try
            {
                ParseMessage pm = new ParseMessage();
                pm.ParseUserMessage(e.Message.Text);
                if (e.Message.Text == "/paste" &  !pm.CommandExecuted)
                {
                    PasteDotaThread pdt = new PasteDotaThread();
                    pdt.GetPasteFromNotepad(pm);
                    await botClient.SendTextMessageAsync(chatId: e.Message.Chat,
                        text: pdt.MessageText, replyToMessageId: e.Message.MessageId);
                    Console.WriteLine($"chat ID: {e.Message.Chat.Id} ");
                }
                //TODO rewrite this
                if (pm.CurrentRegex.IsMatch(e.Message.Text)& !pm.CommandExecuted)
                {
                    var cube = DndCaluclation.GetCube(pm.BotCommand);
                    DNDResults dndr = new DNDResults();
                    dndr.SomeResult(pm,cube);
                    await botClient.SendTextMessageAsync(chatId: e.Message.Chat, text:dndr.TextMessage,
                        replyToMessageId: e.Message.MessageId, parseMode: ParseMode.Markdown);
                    dndr.FinalResult(pm, cube);
                    await botClient.SendTextMessageAsync(chatId: e.Message.Chat, text: dndr.TextMessage,
                        replyToMessageId: e.Message.MessageId, parseMode: ParseMode.Markdown);

                }

                if (e.Message.Text == "/test")
                {
                    await botClient.SendTextMessageAsync(chatId: e.Message.Chat, text: "_zalupa_,",
                        replyToMessageId: e.Message.MessageId, parseMode: ParseMode.Markdown);
                }

            }
            catch (Exception er)
            {
                await botClient.SendPhotoAsync(chatId: e.Message.Chat, photo:
                    "http://reactionimage.org/img/gallery/3926393515.jpg",
                    replyToMessageId: e.Message.MessageId,
                    caption: "Какая-то хуйня, больше так не делай",
                    parseMode: ParseMode.Markdown);
            }
        }
    }
}
