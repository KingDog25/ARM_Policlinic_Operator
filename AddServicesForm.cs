using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ARM_Policlinic_Operator
{
    public partial class AddServicesForm : Form
    {
        public AddServicesForm()
        {
            InitializeComponent();
        }

        private void buttonReg_Click(object sender, EventArgs e)
        {
            Person.Name = textBox1.Text;
            Person.cost = numericUpDown1.Value;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
