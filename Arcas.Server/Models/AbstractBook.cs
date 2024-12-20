using Arcas.Server.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Arcas.Server.Models
{
    /// <summary>
    /// 抽象书表 AbstractBook
    /// 字段：ISBN, title, author, publishHouse
    /// </summary>
    public class AbstractBook
    {
        /// <summary>
        /// ISBN码（主键）
        /// </summary>
        [Key]
        [StringLength(13)]
        [Column("ISBN")]
        public string ISBN { get; set; }

        /// <summary>
        /// 书名
        /// </summary>
        [StringLength(100)]
        [Column("title")]
        public string title { get; set; }

        [Column("category")]
        public int category { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        [StringLength(100)]
        [Column("author")]
        public string author { get; set; }

        /// <summary>
        /// 出版社
        /// </summary>
        [StringLength(100)]
        [Column("publishHouse")]
        public string publishHouse { get; set; }

        // 导航属性
        public virtual ICollection<ConcreteBook> ConcreteBooks { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
