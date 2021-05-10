﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DoumaZone
{
    public partial class event_co_page : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\siddh\Documents\DoumaDB.mdf;Integrated Security=True;Connect Timeout=30");

        public event_co_page()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();

            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update Accounts set login = 0 where Id = '" + event_co_login.id + "' ";
            cmd.ExecuteNonQuery();

            con.Close();

            guest_page gp = new guest_page();
            gp.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UpdateEventCoPass pass = new UpdateEventCoPass();
            pass.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update event set event_cat = '" + comboBox1.Text + "', event_name = '" + textBox3.Text + "', event_name = '" + textBox4.Text + "', begin_date = '" + dateTimePicker1.Value + "', end_date = '" + dateTimePicker2.Value + "' where event_id = '" + textBox1.Text + "'";
                cmd.ExecuteNonQuery();

                con.Close();

                display();
                MessageBox.Show("Record Successfully Updated");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "delete from event where event_id = '" + textBox1.Text + "'";

                if (cmd.ExecuteNonQuery() != 0)
                    MessageBox.Show("Event deleted successfully!!");

                else
                    MessageBox.Show("There is no event to be deleted...?");

                con.Close();
                display();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //Search by event_co_id

            try
            {
                con.Open();

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from event where event_co_id = '" + event_co_login.id + "'";
                cmd.ExecuteNonQuery();

                con.Close();
                display();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "insert into event values('" + textBox1.Text + "', '" + comboBox1.Text + "', '" + textBox3.Text + "', '" + textBox4.Text + "', '" + dateTimePicker1.Value + "', '" + dateTimePicker2.Value + "', '" + event_co_login.id + "' )";
                cmd.ExecuteNonQuery();

                con.Close();

                MessageBox.Show("Event added successfully.");
                display();
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            display();
        }

        private void event_co_page_Load(object sender, EventArgs e)
        {
            label4.Text = "Event Co ID: " + event_co_login.id;
            display();
        }

        public void display()
        {
            try
            {
                con.Open();

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "select * from event";
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
            }
        }
    }
}
