using Microsoft.EntityFrameworkCore;
using Sony.Core.Models;
using Sony.Core.Services.Contract;
using Sony.Core.Services.Helpers;
using Sony.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sony.Core.Services.Implementation
{
    public class InvoiceDetailService : ServiceBase<InvoiceDetail>, IInvoiceDetailService
    {
        public InvoiceDetailService(ApplicationDbContext context) : base(context)
        {
        }

        public List<InvoiceDetail> GetAllNotFinished()
        {
            return __THIS__.Include(x=>x.Product).Include(x => x.Invoice).Where(x => x.Invoice.IsFinished == false).ToList();
        }
    }
}
