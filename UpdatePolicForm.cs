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
    public partial class UpdatePolicForm : Form
    {
        public UpdatePolicForm()
        {
            InitializeComponent();
        }

        private void UpdatePolicForm_Load(object sender, EventArgs e)
        {
            // Заполнение combobox данными
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HospitalL_ARM_Manager"].ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("SELECT No, Seria FROM Polic", sqlConnection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    //user.SetId(Convert.ToInt32(reader["ID"]));
                    comboBox2.Items.Add(Tuple.Create(reader["Seria"].ToString(), Convert.ToUInt64(reader["No"])));
                }
                reader.Close();
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Tuple<string, UInt64> selectedTuple = (Tuple<string, UInt64>)comboBox2.SelectedItem;
            string name = selectedTuple.Item1;
            Person.Id_U = selectedTuple.Item2;
            textBox1.Text = name;
            // Заполнение Textbox данными
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HospitalL_ARM_Manager"].ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("SELECT Address, Pol FROM Polic Where No = @id", sqlConnection);
                cmd.Parameters.AddWithValue("@id", selectedTuple.Item2.ToString());
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    textBox2.Text = reader["Address"].ToString();
                    comboBox1.Text = reader["Pol"].ToString();
                }
            }
        }

        private void buttonReg_Click(object sender, EventArgs e)
        {
            Tuple<string, UInt64> selectedTuple = (Tuple<string, UInt64>)comboBox2.SelectedItem;
            Person.Sr = textBox1.Text;
            Person.No_Str = selectedTuple.Item2.ToString();
            Person.Tl = textBox2.Text;
            Person.Pt = comboBox1.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
