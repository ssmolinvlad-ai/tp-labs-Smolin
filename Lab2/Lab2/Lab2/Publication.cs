using System;

namespace Lab2
{
    // Базовый абстрактный класс для всех публикаций
    public abstract class Publication
    {
        private string title;
        private string uniqueId;
        private int year;
        private string author;
        private bool isAvailable;

        public string Title
        {
            get { return title; }
        }

        public string UniqueId
        {
            get { return uniqueId; }
        }

        public int Year
        {
            get { return year; }
        }

        public string Author
        {
            get { return author; }
        }

        public bool IsAvailable
        {
            get { return isAvailable; }
        }

        // Конструктор
        public Publication(string title, string uniqueId, int year, string author)
        {
            if (title == null || title == "")
                throw new ArgumentException("Название не может быть пустым.");

            if (uniqueId == null || uniqueId == "")
                throw new ArgumentException("ID не может быть пустым.");

            if (year < 0 || year > DateTime.Now.Year + 1)
                throw new ArgumentOutOfRangeException("Год указан неверно.");

            this.title = title;
            this.uniqueId = uniqueId;
            this.year = year;

            if (author == null)
                this.author = "";
            else
                this.author = author;

            this.isAvailable = true; 
        }

        public abstract string GetPublicationType();

        public void Issue()
        {
            if (isAvailable == false)
                throw new InvalidOperationException("Публикация уже выдана.");

            isAvailable = false;
        }

        public void Return()
        {
            if (isAvailable == true)
                throw new InvalidOperationException("Публикация уже доступна.");

            isAvailable = true;
        }

        public override string ToString()
        {
            string result = GetPublicationType() + ": '" + Title + "' (ID: " + UniqueId + ", " + Year + ")";

            if (Author != "")
                result = result + ", Автор: " + Author;

            if (IsAvailable)
                result = result + ", Доступна";
            else
                result = result + ", Выдана";

            return result;
        }
    }
}