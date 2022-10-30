using System;
using System.Collections.Generic;
using System.Text;

namespace ChatAPP.Models
{
    public class Message
    {
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string Msg { get; set; }

        public string visibility { get; set; }
        public string Revisibility { get; set; }


    }
}
