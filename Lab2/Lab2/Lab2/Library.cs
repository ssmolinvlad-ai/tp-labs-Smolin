using System;
using System.Collections.Generic;

namespace Lab2
{
    public class Library
    {
        private List<Publication> _items = new List<Publication>();

        public void AddPublication(Publication item) => _items.Add(item);

        private Publication FindById(string uniqueId)
        {
            foreach (var p in _items)
                if (p.UniqueId == uniqueId)
                    return p;
            return null;
        }

        public void GiveItem(string uniqueId)
        {
            var found = FindById(uniqueId);
            if (found == null)
                throw new ArgumentException($"Публикация с ID '{uniqueId}' не найдена.");
            found.Issue();  
        }

        public void ReturnItem(string uniqueId)
        {
            var found = FindById(uniqueId);
            if (found == null)
                throw new ArgumentException($"Публикация с ID '{uniqueId}' не найдена.");
            found.Return(); 
        }

        public List<Publication> FindByAuthor(string authorName)
        {
            List<Publication> result = new List<Publication>();
            if (string.IsNullOrWhiteSpace(authorName))
                return result;

            string search = authorName.ToLower();
            foreach (var p in _items)
            {
                if (p.Author.ToLower().Contains(search))
                    result.Add(p);
            }
            return result;
        }

        public void PrintAll()
        {
            foreach (var p in _items)
                Console.WriteLine(p);
        }
    }
}