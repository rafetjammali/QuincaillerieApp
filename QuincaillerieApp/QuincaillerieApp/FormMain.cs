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
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Formvente().Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new Formproduit().Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new Formfornisseur().Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            new credit.Formcredit().Show();
            this.Hide();
        }
        
    

        private void button7_Click(object sender, EventArgs e)
        {
            new client.FormClient().Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            new navigation().navigateHome();
            this.Hide();
        }
    }
}
