using AppJudgmentDay.Entities;
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
    /// Логика взаимодействия для ModifityWorker.xaml
    /// </summary>
    public partial class ModifityWorker : Window
    {
        public ModifityWorker()
        {
            InitializeComponent();
        }
        //переменные и поля
        #region
        public string ver = "Верно";
        public string nev = "Неверно";
        public string pattern = @"^[А-ЯЁ][а-яё]*$";
        private int lineCount = 0;
        private string firstName;
        private string lastName;
        private string dad;
        private string year;
        private string phoneNumber;
        private string email;
        public int t;
        public int mod;
        public string tt;
        public string result;
        public int resultInt;
        public string post;
        #endregion
        //проверка корректности ввода
        #region
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
            if (Regex.IsMatch(dad, pattern) | dad == "")
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
            if (Regex.IsMatch(phoneNumber, pattern_num))
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
        private void postTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            post = postTextBox.Text;
            if (Regex.IsMatch(post, pattern))
            {
                post_result.Text = ver;
                post_result.Foreground = System.Windows.Media.Brushes.Green;
            }
            else
            {
                post_result.Text = nev;
                post_result.Foreground = System.Windows.Media.Brushes.Red;
            }
        }
        #endregion
        //конец проверки корректности ввода
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
                firstnameTextBox.IsEnabled = true;
                lastnameTextBox.IsEnabled = true;
                dadTextBox.IsEnabled = true;
                yearTextBox.IsEnabled = true;
                phonenumberTextBox.IsEnabled = true;
                emailTextBox.IsEnabled = true;
                btnBlock.IsEnabled = true;
                btnCheck.IsEnabled = false;
                id.IsEnabled = false;
                postTextBox.IsEnabled = true;
            }
            else { MessageBox.Show("Переделай"); }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var temp = DB.Workers.GetAll().ToArray();

            DB.Workers.Modify(temp[resultInt], new Worker()
            {
                FirstName = firstName,
                MiddleName = dad,
                LastName = lastName,
                PhoneNumber = phoneNumber,
                Email = email,
                Age = year,
                Id = result,
                Post = post
            });
            DB.Workers.SaveAll();
            MessageBox.Show("Данные внесены.");
            this.Close();
        }
    }
}
