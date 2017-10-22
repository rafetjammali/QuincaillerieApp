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
    public partial class FormImpressionfacture : Form
    {
        public FormImpressionfacture()
        {
            InitializeComponent();
        }

        string date;
        private void button1_Click(object sender, EventArgs e)
        {
            dataBase data = new dataBase();
            SqlConnection connection = data.connectiondata();
            try
            {
                connection.Open();
                SqlDataAdapter query2 = new SqlDataAdapter("select v.Quantite,p.nomproduit,v.prix from produit p,venteTemp v where v.idproduit=p.idproduit", connection);
                DataTable dataTable2 = new DataTable();
                query2.Fill(dataTable2);
                dataGridView1.DataSource = dataTable2;
                button2.Visible = true;
                date = dateFacture();
               
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

        private void button2_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.ShowDialog();
            //si on a une nouvl page l impresion debut de zero
            i = 0;
            MessageBox.Show("Merci,Au revoir");
         
            //copier les information de table temp vers la table vente
            dataBase data = new dataBase();
            SqlConnection connection = data.connectiondata();
            connection.Open();
            SqlCommand query = new SqlCommand("Insert into vente SELECT * FROM venteTemp", connection);
            query.ExecuteNonQuery();

            //delete the temp record from the table
            SqlCommand query2 = new SqlCommand("DELETE FROM venteTemp", connection);
            query2.ExecuteNonQuery();
            connection.Close();

            button2.Visible = true;
        }

        private string dateFacture()
        {
            //retourner la date de facture(idvente)
            dataBase data = new dataBase();
            SqlConnection connection = data.connectiondata();
                connection.Open();
                SqlDataAdapter query2 = new SqlDataAdapter("select data from venteTemp", connection);
                DataTable dataTable2 = new DataTable();
                query2.Fill(dataTable2);
                string datevente = dataTable2.Rows[0][0].ToString();
                connection.Close();
            return datevente;
        }

        int i = 0;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int height = 0;
            
            Pen p = new Pen(Brushes.Black, 2.5f);

            Font font = new Font("Courier New", 14);
            Font font2 = new Font("Courier New", 12);

          


            //ecrire th header
            e.Graphics.DrawString("Quincaillerie Aouled si Ali b Mahmoud", font, Brushes.Black, 20, 10);
            e.Graphics.DrawString("Vente en gros des produits divers , Num :97.098.150 ,Rue bla bla bla ", font2, Brushes.Black, 20, 30);
           e.Graphics.DrawString("Date de Facture :" + date + " Sidi Bouzid ", font2, Brushes.Black, 20, 50);

            #region qte
            e.Graphics.FillRectangle(Brushes.Gray, new Rectangle(100, 100, dataGridView1.Columns[0].Width, dataGridView1.Rows[0].Height));
            e.Graphics.DrawRectangle(p, new Rectangle(100, 100, dataGridView1.Columns[0].Width, dataGridView1.Rows[0].Height));
            e.Graphics.DrawString(dataGridView1.Columns[0].HeaderText.ToString(), dataGridView1.Font, Brushes.Black, new Rectangle(100, 100, dataGridView1.Columns[0].Width, dataGridView1.Rows[0].Height));
            #endregion

            #region NomProduit
            //+ dataGridView1.Columns[0].Width pour prendre l espace on pa ecrire sur la meme espace presedient
            e.Graphics.FillRectangle(Brushes.Gray, new Rectangle(100 + dataGridView1.Columns[0].Width, 100, dataGridView1.Columns[0].Width , dataGridView1.Rows[0].Height));
            e.Graphics.DrawRectangle(p, new Rectangle(100 + dataGridView1.Columns[0].Width, 100, dataGridView1.Columns[0].Width, dataGridView1.Rows[0].Height));
            e.Graphics.DrawString("Désignation", dataGridView1.Font, Brushes.Black, new Rectangle(100 + dataGridView1.Columns[0].Width, 100, dataGridView1.Columns[0].Width, dataGridView1.Rows[0].Height));
            #endregion

              #region montant
            e.Graphics.FillRectangle(Brushes.Gray, new Rectangle(200 + dataGridView1.Columns[0].Width, 100, dataGridView1.Columns[0].Width, dataGridView1.Rows[0].Height));
            e.Graphics.DrawRectangle(p, new Rectangle(200 + dataGridView1.Columns[0].Width, 100,dataGridView1.Columns[0].Width, dataGridView1.Rows[0].Height));
            e.Graphics.DrawString(dataGridView1.Columns[2].HeaderText.ToString(), dataGridView1.Font, Brushes.Black, new Rectangle(200 + dataGridView1.Columns[0].Width, 100, dataGridView1.Columns[0].Width, dataGridView1.Rows[0].Height));
            #endregion

            height = 100;

            while (i < dataGridView1.Rows.Count)
            {
                //pour cree une autre page
                if (height > e.MarginBounds.Height)
                {
                    height = 100;
                    e.HasMorePages = true;
                    return;
                }


                height += dataGridView1.Rows[0].Height;

                e.Graphics.DrawRectangle(p, new Rectangle(100, height, dataGridView1.Columns[0].Width , dataGridView1.Rows[0].Height));
                e.Graphics.DrawString(dataGridView1.Rows[i].Cells[0].FormattedValue.ToString(), dataGridView1.Font, Brushes.Black, new Rectangle(100, height, dataGridView1.Columns[0].Width +100, dataGridView1.Rows[0].Height));

                e.Graphics.DrawRectangle(p, new Rectangle(100 + dataGridView1.Columns[0].Width, height, dataGridView1.Columns[0].Width +100, dataGridView1.Rows[0].Height));
                e.Graphics.DrawString(dataGridView1.Rows[i].Cells[1].FormattedValue.ToString(), dataGridView1.Font, Brushes.Black, new Rectangle(100 + dataGridView1.Columns[0].Width, height, dataGridView1.Columns[0].Width +100 , dataGridView1.Rows[0].Height));

                e.Graphics.DrawRectangle(p, new Rectangle(200 + dataGridView1.Columns[0].Width, height, dataGridView1.Columns[0].Width, dataGridView1.Rows[0].Height));
                  e.Graphics.DrawString(dataGridView1.Rows[i].Cells[2].FormattedValue.ToString(), dataGridView1.Font, Brushes.Black, new Rectangle(200 + dataGridView1.Columns[0].Width, height, dataGridView1.Columns[0].Width , dataGridView1.Rows[0].Height));
                
                i++;
              
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            new navigation().navigateMain();
            this.Hide();
        }
    }
}
