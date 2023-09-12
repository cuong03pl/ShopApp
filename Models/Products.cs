using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopApp.Models
{
    [Table("Products")]
    public class Products : CommonModel
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Detail { get; set; }

        public string Price { get; set; }
        public string PriceDiscount { get; set; }
        public string Quanlity { get; set; }
        public string SeoTitle { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }

        public int ProductCategoryID { get; set; }
        [ForeignKey("ProductCategoryID")]
        public ProductCategory productCategory { get; set; }


        public ICollection<ProductImage> Image { get; set; }
    }
}
