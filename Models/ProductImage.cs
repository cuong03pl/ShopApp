using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopApp.Models
{
    [Table("ProductImage")]
    public class ProductImage
    {
        [Key]
        public int Id { get; set; }
        public string Image { set; get; }
        public bool IsDefault { set; get; }
        public int ProductId { set; get; }

        [ForeignKey("ProductId")]
        public Products products { get; set; }
    }
}
