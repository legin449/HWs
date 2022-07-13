using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace HW11
{
    internal class Manager : Consult
    {
        public override void Init(TextBox FirstName, TextBox LastName, TextBox MiddleName, TextBox PhoneNumber, TextBox Passport)
        {
            base.Init(FirstName, LastName, MiddleName, PhoneNumber, Passport);
            FirstName.IsReadOnly = false;
            LastName.IsReadOnly = false;
            MiddleName.IsReadOnly = false;
            Passport.IsReadOnly = false;
        }
        public override void ShowClientData(Client client, TextBox FirstName, TextBox LastName, TextBox MiddleName, TextBox PhoneNumber, TextBox Passport)
        {
            base.ShowClientData(client, FirstName, LastName, MiddleName, PhoneNumber, Passport);
            Passport.Text = client.Passport;
        }
        public override bool ChangeClientData(Client client, Client newClient)
        {
            base.ChangeClientData(client, newClient);
            client.FirstName = newClient.FirstName;
            client.LastName = newClient.LastName;
            client.MiddleName = newClient.MiddleName;
            client.Passport = newClient.Passport;
            return base.ChangeClientData(client, newClient);

        }
    }
}
