using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMail_Campaign
{
    public class EmailSentlog
    {
        public string logtime { get; set; }
        public string contactname { get; set; }
        public string email { get; set; }
        public string status { get; set; }
        public string message { get; set; }
    }
}
