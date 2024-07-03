using Npgsql;

namespace BookClubAPI.Services
{
    public interface IBookService
    {
        Task<List<Book>> SelectAllBooks();

        public class BookService : IBookService
        {
            private readonly IConfiguration _configuration;
            private string _conn;
            public BookService(IConfiguration configuration)
            {
                _configuration = configuration;
                _conn = _configuration.GetConnectionString("BookServer");
            }

            public async Task<List<Book>> SelectAllBooks()
            {
                List<Book> bookList = new List<Book>();

                using (var dataSource = NpgsqlDataSource.Create(_conn))
                {
                    //dataSource.OpenConnection(); //Not needed, connection internally arranged
                    using (var funcSelectAll = dataSource.CreateCommand("SELECT * FROM select_all_books()"))
                    {
                        using (var reader = funcSelectAll.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Book newBook = new Book();

                                newBook.Id = reader.GetInt32(0);
                                newBook.Title = reader.GetString(1);
                                newBook.Author = reader.GetString(2);
                                newBook.PublicationDate = reader.GetDateTime(3);
                                newBook.PageCount = reader.GetInt32(4);
                                newBook.Genre = reader.GetString(5);
                                newBook.CoverImageUrl = reader.GetString(6);
                                newBook.Tags = new List<string>(reader.GetFieldValue<string[]>(7));
                                newBook.IsRead = reader.GetBoolean(8);

                                Console.WriteLine(newBook.PublicationDate.Date);

                                bookList.Add(newBook);
                            }
                        }
                    }
                }

                //Older version of 
                //using (var conn = new NpgsqlConnection(_conn))
                //{
                //    conn.Open();
                //    using (var funcSelectAll = new NpgsqlCommand("SELECT * FROM select_all_books()", conn))
                //    {
                //        using (var reader = funcSelectAll.ExecuteReader())
                //        {
                //            while (reader.Read())
                //            {
                //                Book newBook = new Book();

                //                newBook.Author = reader["title"].ToString();

                //                bookList.Add(newBook);
                //            }
                //        }
                //    }
                //}
                return bookList;
            }
        }
    }
}
