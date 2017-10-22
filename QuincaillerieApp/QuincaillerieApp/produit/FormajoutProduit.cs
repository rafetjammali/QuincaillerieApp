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
    public partial class FormajoutProduit : Form
    {
        public FormajoutProduit()
        {
            InitializeComponent();
        }

        private void vider()
        {
            iDTextBox.Text = null;
            nomTextBox.Text = null;
            quantiteBox.Text = null;
            categorietextBox.Text = null;
            PUHTtextBox.Text = null;
            RemisetextBox.Text = null;
            TVAtextBox.Text = null;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            vider(); 
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

        //chack the bigest Id in the table achat
        private int bigerIdInTab()
        {
            int i = 0;
            int id = 0;
            dataBase data = new dataBase();
            SqlConnection connection = data.connectiondata();
            try
            {
                connection.Open();
                SqlDataAdapter query = new SqlDataAdapter("select max(idachat) from achat", connection);
                DataTable dataTable = new DataTable();
                query.Fill(dataTable);
                string idachat = dataTable.Rows[0][0].ToString();
                i = Convert.ToInt32(idachat);
                id = i + 1;
               // MessageBox.Show(id.ToString());
            }
            catch (SqlException expectation)
            {
                MessageBox.Show(expectation.ToString());
            }
            finally { connection.Close(); }

            return id;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {   
            int quantite;
            double PUHT;
            int remise;
            int tva;

            if (iDTextBox.Text == "" || nomTextBox.Text == "" || quantiteBox.Text == "" || categorietextBox.Text == ""
            || PUHTtextBox.Text == "" || RemisetextBox.Text == "" || TVAtextBox.Text == "")
            {
                MessageBox.Show("Tu as oublie un espace vide");
            }
            else if (int.TryParse(quantiteBox.Text, out quantite) == false)
            {
                MessageBox.Show("vous voulez -vous vérifier la quantite ","Quantite");
            }
            else if (double.TryParse(PUHTtextBox.Text, out PUHT) == false)
            {
                MessageBox.Show("vous voulez -vous vérifier le PUHT ","PUHT");
            }
            else if (PUHTtextBox.Text.Trim()=="0")
            {
                MessageBox.Show("vous voulez -vous changer le PUHT(pas egal zero)","PUHT");
            }
            else if (int.TryParse(RemisetextBox.Text, out remise) == false)
            {
                MessageBox.Show("vous voulez -vous vérifier la Remise ","Remise");
            }
            else if (RemisetextBox.Text.Trim()== "0")
            {
                MessageBox.Show("vous voulez -vous changer le Remise(pas egal zero) ", "PUHT");
            }
            else if (int.TryParse(TVAtextBox.Text,out tva) == false)
            {
                MessageBox.Show("vous voulez -vous vérifier le TVA","TVA");
            }
            else if (TVAtextBox.Text.Trim() == "0")
            {
                MessageBox.Show("vous voulez -vous changer le TVA (pas egal zero)", "PUHT");
            }
            else
            {
                 string unite;

                 double montantH = 0;
                 double PUTT = 0;
                 double PUTTTtotal = 0;
                 double prixvente = 0;
                 double pritventmin = 0;

                 int id=bigerIdInTab();

                //see wich radio button is chcked
                    if(radioButton1.Checked==true)
                    {unite="PC";}
                    else
                    {unite="CT";}

                //calculate
                    montantH = quantite * (PUHT - ((PUHT / 100) * remise));
                    PUTTTtotal = montantH + ((montantH / 100) * Convert.ToDouble(tva));
                    PUTT = PUTTTtotal / quantite;
                    prixvente = PUTT + ((PUTT / 100) * 15);
                    pritventmin = PUTT + ((PUTT / 100) * 10);

                    dataBase data = new dataBase();
                    SqlConnection connection = data.connectiondata();
                    try
                    {
                        connection.Open();
                        SqlCommand query = new SqlCommand("Insert into produit Values(@idproduit,@nomproduit,@unite,@Quantiteproduit,@prixvente,@prixventemin,@categorie)", connection);
                        query.Parameters.AddWithValue("@idproduit", iDTextBox.Text.Trim().ToLower());
                        query.Parameters.AddWithValue("@nomproduit", nomTextBox.Text.Trim().ToLower());
                        query.Parameters.AddWithValue("@unite", unite);
                        query.Parameters.AddWithValue("@Quantiteproduit", quantite);
                        query.Parameters.AddWithValue("@prixvente", prixvente);
                        query.Parameters.AddWithValue("@prixventemin", pritventmin);
                        query.Parameters.AddWithValue("@categorie", categorietextBox.Text.Trim().ToLower());
                        query.ExecuteNonQuery();

                        //second command to add in achat
                        SqlCommand query2 = new SqlCommand("Insert into achat Values(@idachat,@Dataachat,@PUHT,@Remise,@TVA,@montantH,@PUTT,@login,@idproduit)", connection);
                        query2.Parameters.AddWithValue("@idachat", id);
                        query2.Parameters.AddWithValue("@Dataachat", DateTime.Today);
                        query2.Parameters.AddWithValue("@PUHT", PUHT);
                        query2.Parameters.AddWithValue("@Remise", remise);
                        query2.Parameters.AddWithValue("@TVA", tva);
                        query2.Parameters.AddWithValue("@montantH", montantH);
                        query2.Parameters.AddWithValue("@PUTT", PUTT);
                        query2.Parameters.AddWithValue("@Login", Form1.pass.ToString().ToLower());
                        query2.Parameters.AddWithValue("@idproduit", iDTextBox.Text.Trim().ToLower());

                        query2.ExecuteNonQuery();

                        if (MessageBox.Show("Est ce que vous voulez ajouter un autre Produit ?", "Add to db", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            this.Show();
                        }
                        else
                        {
                            new Formproduit().Show();
                            this.Hide();
                        }

                    }
                    catch (SqlException expectation)
                    {//"voulez verifier votre Cle primaire et vos Informations",
                        MessageBox.Show("Will you check your information","General Exception :"+expectation.ToString());
                    }
                    catch (ArithmeticException mathExc)
                    {
                        MessageBox.Show("Some wrong Number","Arithmetic Exception " +mathExc.ToString());
                    }
                    finally
                    {
                        connection.Close();
                       // vider();
                    }
            }                 
        }
    }
}
