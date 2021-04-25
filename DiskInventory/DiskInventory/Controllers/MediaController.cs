using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiskInventory.Models;
using Microsoft.EntityFrameworkCore;

namespace DiskInventory.Controllers
{
    public class MediaController : Controller
    {
        private diskInventoryMPContext context { get; set; }

        public MediaController(diskInventoryMPContext ctx)
        {
            context = ctx;
        }
        public IActionResult Index()
        {
            var mediaItems = context.Media.OrderBy(m => m.Title).ToList();
            return View(mediaItems);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            ViewBag.Genres = context.Genres.OrderBy(g => g.Description).ToList();
            ViewBag.MediaTypes = context.MediaTypes.OrderBy(t => t.Description).ToList();
            ViewBag.LoanStatuses = context.LoanStatuses.OrderBy(s => s.Description).ToList();

            Medium media = new Medium();
            media.ReleaseDate = DateTime.Today;
            return View("Edit", media);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            ViewBag.Genres = context.Genres.OrderBy(g => g.Description).ToList();
            ViewBag.MediaTypes = context.MediaTypes.OrderBy(t => t.Description).ToList();
            ViewBag.LoanStatuses = context.LoanStatuses.OrderBy(s => s.Description).ToList();

            var media = context.Media.Find(id);
            return View(media);
        }

        [HttpPost]
        public IActionResult Edit(Medium media)
        {
            if (ModelState.IsValid)
            {
                if (media.MediaId == 0)
                    context.Media.Add(media);
                else
                    context.Media.Update(media);
                context.SaveChanges();
                return RedirectToAction("Index", "Media");
            }
            else
            {
                ViewBag.Action = (media.MediaId == 0) ? "Add" : "Edit";
                ViewBag.Genres = context.Genres.OrderBy(g => g.Description).ToList();
                ViewBag.MediaTypes = context.MediaTypes.OrderBy(t => t.Description).ToList();
                ViewBag.LoanStatuses = context.LoanStatuses.OrderBy(s => s.Description).ToList();

                return View(media);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var media = context.Media.Find(id);
            return View(media);
        }

        [HttpPost]
        public IActionResult Delete(Medium media)
        {
            context.Media.Remove(media);
            context.SaveChanges();
            return RedirectToAction("Index", "Media");
        }
    }
}
