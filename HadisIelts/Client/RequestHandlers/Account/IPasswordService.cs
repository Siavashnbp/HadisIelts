namespace HadisIelts.Client.RequestHandlers.Account
{
    public interface IPasswordService
    {
        internal string HashPassword(string password);
    }
}
