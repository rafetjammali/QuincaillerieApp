using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace QuincaillerieApp.client
{
    public partial class Formajoutclient : Form
    {
        public Formajoutclient()
        {
            InitializeComponent();
        }

        private void vider()
        {
            iDTextBox.Text = null;
            NameTextBox.Text = null;
            lastnametextbox.Text = null;
            numtextbox.Text = null;
            adressetextBox.Text = null;
            villetextBox.Text = null;
            governmenttextBox.Text = null;
        }


        private void btnAnnuler_Click(object sender, EventArgs e)
        {
            vider();
        }

        private void btnajouter_Click(object sender, EventArgs e)
        {
            int num;

            if(iDTextBox.Text=="" || NameTextBox.Text =="" || lastnametextbox.Text =="" || numtextbox.Text==""|| 
                adressetextBox.Text==""||villetextBox.Text==""|| governmenttextBox.Text=="")
            {
                MessageBox.Show("Tu as oublie un espace vide");
            }
            else if (int.TryParse(numtextbox.Text, out num) == false)
            {
                MessageBox.Show("vous voulez -vous vérifier le numero de telephone ");
            }
            else
            {
                dataBase data = new dataBase();
                SqlConnection connection = data.connectiondata(); 
                try
                {
                    connection.Open();
                    SqlCommand query = new SqlCommand("Insert into client Values(@idclient,@nomclient,@prenomclient,@numero,@adresse,@ville,@gouvernement)", connection);
                    query.Parameters.AddWithValue("@idclient", iDTextBox.Text.Trim().ToLower());
                    query.Parameters.AddWithValue("@nomclient",NameTextBox.Text.Trim().ToLower());
                    query.Parameters.AddWithValue("@prenomclient",lastnametextbox.Text.Trim().ToLower());
                    query.Parameters.AddWithValue("@numero", num);
                    query.Parameters.AddWithValue("@adresse",adressetextBox.Text.Trim().ToLower());
                    query.Parameters.AddWithValue("@ville", villetextBox.Text.Trim().ToLower());
                    query.Parameters.AddWithValue("@gouvernement", governmenttextBox.Text.Trim().ToLower());
                    query.ExecuteNonQuery();

                    if (MessageBox.Show("Est ce que vous voulez ajouter un autre Client ?", "Add to db", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        this.Show();
                    }
                    else
                    {
                        new FormClient().Show();
                        this.Hide();
                    }
                }
                catch (SqlException expectation)
                {
                    MessageBox.Show("voulez verifier votre Cle primaire et vos Informations",expectation.ToString());
                }
                finally
                {
                    connection.Close();
                    vider();
                }
            }
        }

        private void label12_Click(object sender, EventArgs e)
        {
            new navigation().navigateMain();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            new navigation().navigateHome();
            this.Hide();
        }
    }
}
