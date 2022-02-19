using ApiUnitTesring.Models;
using ApiUnitTesring.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace ApiUnitTesring.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        //Get api/books
        [HttpGet]
        public ActionResult<IEnumerable<Book>> Get()
        {
            var items = _bookService.GetAll();
            return Ok(items);
        }

        //Get api/books/id
        [HttpGet("{id}")]
        public ActionResult<Book> GetById(Guid id)
        {
            var item = _bookService.GetByID(id);
            if (item.Title == null)
                return NotFound();
            return Ok(item);
        }

        //Post api/books
        [HttpPost]
        public ActionResult<Book> Post([FromBody] Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var item = _bookService.Add(book);
            return CreatedAtAction("Get", new { id = item.Id }, item);
            //return Ok(item);
        }

        //Delete api/books/id
        [HttpDelete]
        public ActionResult Delete(Guid id)
        {
            var item = _bookService.GetByID(id);

            if (item.Title == null)
                return NotFound();

            _bookService.Delete(id);
            return Ok();
        }
    }
}
