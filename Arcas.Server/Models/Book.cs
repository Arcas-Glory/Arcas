using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sqlTest.Server.Models
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string ISBN { get; set; }
        [Required]
        public string OwnerId { get; set; }
        [Required]
        [MaxLength(100)]
        public string Title {  get; set; }


        [MaxLength(1000)] 
        public string Description {  get; set; }
        //navi attribute
        [ForeignKey("OwnerId")]
        public User Owner { get; set; }
    }
}
