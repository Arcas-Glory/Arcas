using Arcas.Server.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Arcas.Server.Models
{
    /// <summary>
    /// 具体书表 ConcreteBook
    /// 字段：bookID, ISBN, userID, bookState, cover
    /// </summary>
    public class ConcreteBook
    {
        /// <summary>
        /// 具体书ID（主键）
        /// </summary>
        [Key]
        [Column("bookID")]
        public int bookID { get; set; }

        /// <summary>
        /// ISBN码（外键，引用AbstractBook）
        /// </summary>
        [Required]
        [StringLength(13)]
        [Column("ISBN")]
        public string ISBN { get; set; }

        /// <summary>
        /// 用户ID（持有者，外键，引用User）
        /// </summary>
        [Column("userID")]
        public int userID { get; set; }

        /// <summary>
        /// 书籍状态
        /// </summary>
        [Column("bookState")]
        public int bookState { get; set; }

        /// <summary>
        /// 封面（varchar类型，这里未限制长度，可根据需要调整）
        /// </summary>
        [Column("cover")]
        public string cover { get; set; }

        // 导航属性
        [ForeignKey("ISBN")]
        public virtual AbstractBook AbstractBook { get; set; }

        [ForeignKey("userID")]
        public virtual User User { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<ExchangeDetail> ExchangeDetails { get; set; }
    }
}
