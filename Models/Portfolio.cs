using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SysExp.Models
{
    public class Portfolio
    {
        [Key]
        public int Id { get; set; }

        [Display(Name="Portfolio Name")]
        [Required]
        public string Name { get; set; }

        public string Details { get; set; }

        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        [Display(Name = "Strategy")]
        public int StrategyId { get; set; }

        [ForeignKey("StrategyId")]
        public virtual Strategy Strategy { get; set; }

    }
}
