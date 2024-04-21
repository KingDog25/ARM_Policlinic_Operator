using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ARM_Policlinic_Operator
{
    public partial class MainForm : Form
    {
        // Создаем экземпляр класса SoundPlayer и указываем путь к аудиофайлу
        SoundPlayer sound1 = new SoundPlayer(@"..\..\sound\Запрос.wav");
        SoundPlayer soundExit = new SoundPlayer(@"..\..\sound\Exit.wav");
        SoundPlayer soundAdd = new SoundPlayer(@"..\..\sound\lineAdd.wav");
        SoundPlayer soundDrum = new SoundPlayer(@"..\..\sound\DRUMROLL.WAV");
        SoundPlayer soundPush = new SoundPlayer(@"..\..\sound\PUSH.WAV");
        SoundPlayer soundWHOOSH = new SoundPlayer(@"..\..\sound\WHOOSH.WAV");
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Создание экземпляра формы AuthorizationForm
            AutorizationForm authorizationForm = new AutorizationForm();
            authorizationForm.ShowDialog();
            // Проверка результата авторизации
            if (authorizationForm.DialogResult == DialogResult.OK)
            {
                this.Show();    // Отображение главной формы
            }
            else
            {
                Application.Exit();     // Закрытие приложения, если авторизация не прошла успешно
            }

        }

        private void выходToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        void selectQuery(string query, string tableName)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HospitalL_ARM_Manager"].ConnectionString))
            {
                sqlConnection.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, sqlConnection);
                DataSet dataSet = new DataSet();     //создаем датасет для результатов выборки
                //Запишем данные в таблицу формы
                dataAdapter.Fill(dataSet);          //заполняем датасет с помощью адаптера
                dataGridView1.DataSource = dataSet.Tables[0];
                Table.tableName = tableName;
                dataGridView1.AutoResizeColumns();
                sound1.Play(); // Воспроизводим звук
            }

        }

        //Учреждение Select
        private void медУчрежденияToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SingletonClass autorizObject = SingletonClass.getInstance();
            if (autorizObject.getField1() == 1) //если авторизован руководитель
            {
                string query = "SELECT ID AS 'Код учреждения', MedicalName AS 'Название', Adress AS 'Адрес',  ORGN AS 'Код ОРГН' From Medical_Institution";
                selectQuery(query, "Medical_Institution");
            }
        }

        //Отделы Select
        private void отделыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string query = "SELECT d.ID AS 'Код отделения', d.Department AS 'Название отделения', m.MedicalName AS 'Название учреждения'\r\nFROM Department d\r\nJOIN Medical_Institution m ON d.MedicalInst_ID = m.ID;";
            selectQuery(query, "Table_Department");
        }

        private void должностиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string query = "SELECT p.ID AS 'Код должности', p.PostName AS 'Название должности', d.Department AS 'Отдел'\r\nFROM Post p\r\n JOIN Department d ON p.Department_ID = d.ID;";
            selectQuery(query, "Post");
        }

        private void врачиToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string query = "SELECT d.ID AS 'Код сотрудника', LastName AS 'Фамилия', Name AS 'Имя', Patronymic AS 'Отчество', Phone AS 'Телефон', PostName AS 'Должность' From Doctor d JOIN Post p ON p.ID = d.Post_ID;";
            selectQuery(query, "Doctor");
        }

        private void пациентыToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            string query = "SELECT ID AS 'ОМС Пациента', LastName AS 'Фамилия', Name AS 'Имя', Patronymic AS 'Отчество', Phone AS 'Телефон', Birthday AS 'Дата рождения', No_passport AS '№ паспорта', Seria AS 'Серия паспорта', Issued_by AS 'Выдан', Date_Issue AS 'Дата выдачи' From Patient;";
            selectQuery(query, "Patient");
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            string query = "SELECT o.No AS 'ОМС', o.Seria AS 'Серия ОМС', o.Address AS 'Адрес', o.Pol AS 'Пол' From Polic o Join Patient p ON p.ID = o.No;";
            selectQuery(query, "Polic");
        }

        private void историяЛеченийToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string query = "SELECT h.ID AS '№ записи', h.Patient_ID AS 'ОМС Пациента', CONCAT(d.LastName, ' ', d.Name, ' ', d.Patronymic) AS 'ФИО Доктора', DateTime_Priem AS 'Дата приема', DateTime_NextPriem AS 'Следующая дата приема', Description AS 'Описание', Diagnosis AS 'Диагноз' From History_Treatment h Join Doctor d ON d.ID = h.Doctor_ID";
            selectQuery(query, "History_Treatment");
        }

        private void переченьУслугToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string query = "SELECT ID AS '№ записи', Name AS 'Название услуги', Cost AS 'Стоимость'  From Services";
            selectQuery(query, "Services");
        }

        private void оказанныеМедУслугиToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string query = "SELECT ID AS '№ записи', Name AS 'Услуга', DateTimeProvision AS 'Дата оказания', History_ID AS 'Номер записи в истории приема', Count AS 'Количество услуг данного типа', Services_ID AS '№ услуги' From ServicesProvision";
            selectQuery(query, "ServicesProvision");
        }

        private void видыПрепаратовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string query = "SELECT ID_form AS '№ записи', Name AS 'Вид препарата' From TypeDrug";
            selectQuery(query, "TypeDrug");
        }

        private void лекарстваToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string query = "SELECT m.ID AS '№ записи', m.Name AS 'Имя', Price AS 'Цена', t.Name AS 'Тип препарата', f.Name AS 'Форма', f.Weight AS 'Вес' From Medicament m Join TypeDrug t ON t.ID_form = m.DrugType_ID Join FormDrug f ON f.ID = m.Form_ID";
            selectQuery(query, "Medicament");
        }

        private void формыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string query = "SELECT ID AS '№ записи', Name AS 'Форма', Weight AS 'Вес' From FormDrug";
            selectQuery(query, "FormDrug");
        }

        private void рецептыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string query = "SELECT r.ID AS '№ записи', m.Name AS 'Препарат', Count AS 'Количество', History_ID AS '№ Истории лечения' From Recipe r Join Medicament m ON r.Medicament_ID = m.ID ";
            selectQuery(query, "Recipe");
        }


        //Добавление
        //Мед. учреждения
        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddPoliclinicForm policlinicForm = new AddPoliclinicForm();
            // При открытии второй формы
            policlinicForm.MedicalInstitutionSaved += MedicalInstitutionForm_MedicalInstitutionSaved;
            policlinicForm.ShowDialog();
        }

        private void MedicalInstitutionForm_MedicalInstitutionSaved(MedicalInstitution medicalInstitution)
        {
            this.Show(); // Отображение главной формы
            dataGridView1.DataSource = null; //Очистка содержимого

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HospitalL_ARM_Manager"].ConnectionString))
            {
                // Создаем параметры запроса
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@MedicalName", medicalInstitution.GetName()),
                    new SqlParameter("@Adress", medicalInstitution.GetAddress()),
                    new SqlParameter("@ORGN", medicalInstitution.GetORGN()),
                };

                // Создаем запрос с параметрами
                string query = "INSERT INTO Medical_Institution (MedicalName, Adress, ORGN) " +
                               "VALUES (@MedicalName, @Adress, @ORGN)";
                sqlConnection.Open();
                ExecuteQuery(query, parameters, sqlConnection); //отправляем запрос и проверяем данные
            }
            медУчрежденияToolStripMenuItem1.PerformClick();  // Симулируем нажатие на ToolStripMenuItem
            dataGridView1.AutoResizeColumns();
        }

        private void ExecuteQuery(string query, SqlParameter[] parameters, SqlConnection sqlConnection)
        {
            try
            {
                SqlCommand command = new SqlCommand(query, sqlConnection);
                command.Parameters.AddRange(parameters);

                if (command.ExecuteNonQuery() != 1)
                    MessageBox.Show("Ошибка выполнения запроса!", "Ошибка!");
                else
                {
                    MessageBox.Show("Данные обработаны!", "Внимание!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void изменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateMedicalInstForm updateMedicalInst = new UpdateMedicalInstForm();
            updateMedicalInst.ShowDialog();
            if (updateMedicalInst.DialogResult == DialogResult.OK)
            {
                // Создаем параметры запроса
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@ID", Person.Id),
                    new SqlParameter("@MedicalName", Person.Name),
                    new SqlParameter("@Adress", Person.Pt),
                    new SqlParameter("@ORGN", Person.Tl),
                };

                string query = "UPDATE Medical_Institution SET MedicalName = @MedicalName, Adress = @Adress, ORGN = @ORGN  WHERE ID = @ID";
                // Проверяем и отправляем запрос
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HospitalL_ARM_Manager"].ConnectionString))
                {
                    sqlConnection.Open();
                    ExecuteQuery(query, parameters, sqlConnection); //отправляем запрос и проверяем данные
                }
                медУчрежденияToolStripMenuItem1.PerformClick();  // Симулируем нажатие на ToolStripMenuItem
            }
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Данная операция возможна только с письменного распоряжения." +
                "Обратитесь к системному администратору за помощью.");
        }

        private void добавитьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AddDepartmentForm addDepartment = new AddDepartmentForm();
            addDepartment.ShowDialog();
            if (addDepartment.DialogResult == DialogResult.OK)
            {
                this.Show(); // Отображение главной формы
                dataGridView1.DataSource = null; //Очистка содержимого

                // Создаем параметры запроса
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HospitalL_ARM_Manager"].ConnectionString))
                {
                    // Создаем параметры запроса
                    SqlParameter[] parameters = new SqlParameter[]
                    {
                        new SqlParameter("@ID", Person.Id),
                        new SqlParameter("@Name", Person.Name),
                    };

                    string query = "INSERT INTO Department (Department, MedicalInst_ID) VALUES (@Name, @ID)";
                    sqlConnection.Open();
                    ExecuteQuery(query, parameters, sqlConnection); //отправляем запрос и проверяем данные
                }
                отделыToolStripMenuItem.PerformClick();  // Симулируем нажатие на ToolStripMenuItem
                dataGridView1.AutoResizeColumns();
            }
        }

        private void добавитьToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            AddPostForm addPost = new AddPostForm();
            addPost.ShowDialog();
            if (addPost.DialogResult == DialogResult.OK)
            {
                this.Show(); // Отображение главной формы
                dataGridView1.DataSource = null; //Очистка содержимого

                // Создаем параметры запроса
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HospitalL_ARM_Manager"].ConnectionString))
                {
                    // Создаем параметры запроса
                    SqlParameter[] parameters = new SqlParameter[]
                    {
                        new SqlParameter("@ID", Person.Id),
                        new SqlParameter("@PostName", Person.Name),
                    };

                    string query = "INSERT INTO Post (PostName, Department_ID) VALUES (@PostName, @ID)";
                    sqlConnection.Open();
                    ExecuteQuery(query, parameters, sqlConnection); //отправляем запрос и проверяем данные
                }
                должностиToolStripMenuItem.PerformClick();  // Симулируем нажатие на ToolStripMenuItem
                dataGridView1.AutoResizeColumns();
            }

        }

        private void добавитьToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            AddDoctorForm addDoctor = new AddDoctorForm();
            addDoctor.ShowDialog();
            if (addDoctor.DialogResult == DialogResult.OK)
            {
                this.Show(); // Отображение главной формы
                dataGridView1.DataSource = null; //Очистка содержимого

                // Создаем параметры запроса
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HospitalL_ARM_Manager"].ConnectionString))
                {
                    // Создаем параметры запроса
                    SqlParameter[] parameters = new SqlParameter[]
                    {
                        new SqlParameter("@ID", Person.Id),
                        new SqlParameter("@LastName", Person.Sur),
                        new SqlParameter("@Name", Person.Name),
                        new SqlParameter("@Patronymic", Person.Pt),
                        new SqlParameter("@Phone", Person.Tl),
                    };

                    string query = "INSERT INTO Doctor (LastName, Name, Patronymic, Phone, Post_ID) VALUES (@LastName, @Name, @Patronymic, @Phone, @ID)";
                    sqlConnection.Open();
                    ExecuteQuery(query, parameters, sqlConnection); //отправляем запрос и проверяем данные
                }
                врачиToolStripMenuItem1.PerformClick();  // Симулируем нажатие на ToolStripMenuItem
                dataGridView1.AutoResizeColumns();
            }
        }

        private void добавитьToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            AddHistoryForm addHistory = new AddHistoryForm();
            addHistory.ShowDialog();
            if (addHistory.DialogResult == DialogResult.OK)
            {
                this.Show(); // Отображение главной формы
                dataGridView1.DataSource = null; //Очистка содержимого

                // Создаем параметры запроса
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HospitalL_ARM_Manager"].ConnectionString))
                {
                    // Создаем параметры запроса
                    SqlParameter[] parameters = new SqlParameter[]
                    {
                        new SqlParameter("@Doctor_ID", Person.Id),
                        new SqlParameter("@Patient_ID", Person.Id_U.ToString()),
                        new SqlParameter("@DateTime_Priem", Person.dateTime1),
                        new SqlParameter("@DateTime_NextPriem", Person.dateTime2),
                        new SqlParameter("@Diagnosis", Person.Name),
                        new SqlParameter("@Description", Person.Pt),
                    };

                    string query = "INSERT INTO History_Treatment (Doctor_ID, Patient_ID, DateTime_Priem, DateTime_NextPriem, Diagnosis, Description) VALUES (@Doctor_ID, @Patient_ID, @DateTime_Priem, @DateTime_NextPriem, @Diagnosis, @Description)";
                    sqlConnection.Open();
                    ExecuteQuery(query, parameters, sqlConnection); //отправляем запрос и проверяем данные
                }
                историяЛеченийToolStripMenuItem1.PerformClick();  // Симулируем нажатие на ToolStripMenuItem
                dataGridView1.AutoResizeColumns();
            }
        }

        private void добавитьToolStripMenuItem7_Click(object sender, EventArgs e)
        {
            AddServicesForm servicesForm = new AddServicesForm();
            servicesForm.ShowDialog();
            if (servicesForm.DialogResult == DialogResult.OK)
            {
                this.Show(); // Отображение главной формы
                dataGridView1.DataSource = null; //Очистка содержимого

                // Создаем параметры запроса
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HospitalL_ARM_Manager"].ConnectionString))
                {
                    // Создаем параметры запроса
                    SqlParameter[] parameters = new SqlParameter[]
                    {
                        new SqlParameter("@Name", Person.Name),
                        new SqlParameter("@Cost", Person.cost),
                    };

                    string query = "INSERT INTO Services (Name, Cost) VALUES (@Name, @Cost)";
                    sqlConnection.Open();
                    ExecuteQuery(query, parameters, sqlConnection); //отправляем запрос и проверяем данные
                }
                переченьУслугToolStripMenuItem.PerformClick();  // Симулируем нажатие на ToolStripMenuItem
                dataGridView1.AutoResizeColumns();
            }
        }

        private void добавитьToolStripMenuItem8_Click(object sender, EventArgs e)
        {
            AddServicesProvisionForm servicesProvisionForm = new AddServicesProvisionForm();
            servicesProvisionForm.ShowDialog();
            if (servicesProvisionForm.DialogResult == DialogResult.OK)
            {
                this.Show(); // Отображение главной формы
                dataGridView1.DataSource = null; //Очистка содержимого

                // Создаем параметры запроса
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HospitalL_ARM_Manager"].ConnectionString))
                {
                    // Создаем параметры запроса
                    SqlParameter[] parameters = new SqlParameter[]
                    {
                        new SqlParameter("@Name", Person.Name),
                        new SqlParameter("@DateTimeProvision", Person.dateTime1),
                        new SqlParameter("@History_ID", Person.Id),
                        new SqlParameter("@Count", Person.count),
                        new SqlParameter("@Services_ID", Person.Id2),
                    };

                    string query = "INSERT INTO ServicesProvision (Name, DateTimeProvision, History_ID, Count, Services_ID) VALUES (@Name, @DateTimeProvision, @History_ID, @Count, @Services_ID)";
                    sqlConnection.Open();
                    ExecuteQuery(query, parameters, sqlConnection); //отправляем запрос и проверяем данные
                }
                оказанныеМедУслугиToolStripMenuItem1.PerformClick();  // Симулируем нажатие на ToolStripMenuItem
                dataGridView1.AutoResizeColumns();
            }
        }

        private void добавитьToolStripMenuItem9_Click(object sender, EventArgs e)
        {
            AddTypeDrugForm addTypeDrugForm = new AddTypeDrugForm();
            addTypeDrugForm.ShowDialog();
            if (addTypeDrugForm.DialogResult == DialogResult.OK)
            {
                this.Show(); // Отображение главной формы
                dataGridView1.DataSource = null; //Очистка содержимого

                // Создаем параметры запроса
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HospitalL_ARM_Manager"].ConnectionString))
                {
                    // Создаем параметры запроса
                    SqlParameter[] parameters = new SqlParameter[]
                    {
                        new SqlParameter("@Name", Person.Name),
                    };

                    string query = "INSERT INTO TypeDrug (Name) VALUES (@Name)";
                    sqlConnection.Open();
                    ExecuteQuery(query, parameters, sqlConnection); //отправляем запрос и проверяем данные
                }
                видыПрепаратовToolStripMenuItem.PerformClick();  // Симулируем нажатие на ToolStripMenuItem
                dataGridView1.AutoResizeColumns();
            }
        }

        private void добавитьToolStripMenuItem10_Click(object sender, EventArgs e)
        {
            AddFormDrugForm addFormDrug = new AddFormDrugForm();
            addFormDrug.ShowDialog();
            if (addFormDrug.DialogResult == DialogResult.OK)
            {
                this.Show(); // Отображение главной формы
                dataGridView1.DataSource = null; //Очистка содержимого

                // Создаем параметры запроса
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HospitalL_ARM_Manager"].ConnectionString))
                {
                    // Создаем параметры запроса
                    SqlParameter[] parameters = new SqlParameter[]
                    {
                        new SqlParameter("@Name", Person.Name),
                        new SqlParameter("@Weight", Person.Pt),
                    };

                    string query = "INSERT INTO FormDrug (Name, Weight) VALUES (@Name, @Weight)";
                    sqlConnection.Open();
                    ExecuteQuery(query, parameters, sqlConnection); //отправляем запрос и проверяем данные
                }
                формыToolStripMenuItem.PerformClick();  // Симулируем нажатие на ToolStripMenuItem
                dataGridView1.AutoResizeColumns();
            }
        }

        private void добавитьToolStripMenuItem11_Click(object sender, EventArgs e)
        {
            AddMedicamentForm medicamentForm = new AddMedicamentForm();
            medicamentForm.ShowDialog();
            if (medicamentForm.DialogResult == DialogResult.OK)
            {
                this.Show(); // Отображение главной формы
                dataGridView1.DataSource = null; //Очистка содержимого

                // Создаем параметры запроса
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HospitalL_ARM_Manager"].ConnectionString))
                {
                    // Создаем параметры запроса
                    SqlParameter[] parameters = new SqlParameter[]
                    {
                        new SqlParameter("@Name", Person.Name),
                        new SqlParameter("@Price", Person.cost),
                        new SqlParameter("@Form_ID", Person.Id),
                        new SqlParameter("@DrugType_ID", Person.Id2),
                    };

                    string query = "INSERT INTO Medicament (Name, Price, Form_ID, DrugType_ID) VALUES (@Name, @Price, @Form_ID, @DrugType_ID)";
                    sqlConnection.Open();
                    ExecuteQuery(query, parameters, sqlConnection); //отправляем запрос и проверяем данные
                }
                лекарстваToolStripMenuItem1.PerformClick();  // Симулируем нажатие на ToolStripMenuItem
                dataGridView1.AutoResizeColumns();
            }
        }

        private void добавитьToolStripMenuItem12_Click(object sender, EventArgs e)
        {
            AddRecipeForm addRecipe = new AddRecipeForm();
            addRecipe.ShowDialog();
            if (addRecipe.DialogResult == DialogResult.OK)
            {
                this.Show(); // Отображение главной формы
                dataGridView1.DataSource = null; //Очистка содержимого

                // Создаем параметры запроса
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HospitalL_ARM_Manager"].ConnectionString))
                {
                    // Создаем параметры запроса
                    SqlParameter[] parameters = new SqlParameter[]
                    {
                        new SqlParameter("@Medicament_ID", Person.Id2),
                        new SqlParameter("@Count", Person.count),
                        new SqlParameter("@History_ID", Person.Id),
                    };

                    string query = "INSERT INTO Recipe (Medicament_ID, Count, History_ID) VALUES (@Medicament_ID, @Count, @History_ID)";
                    sqlConnection.Open();
                    ExecuteQuery(query, parameters, sqlConnection); //отправляем запрос и проверяем данные
                }
                рецептыToolStripMenuItem.PerformClick();  // Симулируем нажатие на ToolStripMenuItem
                dataGridView1.AutoResizeColumns();
            }
        }

        private void изменитьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            UpdateDepartmentForm updateDepartmentForm = new UpdateDepartmentForm();
            updateDepartmentForm.ShowDialog();
            if (updateDepartmentForm.DialogResult == DialogResult.OK)
            {
                // Создаем параметры запроса
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@ID", Person.Id2),
                    new SqlParameter("@Department", Person.Name),
                    new SqlParameter("@MedicalInst_ID", Person.Id),
                };

                string query = "UPDATE Department SET Department = @Department, MedicalInst_ID = @MedicalInst_ID  WHERE ID = @ID";
                // Проверяем и отправляем запрос
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HospitalL_ARM_Manager"].ConnectionString))
                {
                    sqlConnection.Open();
                    ExecuteQuery(query, parameters, sqlConnection); //отправляем запрос и проверяем данные
                }
                отделыToolStripMenuItem.PerformClick();  // Симулируем нажатие на ToolStripMenuItem
            }

        }

        private void изменToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdatePostForm updatePostForm = new UpdatePostForm();
            updatePostForm.ShowDialog();
            if (updatePostForm.DialogResult == DialogResult.OK)
            {
                // Создаем параметры запроса
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@ID", Person.Id2),
                    new SqlParameter("@PostName", Person.Name),
                    new SqlParameter("@Department_ID", Person.Id),
                };

                string query = "UPDATE Post SET PostName = @PostName, Department_ID = @Department_ID  WHERE ID = @ID";
                // Проверяем и отправляем запрос
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HospitalL_ARM_Manager"].ConnectionString))
                {
                    sqlConnection.Open();
                    ExecuteQuery(query, parameters, sqlConnection); //отправляем запрос и проверяем данные
                }
                должностиToolStripMenuItem.PerformClick();  // Симулируем нажатие на ToolStripMenuItem
            }
        }

        private void изменитьToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            UpdateDoctorForm updateDoctorForm = new UpdateDoctorForm();
            updateDoctorForm.ShowDialog();
            if (updateDoctorForm.DialogResult == DialogResult.OK)
            {
                // Создаем параметры запроса
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@ID", Person.Id),
                    new SqlParameter("@LastName", Person.Sur),
                    new SqlParameter("@Name", Person.Name),
                    new SqlParameter("@Patronymic", Person.Pt),
                    new SqlParameter("@Phone", Person.Tl),
                    new SqlParameter("@Post_ID", Person.Id2),
                };

                string query = "UPDATE Doctor SET LastName = @LastName, Name = @Name, Patronymic = @Patronymic, Phone = @Phone, Post_ID = @Post_ID  WHERE ID = @ID"; 
                // Проверяем и отправляем запрос
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HospitalL_ARM_Manager"].ConnectionString))
                {
                    sqlConnection.Open();
                    ExecuteQuery(query, parameters, sqlConnection); //отправляем запрос и проверяем данные
                }
                врачиToolStripMenuItem1.PerformClick();  // Симулируем нажатие на ToolStripMenuItem
            }

        }

        private void добавитьToolStripMenuItem13_Click(object sender, EventArgs e)
        {
           AddPatientForm addPatientForm = new AddPatientForm();
            addPatientForm.ShowDialog();

            if (addPatientForm.DialogResult == DialogResult.OK)
            {
                this.Show(); // Отображение главной формы
                dataGridView1.DataSource = null; //Очистка содержимого

                // Создаем параметры запроса
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HospitalL_ARM_Manager"].ConnectionString))
                {
                    // Создаем параметры запроса
                    SqlParameter[] parameters = new SqlParameter[]
                    {
                        new SqlParameter("@ID", Person.Po_U),
                        new SqlParameter("@LastName", Person.Sur),
                        new SqlParameter("@Name", Person.Name),
                        new SqlParameter("@Patronymic", Person.Pt),
                        new SqlParameter("@Phone", Person.Tl),
                        new SqlParameter("@Birthday", Person.dateTime1),
                        new SqlParameter("@No_passport", Person.No_Str),
                        new SqlParameter("@Seria", Person.Sr),
                        new SqlParameter("@Issued_by", Person.Is),
                        new SqlParameter("@Date_Issue", Person.dateTime2),
                    };

                    string query = "INSERT INTO Patient (ID, LastName, Name, Patronymic, Phone, Birthday, No_passport, Seria, Issued_by, Date_Issue) VALUES (@ID, @LastName, @Name, @Patronymic, @Phone, @Birthday, @No_passport, @Seria, @Issued_by, @Date_Issue)";
                    sqlConnection.Open();
                    ExecuteQuery(query, parameters, sqlConnection); //отправляем запрос и проверяем данные
                }
                пациентыToolStripMenuItem2.PerformClick();  // Симулируем нажатие на ToolStripMenuItem
                dataGridView1.AutoResizeColumns();
            }
        }

        private void добавитьToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            AddPolicForm addPolicForm = new AddPolicForm();
            addPolicForm.ShowDialog();
            if (addPolicForm.DialogResult == DialogResult.OK)
            {
                this.Show(); // Отображение главной формы
                dataGridView1.DataSource = null; //Очистка содержимого

                // Создаем параметры запроса
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HospitalL_ARM_Manager"].ConnectionString))
                {
                    // Создаем параметры запроса
                    SqlParameter[] parameters = new SqlParameter[]
                    {
                        new SqlParameter("@No", Person.No_Str),
                        new SqlParameter("@Seria", Person.Sr),
                        new SqlParameter("@Address", Person.Sur),
                        new SqlParameter("@Pol", Person.Pt),
                    };

                    string query = "INSERT INTO Polic (No, Seria, Address, Pol) VALUES (@No, @Seria, @Address, @Pol)";
                    sqlConnection.Open();
                    ExecuteQuery(query, parameters, sqlConnection); //отправляем запрос и проверяем данные
                }
                toolStripMenuItem3.PerformClick();  // Симулируем нажатие на ToolStripMenuItem
                dataGridView1.AutoResizeColumns();
            }
        }

        private void изменитьToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            UpdatePolicForm updatePolicForm = new UpdatePolicForm();
            updatePolicForm.ShowDialog();
            if (updatePolicForm.DialogResult == DialogResult.OK)
            {
                this.Show(); // Отображение главной формы
                dataGridView1.DataSource = null; //Очистка содержимого

                // Создаем параметры запроса
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HospitalL_ARM_Manager"].ConnectionString))
                {
                    // Создаем параметры запроса
                    SqlParameter[] parameters = new SqlParameter[]
                    {
                        new SqlParameter("@No", Person.No_Str),
                        new SqlParameter("@Seria", Person.Sr),
                        new SqlParameter("@Address", Person.Tl),
                        new SqlParameter("@Pol", Person.Pt),
                    };

                    string query = "UPDATE Polic SET No = @No, Seria = @Seria, Address = @Address, Pol = @Pol WHERE No = @No";
                    sqlConnection.Open();
                    ExecuteQuery(query, parameters, sqlConnection); //отправляем запрос и проверяем данные
                }
                toolStripMenuItem3.PerformClick();  // Симулируем нажатие на ToolStripMenuItem
                dataGridView1.AutoResizeColumns();
            }
        }

        private void изменитьToolStripMenuItem4_Click(object sender, EventArgs e)
        {

        }

        private void изменитьToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            UpdatePatientForm updatePatientForm = new UpdatePatientForm();
            updatePatientForm.ShowDialog();

            if (updatePatientForm.DialogResult == DialogResult.OK)
            {
                // Создаем параметры запроса
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@ID", Person.Id_U.ToString()),
                    new SqlParameter("@LastName", Person.Sur),
                    new SqlParameter("@Name", Person.Name),
                    new SqlParameter("@Patronymic", Person.Pt),
                    new SqlParameter("@Phone", Person.Tl),
                    new SqlParameter("@Birthday", Person.dateTime1),
                    new SqlParameter("@Date_Issue", Person.dateTime2),
                    new SqlParameter("@No_passport", Person.No_Str),
                    new SqlParameter("@Seria", Person.Sr),
                    new SqlParameter("@Issued_by", Person.Is),
                };
                //SqlCommand cmd = new SqlCommand("SELECT ID, LastName, Name, Patronymic, Phone, Birthday, No_passport, Seria, Issued_by, Date_Issue FROM Patient Where ID = @id", sqlConnection);
                string query = "UPDATE Patient SET LastName = @LastName, Name = @Name, Patronymic = @Patronymic, Phone = @Phone, Birthday = @Birthday, No_passport = @No_passport, Seria = @Seria, Issued_by = @Issued_by, Date_Issue = @Date_Issue WHERE ID = @ID";
                // Проверяем и отправляем запрос
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HospitalL_ARM_Manager"].ConnectionString))
                {
                    sqlConnection.Open();
                    ExecuteQuery(query, parameters, sqlConnection); //отправляем запрос и проверяем данные
                }
                пациентыToolStripMenuItem2.PerformClick();  // Симулируем нажатие на ToolStripMenuItem
            }

        }

        private void изменитьToolStripMenuItem4_Click_1(object sender, EventArgs e)
        {
            UpdateServicesForm updateServicesForm = new UpdateServicesForm();
            updateServicesForm.ShowDialog();

            if (updateServicesForm.DialogResult == DialogResult.OK)
            {
                // Создаем параметры запроса
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@ID", Person.Id_U.ToString()),
                    new SqlParameter("@Name", Person.Name),
                    new SqlParameter("@Cost", Person.cost),
                };
                string query = "UPDATE Services SET Name = @Name, Cost = @Cost WHERE ID = @ID";
                // Проверяем и отправляем запрос
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HospitalL_ARM_Manager"].ConnectionString))
                {
                    sqlConnection.Open();
                    ExecuteQuery(query, parameters, sqlConnection); //отправляем запрос и проверяем данные
                }
                переченьУслугToolStripMenuItem.PerformClick();  // Симулируем нажатие на ToolStripMenuItem
            }
        }

        private void изменитьToolStripMenuItem6_Click(object sender, EventArgs e)
        {

        }

        private void удалитьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Данная операция возможна только с письменного распоряжения." +
                "Обратитесь к системному администратору за помощью.");
        }

        private void удалитьToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            DeletePostForm deletePostForm = new DeletePostForm();
            deletePostForm.ShowDialog();
            if (deletePostForm.DialogResult == DialogResult.OK)
            {
                // Создаем параметры запроса
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@ID", Person.Id.ToString()),
                };
                string query = "DELETE FROM Post WHERE ID = @ID";
                // Проверяем и отправляем запрос
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HospitalL_ARM_Manager"].ConnectionString))
                {
                    sqlConnection.Open();
                    ExecuteQuery(query, parameters, sqlConnection); //отправляем запрос и проверяем данные
                }
                должностиToolStripMenuItem.PerformClick();  // Симулируем нажатие на ToolStripMenuItem
            }
        }

        private void удалитьToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            DeleteDoctorForm deleteDoctorForm = new DeleteDoctorForm();
            deleteDoctorForm.ShowDialog();
            if (deleteDoctorForm.DialogResult == DialogResult.OK)
            {
                // Создаем параметры запроса
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@ID", Person.Id.ToString()),
                };
                string query = "DELETE FROM Doctor WHERE ID = @ID";
                // Проверяем и отправляем запрос
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["HospitalL_ARM_Manager"].ConnectionString))
                {
                    sqlConnection.Open();
                    ExecuteQuery(query, parameters, sqlConnection); //отправляем запрос и проверяем данные
                }
                врачиToolStripMenuItem1.PerformClick();  // Симулируем нажатие на ToolStripMenuItem
            }
        }
    }
}
