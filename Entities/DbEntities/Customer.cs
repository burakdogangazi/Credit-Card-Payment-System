using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.DbEntities
{
    public class Customer:DbEntity
    {
        //[ForeignKey("BankConfiguration")]
        //public int BankConfigurationId { get; set; }
        //public virtual BankConfiguration? BankConfiguration { get; set; }
        /// <summary>
        /// Müşteriye ait.
        /// </summary>
        [StringLength(50)]
        [Column(TypeName = "varchar")]
        public string? CustomerName { get; set; }
    }
}
