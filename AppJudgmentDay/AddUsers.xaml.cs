using AppJudgmentDay.Entities;
using AppJudgmentDay.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для AddUsers.xaml
    /// </summary>
    public partial class AddUsers : Window
    {
        public AddUsers()
        {
            InitializeComponent();
        }
        //Поля для записи в файл
        #region
        public string countValue;
        private int countUser;
        public string countPath = "Count.txt";
        public string ver = "Верно";
        public string nev = "Неверно";
        public string pattern = @"^[А-ЯЁ][а-яё]*$";
        
        private string ID;
        private string firstName;
        private string lastName;
        private string dad;
        private string year;
        private string phoneNumber;
        private string email;
        private int lineCount;
        private string lineCountStr;
        //private int id;
        #endregion
        #region
        //проверка корректности ввода
        private void firstnameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            firstName = firstnameTextBox.Text;
            if (Regex.IsMatch(firstName, pattern))
            {
                firstname_result.Text = ver;
                firstname_result.Foreground = System.Windows.Media.Brushes.Green;
            }
            else
            {
                firstname_result.Text = nev;
                firstname_result.Foreground = System.Windows.Media.Brushes.Red;
            }
        }
        private void lastnameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            lastName = lastnameTextBox.Text;
            if (Regex.IsMatch(lastName, pattern))
            {
                lastname_result.Text = ver;
                lastname_result.Foreground = System.Windows.Media.Brushes.Green;
            }
            else
            {
                lastname_result.Text = nev;
                lastname_result.Foreground = System.Windows.Media.Brushes.Red;
            }
        }
        private void dadTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            dad = dadTextBox.Text;
            if (Regex.IsMatch(dad, pattern)| dad == "")
            {
                dad_result.Text = ver;
                dad_result.Foreground = System.Windows.Media.Brushes.Green;
            }
            else
            {
                dad_result.Text = nev;
                dad_result.Foreground = System.Windows.Media.Brushes.Red;
            }
        }
        private void yearTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            year = yearTextBox.Text;
            if (int.TryParse(year, out int age))
            {
                if (age > 3 && age <= 130)
                {
                    year_result.Text = ver;
                    year_result.Foreground = System.Windows.Media.Brushes.Green;
                }
                else
                {
                    year_result.Text = nev;
                    year_result.Foreground = System.Windows.Media.Brushes.Red;
                }
            }
        }

        private void phonenumberTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            phoneNumber = phonenumberTextBox.Text;
            string pattern_num = @"^(7|8)-\d{3}-\d{3}-\d{2}-\d{2}$"; 
            if (Regex.IsMatch (phoneNumber, pattern_num))
            {
                phoneNum_result.Text = ver;
                phoneNum_result.Foreground = System.Windows.Media.Brushes.Green;
            }
            else
            {
                phoneNum_result.Text = nev;
                phoneNum_result.Foreground = System.Windows.Media.Brushes.Red;
            }
        }

        private void emailTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            email = emailTextBox.Text;
            string pattern_email = @"^[a-zA-Z0-9._%+-]+@(yandex\.ru|gmail\.com|mail\.ru)$";
            if (Regex.IsMatch(email, pattern_email))
            {
                email_result.Text = ver;
                email_result.Foreground = System.Windows.Media.Brushes.Green;
            }
            else
            {
                email_result.Text = nev;
                email_result.Foreground = System.Windows.Media.Brushes.Red;
            }
        }
        #endregion
        //конец проверки корректности ввода

        private void addData(object sender, RoutedEventArgs e)
        {
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
            lineCountStr = lineCount.ToString();
            //Загрузка счетчика пользователй, присвоение ID на основе количества пользователей
            LoadCount();
            countUser++;
            ID = countUser.ToString();
            //Вызов метода из DB для записи данных в файл
            #region
            if (firstname_result.Text == ver && lastname_result.Text == ver && dad_result.Text == ver && year_result.Text == ver && phoneNum_result.Text == ver && email_result.Text == ver)
            {

                var temp = DB.Readers.GetAll().ToArray();
                DB.Readers.Append(new Reader()
                {
                    FirstName = firstName,
                    MiddleName = dad,
                    LastName = lastName,
                    PhoneNumber = phoneNumber,
                    Email = email,
                    Age = year,
                    Id = ID
                }); 

                MessageBox.Show("Данные внесены");
                this.Close();
            }
            else
            {
                MessageBox.Show("Исправьте ошибки");
            }
            #endregion
            SaveCount();
        }
        //отмена
        private void cancel(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите вернуться назад?", "Выход", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
               this.Close();
            }
        }
        //Сохранение счетчика в файл
        private void SaveCount()
        {
            File.WriteAllText(countPath, countUser.ToString());
        }
        //Получение счетчика из файла
        private void LoadCount()
        {
            if (File.Exists(countPath))
            {
                countValue = File.ReadAllText(countPath);
                if(int.TryParse(countValue, out int result))
                {
                    countUser = result;
                }
            }
        }
    }
}
