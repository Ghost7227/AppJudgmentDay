using AppJudgmentDay.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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
    /// Логика взаимодействия для AddBooks.xaml
    /// </summary>
    public partial class AddBooks : Window
    {
        public string ver = "Верно";
        public string nev = "Неверно";

        private string author;
        private string nameBook;
        private string bookStyle;
        private string produser;
        private string isbn;
        public string countBookPath = "BookCount.txt";
        public int countBook;
        public string countValue;

        public string pattern = @"^[A-Za-zА-Яа-яЁё\s-*]+$";
        public AddBooks()
        {
            InitializeComponent();
        }
        //проверка корректности ввода
        #region
        private void authorTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            author = authorTextBox.Text;
            if (Regex.IsMatch(author, pattern))
            {
                author_result.Text = ver;
                author_result.Foreground = System.Windows.Media.Brushes.Green;
            }
            else
            {
                author_result.Text = nev;
                author_result.Foreground = System.Windows.Media.Brushes.Red;
            }
        }

        private void nameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            nameBook = nameTextBox.Text;
            if (Regex.IsMatch(nameBook, pattern))
            {
                name_result.Text = ver;
                name_result.Foreground = System.Windows.Media.Brushes.Green;
            }
            else
            {
                name_result.Text = nev;
                name_result.Foreground = System.Windows.Media.Brushes.Red;
            }
        }

        private void styleTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            string stylepattern = @"[A-Za-zА-ЯЁа-яё]+$";
            bookStyle = styleTextBox.Text;
            if (Regex.IsMatch(bookStyle, stylepattern))
            {
                style_result.Text = ver;
                style_result.Foreground = System.Windows.Media.Brushes.Green;
            }
            else
            {
                style_result.Text = nev;
                style_result.Foreground = System.Windows.Media.Brushes.Red;
            }
        }

        private void produserTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            produser = produserTextBox.Text;
            if (Regex.IsMatch(produser, pattern))
            {
                produser_result.Text = ver;
                produser_result.Foreground = System.Windows.Media.Brushes.Green;
            }
            else
            {
                produser_result.Text = nev;
                produser_result.Foreground = System.Windows.Media.Brushes.Red;
            }
        }

        private void isbnTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            string isbn_pstter = @"^\d{3}-\d{1}-\d{2}-\d{6}-\d{1}$";
            isbn = isbnTextBox.Text;
            if (Regex.IsMatch(isbn, isbn_pstter))
            {
                isbn_result.Text = ver;
                isbn_result.Foreground = System.Windows.Media.Brushes.Green;
            }
            else
            {
                isbn_result.Text = nev;
                isbn_result.Foreground = System.Windows.Media.Brushes.Red;
            }
        }
        #endregion
        public string lineCountStr;
        public int lineCount;
        public string ID;
        private void addData(object sender, RoutedEventArgs e)
        {
            string filePath = "DataBaseBooks.txt";
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
            countBook++;
            ID = countBook.ToString();
            if (author_result.Text == ver && name_result.Text == ver && style_result.Text == ver && produser_result.Text == ver && isbn_result.Text == ver )
            {
                
                //Вызов метода из DB для записи данных в файл
                var temp = DB.Books.GetAll().ToArray();
                DB.Books.Append(new Book()
                {
                    Idbook = ID,
                    Isbn = isbn,
                    Author = author,
                    Title = nameBook,
                    Janre = bookStyle,
                    Produser = produser
            });

                MessageBox.Show("Данные внесены");
                this.Close();
            }
           SaveCount();
        }

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
            File.WriteAllText(countBookPath, countBook.ToString());
        }
        //Получение счетчика из файла
        private void LoadCount()
        {
            if (File.Exists(countBookPath))
            {
                countValue = File.ReadAllText(countBookPath);
                if (int.TryParse(countValue, out int result))
                {
                    countBook = result;
                }
            }
        }
        

    }
}
