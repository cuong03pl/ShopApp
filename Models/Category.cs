using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopApp.Models
{
    [Table("Category")]
    public class Category : CommonModel
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Tên danh mục")]
        [Required(ErrorMessage ="Bắt buộc phải nhập tên danh mục")]
        public string Title { get; set; }

        [Display(Name = "Mô tả")]
        public string Description { get; set; }
        public string Position { get; set; }
        public string SeoTitle { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }

        [Display(Name = "Chuỗi định danh (url)", Prompt = "Nhập hoặc để trống tự phát sinh theo Title")]
        [StringLength(160, MinimumLength = 5, ErrorMessage = "{0} dài {1} đến {2}")]
        [RegularExpression(@"^[a-z0-9-]*$", ErrorMessage = "Chỉ dùng các ký tự [a-z0-9-]")]
        public string? Slug { set; get; }
        public ICollection<New>? news { get; set; }
        public ICollection<Post>? posts { get; set; }

    }
}
