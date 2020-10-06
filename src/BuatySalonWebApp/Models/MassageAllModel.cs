using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BuatySalonWebApp.Models
{
    public class MassageAllModel
    {
        public MassageAllModel()
        {
            MassageImages = new List<MassageImage>();
        }
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string LongDescription { get; set; }
        [Required]
        public string ShortDescription { get; set; }
        [Required]
        public string Image { get; set; }
        public string Url { get; set; }
        public int SortNumber { get; set; }
        public virtual ICollection<MassageImage> MassageImages { get; set; }
        public List<MassageAllModelCategoryAllMassageModel> MassageAllModelCategories { get; set; }
    }
}
