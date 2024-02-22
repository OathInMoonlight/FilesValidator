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
    /// CreateNewFile.xaml 的交互逻辑
    /// </summary>
    public partial class CreateNewFile : Window
    {
        public CreateNewFile_SelectPage cnf_selectPage;
        public CreateNewFile_ProcessingPage cnf_processingPage;
        internal VerificationFile? verificationFile;
        public CreateNewFile()
        {
            InitializeComponent();
            cnf_selectPage = new CreateNewFile_SelectPage();
            cnf_processingPage = new CreateNewFile_ProcessingPage(this);
            this.Content = cnf_selectPage;
        }
    }
}
