﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Kartoteka
{
    public class AuthorViewModel:ViewModelBase
    {
        public RelayCommand<Window> CloseWindowCommand { get; private set; }
        public RelayCommand<object> SaveListCommand { get; private set; }

        private ObservableCollection<BookModel> books;
        private  AuthorModel selectedAuthor;
        public  AuthorModel SelectedAuthor
        {
            get
            {
                return selectedAuthor;
            }
            set
            {
                selectedAuthor = value;
                RaisePropertyChanged("SelectedAuthor");
            }
        }

        public ObservableCollection<BookModel> Books
        {
            get
            {
                return books;
            }

            set
            {
                books = value;
                RaisePropertyChanged("Books");
            }
        }
        private void GetFromList(object parameter)
        {
            if (CustomCommands.IsFilled(SelectedAuthor.Name, SelectedAuthor.Surname) == true)
            {
                SaveAuthor(CustomCommands.GetBooksFromList(parameter));
            }
            else MessageBox.Show("Fill in the name and surname fields");
        }
        private void SaveAuthor(List<BookModel> newbooks)
        {
            using (DataBaseModel db1 = new DataBaseModel())
            {
                CustomCommands.AddBooks(SelectedAuthor, newbooks, db1);
                db1.authors.Add(SelectedAuthor);
                db1.SaveChanges();
            }
            MessageBox.Show("Saved succsessfully");
            SelectedAuthor.Name = null;
            SelectedAuthor.Surname = null;
            SelectedAuthor.books.Clear();
        }

        public AuthorViewModel()
        {         
            this.CloseWindowCommand = new RelayCommand<Window>(CustomCommands.CloseWindow);
            this.SaveListCommand = new RelayCommand<object>(GetFromList);
            this.SelectedAuthor = new AuthorModel();
            books = new ObservableCollection<BookModel>();
            using (DataBaseModel db = new DataBaseModel())
            {
                foreach(BookModel newbook in db.books)
                {
                    books.Add(newbook);
                }
            }
        }
    }
}
