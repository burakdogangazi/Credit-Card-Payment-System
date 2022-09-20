using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.DbEntities
{
    public class CreditCardConfiguration : DbEntity
    {
        [ForeignKey("BankConfiguration")]
        public int BankConfigurationId { get; set; }

        public virtual BankConfiguration? BankConfiguration { get; set; }

        /// <summary>
        /// Bankaya ait Kredi Kart Tipi.
        /// Bellirli Konfigürasyonlar için kullanılabilir.
        /// </summary>
        [StringLength(3)]
        [Column(TypeName ="varchar")]
        public string? CardType { get; set; }

        /// <summary>
        /// Banka ile anlaşma sağlanan taksit miktarı.
        /// </summary>
        public int Installment { get; set; }

        /// <summary>
        /// Kredi kartı benzersiz başlangıç numarasıdır.
        /// İlk 6 hane için "Uluslararası Kimlik Numarasıdır".
        /// </summary>
        [StringLength(6)]
        [Column(TypeName ="varchar")]
        public string? BinNumber { get; set; }
    }

}
