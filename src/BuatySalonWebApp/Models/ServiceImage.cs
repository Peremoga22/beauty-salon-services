using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuatySalonWebApp.Models
{
    public class ServiceImage
    {
        public int Id { get; set; }
        public string Nmae { get; set; }
        public virtual ModelService ModelService { get; set; }
    }
}
