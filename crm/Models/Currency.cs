using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace crm.Models
{
    [Index("Symbol", IsUnique = true, Name ="IDX_symbol_unique")]
    public class Currency
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Symbol nie moze byc pusty")]
        public string Symbol { get; set; }
        [Required(ErrorMessage = "Nazwa nie moze byc pusta")]
        public string Name { get; set; }
        public double Rate { get; set; }
        public bool Is_Sync { get; set; } = false;
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        public bool Ghosted { get; set; } = false;
    }
}
