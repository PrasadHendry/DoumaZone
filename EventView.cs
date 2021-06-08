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
    public partial class EventView : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=G:\Douma(Scrum)\Database\DoumaDB_log\DoumaDB.mdf;Integrated Security=True;Connect Timeout=30");

        public EventView()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            guest_page gp = new guest_page();
            gp.Show();
            this.Close();
        }

        //styling
        void StyleDatagridview()
        {
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.RoyalBlue;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView1.BackgroundColor = Color.FromArgb(30, 30, 30);
            dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;//optional
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("MS Reference Sans Serif", 10);
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(37, 37, 38);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
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

        private void EventView_Load(object sender, EventArgs e)
        {
            display();
            StyleDatagridview();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            display();
            textBox1.Text = "";
            label9.Visible = false;
            label2.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(textBox1.Text, @"^([0][1-9]|10|11|12)-([0][1-9]|[12]{1}[0-9]|30|31)-(\d{4})$"))
            {
                MessageBox.Show("Invalid date. Format: mm-dd-yyyy");
                return;
            }

            try
            {
                con.Open();

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from event where begin_date = '" + textBox1.Text + "'";
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

            label9.Visible = false;
            label2.Visible = false;
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            label9.Visible = true;
            label2.Visible = true;
        }
    }
}
