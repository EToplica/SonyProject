using Sony.Core.Models;
using Sony.Core.Services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sony.Core.Services.Contract
{
    public interface IInvoiceService:IServiceBase<Invoice>
    {

        Invoice GetLastInvoice(int itemId);
        ICollection<Invoice> GetAllNotFinished();
        Invoice GetLastWithDetails(int itemId);
    }
}
