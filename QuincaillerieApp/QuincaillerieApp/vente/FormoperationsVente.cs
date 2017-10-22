using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace QuincaillerieApp
{
    public partial class FormoperationsVente : Form
    {
        public FormoperationsVente()
        {
            InitializeComponent();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            new navigation().navigateHome();
            this.Hide();
        }

        private void label12_Click(object sender, EventArgs e)
        {
            new navigation().navigateMain();
            this.Hide();
        }

        //methode for the select 
        private void Select(string text)
        {
            dataBase data = new dataBase();
            SqlConnection connection = data.connectiondata();
            try
            {
                connection.Open();
                //read from the data base
                SqlDataAdapter query = new SqlDataAdapter(text, connection);
                DataTable dataTable = new DataTable();
                query.Fill(dataTable);
                DataGridView.Visible = true;
                DataGridView.DataSource = dataTable;
            }
            catch (SqlException expectation)
            {
                MessageBox.Show(expectation.ToString());
            }
            finally { connection.Close(); }
        }

        DateTime curDate;
        private void btnSearch_Click(object sender, EventArgs e)
        {
            

            if (txtSearch.Text == "")
            {
                MessageBox.Show("Tu as oublie un espace vide");
            }
            else if (DateTime.TryParse(txtSearch.Text.Trim(), out curDate) == false)
            {
                MessageBox.Show("vous voulez -vous vérifier la date ");
            }
            else
            {
                string querry = "Select p.nomproduit,t.prix,t.Quantite,t.Login from produit p,vente t where data='" + curDate + "' and t.idproduit = p.idproduit";
                Select(querry);
                button1.Visible = true;
                button2.Visible = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            txtSearch.Text = null;
            DataGridView.DataSource = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string querry = "Select SUM(Quantite) from vente where data='" + curDate + "'";
            Select(querry);
        }
 
        private void button2_Click(object sender, EventArgs e)
        {
            string querry = "Select SUM(prix) from vente where data='" + curDate + "'";
            Select(querry);
        }
    }
}
