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
    public partial class AddRecipeForm : Form
    {
        public AddRecipeForm()
        {
            InitializeComponent();
        }

        private void RecipeForm_Load(object sender, EventArgs e)
        {
            // Заполнение combobox данными
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HospitalL_ARM_Manager"].ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("SELECT ID, CONCAT(DateTime_Priem, ' ', Diagnosis) AS DateDig FROM History_Treatment", sqlConnection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    comboBox1.Items.Add(Tuple.Create(reader["DateDig"].ToString(), Convert.ToInt32(reader["ID"])));
                }
                reader.Close();
            }

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HospitalL_ARM_Manager"].ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("SELECT ID, CONCAT(Name, ' ', +Price) AS NamCost FROM Medicament", sqlConnection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    comboBox2.Items.Add(Tuple.Create(reader["NamCost"].ToString(), Convert.ToInt32(reader["ID"])));
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
            Person.count = ((int)numericUpDown1.Value);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
