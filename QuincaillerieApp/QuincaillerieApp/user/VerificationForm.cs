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
    public partial class VerificationForm : Form
    {
        public VerificationForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            new navigation().navigateHome();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (TextBoxCode.Text == "")
            {
                MessageBox.Show("Code Vide");
            }
            else if (TextBoxCode.Text == "0000")
            {
                new ForminscriptionForm().Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("le code est faux");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TextBoxCode.Text = null;
        }
    }
}
