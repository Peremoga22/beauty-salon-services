using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuatySalonWebApp.Models
{
    public class NailsAllModelCategoryAllNailsModel
    {
        public Guid NailsAllModelId { get; set; }
        public NailsAllModel NailsAllModel { get; set; }

        public int CategoryAllNailsModelId { get; set; }
        public CategoryAllNailsModel CategoryAllNailsModel { get; set; }
    }
}
