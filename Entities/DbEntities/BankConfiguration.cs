using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.DbEntities
{
    public class BankConfiguration : DbEntity
    {
        public BankConfiguration()
        {
            CreditCardConfigurations = new List<CreditCardConfiguration>();
        }

        [StringLength(50)]
        [Column(TypeName ="varchar")]
        public string BankName { get; set; }

        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
        [Column(TypeName = "varchar")]
        public string BankType { get; set; }

        /// <summary>
        /// Banka ödeme işlemleri için yine bankadan alınan
        /// Müşteri Bilgisidir.
        /// </summary>
        [StringLength(50)]
        [Column(TypeName = "varchar")]
        public string CustomerName { get; set; }

        /// <summary>
        /// Bankadan alınan müşteri bilgisine ait şifre bilgisidir.
        /// </summary>
        [StringLength(50)]
        [Column(TypeName = "varchar")]
        public string CustomerPassword { get; set; }

        /// <summary>
        /// Banka ikili güvenlik doğrulaması için doğrulama adresi.
        /// Bu adres 3D Secure için olabilir.
        /// </summary>
        [StringLength(500)]
        [Column(TypeName = "varchar")]
        public string ValidityAddress { get; set; }

        /// <summary>
        /// Banka ödeme adresi.
        /// </summary>
        [StringLength(500)]
        [Column(TypeName ="varchar")]
        public string PaymentAddress { get; set; }

        [StringLength(50)]
        [Column(TypeName = "varchar")]
        public string TerminalNumber { get; set; }


        public ICollection<CreditCardConfiguration> CreditCardConfigurations { get; set; }
    }
}
