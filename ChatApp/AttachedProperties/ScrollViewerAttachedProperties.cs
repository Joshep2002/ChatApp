﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ChatApp.AttachedProperties
{
    public class ScrollViewerAttachedProperties
    {

        // Attached Properties for AutoScroll Conversation to las Message
        public static readonly DependencyProperty AutoScrollProperty =
            DependencyProperty.RegisterAttached("AutoScroll",typeof(bool),typeof(ScrollViewerAttachedProperties),new PropertyMetadata(false,AutoScrollPropertyChanged));

        public static void AutoScrollPropertyChanged(DependencyObject obj,DependencyPropertyChangedEventArgs args)
        {
            var scrollViewer = obj as ScrollViewer;
            if(scrollViewer!=null && (bool)args.NewValue)
            {
                scrollViewer.ScrollChanged += ScrollViewer_ScrollChanged;
                scrollViewer.ScrollToEnd();
            }
            else
            {
                scrollViewer.ScrollChanged += ScrollViewer_ScrollChanged;
            }
        }

        private static void ScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if(e.ExtentHeightChange != 0)
            {
                var scrollViewr = sender as ScrollViewer;
                scrollViewr?.ScrollToBottom();
            }
        }


        public static bool GetAutoScroll(DependencyObject obj)
        {
            return (bool)obj.GetValue(AutoScrollProperty);
        }

        public static void SetAutoScroll(DependencyObject obj, bool value)
        {
            obj.SetValue(AutoScrollProperty, value);
        }

        

    }
}
