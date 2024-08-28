using BookClubAPI.Data_Objects;
using BookClubAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookClubAPI.Controllers
{
    [Route("api/book")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet("book/list")]
        public async Task<ActionResult<List<Book>>> GetBooks()
        {
            try
            {
                List<Book> bookList = await _bookService.GetBooks();
                return Ok(bookList);
            }
            catch (Exception ex)
            {
                throw new Exception("GetBooks failed in controller: " + ex.Message, ex);
            }
        }

        [HttpPost("book/insert")]
        public async Task<ActionResult<int>> CreateBook(Book book)
        {
            try
            {
                var result = await _bookService.CreateBook(book);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception("CreateBook failed in controller: " + ex.Message, ex);
            }
        }

        [HttpPut("book/update")]
        public async Task<ActionResult<int>> UpdateBook(Book book)
        {
            try
            {
                var result = await _bookService.UpdateBook(book);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception("UpdateBook failed in controller: " + ex.Message, ex);
            }
        }

        [HttpDelete("book/delete/{id}")]
        public async Task<ActionResult<int>> DeleteBook(int id)
        {
            try
            {
                var result = await _bookService.DeleteBook(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception("DeleteBook failed in controller: " + ex.Message, ex);
            }
        }

        [HttpGet("genre/list")]
        public async Task<ActionResult<int>> GetGenres()
        {
            try
            {
                List<Genre> genreList = await _bookService.GetGenres();
                return Ok(genreList);
            }
            catch (Exception ex)
            {
                throw new Exception("GetGenres failed in controller: " + ex.Message, ex);
            }
        }
    }
}
