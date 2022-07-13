class Program
{
    static void Main()
    {
        //decaring list and random
        List<int> list = new List<int>();
        Random random = new Random();

        //Filling and printing list
        Console.WriteLine("List of ints:");
        for (int i = 0; i < 100; i++)
        {
            list.Add(random.Next(0, 100));
            Console.Write(" " + list[i].ToString() + " ");
        }
        Console.WriteLine("\nList size before deleting is " + list.Count.ToString() + "\nPlease click enter to proceed");
        Console.ReadLine();
        //Console.ReadLine();
        int listCount = list.Count;
        Console.WriteLine("Deleting ints with condition i > 25 And i < 50");
        //Removing ints with condition
        for (int i = listCount - 1; i > 0; i--)
        {
            if (list[i] > 25 && list[i] < 50)
            {
                list.Remove(list[i]);
            }
        }
        Console.WriteLine("\nList size now is " + list.Count.ToString() + "\nPlease click enter to proceed");
        Console.ReadLine();
        Console.WriteLine("New list of ints");
        listCount = list.Count;
        //Print new list
        for (int i = 0; i < listCount; i++)
        {
            Console.Write(" " + list[i].ToString() + " ");
        }
    }
}

