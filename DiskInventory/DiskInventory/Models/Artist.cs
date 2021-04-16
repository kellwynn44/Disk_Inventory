using System;
using System.Collections.Generic;

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
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int ArtistTypeId { get; set; }

        public virtual ArtistType ArtistType { get; set; }
        public virtual ICollection<MediaArtist> MediaArtists { get; set; }
    }
}
