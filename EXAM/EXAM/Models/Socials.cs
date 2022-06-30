using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EXAM.Models
{
    public class Socials
    {
        public int Id { get; set; }
        [Required]
        public string IconClass { get; set; }
        [Required]
        public string Link { get; set; }
        [Required]
        public int TeamId { get; set; }
        public Team Team { get; set; }
    }
}
