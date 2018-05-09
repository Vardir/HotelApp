using System.Linq;
using System.Collections.Generic;

namespace HotelsApp.Core.Extensions
{
    public static class IntHelpers
    {
        public static int GetUniqueId(int initial, IEnumerable<int> existingIds)
        {
            if (existingIds.Any())
                return existingIds.Max() + 1;
            return initial;
        }
    }
}