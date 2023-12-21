using System.ComponentModel.DataAnnotations;

namespace Service.Catalog.API.Models
{
    public class Product
    {
        [Key]
        public virtual int ProductId { get; set; }
        [Required]
        public virtual string Name { get; set; }
        [Range(1,1_000_000)]
        public virtual double Price { get; set; }
        public virtual string Description { get; set; }
        public virtual string CategoryName { get; set; }
        public virtual string ImageURL { get; set; }        

        //! Other properties

    }
}