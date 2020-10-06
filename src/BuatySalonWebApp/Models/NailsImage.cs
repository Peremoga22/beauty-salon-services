using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuatySalonWebApp.Models
{
    public class NailsImage
    {
        public int Id { get; set; }
        public string Nmae { get; set; }
        public NailsAllModel NailsAllModel { get; set; }
    }
}
