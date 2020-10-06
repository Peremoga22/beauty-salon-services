using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuatySalonWebApp.Models
{
    public class MassageAllModelCategoryAllMassageModel
    {
        public Guid MassageAllModelId { get; set; }

        public MassageAllModel MassageAllModel { get; set; }

        public int CategoryMassageModelId { get; set; }
        public CategoryMassageModel CategoryMassageModel { get; set; }
    }
}
