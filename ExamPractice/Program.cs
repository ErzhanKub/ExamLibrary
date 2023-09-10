using ExamPractice.Enum;
using ExamPractice.Model;
using ExamPractice.Service;
using System;
using System.Text;

namespace Practic
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            var database = new Database("Server=.; Database=LibraryDb; Trusted_connection=True; Encrypt=Optional");
            var app = new App(database);
            await app.Start();
        }
    }
}