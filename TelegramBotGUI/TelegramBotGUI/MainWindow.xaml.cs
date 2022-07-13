using System;
using Telegram.Bot;
using System.IO;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Newtonsoft.Json;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TelegramBotGUI
{
    
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            
            InitializeComponent();

            GetListUpdates();
        }

        Dictionary<string, Update> GetListUpdates()
        {
            Dictionary<string, Update> messages = new Dictionary<string, Update>();

            if (System.IO.File.Exists("json.txt"))
            {
                var jsonText = System.IO.File.ReadAllText("json.txt");

                if (!string.IsNullOrEmpty(jsonText))
                {
                    List<Update> jsonArr = JsonConvert.DeserializeObject<List<Update>>(jsonText);
                    foreach (var item in jsonArr)
                    {
                        messages[item.Message.MessageId + ": " + item.Message.From.Username + ": " + item.Message.Text] = item;
                    }
                    listBox.ItemsSource = messages.Keys;
                }
            }
            return messages;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string token;
            if (System.IO.File.Exists("token.txt"))
            {
                token = System.IO.File.ReadAllText("token.txt");
            }
            else
            {
                token = inputBox_Token.Text;
                System.IO.File.WriteAllText("token.txt", token);
            }
            var botClient = new TelegramBotClient(token);
            
            ProcessUpdates(botClient);
        }
        /// <summary>
        /// Main workflow for processing updates
        /// </summary>
        /// <param name="botClient"></param>
        /// <returns></returns>
        async void ProcessUpdates(TelegramBotClient botClient)
        {
            Update botUpdatesPrev = new Update();
            Message message;
            List<Update> updates = new List<Update> { };
            string destinationFilePath;
            int offset = 0;
            string fileid;
            string jsonText;
            if (System.IO.File.Exists("json.txt"))
            {
                jsonText = System.IO.File.ReadAllText("json.txt");
                if (!string.IsNullOrEmpty(jsonText))
                {
                    updates = JsonConvert.DeserializeObject<List<Update>>(jsonText);
                }
            }
            
            while (true)
            {
                var botUpdates = new Telegram.Bot.Types.Update[0];
                try
                {
                    botUpdates = await botClient.GetUpdatesAsync(offset: offset);
                    SendButton.IsEnabled = true;
                    RunBotButton.IsEnabled = false;
                }
                catch (Exception)
                {
                    MessageBox.Show("The bot cannot read token");
                    return;
                }
                var tgUpdatesLen = botUpdates.Length;
                if (tgUpdatesLen != 0)
                {
                    updates.Add(botUpdates[0]);
                    if (botUpdates[0].Message.Type == MessageType.Text)
                    {
                        if (botUpdates[0].Message.Text == "/start")
                        {
                            //Console.WriteLine("message was sent");
                            message = await botClient.SendTextMessageAsync(botUpdates[0].Message.Chat.Id, "Hi, " + 
                                botUpdates[0].Message.Chat.FirstName + "\nSend me file for downloading\n/listdl to list downloaded files");
                        }
                    }
                    if (botUpdates[0].Message.Type == MessageType.Document)
                    {
                        if (!Directory.Exists("downloads"))
                        {
                            Directory.CreateDirectory("downloads");
                        }
                        destinationFilePath = System.IO.Path.Combine("downloads", botUpdates[0].Message.Document.FileName);
                        fileid = botUpdates[0].Message.Document.FileId;
                        //Console.WriteLine("This is FILE!");
                        DownloadFile(botClient, fileid, destinationFilePath);
                        message = await botClient.SendTextMessageAsync(botUpdates[0].Message.Chat.Id, "File was downloaded");
                    }
                    else if (botUpdates[0].Message.Type == MessageType.Photo)
                    {
                        if (!Directory.Exists("downloads"))
                        {
                            Directory.CreateDirectory("downloads");
                        }
                        destinationFilePath = System.IO.Path.Combine("downloads", "photo_" + DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + ".jpg");
                        var maxSizePhoto = botUpdates[0].Message.Photo.Length - 1;
                        fileid = botUpdates[0].Message.Photo[maxSizePhoto].FileId;
                        //Console.WriteLine("This is PHOTO!");
                        DownloadFile(botClient, fileid, destinationFilePath);
                        message = await botClient.SendTextMessageAsync(botUpdates[0].Message.Chat.Id, "File was downloaded");
                    }

                    if (botUpdates[0].Message.Text == "/listdl")
                    {
                        if (!Directory.Exists("downloads"))
                        {
                            message = await botClient.SendTextMessageAsync(botUpdates[0].Message.Chat.Id, "No any downloaded files");

                        }
                        else
                        {
                            var FilesList = Directory.GetFiles("downloads");
                            string Files = "";
                            foreach (var File in FilesList)
                            {
                                Files += System.IO.Path.GetFileName(File) + "; ";
                            }
                            sendMessage(botClient, botUpdates[0], "Downloaded files: " + Files);
                            //Console.WriteLine("showing downloads");
                        }
                    }
                    System.IO.File.WriteAllText("json.txt", JsonConvert.SerializeObject(updates));
                    GetListUpdates();
                    offset = botUpdates[0].Id + 1;
                }
            }
        }
        /// <summary>
        /// Method for downloading files from telegram
        /// </summary>
        /// <param name="botClient">botClient</param>
        /// <param name="fileid">fileid</param>
        /// <param name="destinationFilePath">path to download</param>
        static async void DownloadFile(TelegramBotClient botClient, string fileid, string destinationFilePath)
        {
            var fileInfo = await botClient.GetFileAsync(fileid);
            var filePath = fileInfo.FilePath;
            FileStream fileStream = System.IO.File.OpenWrite(destinationFilePath);
            await botClient.DownloadFileAsync(
                filePath: filePath,
                destination: fileStream);
        }
        /// <summary>
        /// Send Message activity
        /// </summary>
        /// <param name="botClient"></param>
        /// <param name="botUpdate"></param>
        /// <param name="body">message text</param>
        /// <param name="boolReply">if reply needed</param>
        async void sendMessage(TelegramBotClient botClient, Update botUpdate, string body, bool boolReply = false)
        {
            Message message;
            if (boolReply)
            {
                message = await botClient.SendTextMessageAsync(botUpdate.Message.Chat.Id, body, replyToMessageId: botUpdate.Message.MessageId);
            }
            else
            {
                message = await botClient.SendTextMessageAsync(botUpdate.Message.Chat.Id, body);
            }
        }
        /// <summary>
        /// Choose update from listbox, then send message
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SendButtonClick(object sender, RoutedEventArgs e)
        {
            string token = System.IO.File.ReadAllText("token.txt");
            var botClient = new TelegramBotClient(token);
            Dictionary<string, Update> messages = new Dictionary<string, Update>();
            var selecteditem = listBox.SelectedItem;
                if (selecteditem != null)
                {
                    string key = listBox.SelectedItem.ToString();
                    messages = GetListUpdates();
                    string reply = inputBox.Text;
                    sendMessage(botClient, messages[key], reply, true);
                }
                else
                {
                    MessageBox.Show("Messages wasn't selected");
                }
        }
    }
}
