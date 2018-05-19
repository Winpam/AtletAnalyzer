using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Globalization;

namespace AtletAnalyzer
{
    public partial class addrezult : Form
    {
        private SqlConnection sqlConnection = null;
        private int Id;
        public addrezult (SqlConnection connection, int Id)
        {
            InitializeComponent();
            COMBO();
            sqlConnection = connection;
            this.Id = Id;
        }
        void  COMBO(){
            string connectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=c:\users\peka\documents\visual studio 2013\Projects\AtletAnalyzer\AtletAnalyzer\AA.mdf; Integrated Security=True";
            string qwert = "SELECT * FROM ddisc";
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand com1 = new SqlCommand(qwert,sqlConnection);
            SqlDataReader sqlReader1;
            try
            {
                sqlConnection.Open();
                sqlReader1 = com1.ExecuteReader();
                var ordinal = sqlReader1.GetOrdinal("dis");
               while (sqlReader1.Read())
                {
                 string sname = sqlReader1.GetString(ordinal);
                 comboBox1.Items.Add(sname);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
           
            }
        }


        private async void update_Load1(object sender, EventArgs e)
        {
            SqlCommand addost = new SqlCommand("SELECT [Id], [discid], [atletid], [dostizh], [date] FROM [ddost] WHERE [Id]=@Id", sqlConnection);
            addost.Parameters.AddWithValue("atletid", Id);


            SqlDataReader sqlReader = null; 
            try
            {
                sqlReader = await addost.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {

                    textBox5.Text = Convert.ToString(sqlReader["discid"]);
                    textBox6.Text = Convert.ToString(sqlReader["dostizh"]);
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null && !sqlReader.IsClosed)
                {
                    sqlReader.Close();
                }
            }
        }

        private async void button1_Click_1(object sender, EventArgs e)
        {
            SqlCommand updateAcommand1 = new SqlCommand("INSERT INTO [ddost] (discid,atletid,dostizh,date) VALUES (@discid, @atletid, @dostizh, @date)", sqlConnection);
            updateAcommand1.Parameters.AddWithValue("atletid", Id);
            updateAcommand1.Parameters.AddWithValue("discid", (textBox5.Text));
            updateAcommand1.Parameters.AddWithValue("dostizh", textBox6.Text);
            updateAcommand1.Parameters.AddWithValue("date", DateTime.Now.Date);

            try
            {
                await updateAcommand1.ExecuteNonQueryAsync();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private void button2_Click_1(object sender, EventArgs e)
        {
            Close();
        }





    }
}
