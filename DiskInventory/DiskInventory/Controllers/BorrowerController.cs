using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiskInventory.Models;
using Microsoft.EntityFrameworkCore;

namespace DiskInventory.Controllers
{
    public class BorrowerController : Controller
    {
        private diskInventoryMPContext context { get; set; }

        public BorrowerController(diskInventoryMPContext ctx)
        {
            context = ctx;
        }
        public IActionResult Index()
        {
            var borrowers = context.Borrowers.OrderBy(b => b.LastName).ThenBy(b => b.FirstName).ToList();
            return View(borrowers);
        }
    }
}
