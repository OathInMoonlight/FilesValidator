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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FilesValidator
{
    /// <summary>
    /// CreateNewFile_ProcessingPage.xaml 的交互逻辑
    /// </summary>
    public partial class CreateNewFile_ProcessingPage : Page
    {
        private bool isProcessing;
        private CreateNewFile parent;
        public CreateNewFile_ProcessingPage(CreateNewFile createNewFile)
        {
            InitializeComponent();
            isProcessing = false;
            parent = createNewFile;
        }
        public bool IfCancelled()
        {
            return !isProcessing;
        }
        public void UIUpgrade(string? filePath)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                progressBar.Value++;
                if(filePath != null)
                {
                    showPath_textBox.Text += filePath + Environment.NewLine;
                    showPath_textBox.LineDown();
                }
            });
        }
        public async void Progressing()
        {
            isProcessing = true;
            if(IfCancelled() || parent.verificationFile == null)
            {
                return;
            }

            status_textBlock.Text = "正在计算文件数量...";
            if(parent.verificationFile.fileMode == VerificationFile.FilePathMode.multi)
            {
                progressBar.IsIndeterminate = true;
                UIUpgrade(null);
                await Task.Run(() => parent.verificationFile.getFilesCount(parent.verificationFile.filePath));
                progressBar.Maximum = parent.verificationFile.filesCount;
                progressBar.IsIndeterminate = false;
                progressBar.Value = 0;
                MessageBox.Show("共发现" + parent.verificationFile.filesCount.ToString() + "个文件", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                parent.verificationFile.filesCount = 1;
                progressBar.Maximum = 1;
            }
            if(IfCancelled())
            {
                return;
            }

            progressBar.Value = 0;
            status_textBlock.Text = "正在计算校验码...";
            UIUpgrade(null);
            await Task.Run(() => parent.verificationFile.LoopThroughPath(parent.verificationFile.filePath, IfCancelled, UIUpgrade));
            if(IfCancelled())
            {
                return;
            }

            status_textBlock.Text = "正在写入校验文件...";
            progressBar.Value = 0;
            progressBar.Maximum = parent.verificationFile.hashCode.Count;
            UIUpgrade(null);
            await Task.Run(() => parent.verificationFile.WriteToFile(IfCancelled, UIUpgrade));
            if(IfCancelled())
            {
                return;
            }

            showPath_textBox.Text += "完成" + Environment.NewLine;
            status_textBlock.Text = "创建成功";
            isProcessing = false;
        }
        private void CancelImmediately(object sender, RoutedEventArgs e)
        {
            isProcessing = false;

            if(parent.verificationFile == null)
            { 
                return;
            }
            string fileName = parent.verificationFile.createFileName();
            if(File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            parent.verificationFile.hashCode.Clear();
            parent.verificationFile = null;
            GC.Collect();
            showPath_textBox.Text = string.Empty;

            parent.Content = parent.cnf_selectPage;
            parent.Width = 400;
            parent.Height = 350;
            parent.Left = parent.Left + 100;
            parent.Top = parent.Top + 12.5;
        }
        private void Complete(object sender, RoutedEventArgs e)
        {
            if(IfCancelled())
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                parent.Close();
            }
        }
    }
}
