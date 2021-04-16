﻿using System;
using System.Collections.Generic;

#nullable disable

namespace DiskInventory.Models
{
    public partial class DiskHasBorrower
    {
        public int DiskHasBorrowerId { get; set; }
        public DateTime BorrowedDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnedDate { get; set; }
        public int BorrowerId { get; set; }
        public int MediaId { get; set; }

        public virtual Borrower Borrower { get; set; }
        public virtual Medium Media { get; set; }
    }
}
