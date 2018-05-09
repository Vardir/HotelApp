using System.Windows;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace HotelsApp.UI.Animations
{
    public static class PageAnimationsHelpers
    {
        public static async Task SlideAndFadeInFromRightAsync(this Page page, float seconds)
        {
            var sb = new Storyboard();
            sb.AddSlideFromRight(seconds, page.Width);
            sb.AddFadeIn(seconds);
            sb.Begin(page);

            page.Visibility = Visibility.Visible;
            await Task.Delay((int)seconds * 1000);
        }

        public static async Task SlideAndFadeOutToLeftAsync(this Page page, float seconds)
        {
            var sb = new Storyboard();
            sb.AddSlideToLeft(seconds, page.Width);
            sb.AddFadeOut(seconds);
            sb.Begin(page);

            page.Visibility = Visibility.Visible;
            await Task.Delay((int)seconds * 1000);
        }
    }
}
