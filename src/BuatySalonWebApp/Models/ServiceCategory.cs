using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuatySalonWebApp.Models
{
    public class ServiceCategory
    {
        public Guid ServiceId { get; set; }
        public ModelService ModelService { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
