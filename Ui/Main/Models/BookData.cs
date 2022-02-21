using System;
using System.Collections.Generic;
using UiUnitTesting.Models;

public class BookData
{
    public static IEnumerable<Book> GetBookItems()
    {
        var _books = new List<Book>()
            {
               new Book()
               {
               Id=new Guid("B0E1F163-B837-4B64-ADCE-994E7C1281DA"),
               Title="Book1",
               Author="Author1",
               Description="des1"
               },
               new Book()
               {
               Id=new Guid("3C378D52-59ED-425D-ABC1-4574DAFDB38D"),
               Title="Book2",
               Author="Author2",
               Description="des2"
               },
               new Book()
               {
               Id=new Guid("19A07908-8AB2-4B83-A708-A1FE118CDAF7"),
               Title="Book3",
               Author="Author3",
               Description="des3"
               },
               new Book()
               {
               Id=new Guid("105499A0-1C62-495A-9111-6A41638114AE"),
               Title="Book4",
               Author="Author4",
               Description="des4"
               },
               new Book()
               {
               Id=new Guid("2000704A-7DE3-48AD-A84A-8E3EA0F2E919"),
               Title="Book5",
               Author="Author5",
               Description="des5"
               }
            };

        return _books;
    }
}
