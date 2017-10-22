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
    public partial class Formaffichefornisseru : Form
    {
        public Formaffichefornisseru()
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //dont do anything 
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
                MessageBox.Show("Tu as oublie un espace vide");
            }
            else
            {
                string text = "Select nomfornissur,active,numero,adresse,ville,gouvernement from fornisseur where idfornisseur='" + txtSearch1.Text.Trim() + "'";
                getdata(text);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtSearch2.Text== "")
            { 
                MessageBox.Show("Tu as oublie un espace vide"); 
            }
            else
            {
                string text = "Select nomfornissur,active,numero,adresse,ville,gouvernement from fornisseur where numero='" + txtSearch2.Text.Trim() + "'";
                getdata(text);
            }
        }

        private void vider()
        {
            txtSearch2.Text = null;
            txtSearch1.Text = null;
            DataGridView.DataSource = null;
        }
        private void buttonAnnuler_Click(object sender, EventArgs e)
        {
            vider();
        }
    }
}
