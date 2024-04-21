using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARM_Policlinic_Operator
{
    public class SingletonClass
    {
        // Переменная instance является статической, чтобы была возможность обращаться к ней без создания объекта класса.
        private static SingletonClass instance;
        // Переменная autorizType хранит тип авторизации пользователя.
        private int autorizType;

        // конструктор класса является приватным, чтобы нельзя было создать объект класса извне
        private SingletonClass()
        {
            autorizType = -1; // устанавливаем значение по умолчанию
        }

        // метод для получения единственного объекта класса
        public static SingletonClass getInstance()
        {
            // Проверяем, создан ли объект класса
            if (instance == null)
            {
                // Если нет, то создаем его
                instance = new SingletonClass();
            }
            // Возвращаем единственный объект класса
            return instance;
        }

        // Метод doSomething() представляет собой пример метода, который будет выполняться на единственном объекте класса.
        public void doSomething()
        {
            // Реализация метода
        }

        // Метод для получения значения переменной autorizType
        public int getField1()
        {
            return autorizType;
        }

        // Метод для установки значения переменной autorizType
        public void setField1(int field1)
        {
            this.autorizType = field1;
        }

    }

    // Класс UserClass представляет собой класс, описывающий пользователя.
    public class UserClass
    {
        // Поле typeStaticInt является статическим и доступным для всех объектов данного класса.
        public static int typeStaticInt = 0;
        internal string User { get; set; }
        internal string Pass { get; set; }
        internal string FIO { get; set; }
        internal int Autoriz { get; set; }
        internal int ID { get; set; }


        // Конструктор класса, принимающий тип авторизации
        public UserClass(int autoriz)
        {
            Autoriz = autoriz;
        }

        // Конструктор класса, принимающий ФИО пользователя
        public UserClass(string fio)
        {
            FIO = fio;
        }
    }

    public class Person
    {
        // Переменная ID хранит идентификатор пользователя.
        private int id;
        public static int Id;
        public static int Id2;
        public static UInt64 Id_U;
        public static string No_Str;
        public static string Po_U;
        public static decimal cost;
        private string name;
        public static string Name;
        public static int count;
        internal static string Sur;
        internal static string Pt;
        internal static string Tl;
        internal static string Sr;
        internal static string Is;
        internal static DateTime dateTime1;
        internal static DateTime dateTime2;

        // Метод для установки ID пользователя.
        public void SetId(int userId)
        {
            id = userId;
        }

        // Метод для получения ID пользователя.
        public int GetId()
        {
            return id;
        }

        public void SetName(string NameLc)
        {
            name = NameLc;
        }

        public string GetName()
        {
            return name;
        }
    }

    internal class MedicalInstitution
    {
        private string Name;
        private string Address;
        private int ORGN;
       
        // Метод для установки данных
        public void SetData(string name, string address, int orgn)
        {
            Name = name;
            Address = address;
            ORGN = orgn;
        }

        // Методы для получения данных
        public string GetName()
        {
            return Name;
        }

        public string GetAddress()
        {
            return Address;
        }

        public int GetORGN()
        {
            return ORGN;
        }
    }
}

