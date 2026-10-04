using System;

namespace Lab2
{
    public class Book : Publication
    {
        private string isbn;
        private int pageCount;

        public string Isbn
        {
            get { return isbn; }
        }

        public int PageCount
        {
            get { return pageCount; }
        }

        public Book(string title, string uniqueId, int year, string author,
                    string isbn, int pageCount)
            : base(title, uniqueId, year, author)
        {
            if (isbn == null || isbn == "")
                throw new ArgumentException("ISBN не может быть пустым.");

            if (pageCount <= 0)
                throw new ArgumentOutOfRangeException("Количество страниц должно быть больше нуля.");

            this.isbn = isbn;
            this.pageCount = pageCount;
        }

        public override string GetPublicationType()
        {
            return "Книга";
        }

        public override string ToString()
        {
            return base.ToString() + ", ISBN: " + Isbn + ", стр.: " + PageCount;
        }
    }
}