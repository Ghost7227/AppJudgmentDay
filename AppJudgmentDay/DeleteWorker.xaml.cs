using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AppJudgmentDay
{
    /// <summary>
    /// Логика взаимодействия для DeleteWorker.xaml
    /// </summary>
    public partial class DeleteWorker : Window
    {
        public DeleteWorker()
        {
            InitializeComponent();
        }
        public string tt;
        public int mod;
        public int lineCount;
        public string idInput;
        public string result;
        public int resultInt;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Счетчик строк для проверки коректности номера строки
            #region
            string filePath = "DataBaseWorkers.txt";
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    while (reader.ReadLine() != null)
                    {
                        lineCount++;
                    }
                }
            }
            catch
            {
                MessageBox.Show($"Произошла ошибка");
            }
            #endregion
            //получение строки, данные которой нужно изменить
            string[] lines = File.ReadAllLines(filePath);
            tt = id.Text;
            if (int.TryParse(tt, out int lineNumber))
            {
                if (lineNumber > 0 && lineNumber <= lines.Length)
                {
                    string line = lines[lineNumber - 1];
                    int index = line.IndexOf('#');
                    if (index != -1)
                    {
                        result = line.Substring(0, index);//По сути, получение указаного ID
                        resultInt = Convert.ToInt32(tt) - 1;//Получение номера строки для перезаписи строки
                        mod = resultInt + 1;
                    }
                }
            }

            if (mod <= lineCount)
            {
                btnbloc.IsEnabled = true;
                btnCheck.IsEnabled = false;
                id.IsEnabled = false;
            }
            else
            {
                MessageBox.Show("Переделай");
            }
        }

        private void btnbloc_Click(object sender, RoutedEventArgs e)
        {
            var temp = DB.Workers.GetAll().ToArray();
            DB.Workers.Delete(temp[resultInt]);
            MessageBox.Show("Данные удалены.");
            this.Close();
        }
    }
}
