using System;

namespace Common
{
    public class BookDTO
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
    }
}
