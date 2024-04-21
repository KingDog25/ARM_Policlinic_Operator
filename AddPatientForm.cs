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
    public partial class AddPatientForm : Form
    {
        public AddPatientForm()
        {
            InitializeComponent();
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
            Person.Po_U = numericUpDown2.Text;
            Person.Sr = textBoxSeria.Text;
            Person.Is = textBoxIssue.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void AddPatientForm_Load(object sender, EventArgs e)
        {
           /*
            // Заполнение combobox данными
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HospitalL_ARM_Manager"].ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("SELECT No, Address FROM Polic", sqlConnection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    //user.SetId(Convert.ToInt32(reader["ID"]));
                    comboBox1.Items.Add(Tuple.Create(reader["Address"].ToString(), Convert.ToUInt64(reader["No"])));
                }
                reader.Close();
            }
           */

        }
    }
}
