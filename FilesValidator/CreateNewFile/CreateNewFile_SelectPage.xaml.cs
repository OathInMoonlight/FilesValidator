using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FilesValidator
{
    /// <summary>
    /// CreateNewFile_SelectPage.xaml 的交互逻辑
    /// </summary>
    public partial class CreateNewFile_SelectPage : Page
    {
        public CreateNewFile_SelectPage()
        {
            InitializeComponent();
        }

        private void Browse(object sender, RoutedEventArgs e)
        {
            if(singleFile_radioButton.IsChecked == true)
            {
                OpenFileDialog dialog = new OpenFileDialog();
                if(dialog.ShowDialog() == DialogResult.OK)
                {
                    path_textBox.Text = dialog.FileName;
                }
            }
            else
            {
                FolderBrowserDialog dialog = new FolderBrowserDialog();
                if(dialog.ShowDialog() == DialogResult.OK)
                {
                    path_textBox.Text = dialog.SelectedPath;
                }
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
            if(singleFile_radioButton.IsChecked == true && File.Exists(path_textBox.Text) == false)
            {
                System.Windows.MessageBox.Show("文件不存在", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if(multiFile_radioButton.IsChecked == true && Directory.Exists(path_textBox.Text) == false)
            {
                System.Windows.MessageBox.Show("路径不存在", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            CreateNewFile parent = (CreateNewFile)Window.GetWindow(this);
            parent.Content = parent.cnf_processingPage;
            parent.Left = parent.Left - 100;
            parent.Top = parent.Top - 12.5;
            parent.Height = 400;
            parent.Width = 600;

            VerificationFile.FilePathMode filePathMode = VerificationFile.FilePathMode.multi;
            if(singleFile_radioButton.IsChecked == true)
            {
                filePathMode = VerificationFile.FilePathMode.single;
            }
            string path = path_textBox.Text.Replace("/", "\\");
            VerificationFile.EncryptingMode encryptingMode = VerificationFile.EncryptingMode.sha256;
            if(md5_radioButton.IsChecked == true)
            {
                encryptingMode = VerificationFile.EncryptingMode.md5;
            }
            parent.verificationFile = new VerificationFile(filePathMode, path, encryptingMode);
            parent.cnf_processingPage.Progressing();
        }
    }
}
