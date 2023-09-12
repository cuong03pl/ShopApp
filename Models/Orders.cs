using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopApp.Models
{
    [Table("Orders")]
    public class Orders : CommonModel
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; }
        public string CustomerName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public decimal TotalAmount { get; set; }
        public int Quanlity { get; set; }
        
        public ICollection<OrderDetails> orderDetails { set; get; }
        
    }
}
