using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BuatySalonWebApp.Models
{
    public class ContactUs
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string LongDescription { get; set; }
        [Required]
        public string ShortDescription { get; set; }

        public virtual ICollection<ContactUsInfo> ContactUsInfo { get; set; }
    }
}
