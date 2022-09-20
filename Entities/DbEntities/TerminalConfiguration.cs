using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DbEntities
{
    public class TerminalConfiguration:DbEntity
    {
        [ForeignKey("BankConfiguration")]
        public int BankConfigurationId { get; set; }

        public BankConfiguration? BankConfiguration { get; set; }

        [StringLength(50)]
        [Column(TypeName ="varchar")]
        public string TerminalNumber { get; set; }
    }
}
