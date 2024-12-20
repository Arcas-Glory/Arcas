using Arcas.Server.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Arcas.Server.Models
{
    public class ExchangeDetail
    {
        [Key]
        [Column("exchangeID")]
        public int exchangeID { get; set; }

        [Column("IDA")]
        public int ida { get; set; }

        [Column("IDB")]
        public int idb { get; set; }

        [Column("bookID")]
        public int bookID { get; set; }

        [StringLength(30)]
        [Column("trackingID")]
        public string trackingID { get; set; }

        [Column("isApproved")]
        public bool isApproved { get; set; }

        [StringLength(100)]
        [Column("content")]
        public string content { get; set; }

        [StringLength(100)]
        [Column("detailsA")]
        public string detailsA { get; set; }

        [StringLength(100)]
        [Column("detailsB")]
        public string detailsB { get; set; }

        [ForeignKey("ida")]
        [InverseProperty("ExchangeDetailsA")]
        public virtual User UserA { get; set; }

        [ForeignKey("idb")]
        [InverseProperty("ExchangeDetailsB")]
        public virtual User UserB { get; set; }

        public virtual ConcreteBook ConcreteBook { get; set; }
    }
}
