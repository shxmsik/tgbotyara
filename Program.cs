using Telegram.Bot.Polling;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using System.Threading.Tasks;
using System.Threading;
using System;


internal class Program
{
    private static void Main(string[] args)
    {
        var client = new TelegramBotClient("6990271468:AAFBpkvYnIdpgihnQ5-_kNTx71z8XBKgxho");
        client.StartReceiving(UpdateHandler, Error); /*метод, который выводит бот*/
        Console.ReadLine();
    }
    private static async Task MessageHeandler(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        switch (update.Type)
        {
            case UpdateType.Message:
                {
                    var message = update.Message;
                    var user = message.From;
                    var chat = message.Chat;

                    switch (message.Type)
                    {
                        case MessageType.Text:
                            {
                                if (message.Text == null)
                                {
                                    return;

                                }
                                if (message.Text == "/start")
                                {
                                    //создание кнопок в строке
                                    InlineKeyboardMarkup inlineKeyboard = (new[]
                                    {
                                        new []
                                        {
                                            InlineKeyboardButton.WithCallbackData(text: "Товар", callbackData: "Товар"),
                                        },
                                        new []
                                        {
                                            InlineKeyboardButton.WithCallbackData(text: "Контакты", callbackData: "Контакты"),
                                        },
                                        new []
                                        {
                                            InlineKeyboardButton.WithCallbackData(text: "Адрес", callbackData: "Адрес"),
                                        },
                                        });

                                    Message sentMessage = await botClient.SendTextMessageAsync(
                                        chatId: chat.Id,
                                        text: "Выберите Услугу : ",
                                        replyMarkup: inlineKeyboard,
                                        cancellationToken: cancellationToken) ;

                                    return;
                                }
                                return;
                            }
                    }
                    return;
                }
        }

    }
    private static async Task CallBack(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        Random rnd = new Random();
        int min = 1, max = 3;
        int randomnamber=rnd.Next(min, max);
        //вывод в зависимости от выбранной кнопки
        //1-Камень,2-Ножницы,3-Бумага
        if (update != null && update.CallbackQuery != null)
        {
            string answer = update.CallbackQuery.Data;
            switch (answer)
            {
                case "Товар":
                    { 
                    Message message = await botClient.SendPhotoAsync(
                    chatId: update.CallbackQuery.Message.Chat.Id,
                     photo: InputFile.FromUri("https://static.cdek.shopping/images/shopping/8efa68db948f4571a2701b2d86c4c542.jpg"),
                    parseMode: ParseMode.Html,
                     cancellationToken: cancellationToken);
                    await botClient.SendTextMessageAsync(
                     chatId: update.CallbackQuery.Message.Chat.Id,
                     text: "В наличии, 13000₽",
                     cancellationToken: cancellationToken);
                   
                        await botClient.SendPhotoAsync(
                   chatId: update.CallbackQuery.Message.Chat.Id,
                    photo: InputFile.FromUri("https://i8.amplience.net/i/jpl/fp_643388_a?v=1"),
                   parseMode: ParseMode.Html,
                    cancellationToken: cancellationToken);
                       await botClient.SendTextMessageAsync(
                     chatId: update.CallbackQuery.Message.Chat.Id,
                     text: "В наличии, 10000₽",
                     cancellationToken: cancellationToken);
                        await botClient.SendPhotoAsync(
                   chatId: update.CallbackQuery.Message.Chat.Id,
                    photo: InputFile.FromUri("https://trendzone.ru/upload/iblock/8f7/w990gl5_nb_02_i.jpg"),
                   parseMode: ParseMode.Html,
                    cancellationToken: cancellationToken);
                        await botClient.SendTextMessageAsync(
                      chatId: update.CallbackQuery.Message.Chat.Id,
                      text: "В наличии, 16000₽",
                      cancellationToken: cancellationToken);

                    }
                    break;
                case "Контакты":
                    await botClient.SendTextMessageAsync(
                     chatId: update.CallbackQuery.Message.Chat.Id,
                     text: "Имя Андрей \n Номер +79859435633",
                     cancellationToken: cancellationToken);
                        
                    break;
                case "Адрес":
                    await botClient.SendTextMessageAsync(
                     chatId: update.CallbackQuery.Message.Chat.Id,
                     text: "Метро Бабушкинская \n проезд Шокальского",
                     cancellationToken: cancellationToken);
                    break;

            }


            //InlineKeyboardMarkup inlineKeyboard = update.CallbackQuery.Message.ReplyMarkup!;
            //var inlines = inlineKeyboard.InlineKeyboard;
            //foreach (var item1 in inlines)
            //{
            //    foreach (var item2 in item1)
            //    {

            //       
            //    }
            //}
        }
    }

    private static Task Error(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
    {
        throw new NotImplementedException();
    }
    private static async Task UpdateHandler(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        await CallBack(botClient, update, cancellationToken);
        await MessageHeandler(botClient, update, cancellationToken);

    }
}
