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
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            string townFirst = comboBoxFrom.Text;
            string townSecond = comboBoxTo.Text;
            string dateTime = dateTimePicker1.Text;
            
        }
    }
}
