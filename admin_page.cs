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
    public partial class admin_page : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\siddh\Documents\DoumaDB.mdf;Integrated Security=True;Connect Timeout=30");

        public admin_page()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            guest_page gp = new guest_page();
            gp.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from event_co_acc";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }

            catch(Exception ex)
            {
                MessageBox.Show("Unable to display the table.");
            }

            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "insert into event_co_acc values ( '" + Convert.ToInt32(textBox1.Text) + "', '" + textBox2.Text + "', '" + Convert.ToInt32(textBox3.Text) + "', '" + textBox4.Text + "', '" + Convert.ToInt32(textBox5.Text) + "')";
                cmd.ExecuteNonQuery();
                MessageBox.Show("Event added successfully.");
            }

            catch (Exception ex)
            {
                MessageBox.Show("Unable to add the event.");
                MessageBox.Show(ex.ToString());
            }

            con.Close();
        }
    }
}
