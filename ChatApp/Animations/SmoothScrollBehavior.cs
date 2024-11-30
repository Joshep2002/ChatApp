using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace ChatApp.Animations
{


    public class SmoothScrollBehavior
    {
        public static readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.RegisterAttached("IsEnabled", typeof(bool), typeof(SmoothScrollBehavior), new PropertyMetadata(false, OnIsEnabledChanged));

        public static bool GetIsEnabled(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsEnabledProperty);
        }

        public static void SetIsEnabled(DependencyObject obj, bool value)
        {
            obj.SetValue(IsEnabledProperty, value);
        }

        private static void OnIsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ScrollViewer scrollViewer)
            {
                if ((bool)e.NewValue)
                {
                    scrollViewer.PreviewMouseWheel += OnMouseWheel;
                }
                else
                {
                    scrollViewer.PreviewMouseWheel -= OnMouseWheel;
                }
            }
        }

        private static void OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (sender is ScrollViewer scrollViewer)
            {
                double newOffset = scrollViewer.VerticalOffset - e.Delta;
                if (newOffset < 0) newOffset = 0;
                else if (newOffset > scrollViewer.ExtentHeight) newOffset = scrollViewer.ExtentHeight;

                DoubleAnimation verticalAnimation = new DoubleAnimation
                {
                    From = scrollViewer.VerticalOffset,
                    To = newOffset,
                    Duration = TimeSpan.FromMilliseconds(5000), // Duración de la animación
                    DecelerationRatio = 0.2 // Ratio de desaceleración
                };

                verticalAnimation.Completed += (s, a) => scrollViewer.ScrollToVerticalOffset(newOffset);
                scrollViewer.BeginAnimation(ScrollViewerBehavior.VerticalOffsetProperty, verticalAnimation);

                e.Handled = true;
            }
        }
    }

    public static class ScrollViewerBehavior
    {
        public static readonly DependencyProperty VerticalOffsetProperty =
            DependencyProperty.RegisterAttached("VerticalOffset", typeof(double), typeof(ScrollViewerBehavior), new PropertyMetadata(OnVerticalOffsetChanged));

        public static double GetVerticalOffset(DependencyObject obj)
        {
            return (double)obj.GetValue(VerticalOffsetProperty);
        }

        public static void SetVerticalOffset(DependencyObject obj, double value)
        {
            obj.SetValue(VerticalOffsetProperty, value);
        }

        private static void OnVerticalOffsetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ScrollViewer scrollViewer)
            {
                scrollViewer.ScrollToVerticalOffset((double)e.NewValue);
            }
        }
    }
}