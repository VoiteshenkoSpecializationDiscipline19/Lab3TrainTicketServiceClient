using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Ticket
    {
        public int Id { set; get; }
        public string From { set; get; }
        public string To { set; get; }
        public string When { set; get; }
        public double Price { set; get; }
    }
}
