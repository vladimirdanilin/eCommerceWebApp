﻿using eCommerceWebApp.Data;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerceWebApp.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string FullName { get; set; }
        
        public string? PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        [NotMapped]
        public Role Role { get; set; }

        //public List<Address> ShippingAddresses { get; set; }

        public ShoppingCart? ShoppingCart { get; set; }

        public string? ProfilePictureURL { get; set; }

        public List<Order>? Orders { get; set; }

        //Relationships

        public List<User_Address>? Users_Addresses { get; set; }
    }
}