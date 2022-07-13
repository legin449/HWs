class Program
{
    static void Main()
    {
        //declaring vars and initializing Dictionary
        Dictionary<string, string> PhoneBook = new Dictionary<string, string>();
        string name = "";
        string phone = "";

        //Filling the dict
        while (true)
        {
            Console.WriteLine("Please input name");
            name = Console.ReadLine();
            if (name == "" || name == null)
            {
                break;
            }
            Console.WriteLine("Please input phone number");
            phone = Console.ReadLine();
            if (phone == "" || phone == null)
            {
                break;
            }

            PhoneBook[phone] = (name);

        }

        //Find name by phone
        Console.WriteLine("Press enter to find name by phone number");
        Console.ReadLine();
        Console.WriteLine("Please input phone number");
        phone = Console.ReadLine();

        PhoneBook.TryGetValue(phone, out name);

        Console.WriteLine("the name is " + name);
    }
}