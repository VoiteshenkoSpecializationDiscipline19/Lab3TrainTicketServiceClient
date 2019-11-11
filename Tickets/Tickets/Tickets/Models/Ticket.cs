using System;
using System.ComponentModel.DataAnnotations;
namespace Tickets.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string From { get; set; }
        public string To { get; set; }

        [DataType(DataType.Date)]
        public DateTime When { get; set; }
        
        public decimal Price { get; set; }
    }
}
