using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW11
{
    internal interface Changes
    {
        Dictionary<string,string> WasChanged(Client originClient, Client newClient, string TypeOfChanges, string worker);
    }
}
