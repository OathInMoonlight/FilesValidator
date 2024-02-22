using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FilesValidator
{
    /// <summary>
    /// CompareFiles_selectPage.xaml 的交互逻辑
    /// </summary>
    public partial class CompareFiles_selectPage : Page
    {
        public CompareFiles_selectPage()
        {
            InitializeComponent();
        }

        private void Browse1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Verification files (*.vf)|*.vf";
            if(dialog.ShowDialog() == DialogResult.OK)
            {
                path1_textBox.Text = dialog.FileName;
            }
        }
        private void Browse2(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Verification files (*.vf)|*.vf";
            if(dialog.ShowDialog() == DialogResult.OK)
            {
                path2_textBox.Text = dialog.FileName;
            }
        }
        private void Cancel(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            Window.GetWindow(this).Close();
        }
        private void Start(object sender, RoutedEventArgs e)
        {
            if(File.Exists(path1_textBox.Text) == false)
            {
                System.Windows.MessageBox.Show("文件1不存在", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if(File.Exists(path2_textBox.Text) == false)
            {
                System.Windows.MessageBox.Show("文件2不存在", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            CompareFiles parent = (CompareFiles)Window.GetWindow(this);
            parent.Content = parent.cf_processingPage;
            parent.Left = parent.Left - 250;
            parent.Top = parent.Top - 150;
            parent.Height = 600;
            parent.Width = 900;

            string path1 = path1_textBox.Text.Replace("/", "\\");
            string path2 = path2_textBox.Text.Replace("/", "\\");
            parent.filesComparator = new FilesComparator(path1_textBox.Text, path2_textBox.Text);
            parent.cf_processingPage.Progressing();
        }
    }
}
