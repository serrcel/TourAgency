using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Database DB = new Database();
            DB.Open();
            ObservableCollection<Tour> tourList = new ObservableCollection<Tour>();
            tourList = DB.getTourList();
            ListTour.ItemsSource = tourList;
            DB.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (textbox1.Text == "" | textbox2.Text == ""
                | textbox3.Text == "" | textbox4.Text == "")
            {
                MessageBox.Show("Пожалуйста, внесите инофрмацию о себе");
                return;
            }
            var senderObject = ((sender as Button).DataContext) as Tour;
            Person currentPerson = new Person()
            {
                Lastname = textbox1.Text,
                Name = textbox2.Text,
                Patronymic = textbox3.Text,
                Passport = textbox4.Text,
            };
            currentPerson.TourID = senderObject.ID;
            currentPerson.FlyCount = 1;
            Database DB = new Database();
            DB.Open();

            if (DB.clientAlready(textbox4.Text))
            {
                int userFlyCount = DB.GetPersonFlyCount(textbox4.Text);
                if (userFlyCount > 2)
                    MessageBox.Show("Для вас персональная скидка -30% на тур!");
                currentPerson.FlyCount += userFlyCount;
                DB.updatePerson(currentPerson);
            }
            else
            {
                DB.newClient(currentPerson);
            }
            DB.Close();
        }
    }
}
