using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sony.Core.Models;
using Sony.Core.Services.Contract;
using Sony.Data;

namespace Sony.Controllers
{
    public class ItemsController : Controller
    {
        
        private readonly IItemService _itemService;
        private readonly IItemTypeService _itemTypeService;

        public ItemsController(IItemService itemService,IItemTypeService itemTypeService)
        {
            _itemService = itemService;
            _itemTypeService = itemTypeService;
        }

        // GET: Items
        public IActionResult Index()
        {

            return View(_itemService.GetAll());
        }

        // GET: Items/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var item = await _itemService.GetAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Items/Create
        public IActionResult Create()
        {
            ViewData["ItemTypeId"] = new SelectList(_itemTypeService.GetAll(), "Id", "Name");
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ItemTypeId,IsAvailable")] Item item)
        {
            if (ModelState.IsValid)
            {
                _itemService.Add(item);
                return RedirectToAction(nameof(Index));
            }
            ViewData["ItemTypeId"] = new SelectList(_itemTypeService.GetAll(), "Id", "Name", item.ItemTypeId);
            return View(item);
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _itemService.GetAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            ViewData["ItemTypeId"] = new SelectList(_itemTypeService.GetAll(), "Id", "Name", item.ItemTypeId);
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ItemTypeId,IsAvailable")] Item item)
        {
            if (id != item.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _itemService.Update(item);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ItemTypeId"] = new SelectList(_itemService.GetAll(), "Id", "Id", item.ItemTypeId);
            return View(item);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
     

            var item = await _itemService.GetAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _itemService.Remove(_itemService.Get(id));
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(int id)
        {
            return _itemService.GetAll().Any(e => e.Id == id);
        }
    }
}
