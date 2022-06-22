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

using System.IO;

namespace TourAgencyProject
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            // Получает относительный путь до БД
            string path = Directory.GetCurrentDirectory();
            AppDomain.CurrentDomain.SetData("DataDirectory", path);

            InitializeComponent();
        }

    }
}
