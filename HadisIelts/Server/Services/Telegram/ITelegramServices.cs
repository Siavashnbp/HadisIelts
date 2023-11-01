namespace HadisIelts.Server.Services.Telegram
{
    public interface ITelegramServices
    {
        public Task SendMessage(string text);
    }
}
