using System.Windows;
using System.Windows.Media;

namespace Hotel.UI.Extensions
{
    public static class FrameworkElementExtensions
    {
        public static FrameworkElement FindChildByTag(this FrameworkElement root, string tag)
        {
            int count = VisualTreeHelper.GetChildrenCount(root);
            for (int i = 0; i < count; i++)
            {
                var child = VisualTreeHelper.GetChild(root, i) as FrameworkElement;
                if (child == null) continue;
                if (child.Tag is string t && t == tag)
                        return child;
                var deep = FindChildByTag(child, tag);
                if (deep != null) return deep;
            }
            return null;
        }
    }
}