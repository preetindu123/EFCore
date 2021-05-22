using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }
        [Column(TypeName = "varchar(50)")]

        [Required]
        public string Name { get; set; }

        //nav property of child. not compulsary but you can do it this way
        //virtual is optional
        public virtual ICollection<Product> Products { get; set; }
    }
}
