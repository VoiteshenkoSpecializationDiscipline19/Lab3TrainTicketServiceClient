using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Tickets.Models
{
    public class TicketsContext : DbContext
    {
        public TicketsContext (DbContextOptions<TicketsContext> options)
            : base(options)
        {
        }

        public DbSet<Tickets.Models.Ticket> Ticket { get; set; }
    }
}
