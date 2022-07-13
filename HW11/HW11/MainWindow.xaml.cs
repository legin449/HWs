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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using System.IO;


namespace HW11
{

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string ClientsPath = "Clients.txt";
        private const string DataChangedPath = "DataChanged.txt";

        ObservableCollection<Client> Clients = new ObservableCollection<Client>();

        Consult consult;

        public MainWindow()
        {
            InitializeComponent();
            LoadClient.IsEnabled = false;
            UpdateClientButton.IsEnabled = false;
            LoadClients();
            ClientData.ItemsSource = Clients;
            Photo.Source = new BitmapImage(new Uri("/PhotoTemplate.bmp", UriKind.Relative));
        }
        /// <summary>
        /// Load info about clients
        /// </summary>
        /// <returns></returns>
        ObservableCollection<Client> LoadClients()
        {
            if(File.Exists(ClientsPath))
            {
                var jsonText = File.ReadAllText(ClientsPath);
                if (!string.IsNullOrEmpty(jsonText))
                {
                    Clients = JsonConvert.DeserializeObject<ObservableCollection<Client>>(jsonText);
                }
            }
            return Clients;
        }
        /// <summary>
        /// Add random client (for testing)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_NewClient(object sender, RoutedEventArgs e)
        {
            Random randomize = new Random();
            var k = Clients.Count() + 1;
            Clients.Add(new Client("Имя " + k.ToString(),
                "Фамилия " + k.ToString(),
                "Отчество " + k.ToString(),
                randomize.Next(89000000, 89999999).ToString(),
                randomize.Next(9000000, 9999999).ToString()));

            ClientData.ItemsSource = Clients;
            File.WriteAllText(ClientsPath, JsonConvert.SerializeObject(Clients));
            
        }
        /// <summary>
        /// Display info about client
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Client selecteditem = ClientData.SelectedItem as Client;
            if (selecteditem != null)
            {
                consult.ShowClientData(selecteditem, FirstName, LastName, MiddleName, PhoneNumber, Passport);
                UpdateClientButton.IsEnabled = true;
            }
            else
            {
                MessageBox.Show("Клиент не был выбран");
            }
            
        }
        /// <summary>
        /// Updating info about client
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateClient(object sender, RoutedEventArgs e)
        {
            Client selecteditem = ClientData.SelectedItem as Client;
            Client newClient = new Client(FirstName.Text, LastName.Text,MiddleName.Text,PhoneNumber.Text,Passport.Text);
            bool WasUpdated = false;
            WasUpdated = consult.ChangeClientData(selecteditem, newClient);
            if (!WasUpdated)
            {
                MessageBox.Show("Данные не были обновлены, так как поле номера телефона пустое");
            }
            else
            {
                File.WriteAllText(ClientsPath, JsonConvert.SerializeObject(Clients));
                MessageBox.Show("Данные были успешно обновлены");
            }
        }
        /// <summary>
        /// Choose worker
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem SelectedMode = (ComboBoxItem)SelectionMode.SelectedItem;
            if (SelectedMode.Content.ToString() == "Consult")
            {
                consult = new Consult();
                LoadClient.IsEnabled = true;
                consult.Init(FirstName, LastName, MiddleName, PhoneNumber, Passport);
            }
            else if (SelectedMode.Content.ToString() == "Manager")
            {
                consult = new Manager();
                LoadClient.IsEnabled = true;
                consult.Init(FirstName, LastName, MiddleName, PhoneNumber, Passport);
            }
        }
    }
}
