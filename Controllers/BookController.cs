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
            List<Book> bookList=await _bookService.SelectAllBooks();
            return Ok(bookList);
        }
    }
}
