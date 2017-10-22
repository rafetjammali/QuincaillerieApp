using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace QuincaillerieApp.vente
{
    public partial class vente : Form
    {
        public vente()
        {
            InitializeComponent();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            //rien
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text == "")
            {
                MessageBox.Show("La boite de recherche par code est vide");
            }
            else
            {
                dataBase data = new dataBase();
                SqlConnection connection = data.connectiondata();
                try
                {
                    connection.Open();
                    //read from the data base
                    SqlDataAdapter query = new SqlDataAdapter("Select nomproduit,unite,quantiteproduit,prixvente,prixventemin from produit where idproduit='" + txtSearch.Text.Trim() + "'", connection);
                    DataTable dataTable = new DataTable();
                    query.Fill(dataTable);
                    if (dataTable.Rows.Count == 1)
                    {
                        DataGridView.Visible = true;
                        DataGridView.DataSource = dataTable;
                        button2.Visible=true;
                    }
                    else
                    { MessageBox.Show("Le client n'existe pas");
                    txtSearch.Text = null;
                    }
                  
                }
                catch (SqlException expectation)
                {
                    MessageBox.Show(expectation.ToString());
                }
                finally { connection.Close(); }
            }
        }

        //les variables 
        decimal ventenumTotal;
        decimal venteminTotal;
        decimal prixunitaire;
        decimal quantite;

        private void button2_Click(object sender, EventArgs e)
        {
          
  
            if (textBox2.Text=="")
            {
                MessageBox.Show("La quantite est vide");
            }
             else if (decimal.TryParse(textBox2.Text, out quantite) == false)
            {
                MessageBox.Show("vous voulez -vous vérifier la quantite ","Quantite");
            }
            else
              {
                  dataBase data = new dataBase();
                  SqlConnection connection = data.connectiondata(); 
                 try
                    {
                        connection.Open();
                        //read from the data base
                        SqlDataAdapter query = new SqlDataAdapter("Select quantiteproduit from produit where idproduit='" + txtSearch.Text.Trim() + "'", connection);
                        DataTable dataTable = new DataTable();
                        query.Fill(dataTable);
                        if (dataTable.Rows.Count == 1)
                        {
                            string stringquantite = dataTable.Rows[0][0].ToString();
                            decimal quantiteDB = Convert.ToDecimal(stringquantite);

                            if (quantite <= quantiteDB)
                            {
                                //get the prix vente and vente min
                                #region Prix
                                SqlDataAdapter query2 = new SqlDataAdapter("Select prixvente,prixventemin from produit where idproduit='" + txtSearch.Text.Trim() + "'", connection);
                                DataTable dataTable2 = new DataTable();
                                query2.Fill(dataTable2);
                                string ventestring = dataTable2.Rows[0][0].ToString();
                                string venteminstring = dataTable2.Rows[0][1].ToString();

                                decimal   ventenum = Convert.ToDecimal(ventestring);
                                ventenumTotal = ventenum * quantite;

                                decimal  ventemin = Convert.ToDecimal(venteminstring);
                                venteminTotal = ventemin * quantite;

                                lblV.Text = ventestring;
                                lblvente.Text = ventenumTotal.ToString();

                                lblVM.Text = venteminstring;
                                lblventemin.Text = venteminTotal.ToString();
                                #endregion

                                //get the original prix
                                #region prixUnitaire
                                SqlDataAdapter query3 = new SqlDataAdapter("select PUTT from achat where idproduit='" + txtSearch.Text.Trim() + "'", connection);
                                 DataTable dataTable3 = new DataTable();
                                 query3.Fill(dataTable3);
                                 string prixunitaireString = dataTable3.Rows[0][0].ToString();
                                 prixunitaire = Convert.ToDecimal(prixunitaireString);

                                 lblprixunitaire.Text = prixunitaireString;
                                #endregion

                                 button3.Visible = true;
                                 
                            }
                            else
                            {
                                MessageBox.Show("La quantite volue est plus grande que la quatite du stock");
                            }
                        }
                        else
                        { MessageBox.Show("Le client n'existe pas"); }
                       
                    }
                    catch (SqlException expectation)
                    {
                        MessageBox.Show(expectation.ToString(),"2");
                    }
                    finally { connection.Close(); }
             }
        }

        private void vider()
        {
            txtSearch.Text = null;
            textBox2.Text = null;
            txtprix.Text = null;
            DataGridView.DataSource = null;
        }
        
        private int bigerIdInTab()
        {
            int i = 0;
            int id = 0;
            dataBase data = new dataBase();
            SqlConnection connection = data.connectiondata();
            try
            {
                connection.Open();
                SqlDataAdapter query = new SqlDataAdapter("select max(idvente) from vente", connection);
                DataTable dataTable = new DataTable();
                query.Fill(dataTable);
                string idvente = dataTable.Rows[0][0].ToString();
                i = Convert.ToInt32(idvente);
                id = i + 1;
            }
            catch (SqlException expectation)
            {
                MessageBox.Show(expectation.ToString());
            }
            finally { connection.Close(); }

            return id;

        }

        private void button3_Click(object sender, EventArgs e)
        {
              decimal prix;
             int id = bigerIdInTab();

            if (txtprix.Text=="")
            {
                MessageBox.Show("Le prix Est est vide");
            }
             else if (decimal.TryParse(txtprix.Text, out prix) == false)
            {
                MessageBox.Show("vous voulez -vous vérifier Le prix ","Prix");
            }
            else if (prix >= venteminTotal  && prix <= ventenumTotal)
            {
                 dataBase data = new dataBase();
                  SqlConnection connection = data.connectiondata(); 
                 try
                 {
                     connection.Open();

                        SqlCommand query = new SqlCommand("Insert into venteTemp Values(@idvente,@data,@prix,@Quantite,@idadmin,@idproduit)", connection);
                        query.Parameters.AddWithValue("@idvente", id);
                        query.Parameters.AddWithValue("@data", DateTime.Today);
                        query.Parameters.AddWithValue("@prix", prix);
                        query.Parameters.AddWithValue("@Quantite", quantite);
                        query.Parameters.AddWithValue("@idadmin", Form1.pass);
                        query.Parameters.AddWithValue("@idproduit", txtSearch.Text.Trim().ToLower());
                        query.ExecuteNonQuery();

                        SqlCommand query2 = new SqlCommand("Update produit set quantiteproduit= quantiteproduit-" + quantite + " where idproduit='" + txtSearch.Text.Trim() + "'", connection);
                        query2.ExecuteNonQuery();
                      

                        //write in the file
                        if (MessageBox.Show("Voulez vous imprimer le facteur?", "print", MessageBoxButtons.OKCancel) == DialogResult.OK)
                        {
                            new FormImpressionfacture().Show();
                            this.Hide();
                        }
                        else
                        {
                            vider();
                            //id suivante
                            id++;
                         
                        }
                    }
                 catch (SqlException expectation)
                 {
                     MessageBox.Show(expectation.ToString());
                 }
                 finally { connection.Close(); }
            }
            else
            {
                MessageBox.Show("vous voulez -vous vérifier Le prix ", "Prix");
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {
            //copier les information de table temp vers la table vente
            dataBase data = new dataBase();
            SqlConnection connection = data.connectiondata();
            connection.Open();
            SqlCommand query = new SqlCommand("Insert into vente SELECT * FROM venteTemp", connection);
            query.ExecuteNonQuery();

            //ask are u sure or u wanna add someting 
            MessageBox.Show("Vous avez fini ??","Done and Going Home");

            //delete the temp record from the table
            SqlCommand query2 = new SqlCommand("DELETE FROM venteTemp", connection);
            query2.ExecuteNonQuery();
            connection.Close();

            new navigation().navigateMain();
            this.Hide();
        }

        }
    }

