using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class UserEmailOption
    {
        public List<string>? ToEmails { get; set; }
        public string? Subject { get; set; }
        public string? Body { get; set; }
        public List<KeyValuePair<string,string>>? PlaceHolders { get; set; } 
    }
}
