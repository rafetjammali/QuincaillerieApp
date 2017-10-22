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
    public partial class Formmodifirfornisseur : Form
    {
        public Formmodifirfornisseur()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            new navigation().navigateHome();
            this.Hide();
        }

        private void label10_Click(object sender, EventArgs e)
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
                    SqlDataAdapter query = new SqlDataAdapter("Select nomfornissur,active,numero,adresse,ville,gouvernement from fornisseur where idfornisseur='" + txtSearch.Text.Trim() + "'", connection);
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

        private void btnAnnuler_Click(object sender, EventArgs e)
        {
            txtSearch.Text = null;
            DataGridView.DataSource = null;
        }

        private string Active()
        {
            string answer;

            if (radioButton1.Checked == true)
            {
                answer = "oui";
            }
            else
            {
                answer = "non";
            }
            return answer;
        }

        private void button1_Click(object sender, EventArgs e)
        {
                string active = Active();

                dataBase data = new dataBase();
                SqlConnection connection = data.connectiondata();
                try
                {
                    SqlCommand query = new SqlCommand("Update fornisseur set active = '" + active + "' where idfornisseur='" + txtSearch.Text.Trim().ToLower() + "'", connection);
                    connection.Open();
                    query.ExecuteNonQuery();
                    MessageBox.Show("Le type de fornisseur a ete mise a jours");
                }
                catch (SqlException expectation)
                {
                    MessageBox.Show(expectation.ToString());
                }
                finally { connection.Close(); }
        }
    }
}
