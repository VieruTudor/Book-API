using BookAPI.Common.Entities;
using BookAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookAPI.DataPersistence
{
    public class BooksRepository : IBooksRepository
    {
        private IList<Book> books;

        public BooksRepository()
        {
            this.books = new List<Book>();
        }

        public Book Add(Book entity)
        {
            entity.Id = Guid.NewGuid();
            this.books.Add(entity);

            return entity;
        }

        public IEnumerable<Book> Get(int pageNumber, int pageSize)
        {
            return books
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);
        }

        public IEnumerable<Book> GetByAuthor(string author)
        {
            return books.Where(x => x.Author == author);
        }

        public Book GetById(Guid id)
        {
            return books.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Book> GetByYear(int year)
        {
            return books.Where(x => x.Year == year);
        }

        public bool Remove(Guid id)
        {
            var existingEntity = books.FirstOrDefault(x => x.Id == id);
            if (existingEntity == null)
            {
                throw new Exception("Item not found");
            }
            return books.Remove(existingEntity);
        }

        public bool Update(Book updatedEntity)
        {
            var existingEntity = books.FirstOrDefault(x => x.Id == updatedEntity.Id);
            if (existingEntity == null)
            {
                throw new Exception("Item not found");
            }

            existingEntity.Name = updatedEntity.Name;
            existingEntity.Author = updatedEntity.Author;
            existingEntity.Year = updatedEntity.Year;

            return true;
        }
    }
}
