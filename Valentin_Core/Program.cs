using System;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;
using File = System.IO.File;


namespace Valentin_Core
{
    class Program
    {
        static ITelegramBotClient botClient;

        static void Main()
        {
            string bot_token = Environment.GetEnvironmentVariable("BOT_TOKEN");
            var bot = new TelegramBotClient(bot_token);
            botClient = bot;
            bot.OnMessage += Bot_OnMessage;

            bot.StartReceiving();
            Thread.Sleep(Int32.MaxValue);
        }


        static async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            try
            {
                Message message = new Message();
                ParseMessage pm = new ParseMessage();
                PasteDotaThread pdt = new PasteDotaThread();
                pm.ParseUserMessage(message.ReplyToMessage.Text);
                switch (pm.ExistBotCommands.Find(x => x.Equals(pm.BotCommand)))
                {
                    case "/paste":
                        pdt.GetPasteFromNotepad(pm);
                        await botClient.SendTextMessageAsync(chatId: e.Message.Chat,
                            text: pdt.MessageText, replyToMessageId: e.Message.MessageId);
                        break;
                    case "/d4":
                    case "/d8":
                    case "/d10":
                    case "/d12":
                    case "/d20":
                    case "/d100":
                        var cube = DndCaluclation.GetCube(pm.BotCommand);
                        DNDResults dndr = new DNDResults();
                        dndr.SomeResult(pm, cube);
                        await botClient.SendTextMessageAsync(chatId: e.Message.Chat, text: dndr.TextMessage,
                            replyToMessageId: e.Message.MessageId, parseMode: ParseMode.Markdown);
                        dndr.FinalResult(pm, cube);
                        await botClient.SendTextMessageAsync(chatId: e.Message.Chat, text: dndr.TextMessage,
                            replyToMessageId: e.Message.MessageId, parseMode: ParseMode.Markdown);
                        break;
                    case "/otec":
                        pdt.GetPasteFromOtecNotepad(pm);
                        await botClient.SendTextMessageAsync(chatId: e.Message.Chat,
                            text: pdt.MessageText, replyToMessageId: e.Message.MessageId);
                        break;
                    case "/help":
                        await botClient.SendTextMessageAsync(chatId: e.Message.Chat,
                            text: "Напиши вот этому человеку @shadowmorex", replyToMessageId: e.Message.MessageId);
                        break;
                    case "/fresco":
                        Quotes quotes = new Quotes(pm);
                        await using (var file = File.OpenRead(quotes.PathToFile))
                        {
                            var iof = new InputOnlineFile(file, "quotes.bpm");
                            await botClient.SendPhotoAsync(e.Message.Chat, iof, "Все факты достоверны и тщательно проверены администрацией",
                                replyToMessageId: e.Message.MessageId);
                        }
                        break;
                    case "/test":
                        await using (var file =
                            File.OpenRead(@"D:\projects\Valentin_Core\Valentin_Core\resources\img\fresco.jpg"))
                        {
                            var iofa = new InputOnlineFile(file, "shit.png");
                            await botClient.SendPhotoAsync(e.Message.Chat, iofa, "иди нахуй",
                                replyToMessageId: e.Message.MessageId);
                        }

                        break;



                }
            }
            catch (Exception er)
            {
                await botClient.SendPhotoAsync(chatId: e.Message.Chat, photo:
                    "http://reactionimage.org/img/gallery/3926393515.jpg",
                    replyToMessageId: e.Message.MessageId,
                    caption: "Какая-то хуйня, больше так не делай.\n" +
                             "Если думаешь, что проблема в боте\n" +
                             "то вызови автора командой */help*",
                    parseMode: ParseMode.Markdown);
            }
        }
    }
}
