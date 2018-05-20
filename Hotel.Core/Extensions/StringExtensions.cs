using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace HotelsApp.Core.Extensions
{
    public static class StringHelpers
    {
        public static string UniqueName(string inputName, IEnumerable<string> existingNames)
        {
            string newName = inputName;
            int i = 1;
            while (existingNames.Contains(newName))
            {
                int lastBracket = newName.LastIndexOf('(');
                string replacer = $"({i})";
                if (lastBracket > 0)
                    newName = newName.Replace(newName.Substring(lastBracket), replacer);
                else newName = newName + replacer;
                i++;
            }
            return newName;
        }
        public static string GetMD5(this string input)
        {
            using (var md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                var sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}