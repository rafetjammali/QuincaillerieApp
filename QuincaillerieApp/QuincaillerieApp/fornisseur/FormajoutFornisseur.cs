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
    public partial class FormajoutFornisseur : Form
    {
        public FormajoutFornisseur()
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

        private void vider()
        {
            iDTextBox.Text = null;
            nameTextBox.Text = null;
            numerotextbox.Text = null;
            adressebox.Text = null;
            villetextbox.Text = null;
            governmenttextBox.Text = null;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            vider();
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
            int num;

            if (iDTextBox.Text == "" || nameTextBox.Text == "" || numerotextbox.Text == "" || 
                adressebox.Text == "" || villetextbox.Text == "" || governmenttextBox.Text == "")
            {
                MessageBox.Show("Tu as oublie un espace vide");
            }
            else if (int.TryParse(numerotextbox.Text.Trim(), out num) == false)
            {
                MessageBox.Show("vous voulez -vous vérifier le numero de telephone ");
            }
            else
            {
                string active = Active();

                dataBase data = new dataBase();
                SqlConnection connection = data.connectiondata(); 
                try
                   {
                       connection.Open();
                       SqlCommand query = new SqlCommand("Insert into fornisseur Values(@idfornisseur,@nomfornissur,@active,@numero,@adresse,@ville,@gouvernement)", connection);
                       query.Parameters.AddWithValue("@idfornisseur", iDTextBox.Text.Trim().ToLower());
                       query.Parameters.AddWithValue("@nomfornissur", nameTextBox.Text.Trim().ToLower());
                       query.Parameters.AddWithValue("@active", active);
                       query.Parameters.AddWithValue("@numero", num);
                       query.Parameters.AddWithValue("@adresse", adressebox.Text.Trim().ToLower());
                       query.Parameters.AddWithValue("@ville", villetextbox.Text.Trim().ToLower());
                       query.Parameters.AddWithValue("@gouvernement", governmenttextBox.Text.Trim().ToLower());
                       query.ExecuteNonQuery();
   
                            if (MessageBox.Show("Est ce que vous voulez ajouter un autre Fornisseur ?", "Add to db", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                this.Show();
                            }
                            else
                            {
                                new Formfornisseur().Show();
                                this.Hide();
                            }
                   }
                catch (SqlException expectation)
                {
                    MessageBox.Show("voulez verifier votre Cle primaire et vos Informations", expectation.ToString());
                }
                finally
                {
                    connection.Close();
                    vider();
                }
            }
        }
    }
}
