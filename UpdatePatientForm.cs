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
    public partial class UpdatePatientForm : Form
    {
        public UpdatePatientForm()
        {
            InitializeComponent();
        }

        private void UpdatePatientForm_Load(object sender, EventArgs e)
        {
            // Заполнение combobox данными
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HospitalL_ARM_Manager"].ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("SELECT ID, Concat(LastName, ' ', Name, ' ', Patronymic) AS FIO, Phone, Birthday, No_passport, Seria, Issued_by, Date_Issue FROM Patient", sqlConnection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    comboBox1.Items.Add(Tuple.Create(reader["FIO"].ToString(), Convert.ToUInt64(reader["ID"])));
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
                SqlCommand cmd = new SqlCommand("SELECT ID, LastName, Name, Patronymic, Phone, Birthday, No_passport, Seria, Issued_by, Date_Issue FROM Patient Where ID = @id", sqlConnection);
                cmd.Parameters.AddWithValue("@id", selectedTuple.Item2.ToString());
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string phone = reader["Phone"].ToString();
                    string birthday = reader["Birthday"].ToString();
                    string dateIssue = reader["Date_Issue"].ToString();
                    string passport_N = reader["No_passport"].ToString();
                    string seria = reader["Seria"].ToString();
                    string issuedBy = reader["Issued_by"].ToString();

                    textBoxSur.Text = reader["LastName"].ToString();
                    textBoxName.Text = reader["Name"].ToString(); // Assuming Name is the second part of the full name
                    textBoxPatr.Text = reader["Patronymic"].ToString();
                    textBoxPhone.Text = phone;
                    dateTimePicker1.Value = Convert.ToDateTime(birthday);
                    dateTimePicker2.Value = Convert.ToDateTime(dateIssue);
                    numericUpDown1.Text = passport_N;
                    textBoxSeria.Text = seria;
                    textBoxIssue.Text = issuedBy;
                }
            }
        }

        private void buttonReg_Click(object sender, EventArgs e)
        {
            Person.Sur = textBoxSur.Text;
            Person.Name = textBoxName.Text;
            Person.Pt = textBoxPatr.Text;
            Person.Tl = textBoxPhone.Text;
            Person.dateTime1 = dateTimePicker1.Value.Date;
            Person.dateTime2 = dateTimePicker2.Value.Date;
            Person.No_Str = numericUpDown1.Value.ToString();
            Person.Sr = textBoxSeria.Text;
            Person.Is = textBoxIssue.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
