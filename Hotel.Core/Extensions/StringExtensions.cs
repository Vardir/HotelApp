using System.Linq;
using System.Collections.Generic;

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
    }
}