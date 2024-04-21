﻿using System;
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
    public partial class AddPostForm : Form
    {
        public AddPostForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Считывание значений из полей editbox
            Person.Name = textBox1.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void AddPostForm_Load(object sender, EventArgs e)
        {
            // Заполнение combobox данными
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HospitalL_ARM_Manager"].ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("SELECT ID, Department FROM Department", sqlConnection);
                SqlDataReader reader = cmd.ExecuteReader();
                Person user = new Person();
                while (reader.Read())
                {
                    comboBox1.Items.Add(Tuple.Create(reader["Department"].ToString(), Convert.ToInt32(reader["ID"])));
                }
                reader.Close();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
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
