using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EXAM.Models
{
    public class Team
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Position { get; set; }
        public string Img { get; set; }
        [Required]
        public string Desc { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }

        public List<Socials> Socials { get; set; }
    }
}
