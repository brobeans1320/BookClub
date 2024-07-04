using BookClubAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookClubAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet("getBooks")]
        public async Task<ActionResult<List<Book>>> GetBooks()
        {
            List<Book> bookList = await _bookService.SelectAllBooks();
            return Ok(bookList);
        }

        [HttpPost("createBook")]
        public async Task<ActionResult<int>> CreateBook(Book book)
        {
            var result = await _bookService.CreateBook(book);
            return Ok(result);
        }

        [HttpPut("updateBook")]
        public async Task<ActionResult<int>> UpdateBook(Book book)
        {
            var result = await _bookService.UpdateBook(book); 
            return Ok(result);
        }

        [HttpDelete("deleteBook/{id}")]
        public async Task<ActionResult<int>> DeleteBook(int id)
        {
            var result = await _bookService.DeleteBook(id);
            return Ok(result);
        }
    }
}
