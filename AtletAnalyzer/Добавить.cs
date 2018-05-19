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

namespace AtletAnalyzer
{
    public partial class ADD : Form
    {
        private SqlConnection sqlConnection = null;

        public ADD(SqlConnection connection)
        {
            InitializeComponent();
            sqlConnection = connection;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            SqlCommand insertcommand = new SqlCommand("INSERT INTO [Atlety] (F,I,O,TLF,AGE,ADR,MAIL) VALUES (@F, @I, @O, @TLF, @AGE, @ADR, @MAIL)", sqlConnection);
            insertcommand.Parameters.AddWithValue("F", textBox1.Text);
            insertcommand.Parameters.AddWithValue("I", textBox2.Text);
            insertcommand.Parameters.AddWithValue("O", textBox3.Text);
            insertcommand.Parameters.AddWithValue("TLF", textBox4.Text);
            insertcommand.Parameters.AddWithValue("AGE", (textBox5.Text));
            insertcommand.Parameters.AddWithValue("ADR", textBox6.Text);
            insertcommand.Parameters.AddWithValue("MAIL", textBox7.Text);

            try
            {
                await insertcommand.ExecuteNonQueryAsync();

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

 
    }
}
