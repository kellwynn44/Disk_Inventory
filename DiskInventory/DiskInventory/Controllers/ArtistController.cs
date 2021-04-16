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
            var artists = context.Artists.OrderBy(a => a.LastName).ThenBy(a => a.FirstName).ToList();
            return View(artists);
        }
    }
}
