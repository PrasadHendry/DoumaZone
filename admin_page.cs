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
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=G:\Douma(Scrum)\Database\DoumaDB_log\DoumaDB.mdf;Integrated Security=True;Connect Timeout=30");

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
                    cmd.CommandText = "update Accounts set username = '" + textBox2.Text + "', phone_no = '" + textBox4.Text + "' where Id = '" + textBox1.Text + "'";
                
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
                cmd.CommandText = "select * from Accounts where Id = '" + textBox1.Text + "'";
                cmd.ExecuteNonQuery();

                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                if (dt.Rows.Count != 0)
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select * from event where event_co_id = '" + textBox1.Text + "'";
                    cmd.ExecuteNonQuery();

                    da.Fill(dt);

                    if (dt.Rows.Count != 0)
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "delete from event where event_co_id = '" + textBox1.Text + "'";
                        cmd.ExecuteNonQuery();
                    }

                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "delete from Accounts where Id = '" + textBox1.Text + "'";
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Record Deleted successfully");
                }

                else
                {
                    MessageBox.Show("There is no record to be deleted");
                }
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

                if (dt.Rows.Count == 0)
                    MessageBox.Show("No such record to be found");

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

        //styling
        void StyleDatagridview()
        {
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.SeaGreen;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView1.BackgroundColor = Color.FromArgb(255, 140, 26);

            dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;//optional
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("MS Reference Sans Serif", 10);
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(255, 140, 26);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            
            //-
            
            dataGridView2.BorderStyle = BorderStyle.None;
            dataGridView2.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView2.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView2.DefaultCellStyle.SelectionBackColor = Color.SeaGreen;
            dataGridView2.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView2.BackgroundColor = Color.FromArgb(255, 140, 26);

            dataGridView2.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;//optional
            dataGridView2.EnableHeadersVisualStyles = false;
            dataGridView2.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font("MS Reference Sans Serif", 10);
            dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(255, 140, 26);
            dataGridView2.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
        }

        private void admin_page_Load(object sender, EventArgs e)
        {
            display();
            StyleDatagridview();

        }

        private void button7_Click(object sender, EventArgs e)
        {
            display();
            StyleDatagridview();
        }
    }
}
