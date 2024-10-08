﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceWebApp.Models
{
    public class ShoppingCart
    {
        [Key]
        public int Id { get; set; }

        //Relationships

        //CartItems

        public List<CartItem>? CartItems { get; set; } //Product and its quantity

        //User

        public int UserId { get; set; }
        [ForeignKey("UserId")]

        public User User { get; set; }
    }
}
