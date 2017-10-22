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
    public partial class Formpayercredit : Form
    {
        public Formpayercredit()
        {
            InitializeComponent();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            new navigation().navigateMain();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            new navigation().navigateHome();
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
                    SqlDataAdapter query2 = new SqlDataAdapter("Select montant from credit where idclient='" + Formcredit.idclient.Trim() + "'", connection);
                    DataTable dataTable2 = new DataTable();
                    query2.Fill(dataTable2);

                    string stringmontant = dataTable2.Rows[0][0].ToString();
                    decimal montantdata = Convert.ToDecimal(stringmontant);
                    //check if the montant de bass is less then the montant that u gona take it off
                        if (montant <= montantdata)
                        {

                            SqlCommand query = new SqlCommand("Update credit set montant= montant - " + Convert.ToDecimal(montant) + " where idclient='" + Formcredit.idclient.Trim() + "'", connection);
                            connection.Open();
                            query.ExecuteNonQuery();
                            MessageBox.Show("Mise a jour est terminee");
                        }
                        else
                        {
                            MessageBox.Show("Verifier la valeur du montant car elle est superieur a celle du credit");
                        }
                }
                catch (SqlException expectation)
                {
                    MessageBox.Show(expectation.ToString());
                }
                finally { connection.Close(); }
            }
        }
    }
}
