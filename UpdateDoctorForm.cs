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
    public partial class UpdateDoctorForm : Form
    {
        public UpdateDoctorForm()
        {
            InitializeComponent();
        }

        private void UpdateDoctorForm_Load(object sender, EventArgs e)
        {
            // Заполнение combobox данными
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HospitalL_ARM_Manager"].ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("SELECT ID, CONCAT(LastName, ' ', Name, ' ', Patronymic) AS Dtr FROM Doctor", sqlConnection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    comboBox1.Items.Add(Tuple.Create(reader["Dtr"].ToString(), Convert.ToInt32(reader["ID"])));
                }
                reader.Close();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Tuple<string, int> selectedTuple = (Tuple<string, int>)comboBox1.SelectedItem;
            //string fio = selectedTuple.Item1;
            int id = selectedTuple.Item2;
            Person.Id = id;
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HospitalL_ARM_Manager"].ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("SELECT LastName, Name, Patronymic, Phone, Post_ID FROM Doctor WHERE ID = @id", sqlConnection);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    textBoxFam.Text = (reader["LastName"].ToString());
                    textBoxName.Text = (reader["Name"].ToString());
                    textBoxPatron.Text = (reader["Patronymic"].ToString());
                    textBoxPhone.Text = (reader["Phone"].ToString());
                    //Person.Id = (Convert.ToInt32(reader["Post_ID"]));
                }
                reader.Close();
            }

            // Заполнение combobox данными
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HospitalL_ARM_Manager"].ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("SELECT ID, Department FROM Department Where ID = @PersonId", sqlConnection);
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
            Person.Name = textBoxName.Text;
            Person.Sur = textBoxFam.Text;
            Person.Pt = textBoxPatron.Text;
            Person.Tl = textBoxPhone.Text;
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
