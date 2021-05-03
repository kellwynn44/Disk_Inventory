using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiskInventory.Models;
using Microsoft.EntityFrameworkCore;

namespace DiskInventory.Controllers
{
    public class ArtistController : Controller
    {
        private diskInventoryMPContext context { get; set; }

        public ArtistController(diskInventoryMPContext ctx)
        {
            context = ctx;
        }
        public IActionResult Index()
        {
            //var artists = context.Artists.OrderBy(a => a.LastName).ThenBy(a => a.FirstName).ToList();
            var artists = context.Artists.OrderBy(a => a.LastName).ThenBy(a => a.FirstName).Include(t => t.ArtistType).ToList();
            return View(artists);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            ViewBag.ArtistTypes = context.ArtistTypes.OrderBy(t => t.Description).ToList();
            return View("Edit", new Artist());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            ViewBag.ArtistTypes = context.ArtistTypes.OrderBy(t => t.Description).ToList();
            var artists = context.Artists.Find(id);
            return View(artists);
        }

        [HttpPost]
        public IActionResult Edit(Artist artist)
        {
            if (ModelState.IsValid)
            {
                if (artist.ArtistId == 0)
                {
                    //context.Artists.Add(artist);
                    //new code to use stored procedure
                    context.Database.ExecuteSqlRaw("execute sp_insert_artist @p0, @p1, @p2",
                        parameters: new[] {artist.FirstName, artist.LastName,
                        artist.ArtistTypeId.ToString()});
                }

                else
                {
                    //context.Artists.Update(artist);
                    //new code to use stored procedure
                    context.Database.ExecuteSqlRaw("execute sp_update_artist @p0, @p1, @p2, @p3",
                      parameters: new[] {artist.ArtistId.ToString(), artist.FirstName, artist.LastName,
                        artist.ArtistTypeId.ToString()});
                }
                    
                //context.SaveChanges();
                return RedirectToAction("Index", "Artist");
            }
            else
            {
                ViewBag.Action = (artist.ArtistId == 0) ? "Add" : "Edit";
                ViewBag.ArtistTypes = context.ArtistTypes.OrderBy(t => t.Description).ToList();
                return View(artist);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var artist = context.Artists.Find(id);
            return View(artist);
        }

        [HttpPost]
        public IActionResult Delete(Artist artist)
        {
            //context.Artists.Remove(artist);
            //new code for stored procedure
            context.Database.ExecuteSqlRaw("execute sp_delete_artist @p0",
                      parameters: new[] {artist.ArtistId.ToString()});

            //context.SaveChanges();
            return RedirectToAction("Index", "Artist");
        }
    }
}
