namespace HadisIelts.Client.Services.Account.Services
{
    public interface IPasswordService
    {
        internal string HashPassword(string password);
    }
}
