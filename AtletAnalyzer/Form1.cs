using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AtletAnalyzer
{
    public partial class Form1 : Form
    {
        SqlConnection sqlConnection;
        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'aADataSet.Atlety' table. You can move, or remove it, as needed.
              string connectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=c:\users\peka\documents\visual studio 2013\Projects\AtletAnalyzer\AtletAnalyzer\AA.mdf; Integrated Security=True";

            sqlConnection = new SqlConnection(connectionString);

            await sqlConnection.OpenAsync();

            listView1.GridLines = true;
            listView1.FullRowSelect = true;
            listView1.View = View.Details;
            listView1.Columns.Add("Id");
            listView1.Columns.Add("I");
            listView1.Columns.Add("F");
            listView1.Columns.Add("O");
            listView1.Columns.Add("TLF");
            listView1.Columns.Add("ADR");
            listView1.Columns.Add("AGE");
            listView1.Columns.Add("MAIL");

            await Load11();
         
        }

        private async Task Load11()
        {
            SqlDataReader sqlReader = null;

            SqlCommand command = new SqlCommand("SELECT * FROM [Atlety] ORDER BY I", sqlConnection);

            try
            {
                sqlReader = await command.ExecuteReaderAsync();

                while (await sqlReader.ReadAsync())
                {
                    ListViewItem item = new ListViewItem(new string[]{
                        Convert.ToString(sqlReader["Id"]),
                        Convert.ToString(sqlReader["I"]),
                        Convert.ToString(sqlReader["F"]),
                        Convert.ToString(sqlReader["O"]),
                        Convert.ToString(sqlReader["TLF"]),
                        Convert.ToString(sqlReader["ADR"]),
                        Convert.ToString(sqlReader["AGE"]),
                        Convert.ToString(sqlReader["MAIL"])
                    });
                    listView1.Items.Add(item);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }



        }



        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                sqlConnection.Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                sqlConnection.Close();
        }

        private async void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();

            await Load11();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ADD f2 = new ADD(sqlConnection);
            f2.Show();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                update update1 = new update(sqlConnection, Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text));
                update1.Show();
            }
            else MessageBox.Show("Ни одна строка не выделена!","Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); 
        }

        private async void button2_Click(object sender, EventArgs e) {

            if (listView1.SelectedItems.Count > 0)
            {




                DialogResult res = MessageBox.Show("Вы действительно хотите удалить этого человека?", "Подтвердите удаление", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                switch (res)
                {
                    case DialogResult.OK:

                        SqlCommand delete1 = new SqlCommand("DELETE FROM [Atlety] WHERE [Id]=@Id", sqlConnection);
                        delete1.Parameters.AddWithValue("Id", Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text));

                        try
                        {
                            await delete1.ExecuteNonQueryAsync();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        listView1.Items.Clear();
                        await Load11();


                        break;
                }
            }
            else MessageBox.Show("Ни одна строка не выделена!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); 
        }

        private void button6_Click(object sender, EventArgs e)
        {
            chart1.Series["age"].XValueMember = "AGE";
            chart1.Series["age"].YValueMembers = "TLF";
            chart1.DataSource = aADataSet.Atlety;
            chart1.DataBind();


        }

        private void button7_Click(object sender, EventArgs e)
        {
            
                if (listView1.SelectedItems.Count > 0)
                {
                    addrezult addrezult1 = new addrezult(sqlConnection, Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text));
                    addrezult1.Show();
                }
                else MessageBox.Show("Ни одна строка не выделена!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            
        }


    }
}
