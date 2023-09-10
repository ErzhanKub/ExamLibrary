using ExamPractice.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamPractice.Model
{
    public class Book
    {
        public int Id { get; set; }
        private string _title;
        public string Title
        {
            get => _title; set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Название книги не может быть пустым", nameof(value));
                }
                _title = value;
            }
        }
        private string _author;
        public string Author
        {
            get => _author;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Автор книги не может быть пустым", nameof(value));
                }
                _author = value;
            }
        }
        public int Year { get; set; }
        public GenreType Genre { get; set; }
        public override string ToString() => $"Название: {_title} | Автор: {_author} | Год издания: {Year} | Жанр: {Genre} | ID: {Id}";
    }
}
