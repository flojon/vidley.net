using System;
using System.ComponentModel.DataAnnotations;

namespace vidley.net.Features.Rentals
{
    public class RentalParams
    {
        [Required]
        public string CustomerId { get; set; }

        [Required]
        public string MovieId { get; set; }

        public DateTime? DateRented { get; set; }
        public DateTime? DateReturned { get; set; }
        public decimal RentalFee { get; set; }
    }
}
