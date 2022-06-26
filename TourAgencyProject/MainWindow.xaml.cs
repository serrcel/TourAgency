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
        Person LastPerson = new Person();
        public ObservableCollection<Tour> tourList;
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

            if (DB.getPerson(textbox4.Text).Name != "НЕПОЛЬЗОВАТЕЛЬ")
            {
                int userFlyCount = DB.getPerson(textbox4.Text).FlyCount;
                if (userFlyCount > 2)
                    MessageBox.Show("Для вас персональная скидка -30% на тур!");
                currentPerson.FlyCount += userFlyCount;
                DB.updatePerson(currentPerson);
            }
            else
            {
                DB.newClient(currentPerson);
                MessageBox.Show("Билет куплет, приятного отдыха!");
            }
            LastPerson = DB.getPerson(textbox4.Text);
            AddNewRecord(LastPerson);
            DB.Close();
        }
        private void AddNewRecord(Person client)
        {
            Database DB = new Database();
            DB.Open();
            var currentTour = DB.getTour(client.TourID);
            Records newRecord = new Records()
            {
                Name = $"Путевка в {currentTour.Name}",
                PersonFIO = $"{client.Lastname} {client.Name} {client.Patronymic}",
                Person_id = client.Id,
                Tour_id = client.TourID,
                OrderDate = DateTime.Now
            };
            DB.newRecord(newRecord);

        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            JournalHistory journalWindow = new JournalHistory();
            journalWindow.Show();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            UsersShow UsersWindow = new UsersShow();
            UsersWindow.Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Filters filterWindow = new Filters();
            filterWindow.mainWindow = this;
            filterWindow.Show();
        }
    }
}
