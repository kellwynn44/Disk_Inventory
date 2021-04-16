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
    }
}
