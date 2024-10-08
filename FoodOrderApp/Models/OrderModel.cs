﻿using System.ComponentModel.DataAnnotations;

namespace FoodOrderApp.Models
{
    public class OrderModel
    {
        [Key]
        public int? Id { get; set; }
        public string Orderer { get; set; }
        public string OrdererName { get; set; }
        [Required(ErrorMessage ="Nazwa restauracji jest wymagana!")]
        [StringLength(100,
            MinimumLength =1, ErrorMessage = "")]
        public string? RestaurantName { get; set; }
        [Required(ErrorMessage = "Minimalna wartość zamóienia jest wymagana!")]
        [Range(0, int.MaxValue,
            ErrorMessage = "Minimalna wartość zamóienia musi być dodatnia!")]
        public double? MinCost { get; set; }
        public double? CurrentCost { get; set; }
        [Required(ErrorMessage = "Koszt dostawy jest wymagany!")]
        [Range(0, int.MaxValue,
            ErrorMessage = "Koszt dostawy musi być dodatni!")]
        public double? DeliveryFee { get; set; }
        [Range(0, int.MaxValue,
            ErrorMessage = "Minimalna kwota dla darmowego dowozu musi być dodatnia!")]
        public double? MinCostForFreeDelivery { get; set; }
        [Required(ErrorMessage = "Numer telefonu jest wymagany!")]
        [Phone]
        public string? PhoneNumber { get; set; }
        [Required(ErrorMessage = "Numer konta jest wymagany!")]
        public string? AccountNumber { get; set; }
        public bool IsClosed { get; set; }

        public OrderModel() { }
        public OrderModel(OrderModel order) 
        {
            this.Id = order.Id;
            this.Orderer = order.Orderer;
            this.OrdererName = order.OrdererName;
            this.RestaurantName = order.RestaurantName;
            this.MinCost = order.MinCost;
            this.CurrentCost = order.CurrentCost;
            this.MinCostForFreeDelivery = order.MinCostForFreeDelivery;
            this.DeliveryFee = order.DeliveryFee;
            this.PhoneNumber = order.PhoneNumber;
            this.AccountNumber = order.AccountNumber;
            this.IsClosed = order.IsClosed;
        }
    }
    
}
