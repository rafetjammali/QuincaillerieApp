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
    public partial class Formcredit : Form
    {
        public static string idclient;
       
        public Formcredit()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Formajoutercredit().Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new Formpayercredit().Show();
            this.Hide();
        }

        //select and search
        private void getdata()
        {
            dataBase data = new dataBase();
            SqlConnection connection = data.connectiondata();
            try
            {
                connection.Open();
                //read from the data base
                SqlDataAdapter query = new SqlDataAdapter("Select Datecredit,montant,login from credit where idclient='" + txtSearch.Text.Trim() + "'", connection);
                DataTable dataTable = new DataTable();
                query.Fill(dataTable);
               
                //si le client a un credit on desactive le chois de donne un credit 
                //si l client n a pas un credit on dactive le chois de paiment
                        if (dataTable.Rows.Count == 1)
                        {
                            dataGridView1.DataSource = dataTable;
                            btnajout.Visible = false;
                            btnpayer.Visible = true;
                            idclient = txtSearch.Text.Trim();
                        }
                        else
                        {
                                //verifier si le clinet que nous allon ajouter un credit existe a notre base
                                SqlDataAdapter query2 = new SqlDataAdapter("Select idclient from client where idclient='" + txtSearch.Text.Trim().ToLower() + "'", connection);
                                DataTable dataTable2 = new DataTable();
                                query2.Fill(dataTable2);
                                        if (dataTable2.Rows.Count == 1)
                                        {
                                            btnpayer.Visible = false;
                                            btnajout.Visible = true;
                                            idclient = txtSearch.Text.Trim();
                                        }
                                        else
                                        { MessageBox.Show("Le client n'existe pas"); }
                    
                        }
               
            }
            catch (SqlException expectation)
            {
                MessageBox.Show(expectation.ToString());
            }
            finally { connection.Close(); }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text == "")
            {
                MessageBox.Show("Tu as oublie un espace vide");
            }
            else {
           
                  getdata();
            }
        }

        private void btnVider_Click(object sender, EventArgs e)
        {
            txtSearch.Text = null;
            dataGridView1.DataSource = null;
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
    }
}
