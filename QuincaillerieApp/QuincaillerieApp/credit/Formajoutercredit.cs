using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace QuincaillerieApp.credit
{
    public partial class Formajoutercredit : Form
    {
        public Formajoutercredit()
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

        private void button2_Click(object sender, EventArgs e)
        {
    
            montantTextBox.Text = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            decimal montant;

            if (montantTextBox.Text == "")
            {
                MessageBox.Show("Tu as oublie un espace vide");
            }
                //test if montant is a number
            else if (decimal.TryParse(montantTextBox.Text, out montant) == false)
            {
                MessageBox.Show("vous voulez -vous vérifier la montant ");
            }
            else
            {
                dataBase data = new dataBase();
                SqlConnection connection = data.connectiondata();
                try
                {
                    connection.Open();
                    SqlCommand query = new SqlCommand("Insert into credit Values(@datecredit,@montant,@idclient,@login)", connection);
                    query.Parameters.AddWithValue("@datecredit", DateTime.Now);
                    query.Parameters.AddWithValue("@montant", montant);
                    query.Parameters.AddWithValue("@idclient",Formcredit.idclient);
                    query.Parameters.AddWithValue("@login",Form1.pass);
                    query.ExecuteNonQuery();

                    MessageBox.Show("Votre credit a ete ajoute");
                    montantTextBox.Text = null;
                }
                catch (SqlException expectation)
                {
                    MessageBox.Show(expectation.ToString());
                }
                finally
                {
                    connection.Close();
           
                }
            }
        }
    }
}
