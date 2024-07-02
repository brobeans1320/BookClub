namespace BookCubAPI.Services
{
    public interface IBookService
    {
        Task<List<Book>> SelectAllBooks();

        public class BookService : IBookService
        {
            private readonly IBookService _bookService;
            private string _conn;
        }
    }
}
