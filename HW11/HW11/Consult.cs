using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;


namespace HW11
{
    class Consult
    {
        /// <summary>
        /// Initialization of client for displaying on window
        /// </summary>
        /// <param name="FirstName"></param>
        /// <param name="LastName"></param>
        /// <param name="MiddleName"></param>
        /// <param name="PhoneNumber"></param>
        /// <param name="Passport"></param>
        public virtual void Init(TextBox FirstName, TextBox LastName, TextBox MiddleName, TextBox PhoneNumber, TextBox Passport)
        {
            FirstName.IsReadOnly = true;
            LastName.IsReadOnly = true;
            MiddleName.IsReadOnly = true;
            PhoneNumber.IsReadOnly = false;
            Passport.IsReadOnly = true;
            
        }
        /// <summary>
        /// Showing data about client
        /// </summary>
        /// <param name="client"></param>
        /// <param name="FirstName"></param>
        /// <param name="LastName"></param>
        /// <param name="MiddleName"></param>
        /// <param name="PhoneNumber"></param>
        /// <param name="Passport"></param>
        public virtual void ShowClientData(Client client, TextBox FirstName, TextBox LastName, TextBox MiddleName, TextBox PhoneNumber, TextBox Passport)
        {
            if (!string.IsNullOrEmpty(client.Passport))
            {
                Passport.Text = "**********";
                FirstName.Text = client.FirstName;
                LastName.Text = client.LastName;
                MiddleName.Text = client.MiddleName;
                PhoneNumber.Text = client.PhoneNumber;
            }
        }
        /// <summary>
        /// Changing data about client (only phone number)
        /// </summary>
        /// <param name="client"></param>
        /// <param name="newClient"></param>
        /// <returns></returns>
        public virtual bool ChangeClientData(Client client, Client newClient)
        {
            bool WasUpdated = false;
            if (!string.IsNullOrEmpty(newClient.PhoneNumber))
            {
                client.PhoneNumber = newClient.PhoneNumber;
                WasUpdated = true;

            }
            else
            {
                WasUpdated = false;
            }
            return WasUpdated;
        }
    }
}
