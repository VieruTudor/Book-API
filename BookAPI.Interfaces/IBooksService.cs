using BookAPI.Common.Entities;
using Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookAPI.Interfaces
{
    public interface IBooksService
    {
        BookDTO GetById(Guid id);
        IEnumerable<BookDTO> Get(int pageNumber, int pageSize); // what is pageNumber and pageSize tho?
        //IEnumerable<BookDTO> GetByAuthor(string author);
        //IEnumerable<BookDTO> GetByYear(int year);
        BookDTO Add(BookDTO entity);
        bool Update(BookDTO updatedEntity);
        bool Remove(Guid id);
    }
}
