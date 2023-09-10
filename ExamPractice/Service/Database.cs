using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Dapper;
using ExamPractice.Model;

namespace ExamPractice.Service
{
    public class Database
    {
        private readonly string _connection;
        public Database(string connectionString)
        {
            _connection = connectionString;
        }
        public async Task<Book> GetBookByName(string title)
        {
            if (title != null)
            {
                using var connection = new SqlConnection(_connection);
                {
                    var sql = $"SELECT * FROM Books where Title = @title";
                    var book = await connection.QueryFirstOrDefaultAsync<Book>(sql, new { title });
                    Console.WriteLine("\nУспешно завершено!");
                    return book;
                }
            }
            else
            {
                throw new ArgumentNullException("Название не может быть null", nameof(title));
            }

        }
        public async Task AddBook(Book book)
        {
            using var connection = new SqlConnection(_connection);
            {
                var sql = "INSERT INTO Books Values (@Title, @Author, @Year, @Genre)";
                await connection.ExecuteAsync(sql, new { book.Title, book.Author, book.Year, Genre = (int)book.Genre });
                Console.WriteLine("\nУспешно завершено!");
            }
        }
        public async Task DeleteBook(long id)
        {
            using var connection = new SqlConnection(_connection);
            {
                var sql = "DELETE FROM Books WHERE Id = @Id";
                await connection.ExecuteAsync(sql, new { Id = id });
                Console.WriteLine("\nУспешно завершено!");
            }
        }
        public async Task EditBook(Book book, long id)
        {
            using var connection = new SqlConnection(_connection);
            {
                var sql = "UPDATE Books SET Title = @Title, Author = @Author, [Year] = @Year, Genre = @Genre WHERE Id = @id";
                await connection.ExecuteAsync(sql, new { book.Title, book.Author, book.Year, Genre = (int)book.Genre, id });
                Console.WriteLine("\nУспешно завершено!");
            }
        }
        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            using var connection = new SqlConnection(_connection);
            {
                var sql = "SELECT * FROM Books";
                var books = await connection.QueryAsync<Book>(sql);
                Console.WriteLine("\nУспешно завершено!");
                return books;
            }

        }
    }
}
