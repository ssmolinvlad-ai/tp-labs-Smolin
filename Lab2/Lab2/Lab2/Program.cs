using System;
using System.Collections.Generic;

namespace Lab2
{
    class Program
    {
        static void Main()
        {
            Library library = new Library();

            library.AddPublication(new Book("Война и мир", "B001", 1869, "Лев Толстой", "978-5-17-123456-7", 1300));
            library.AddPublication(new Book("Преступление и наказание", "B002", 1866, "Фёдор Достоевский", "978-5-17-789012-3", 600));
            library.AddPublication(new Magazine("Наука и жизнь", "M001", 2024, "Редакция", 12, "Наука-Пресс"));
            library.AddPublication(new Magazine("Вокруг света", "M002", 2023, null, 7, "Вокруг света"));
            library.AddPublication(new ElectronicEdition("Матрица (сценарий)", "E001", 1999, "Лана Вачовски", "PDF", 2.5));
            library.AddPublication(new ElectronicEdition("Начало (сценарий)", "E002", 2010, "Кристофер Нолан", "EPUB", 3.1));

            Console.WriteLine("=== Все публикации ===");
            library.PrintAll();

            // Выдача
            Console.WriteLine("\n--- Выдаём книгу B001 ---");
            library.GiveItem("B001");
            Console.WriteLine("После выдачи:");
            library.PrintAll();

            // Возврат
            Console.WriteLine("\n--- Возвращаем книгу B001 ---");
            library.ReturnItem("B001");
            Console.WriteLine("После возврата:");
            library.PrintAll();

            // Поиск по автору
            Console.WriteLine("\n=== Поиск по автору 'Толстой' ===");
            var found = library.FindByAuthor("Толстой");
            foreach (var p in found)
                Console.WriteLine($"  {p}");

            Console.WriteLine("\n=== Поиск по автору 'вач' (частичное) ===");
            found = library.FindByAuthor("вач");
            foreach (var p in found)
                Console.WriteLine($"  {p}");

            Console.WriteLine("\nНажмите любую клавишу для завершения...");
            Console.ReadKey();
        }
    }
}