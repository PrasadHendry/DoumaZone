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
    public partial class UpdateEventCoPass : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=G:\Douma(Scrum)\Database\DoumaDB_log\DoumaDB.mdf;Integrated Security=True;Connect Timeout=30");

        public UpdateEventCoPass()
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
                cmd.CommandText = "select * from Accounts where Id = '" + textBox4.Text + "' and password = '" + textBox2.Text + "' ";
                cmd.ExecuteNonQuery();

                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                if ( dt.Rows.Count != 0 )
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "update Accounts set password = '" + textBox3.Text + "', username = '" + textBox1.Text + "' where Id = '" + textBox4.Text + "' ";
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Password and Username updated successfully");
                }

                else
                    MessageBox.Show("Password was incorrect");

                con.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void UpdateEventCoPass_Load(object sender, EventArgs e)
        {
            textBox4.Text = event_co_login.id.ToString();
        }
    }
}
