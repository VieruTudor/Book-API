using BookAPI.Common.Utils;
using BookAPI.Interfaces;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookAPI.Services
{
    public class BooksService : IBooksService
    {
        // implement later
        private readonly IBooksRepository booksRepository;

        public BooksService(IBooksRepository booksRepository)
        {
            this.booksRepository = booksRepository;
        }

        public BookDTO GetById(Guid id)
        {
            var returnedBook = this.booksRepository.GetById(id)?.ToDTO();
            return returnedBook;
        }

        public IEnumerable<BookDTO> Get(int pageNumber = 1, int pageSize = 100)
        {
            var results = booksRepository.Get(pageNumber, pageSize).ToList();
            return results.Any() ? results.Select(x => x.ToDTO()) : new List<BookDTO>();
        }

        public BookDTO Add(BookDTO entity)
        {
            return booksRepository.Add(entity.ToEntity()).ToDTO();
        }

        public bool Update(BookDTO updatedEntity)
        {
            return booksRepository.Update(updatedEntity.ToEntity());
        }

        public bool Remove(Guid id)
        {
            return booksRepository.Remove(id);
        }
    }
}
