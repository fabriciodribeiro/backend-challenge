using System.Text;
using System.Security.Cryptography;

namespace Challenge.Application.Services
{
    public static class HashService
    {
        public static string Cryptograph(string text, string salt)
        {
            var md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes($"{ text }-{salt}");
            byte[] hash = md5.ComputeHash(inputBytes);
            var sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }
}
