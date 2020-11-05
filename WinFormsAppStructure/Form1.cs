using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
    /*
     Рычков Владислав ИЭ-65-18 
     15.Ежедневно в течение апреля измеряли уровень шума вблизи предприятий города.
     Известны названия предприятий. Определить предприятие, вблизи которого наблюдался максимальный уровень шума, и день, в который наблюдался этот уровень. 
     Выполнить сортировку списка.
     */
namespace WinFormsAppStructure
{
    public partial class Form1 : Form // создание класса
    {
        static int size = 30; // размер структуры 
        FindNoise[] ArrStruct = new FindNoise[size]; // массив стуктуры
        public Form1() // конструктор
        {
            InitializeComponent();
        }
        public struct FindNoise // создание структуры
        {
            public string Name { get; set; } // поля структуры
            public int Day { get; set; }
            public int Noise { get; set; }
        }
        private void button1_Click(object sender, EventArgs e) // метод для ввода данных из файла
        {
            dataGridView1.Rows.Clear(); // отчистка полей перед заполнением
            OpenFileDialog file = new OpenFileDialog(); // открытие файла
            file.ShowDialog();
            using (StreamReader fs = new StreamReader(file.FileName)) // считывание файла
            {
                int i = 0;
                while (true) // заполнение стуктуры
                {
                    string temp = fs.ReadLine(); // считывание строки
                    if (temp == null) break; // продолжать, пока не обнаружить пустой строки
                    string[] words = temp.Split(' '); // разбиение сторки на массив слов, через пробел 
                    ArrStruct[i].Name = words[0];          // заполнение структуры
                    var intday = Convert.ToInt32(words[1]);
                    ArrStruct[i].Day = intday;
                    var intnoise = Convert.ToInt32(words[2]);
                    ArrStruct[i].Noise = intnoise; 
                    dataGridView1.Rows.Add(ArrStruct[i].Name, ArrStruct[i].Day, ArrStruct[i].Noise);
                    i++; // заполнение таблицы данными
                }
            }
        }
        private void Form1_Load(object sender, EventArgs e) // заполнение таблицы столбцами
        {
            var column1 = new DataGridViewColumn(); // первая колонка
            column1.HeaderText = "Предприятие";
            column1.Name = "dname";
            column1.CellTemplate = new DataGridViewTextBoxCell();
            var column2 = new DataGridViewColumn(); // вторая колонка
            column2.HeaderText = "День (1-15)";
            column2.Name = "dday";
            column2.CellTemplate = new DataGridViewTextBoxCell();
            var column3 = new DataGridViewColumn(); // третья колонка
            column3.HeaderText = "Уровень шума (дБА)";
            column3.Name = "dnoise";
            column3.CellTemplate = new DataGridViewTextBoxCell();
            dataGridView1.Columns.Add(column1); // добавление колонок в таблицу
            dataGridView1.Columns.Add(column2);
            dataGridView1.Columns.Add(column3);
        }
        private void button2_Click(object sender, EventArgs e) // метод для поиска максимального шума
        {
            label2.Text = "";
            int maxNoise = ArrStruct[0].Noise; // задал максимуму нулевой элемент
            int maxDay = 0;
            string maxName = "empty";
            for (int i = 1; i < size; i++) // цикл для поиска 
            {
                if (ArrStruct[i].Noise > maxNoise) // перебор максимальных значений
                {
                    maxNoise = ArrStruct[i].Noise;
                    maxDay = ArrStruct[i].Day;
                    maxName = ArrStruct[i].Name;
                }
            }
            label2.Text += "Максимальный шум - " + maxNoise + "(дБА) наблюдался " + maxDay + " числа, у предприятия " + maxName;
        } // вывод результатов 
        private void button3_Click(object sender, EventArgs e) // сортировка по дням (убывание)
        {
            dataGridView1.Sort(dataGridView1.Columns[1], ListSortDirection.Descending);
        }
        private void button5_Click(object sender, EventArgs e) // сортировка по шуму (убывание)
        {
            dataGridView1.Sort(dataGridView1.Columns[2], ListSortDirection.Descending);
        }
        private void button4_Click(object sender, EventArgs e) // сортировка по дням (возрастание)
        {
            dataGridView1.Sort(dataGridView1.Columns[1], ListSortDirection.Ascending);
        }
        private void button6_Click(object sender, EventArgs e) // сортировка по шуму (возрастание)
        {
            dataGridView1.Sort(dataGridView1.Columns[2], ListSortDirection.Ascending);
        }
    }
}