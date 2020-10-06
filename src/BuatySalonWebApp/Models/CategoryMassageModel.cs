using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BuatySalonWebApp.Models
{
    public class CategoryMassageModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public List<MassageAllModelCategoryAllMassageModel> MassageAllModelCategory { get; set; }
    }
}
