using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [ReadOnly(true)]
        public string Symbol { get; set; }
        [Required(ErrorMessage = "Nazwa nie moze byc pusta")]
        public string Name { get; set; }
        public double Rate { get; set; }
        public bool Is_Sync { get; set; } = false;
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Created_at { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Updated_at { get; set; }
        public bool Ghosted { get; set; } = false;
    }
}
