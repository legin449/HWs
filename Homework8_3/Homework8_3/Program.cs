class Program
{
    static void Main()
    {
        //initializing HashSet
        HashSet<string> values = new HashSet<string>();
        string number = "";

        //Filling HashSet
        while (true)
        {
            Console.WriteLine("Please input number\nif you want to stop, please input empty string\n");
            number = Console.ReadLine();
            if (number == "")
            {
                break;
            }else if (values.Contains(number))
            {
                Console.WriteLine("Set already contains this value");
            }
            else
            {
                values.Add(number);
            }
        }
    }
}