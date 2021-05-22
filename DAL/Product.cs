using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName ="varchar(50)")]
        [Required]
        public string Name { get; set; }

        [Column(TypeName = "varchar(50)")]
        [Required]
        public string Description { get; set; }

        public decimal UnitPrice { get; set; }
        //[ForeignKey("Category")] //add either this or CategoryId to below nav property any one, both are correct ways
        public int CategoryId { get; set; }

        //adding parent table entity into child table
        //navtgation property
        //virtual is optional

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

    }
}
