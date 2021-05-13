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
    public partial class event_co_page : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=G:\Douma(Scrum)\Database\DoumaDB_log\DoumaDB.mdf;Integrated Security=True;Connect Timeout=30");

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
            if (event_co_login.id == 1)
            {
                UpdateAdminPass pass1 = new UpdateAdminPass();
                pass1.Show();
            }

            else
            {
                UpdateEventCoPass pass = new UpdateEventCoPass();
                pass.Show();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update event set event_cat = '" + comboBox1.Text + "', event_name = '" + textBox3.Text + "', event_name = '" + textBox4.Text + "', begin_date = '" + textBox2.Text + "', end_date = '" + textBox5.Text + "' where event_id = '" + textBox1.Text + "'";
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
            try
            {
                con.Open();

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "delete from event where event_id = '" + textBox1.Text + "' and event_co_id = '" + event_co_login.id + "'";

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
                cmd.CommandText = "select * from event where event_co_id = '" + event_co_login.id + "'";
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
            if (!Regex.IsMatch(textBox2.Text, @"^([0][1-9]|10|11|12)-([0][1-9]|[12]{1}[0-9]|30|31)-(\d{4})$") || !Regex.IsMatch(textBox5.Text, @"^([0][1-9]|10|11|12)-([0][1-9]|[12]{1}[0-9]|30|31)-(\d{4})$"))
            {
                MessageBox.Show("Invalid date. Format: mm-dd-yyyy");
                return;
            }

            try
            {
                con.Open();

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "select * from event where event_co_id = '" + event_co_login.id + "' ";
                cmd.ExecuteNonQuery();

                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                if (dt.Rows.Count == 5)
                {
                    MessageBox.Show("You have reached the max number of events.");
                    con.Close();
                    return;
                }

                cmd.CommandText = "insert into event values('" + textBox1.Text + "', '" + comboBox1.Text + "', '" + textBox3.Text + "', '" + textBox4.Text + "', '" + textBox2.Text + "', '" + textBox5.Text + "', '" + event_co_login.id + "' )";
                cmd.ExecuteNonQuery();

                con.Close();

                MessageBox.Show("Event added successfully.");
                display();
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                con.Close();
            }
        }

        //styling
        void StyleDatagridview()
        {
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.SeaGreen;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView1.BackgroundColor = Color.FromArgb(30, 30, 30);
            //dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;//optional
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("MS Reference Sans Serif", 10);
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(37, 37, 38);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            display();
            StyleDatagridview();
        }

        private void event_co_page_Load(object sender, EventArgs e)
        {
            label4.Text = "Event Co ID: " + event_co_login.id;
            display();
            StyleDatagridview();
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
                con.Close();
            }
        }
    }
}
