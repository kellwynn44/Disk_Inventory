using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        [Required(ErrorMessage ="A title is required.")]
        [StringLength(60, MinimumLength = 2, ErrorMessage ="Please enter a valid title.")]
        public string Title { get; set; }

        [Required(ErrorMessage ="Please enter a date.")]
        public DateTime ReleaseDate { get; set; }

        [Required]
        public int GenreId { get; set; }
        [Required]
        public int MediaTypeId { get; set; }
        [Required]
        public int? LoanStatusId { get; set; }

        //virtual pointers
        public virtual Genre Genre { get; set; }
        public virtual LoanStatus LoanStatus { get; set; }
        public virtual MediaType MediaType { get; set; }
        public virtual ICollection<DiskHasBorrower> DiskHasBorrowers { get; set; }
        public virtual ICollection<MediaArtist> MediaArtists { get; set; }
    }
}
