using System;
using System.Collections.Generic;
using Domain;

namespace Application.Core.Model
{
    public class MovieDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public MovieCategory MovieCategory { get; set; }
        public IEnumerable<MovieShowcaseDto> Showcases { get; set; }
    }
}
