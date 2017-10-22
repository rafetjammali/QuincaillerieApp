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
    public partial class FormaffichePoduit : Form
    {
        public FormaffichePoduit()
        {
            InitializeComponent();
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

        private void buttonAnnuler_Click(object sender, EventArgs e)
        {
            txtSearch1.Text = null;
            txtSearch2.Text = null;
            DataGridView.DataSource = null;//chck if it works
        }

        //fonction pour la select et recherche
        private void getdata(string text)
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch1.Text == "")
            {
                MessageBox.Show("La boite de recherche par code est vide");
            }
            else
            {
                string text = "Select nomproduit,unite,quantiteproduit,prixvente,prixventemin from produit where idproduit='" + txtSearch1.Text.Trim() + "'";
                getdata(text);
            }
        }

        private void btnSearch2_Click(object sender, EventArgs e)
        {
            if (txtSearch2.Text == "")
            {
                MessageBox.Show("La boite de recherche par numero est vide");
            }
            else
            {
                string text = "Select nomproduit,unite,quantiteproduit,prixvente,prixventemin from produit where categorie='" + txtSearch2.Text.Trim() + "'";
                getdata(text);
            }
        }

        private void txtSearch1_TextChanged(object sender, EventArgs e)
        {
            //write nothing
        }
    }
}
