using UiUnitTesting.Controllers;
using UiUnitTesting.Models;
using UiUnitTesting.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Moq;

namespace XUnitUiTest
{
    public class BooksControllerTest
    {
        //تست فرم نمایش کتاب ها
        [Fact]
        public void IndexUnitTest()
        {
            //arrange
            var moqBookService = new Mock<IBookService>();
            moqBookService.Setup(n => n.GetAll()).Returns(MoqData.GetBookItems());
            var controller = new BookController(moqBookService.Object);
            //act
            var result = controller.Index();
            //assert

            var resultView = Assert.IsType<ViewResult>(result as ViewResult);

            var viewResultBooks = Assert.IsAssignableFrom<List<Book>>(resultView.ViewData.Model as List<Book>);
            //
            Assert.Equal(5, viewResultBooks.Count);
        }

        //تست فرم نمایش جزئیات کتاب ها
        [Theory]
        [InlineData("B0E1F163-B837-4B64-ADCE-994E7C1281DA", "B0E1F163-B837-4B64-ADCE-994E7C128123")]
        public void DetailsUnitTest(string validGuid, string invalidGuid)
        {
            //Arrange
            var moqBookService = new Mock<IBookService>();
            var validGuidId = new Guid(validGuid);
            moqBookService.Setup(n => n.GetByID(validGuidId)).Returns(MoqData.GetBookItems().FirstOrDefault(x => x.Id == validGuidId));
            var controller = new BookController(moqBookService.Object);
            //Act
            var result = controller.Details(validGuidId);
            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var viewResultValue = Assert.IsAssignableFrom<Book>(viewResult.ViewData.Model);
            Assert.Equal("Book1", viewResultValue.Title);
            Assert.Equal("Author1", viewResultValue.Author);
            Assert.Equal(validGuidId, viewResultValue.Id);

            //Arrange
            var invalidGuidId = new Guid(invalidGuid);
            moqBookService.Setup(n => n.GetByID(invalidGuidId)).Returns(MoqData.GetBookItems().FirstOrDefault(x => x.Id == invalidGuidId));

            //Act
            var notFoundresult = controller.Details(invalidGuidId);

            //Assert
            Assert.IsType<NotFoundResult>(notFoundresult);
        }

        //تست فرم ثبت کتاب 
        [Fact]
        public void CreateTest()
        {
            //arrange
            var moqBookService = new Mock<BookService>();
            var controller = new BookController(moqBookService.Object);
            var newBookItem = new Book()
            {
                Title = "Book6",
                Author = "Auth6",
                Description = "Des6"
            };
            //act
            var result = controller.Create(newBookItem);

            //Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.Null(redirectToActionResult.ControllerName);

            //arrange
            var newInvalidBookItem = new Book()
            {
                Author = "Auth7",
                Description = "Des7"
            };
            controller.ModelState.AddModelError("Title", "Title is Required!");
            //act
            var invalidResult = controller.Create(newInvalidBookItem);

            //Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(invalidResult);
            Assert.IsType<SerializableError>(badRequestResult.Value);

        }

        //تست فرم حذف کتاب 
        [Theory]
        [InlineData("B0E1F163-B837-4B64-ADCE-994E7C1281DA")]
        public void DeleteTest(string validGuid)
        {
            //arrange
            var moqBookService = new Mock<IBookService>();
            moqBookService.Setup(n => n.GetAll()).Returns(MoqData.GetBookItems);
            var controller = new BookController(moqBookService.Object);
            var validGuidId = new Guid(validGuid);

            //act
            var result = controller.Delete(validGuidId, null);

            //assert
            var actionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", actionResult.ActionName);
            Assert.Null(actionResult.ControllerName);
        }
    }
}
