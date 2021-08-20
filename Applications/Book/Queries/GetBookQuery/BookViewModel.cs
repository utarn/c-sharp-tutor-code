using System.ComponentModel.DataAnnotations;

namespace Mvcday1.Applications.Book.Queries.GetBookQuery
{
    public class BookViewModel
    {
        [Display(Name = "รหัสหนังสือ")]
        public int Id { get; set; }
        [Display(Name = "ชื่อหนังสือ")]
        public string Title { get; set; } = default!;
    }
}