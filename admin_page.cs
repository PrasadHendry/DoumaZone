using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace DoumaZone
{
    public partial class admin_page : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\siddh\Documents\DoumaDB.mdf;Integrated Security=True;Connect Timeout=30");

        public admin_page()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update Accounts set login = 0 where Id = 1 ";
                cmd.ExecuteNonQuery();

                con.Close();

                guest_page gp = new guest_page();
                gp.Show();
                this.Hide();
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                con.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(textBox4.Text, @"^\d{10}$"))
            {
                MessageBox.Show("Invalid phone number");
                return;
            }

            try
            {
                con.Open();

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "insert into Accounts values('" + textBox1.Text + "', '" + textBox2.Text + "', '" + textBox3.Text + "',0 , '" + textBox4.Text + "')";
                cmd.ExecuteNonQuery();

                con.Close();

                MessageBox.Show("Event Co-ordinator added successfully.");
                display();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                con.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;

                if (textBox1.Text == "1")
                    cmd.CommandText = "update Accounts set phone_no = '" + textBox4.Text + "' where Id = '" + textBox1.Text + "'";

                else
                    cmd.CommandText = "update Accounts set name = '" + textBox2.Text + "', phone_no = '" + textBox4.Text + "' where Id = '" + textBox1.Text + "'";
                
                cmd.ExecuteNonQuery();

                con.Close();

                display();
                MessageBox.Show("Record Successfully Updated");
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                con.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "1")
            {
                MessageBox.Show("You can't delete admin account.");
                return;
            }

            try
            {
                con.Open();

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "delete from Accounts where Id = '" + textBox1.Text + "'";
                cmd.ExecuteNonQuery();

                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                if (dt.Rows.Count != 0)
                    MessageBox.Show("Record Deleted successfully");

                else
                    MessageBox.Show("There is no record to be deleted");

                con.Close();
                display();
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                con.Close();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from Accounts where Id = '" + textBox1.Text + "'";
                cmd.ExecuteNonQuery();

                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                dataGridView1.DataSource = dt;

                con.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                con.Close();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            UpdateAdminPass pass = new UpdateAdminPass();
            pass.Show();
        }

        public void display()
        {
            try
            {
                con.Open();

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "select * from Accounts";
                cmd.ExecuteNonQuery();

                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(dt);
                dataGridView1.DataSource = dt;

                DataTable dt1 = new DataTable();
                SqlDataAdapter da1 = new SqlDataAdapter(cmd);

                cmd.CommandText = "select * from event";
                cmd.ExecuteNonQuery();

                da1.Fill(dt1);
                dataGridView2.DataSource = dt1;

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                con.Close();
            }
        }

        private void admin_page_Load(object sender, EventArgs e)
        {
            display();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            display();
        }
    }
}
