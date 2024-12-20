using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Arcas.Server.Models
{
    /// <summary>
    /// 评论表 Comment
    /// 字段：commentID, ISBN, bookID, likeNum, commentatorID, commentContent
    /// </summary>
    public class Comment
    {
        /// <summary>
        /// 评论ID（主键）
        /// </summary>
        [Key]
        [Column("commentID")]
        public int commentID { get; set; }

        /// <summary>
        /// ISBN号（外键，引用AbstractBook）
        /// </summary>
        [Required]
        [StringLength(13)]
        [Column("ISBN")]
        public string ISBN { get; set; }

        /// <summary>
        /// 具体书ID（外键，引用ConcreteBook）
        /// </summary>
        [Column("bookID")]
        public int bookID { get; set; }

        /// <summary>
        /// 点赞数
        /// </summary>
        [Column("likeNum")]
        public int? likeNum { get; set; }

        /// <summary>
        /// 评论者ID（外键，引用User）
        /// </summary>
        [Column("commentatorID")]
        public int commentatorID { get; set; }

        /// <summary>
        /// 评论内容（文本类型）
        /// </summary>
        [Column("commentContent")]
        public string commentContent { get; set; }

        // 导航属性
        public virtual AbstractBook AbstractBook { get; set; }
        public virtual ConcreteBook ConcreteBook { get; set; }
        public virtual User User { get; set; }
    }
}
