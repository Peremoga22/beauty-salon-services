using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BuatySalonWebApp.Models
{
    public class ContactUsIcon
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Icon { get; set; }
    }
}
