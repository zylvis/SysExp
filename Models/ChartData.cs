using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SysExp.Models
{
    public class ChartData
    {
        [Key]
        public int Id { get; set; }

        [DataType(DataType.Date)]
        [Required]
        public DateTime Date { get; set; }

        public string DateToString { get; set; }

        public int Equity { get; set; }

        public int PortfolioId { get; set; }
        [ForeignKey("PortfolioId")]
        public virtual Portfolio Portfolio { get; set; }

        public int StrategyId { get; set; }
        [ForeignKey("StrategyId")]
        public virtual Strategy Strategy { get; set; }


    }


}
