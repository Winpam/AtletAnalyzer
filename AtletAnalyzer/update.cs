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
    public partial class update : Form
    {
        private SqlConnection sqlConnection = null;
        private int Id;
        public update(SqlConnection connection, int Id)
        {
            InitializeComponent();
            sqlConnection = connection;
            this.Id = Id;
        }

        private async void update_Load(object sender, EventArgs e)
        {
            SqlCommand getAtletInfo = new SqlCommand("SELECT [F], [I], [O], [TLF], [AGE], [ADR], [MAIL] FROM [Atlety] WHERE [Id]=@Id",sqlConnection);
            getAtletInfo.Parameters.AddWithValue("Id", Id);
            SqlDataReader sqlReader = null;
            try
            {
                sqlReader = await getAtletInfo.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    textBox1.Text = Convert.ToString(sqlReader["I"]);
                    textBox2.Text = Convert.ToString(sqlReader["F"]);
                    textBox3.Text = Convert.ToString(sqlReader["O"]);
                    textBox4.Text = Convert.ToString(sqlReader["TLF"]);
                    textBox5.Text = Convert.ToString(sqlReader["AGE"]);
                    textBox6.Text = Convert.ToString(sqlReader["ADR"]);
                    textBox7.Text = Convert.ToString(sqlReader["MAIL"]);
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

        private async void button1_Click(object sender, EventArgs e)
        {
            SqlCommand updateAcommand = new SqlCommand("UPDATE [Atlety] SET [I]=@I, [F]=@F, [O]=@O,[TLF]=@TLF, [AGE]=@AGE, [ADR]=@ADR, [MAIL]=@MAIL WHERE [Id]=@Id", sqlConnection);
            updateAcommand.Parameters.AddWithValue("Id", Id);
            updateAcommand.Parameters.AddWithValue("I", textBox1.Text);
            updateAcommand.Parameters.AddWithValue("F", textBox2.Text);
            updateAcommand.Parameters.AddWithValue("O", textBox3.Text);
            updateAcommand.Parameters.AddWithValue("TLF", textBox4.Text);
            updateAcommand.Parameters.AddWithValue("AGE", textBox5.Text);
            updateAcommand.Parameters.AddWithValue("ADR", textBox6.Text);
            updateAcommand.Parameters.AddWithValue("MAIL", textBox7.Text);

            try
            {
                await updateAcommand.ExecuteNonQueryAsync();
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
