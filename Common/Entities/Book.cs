using System;
using System.Collections.Generic;
using System.Text;

namespace BookAPI.Common.Entities
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
    }
}
