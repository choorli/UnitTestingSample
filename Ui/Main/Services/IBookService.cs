using System;
using System.Collections.Generic;
using UiUnitTesting.Models;

namespace UiUnitTesting.Services
{
    public interface IBookService
    {
        IEnumerable<Book> GetAll();
        Book Add(Book book);
        Book GetByID(Guid id);
        void Delete(Guid id);
    }
}
