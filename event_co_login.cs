using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DoumaZone
{
    public partial class event_co_login : Form
    {
        public event_co_login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //select username from DoumaDB where id in (2,4)



            if ( (textBox1.Text == "eventco1" && textBox2.Text == "pass") || (textBox1.Text == "eventco2" && textBox2.Text == "pass") || (textBox1.Text == "eventco3" && textBox2.Text == "pass") )
            {
                event_co_page page = new event_co_page();
                page.Show();
                this.Hide();
            }

            else
                MessageBox.Show("The entered username or password is incorrect. Please try again.");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            welcome_page page = new welcome_page();
            page.Show();
            this.Close();
        }
    }
}
