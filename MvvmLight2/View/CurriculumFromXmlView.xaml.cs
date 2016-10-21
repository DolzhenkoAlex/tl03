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
using System.IO;

namespace MvvmLight2
{
    /// <summary>
    /// Логика взаимодействия для CurriculumFromXmlView.xaml
    /// </summary>
    public partial class CurriculumFromXmlView : UserControl
    {
        public CurriculumFromXmlView()
        {
            InitializeComponent();
        }

        private void treeCatalog_Loaded(object sender, RoutedEventArgs e)
        {
            foreach(DriveInfo drive in DriveInfo.GetDrives())
            {
                TreeViewItem item = new TreeViewItem();
                item.Tag = drive;
                item.Header = drive.ToString();
                item.Items.Add("*");
                treeCatalog.Items.Add(item);

            }
        }

        private void TreeViewItem_Expanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = (TreeViewItem)e.OriginalSource;
            item.Items.Clear();

            DirectoryInfo dir;
            if(item.Tag is DriveInfo)
            {
                DriveInfo drive = (DriveInfo)item.Tag;
                dir = drive.RootDirectory;
            }
            else
            {
                dir = (DirectoryInfo)item.Tag;
            }

            try
            {
                foreach(DirectoryInfo subDir in dir.GetDirectories())
                {
                    TreeViewItem newItem = new TreeViewItem();
                    newItem.Tag = subDir;
                    newItem.Header = subDir.ToString();
                    newItem.Items.Add("*");
                    item.Items.Add(newItem);
                }
            }
            catch
            { }
        }

        private void treeCatalog_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            //DirectoryInfo dir = (DirectoryInfo)treeCatalog.SelectedValue;
            //string FullName = dir.FullName;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBoxAcademicYear.SelectedIndex = Properties.Settings.Default.CurriculumComboBoxAcademicYearIndex;
        }

        private void CurriculumFromXmlView_OnUnloaded(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.CurriculumComboBoxAcademicYearIndex = ComboBoxAcademicYear.SelectedIndex; 
        }
    }
}
