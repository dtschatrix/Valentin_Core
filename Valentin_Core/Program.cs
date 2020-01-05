using System;
using System.Threading;
using Microsoft.VisualBasic.CompilerServices;
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
            var bot = new TelegramBotClient("BOT API");
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

                if (e.Message.Text == "/paste" && (e.Message.Chat.Id == -332067211 || e.Message.Chat.Id == -233464656))
                {
                    PasteDotaThread pdt = new PasteDotaThread();
                    pdt.GetPasteFromNotepad();
                    await botClient.SendTextMessageAsync(chatId: e.Message.Chat,
                        text: pdt.MessageText, replyToMessageId: e.Message.MessageId);
                    Console.WriteLine($"chat ID: {e.Message.Chat.Id} ");
                }
                //TODO rewrite this
                if (pm.CurrentRegex.IsMatch(e.Message.Text))
                {
                    pm.ParseUserMessage(e.Message.Text);
                    DND dnd = new DND();
                    dnd.SetCubeType(CubeTypeNumber.d20);
                    dnd.GetCubeResult(dnd);
                    dnd.SomeResult($"{pm.MessageText}");
                    await botClient.SendTextMessageAsync(chatId: e.Message.Chat, text:
                        dnd.MessageText, replyToMessageId: e.Message.MessageId, parseMode: ParseMode.Markdown);
                    Thread.Sleep(1000);
                    dnd.FinalResult(dnd, $"{pm.MessageText}");
                    await botClient.SendTextMessageAsync(chatId: e.Message.Chat, text:
                        dnd.MessageText, replyToMessageId: e.Message.MessageId, parseMode: ParseMode.Markdown);
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
