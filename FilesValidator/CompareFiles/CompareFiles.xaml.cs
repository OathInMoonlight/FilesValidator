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
using System.Windows.Shapes;

namespace FilesValidator
{
    /// <summary>
    /// CompareFiles.xaml 的交互逻辑
    /// </summary>
    public partial class CompareFiles : Window
    {
        public CompareFiles_selectPage cf_selectPage;
        public CompareFiles_processingPage cf_processingPage;
        internal FilesComparator? filesComparator;
        public CompareFiles()
        {
            InitializeComponent();
            cf_selectPage = new CompareFiles_selectPage();
            cf_processingPage = new CompareFiles_processingPage(this);
            this.Content = cf_selectPage;
        }
    }
}
