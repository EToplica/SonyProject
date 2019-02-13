using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sony.Core.Models
{
    public class Item
    {
        public int Id { get; set; }

        
        public string Name { get; set; }

        public int ItemTypeId { get; set; }
        public ItemType ItemType { get; set; }

        public bool IsAvailable { get; set; }
    }
}
