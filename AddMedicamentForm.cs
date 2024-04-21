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
    public partial class AddMedicamentForm : Form
    {
        public AddMedicamentForm()
        {
            InitializeComponent();
        }

        private void AddMedicamentForm_Load(object sender, EventArgs e)
        {
            // Заполнение combobox данными
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HospitalL_ARM_Manager"].ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("SELECT ID, Name FROM FormDrug", sqlConnection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    comboBox1.Items.Add(Tuple.Create(reader["Name"].ToString(), Convert.ToInt32(reader["ID"])));
                }
                reader.Close();
            }

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HospitalL_ARM_Manager"].ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("SELECT ID_form, Name FROM TypeDrug", sqlConnection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    comboBox2.Items.Add(Tuple.Create(reader["Name"].ToString(), Convert.ToInt32(reader["ID_form"])));
                }
                reader.Close();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                var tuple = (Tuple<string, int>)comboBox1.SelectedItem; // заменить индекс_элемента на нужный индекс
                Person.Id = tuple.Item2;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem != null)
            {
                var tuple = (Tuple<string, int>)comboBox2.SelectedItem; // заменить индекс_элемента на нужный индекс
                Person.Id2 = tuple.Item2;
            }
        }

        private void buttonReg_Click(object sender, EventArgs e)
        {
            Person.Name = textBox1.Text;
            Person.cost=numericUpDown1.Value;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
