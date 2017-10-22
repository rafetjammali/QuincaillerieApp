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
    public partial class Formmodifierproduit : Form
    {
        public Formmodifierproduit()
        {
            InitializeComponent();
        }

        private void btnAnnuler_Click(object sender, EventArgs e)
        {
            txtSearch.Text = null;
            DataGridView.DataSource = null;
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text == "")
            {
                MessageBox.Show("La boite de recherche est vide");
            }
            else
            {
                dataBase data = new dataBase();
                SqlConnection connection = data.connectiondata();
                try
                {
                    connection.Open();
                    //read from the data base
                    SqlDataAdapter query = new SqlDataAdapter("Select quantiteproduit,prixvente,prixventemin from produit where idproduit='" + txtSearch.Text.Trim() + "'", connection);
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
        }
        private void updatedata(string text)
        {
            dataBase data = new dataBase();
            SqlConnection connection = data.connectiondata();
            try
            {
                SqlCommand query = new SqlCommand(text, connection);
                connection.Open();
                query.ExecuteNonQuery();

            }
            catch (SqlException expectation)
            {
                MessageBox.Show(expectation.ToString());
            }
            finally { connection.Close(); }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            decimal quantite;

            if (quantitetextBox.Text == "")
            {
                MessageBox.Show("La nouvelle Quantite est vide");
            }//verifie si la quantite n est pa decimal
            else if(decimal.TryParse(quantitetextBox.Text,out quantite)==false)
            {
             MessageBox.Show("vous voulez -vous vérifier la nouvelle quantite");
            }
            else
            {
              string Text = "Update produit set Quantiteproduit= " + quantite + " where idproduit='" + txtSearch.Text.Trim() + "'";
              updatedata(Text);
              MessageBox.Show("La Quantite a ete mise a jours");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            decimal prix;

            if (prixtextBox.Text == "")
            {
                MessageBox.Show("Le prix est vide");
            }
            else if (decimal.TryParse(prixtextBox.Text, out prix) == false)
            {
                MessageBox.Show("vous voulez -vous vérifier le prix");
            }
            else
            {
                string text = "Update produit set prixvente= " + prix + " where idproduit='" + txtSearch.Text.Trim() + "'";
                updatedata(text);
                MessageBox.Show("Le prix de vente a ete mise a jours");
            }
        }
    }
}
