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
    public partial class DeleteDoctorForm : Form
    {
        public DeleteDoctorForm()
        {
            InitializeComponent();
        }

        private void DeleteDoctorForm_Load(object sender, EventArgs e)
        {
            // Заполнение combobox данными
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HospitalL_ARM_Manager"].ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("SELECT ID, Concat(LastName, Name, Patronymic) As FIO FROM Doctor", sqlConnection);
                SqlDataReader reader = cmd.ExecuteReader();
                Person user = new Person();
                while (reader.Read())
                {
                    comboBox1.Items.Add(Tuple.Create(reader["FIO"].ToString(), Convert.ToInt32(reader["ID"])));
                }
                reader.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Tuple<string, int> selectedTuple = (Tuple<string, int>)comboBox1.SelectedItem;
            Person.Id = selectedTuple.Item2;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
