using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;


namespace HW11
{
    class Consult : Changes
    {
        public string Name = "Консультант";
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
                client.DateOfChanges = newClient.DateOfChanges;
                client.TypeOfChanges = newClient.TypeOfChanges;
                client.WasChanged = newClient.WasChanged;
                client.ChangedBy = newClient.ChangedBy;
            }
            else
            {
                WasUpdated = false;
            }
            return WasUpdated;
        }
        Dictionary<string, string> Changes.WasChanged(Client originClient, Client newClient, string TypeOfChanges)
        {
            #region
            //List<string> origClientList = originClient.Serialization().Split(';').ToList<string>(); ;
            //List<string> newClientList = newClient.Serialization().Split(';').ToList<string>();
            //foreach(string s in origClientList)
            //{
            //    var index = origClientList.IndexOf(s);
            //    if (origClientList[index] == newClientList[index])
            //    {

            //    }
            //}
            #endregion
            Dictionary<string, string> ChangesInfo = new Dictionary<string, string>();
            string Changes = "";
            string Date = DateTime.Now.ToString();
            if(!(originClient.FirstName == newClient.FirstName))
            {
                Changes += "Имя было изменено;";
            }
            if(!(originClient.LastName == newClient.LastName))
            {
                Changes += "Фамилия была изменена;";
            }
            if(!(originClient.MiddleName == newClient.MiddleName))
            {
                Changes += "Отчество было изменено;";
            }
            if(!(originClient.PhoneNumber == newClient.PhoneNumber))
            {
                Changes += "Номер телефона был изменен;";
            }
            if(!(originClient.Passport != newClient.Passport))
            {
                Changes += "Паспорт был изменен;";
            }
            Changes += $"\nКем были выполнены изменения:\n{this.Name}\nТип изменений: {TypeOfChanges}\nКогда было изменено: {Date}";
            ChangesInfo["Дата"] = Date;
            ChangesInfo["Изменения"] = Changes;
            ChangesInfo["Тип изменений"] = TypeOfChanges;
            ChangesInfo["Сотрудник"] = this.Name;
            return ChangesInfo;
        }
    }
}
