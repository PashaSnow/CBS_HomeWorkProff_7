using System;
using System.Windows;

namespace AdditionTaskWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window    
    {
        public MainWindow()
        {
            InitializeComponent();
            new Presenter(this);
        }

        private EventHandler open;
        private EventHandler clear;

        public event EventHandler Open
        {
            add { open += value; }
            remove { open -= value; }
        }

        public event EventHandler Clear
        {
            add { clear += value; }
            remove { clear -= value; }
        }


        // Хендлери
        private void OpenClick(object sender, RoutedEventArgs e)
        {
            open.Invoke(sender, e);
        }

        private void ClearClick(object sender, RoutedEventArgs e)
        {
            clear.Invoke(sender, e);
        }
    }
}
