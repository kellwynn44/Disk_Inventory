﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace DiskInventory.Models
{
    public partial class DiskHasBorrower
    {
        public int DiskHasBorrowerId { get; set; }

        [Required(ErrorMessage = "Please enter a borrowed date.")]
        public DateTime BorrowedDate { get; set; }

        [Required(ErrorMessage = "Please enter a due date.")]
        public DateTime DueDate { get; set; }
        public DateTime? ReturnedDate { get; set; }

        [Required(ErrorMessage = "Please select a borrower.")]
        public int BorrowerId { get; set; }

        [Required(ErrorMessage = "Please select a title.")]
        public int MediaId { get; set; }

        public virtual Borrower Borrower { get; set; }
        public virtual Medium Media { get; set; }
    }
}
