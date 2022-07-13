using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework7
{
    public class Functions
    {
        /// <summary>
        /// Getting info from user about worker
        /// </summary>
        /// <returns>list of strings (name, age, height, date of birth, place of birth)</returns>
        public static List<string> GetInfo()
        {
            string name, age, height, dateOfBirth, placeOfBirth;
            Console.WriteLine("Please type new name");
            name = Console.ReadLine();
            Console.WriteLine("Please type new age");
            age = Console.ReadLine();
            Console.WriteLine("Please type new height");
            height = Console.ReadLine();
            Console.WriteLine("Please type new date of birth");
            dateOfBirth = Console.ReadLine();
            Console.WriteLine("Please type new place of birth");
            placeOfBirth = Console.ReadLine();
            List<string> info = new List<string>() {name, age, height, dateOfBirth, placeOfBirth};
            return info;
        }
    }
}
