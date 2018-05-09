using System;
using System.Windows;
using System.Windows.Media.Animation;
using System.Runtime.CompilerServices;

namespace HotelsApp.UI.Animations
{
    public static class StoryBoardHelpers
    {
        #region To/From Right
        /// <summary>
        /// Slide from right animation for storyboard
        /// </summary>
        /// <param name="storyboard">Storyboard to which should animation be added</param>
        /// <param name="seconds">Time in seconds to make an animation</param>
        /// <param name="offset">The distance to the right to start from</param>
        /// <param name="decelerationRatio">The rate of deceleration</param>
        /// <param name="keepMargin">Whether to keep the element at the same width during animation</param>
        public static void AddSlideFromRight(this Storyboard storyboard, float seconds, double offset, float decelerationRatio = 0.9f, bool keepMargin = true)
        {
            var from = new Thickness(keepMargin ? offset : 0, 0, -offset, 0);
            AddSlideEffect(storyboard,
                new SlideEffectParameters(from, new Thickness(0), seconds, decelerationRatio));
        }

        /// <summary>
        /// Slide to right animation for storyboard
        /// </summary>
        /// <param name="storyboard">Storyboard to which should animation be added</param>
        /// <param name="seconds">Time in seconds to make an animation</param>
        /// <param name="offset">The distance to the right to start from</param>
        /// <param name="decelerationRatio">The rate of deceleration</param>
        /// <param name="keepMargin">Whether to keep the element at the same width during animation</param>
        public static void AddSlideToRight(this Storyboard storyboard, float seconds, double offset, float decelerationRatio = 0.9f, bool keepMargin = true)
        {
            var to = new Thickness(keepMargin ? offset : 0, 0, -offset, 0);
            AddSlideEffect(storyboard,
                new SlideEffectParameters(new Thickness(0), to, seconds, decelerationRatio));
        }
        #endregion

        #region To/From Left
        /// <summary>
        /// Slide from left animation for storyboard
        /// </summary>
        /// <param name="storyboard">Storyboard to which should animation be added</param>
        /// <param name="seconds">Time in seconds to make an animation</param>
        /// <param name="offset">The distance to the left to start from</param>
        /// <param name="decelerationRatio">The rate of deceleration</param>
        /// <param name="keepMargin">Whether to keep the element at the same width during animation</param>
        public static void AddSlideFromLeft(this Storyboard storyboard, float seconds, double offset, float decelerationRatio = 0.9f, bool keepMargin = true)
        {
            var from = new Thickness(-offset, 0, keepMargin ? offset : 0, 0);
            AddSlideEffect(storyboard,
                new SlideEffectParameters(from, new Thickness(0), seconds, decelerationRatio));
        }

        /// <summary>
        /// Slide to left animation for storyboard
        /// </summary>
        /// <param name="storyboard">Storyboard to which should animation be added</param>
        /// <param name="seconds">Time in seconds to make an animation</param>
        /// <param name="offset">The distance to the left to start from</param>
        /// <param name="decelerationRatio">The rate of deceleration</param>
        /// <param name="keepMargin">Whether to keep the element at the same width during animation</param>
        public static void AddSlideToLeft(this Storyboard storyboard, float seconds, double offset, float decelerationRatio = 0.9f, bool keepMargin = true)
        {
            var to = new Thickness(-offset, 0, keepMargin ? offset : 0, 0);
            AddSlideEffect(storyboard,
                new SlideEffectParameters(new Thickness(0), to, seconds, decelerationRatio));
        }
        #endregion

        #region To/From Bottom
        /// <summary>
        /// Slide from bottom animation for storyboard
        /// </summary>
        /// <param name="storyboard">Storyboard to which should animation be added</param>
        /// <param name="seconds">Time in seconds to make an animation</param>
        /// <param name="offset">The distance to the top to start from</param>
        /// <param name="decelerationRatio">The rate of deceleration</param>
        /// <param name="keepMargin">Whether to keep the element at the same height during animation</param>
        public static void AddSlideFromBottom(this Storyboard storyboard, float seconds, double offset, float decelerationRatio = 0.9f, bool keepMargin = true)
        {
            var from = new Thickness(0, keepMargin ? offset : 0, 0, -offset);
            AddSlideEffect(storyboard,
                new SlideEffectParameters(from, new Thickness(0), seconds, decelerationRatio));
        }

