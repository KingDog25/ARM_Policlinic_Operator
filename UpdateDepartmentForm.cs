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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ARM_Policlinic_Operator
{
    public partial class UpdateDepartmentForm : Form
    {
        public UpdateDepartmentForm()
        {
            InitializeComponent();
        }

        private void UpdateDepartmentForm_Load(object sender, EventArgs e)
        {
            // Заполнение combobox данными
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HospitalL_ARM_Manager"].ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("SELECT ID, MedicalName FROM Medical_Institution", sqlConnection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    comboBox1.Items.Add(Tuple.Create(reader["MedicalName"].ToString(), Convert.ToInt32(reader["ID"])));
                }
                reader.Close();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            Tuple<string, int> selectedTuple = (Tuple<string, int>)comboBox1.SelectedItem;
            string name = selectedTuple.Item1;
            Person.Id = selectedTuple.Item2;
            comboBox1.Text = name;
            // Заполнение combobox данными
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HospitalL_ARM_Manager"].ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("SELECT ID, Department, MedicalInst_ID FROM Department Where MedicalInst_ID = @PersonId", sqlConnection);
                cmd.Parameters.AddWithValue("@PersonId", Person.Id);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    comboBox2.Items.Add(Tuple.Create(reader["Department"].ToString(), Convert.ToInt32(reader["ID"])));
                }
                reader.Close();
            }
        }

        private void buttonReg_Click(object sender, EventArgs e)
        {
            Tuple<string, int> selectedTuple = (Tuple<string, int>)comboBox2.SelectedItem;
            Person.Name = comboBox2.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Tuple<string, int> selectedTuple = (Tuple<string, int>)comboBox2.SelectedItem;
            Person.Id2 = selectedTuple.Item2;
        }
    }
}
