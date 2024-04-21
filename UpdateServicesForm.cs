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
    public partial class UpdateServicesForm : Form
    {
        public UpdateServicesForm()
        {
            InitializeComponent();
        }

        private void UpdateServicesForm_Load(object sender, EventArgs e)
        {
            // Заполнение combobox данными
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HospitalL_ARM_Manager"].ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("SELECT ID, Name, Cost FROM Services", sqlConnection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    comboBox1.Items.Add(Tuple.Create(reader["Name"].ToString(), Convert.ToUInt64(reader["ID"])));
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Tuple<string, UInt64> selectedTuple = (Tuple<string, UInt64>)comboBox1.SelectedItem;
            Person.Id_U = selectedTuple.Item2;
            // Заполнение Textbox данными
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HospitalL_ARM_Manager"].ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("SELECT ID, Name, Cost FROM Services Where ID = @id", sqlConnection);
                cmd.Parameters.AddWithValue("@id", selectedTuple.Item2.ToString());
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    textBox1.Text = reader["Name"].ToString();
                    numericUpDown1.Value = (decimal)reader["Cost"];
                }
            }
        }

        private void buttonReg_Click(object sender, EventArgs e)
        {
            Person.Name = textBox1.Text;
            Person.cost = numericUpDown1.Value;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
