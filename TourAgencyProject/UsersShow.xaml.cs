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
    /// Логика взаимодействия для UsersShow.xaml
    /// </summary>
    public partial class UsersShow : Window
    {
        public UsersShow()
        {
            InitializeComponent();
            Database DB = new Database();
            DB.Open();
            UsersDataGrid.ItemsSource = DB.getUsers().DefaultView;
            DB.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}
