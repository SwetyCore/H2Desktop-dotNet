using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace H2Desktop_dotNet
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private class Win32
        {
            [DllImport("user32.dll")]
            public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
            public const UInt32 SWP_NOSIZE = 0x0001;
            public const UInt32 SWP_NOMOVE = 0x0002;
            public const UInt32 SWP_NOACTIVATE = 0x0010;
            public static readonly IntPtr HWND_BOTTOM = new IntPtr(1);

            [DllImport("user32.dll")]
            public static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);
        }



        private void SetBottom(Window window)
        {
            IntPtr hWnd = new WindowInteropHelper(window).Handle;
            Win32.SetWindowPos(hWnd, Win32.HWND_BOTTOM, 0, 0, 0, 0, Win32.SWP_NOSIZE | Win32.SWP_NOMOVE | Win32.SWP_NOACTIVATE);
            Win32.SetWindowLong(hWnd, (-20), 0x80);
        }

        public MainWindow()
        {
            InitializeComponent();
            Height = SystemParameters.WorkArea.Height;
            Width = SystemParameters.WorkArea.Width;
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            SetBottom(this);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            SetBottom(this);
        }
    }
}
