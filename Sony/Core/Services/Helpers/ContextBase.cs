using Microsoft.EntityFrameworkCore;
using Sony.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sony.Core.Services.Helpers
{
        public abstract class ContextBase<TEntity> where TEntity : class, new()
        {
            protected readonly ApplicationDbContext _context;
            public ContextBase(ApplicationDbContext context)
            {
                _context = context;
            }



            protected DbSet<TEntity> __THIS__ => _context.Set<TEntity>();

            protected void Save()
            {
                _context.SaveChanges();
            }
            protected async Task SaveAsync()
            {
                await _context.SaveChangesAsync();
            }
        }
}
