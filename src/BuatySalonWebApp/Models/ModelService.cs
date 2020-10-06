using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BuatySalonWebApp.Models
{
    public class ModelService
    {
        public ModelService()
        {
            ServiceImage = new List<ServiceImage>();
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
        [Required]
        public string Skills { get; set; }
        [Required]
        public string ProjectUrl { get; set; }

        public string Url { get; set; }
        public int SortNumber { get; set; }

        public List<ServiceCategory> ServiceCategories { get; set; }
        public virtual ICollection<ServiceImage> ServiceImage { get; set; }
    }
}
