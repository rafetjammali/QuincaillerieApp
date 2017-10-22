using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QuincaillerieApp
{
    public partial class Formproduit : Form
    {
        public Formproduit()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new FormaffichePoduit().Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new Formmodifierproduit().Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new FormajoutProduit().Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            new navigation().navigateHome();
            this.Hide();
        }

        private void label12_Click(object sender, EventArgs e)
        {
            new navigation().navigateMain();
            this.Hide();
        }
    }
}
