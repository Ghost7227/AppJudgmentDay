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
    /// Логика взаимодействия для ReceiveBook.xaml
    /// </summary>
    public partial class ReceiveBook : Window
    {
        public string WorkPath = "DataBaseWorkers.txt";
        public string BookPath = "DataBaseBooks.txt";
        public string reader1;
        public ReceiveBook()
        {
            InitializeComponent();
            LoadItemListView();
            Loadreader();
            reader.Text = reader1;
        }
        //Вывод сотрудников в список
        private void LoadItemListView()
        {
            if (File.Exists(WorkPath))
            {
                var users = File.ReadAllLines(WorkPath);
                foreach (var user in users)
                {
                    var userDetails = user.Split('#');
                    if (userDetails.Length == 8)
                    {
                        string formatedUser = $"{userDetails[0]}, {userDetails[1]} {userDetails[2]} {userDetails[3]}, {userDetails[4]}, {userDetails[5]}, {userDetails[6]}, {userDetails[7]}";
                        worker.Items.Add(formatedUser);
                    }
                }
            }
            else
            {
                MessageBox.Show("Что-то пошло не так");
            }
        }
        
        public int selectedindex;
        private void worker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (worker.SelectedItem != null)
            {
                string selectedItem = worker.SelectedItem.ToString();
                selectedindex = worker.SelectedIndex;
                //ChooseBooks.Items.Add(selectedItem);
                //chooseBook.Add(selectedItem);
                worker.IsEnabled = false;
                //MessageBox.Show(selectedItem);
                booksList.IsEnabled = true;
                Searchbooks();
            }
        }
        //Получение данных о читателе из буферного файла
        private void Loadreader()
        {
            using (var read = new StreamReader("BufReaders.txt"))
            {
                reader1 = read.ReadLine();
                read.Close();
            }
            using (FileStream fs = new FileStream("BufReaders.txt", FileMode.Truncate)) ;
        }
        //Поиск книг, в записях о держателе которых содержится полученный читатель
        List<int> indexs = new List<int>();
        public string readerObrez;
        private void Searchbooks()
        {
            string[] readerArr = reader1.Split(',');
            readerObrez = $"{readerArr[0]}{readerArr[1]}";
            try
            {
                //MessageBox.Show(reader1);
                var lines = File.ReadAllLines(BookPath);
                //string strlin = $"{lines[0]} ПРОБЕЛЫ {lines[1]} ПРОБЕЛЫ {lines[2]}";
                //MessageBox.Show(strlin);
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].Contains(readerObrez))
                    {
                        indexs.Add(i);
                        LoadBooks();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fatality!", ex.Message);
            }

        }
        //string path, List<int> indexs
        private void LoadBooks()
        {
            try
            {
                string[] lines = File.ReadAllLines(BookPath);
                booksList.Items.Clear();
                var uniqueindex = indexs.Distinct().OrderBy(i => i);
                foreach (int index in uniqueindex)
                {
                    if (index >= 0 && index < lines.Length)
                    {
                        string line = lines[index];
                        string[] splitLine = line.Split('#');
                        string lineout = $"{splitLine[0]} {splitLine[1]} {splitLine[2]} {splitLine[3]} {splitLine[4]} {splitLine[5]}";
                        booksList.Items.Add(lineout);
                    } 
                }
            }
            catch
            {
                MessageBox.Show("Ошибка чтения или вывода книг");
            }
        }
        List<string> chooseBook = new List<string>();
        private void booksListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (booksList.SelectedItem != null)
            {
                string selectedItem = booksList.SelectedItem.ToString();//Получаем элемент списка в виде строки
                selectedindex = booksList.SelectedIndex;//Получаем индекс выбранного элемента
                ChooseBooks.Items.Add(selectedItem);//Добавляем выбранный элемент в список выбранных в окне
                chooseBook.Add(selectedItem);//Добавляем выбранный элемент в список выбранных програмно, здесь
                booksList.IsEnabled = false;//Делаем первый список недоступным
                rec.IsEnabled = true;
                booksList.IsEnabled = false ;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            /*try
            {
                string[] lines = File.ReadAllLines(BookPath);
                for (int i = 0; i < lines.Length; i++)
                {
                    lines[i].Replace(readerObrez, string.Empty);
                }
                File.WriteAllLines(BookPath, lines);
                MessageBox.Show("Книга возвращена");
                this.Close();
            }
            catch
            {
                MessageBox.Show("Не удалось вернуть книгу");
            }*/
            /*try
            {
                // Читаем все строки файла
                string[] lines = File.ReadAllLines(BookPath);
                foreach (int lineindex in indexs)
                {
                    //int lineIndex = indexs[0];
                    // Проверяем, существует ли указанная строка
                    if (lineIndex >= 0 && lineIndex < lines.Length)
                    {
                        string rederFormatReplace = $"{readerObrez};";
                        // Удаляем слово из указанной строки
                        lines[lineIndex] = lines[lineIndex].Replace(readerObrez, string.Empty);
                    }
                    else
                    {
                        MessageBox.Show($"Строка с индексом {lineIndex} не существует.");
                        return;
                    }

                    // Записываем обновлённые строки обратно в файл
                    File.WriteAllLines(BookPath, lines);

                    MessageBox.Show("Слово успешно удалено!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обработке файла: {ex.Message}");
            }*/
            try
            {
                // Читаем все строки файла
                string[] lines = File.ReadAllLines(BookPath);

                // Пройдемся по каждому индексу в списке
                foreach (int lineIndex in indexs)
                {
                    // Проверяем, существует ли указанная строка
                    if (lineIndex >= 0 && lineIndex < lines.Length)
                    {
                        string rederFormatReplace = $"{readerObrez};";
                        // Удаляем слово из указанной строки
                        lines[lineIndex] = lines[lineIndex].Replace(rederFormatReplace, string.Empty);
                    }
                    else
                    {
                        MessageBox.Show($"Строка с индексом {lineIndex} не существует.", "Ошибка");
                        return;
                    }
                }

                // Записываем обновлённые строки обратно в файл
                File.WriteAllLines(BookPath, lines);

                MessageBox.Show("Слово успешно удалено из указанных строк!", "Успех");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обработке файла: {ex.Message}", "Ошибка");
            }
        }
    }
}
