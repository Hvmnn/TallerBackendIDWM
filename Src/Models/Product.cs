using System.ComponentModel.DataAnnotations;

namespace TallerBackendIDWM.Src.Models
{
    using System;
    using Microsoft.AspNetCore.Http;

    public class Product 
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Type { get; set; }
        public required int Price { get; set; }
        public required int Stock { get; set; }
        public required string Image { get; set; }
        
    }

}