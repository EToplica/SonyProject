using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Sony.Core.Models;
using Sony.Core.Models.ViewModels;
using Sony.Core.Services.Contract;
using Sony.Data;
using Sony.Models;

namespace Sony.Controllers
{    

    [Route("api/[controller]")]
    public class InvoicesController : BaseController
    {
        private readonly IInvoiceService _invoiceService;
        private readonly IItemService _itemService;
        private readonly IInvoiceDetailService _invoiceDetailService;

        public InvoicesController(UserManager<ApplicationUser> userManager, IInvoiceService invoiceService, IItemService itemService, IInvoiceDetailService invoiceDetailService) : base(userManager)
        {
            _invoiceService = invoiceService;
            _itemService = itemService;
            _invoiceDetailService = invoiceDetailService;
        }

        [HttpPost, Route("start")]
        public IActionResult Start([FromBody]StartInvoiceViewModel model)
        {
            var newInvoice = new Invoice()
            {
                ItemId = model.ItemId,
                HasPayed = (bool)model.HasPayed,
                Players = model.Players,
                StartAt = DateTime.Now,
                InsertedById = UserId
            };

            var item = _itemService.Get((int)model.ItemId);
            item.IsAvailable = false;

            try
            {
                _invoiceService.Add(newInvoice);
                _itemService.Update(item);
                return Ok(true);

            }
            catch (Exception ex)
            {
                Debugger.Break();
                return Ok(false);

            }

        }

        [HttpGet, Route("last/{itemId}")]
        public IActionResult LastInvoice(int itemId)
        {
            var lastInvoice = _invoiceService.GetLastInvoice(itemId);

           
            if (lastInvoice != null)
                return Ok(JsonConvert.SerializeObject(lastInvoice, Formatting.Indented, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }));
            return Ok("notFound");
        }

        [HttpGet, Route("details")]
        public IActionResult InitDetails()
        {
            var inv = new List<Invoice>();
            var allDetails = _invoiceService.GetAllNotFinished().GroupBy(z => z.ItemId);
            foreach (var item in allDetails)
            {
                foreach (var itm in item)
                {
                    inv.Add(itm);
                }
            }
            return Ok(JsonConvert.SerializeObject(inv, Formatting.Indented, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            }));
        }
        [HttpPost, Route("stop")]
        public IActionResult Stop([FromBody] ItemDetailViewModel item)
        {
            //kur don me bo update:
            //var invoice = _invoiceService.GetLastWithDetails(item.ItemId);
            //invoice.InvoiceDetails = new HashSet<InvoiceDetail>();
            ////_invoiceDetailService.RemoveRange(invoice.InvoiceDetails);
            ///
            try
            {
                var itm = _itemService.Get(item.ItemId);
                itm.IsAvailable = true;

                var invoice = _invoiceService.GetLastWithDetails(item.ItemId);
                invoice.Stop();
                invoice.HasPayed = true;
                invoice.IsFinished = true;


                _itemService.Update(itm);
                item.ProductDetails.ForEach(x => invoice.InvoiceDetails.Add(new InvoiceDetail(x.ProductId, x.Quantity)));
                _invoiceService.Update(invoice);
                return Ok(true);
            }
            catch(Exception ex)
            {
                return Ok(false);
            }
           
        }

    }

   
}
