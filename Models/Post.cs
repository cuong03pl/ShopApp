using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopApp.Models
{
    [Table("Post")]
    public class Post : CommonModel
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Detail { get; set; }
        public string? Image { get; set; }
        public string SeoTitle { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }
        public int CategoryID { get; set; }

        [ForeignKey("CategoryID")]
        public Category? category { get; set; }
    }
}
