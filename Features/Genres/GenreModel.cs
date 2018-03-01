using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace vidley.net.Features.Genres
{
    public class GenreModel
    {
        public GenreModel(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        [BindNever]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
    }
}
