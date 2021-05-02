using BookAPI.Common.Entities;
using Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookAPI.Common.Utils
{
    public static class Mapper
    {
        public static Book ToEntity(this BookDTO dto)
        {
            return new Book {
                Id = dto.Id ?? Guid.Empty,
                Name = dto.Name,
                Author = dto.Author,
                Year = dto.Year 
            };
        }

        public static BookDTO ToDTO(this Book entity)
        {
            return new BookDTO
            {
                Id = entity.Id,
                Name = entity.Name,
                Author = entity.Author,
                Year = entity.Year
            };
        }
    }
}
