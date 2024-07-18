using System.ComponentModel.DataAnnotations;

namespace FoodOrderApp.Models
{
    public class OrderPositionModel
    {
        [Key]
        public int? Id { get; set; }
        public string UserId { get; set; }
        [Required(ErrorMessage ="Treść zamóienia jest wymagana!")]
        [StringLength(50)]
        public string Position { get; set; }
        public string? Comment { get; set; }
        [StringLength(50)]
        public string? Additives { get; set; }
        [Required(ErrorMessage ="Koszt jest wymagany!")]
        public double? Cost { get; set; }
        public int OrderId { get; set; }
        public bool IsPaid { get; set; }

        public OrderPositionModel() { }
        public OrderPositionModel(OrderPositionModel source)
        {
            Id = source.Id;
            UserId = source.UserId;
            Position = source.Position;
            Comment = source.Comment;
            Additives = source.Additives;
            Cost = source.Cost;
            OrderId = source.OrderId;
            IsPaid = source.IsPaid;
        }
    }
}
