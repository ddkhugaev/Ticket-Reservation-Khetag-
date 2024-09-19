using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace Ticket_Reservation
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            StreamReader sr = new StreamReader("data/plane flights.txt");

            while (!sr.EndOfStream)
            {
                string s = sr.ReadLine();
                if (s.Contains('~'))
                {
                    string[] input = s.Split('~');
                    if (!comboBoxFrom.Items.Contains(input[0]))
                    {
                        comboBoxFrom.Items.Add(input[0]);
                    }
                    if (!comboBoxTo.Items.Contains(input[1]))
                    {
                        comboBoxTo.Items.Add(input[1]);
                    }
                }
            }
            sr.Close();

        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            string townFirst = comboBoxFrom.Text;
            string townSecond = comboBoxTo.Text;
            //string dateTime = dateTimePicker1.Text;
            getTicket(townFirst, townSecond);

        }

        private void getTicket(string from, string to)
        {
            Ticket ticket = null;
            using (StreamReader file = new StreamReader("data/plane flights.txt"))
            {
                while (!file.EndOfStream)
                {
                    string str = file.ReadLine();
                    if (str.Contains(from + "~" + to))
                    {
                        string[] ticketInfo = file.ReadLine().Split('$');
                        string date = ticketInfo[0];
                        decimal cost = Convert.ToDecimal(ticketInfo[1]);
                        ticket = new Ticket(from, to, date, cost);
                        listBoxTickets.Items.Add(ticket);
                    }
                }
            }
        }

        private void администраторToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAdim formAdim = new FormAdim();
            formAdim.Show();
        }
    }
}
