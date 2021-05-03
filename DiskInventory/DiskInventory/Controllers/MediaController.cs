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
            //var mediaItems = context.Media.OrderBy(m => m.Title).ToList();
            var mediaItems = context.Media.OrderBy(m => m.Title)
                .Include(g => g.Genre).Include(t => t.MediaType)
                .Include(s =>s.LoanStatus).ToList();
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
                {
                    //context.Media.Add(media);
                    //new code for stored procedure
                    context.Database.ExecuteSqlRaw("execute sp_insert_media @p0, @p1, @p2, @p3, @p4",
                        parameters: new[] {media.Title, media.ReleaseDate.ToString(), 
                        media.GenreId.ToString(), media.MediaTypeId.ToString(), 
                            media.LoanStatusId.ToString()});
                }

                else
                {
                    //context.Media.Update(media);
                    //new code for stored procedure
                    context.Database.ExecuteSqlRaw("execute sp_update_media @p0, @p1, @p2, @p3, @p4, @p5",
                        parameters: new[] {media.MediaId.ToString(), media.Title, media.ReleaseDate.ToString(),
                        media.GenreId.ToString(), media.MediaTypeId.ToString(),
                            media.LoanStatusId.ToString()});
                }
                    
                //context.SaveChanges();
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
            //context.Media.Remove(media);
            //new code for stored procedure
            context.Database.ExecuteSqlRaw("execute sp_delete_media @p0",
                      parameters: new[] {media.MediaId.ToString()});

            //context.SaveChanges();
            return RedirectToAction("Index", "Media");
        }
    }
}
