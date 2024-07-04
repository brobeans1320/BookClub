using Npgsql;
using System.ComponentModel;

namespace BookClubAPI.Services
{
    public interface IBookService
    {
        Task<List<Book>> SelectAllBooks();
        Task<int> CreateBook(Book book);
        Task<int> UpdateBook(Book book);
        Task<int> DeleteBook(int id);


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
                                var newBook = new Book
                                {
                                    Id = reader.GetInt32(0),
                                    Title = reader.GetString(1),
                                    AuthorFirstName = reader.GetString(2),
                                    AuthorLastName = reader.GetString(3),
                                    PublicationDate = reader.GetDateTime(4),
                                    PageCount = reader.GetInt32(5),
                                    Genre = reader.IsDBNull(6) ? null : reader.GetString(6),
                                    CoverImageUrl = reader.IsDBNull(7) ? null : reader.GetString(7),
                                    Tags = reader.IsDBNull(8) ? new List<string>() : new List<string>(reader.GetFieldValue<string[]>(8)),
                                    IsRead = reader.GetBoolean(9)
                                };

                                bookList.Add(newBook);
                            }
                        }
                    }
                }
                return bookList;
            }

            public async Task<int> CreateBook(Book book)
            {
                var result = 0;

                using (var conn = new NpgsqlConnection(_conn))
                {
                    conn.Open();
                    using (var cmdCreate = new NpgsqlCommand("SELECT create_book($1, $2, $3, $4, $5, $6, $7, $8, $9)", conn))
                    {
                        cmdCreate.Parameters.AddRange(new[]
                        {
                            new NpgsqlParameter { Value = book.Title },
                            new NpgsqlParameter { Value = book.AuthorFirstName },
                            new NpgsqlParameter { Value = book.AuthorLastName },
                            new NpgsqlParameter { Value = book.PublicationDate, NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Date },
                            new NpgsqlParameter { Value = book.PageCount },
                            new NpgsqlParameter { Value = book.Genre },
                            new NpgsqlParameter { Value = book.CoverImageUrl },
                            new NpgsqlParameter { Value = book.Tags.ToArray() },
                            new NpgsqlParameter { Value = book.IsRead }
                        });


                        result = (int)cmdCreate.ExecuteScalar();
                    }
                }
                return result;
            }

            public async Task<int> UpdateBook(Book book)
            {
                var result = 0;

                using (var dataSource = NpgsqlDataSource.Create(_conn))
                {
                    using (var cmdUpdate = dataSource.CreateCommand("SELECT update_book($1, $2, $3, $4, $5, $6, $7, $8, $9, $10)"))
                    {
                        cmdUpdate.Parameters.AddRange(new[]
                        {
                            new NpgsqlParameter { Value = book.Id },
                            new NpgsqlParameter { Value = book.Title },
                            new NpgsqlParameter { Value = book.AuthorFirstName },
                            new NpgsqlParameter { Value = book.AuthorLastName },
                            new NpgsqlParameter { Value = book.PublicationDate, NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Date },
                            new NpgsqlParameter { Value = book.PageCount },
                            new NpgsqlParameter { Value = book.Genre },
                            new NpgsqlParameter { Value = book.CoverImageUrl },
                            new NpgsqlParameter { Value = book.Tags.ToArray() },
                            new NpgsqlParameter { Value = book.IsRead }
                        });

                        result = (int)cmdUpdate.ExecuteScalar();
                    }
                }
                return result;
            }

            public async Task<int> DeleteBook(int id)
            {
                var result = 0;

                using (var dataSource = NpgsqlDataSource.Create(_conn))
                {
                    using (var cmdDelete = dataSource.CreateCommand("SELECT delete_book($1)"))
                    {
                        cmdDelete.Parameters.Add(new NpgsqlParameter { Value = id });
                        result = (int)cmdDelete.ExecuteScalar();
                    }
                }
                return result;
            }
        }
    }
}
