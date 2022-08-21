using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using static H2Desktop_dotNet.ViewModel.QuickStart;

namespace H2Desktop_dotNet.View
{
    /// <summary>
    /// QuickStart.xaml 的交互逻辑
    /// </summary>
    public partial class QuickStart : Page
    {
        ViewModel.QuickStart VM = new ViewModel.QuickStart();
        public QuickStart()
        {
            InitializeComponent();
            DataContext = VM;
        }

        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var si=(sender as ListView).SelectedItem as ApplicationItem;
            if (si!=null)
            {
                Process.Start("explorer.exe", si.Location);
            }
        }
    }

}
