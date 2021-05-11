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
    public partial class event_co_login : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\siddh\Documents\DoumaDB.mdf;Integrated Security=True;Connect Timeout=30");

        public static int id = 0;

        public event_co_login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            // and username != 'admin'
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Accounts where username = '" + textBox1.Text + "' and password = '" + textBox2.Text + "' ";
            cmd.ExecuteNonQuery();

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            if ( dt.Rows.Count != 0 )
            {
                cmd.CommandText = "select Id from Accounts where username = '" + textBox1.Text + "' ";
                cmd.ExecuteNonQuery();

                da.Fill(dt);
                foreach (DataRow row in dt.Rows)
                    id = Convert.ToInt32(row["Id"].ToString());

                con.Close();

                event_co_page page = new event_co_page();
                page.Show();
                this.Hide();
            }

            else
                MessageBox.Show("The entered username or password is incorrect. Please try again.");

            con.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            welcome_page page = new welcome_page();
            page.Show();
            this.Hide();
        }
    }
}
