using System.ComponentModel.DataAnnotations;

namespace FoodOrderApp.Models
{
    public class OrderPositionModel
    {
        [Key]
        public int Id { get; set; }
        public string User { get; set; }
        public string Position { get; set; }
        public string Additives { get; set; }
        public int Cost { get; set; }
    }
}
