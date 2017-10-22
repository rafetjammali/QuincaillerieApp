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
    public partial class Form1 : Form
    {
        public static string pass;

        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            //rien
        }

        //count how many rows in th data table
        private void Check(DataTable dataTable, SqlDataAdapter query)
        {
            query.Fill(dataTable);
           
            if (dataTable.Rows.Count == 1)
            {//passage de l admin pour utilise al la page de vente
                pass = textBox1.Text.ToLower();
                     //   MessageBox.Show(pass);
                new navigation().navigateMain();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Verifier votre login/mot de passe");
            }
        }

        private void Login_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Login ou mot de passe sont Vide", "Login/Pass");
            }
            else
            {
                dataBase data =new dataBase();
                SqlConnection connection = data.connectiondata();
                        try
                        {
                            SqlDataAdapter query = data.select(connection,textBox1.Text,textBox2.Text);
                            DataTable dataTable = new DataTable();
                            this.Check(dataTable,query);
                        }
                        catch (SqlException expectation)
                        {
                            MessageBox.Show("Error,check login or password",expectation.ToString());
                        }
                        finally { connection.Close(); }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new VerificationForm().Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = null;
            textBox2.Text = null;
        }
    }
}
