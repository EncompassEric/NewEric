namespace Crosscutting.NetFramework.Extensions
{
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    public static class StringExtension
    {
        public static string GetMd5(this string str)
        {
            var input = Encoding.Default.GetBytes(str);
            var md5 = new MD5CryptoServiceProvider();
            var output = md5.ComputeHash(input);
             
            return output.Aggregate(string.Empty, (current, t) => current + t.ToString("x"));
        }
    }
}
