using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DiskInventory.Models;

namespace DiskInventory.Controllers
{
    public class DiskHasBorrowerController : Controller
    {
        private diskInventoryMPContext context { get; set; }

        public DiskHasBorrowerController(diskInventoryMPContext ctx)
        {
            context = ctx;
        }
        public IActionResult Index()
        {
            var diskHasBorrower = context.DiskHasBorrowers
                .Include(d => d.Media).OrderBy(d => d.Media.Title)
                .Include(b => b.Borrower).ToList();

            return View(diskHasBorrower);
        }
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            ViewBag.Media = context.Media.OrderBy(g => g.Title).ToList();
            ViewBag.Borrowers = context.Borrowers.OrderBy(t => t.LastName).ToList();

            DiskHasBorrower newDiskHasBorrower = new DiskHasBorrower();
            newDiskHasBorrower.BorrowedDate = DateTime.Today;
            newDiskHasBorrower.DueDate = DateTime.Today;
            return View("Edit", newDiskHasBorrower);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            ViewBag.Media = context.Media.OrderBy(g => g.Title).ToList();
            ViewBag.Borrowers = context.Borrowers.OrderBy(t => t.LastName).ToList();

            var diskHasBorrower = context.DiskHasBorrowers.Find(id);
            return View(diskHasBorrower);
        }
        [HttpPost]
        public IActionResult Edit(DiskHasBorrower diskHasBorrower)
        {
            if (ModelState.IsValid)
            {
                if (diskHasBorrower.DiskHasBorrowerId == 0)
                    context.DiskHasBorrowers.Add(diskHasBorrower);
                else
                    context.DiskHasBorrowers.Update(diskHasBorrower);
                context.SaveChanges();
                return RedirectToAction("Index", "DiskHasBorrower");
            }
            else
            {
                ViewBag.Action = (diskHasBorrower.DiskHasBorrowerId == 0) ? "Add" : "Edit";
                ViewBag.Media = context.Media.OrderBy(g => g.Title).ToList();
                ViewBag.Borrowers = context.Borrowers.OrderBy(t => t.LastName).ToList();

                return View(diskHasBorrower);
            }
        }

        //[HttpGet]
        //public IActionResult Delete(int id)
        //{
        //    var diskHasBorrower = context.DiskHasBorrowers.Find(id);
        //    return View(diskHasBorrower);
        //}

        //[HttpPost]
        //public IActionResult Delete(DiskHasBorrower diskHasBorrower)
        //{
        //    context.DiskHasBorrowers.Remove(diskHasBorrower);
        //    context.SaveChanges();
        //    return RedirectToAction("Index", "DiskHasBorrower");
        //}
    }
}
