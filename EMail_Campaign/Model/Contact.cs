using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMail_Campaign
{
    [Serializable]
    public class Contact
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string City { get; set; }
        public string State{ get; set; }
        public string Zip{ get; set; }
        public string Age{ get; set; }

    }
}
