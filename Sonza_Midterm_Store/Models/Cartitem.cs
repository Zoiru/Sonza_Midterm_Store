using System.ComponentModel.DataAnnotations.Schema;

namespace Sonza_Midterm_Store.Models
{
    public class CartItem
    {
        public int Id { get; set; }

        // Foreign key to Product
        public int ProductId { get; set; }

        public int Quantity { get; set; }

        // Navigation property
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
