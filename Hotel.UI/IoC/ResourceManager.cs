using System.Windows;

namespace HotelsApp.UI.IoC
{
    public static class ResourceManager
    {
        static ResourceDictionary Resources => Application.Current.Resources;

        public static string GetSymbol(string resourceName)
        {
            if (Resources.Contains(resourceName))
                return Resources[resourceName] as string;
            return null;
        }
    }
}