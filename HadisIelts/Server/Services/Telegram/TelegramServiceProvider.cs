using Telegram.Bot;

namespace HadisIelts.Server.Services.Telegram
{
    public class TelegramServiceProvider : ITelegramServices
    {
        private readonly TelegramBotClient _telegramClient;
        private string _chatId;
        public TelegramServiceProvider(TelegramConfiguration telegramConfig)
        {
            _telegramClient = new TelegramBotClient(telegramConfig.Token);
            _chatId = telegramConfig.ChatId;
        }
        public async Task SendMessage(string text)
        {
            try
            {
                await _telegramClient.SendTextMessageAsync(_chatId, text);

            }
            catch (Exception)
            {

            }
        }
    }
}
