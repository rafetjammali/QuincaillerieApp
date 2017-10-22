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
    public partial class Formvente : Form
    {
        public Formvente()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new vente.vente().Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new FormoperationsVente().Show();
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
