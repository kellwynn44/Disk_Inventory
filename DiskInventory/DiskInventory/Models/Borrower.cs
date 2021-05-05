using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace DiskInventory.Models
{
    public partial class Borrower
    {
        public Borrower()
        {
            DiskHasBorrowers = new HashSet<DiskHasBorrower>();
        }

        public int BorrowerId { get; set; }

        [Required(ErrorMessage = "Please enter a first name.")]
        [StringLength(60, MinimumLength = 2, ErrorMessage = "Please enter a valid first name.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter a last name.")]
        [StringLength(60, MinimumLength = 2, ErrorMessage ="Please enter a valid last name.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter a phone number.")]
        [StringLength(15, MinimumLength = 10, ErrorMessage = "Please enter a valid phone number.")]
        public string PhoneNum { get; set; }

        public virtual ICollection<DiskHasBorrower> DiskHasBorrowers { get; set; }
    }
}
