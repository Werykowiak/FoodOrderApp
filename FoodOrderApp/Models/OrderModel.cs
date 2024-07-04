using System.ComponentModel.DataAnnotations;

namespace FoodOrderApp.Models
{
    public class OrderModel
    {
        [Required]
        [Key]
        public int? Id { get; set; }
        public string Orderer { get; set; }
        [Required]
        public string? RestaurantName { get; set; }
        [Required]
        public double? MinCost { get; set; }
        [Required]
        public double? DeliveryFee { get; set; }
        public double? MinCostForFreeDelivery { get; set; }
        public int? PhoneNumber { get; set; }
        [Required]
        public int? AccountNumber { get; set; }
    }
}
