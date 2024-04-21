using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ARM_Policlinic_Operator
{
    public partial class AddPolicForm : Form
    {
        public AddPolicForm()
        {
            InitializeComponent();
        }

        private void buttonReg_Click(object sender, EventArgs e)
        {
            Person.Sr = textBox1.Text;
            Person.Sur = textBox2.Text;
            Person.Pt = comboBox1.Text;
            Person.No_Str = comboBox2.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void AddPolicForm_Load(object sender, EventArgs e)
        {
            // Заполнение combobox данными
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HospitalL_ARM_Manager"].ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("SELECT ID, CONCAT(LastName, ' ', Name, ' ', Patronymic) AS 'FIO' FROM Patient", sqlConnection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    //user.SetId(Convert.ToInt32(reader["ID"]));
                    comboBox2.Items.Add(Tuple.Create(reader["FIO"].ToString(), Convert.ToUInt64(reader["ID"])));
                }
                reader.Close();
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