        /// <summary>
        /// Slide to bottom animation for storyboard
        /// </summary>
        /// <param name="storyboard">Storyboard to which should animation be added</param>
        /// <param name="seconds">Time in seconds to make an animation</param>
        /// <param name="offset">The distance to the bottom to start from</param>
        /// <param name="decelerationRatio">The rate of deceleration</param>
        /// <param name="keepMargin">Whether to keep the element at the same height during animation</param>
        public static void AddSlideToBottom(this Storyboard storyboard, float seconds, double offset, float decelerationRatio = 0.9f, bool keepMargin = true)
        {
            var to = new Thickness(0, keepMargin ? offset : 0, 0, -offset);
            AddSlideEffect(storyboard,
                new SlideEffectParameters(new Thickness(0), to, seconds, decelerationRatio));
        }
        #endregion

        #region Fade In/Out
        /// <summary>
        /// Fade-in animation for storyboard
        /// </summary>
        /// <param name="storyboard">Storyboard to which should animation be added</param>
        /// <param name="seconds">Time in seconds to make an animation</param>
        public static void AddFadeIn(this Storyboard storyboard, float seconds)
        {
            AddFadeEffect(storyboard, seconds, 0, 1);
        }

        /// <summary>
        /// Fade-out animation for storyboard
        /// </summary>
        /// <param name="storyboard">Storyboard to which should animation be added</param>
        /// <param name="seconds">Time in seconds to make an animation</param>
        public static void AddFadeOut(this Storyboard storyboard, float seconds)
        {
            AddFadeEffect(storyboard, seconds, 1, 0);
        }
        #endregion

        #region Effects
        /// <summary>
        /// Fade animation for storyboard
        /// </summary>
        /// <param name="storyboard">Storyboard to which should animation be added</param>
        /// <param name="seconds">Time in seconds to make an animation</param>
        /// <param name="from">The value to fade from</param>
        /// <param name="to">The value to fade to</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static void AddFadeEffect(Storyboard storyboard, float seconds, double from, double to)
        {
            var slideAnimation = new DoubleAnimation()
            {
                Duration = new Duration(TimeSpan.FromSeconds(seconds)),
                From = from,
                To = to
            };
            Storyboard.SetTargetProperty(slideAnimation, new PropertyPath("Opacity"));
            storyboard.Children.Add(slideAnimation);
        }

        /// <summary>
        /// Slide animation for storyboard
        /// </summary>
        /// <param name="storyboard">Storyboard to which should animation be added</param>
        /// <param name="seconds">Time in seconds to make an animation</param>
        /// <param name="offset">The distance to the right to start from</param>
        /// <param name="decelerationRatio">The rate of deceleration</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static void AddSlideEffect(this Storyboard storyboard, SlideEffectParameters parameters)
        {
            var slideAnimation = new ThicknessAnimation()
            {
                Duration = new Duration(TimeSpan.FromSeconds(parameters.Seconds)),
                From = parameters.From,
                To = parameters.To,
                DecelerationRatio = parameters.DecelerationRatio
            };
            Storyboard.SetTargetProperty(slideAnimation, new PropertyPath("Margin"));
            storyboard.Children.Add(slideAnimation);
        }
        #endregion
    }

    struct SlideEffectParameters
    {
        public Thickness From { get; }
        public Thickness To { get; }
        public float Seconds { get; }
        public float DecelerationRatio { get; }

        public SlideEffectParameters(Thickness from, Thickness to, float seconds, float decelerationRate)
        {
            From = from;
            To = to;
            Seconds = seconds;
            DecelerationRatio = decelerationRate;
        }
    }
}
