using MvvmLight2.ViewModelLocators;
using System.Windows.Controls;

namespace MvvmLight2
{
    /// <summary>
    /// Логика взаимодействия для UniversityView.xaml
    /// </summary>
    public partial class UniversityView : UserControl
    {
        public UniversityView()
        {
            Unloaded += (s, e) => UniversityVMLocator.ClearUniversityVM();
            InitializeComponent();
        }
    }
}
