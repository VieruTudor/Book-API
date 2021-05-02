using Book_API.Model;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_API.Utils
{
    public static class MapperUtil
    {
        public static BookModel ToModel(this BookDTO dto)
        {
            return new BookModel
            {
                Id = dto.Id ?? Guid.Empty,
                Name = dto.Name,
                Author = dto.Author,
                Year = dto.Year
            };
        }

        public static BookDTO ToDTO(this BookModel model, Guid? id)
        {
            return new BookDTO
            {
                Id = model.Id,
                Name = model.Name,
                Author = model.Author,
                Year = model.Year
            };
        }
    }
}
