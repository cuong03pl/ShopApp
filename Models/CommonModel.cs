using System.ComponentModel.DataAnnotations;

namespace ShopApp.Models
{
    public abstract class CommonModel
    {

        [Display(Name = "Ngày tạo")]
        public DateTime? CreatedDate { get; set; }

        [Display(Name = "Tạo bởi")]
        public string? CreatedBy { get; set; }

        [Display(Name = "Ngày sửa")]
        public DateTime? ModifierDate { get; set; }

        [Display(Name = "Sửa bởi")]
        public string? ModifierBy { get; set; }
    }
}
