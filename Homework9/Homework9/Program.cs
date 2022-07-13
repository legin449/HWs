using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

class Program
{

    static void Main()
    {
        Console.WriteLine(@"Please input ""exit"" to exit");
        string token = System.IO.File.ReadAllText(@"D:\SkillBox\token.txt");
        var botClient = new TelegramBotClient(token);
        Task.Run(() => ProcessUpdates(botClient));
        var text = "";
        while (text != "exit")
        {
            text = Console.ReadLine();
        }
    }
    /// <summary>
    /// Main workflow for processing updates
    /// </summary>
    /// <param name="botClient"></param>
    /// <returns></returns>
    static async Task ProcessUpdates(TelegramBotClient botClient)
    {
        Update botUpdatesPrev = new Update();
        Message message;
        string destinationFilePath;
        int offset = 0;
        string fileid;

        while (true)
        {
            var botUpdates = await botClient.GetUpdatesAsync(offset: offset);
            var tgUpdatesLen = botUpdates.Length;
            if (tgUpdatesLen != 0)
            {
                if (botUpdates[0].Message.Type == MessageType.Text)
                {
                    if (botUpdates[0].Message.Text == "/start")
                    {
                        Console.WriteLine("message was sent");
                        message = await botClient.SendTextMessageAsync(botUpdates[0].Message.Chat.Id, "Hi, " + botUpdates[0].Message.Chat.FirstName + "\nSend me file for downloading\n/listdl to list downloaded files");
                    }
                }
                if (botUpdates[0].Message.Type == MessageType.Document)
                {
                    if (!Directory.Exists("downloads"))
                    {
                        Directory.CreateDirectory("downloads");
                    }
                    destinationFilePath = Path.Combine("downloads", botUpdates[0].Message.Document.FileName);
                    fileid = botUpdates[0].Message.Document.FileId;
                    Console.WriteLine("This is FILE!");
                    DownloadFile(botClient, fileid, destinationFilePath);
                    message = await botClient.SendTextMessageAsync(botUpdates[0].Message.Chat.Id, "File was downloaded");
                }
                else if (botUpdates[0].Message.Type == MessageType.Photo)
                {
                    destinationFilePath = Path.Combine("downloads", "photo_" + DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + ".jpg");
                    var maxSizePhoto = botUpdates[0].Message.Photo.Length - 1;
                    fileid = botUpdates[0].Message.Photo[maxSizePhoto].FileId;
                    Console.WriteLine("This is PHOTO!");
                    DownloadFile(botClient, fileid, destinationFilePath);
                    message = await botClient.SendTextMessageAsync(botUpdates[0].Message.Chat.Id, "File was downloaded");
                }

                if (botUpdates[0].Message.Text == "/listdl")
                {
                    var FilesList = Directory.GetFiles("downloads");
                    string Files = "";
                    foreach (var File in FilesList)
                    {
                        Files += Path.GetFileName(File) + "; ";
                    }
                    message = await botClient.SendTextMessageAsync(botUpdates[0].Message.Chat.Id, "Downloaded files: " + Files);
                    Console.WriteLine("showing downloads");
                }
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
        await using FileStream fileStream = System.IO.File.OpenWrite(destinationFilePath);
        await botClient.DownloadFileAsync(
            filePath: filePath,
            destination: fileStream);
    }

}