using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket_Reservation
{
    // Класс билета
    public class Ticket
    {
        public Ticket(string firtstTown, string secondTown, string date)
        {
            FirtstTown = firtstTown;
            SecondTown = secondTown;
            Date = date;
        }

        public string FirtstTown { get; set; }
        public string SecondTown { get; set; }
        public string Date { get; set; }
        public decimal Cost { get; set; }
        public int Seat { get; set; }
    }
}
