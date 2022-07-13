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
using System.Collections.Specialized;



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
        Changes interfaceChanges = new Consult();


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
            string jsonText;
            Dictionary<string, string> TempDict = new Dictionary<string, string>();
            if (File.Exists(ClientsPath))
            {
                jsonText = File.ReadAllText(ClientsPath);
                if (!string.IsNullOrEmpty(jsonText))
                {
                    Clients = JsonConvert.DeserializeObject<ObservableCollection<Client>>(jsonText);
                    jsonText = null;
                }
            }
            return Clients;
        }
        Client GetClient(string Info)
        {
            var infoSplitted = Info.Split(';');
            Client client = new Client(
                infoSplitted[0], 
                infoSplitted[1],
                infoSplitted[2],
                infoSplitted[3],
                infoSplitted[4],
                infoSplitted[5],
                infoSplitted[6],
                infoSplitted[7],
                infoSplitted[8]);
            return client;
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
            Client newClient = new Client("Имя " + k.ToString(),
                "Фамилия " + k.ToString(),
                "Отчество " + k.ToString(),
                randomize.Next(89000000, 89999999).ToString(),
                randomize.Next(9000000, 9999999).ToString(), DateTime.Now.ToString(),consult.Name,"", $"{consult.Name} добавил новую запись");
            Clients.Add(newClient);

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
                consult.ShowClientData(selecteditem, FirstName, LastName, MiddleName, PhoneNumber, Passport, ChangeText);
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
            Dictionary<string, string> ChangesInfo = new Dictionary<string, string>();
            Client selecteditem = ClientData.SelectedItem as Client;
            Client newClient = new Client(FirstName.Text, LastName.Text,MiddleName.Text,PhoneNumber.Text,Passport.Text,"","","","");
            ChangesInfo = interfaceChanges.WasChanged(selecteditem, newClient, "Обновление данных", consult.Name);
            newClient = new Client(FirstName.Text, LastName.Text, MiddleName.Text, PhoneNumber.Text, Passport.Text, ChangesInfo["Дата"], ChangesInfo["Сотрудник"], ChangesInfo["Тип изменений"], ChangesInfo["Изменения"]);
            bool WasUpdated = false;
            WasUpdated = consult.ChangeClientData(selecteditem, newClient);
            if (!WasUpdated)
            {
                MessageBox.Show("Данные не были обновлены, так как поле номера телефона пустое");
            }
            else
            {
                selecteditem = new Client(selecteditem.FirstName, selecteditem.LastName, selecteditem.MiddleName, selecteditem.PhoneNumber, selecteditem.Passport, ChangesInfo["Дата"], consult.Name, ChangesInfo["Тип изменений"], ChangesInfo["Изменения"]);
                File.WriteAllText(ClientsPath, JsonConvert.SerializeObject(Clients));
                MessageBox.Show("Данные были успешно обновлены");
                ChangeText.Text = selecteditem.TypeOfChanges;
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
                consult.Name = "Консультант";
            }
            else if (SelectedMode.Content.ToString() == "Manager")
            {
                consult = new Manager();
                LoadClient.IsEnabled = true;
                consult.Init(FirstName, LastName, MiddleName, PhoneNumber, Passport);
                consult.Name = "Менеджер";
            }
        }
    }
}
