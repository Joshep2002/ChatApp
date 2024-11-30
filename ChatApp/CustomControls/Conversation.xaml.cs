using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChatApp.CustomControls
{
    /// <summary>
    /// Lógica de interacción para Conversation.xaml
    /// </summary>
    public partial class Conversation : UserControl
    {
        public Conversation()
        {
            InitializeComponent();
        }
        #region Scroll_Lento

        private void ScrollViewerPreview(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scrollViewer = sender as ScrollViewer;
            if (scrollViewer != null)
            {
                // Ajustar este factor para hacer el scroll más lento
                double scrollFactor = 0.2;
                double newOffset = scrollViewer.VerticalOffset - (e.Delta * scrollFactor);
                scrollViewer.ScrollToVerticalOffset(newOffset);
                e.Handled = true; // Marcar el evento como manejado
            }
        }
        #endregion
    }
}
