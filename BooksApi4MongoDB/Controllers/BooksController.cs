using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksApi4MongoDB.Models;
using BooksApi4MongoDB.Services;
using Microsoft.AspNetCore.Mvc;

namespace BooksApi4MongoDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : Controller
    {
        #region Fields
        private readonly BookService _bookService;
        #endregion

        #region Ctor
        public BooksController(BookService bookService)
        {
            _bookService = bookService;
        }
        #endregion

        #region Services

        [HttpGet]
        public ActionResult<List<Book>> Get() =>
            _bookService.Get();

        [HttpGet("{id:length(24)}",Name ="GetBook")]
        public ActionResult<Book> Get(string id)
        {
            var book = _bookService.Get(id);
            if (book == null)
                return NotFound();
            return book;
        }

        [HttpPost]
        public ActionResult<Book> Create(Book book)
        {
            _bookService.Create(book);
            return CreatedAtRoute("GetBook", new { id = book.Id.ToString() }, book);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id,Book bookIn)
        {
            var book = _bookService.Get(id);
            if (book == null)
                return NotFound();
            _bookService.Update(id, bookIn);
            return Ok();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var book = _bookService.Get(id);
            if (book == null)
                return NotFound();
            _bookService.Remove(id);
            return Ok();
        }
        #endregion
    }
}