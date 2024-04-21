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
    public partial class AddHistoryForm : Form
    {
        public AddHistoryForm()
        {
            InitializeComponent();
        }

        private void AddHistoryForm_Load(object sender, EventArgs e)
        {
            // Заполнение combobox данными
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HospitalL_ARM_Manager"].ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("SELECT ID, CONCAT(d.LastName, ' ', d.Name, ' ', d.Patronymic) AS 'FIO' FROM Doctor d", sqlConnection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    //user.SetId(Convert.ToInt32(reader["ID"]));
                    comboBox1.Items.Add(Tuple.Create(reader["FIO"].ToString(), Convert.ToInt32(reader["ID"])));
                }
                reader.Close();
            }

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HospitalL_ARM_Manager"].ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("SELECT ID, CONCAT(LastName, ' ', Name, ' ', Patronymic) AS 'FIO' FROM Patient", sqlConnection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    //user.SetId(Convert.ToInt32(reader["ID"]));
                    comboBox2.Items.Add(Tuple.Create(reader["FIO"].ToString(), Convert.ToUInt64(reader["ID"])));
                }
                reader.Close();
            }
        }

        private void buttonReg_Click(object sender, EventArgs e)
        {
            Person.dateTime1 = dateTimePicker1.Value;
            Person.dateTime2 = dateTimePicker2.Value;
            Person.Name = textBox1.Text;
            Person.Pt = textBox2.Text;
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
            if (comboBox1.SelectedItem != null)
            {
                var tuple = (Tuple<string, UInt64>)comboBox2.SelectedItem; // заменить индекс_элемента на нужный индекс
                Person.Id_U = tuple.Item2;
            }
        }
    }
}
