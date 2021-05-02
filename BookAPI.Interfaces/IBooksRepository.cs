using System;
using System.Collections;
using System.Collections.Generic;
using BookAPI.Common.Entities;
namespace BookAPI.Interfaces
{
    public interface IBooksRepository
    {
        Book GetById(Guid id);
        IEnumerable<Book> Get(int pageNumber, int pageSize); // what is pageNumber and pageSize tho?
        IEnumerable<Book> GetByAuthor(string author);
        IEnumerable<Book> GetByYear(int year);
        Book Add(Book entity);
        bool Update(Book updatedEntity);
        bool Remove(Guid id);

        
    }
}
