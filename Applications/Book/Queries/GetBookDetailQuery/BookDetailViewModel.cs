using System.ComponentModel.DataAnnotations;

namespace Mvcday1.Applications.Book.Queries.GetBookDetailQuery
{
    public class BookDetailViewModel
    {
        [Display(Name = "รหัสหนังสือ")]
        public int Id { get; set; }
        [Display(Name = "ชื่อหนังสือ")]
        public string Title { get; set; } = default!;
        [Display(Name = "ราคา")]
        public decimal Price { get; set; }
        [Display(Name = "ประเภท")]
        public string Category { get; set; } = default!;
    }
}