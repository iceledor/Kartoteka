﻿using AutoMapper;
using Kartoteka.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Kartoteka.DAL
{
    public class EFBooksRepository : IBooksRepository
    {

        public void DeleteBook(int ID)
        {
            using (KartotekaModel db = new KartotekaModel())
            {
                BookModel book = db.books.Find(ID);
                if (book != null)
                    db.books.Remove(book);
                db.SaveChanges();
            }
        }
        public void EditBook(Book BookToEdit)
        {
            using (KartotekaModel db = new KartotekaModel())
            {
                BookModel book = db.books.Find(BookToEdit.Id);
                book.Description = BookToEdit.Description;
                book.Name = BookToEdit.Name;
                book.Year = BookToEdit.Year;
                book.authors.Clear();
                if (BookToEdit.authors != null)
                {
                    foreach (Author author in BookToEdit.authors)
                    {
                        book.authors.Add(db.authors.Find(author.Id));
                    }
                }
                db.SaveChanges();
            }
        }

        public List<Book> GetAllBooks()
        {
            using (KartotekaModel db = new KartotekaModel())
            {
                List<Book> AllBooks = new List<Book>();
                foreach (BookModel bk in db.books)
                {
                    AllBooks.Add(GetBookByID(bk.Id));
                }
                return AllBooks;
            }
        }

        public Book GetBookByID(int ID)
        {
            using (KartotekaModel db = new KartotekaModel())
            {
                BookModel book = db.books.Find(ID);
                Book BookToReturn = Mapper.Map<BookModel, Book>(book);
                return BookToReturn;
            }
        }

        public int RegisterNewBook(Book NewBook)
        {
            using (KartotekaModel db = new KartotekaModel())
            {
                BookModel bookmodel = Mapper.Map<Book, BookModel>(NewBook);
                db.books.Add(bookmodel);
                db.SaveChanges();
                return bookmodel.Id;
            }
        }
    }
}
