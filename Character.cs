using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yggdrasil
{
    class Character
    {
        public string name { get; set; }
        public int ID { get; set; }
        public string description { get; set; }
        public int[] skills { get; set; }
        public int startstory { get; set; }    
    }
}
