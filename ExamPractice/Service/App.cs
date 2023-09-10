using ExamPractice.Enum;
using ExamPractice.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamPractice.Service
{
    public class App
    {
        public readonly Database Database;
        public App(Database database)
        {
            Database = database;
        }
        public async Task Start()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.Unicode;
            Console.WriteLine("Консольное приложение для чтения/создания/изменения/удаления книг в базе данных библиотеки.");
            var exit = false;
            while (!exit)
            {
                Console.WriteLine("Навигация:\n1 - Найти книгу по названию" +
                    "\n2 - Добавить книгу" +
                    "\n3 - Получить все книги" +
                    "\n4 - Удалить книгу" +
                    "\n5 - Изменить книгу" +
                    "\nESC - для выхода");
                ConsoleKeyInfo key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.D1: await FoundBook(); break;

                    case ConsoleKey.D2: await AddBook(); break;

                    case ConsoleKey.D3: await GetAllBooks(); break;

                    case ConsoleKey.D4: await DeleteBook(); break;

                    case ConsoleKey.D5: await EditBook(); break;

                    case ConsoleKey.Escape:
                        exit = true;
                        break;
                }
                Console.ReadKey();
                Console.Clear();
            }
        }
        private async Task EditBook()
        {
            var IDedit = ReadInt("\nУкажите ID изменяемой книги: ");
            var titleEditBook = ReadString("Название книги: ");
            var authorEditBook = ReadString("Автор: ");
            var yearEditBook = ReadInt("Год издания: ");
            var genreChoiseEditBook = ReadInt("Жанр: ");
            GenreType genreEditBook = GenreType.Детектив;
            switch (genreChoiseEditBook)
            {
                case 1:
                    genreEditBook = GenreType.Детектив;
                    break;
                case 2:
                    genreEditBook = GenreType.Драма;
                    break;
                case 3:
                    genreEditBook = GenreType.Фэнтези;
                    break;
                default:
                    Console.WriteLine("Неверный выбор жанра. Пожалуйста, попробуйте снова.");
                    break;
            }
            var editBook = new Book()
            {
                Title = titleEditBook,
                Author = authorEditBook,
                Year = yearEditBook,
                Genre = genreEditBook,
            };
            await Database.EditBook(editBook, IDedit);
        }
        private async Task DeleteBook()
        {
            var IDdelete = ReadInt("\nУкажите ID книги: ");
            await Database.DeleteBook(IDdelete);
        }
        private async Task GetAllBooks()
        {
            var books = await Database.GetAllBooks();
            foreach (var book in books)
            {
                Console.WriteLine($"Название: {book.Title} | Автор: {book.Author} | Год издания: {book.Year} | Жанр: {book.Genre} | ID: {book.Id}");
            }
        }
        private async Task AddBook()
        {
            var titleNewBook = ReadString("\nНазвание книги: ");
            var authorNewBook = ReadString("Автор: ");
            var yearNewBook = ReadInt("Год издания: ");
            var genreChoise = ReadInt("Жанр: ");
            GenreType genre = GenreType.Детектив;
            switch (genreChoise)
            {
                case 1:
                    genre = GenreType.Детектив;
                    break;
                case 2:
                    genre = GenreType.Драма;
                    break;
                case 3:
                    genre = GenreType.Фэнтези;
                    break;
                default:
                    Console.WriteLine("Неверный выбор жанра. Пожалуйста, попробуйте снова.");
                    break;
            }
            var newBook = new Book()
            {
                Title = titleNewBook,
                Author = authorNewBook,
                Year = yearNewBook,
                Genre = genre,
            };
            await Database.AddBook(newBook);
        }
        private async Task FoundBook()
        {
            var nameBook = ReadString("\nНазвание книги: ");
            var getbook = await Database.GetBookByName(nameBook);
            if (getbook != null)
            {
                Console.WriteLine($"Название: {getbook.Title} | Автор: {getbook.Author} | Год издания: {getbook.Year} | Жанр: {getbook.Genre} | ID: {getbook.Id}");
            }
            else
            {
                Console.WriteLine("Книга не найдена");
            };
        }
        private string ReadString(string str)
        {
            Console.Write(str);
            var readString = Console.ReadLine();
            if (readString != null)
            {
                return readString;
            }
            else
            {
                throw new ArgumentNullException("Пустое поле", nameof(readString));
            }
        }
        private int ReadInt(string str)
        {
            Console.Write(str);
            return Convert.ToInt32(Console.ReadLine());
        }
    }
}
