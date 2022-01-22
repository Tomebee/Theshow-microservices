using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain
{
    public class Movie : Entity
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string ShortDescription { get; private set; }
        public string Description { get; private set; }
        public Uri ImageUrl { get; private set; }
        public MovieCategory MovieCategory { get; private set; }

        public virtual ICollection<MovieShowcase> Showcases { get; private set; }

        internal Movie()
        {

        }

        internal Movie(string name, string shortDescription, string description, Uri imageUrl, MovieCategory movieCategory, IEnumerable<MovieShowcase> showcases)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new DomainException("Nazwa filmu jest wymagana.");
            }

            if (string.IsNullOrWhiteSpace(shortDescription) || string.IsNullOrWhiteSpace(description))
            {
                throw new DomainException("Opis filmu jak i krótki opis muszą być wypełnione.");
            }

            if (string.IsNullOrWhiteSpace(imageUrl?.AbsoluteUri))
            {
                throw new DomainException("Miniaturka filmu jest wymagana.");
            }

            Id = Guid.NewGuid();
            Name = name;
            ShortDescription = shortDescription;
            Description = description;
            MovieCategory = movieCategory;
            ImageUrl = imageUrl;
            Showcases = showcases?.ToList();
        }
    }
}
