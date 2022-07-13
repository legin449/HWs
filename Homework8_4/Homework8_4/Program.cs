using System.Xml.Linq;
using Homework8_4;
class Program
{
    static void Main()
    {
        //getting all info about contact

        Contact contact = new Contact();
        Console.WriteLine("Please input name of contact");
        contact.Name = Console.ReadLine();
        XElement contactXML = new XElement("Person",contact.Name);

        Console.WriteLine("Please input street of contact");
        contact.Street = Console.ReadLine();
        Console.WriteLine("Please input house number of contact");
        contact.House = Console.ReadLine();
        Console.WriteLine("Please input flat number of contact");
        contact.Apartement = Console.ReadLine();
        contactXML.Add(new XElement("Address",
                new XElement("Street", contact.Street),
                new XElement("HouseNumber", contact.House),
                new XElement("FlatNumber", contact.Apartement)));
        Console.WriteLine("Please input phone number of contact");
        contact.PhoneNumber = Console.ReadLine();
        Console.WriteLine("Please input flat phone of contact");
        contact.HomePhoneNumber = Console.ReadLine();
        contactXML.Add(new XElement("Phones",
                new XElement("MobilePhone", contact.PhoneNumber),
                new XElement("FlatPhone", contact.HomePhoneNumber)));
        //writing xml to file
        Console.WriteLine("XML is \n");
        File.WriteAllText("xml.txt", contactXML.ToString());
        Console.WriteLine(contactXML);

    }
}