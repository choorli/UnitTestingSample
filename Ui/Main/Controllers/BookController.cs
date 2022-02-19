using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using UiUnitTesting.Models;
using UiUnitTesting.Services;

namespace UiUnitTesting.Controllers
{
    public class BookController : Controller
    {
        public readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        public IActionResult Index()
        {
            var result = _bookService.GetAll();
            return View(result);
        }

        public IActionResult Details(Guid guidId)
        {
            var result = _bookService.GetByID(guidId);
            if (result == null)
                return NotFound();
            return View(result);
        }

        [HttpPost]
        public IActionResult Create(Book item)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                _bookService.Add(item);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        //Get: Books/Delete/5
        public IActionResult Delete(Guid id)
        {
            var result = _bookService.GetByID(id);
            return View(result);
        }

        //Post: Books/Delete/5
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                _bookService.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
