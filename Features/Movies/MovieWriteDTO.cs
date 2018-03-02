
using System.ComponentModel.DataAnnotations;

namespace vidley.net.Features.Movies
{
    public class MovieWriteDTO
    {

        [Required]
        public string Title { get; set; }

        [Required]
        public string GenreId { get; set; }
        
        public int NumberInStock { get; set; }

        public int DailyRentalRate { get; set; }
    }
}
