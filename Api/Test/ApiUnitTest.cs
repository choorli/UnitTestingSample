using ApiUnitTesring.Controller;
using ApiUnitTesring.Models;
using ApiUnitTesring.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace XUnitApiTest
{
    public class ApiUnitTest
    {
        BooksController _booksController;
        IBookService _bookService;

        public ApiUnitTest()
        {
            _bookService = new BookService();

            _booksController = new BooksController(_bookService);
        }

        //تست api -- Get
        [Fact]
        public void GetAllTest()
        {
            var result = _booksController.Get();
            //Arrange
            //Act
            //Assert
            Assert.IsType<OkObjectResult>(result.Result);

            var list = result.Result as OkObjectResult;
            Assert.IsType<List<Book>>(list.Value);

            var listBooks = list.Value as List<Book>;
            Assert.Equal(5, listBooks.Count);

        }

        //تست api -- GetBookByIdTest
        [Theory]
        [InlineData("2000704A-7DE3-48AD-A84A-8E3EA0F2E919", "2000704A-7DE3-48AD-A84A-8E3EA0F2E111")]
        public void GetBookByIdTest(string id1, string id2)
        {
            //Arrange
            var validId = new Guid(id1);//Id exist to list
            var inValidId = new Guid(id2);//Id Not exist to list

            //Act
            var notFoundResult = _booksController.GetById(inValidId);
            //Assert
            var notResponseResult = notFoundResult.Result as NotFoundResult;

            Assert.IsType<NotFoundResult>(notResponseResult);

            //Act
            var okResult = _booksController.GetById(validId);

            //Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
            var item = okResult.Result as OkObjectResult;

            Assert.IsType<Book>(item.Value);
            var bookItem = item.Value as Book;

            Assert.Equal(validId, bookItem.Id);
            Assert.Equal("Book5", bookItem.Title);
        }

        //تست api -- AddBookTest
        [Fact]
        public void AddBookTest()
        {
            //Arrange
            var newCompeleteBook = new Book()
            {
                Title = "title6",
                Author = "Author6",
                Description = "Des6"
            };
            //Act
            var craeteResponse = _booksController.Post(newCompeleteBook);
            //Assert
            Assert.IsType<CreatedAtActionResult>(craeteResponse.Result);

            var item = craeteResponse.Result as CreatedAtActionResult;
            Assert.IsType<Book>(item.Value);

            var bookItem = item.Value as Book;
            Assert.Equal(newCompeleteBook.Title, bookItem.Title);
            Assert.Equal(newCompeleteBook.Author, bookItem.Author);
            Assert.Equal(newCompeleteBook.Description, bookItem.Description);


            //Arrange
            var newInCompeleteBook = new Book()
            {
                Author = "Author6",
                Description = "Drs6"
            };
            //Act
            _booksController.ModelState.AddModelError("Title", "Title is Required");
            var badRequest = _booksController.Post(newInCompeleteBook);
            //Assert
            Assert.IsType<BadRequestObjectResult>(badRequest.Result);

        }

        //تست api -- DeleteBookTest
        [Theory]
        [InlineData("2000704A-7DE3-48AD-A84A-8E3EA0F2E919", "2000704A-7DE3-48AD-A84A-8E3EA0F2E111")]
        public void DeleteBookByIdTest(string id1, string id2)
        {
            //Arrange
            var validId = new Guid(id1);//Id exist in list
            var inValidId = new Guid(id2);//Id Not exist in list
            //Act
            var notFoundResult = _booksController.Delete(inValidId);
            //Assert
            Assert.IsType<NotFoundResult>(notFoundResult);
            Assert.Equal(5, _bookService.GetAll().Count());

            //Act
            var okResult = _booksController.Delete(validId);
            //Assert
            Assert.IsType<OkResult>(okResult);
            Assert.Equal(4, _bookService.GetAll().Count());
        }
    }
}
