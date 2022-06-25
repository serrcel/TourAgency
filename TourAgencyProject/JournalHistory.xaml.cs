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

namespace TourAgencyProject
{
    /// <summary>
    /// Логика взаимодействия для JournalHistory.xaml
    /// </summary>
    public partial class JournalHistory : Window
    {
        Database DB = new Database();
        public List<Records> RecordsList = new List<Records>();
        public JournalHistory()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DB.Open();
            JournalList.ItemsSource = DB.getRecordList().Reverse();
        }

    }
}
