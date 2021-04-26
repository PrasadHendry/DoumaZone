using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DoumaZone
{
    public partial class admin_login : Form
    {
        public admin_login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            //select username from DoumaDB where id == 1



            if (textBox1.Text == "admin" && textBox2.Text == "admin")
            {
                admin_page admin = new admin_page();
                admin.Show();
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
