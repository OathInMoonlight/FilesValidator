﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
    /// CompareFiles_processingPage.xaml 的交互逻辑
    /// </summary>
    public partial class CompareFiles_processingPage : Page
    {
        private bool isProcessing;
        private CompareFiles parent;
        public CompareFiles_processingPage(CompareFiles compareFiles)
        {
            InitializeComponent();
            isProcessing = false;
            parent = compareFiles;
        }
        internal bool IfCancelled()
        {
            return !isProcessing;
        }
        internal void UIUpgrade(FilesComparator.CompareResult compareResult, string? filePath)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                progressBar.Value++;
                if(filePath != null)
                {
                    switch(compareResult)
                    {
                        case FilesComparator.CompareResult.equal:
                            progressBar.Value++;
                            showPath_textBox.Text += filePath + Environment.NewLine;
                            break;
                        case FilesComparator.CompareResult.lost:
                            lostFiles_textBox.Text += filePath + Environment.NewLine;
                            break;
                        case FilesComparator.CompareResult.changed:
                            progressBar.Value++;
                            changedFiles_textBox.Text += filePath + Environment.NewLine;
                            break;
                        case FilesComparator.CompareResult.movedNewPosition:
                            movedFiles_textBox.Text += filePath + Environment.NewLine;
                            break;
                        case FilesComparator.CompareResult.newAdded:
                            newFiles_textBox.Text += filePath + Environment.NewLine;
                            break;
                    }
                }
            });
        }
        public async void Progressing()
        {
            isProcessing = true;
            if(IfCancelled() || parent.filesComparator == null)
            {
                return;
            }

            progressBar.IsIndeterminate = true;
            await Task.Run(() => parent.filesComparator.ReadVerificationFiles());
            if(IfCancelled())
            {
                return;
            }

            if(parent.filesComparator.earlierFile == null || parent.filesComparator.laterFile == null)
            {
                return;
            }
            progressBar.IsIndeterminate = false;
            progressBar.Value = 0;
            progressBar.Maximum = parent.filesComparator.earlierFile.filesCount + parent.filesComparator.laterFile.filesCount;
            await Task.Run(() => parent.filesComparator.CompareTwoFiles(IfCancelled, UIUpgrade));
            showPath_textBox.ScrollToEnd();
            lostFiles_textBox.ScrollToEnd();
            changedFiles_textBox.ScrollToEnd();
            movedFiles_textBox.ScrollToEnd();
            newFiles_textBox.ScrollToEnd();
            if(IfCancelled())
            {
                return;
            }
            MessageBox.Show("校验一致：" + parent.filesComparator.equalFilesCount + "\n丢失：" + parent.filesComparator.lostFilesCount + "\n发生改变：" +
                parent.filesComparator.changedFilesCount + "\n发生移动：" + parent.filesComparator.movedFilesCount +"\n新增：" +
                parent.filesComparator.newFilesCount, "校验结果", MessageBoxButton.OK, MessageBoxImage.Information);

            showPath_textBox.Text += "完成" + Environment.NewLine;
            status_textBlock.Text = "比较完成";
            isProcessing = false;
        }
        private void CancelImmediately(object sender, RoutedEventArgs e)
        {
            isProcessing = false;

            parent.filesComparator = null;
            GC.Collect();
            progressBar.Value = 0;
            showPath_textBox.Text = string.Empty;
            lostFiles_textBox.Text = string.Empty;
            changedFiles_textBox.Text = string.Empty;
            movedFiles_textBox.Text = string.Empty;
            newFiles_textBox.Text = string.Empty;

            parent.Content = parent.cf_selectPage;
            parent.Left = parent.Left + 250;
            parent.Top = parent.Top + 150;
            parent.Height = 300;
            parent.Width = 400;
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
