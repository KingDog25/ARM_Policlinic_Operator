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
    public partial class AddPoliclinicForm : Form
    {
        internal delegate void EventHandler(MedicalInstitution medicalInstitution);
        internal event EventHandler MedicalInstitutionSaved;
        public AddPoliclinicForm()
        {
            InitializeComponent();
        }

        private void buttonReg_Click(object sender, EventArgs e)
        {
            MedicalInstitution medicalInstitution = new MedicalInstitution();
            
            // Считывание значений из полей editbox
            string name = textBox1.Text;
            string address = textBox2.Text;
            int orgn = Convert.ToInt32(numericUpDown1.Value);

            // Установка данных пользователя в объект класса Client
            medicalInstitution.SetData(name, address, orgn);
            this.Close();
            // Вызов события для передачи объекта client обратно в главную форму
            MedicalInstitutionSaved?.Invoke(medicalInstitution); //дэлэгаты
        }
    }
}
