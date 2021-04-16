using System;
using System.Collections.Generic;

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
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string PhoneNum { get; set; }

        public virtual ICollection<DiskHasBorrower> DiskHasBorrowers { get; set; }
    }
}
