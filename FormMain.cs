﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;


namespace Ticket_Reservation
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            FillComboBox();
            FillHistoryInListBox();
        }

        private void FillComboBox()
        {
            using (StreamReader file = new StreamReader("data/plane flights.txt"))
            {
                while (!file.EndOfStream)
                {
                    string s = file.ReadLine();
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
        }

        private void FillHistoryInListBox()
        {
            using (StreamReader file = new StreamReader("data/search history.txt"))
            {
                while (!file.EndOfStream)
                {
                    string s = file.ReadLine();
                    listBoxTickets.Items.Add(s);
                }
            }
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            string townFirst = comboBoxFrom.Text;
            string townSecond = comboBoxTo.Text;
            //string dateTime = dateTimePicker1.Text;
            if (townIsNullOrEmty(townFirst, townSecond))
                MessageBox.Show("Заполните поля ввода городом\nпрежде чем перейти к покупкам!", "Error!");
            else
                getTicketFromFIle(townFirst, townSecond);

        }

        private void getTicketFromFIle(string townFrom, string townTo)
        {
            bool flag = true;
            using (StreamReader file = new StreamReader("data/plane flights.txt"))
            {
                while (!file.EndOfStream)
                {
                    string str = file.ReadLine();
                    if (str.Contains(townFrom + "~" + townTo))
                    {
                        string ticketInfo = file.ReadLine();

                        if (ticketInfo.Contains('$')) flag = false;

                        while (ticketInfo.Contains('$'))
                        {
                            string[] data = ticketInfo.Split('$');
                            string date = data[0];
                            decimal cost = Convert.ToDecimal(data[1]);
                            AddTicketToListBox(townFrom, townTo, date, cost);

                            ticketInfo = file.ReadLine();
                        }
                    }
                }
            }
            if (flag) MessageBox.Show($"На данный момент нет билетов из {townFrom}, в {townTo}!");
        }

        private void администраторToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAdim formAdim = new FormAdim();
            formAdim.Show();
        }

        private void AddTicketToListBox(string from, string to, string date, decimal cost)
        {
            Ticket ticket = new Ticket(from, to, date, cost);
            listBoxTickets.Items.Add(ticket);
        }

        private bool townIsNullOrEmty(string town1, string town2)
        {
            return string.IsNullOrEmpty(town1) || string.IsNullOrEmpty(town2);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBoxTickets.Items.Clear();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            writeSearchHistoryToFile("data/search history.txt");
        }

        private void writeSearchHistoryToFile(string fileName)
        {
            using (StreamWriter file = new StreamWriter(fileName))
            {
                foreach (var item in listBoxTickets.Items)
                {
                    file.WriteLine(item);
                }
            }
        }

        private void buttonPay_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Оплата пока на работает :(");
        }

        private void buttonAddToBasket_Click(object sender, EventArgs e)
        {
            string purchasedTickets = listBoxTickets.SelectedItem.ToString();
            listBoxBasket.Items.Add(purchasedTickets);
        }

        private void buttonRemoveFromBasket_Click(object sender, EventArgs e)
        {
            var selectedItem = listBoxBasket.SelectedItem;
            listBoxBasket.Items.Remove(selectedItem);
        }
    }
}
