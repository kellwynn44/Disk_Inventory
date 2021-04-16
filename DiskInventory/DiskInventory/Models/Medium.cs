using System;
using System.Collections.Generic;

#nullable disable

namespace DiskInventory.Models
{
    public partial class Medium
    {
        public Medium()
        {
            DiskHasBorrowers = new HashSet<DiskHasBorrower>();
            MediaArtists = new HashSet<MediaArtist>();
        }

        public int MediaId { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int GenreId { get; set; }
        public int MediaTypeId { get; set; }
        public int? LoanStatusId { get; set; }

        public virtual Genre Genre { get; set; }
        public virtual LoanStatus LoanStatus { get; set; }
        public virtual MediaType MediaType { get; set; }
        public virtual ICollection<DiskHasBorrower> DiskHasBorrowers { get; set; }
        public virtual ICollection<MediaArtist> MediaArtists { get; set; }
    }
}
