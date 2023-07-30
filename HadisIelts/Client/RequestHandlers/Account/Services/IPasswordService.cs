namespace HadisIelts.Client.RequestHandlers.Account.Services
{
    public interface IPasswordService
    {
        internal string HashPassword(string password);
    }
}
