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
    public partial class AddDepartmentForm : Form
    {
        //Person user = new Person();
        public AddDepartmentForm()
        {
            InitializeComponent();
        }

        private void FormAddDepartment_Load(object sender, EventArgs e)
        {
            //Person user = new Person();
            // Заполнение combobox данными
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HospitalL_ARM_Manager"].ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("SELECT ID, MedicalName FROM Medical_Institution", sqlConnection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    //user.SetId(Convert.ToInt32(reader["ID"]));
                    comboBox1.Items.Add(Tuple.Create(reader["MedicalName"].ToString(), Convert.ToInt32(reader["ID"])));
                }
                reader.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Считывание значений из полей editbox
            //user.SetName(textBox1.Text);
            Person.Name = textBox1.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Person user = new Person();
            //Tuple<string, decimal> selectedTuple = (Tuple<string, decimal>)comboBox1.SelectedItem;
            if (comboBox1.SelectedItem != null)
            {
                var tuple = (Tuple<string, int>)comboBox1.SelectedItem; // заменить индекс_элемента на нужный индекс
                //user.SetId(id);
                Person.Id = tuple.Item2;
            }
        }
    }
}
