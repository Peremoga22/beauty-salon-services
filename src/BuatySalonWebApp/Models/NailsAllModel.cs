using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BuatySalonWebApp.Models
{
    public class NailsAllModel
    {
        public NailsAllModel()
        {
            NailsImages = new List<NailsImage>();
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
        public List<NailsAllModelCategoryAllNailsModel> NailsAllModelCategories { get; set; }
        public virtual ICollection<NailsImage> NailsImages { get; set; }
    }
}
