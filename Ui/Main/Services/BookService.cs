using System;
using System.Collections.Generic;
using UiUnitTesting.Models;

namespace UiUnitTesting.Services
{
    public class BookService : IBookService
    {
        private readonly List<Book> _books;

        public BookService()
        {
            _books = (List<Book>)BookData.GetBookItems();
        }

        public IEnumerable<Book> GetAll()
        {
            return _books;
        }

        public Book Add(Book book)
        {
            _books.Add(book);
            return book;
        }

        public Book GetByID(Guid id)
        {
            Book book = new Book();
            foreach(var item in _books)
            {
                if (id == item.Id)
                    book = item;
            }
            return book;
        }

        public void Delete(Guid id)
        {
            Book book = new Book();
            foreach (var item in _books)
            {
                if (id == item.Id)
                    book = item;
            }
            _books.Remove(book);
        }


    }
}
