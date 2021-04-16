using System;
using System.Collections.Generic;

#nullable disable

namespace DiskInventory.Models
{
    public partial class LoanStatus
    {
        public LoanStatus()
        {
            Media = new HashSet<Medium>();
        }

        public int LoanStatusId { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Medium> Media { get; set; }
    }
}
