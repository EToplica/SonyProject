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
    public class ProductService : ServiceBase<Product>, IProductService
    {
        public ProductService(ApplicationDbContext context) : base(context)
        {
        }
    }
}
