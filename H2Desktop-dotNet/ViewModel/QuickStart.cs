using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace H2Desktop_dotNet.ViewModel
{
    partial class QuickStart:ObservableObject
    {
        public class ApplicationItem
        {
            public string Name { get; set; }

            public string Location { get; set; }

            public BitmapSource Icon { get; set; }
        }

        [ObservableProperty]
        private ObservableCollection<ApplicationItem> appItems;

        public QuickStart()
        {
            LoadData();
        }

        const string APP_FOLDER = "applications";
        private void LoadData()
        {
            AppItems = new ObservableCollection<ApplicationItem>();

            if (!Directory.Exists(APP_FOLDER))
            {
                Directory.CreateDirectory(APP_FOLDER);
            }
            var files = Directory.GetFiles(APP_FOLDER);

            foreach (var item in files)
            {
                if (item.ToLower().EndsWith("lnk"))
                {
                    AppItems.Add(ShortCutHelper.ReadShortcut(Path.GetFullPath(item)));

                }
            }
        }
    }
}
