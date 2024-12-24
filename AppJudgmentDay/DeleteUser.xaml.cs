using AppJudgmentDay.Entities;
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
    /// Логика взаимодействия для DeleteUser.xaml
    /// </summary>
    public partial class DeleteUser : Window
    {
        public DeleteUser()
        {
            InitializeComponent();
        }
        
        public string t;
        public string mod;
        public int lineCount;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Счетчик строк для проверки коректности введеного ID
            #region
            string filePath = "DataBaseUsers.txt";
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

            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                int index = line.IndexOf("#");
                if (index == -1)
                {
                    mod = line.Substring(0, index);
                }
            }

            t = id.Text;//получение ID, данные которого нужно изменить
            
            if (t == mod)
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
            var temp = DB.Readers.GetAll().ToArray();
            DB.Readers.Delete(temp[0]);
            MessageBox.Show("Данные удалены.");
            this.Close();
        }
    }
}
