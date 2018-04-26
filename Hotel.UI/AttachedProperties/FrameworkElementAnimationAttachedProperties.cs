using System.Windows;
using Hotel.UI.Animations;

namespace Hotel.UI.AttachedProperties
{
    public abstract class AnimateBaseProperty<TParent> : BaseAttachedProperty<TParent, bool>
       where TParent : BaseAttachedProperty<TParent, bool>, new()
    {
        public bool FirstLoad { get; private set; } = true;

        public override void OnValueUpdated(DependencyObject sender, object value)
        {
            if (!(sender is FrameworkElement element)) return;
            //Don't fire if the value is not changed
            if (sender.GetValue(ValueProperty) == value && !FirstLoad) return;

            if (FirstLoad)
            {
                RoutedEventHandler onLoad = null;
                onLoad = (ss, ee) =>
                {
                    element.Loaded -= onLoad;
                    DoAnimation(element, (bool)value);
                    FirstLoad = false;
                };

                element.Loaded += onLoad;
            }
            else DoAnimation(element, (bool)value);
        }

        protected virtual void DoAnimation(FrameworkElement element, bool value) { }
    }

    public class AnimateSlideInFromLeftProperty : AnimateBaseProperty<AnimateSlideInFromLeftProperty>
    {
        protected override async void DoAnimation(FrameworkElement element, bool value)
        {
            if (value)
            {
                await element.SlideAndFadeInFromLeftAsync(FirstLoad ? 0 : 0.3f, false);
            }
            else await element.SlideAndFadeOutToLeftAsync(FirstLoad ? 0 : 0.3f, false);
        }
    }

    public class AnimateSlideInFromBottomProperty : AnimateBaseProperty<AnimateSlideInFromBottomProperty>
    {
        protected override async void DoAnimation(FrameworkElement element, bool value)
        {
            if (value)
            {
                await element.SlideAndFadeInFromBottomAsync(FirstLoad ? 0 : 0.3f, false);
            }
            else await element.SlideAndFadeOutToBottomAsync(FirstLoad ? 0 : 0.3f, false);
        }
    }

    public class AnimateFadeInProperty : AnimateBaseProperty<AnimateFadeInProperty>
    {
        protected override async void DoAnimation(FrameworkElement element, bool value)
        {
            if (value)
            {
                await element.FadeInAsync(FirstLoad ? 0 : 0.3f);
            }
            else await element.FadeOutAsync(FirstLoad ? 0 : 0.3f);
        }
    }
}