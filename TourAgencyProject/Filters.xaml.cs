using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для Filters.xaml
    /// </summary>
    public partial class Filters : Window
    {
        public ObservableCollection<Tour> listFiletr;
        public MainWindow mainWindow;
        public Filters()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Database DB = new Database();
            DB.Open();
            mainWindow.ListTour.ItemsSource = DB.getTourListWithFilters(otkudaText.Text, destinationText.Text, costText.Text);
            DB.Close();
        }
    }
}
