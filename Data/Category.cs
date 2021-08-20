using System.Collections.Generic;

namespace Mvcday1.Data
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public virtual ICollection<Book> Books { get; }
        public Category()
        {
            Books = new List<Book>();
        }
    }
}