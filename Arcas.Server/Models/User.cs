using Arcas.Server.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Arcas.Server.Models
{
    public class User
    {
        [Key]
        [Column("userID")]
        public int userID { get; set; }

        [Column("username")]
        [StringLength(30)]
        public string username { get; set; }

        [Column("password")]
        [StringLength(30)]
        public string password { get; set; }

        [Column("nickname")]
        [StringLength(30)]
        public string nickname { get; set; }

        [Column("telephone")]
        [StringLength(11)]
        public string telephone { get; set; }

        [Column("token")]
        [StringLength(30)]
        public string token { get; set; }

        // 指明该集合与 ExchangeDetail.UserA 对应
        [InverseProperty("UserA")]
        public virtual ICollection<ExchangeDetail> ExchangeDetailsA { get; set; }

        // 指明该集合与 ExchangeDetail.UserB 对应
        [InverseProperty("UserB")]
        public virtual ICollection<ExchangeDetail> ExchangeDetailsB { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<ConcreteBook> ConcreteBooks { get; set; }
    }
}
