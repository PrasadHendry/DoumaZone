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
    }
}
