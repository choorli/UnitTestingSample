using ApiUnitTesring.Models;
using System;
using System.Collections.Generic;

namespace ApiUnitTesring.Services{
    public interface IBookService
    {
        IEnumerable<Book> GetAll();
        Book Add(Book book);
        Book GetByID(Guid id);
        void Delete(Guid id);
    }
}
