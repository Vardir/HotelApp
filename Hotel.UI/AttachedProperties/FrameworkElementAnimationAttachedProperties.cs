using System.Windows;
using System.Threading.Tasks;
using HotelsApp.UI.Animations;
using System.Collections.Generic;

namespace HotelsApp.UI.AttachedProperties
{
    public abstract class AnimateBaseProperty<TParent> : BaseAttachedProperty<TParent, bool>
        where TParent : BaseAttachedProperty<TParent, bool>, new()
    {
        protected Dictionary<DependencyObject, bool> alreadyLoaded = new Dictionary<DependencyObject, bool>();
        protected Dictionary<DependencyObject, bool> firstLoadValue = new Dictionary<DependencyObject, bool>();

        public override void OnValueUpdated(DependencyObject sender, object value)
        {
            if (!(sender is FrameworkElement element)) return;
            //Don't fire if the value is not changed
            if (sender.GetValue(ValueProperty) == value && alreadyLoaded.ContainsKey(sender)) return;

            if (!alreadyLoaded.ContainsKey(sender))
            {
                alreadyLoaded[sender] = false;
                //if (!(bool)value)
                element.Visibility = Visibility.Hidden;

                RoutedEventHandler onLoad = null;
                onLoad = async (ss, ee) =>
                {
                    element.Loaded -= onLoad;
                    await Task.Delay(5);
                    DoAnimation(element, firstLoadValue.ContainsKey(sender) ? firstLoadValue[sender] : (bool)value, true);
                    alreadyLoaded[sender] = true;
                };

                element.Loaded += onLoad;
            }
            else if (alreadyLoaded[sender] == false)
            {
                firstLoadValue[sender] = (bool)value;
            }
            else DoAnimation(element, (bool)value, false);
        }

        protected virtual void DoAnimation(FrameworkElement element, bool value, bool firstLoad) { }
    }

    public class AnimateSlideInFromLeftProperty : AnimateBaseProperty<AnimateSlideInFromLeftProperty>
    {
        protected override async void DoAnimation(FrameworkElement element, bool value, bool firstLoad)
        {
            if (value)
            {
                await element.SlideAndFadeInAsync(AnimationSlideDirection.Left, firstLoad, firstLoad ? 0 : 0.3f, keepMargin: false);
            }
            else await element.SlideAndFadeOutAsync(AnimationSlideDirection.Left, firstLoad ? 0 : 0.3f, keepMargin: false);
        }
    }

    public class AnimateSlideInFromBottomProperty : AnimateBaseProperty<AnimateSlideInFromBottomProperty>
    {
        protected override async void DoAnimation(FrameworkElement element, bool value, bool firstLoad)
        {
            if (value)
            {
                await element.SlideAndFadeInAsync(AnimationSlideDirection.Bottom, firstLoad, firstLoad ? 0 : 0.3f, keepMargin: false);
            }
            else await element.SlideAndFadeOutAsync(AnimationSlideDirection.Bottom, firstLoad ? 0 : 0.3f, keepMargin: false);
        }
    }

    public class AnimateSlideInFromBottomOnLoadProperty : AnimateBaseProperty<AnimateSlideInFromBottomOnLoadProperty>
    {
        protected override async void DoAnimation(FrameworkElement element, bool value, bool firstLoad)
        {
            await element.SlideAndFadeInAsync(AnimationSlideDirection.Bottom, !value, !value ? 0 : 0.3f, keepMargin: false);
        }
    }

    public class AnimateSlideInFromBottomMarginProperty : AnimateBaseProperty<AnimateSlideInFromBottomMarginProperty>
    {
        protected override async void DoAnimation(FrameworkElement element, bool value, bool firstLoad)
        {
            if (value)
            {
                await element.SlideAndFadeInAsync(AnimationSlideDirection.Bottom, firstLoad, firstLoad ? 0 : 0.3f, keepMargin: true);
            }
            else await element.SlideAndFadeOutAsync(AnimationSlideDirection.Bottom, firstLoad ? 0 : 0.3f, keepMargin: true);
        }
    }

    public class AnimateFadeInProperty : AnimateBaseProperty<AnimateFadeInProperty>
    {
        protected override async void DoAnimation(FrameworkElement element, bool value, bool firstLoad)
        {
            if (value)
            {
                await element.FadeInAsync(firstLoad, firstLoad ? 0 : 0.3f);
            }
            else await element.FadeOutAsync(firstLoad ? 0 : 0.3f);
        }
    }

    /// <summary>
    /// Animates a framework element sliding it from right to left and repeating forever
    /// </summary>
    public class AnimateMarqueeProperty : AnimateBaseProperty<AnimateMarqueeProperty>
    {
        protected override void DoAnimation(FrameworkElement element, bool value, bool firstLoad)
        {
            element.MarqueeAsync(firstLoad ? 0 : 3f);
        }
    }
}