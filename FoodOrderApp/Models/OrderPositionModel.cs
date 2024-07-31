using System.ComponentModel.DataAnnotations;

namespace FoodOrderApp.Models
{
    public class OrderPositionModel
    {
        [Key]
        public int? Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
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
            this.Id = source.Id;
            this.UserId = source.UserId;
            this.UserName = source.UserName;
            this.Position = source.Position;
            this.Comment = source.Comment;
            this.Additives = source.Additives;
            this.Cost = source.Cost;
            this.OrderId = source.OrderId;
            this.IsPaid = source.IsPaid;
        }
    }
}
