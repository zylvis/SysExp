using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SysExp.Models
{
    public class Strategy
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Strategy Name")]
        public string Name { get; set; }

        public byte[] Picture { get; set; }

        public string Details { get; set; }

        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }


    }
}
