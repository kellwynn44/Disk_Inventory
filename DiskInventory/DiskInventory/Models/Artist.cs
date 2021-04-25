using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace DiskInventory.Models
{
    public partial class Artist
    {
        public Artist()
        {
            MediaArtists = new HashSet<MediaArtist>();
        }

        public int ArtistId { get; set; }


        [Required(ErrorMessage = "Please enter a first name.")]
        [StringLength(60, MinimumLength = 2, ErrorMessage ="Please enter a valid name.")]
        public string FirstName { get; set; }

        [StringLength(60)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter an artist type.")]
        public int ArtistTypeId { get; set; }
        
        public virtual ArtistType ArtistType { get; set; }

        public virtual ICollection<MediaArtist> MediaArtists { get; set; }
    }
}
