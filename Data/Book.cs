
namespace Mvcday1.Data
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public decimal Price { get; set; }
        public Category Category { get; set; } = default!;
        public int CategoryId { get; set; }
    }
}