using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SysExp.Models
{
    public class Description
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Picture { get; set; }
        public string TextArea1 { get; set; }
        public string TextArea2 { get; set; }
        public string TextArea3 { get; set; }
        public string TextArea4 { get; set; }


        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
    }
}
