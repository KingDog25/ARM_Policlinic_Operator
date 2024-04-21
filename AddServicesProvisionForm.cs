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
    public partial class AddServicesProvisionForm : Form
    {
        public AddServicesProvisionForm()
        {
            InitializeComponent();
        }

        private void AddServicesProvisionForm_Load(object sender, EventArgs e)
        {
            // Заполнение combobox данными
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HospitalL_ARM_Manager"].ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("SELECT ID, DateTime_Priem FROM History_Treatment", sqlConnection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    comboBox1.Items.Add(Tuple.Create(reader["DateTime_Priem"].ToString(), Convert.ToInt32(reader["ID"])));
                }
                reader.Close();
            }
            
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HospitalL_ARM_Manager"].ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("SELECT ID, Name FROM Services", sqlConnection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    comboBox2.Items.Add(Tuple.Create(reader["Name"].ToString(), Convert.ToInt32(reader["ID"])));
                }
                reader.Close();
            }

        }

        private void buttonReg_Click(object sender, EventArgs e)
        {
            Person.Name = textBox1.Text;
            Person.dateTime1 = dateTimePicker1.Value;
            Person.count = ((int)numericUpDown1.Value);
            this.DialogResult = DialogResult.OK;
            this.Close();
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
    }
}
