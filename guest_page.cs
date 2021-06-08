using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DoumaZone
{
    public partial class guest_page : Form
    {
        public guest_page()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            welcome_page wp = new welcome_page();
            wp.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EventView view = new EventView();
            this.Hide();
            view.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Info inf = new Info();
            inf.Show();
        }
    }
}
