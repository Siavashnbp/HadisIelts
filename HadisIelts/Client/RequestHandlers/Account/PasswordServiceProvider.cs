using System.Security.Cryptography;

namespace HadisIelts.Client.RequestHandlers.Account
{
    public class PasswordServiceProvider : IPasswordService
    {
        string IPasswordService.HashPassword(string password)
        {
            byte[] data = System.Text.Encoding.ASCII.GetBytes(password);
            data = new SHA256Managed().ComputeHash(data);
            return System.Text.Encoding.ASCII.GetString(data);
        }
    }
}
