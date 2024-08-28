using BookClubAPI.Data_Objects;
using Npgsql;
using System.ComponentModel;

namespace BookClubAPI.Services
{
    public interface IBookService
    {
        #region Books
        Task<List<Book>> GetBooks();
        Task<int> CreateBook(Book book);
        Task<int> UpdateBook(Book book);
        Task<int> DeleteBook(int id);
        #endregion

        #region Book Attributes
        Task<List<Genre>> GetGenres();
        #endregion

        public class BookService : IBookService
        {
            private readonly IConfiguration _configuration;
            private string _conn;
            public BookService(IConfiguration configuration)
            {
                _configuration = configuration;
                _conn = _configuration.GetConnectionString("BookServer");
            }

            #region Books
            public async Task<List<Book>> GetBooks()
            {
                try
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
                                        Id = reader.GetGuid(0),
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
                catch (Exception ex)
                {
                    throw new Exception("GetBooks failed in service: " + ex.Message, ex);
                }
            }

            public async Task<int> CreateBook(Book book)
            {
                try
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
                catch (Exception ex)
                {
                    throw new Exception("CreateBook failed in service: " +  ex.Message, ex);
                }
            }

            public async Task<int> UpdateBook(Book book)
            {
                try
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
                catch (Exception ex)
                {
                    throw new Exception("UpdateBook failed in service: " + ex.Message, ex);
                }
            }

            public async Task<int> DeleteBook(int id)
            {
                try
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
                catch (Exception ex)
                {
                    throw new Exception("DeleteBook failed in service: " + ex.Message, ex);
                }
            }
            #endregion

            #region Book Attributes
            public async Task<List<Genre>> GetGenres()
            {
                try
                {
                    var genreList = new List<Genre>();

                    using (var dataSource = NpgsqlDataSource.Create(_conn))
                    {
                        using (var funcGenreSelect = dataSource.CreateCommand("SELECT * FROM bookclub.genres_select()"))
                        {
                            using (var reader = funcGenreSelect.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var newGenre = new Genre
                                    {
                                        Id = reader.GetGuid(0),
                                        Name = reader.GetString(1),
                                        Description = reader.GetString(2)
                                    };
                                    genreList.Add(newGenre);
                                }
                            }
                        }
                    }
                    return genreList;
                }
                catch (Exception ex)
                {
                    throw new Exception("GetGenres failed in service: " + ex.Message, ex);
                }                
            }
            #endregion
        }
    }
}
