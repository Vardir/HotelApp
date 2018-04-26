using System.Windows;
using System.Windows.Media;
using System.Collections.Generic;

namespace Hotel.UI.Extensions
{
    public static class DependencyObjectExtensions
    {
        static Queue<DependencyObject> cachedQueue = new Queue<DependencyObject>();

        public static T FindChild<T>(this DependencyObject reference) where T : class
        {
            cachedQueue.Clear();
            cachedQueue.Enqueue(reference);
            while (cachedQueue.Count > 0)
            {
                DependencyObject child = cachedQueue.Dequeue();
                T obj = child as T;
                if (obj != null)
                {
                    return obj;
                }
                
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(child); i++)
                {
                    cachedQueue.Enqueue(VisualTreeHelper.GetChild(child, i));
                }
            }
            return null;
        }
    }
}