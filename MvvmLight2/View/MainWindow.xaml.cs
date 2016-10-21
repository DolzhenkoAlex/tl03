using System.Windows;
using MvvmLight2.ViewModel;
using System.Configuration;

namespace MvvmLight2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {
            Closing += (s, e) => ViewModelLocator.Cleanup();
            InitializeComponent();
        }
    }
}