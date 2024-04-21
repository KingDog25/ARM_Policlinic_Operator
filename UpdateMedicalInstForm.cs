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
    public partial class UpdateMedicalInstForm : Form
    {
        public UpdateMedicalInstForm()
        {
            InitializeComponent();
        }

        private void UpdateMedicalInstForm_Load(object sender, EventArgs e)
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
            Tuple<string, int> selectedTuple = (Tuple<string, int>)comboBox1.SelectedItem;
            string name = selectedTuple.Item1;
            Person.Id = selectedTuple.Item2;
            textBox1.Text = name;
            // Заполнение Textbox данными
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HospitalL_ARM_Manager"].ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("SELECT Adress, ORGN FROM Medical_Institution Where ID = @id", sqlConnection);
                cmd.Parameters.AddWithValue("@id", selectedTuple.Item2);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    textBox1.Text = reader["Adress"].ToString();
                    numericUpDown1.Value = Convert.ToDecimal(reader["ORGN"]);
                }
            }
        }

        private void buttonReg_Click(object sender, EventArgs e)
        {
            /*
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите клиента");
                return;
            }
           */
            Tuple<string, int> selectedTuple = (Tuple<string, int>)comboBox1.SelectedItem;
            Person.Name = comboBox1.Text;
            //Person.Id = selectedTuple.Item2; 
            Person.Pt = textBox1.Text;
            Person.Tl = numericUpDown1.Value.ToString();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
