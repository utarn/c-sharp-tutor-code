namespace Mvcday1.Applications.Book.Queries.GetBookEditQuery
{
    public class BookEditViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
    }
}