using System;
using System.Collections.Generic;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot
{
    class Program
    {
        [STAThread, Obsolete]
        static void Main(string[] args)
        {
            TelegramSQB Telegram = new TelegramSQB();
            Telegram.Start();
        }
    }

    #region SQB
    [Obsolete]
    public class TelegramSQB
    {
        private string Token { get; } = "";
        private TelegramBotClient _client;

        public void Start()
        {
            Console.WriteLine("Hello World!");
            _client = new TelegramBotClient(Token);
            _client.StartReceiving();
            _client.OnMessage += OnMessageHandler;
            _client.OnReceiveError += OnReceiveErrorHandler;
            Console.ReadLine();
            _client.StopReceiving();
        }

        private void OnReceiveErrorHandler(object sender, ReceiveErrorEventArgs e)
        {
            Console.WriteLine("Receive Error");
        }

        private async void OnMessageHandler(object sender, MessageEventArgs e)
        {
            try
            {
                var message = e.Message;
                var chatId = message.Chat.Id;
                if (message.Text != null)
                    switch (message.Text)
                    {
                        case "Sticker":
                            var sticker = await _client.SendStickerAsync(chatId, sticker: "https://tlgrm.ru/_/stickers/80a/5c9/80a5c9f6-a40e-47c6-acc1-44f43acc0862/3.webp", replyMarkup: GetButtons(), replyToMessageId: message.MessageId);
                            break;
                        case "Message":
                            var letter = await _client.SendTextMessageAsync(chatId, message.Text, replyMarkup: GetButtons());
                            break;
                        case "Picture":
                            var picture = await _client.SendPhotoAsync(chatId, "https://static4.depositphotos.com/1000423/454/i/600/depositphotos_4548401-stock-photo-symbol-of-yin-and-yang.jpg", replyMarkup: GetButtons(), replyToMessageId: message.MessageId);
                            break;
                        case "Start":
                            var start = await _client.SendTextMessageAsync(chatId, "Hello World!", replyMarkup: GetButtons());
                            break;
                        default:
                            await _client.SendTextMessageAsync(chatId, "Select a command");
                            break;
                    }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        private IReplyMarkup GetButtons()
        {
            return new ReplyKeyboardMarkup
            {
                Keyboard = new List<List<KeyboardButton>>
                {
                    new List<KeyboardButton>
                    {
                        new KeyboardButton { Text = "Start" }, new KeyboardButton { Text = "Sticker" }
                    },
                    new List<KeyboardButton>
                    {
                        new KeyboardButton { Text = "Message" }, new KeyboardButton { Text = "Picture" }
                    }
                }
            };
        }
    }

    #endregion
}