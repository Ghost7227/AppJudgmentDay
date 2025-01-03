﻿using AppJudgmentDay.Entities;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AppJudgmentDay
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        //подтверждение выхода
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите выйти?", "Выход", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
        }
        //открытие окна добавления пользователей
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddUsers addUsers = new AddUsers();
            addUsers.ShowDialog();
        }
        //открытие окна просмотра читателей
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Userlist users = new Userlist();
            users.ShowDialog();
        }
        //Открытие окна просмотра библиотеки
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            SewBooks sewBooks = new SewBooks();
            sewBooks.Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Modifity modifity = new Modifity();
            modifity.ShowDialog();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            DeleteUser deleteUser = new DeleteUser();
            deleteUser.ShowDialog();
        }
        //добавление книги
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            AddBooks addBook = new AddBooks();
            addBook.ShowDialog();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            DeleteBook deleteBook = new DeleteBook();
            deleteBook.ShowDialog();
        }

        //работники
        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            AddWorker addWorker = new AddWorker();
            addWorker.ShowDialog();
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            SewWorkers sewWorkers = new SewWorkers();
            sewWorkers.Show();
        }

        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            DeleteWorker deleteWorker = new DeleteWorker();
            deleteWorker.ShowDialog();
        }

        private void Button_Click_11(object sender, RoutedEventArgs e)
        {
            ModifityWorker modifityWorker = new ModifityWorker();
            modifityWorker.Show();
        }
    }
}
