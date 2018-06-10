using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMail_Campaign
{
    [Serializable]
    public class Template
    {
        public string TemplateName { get; set; }
        public string Subject{ get; set; }
        public string EmailContext{ get; set; }

        
    }
}
