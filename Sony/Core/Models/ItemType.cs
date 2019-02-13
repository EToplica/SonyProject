using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sony.Core.Models
{
    public class ItemType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<ItemType> Items { get; set; }
    }
}
