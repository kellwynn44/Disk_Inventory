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

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            return View("Edit", new Borrower());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            var borrowers = context.Borrowers.Find(id);
            return View(borrowers);
        }

        [HttpPost]
        public IActionResult Edit(Borrower borrower)
        {
            if (ModelState.IsValid)
            {
                if (borrower.BorrowerId == 0)
                {
                    //context.Borrowers.Add(borrower);
                    //new code to use stored procedure
                    context.Database.ExecuteSqlRaw("execute sp_insert_borrower @p0, @p1, @p2",
                        parameters: new[] {borrower.LastName, borrower.FirstName,
                        borrower.PhoneNum.ToString()});
                }

                else
                {
                     //context.Borrowers.Update(borrower);
                     //new code to use stored procedure
                    context.Database.ExecuteSqlRaw("execute sp_update_borrower @p0, @p1, @p2, @p3",
                        parameters: new[] {borrower.BorrowerId.ToString(), borrower.LastName, 
                        borrower.FirstName, borrower.PhoneNum.ToString()});
                }
                   
                //context.SaveChanges();
                return RedirectToAction("Index", "Borrower");
            }
            else
            {
                ViewBag.Action = (borrower.BorrowerId == 0) ? "Add" : "Edit";
                return View(borrower);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var borrower = context.Borrowers.Find(id);
            return View(borrower);
        }

        [HttpPost]
        public IActionResult Delete(Borrower borrower)
        {
            //context.Borrowers.Remove(borrower);
            //new code for stored procedure
            context.Database.ExecuteSqlRaw("execute sp_delete_borrower @p0",
                    parameters: new[] {borrower.BorrowerId.ToString()});

            //context.SaveChanges();
            return RedirectToAction("Index", "Borrower");
        }
    }
}
