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
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=G:\Douma(Scrum)\Database\DoumaDB_log\DoumaDB.mdf;Integrated Security=True;Connect Timeout=30");

        public admin_login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();

            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Accounts where Id = 1 and password = '" + textBox2.Text + "'";
            cmd.ExecuteNonQuery();

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            if (dt.Rows.Count != 0)
            {
                cmd.CommandText = "update Accounts set login = 1 where Id = 1 ";
                cmd.ExecuteNonQuery();

                con.Close();
                
                admin_page admin = new admin_page();
                admin.Show();
                this.Hide();
            }

            else
                MessageBox.Show("The entered password is incorrect. Please try again.");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            guest_page page = new guest_page();
            page.Show();
            this.Close();
        }
    }
}
