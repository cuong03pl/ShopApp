using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopApp.Models
{
    [Table("OrderDetails")]
    public class OrderDetails
    {

        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public int Quanlity { get; set; }

        [ForeignKey("OrderId")]
        public Orders Order { get; set; }

        [ForeignKey("ProductId")]
        public Products Product { get; set; }
    }
}
